using System.Net.Mail;
using System.Net;

namespace ReportAndSolveAPI
{
    public static class MailSender
    {
        private static readonly int _smtpPort = 587;

        private static readonly string _smtpServer = "smtp.gmail.com";
        private static readonly string _smtpName = "artem9steponenko@gmail.com";
        private static readonly string _smtpPassword = "yutczhqunopqikqc";

        public static void SendTableChangedMesssage(string tableName, string operation)
        {
            using (SmtpClient smtpClient = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(_smtpName, _smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_smtpName);
                    mailMessage.To.Add("reportandsolvenotificator@gmail.com");
                    mailMessage.Subject = "DB changed";
                    mailMessage.Body = $"Table {tableName} was {operation}";

                    try
                    {
                        smtpClient.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
