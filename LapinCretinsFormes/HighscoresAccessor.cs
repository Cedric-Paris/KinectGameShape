using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    public abstract class HighscoresAccessor
    {
        public abstract SortedDictionary<int, string> Load(string filePath);
        public abstract void Save(SortedDictionary<int, string> highscores, string filePath);
        
        public static SortedDictionary<int, string> FiveFirstElementsOfDictionary(SortedDictionary<int, string> dictionary)
        {
            SortedDictionary<int, string> result = new SortedDictionary<int, string>();
            KeyValuePair<int, string> currentKVP;

            for (int i = 0; i < 5 && i < result.Keys.Count; i++)
            {
                currentKVP = dictionary.ElementAt(i);
                result.Add(currentKVP.Key, currentKVP.Value);
            }

            return result;
        }
    }
}
