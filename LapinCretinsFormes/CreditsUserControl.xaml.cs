using System.Windows;
using System.Windows.Controls;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour CreditsUserControl.xaml
    /// </summary>
    public partial class CreditsUserControl : UserControl
    {
        private IUserControlContainer windowContainer;
        private GameManager gameManager;

        public CreditsUserControl(IUserControlContainer container, GameManager gameManager)
        {
            InitializeComponent();
            this.gameManager = gameManager;
            windowContainer = container;
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new MainMenuUserControl(windowContainer, gameManager));
        }
    }
}
