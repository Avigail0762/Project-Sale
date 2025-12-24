using server.Bll.Interfaces;
using System.Net.Mail;

namespace server.Bll
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendWinnerEmailAsync(string toEmail, string giftName)
        {
            try
            {
                var fromEmail = _configuration["EmailSettings:FromEmail"];
                var appPassword = _configuration["EmailSettings:AppPassword"];
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var port = int.Parse(_configuration["EmailSettings:Port"]);

                var mail = new MailMessage();
                mail.To.Add(toEmail);
                mail.Subject = "זכית בהגרלה!";
                mail.Body = $"מזל טוב! זכית במתנה: {giftName}";
                mail.From = new MailAddress(fromEmail);

                using var smtp = new SmtpClient(smtpServer, port);
                smtp.Credentials = new System.Net.NetworkCredential(fromEmail, appPassword);
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }

    }
}
