﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Drawing;
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

namespace TeacherAssistant_v1.UI
{
    /// <summary>
    /// Interaction logic for TakeAttendance.xaml
    /// </summary>
    public partial class TakeAttendance : Window
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        String Import_FileName;
        long count = 1;
        int wait = 0;
        String previous = "";

        public TakeAttendance()
        {
            InitializeComponent();
        }
        void OnLoad(object sender, RoutedEventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
            {
                cameraComboBox.Items.Add(Device.Name);
            }
            cameraComboBox.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }
        private void FinalFrame_NewFrame(Object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bmp = (BitmapImage)eventArgs.Frame.Clone();
                pictureBox.Source = bmp;
            }
            catch { }

        }
        private void detectButton_Click(object sender, RoutedEventArgs e)
        {
            if (/*services.Check(dayTextBox.Text)*/true)
            {
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cameraComboBox.SelectedIndex].MonikerString);
                videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            }
            else
            {
                MessageBox.Show("Day" + dayTextBox.Text + " not found !");
            }
        }

        private void scanButton_Click(object sender, RoutedEventArgs e)
        {
            
                videoCaptureDevice.Start();
                //timer1.Start();
        }
        /*
        public string Process(Bitmap bitmap)
        {
            var reader = new QRCodeReader();

            try
            {
                LuminanceSource source = new RGBLuminanceSource(bitmap, bitmap.Width, bitmap.Height);
                var binarizer = new HybridBinarizer(source);
                var binBitmap = new BinaryBitmap(binarizer);
                return reader.decode(binBitmap).Text;
            }
            catch (Exception e)
            {
                //catch the exception and return null instead
                return null;
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            BitmapImage img = (BitmapImage)pictureBox.Source;
            if (img != null)
            {
                try
                {
                    QRCodeReader Reader = new QRCodeReader();
                    Result result = Reader.Decode(img);
                    try
                    {
                        string decoded = result.ToString();
                        //bool alreadyScanned = richTextBox.Text.Contains(decoded);
                        if (decoded != previous )
                        {
                            previous = decoded;
                            Console.Beep();
                            //MessageBox.Show( count + ": " + services.ShowName(decoded) + "  " + decoded + "  " + DateTime.Now.ToString("HH:mm:ss") + "\n");
                            count++;
                            wait = 0;
                        }
                        else
                        {
                            if (wait == 8)
                            {
                                Console.Beep();
                                Console.Beep();
                                Console.Beep();
                                Console.Beep();
                            }
                            wait++;
                        }
                        img.Freeze();
                        img.EndInit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "");
                    }
                }
                catch { }
            }
        }
        */
    }
}