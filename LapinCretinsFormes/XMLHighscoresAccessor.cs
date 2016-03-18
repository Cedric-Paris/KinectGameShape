using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                //Images
                /*string path = string.Format("{0}\\{1}", file.DirectoryName, e.Element("Images").Element("Miniature").Value);
                if (File.Exists(path))
                    newClasse.Miniature = new Uri(path, UriKind.Absolute);
                path = string.Format("{0}\\{1}", file.DirectoryName, e.Element("Images").Element("ImagePerso").Value);
                if (File.Exists(path))
                    newClasse.ImagePerso = new Uri(path, UriKind.Absolute);*/
            return result;
        }


        public override void Save(List<Score> highscores, string filePath)
        {
            filePath += ".xml";

            List<Score> listToSave = highscores;

            /*DirectoryInfo directory = new DirectoryInfo(filePath);
            DirectoryInfo directorySave = new DirectoryInfo(string.Format("{0}\\{1}-XMLSave", directory.Parent.FullName, directory.Name.Substring(0, directory.Name.Length - 4)));
            Directory.CreateDirectory(string.Format("{0}\\{1}-XMLSave", directory.Parent.FullName, directory.Name.Substring(0, directory.Name.Length - 4)));*/

            XDocument saveFile = new XDocument();
            //FileInfo fichierImage;
            //List<Uri> listImages = dictionaryToSave.Select(classe => classe.Miniature).Concat(dictionaryToSave.Select(classe => classe.ImagePerso)).ToList();
            /*foreach (Uri uri in listImages)
            {
                if (!File.Exists(uri.OriginalString))
                {
                    fichierImage = new FileInfo(string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), uri.OriginalString));
                }
                else
                    fichierImage = new FileInfo(uri.OriginalString);
                if (!File.Exists(string.Format("{0}\\{1}", directorySave.FullName, fichierImage.Name))) //on verifi que le fichier n'existe pas deja
                    File.Copy(fichierImage.FullName, string.Format("{0}\\{1}", directorySave.FullName, fichierImage.Name));

            }*/

            var elements = listToSave.Select(score => new XElement("Highcore",
                                        new XAttribute("Score", score.Value),
                                        new XAttribute("Nom", score.Nom)));

            saveFile.Add(new XElement("Highscores", elements));
            saveFile.Save(filePath);
        }
        
    }
}