using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour ScoreUserControl.xaml
    /// </summary>
    public partial class ScoreUserControl : UserControl
    {
        private IUserControlContainer _windowContainer;
        private GameManager _gameManager;

        private BitmapSource _backgroundPicture;
        private string _score;

        public ScoreUserControl(IUserControlContainer container, GameManager gameManager, BitmapSource picture, string score)
        {
            InitializeComponent();
            _gameManager = gameManager;
            _windowContainer = container;
            PictureTakenBackgroundImage.ImageSource = picture;
            _backgroundPicture = picture;
            _score = score;
            ScoreText.Text = score;
        }

        public void NextButtonClick(object sender, RoutedEventArgs e)
        {
            _windowContainer.LoadContent(new EmailInputUserControl(_windowContainer, int.Parse(_score), _gameManager, _backgroundPicture));
        }
    }
}
