using CsPotrace;
using KinectToolkit;
using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour GameUserControl.xaml
    /// </summary>
    public partial class GameUserControl : UserControl
    {
        private IUserControlContainer _windowContainer;

        private GameManager _gameManager;

        private const int TIME_TO_PLAY = 20;
        private int _currentTime = 0;
        private bool _gameHasStarted = false;
        private System.Windows.Threading.DispatcherTimer _gameTimer;

        private WriteableBitmap _colorBitmap;

        private KinectOutputToImage _kinectOutput;
        private bool _isTreatingPath = false;
        
        private Window loadWindow;

        public GameUserControl(IUserControlContainer container, GameManager gameManager)
        {
            loadWindow = new LoadingWindow("Calibrage de la Kinect . . .");
            loadWindow.Show();

            InitializeComponent();
            _gameManager = gameManager;
            LoadShape(gameManager.GetRandomShape());
            textTime.Text = TIME_TO_PLAY.ToString();
            _currentTime = TIME_TO_PLAY;
            _windowContainer = container;
            
            _kinectOutput = new KinectOutputToImage();
            _kinectOutput.ImageReady += OnImageReady;
            _kinectOutput.ImageReady += OnGameStart;

            _kinectOutput.kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
            _colorBitmap = new WriteableBitmap(_kinectOutput.kinectSensor.ColorStream.FrameWidth, _kinectOutput.kinectSensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
        }

        private void LoadShape(Shape shape)
        {
            shadePath.Data = shape.ShapeGeometry;
            BitmapImage backImg = new BitmapImage();
            backImg.BeginInit();
            backImg.UriSource = shape.ImageUri;
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
            if (_isTreatingPath)
                return;
            _isTreatingPath = true;
            Thread t = new Thread(() => CalculScore(sourceImage));
            t.Start();
        }


        private ScoreManager _scoreManager = new ScoreManager();

        private void CalculScore(Bitmap s)
        {
            BitmapToXamlPath g = new BitmapToXamlPath();
            string r = g.ConvertBitmap(s);
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    textScore.Text = _scoreManager.ScoreFromGeometries(shadePath.Data, Geometry.Parse(r)).ToString();
                });
            }
            catch (Exception) { }
            _isTreatingPath = false;
        }


        public void OnGameStart(object sender, BitmapSource firstImage)
        {
            if (_gameHasStarted) return;
            loadWindow.Close();
            _gameHasStarted = true;
            _gameTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            _gameTimer.Tick += UpdateTime;
            _gameTimer.Start();
            _kinectOutput.ImageReady -= OnGameStart;
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            _currentTime -= 1;
            Application.Current.Dispatcher.Invoke(() =>
            {
                textTime.Text = _currentTime.ToString();
            });
            if (_currentTime <= 0)
            {
                _gameTimer.Stop();
                OnGameEnd();
                return;
            }
            if (_currentTime <= 1)
                _kinectOutput.kinectSensor.ColorFrameReady += OnPhotoReady;
        }

        public void OnPhotoReady(object sender, ColorImageFrameReadyEventArgs args)
        {
            ColorImageFrame frame = args.OpenColorImageFrame();
            if (frame == null) return;
            byte[] colorPixels = new byte[_kinectOutput.kinectSensor.ColorStream.FramePixelDataLength];
            frame.CopyPixelDataTo(colorPixels);
            frame.Dispose();
            
            Application.Current.Dispatcher.Invoke(() =>
            {
            _colorBitmap.WritePixels(
                        new Int32Rect(0, 0, this._colorBitmap.PixelWidth, this._colorBitmap.PixelHeight),
                        colorPixels,
                        _colorBitmap.PixelWidth * sizeof(int),
                        0);
            });
        }

        private void OnGameEnd()
        {
            _kinectOutput.kinectSensor.ColorStream.Disable();
            _kinectOutput.kinectSensor.DepthStream.Disable();
            Application.Current.Dispatcher.Invoke(() =>
            {
                _windowContainer.LoadContent(new ScoreUserControl(_windowContainer, _gameManager, this._colorBitmap, textScore.Text));
            });
            _kinectOutput.kinectSensor.ColorFrameReady -= OnPhotoReady;
            _kinectOutput.RemoveSubscriptions();

            this._gameManager.SavePicture(_colorBitmap);
        }
    }
}
