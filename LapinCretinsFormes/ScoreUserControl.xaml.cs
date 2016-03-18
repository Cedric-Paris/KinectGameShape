using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
    /// Logique d'interaction pour ScoreUserControl.xaml
    /// </summary>
    public partial class ScoreUserControl : UserControl
    {
        private IUserControlContainer windowContainer;
        private GameManager gameManager;

        private BitmapSource backgroundPicture;
        private string score;

        public ScoreUserControl(IUserControlContainer container, GameManager gameManager, BitmapSource picture, string score, string pourcentage)
        {
            InitializeComponent();
            this.gameManager = gameManager;
            windowContainer = container;
            PictureTakenBackgroundImage.ImageSource = picture;
            backgroundPicture = picture;
            PercentageScoreText.Text = pourcentage;
            this.score = score;
            ScoreText.Text = score;
        }

        public void NextButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new EmailInputUserControl(windowContainer, int.Parse(score), gameManager, backgroundPicture));
        }
    }
}
