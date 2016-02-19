using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KinectToolkit;
using CsPotrace;
using System.Diagnostics;
using System.Threading;
using System.Drawing;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectOutputToImage kinectOutput;
        private bool isTreatingPath = false;

        public MainWindow()
        {
            InitializeComponent();
            kinectOutput = new KinectOutputToImage();
            kinectOutput.ImageReady += OnImageReady;
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
            Thread t = new Thread( () => CalculScore(sourceImage));
            t.Start();
        }


        private void CalculScore(Bitmap s)
        {
            BitmapToXamlPath g = new BitmapToXamlPath();
            string r = g.ConvertBitmap(s);
            try
            {
                Application.Current.Dispatcher.Invoke(() => { shadePath.Data = Geometry.Parse(r); });
            }
            catch (Exception e) { }
            isTreatingPath = false;
        }
    }
}
