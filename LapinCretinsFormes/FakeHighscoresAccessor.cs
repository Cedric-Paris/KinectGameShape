using System.Collections.Generic;

namespace LapinCretinsFormes
{
    public class FakeHighscoresAccessor : HighscoresAccessor
    {
        private static List<Score> _highscores;

        public FakeHighscoresAccessor()
        {
            _highscores = new List<Score>
            {
                new Score(200, "Geraldine"),
                new Score(150, "Jean-Michel"),
                new Score(250, "Jean-Paul"),
                new Score(100, "Cunégonde"),
                new Score(300, "Albertine")
            };
        }

        public override List<Score> Load(string filePath)
        {
            return _highscores;
        }

        public override void Save(List<Score> highscores, string filePath)
        {
            FakeHighscoresAccessor._highscores = FiveFirstElementsOfDictionary(highscores);
        }
    }
}
