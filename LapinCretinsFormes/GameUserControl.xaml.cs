using CsPotrace;
using KinectToolkit;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour GameUserControl.xaml
    /// </summary>
    public partial class GameUserControl : UserControl
    {
        private MainWindow windowContainer;

        private const int TIME_TO_PLAY = 20;
        private int currentTime = 0;
        private bool gameHasStart = false;
        private System.Windows.Threading.DispatcherTimer gameTimer;

        private WriteableBitmap colorBitmap;

        private KinectOutputToImage kinectOutput;
        private bool isTreatingPath = false;

        public GameUserControl(MainWindow container)
        {
            InitializeComponent();
            //LoadShape(ShapeDataBase.GetRandomShape());
            textTime.Text = TIME_TO_PLAY.ToString();
            currentTime = TIME_TO_PLAY;
            windowContainer = container;
            kinectOutput = new KinectOutputToImage();
            kinectOutput.ImageReady += OnImageReady;
            kinectOutput.ImageReady += OnGameStart;

            kinectOutput.kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
            this.colorBitmap = new WriteableBitmap(kinectOutput.kinectSensor.ColorStream.FrameWidth, kinectOutput.kinectSensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
        }

        private void LoadShape(Shape shape)
        {
            shadePath.Data = shape.getShapeGeometry();
            BitmapImage backImg = new BitmapImage();
            backImg.BeginInit();
            backImg.UriSource = shape.getImageUri();
            backImg.EndInit();
            backgroundImage.Source = backImg;
        }

        public void OnImageReady(object sender, BitmapSource sourceImage)
        {
            KinectImage.Source = sourceImage;
            Bitmap b = SourceToBitmapConverter.ConvertToBitmap(sourceImage);
            RefreshScore(b);
        }


        private void RefreshScore(Bitmap sourceImage)
        {
            Debug.WriteLine("Treat Score");
            if (isTreatingPath)
                return;
            isTreatingPath = true;
            Thread t = new Thread(() => CalculScore(sourceImage));
            t.Start();
        }


        private ScoreManager scoreManager = new ScoreManager();

        private void CalculScore(Bitmap s)
        {
            BitmapToXamlPath g = new BitmapToXamlPath();
            string r = g.ConvertBitmap(s);
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    textScore.Text = scoreManager.ScoreFromGeometries(shadePath.Data, Geometry.Parse(r)).ToString();
                });
            }
            catch (Exception e) { }
            isTreatingPath = false;
        }


        public void OnGameStart(object sender, BitmapSource firstImage)
        {
            if (!gameHasStart)
            {
                gameHasStart = true;
                gameTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
                gameTimer.Tick += UpdateTime;
                gameTimer.Start();
                kinectOutput.ImageReady -= OnGameStart;
            }
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            currentTime -= 1;
            Debug.WriteLine(currentTime);
            Application.Current.Dispatcher.Invoke(() =>
            {
                textTime.Text = currentTime.ToString();
            });
            if (currentTime <= 0)
            {
                gameTimer.Stop();
                OnGameEnd();
                return;
            }
            if (currentTime <= 1)
                kinectOutput.kinectSensor.ColorFrameReady += OnPhotoReady;
        }

        public void OnPhotoReady(object sender, ColorImageFrameReadyEventArgs args)
        {
            ColorImageFrame frame = args.OpenColorImageFrame();
            if (frame == null) return;
            byte[] colorPixels = new byte[kinectOutput.kinectSensor.ColorStream.FramePixelDataLength];
            frame.CopyPixelDataTo(colorPixels);
            frame.Dispose();
            
            Application.Current.Dispatcher.Invoke(() =>
            {
            this.colorBitmap.WritePixels(
                        new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                        colorPixels,
                        this.colorBitmap.PixelWidth * sizeof(int),
                        0);
            });
        }

        private void OnGameEnd()
        {
            Debug.WriteLine("End Of Game");
            kinectOutput.kinectSensor.ColorStream.Disable();
            kinectOutput.kinectSensor.DepthStream.Disable();
            Application.Current.Dispatcher.Invoke(() =>
            {
                windowContainer.LoadContent(new ScoreUserControl(windowContainer, this.colorBitmap, textScore.Text, "NON UTILISE ACTUELLEMENT"));
            });
            kinectOutput.kinectSensor.ColorFrameReady -= OnPhotoReady;
            kinectOutput.RemoveSubscriptions();

            BitmapFrame frame = BitmapFrame.Create(colorBitmap);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(frame);
            using (var stream = File.Create("./Temp/Photo.jpeg"))
            {
                encoder.Save(stream);
                stream.Close();
            }
            encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(frame);
            using (var stream = File.Create(GetPhotoName()))
            {
                encoder.Save(stream);
                stream.Close();
            }
        }

        private string GetPhotoName()
        {
            String s = String.Format("./GamePictures/{0:dd-MM-yy H:mm_ss*}.jpeg", System.DateTime.Now);
            return s.Replace(":", "h").Replace("_", "m").Replace("*", "s");
        }
    }
}
