using KinectToolkit;
using System.Windows;
using System.Windows.Controls;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour InstructionsUserControl.xaml
    /// </summary>
    public partial class InstructionsUserControl : UserControl
    {
        private IUserControlContainer _windowContainer;
        private GameManager _gameManager;

        public InstructionsUserControl(IUserControlContainer container, GameManager gameManager)
        {
            InitializeComponent();
            if (KinectOutputToImage.isCalibrate)
                CalibrateButton.IsEnabled = true;
            _gameManager = gameManager;
            _windowContainer = container;
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            _windowContainer.LoadContent(new MainMenuUserControl(_windowContainer, _gameManager));
        }

        private void GameButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Veuillez libérer le champ de vision de la Kinect pendant qu'elle se calibre.", "Attention !", MessageBoxButton.OK);
            _windowContainer.LoadContent(new GameUserControl(_windowContainer, _gameManager));
        }

        private void CalibrateButtonClick(object sender, RoutedEventArgs e)
        {
            KinectOutputToImage.isCalibrate = false;
            CalibrateButton.IsEnabled = false;
        }
    }
}
