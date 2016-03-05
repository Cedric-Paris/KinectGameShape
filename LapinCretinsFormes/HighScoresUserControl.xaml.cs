using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour HighScoresUserControl.xaml
    /// </summary>
    public partial class HighScoresUserControl : UserControl
    {
        private MainWindow windowContainer;

        public HighScoresUserControl(MainWindow container)
        {
            SortedDictionary<int, string> highscores = container.getHighscores();
            KeyValuePair<int, string> currentScore;
            InitializeComponent();
            windowContainer = container;

            int logicSize = highscores.Count();
            if (logicSize == 0) return;
            for (int i=0; i<logicSize; i++)
            {
                currentScore = highscores.ElementAt(logicSize - i - 1);
                Debug.WriteLine(ScoreToString(currentScore.Key, currentScore.Value));
                Debug.WriteLine(GetTextBlockByIndex(i));
                GetTextBlockByIndex(i).Text = ScoreToString(currentScore.Key, currentScore.Value);
            }
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new MainMenuUserControl(windowContainer));
        }

        private string ScoreToString(int score, string name)
        {
            return score + " : " + name;
        }

        private TextBlock GetTextBlockByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return OneTextBlock;
                case 1:
                    return TwoTextBlock;
                case 2:
                    return ThreeTextBlock;
                case 3:
                    return FourTextBlock;
                case 4:
                    return FiveTextBlock;
            }
            return null;
        }
    }
}
