using CsPotrace;
using KinectToolkit;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private WriteableBitmap colorBitmap;

        private KinectOutputToImage kinectOutput;
        private bool isTreatingPath = false;

        public GameUserControl(MainWindow container)
        {
            InitializeComponent();
            currentTime = TIME_TO_PLAY;
            windowContainer = container;
            /*kinectOutput = new KinectOutputToImage();
            kinectOutput.ImageReady += OnImageReady;
            kinectOutput.ImageReady += OnGameStart;*/
        }

        public void OnImageReady(object sender, BitmapSource sourceImage)
        {
            KinectImage.Source = sourceImage;
            Bitmap b = SourceToBitmapConverter.ConvertToBitmap(sourceImage);
            RefreshScore(b);
        }


        private void RefreshScore(Bitmap sourceImage)
        {
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
                System.Timers.Timer t = new System.Timers.Timer(1000);
                t.Elapsed += UpdateTime;
                t.Start();
                kinectOutput.ImageReady -= OnGameStart;
            }
        }

        private void UpdateTime(object source, System.Timers.ElapsedEventArgs g)
        {
            currentTime -= 1;
            Debug.WriteLine(currentTime);
            Application.Current.Dispatcher.Invoke(() =>
            {
                textTime.Text = currentTime.ToString();
            });
            if (currentTime <= 0)
            {
                OnGameEnd();
                return;
            }
            if (currentTime <= 1)
            {
                kinectOutput.kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
                this.colorBitmap = new WriteableBitmap(kinectOutput.kinectSensor.ColorStream.FrameWidth, kinectOutput.kinectSensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                kinectOutput.kinectSensor.ColorFrameReady += OnPhotoReady;
            }
        }

        
        public void OnPhotoReady(object sender, ColorImageFrameReadyEventArgs args)
        {
            if (this.colorBitmap == null)
                return;
            ColorImageFrame frame = args.OpenColorImageFrame();
            byte[] colorPixels = new byte[kinectOutput.kinectSensor.ColorStream.FramePixelDataLength * sizeof(int)];
            frame.CopyPixelDataTo(colorPixels);
            frame.Dispose();
            this.colorBitmap.WritePixels(
                        new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                        colorPixels,
                        this.colorBitmap.PixelWidth * sizeof(int),
                        0);
        }

        private void OnGameEnd()
        {
            kinectOutput.kinectSensor.Dispose();
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Source = this.colorBitmap;

            //windowContainer.LoadContent(new ScoreUserControl(windowContainer, img, textScore.Text, "A SPECIFIER"));

            ///Pour sauvegarder l'image
            /*RenderTargetBitmap bitmap = new RenderTargetBitmap(
                640,
                480,
                96,
                96,
                PixelFormats.Pbgra32);
            bitmap.Render(img);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }*/
        }
    }
}
