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

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour HighScoresUserControl.xaml
    /// </summary>
    public partial class HighScoresUserControl : UserControl
    {

        private IUserControlContainer windowContainer;
        private GameManager gameManager;

        public HighScoresUserControl(IUserControlContainer container, GameManager gameManager)
        {
            InitializeComponent();
            windowContainer = container;
            this.gameManager = gameManager;
            List<Score> highscores = gameManager.GetFiveHighScores();

            int i = 0;
            foreach (Score s in highscores)
            {
                GetTextBlockByIndex(i).Text = ScoreToString(s.Value, s.Nom);
                if (i++ == 5)
                    break;
            }
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new MainMenuUserControl(windowContainer, gameManager));
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
