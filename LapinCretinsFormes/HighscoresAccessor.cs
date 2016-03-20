using System.Collections.Generic;
using System.Linq;

namespace LapinCretinsFormes
{
    public abstract class HighscoresAccessor
    {
        public abstract List<Score> Load(string filePath);
        public abstract void Save(List<Score> highscores, string filePath);
        
        public List<Score> FiveFirstElementsOfDictionary(List<Score> list)
        {
            return list.OrderByDescending( s => s.Value ).Take(5).ToList();
        }
    }
}
