using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LapinCretinsFormes
{
    public class GameManager
    {
        public const string GALERY_DIRECTORY_PATH = "./GamePictures/";
        public const string TEMP_PICTURE_PATH = "./Temp/Photo.jpeg";

        private HighscoresAccessor highScoreAccess;
        private SortedDictionary<int, string> highScores;

        public GameManager()
        {
            highScores = highScoreAccess.Load();
        }

        public SortedDictionary<int, string> GetScores()
        {
            return highScores;
        }

        public void SaveScores()
        {
            highScoreAccess.Save(highScores);
        }


        public void SavePicture(WriteableBitmap picture)
        {
            BitmapFrame frame = BitmapFrame.Create(picture);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(frame);
            using (var stream = File.Create(TEMP_PICTURE_PATH))
            {
                encoder.Save(stream);
                stream.Close();
            }
            encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(frame);
            using (var stream = File.Create(GetPhotoPath()))
            {
                encoder.Save(stream);
                stream.Close();
            }
        }

        private string GetPhotoPath()
        {
            String s = String.Format("{0}{1:dd-MM-yy H:mm_ss*}.jpeg", GALERY_DIRECTORY_PATH,System.DateTime.Now);
            return s.Replace(":", "h").Replace("_", "m").Replace("*", "s");
        }

    }
}
