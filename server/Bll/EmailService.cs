using server.Bll.Interfaces;
using System.Net.Mail;

namespace server.Bll
{
    public class EmailService : IEmailService
    {
        public async Task SendWinnerEmailAsync(string toEmail, string giftName)
        {
            try
            {
                var mail = new MailMessage();
                mail.To.Add(toEmail);
                mail.Subject = "זכית בהגרלה!";
                mail.Body = $"מזל טוב! זכית במתנה: {giftName}";
                mail.From = new MailAddress("a0583290762@gmail.com");

                using var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential("a0583290762@gmail.com", "app-password");
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
