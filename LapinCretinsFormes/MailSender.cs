using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    public class MailSender
    {

        private const string SMTP_CLIENT_NAME = "smtp.gmail.com";
        private const int SMTP_PORT = 587;

        private const string SENDER_MAIL_ADDRESS = "JeuKinectIUTInfo63@gmail.com";
        private const string PASSWORD = "Chevaldo";


        public void SendMail(string recipientMailAddress, string recipientName, string attachmentFilePath)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient(SMTP_CLIENT_NAME);
            mail.From = new MailAddress(SENDER_MAIL_ADDRESS);
            mail.To.Add(recipientMailAddress);
            mail.Subject = "Photo prise lors des portes ouvertes de l'IUT Informatique d'Aubière (jeu Kinect)";
            mail.Body = "Bonjour " + recipientName + " ! \n\n" +
                        "Voici la photo prise lors de votre visite de l'IUT Informatique lorsque vous avez essayé notre jeu sur Kinect. Nous espérons que vous avez passé un bon moment !\n\n" +
                        "Merci d'être venus,\n" +
                        "Cédric Paris & Nawhal Sayarh, élèves de l'IUT Informatique de Clermont-Ferrand.";

            Attachment imageAttachment = new Attachment(attachmentFilePath) { Name = "Photo Jeu Kinect.jpeg" };
            mail.Attachments.Add(imageAttachment);

            smtpServer.Port = SMTP_PORT;
            smtpServer.Credentials = new System.Net.NetworkCredential(SENDER_MAIL_ADDRESS, PASSWORD);
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
            imageAttachment.Dispose();
        }
    }
}
