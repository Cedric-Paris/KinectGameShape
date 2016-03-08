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
        private MainWindow windowContainer;

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


        public EmailInputUserControl(MainWindow container, BitmapSource picture)
        {
            InitializeComponent();
            windowContainer = container;
            PictureTakenBackgroundImage.ImageSource = picture;
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            if (!EmailValidationRules.Validate(MailAdress))
                return;
            if (!String.IsNullOrEmpty(MailAdress))
                SendEmail("./Photo.jpeg", NameTextBox.Text);
            windowContainer.LoadContent(new MainMenuUserControl(windowContainer));
        }

        private void ReplayButtonClick(object sender, RoutedEventArgs e)
        {
            if (!EmailValidationRules.Validate(MailAdress))
                return;
            if (!String.IsNullOrEmpty(MailAdress))
                SendEmail("./Photo.jpeg", NameTextBox.Text);
            windowContainer.LoadContent(new GameUserControl(windowContainer));
        }

        private void SendEmail(string filePath, string name)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("JeuKinectIUTInfo63@gmail.com");
                mail.To.Add(MailAdress);
                mail.Subject = "Photo prise lors des portes ouvertes de l'IUT Informatique d'Aubière (jeu Kinect)";
                mail.Body = "Bonjour " + name + " ! \n\n" +
                            "Voici la photo prise lors de votre visite de l'IUT Informatique lorsque vous avez essayé notre jeu sur Kinect. Nous espérons que vous avez passé un bon moment !\n\n" +
                            "Merci d'être venus,\n" +
                            "Cédric Paris & Nawhal Sayarh, élèves de l'IUT Informatique de Clermont-Ferrand.";

                Attachment imageAttachment = new Attachment(filePath) { Name = "Photo Jeu Kinect 05-03-2016" };
                mail.Attachments.Add(imageAttachment);

                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("JeuKinectIUTInfo63@gmail.com", "Chevaldo");
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                MessageBox.Show("Mail envoyé :)");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème d'envoi du mail... Veuillez réessayer.\n\n" + ex.ToString());
            }
        }
    }
}
