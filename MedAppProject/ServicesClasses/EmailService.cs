using System.Net;
using System.Net.Mail;
namespace MedAppProject.ServicesClasses
{
	public class EmailService:IEmailSender
	{
        public Task SendEmailAsync(string email,string subject,string message)
        {
            var mail = "medappointment2023@gmail.com";
            var pw = "ettcvfjfhckifwfp";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message

                ));

        }
    }
}
