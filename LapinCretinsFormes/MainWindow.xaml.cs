using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KinectToolkit;
using CsPotrace;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.IO;

namespace LapinCretinsFormes
{
    /// <summary>s
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl view;
        private SortedDictionary<int, string> highscores;
            public SortedDictionary<int, string> getHighscores() { return highscores; }

        public MainWindow()
        {
            InitializeComponent();
            highscores = new SortedDictionary<int, string>();
            //LoadContent(new MainMenuUserControl(this));

            AddScore(20000, "Geraldine");
            AddScore(15000, "Jean-Michel");
            AddScore(5000, "Marcel");
            AddScore(25000, "Jeanne-Micheline");
            AddScore(10000, "Cunégonde");
            AddScore(30000, "Albertine");
            AddScore(666, "Alistair");

            test("C:/Users/Nawhal/Documents/Cerisier.jpg");
            MessageBox.Show("fini !");
            Close();
        }

        public void LoadContent(UserControl content)
        {
            view = content;
            ContentOnWindow.Content = content;
        }

        public void AddScore(int score, string name)
        {
            if (highscores.Count < 5)
            {
                highscores.Add(score, name);
                return;
            }
            if (highscores.Keys.Any(s => s < score))
            {
                highscores.Remove(highscores.Select(s =>
                {
                    if (s.Key < score) return s.Key;
                    else return -1;
                }).ToArray()[0]);
                highscores.Add(score, name);
            }
        }

        public void test(string pathName)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(pathName, UriKind.Absolute);
            img.EndInit();

            BitmapToXamlPath g = new BitmapToXamlPath();
            string r = g.ConvertBitmap(SourceToBitmapConverter.ConvertToBitmap(img));

            StringBuilder txt = new StringBuilder();

            txt.AppendLine(r);

            File.WriteAllText("./formes.txt", txt.ToString());
        }
    }
}
