using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    class CSVToutPourriHighscoresAccessor : HighscoresAccessor
    {
        public override SortedDictionary<int, string> Load(string filePath)
        {
            StreamReader reader = new StreamReader(File.OpenRead(filePath));
            SortedDictionary<int, string> result = new SortedDictionary<int, string>();

            while (!reader.EndOfStream)
            {
                string[] values = reader.ReadLine().Split(';');

                result.Add(int.Parse(values[0]), values[1]);
            }

            reader.Close();

            return FiveFirstElementsOfDictionary(result);
        }

        public override void Save(SortedDictionary<int, string> highscores, string filePath)
        {
            highscores = FiveFirstElementsOfDictionary(highscores);

            StringBuilder csv = new StringBuilder();

            foreach (KeyValuePair<int, string> hs in highscores)
                csv.AppendLine(string.Format("{0};{1}", hs.Key, hs.Value));

            File.WriteAllText("./highscore_save", csv.ToString());
        }
    }
}
