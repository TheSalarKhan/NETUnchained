using System.Net.Mail;
using Application;

namespace Utils {
    public class EmailUtils {
        public static bool sendEmail(string subject, string body, string email) {

            try {
                var myMessage = new SendGrid.SendGridMessage();
                myMessage.AddTo(email);
                myMessage.From = new MailAddress(Constant.EMAIL_FROM_ADDRESS, Constant.EMAIL_FROM_NAME);
                myMessage.Subject = subject;
                myMessage.Text = body;

                var transportWeb = new SendGrid.Web(Constant.SENDGRID_API_TOKEN);
                transportWeb.DeliverAsync(myMessage).Wait();

            } catch {
                return false;
            }

            return true;

        }
    }
}