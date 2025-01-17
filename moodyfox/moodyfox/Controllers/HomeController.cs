using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using moodyfox.Models;
using System.Net;

namespace moodyfox.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult SendEmail(string toEmail)
        {
            try
            {
                // Sender and recipient email addresses
                string senderEmail = "support@moodyfoxproductions.com";
                string recipientEmail = "akhilesh.vis17@gmail.com";

                // Email subject and body
                string subject = "Test Email";
                string body = "This is a test email sent from ASP.NET MVC.";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                // SMTP client setup
                using (SmtpClient smtp = new SmtpClient("moodyfoxproductions.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("support@moodyfoxproductions.com", "c6BYAzVwBsiDuHB"); // Replace with your email password
                    smtp.EnableSsl = true; // Enable SSL/TLS
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Timeout = 15000; // Set a timeout for debugging
                    // Create and configure the email message
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(senderEmail);
                        mail.To.Add(new MailAddress(recipientEmail));
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = false; // Set to true if the email body contains HTML

                        // Send the email
                        smtp.Send(mail);
                    }
                }

                return Json("Email sent successfully.");
            }
            catch (SmtpException smtpEx)
            {
                // Log specific SMTP exceptions
                return Json($"SMTP Error: {smtpEx.Message}. Inner Exception: {smtpEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                // Log generic exceptions
                return Json($"Error: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
            }
        }
    }

}

