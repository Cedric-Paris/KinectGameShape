using System.Windows;
using System.Windows.Controls;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour MainMenuUserControl.xaml
    /// </summary>
    public partial class MainMenuUserControl : UserControl
    {
        private IUserControlContainer _windowContainer;
        private GameManager _gameManager;

        public MainMenuUserControl(IUserControlContainer container, GameManager gameManager)
        {
            InitializeComponent();
            _gameManager = gameManager;
            _windowContainer = container;
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            _windowContainer.Close();
        }

        private void NewGameButtonClick(object sender, RoutedEventArgs e)
        {
            _windowContainer.LoadContent(new InstructionsUserControl(_windowContainer, _gameManager));
        }

        private void CreditsButtonClick(object sender, RoutedEventArgs e)
        {
            _windowContainer.LoadContent(new CreditsUserControl(_windowContainer, _gameManager));
        }

        private void HighscoresButtonClick(object sender, RoutedEventArgs e)
        {
            _windowContainer.LoadContent(new HighScoresUserControl(_windowContainer, _gameManager));
        }
    }
}
