using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace LapinCretinsFormes
{
    public class GameManager
    {
        public const string HIGHSCORES_FILE_PATH = "./Data/highscores";
        public const string SHAPE_FILE_PATH = "./Data/shapes";
        public const string GALERY_DIRECTORY_PATH = "./GamePictures/";
        public const string TEMP_PICTURE_PATH = "./Temp/Photo.jpeg";

        private static Random _rand = new Random();

        private HighscoresAccessor _highScoreAccess = new XMLHighscoresAccessor();
        private IShapeAccessor _shapeAccess = new FakeShapeAccessor();
        private List<Score> _highScores;
        private List<Shape> _shapesList;

        private MailSender _mailSender = new MailSender();

        public GameManager()
        {
            _highScores = _highScoreAccess.Load(HIGHSCORES_FILE_PATH);
            _shapesList = _shapeAccess.Load(SHAPE_FILE_PATH);
        }

        public void OnApplicationClose()
        {
            SaveScores();
        }

        public List<Score> GetScores()
        {
            return _highScores;
        }

        public void SaveNewScore(int score, string name)
        {
            _highScores.Add(new Score(score, name));
        }

        public List<Score> GetFiveHighScores()
        {
            return _highScoreAccess.FiveFirstElementsOfDictionary(_highScores);
        }

        public void SaveScores()
        {
            _highScoreAccess.Save(_highScores, HIGHSCORES_FILE_PATH);
        }

        public Shape GetRandomShape()
        {
            return _shapesList[_rand.Next(_shapesList.Count)];
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
            string s = string.Format("{0}{1:dd-MM-yy H:mm_ss*}.jpeg", GALERY_DIRECTORY_PATH,System.DateTime.Now);
            return s.Replace(":", "h").Replace("_", "m").Replace("*", "s");
        }

        public void SendMail(string recipientMailAddress, string recipientName)
        {
            _mailSender.SendMail(recipientMailAddress, recipientName, TEMP_PICTURE_PATH);
        }
    }
}
