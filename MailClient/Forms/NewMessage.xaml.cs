using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MailClient.Forms
{
    /// <summary>
    /// Логика взаимодействия для NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        public NewMessage()
        {
            InitializeComponent();
        }
        
            
        
        private void Send()
        {
            try
            {
                TextRange doc = new TextRange(BodyTB.Document.ContentStart, BodyTB.Document.ContentEnd);
                string body = doc.Text;
                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress(emailsenderTB.Text, NamesenderTB.Text);
                // кому отправляем
                MailAddress to = new MailAddress(SendToTB.Text);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = SubjectTB.Text;
                // текст письма
                m.Body = body;
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("frenby4@gmail.com", "Azaza7788");
                smtp.EnableSsl = true;
                smtp.Send(m);
                MessageBox.Show("It's all ok");
                MessageBox.Show(body);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Send();
        }
    }
}
