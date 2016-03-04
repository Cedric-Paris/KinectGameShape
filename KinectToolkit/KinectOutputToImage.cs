using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectToolkit
{
    public class KinectOutputToImage
    {
        private const int MAX_DEPTH = 3700;
        private const int MIN_DEPTH = 2500;
        private const int CALIBRATE_LEVEL = 13;

        public delegate void ImageReadyEventHandler(object sender, BitmapSource image);
        public event ImageReadyEventHandler ImageReady;
        private void OnImageReady()
        {
            ImageReadyEventHandler handler = ImageReady;
            if (handler != null)
                handler(this, colorBitmap);
        }

        private int currentCalibrateImageNb;

        private bool isTreating;

        private bool isCalibrate;

        private List<short>[] depthPixelsReference;

        private DepthImagePixel[] depthPixels;

        private byte[] colorPixels;

        private WriteableBitmap colorBitmap;

        public KinectSensor kinectSensor {get; private set;}

        public KinectOutputToImage()
        {

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    kinectSensor = potentialSensor;
                    break;
                }
            }
            kinectSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            depthPixels = new DepthImagePixel[kinectSensor.DepthStream.FramePixelDataLength];
            colorPixels = new byte[kinectSensor.DepthStream.FramePixelDataLength * sizeof(int)];
            colorBitmap = new WriteableBitmap(kinectSensor.DepthStream.FrameWidth, kinectSensor.DepthStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgra32, null);//Bgr32
            CalibrateImage();
            kinectSensor.Start();
        }



        private void SensorDepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            if (this.isTreating == true)
                return;
            this.isTreating = true;
            DepthImageFrame depthFrame = e.OpenDepthImageFrame();
            if (depthFrame != null)
                {

                    depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);
                    depthFrame.Dispose();

                    int colorPixelIndex = 0;
                    int lenght = this.depthPixels.Length;
                    for (int i = 0; i < lenght; ++i)
                    {

                        short depth = this.depthPixels[i].Depth;

                        byte intensity;
                        if (depth < MIN_DEPTH || depth > MAX_DEPTH || !IsUserPixel(i, depth)) 
                            intensity =(byte) 255;
                        else
                            intensity = (byte)0;

                        // Write out blue byte
                        this.colorPixels[colorPixelIndex++] = intensity;

                        // Write out green byte
                        this.colorPixels[colorPixelIndex++] = intensity;

                        // Write out red byte                        
                        this.colorPixels[colorPixelIndex++] = intensity;
                                                
                        //colorPixelIndex++; si on a pas de transparence (bgr32)
                        if(intensity == 0)
                            this.colorPixels[colorPixelIndex++] = 255;
                        else
                            this.colorPixels[colorPixelIndex++] = 0;
                    }
                    // Ecriture de l'image
                    this.colorBitmap.WritePixels(
                        new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                        this.colorPixels,
                        this.colorBitmap.PixelWidth * sizeof(int),
                        0);
                    OnImageReady();
                }
            this.isTreating = false;
        }

        private void CalibrateImage()
        {
            depthPixelsReference = new List<short>[depthPixels.Length];
            for (int i = 0; i < this.depthPixels.Length; ++i)
            {
                depthPixelsReference[i] = new List<short>();
            }
            kinectSensor.DepthFrameReady += this.CalibrateFrameByFrame;
            currentCalibrateImageNb = 0;
        }


        private void CalibrateFrameByFrame(object sender, DepthImageFrameReadyEventArgs e)
        {
            DepthImageFrame depthFrame = e.OpenDepthImageFrame();
            if (depthFrame == null)
                return;
            depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);
            depthFrame.Dispose();
            for (int i = 0; i < this.depthPixels.Length; ++i)
            {
                if( ! depthPixelsReference[i].Contains(depthPixels[i].Depth))
                    depthPixelsReference[i].Add(depthPixels[i].Depth);
            }
            currentCalibrateImageNb++;
            if (currentCalibrateImageNb >= CALIBRATE_LEVEL && !isCalibrate)
            {
                isCalibrate = true;
                kinectSensor.DepthFrameReady += this.SensorDepthFrameReady;
                kinectSensor.DepthFrameReady -= this.CalibrateFrameByFrame;
            }

        }


        private bool IsUserPixel(int index, short depth)
        {
            foreach (short d in depthPixelsReference[index])
                if (depth >= d - 100 && depth <= d + 100)
                    return false;
            return true;
        }

        public void RemoveSubscriptions()
        {
            ImageReady = null;
            if (!isCalibrate)
                kinectSensor.DepthFrameReady -= this.CalibrateFrameByFrame;
            else
                kinectSensor.DepthFrameReady -= this.SensorDepthFrameReady;
        }


    }
}