using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour EmailInputUserControl.xaml
    /// </summary>
    public partial class EmailInputUserControl : UserControl
    {
        private IUserControlContainer _windowContainer;
        private GameManager _gameManager;
        private int _score;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public string MailAdress
        {
            get { return _mailAdress; }
            set
            {
                _mailAdress = value;
                OnPropertyChanged("MailAdress");
            }
        }
        private string _mailAdress;


        public EmailInputUserControl(IUserControlContainer container, int score, GameManager gameManager, BitmapSource picture)
        {
            InitializeComponent();
            this._score = score;
            this._gameManager = gameManager;
            _windowContainer = container;
            PictureTakenBackgroundImage.ImageSource = picture;
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OnClose();
            }
            catch (InvalidOperationException)
            {
                return;
            }

            _windowContainer.LoadContent(new MainMenuUserControl(_windowContainer, _gameManager));
        }

        private void ReplayButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OnClose();
            }
            catch (InvalidOperationException)
            {
                return;
            }

            _windowContainer.LoadContent(new GameUserControl(_windowContainer, _gameManager));
        }

        private void OnClose()
        {
            if (!EmailValidationRules.Validate(MailTextBox.Text))
                throw new InvalidOperationException("Mail not valid, cannot go on.");

            string name = NameTextBox.Text;
            name = string.IsNullOrWhiteSpace(name) ? "Anonyme" : name;

            _gameManager.SaveNewScore(_score, name);

            if (!string.IsNullOrEmpty(MailAdress))
                SendEmail("./Temp/Photo.jpeg", NameTextBox.Text);
        }

        private void SendEmail(string filePath, string name)
        {
            Window loadWindow = new LoadingWindow("Envoi du Mail . . .");
            loadWindow.Show();
            try
            {
                _gameManager.SendMail(MailAdress, name);
                loadWindow.Close();
            }
            catch (Exception ex)
            {
                loadWindow.Close();
                MessageBox.Show("Problème d'envoi du mail... Veuillez réessayer.\n\n" + ex.ToString());
            }
        }
    }
}
