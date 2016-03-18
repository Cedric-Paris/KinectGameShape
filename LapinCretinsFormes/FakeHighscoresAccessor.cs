using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    public class FakeHighscoresAccessor : HighscoresAccessor
    {
        private static List<Score> highscores;

        public FakeHighscoresAccessor()
        {
            highscores = new List<Score>();

            highscores.Add(new Score(200, "Geraldine"));
            highscores.Add(new Score(150, "Jean-Michel"));
            highscores.Add(new Score(250, "Jean-Paul"));
            highscores.Add(new Score(100, "Cunégonde"));
            highscores.Add(new Score(300, "Albertine"));
        }

        public override List<Score> Load(string filePath)
        {
            return highscores;
        }

        public override void Save(List<Score> highscores, string filePath)
        {
            FakeHighscoresAccessor.highscores = FiveFirstElementsOfDictionary(highscores);
        }
    }
}
