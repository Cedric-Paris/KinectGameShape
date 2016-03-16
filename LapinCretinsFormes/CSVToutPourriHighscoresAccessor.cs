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
        public override List<Score> Load(string filePath)
        {
            filePath += ".csv";

            StreamReader reader = new StreamReader(File.OpenRead(filePath));
            List<Score> result = new List<Score>();

            while (!reader.EndOfStream)
            {
                string[] values = reader.ReadLine().Split(';');

                result.Add(new Score(int.Parse(values[0]), values[1]));
            }

            reader.Close();

            return result;
        }

        public override void Save(List<Score> highscores, string filePath)
        {
            filePath += ".csv";

            highscores = FiveFirstElementsOfDictionary(highscores);

            StringBuilder csv = new StringBuilder();

            foreach (Score s in highscores)
                csv.AppendLine(string.Format("{0};{1}", s.Value, s.Nom));

            File.WriteAllText("./highscore_save", csv.ToString());
        }
    }
}
