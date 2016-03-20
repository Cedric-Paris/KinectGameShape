using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LapinCretinsFormes
{
    public class XMLHighscoresAccessor : HighscoresAccessor
    {
        public override List<Score> Load(string filePath)
        {
            filePath += ".xml";

            List<Score> result = new List<Score>();
            FileInfo file = new FileInfo(filePath);

            XDocument XMLFile = XDocument.Load(filePath);
            List<XElement> dataList = XMLFile.Descendants("Highcore").ToList();

            foreach (XElement e in dataList)
            {
                result.Add(new Score(int.Parse(e.Attribute("Score").Value), e.Attribute("Nom").Value));
            }
            return result;
        }


        public override void Save(List<Score> highscores, string filePath)
        {
            filePath += ".xml";

            List<Score> listToSave = highscores;

            XDocument saveFile = new XDocument();

            var elements = listToSave.Select(score => new XElement("Highcore",
                                        new XAttribute("Score", score.Value),
                                        new XAttribute("Nom", score.Nom)));

            saveFile.Add(new XElement("Highscores", elements));
            saveFile.Save(filePath);
        }
    }
}