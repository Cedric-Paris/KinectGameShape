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
        public const string HIGHSCORES_FILE_PATH = "./Data/highscores";
        public const string SHAPE_FILE_PATH = "./Data/shapes";
        public const string GALERY_DIRECTORY_PATH = "./GamePictures/";
        public const string TEMP_PICTURE_PATH = "./Temp/Photo.jpeg";

        private static Random rand = new Random();

        private HighscoresAccessor highScoreAccess = new FakeHighscoresAccessor();
        private IShapeAccessor shapeAccess = new FakeShapeAccessor();
        private List<Score> highScores;
        private List<Shape> shapesList;

        public GameManager()
        {
            highScores = highScoreAccess.Load(HIGHSCORES_FILE_PATH);
            shapesList = shapeAccess.Load(SHAPE_FILE_PATH);
        }

        public void OnApplicationClose()
        {

        }

        public List<Score> GetScores()
        {
            return highScores;
        }

        public List<Score> GetFiveHighScores()
        {
            return highScoreAccess.FiveFirstElementsOfDictionary(highScores);
        }

        public void SaveScores()
        {
            highScoreAccess.Save(highScores, HIGHSCORES_FILE_PATH);
        }

        public Shape GetRandomShape()
        {
            return shapesList[rand.Next(shapesList.Count)];
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
