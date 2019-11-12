using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

using VideoRecorder.UI;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.QrCode;

//Install-Package Barcode
//Install-Package AForge
//Install-Package AForge.Video
//Install-Package AForge.Video.DirectShow
//Install-Package ZXing.Net -Version 0.16.5
//Install-Package BarCode -Version 4.0.2.2

namespace VideoRecorder
{
    internal class MainWindowViewModel : ObservableObject, IDisposable
    {

        string previous = "";
        //attendanceHelper.Show();

        #region Private fields

        private FilterInfo _currentDevice;

        private BitmapImage _image;
        private string _ipCameraUrl;

        private bool _isDesktopSource;
        private bool _isIpCameraSource;
        private bool _isWebcamSource;

        private IVideoSource _videoSource;
        private VideoFileWriter _writer;
        private bool _recording;
        private DateTime? _firstFrameTime;

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            VideoDevices = new ObservableCollection<FilterInfo>();
            GetVideoDevices();
            IsDesktopSource = true;
            StartSourceCommand = new RelayCommand(StartCamera);
            StopSourceCommand = new RelayCommand(StopCamera);
            IpCameraUrl = "";
        }

        #endregion

        #region Properties

        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public BitmapImage Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }

        public bool IsDesktopSource
        {
            get { return _isDesktopSource; }
            set { Set(ref _isDesktopSource, value); }
        }

        public bool IsWebcamSource
        {
            get { return _isWebcamSource; }
            set { Set(ref _isWebcamSource, value); }
        }

        public bool IsIpCameraSource
        {
            get { return _isIpCameraSource; }
            set { Set(ref _isIpCameraSource, value); }
        }

        public string IpCameraUrl
        {
            get { return _ipCameraUrl; }
            set { Set(ref _ipCameraUrl, value); }
        }
        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { Set(ref _currentDevice, value); }
        }

        public ICommand StartSourceCommand { get; private set; }

        public ICommand StopSourceCommand { get; private set; }

        #endregion

        private void GetVideoDevices()
        {
            var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in devices)
            {
                VideoDevices.Add(device);
            }
            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                MessageBox.Show("No webcam found");
            }
        }

        private void StartCamera()
        {
            if (IsDesktopSource)
            {
                var rectangle = new Rectangle();
                foreach (var screen in System.Windows.Forms.Screen.AllScreens)
                {
                    rectangle = Rectangle.Union(rectangle, screen.Bounds);
                }
                _videoSource = new ScreenCaptureStream(rectangle);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
            else if (IsWebcamSource)
            {
                if (CurrentDevice != null)
                {
                    _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                    _videoSource.NewFrame += video_NewFrame;
                    _videoSource.Start();
                }
                else
                {
                    MessageBox.Show("Current device can't be null");
                }
            }
            else if (IsIpCameraSource)
            {
                _videoSource = new MJPEGStream(IpCameraUrl);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                using (var bitmap = (Bitmap) eventArgs.Frame.Clone())
                {
                    var bi = bitmap.ToBitmapImage();
                    //--------------------------------
                    Bitmap img = (Bitmap)(BitmapImage2Bitmap(bi));
                    if (img != null)
                    {
                        try
                        {
                            ZXing.BarcodeReader Reader = new ZXing.BarcodeReader();
                            Result result = Reader.Decode(img);
                            try
                            {
                                string decoded = result.ToString();
                                //bool alreadyScanned = attendanceHelper.richTextBox.Text.Contains(decoded);
                                if (decoded != previous)
                                {
                                    previous = decoded;
                                    Console.Beep();
                                    //MessageBox.Show(decoded);
                                    // services.UpdateDetails(dayTextBox.Text, decoded, Import_FileName);
                                    richTextBox.AppendText (  decoded + "  " + DateTime.Now.ToString("HH:mm:ss") + "\n");
                                    
                                    //count++;
                                    //wait = 0;
                                }
                                else
                                {
                                    if (10<0)
                                    {
                                        Console.Beep();
                                        Console.Beep();
                                        Console.Beep();
                                        Console.Beep();
                                    }
                                    //wait++;
                                }
                                img.Dispose();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message + "");
                            }
                        }
                        catch { }
                    }
                        //--------------------------------
                        bi.Freeze();
                    Dispatcher.CurrentDispatcher.Invoke(() => Image = bi);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                StopCamera();
            }
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= video_NewFrame;
            }
            Image = null;
        }

        public void Dispose()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
            }
            _writer?.Dispose();
        }
    }
}
