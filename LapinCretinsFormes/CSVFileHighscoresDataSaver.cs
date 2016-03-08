﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    static class CSVFileHighscoresDataSaver
    {
        public static void SaveHighscoresToCSVFile(SortedDictionary<int, string> highscores)
        {
            StringBuilder csv = new StringBuilder();

            foreach (KeyValuePair<int, string> hs in highscores)
                csv.AppendLine(string.Format("{0},{1}", hs.Key, hs.Value));

            File.WriteAllText("./highscore_save", csv.ToString());
        }
    }
}