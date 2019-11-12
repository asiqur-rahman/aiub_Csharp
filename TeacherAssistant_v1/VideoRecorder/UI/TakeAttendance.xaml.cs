using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge.Video;
using System.IO;
using AForge.Video.DirectShow;
using ZXing;

//Install-Package Barcode
//Install-Package AForge
//Install-Package AForge.Video
//Install-Package AForge.Video.DirectShow
//Install-Package ZXing.Net -Version 0.16.5
//Install-Package BarCode -Version 4.0.2.2

namespace VideoRecorder.UI
{
    /// <summary>
    /// Interaction logic for TakeAttendance.xaml
    /// </summary>
    public partial class TakeAttendance : Window, INotifyPropertyChanged
    {
        string previous="";
        #region Public properties

        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { _currentDevice = value; this.OnPropertyChanged("CurrentDevice"); }
        }
        private FilterInfo _currentDevice;

        #endregion


        #region Private fields

        private IVideoSource _videoSource;

        #endregion

        public TakeAttendance()
        {
            InitializeComponent();
            this.DataContext = this;
            GetVideoDevices();
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopCamera();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            StartCamera();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
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
                                //bool alreadyScanned = richTextBox.Text.Contains(decoded);
                                if (decoded != previous)
                                {
                                    previous = decoded;
                                    Console.Beep();
                                    MessageBox.Show(decoded);
                                    //richTextBox.Document.Blocks.Add(new Paragraph(new Run(decoded + "  " + DateTime.Now.ToString("HH:mm:ss") + "\n")));
                                    richTextBox.AppendText(decoded + "  " + DateTime.Now.ToString("HH:mm:ss") + "\n");

                                    //count++;
                                    //wait = 0;
                                }
                                else
                                {
                                    if (10 < 0)
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
                }
                bi.Freeze(); // avoid cross thread operations and prevents leaks
                Dispatcher.BeginInvoke(new ThreadStart(delegate { videoPlayer.Source = bi; }));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopCamera();
        }

        private void GetVideoDevices()
        {
            VideoDevices = new ObservableCollection<FilterInfo>();
            foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
            {
                VideoDevices.Add(filterInfo);
            }
            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                MessageBox.Show("No video sources found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartCamera()
        {
            if (CurrentDevice != null)
            {
                _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
            }
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion
    }
}

