using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
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
    /// Logique d'interaction pour EmailInputUserControl.xaml
    /// </summary>
    public partial class EmailInputUserControl : UserControl
    {
        private IUserControlContainer windowContainer;
        private GameManager gameManager;
        private int score = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
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
            this.score = score;
            this.gameManager = gameManager;
            windowContainer = container;
            PictureTakenBackgroundImage.ImageSource = picture;
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            if (!EmailValidationRules.Validate(MailAdress))
                return;

            string name = NameTextBox.Text;
            name = string.IsNullOrWhiteSpace(name) ? "Anonyme" : name;

            gameManager.SaveNewScore(score, name);

            if (!string.IsNullOrEmpty(MailAdress))
                SendEmail("./Temp/Photo.jpeg", name);
            windowContainer.LoadContent(new MainMenuUserControl(windowContainer, gameManager));
        }

        private void ReplayButtonClick(object sender, RoutedEventArgs e)
        {
            if (!EmailValidationRules.Validate(MailAdress))
                return;
            if (!string.IsNullOrEmpty(MailAdress))
                SendEmail("./Temp/Photo.jpeg", NameTextBox.Text);
            windowContainer.LoadContent(new GameUserControl(windowContainer, gameManager));
        }

        private void SendEmail(string filePath, string name)
        {
            Window loadWindow = new LoadingWindow();
            loadWindow.Show();
            try
            {
                gameManager.sendMail(MailAdress, name);
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
