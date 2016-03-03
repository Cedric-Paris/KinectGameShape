using System;
using System.Collections.Generic;
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

        public EmailInputUserControl(MainWindow container)
        {
            InitializeComponent();
            windowContainer = container;
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            //SendEmail(new Bitmap("Images/back.jp"));            COMMENT INSTANCIER UN BITMAP ???
            windowContainer.LoadContent(new MainMenuUserControl(windowContainer));
        }

        private void ReplayButtonClick(object sender, RoutedEventArgs e)
        {
            //SendEmail(new Bitmap("Images/back.jpg"));
            windowContainer.LoadContent(new GameUserControl(windowContainer));
        }

        private void SendEmail(Bitmap takenPicture) // FAIRE LES TESTS SUR L'ADRESSE MAIL
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("JeuKinectIUTInfo63@gmail.com");
                mail.To.Add("missnaloo@gmail.com");
                mail.Subject = "Photo prise lors des portes ouvertes de l'IUT Informatique d'Aubière (jeu Kinect)";
                mail.Body = "Halo ! Je suis C#.";
                
                mail.Attachments.Add(new Attachment(BitmapToMemStream(takenPicture), TakenPictureContentType()));

                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("JeuKinectIUTInfo63@gmail.com", "Chevaldo");
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                MessageBox.Show("mail Send !!!!!!!!!!!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Transforms the Bitmap image in a MemoryStream, making it a suitable attachment to the mail.
        /// </summary>
        /// <param name="TakenPicture">The Bitmap image to transform.</param>
        /// <returns>The corresponding MemoryStream.</returns>
        private MemoryStream BitmapToMemStream(Bitmap takenPicture)
        {

            MemoryStream memStream = new MemoryStream();

            takenPicture.Save(memStream, ImageFormat.Jpeg);
            memStream.Position = 0;

            return memStream;
        }

        /// <summary>
        /// Gives a proper name and a proper type to the taken picture.
        /// </summary>
        /// <returns>The ContentType of the taken picture.</returns>
        private ContentType TakenPictureContentType()
        {
            ContentType contentType = new ContentType();
            contentType.MediaType = MediaTypeNames.Image.Jpeg;
            contentType.Name = "Photo Jeu Kinect 05-03-2016";

            return contentType;
        }
    }
}
