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

    //    [HttpPost]
    //    public JsonResult SendEmail([FromBody] EnquiryFormViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                // Compose the email
    //                var fromAddress = new MailAddress("your-email@example.com", "Your Name");
    //                var toAddress = new MailAddress("recipient-email@example.com");
    //                const string fromPassword = "your-email-password";
    //                string subject = "New Enquiry Form Submission";
    //                string body = $@"
    //            First Name: {model.FirstName}
    //            Last Name: {model.LastName}
    //            Email: {model.Email}
    //            Contact: {model.Contact}
    //            Reference Video: {model.ReferenceVideo}
    //            Service Requirement: {model.ServiceRequirement}
    //            Message: {model.Message}
    //        ";

    //                var smtp = new SmtpClient
    //                {
    //                    Host = "smtp.gmail.com", // Update to your SMTP host
    //                    Port = 587,
    //                    EnableSsl = true,
    //                    DeliveryMethod = SmtpDeliveryMethod.Network,
    //                    UseDefaultCredentials = false,
    //                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //                };

    //                using (var message = new MailMessage(fromAddress, toAddress)
    //                {
    //                    Subject = subject,
    //                    Body = body
    //                })
    //                {
    //                    smtp.Send(message);
    //                }

    //                return Json(new { success = true, message = "Enquiry submitted successfully." });
    //            }
    //            catch (Exception ex)
    //            {
    //                return Json(new { success = false, message = ex.Message });
    //            }
    //        }
    //        else
    //        {
    //            return Json(new { success = false, message = "Invalid form data." });
    //        }
    //    }

    }
}
