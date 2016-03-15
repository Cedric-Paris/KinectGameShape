using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    public class FakeHighscoresAccessor : HighscoresAccessor
    {
        private static SortedDictionary<int, string> highscores;

        public FakeHighscoresAccessor()
        {
            highscores = new SortedDictionary<int, string>();

            highscores.Add(20000, "Geraldine");
            highscores.Add(15000, "Jean-Michel");
            highscores.Add(5000, "Marcel");
            highscores.Add(25000, "Jeanne-Micheline");
            highscores.Add(10000, "Cunégonde");
            highscores.Add(30000, "Albertine");
            highscores.Add(666, "Alistair");
        }

        public override SortedDictionary<int, string> Load(string filePath)
        {
            return FiveFirstElementsOfDictionary(highscores);
        }

        public override void Save(SortedDictionary<int, string> highscores, string filePath)
        {
            FakeHighscoresAccessor.highscores = FiveFirstElementsOfDictionary(highscores);
        }
    }
}
