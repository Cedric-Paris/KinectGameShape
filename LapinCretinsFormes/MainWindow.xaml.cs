using System.Windows;
using System.Windows.Controls;

namespace LapinCretinsFormes
{
    /// <summary>s
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IUserControlContainer
    {

        private GameManager _gameManager;
        private UserControl _loadedContent;

        public MainWindow()
        {
            InitializeComponent();
            _gameManager = new GameManager();
            LoadContent(new MainMenuUserControl(this, _gameManager));
        }

        public void LoadContent(UserControl content)
        {
            _loadedContent = content;
            ContentOnWindow.Content = _loadedContent;
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _gameManager.OnApplicationClose();
        }
    }
}
