using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour HighScoresUserControl.xaml
    /// </summary>
    public partial class HighScoresUserControl : UserControl
    {
        private IUserControlContainer _windowContainer;
        private GameManager _gameManager;

        public HighScoresUserControl(IUserControlContainer container, GameManager gameManager)
        {
            InitializeComponent();
            _windowContainer = container;
            _gameManager = gameManager;
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
            _windowContainer.LoadContent(new MainMenuUserControl(_windowContainer, _gameManager));
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
