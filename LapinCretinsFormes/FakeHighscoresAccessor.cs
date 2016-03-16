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

            highscores.Add(new Score(20000, "Geraldine"));
            highscores.Add(new Score(15000, "Jean-Michel"));
            highscores.Add(new Score(5000, "Marcel"));
            highscores.Add(new Score(25000, "Jeanne-Micheline"));
            highscores.Add(new Score(10000, "Cunégonde"));
            highscores.Add(new Score(30000, "Albertine"));
            highscores.Add(new Score(666, "Alistair"));
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
