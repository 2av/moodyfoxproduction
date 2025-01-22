using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using moodyfox.Models;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Drawing.Imaging;

namespace moodyfox.Controllers
{
    public class HomeController : Controller
    {
        string EnuiryDataFileName= System.Configuration.ConfigurationManager.AppSettings["EnuiryDataFileName"];
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
        [HttpPost]
        public JsonResult SaveFormData(EnquiryFormViewModel formData)
        {
            try
            {
                // Define the path to save the JSON file
                string folderPath = Server.MapPath("~/App_Data");
                string filePath = Path.Combine(folderPath, EnuiryDataFileName);

                // Ensure the directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Read existing data if the file already exists
                List<EnquiryFormViewModel> existingData = new List<EnquiryFormViewModel>();
                if (System.IO.File.Exists(filePath))
                {
                    var fileContent = System.IO.File.ReadAllText(filePath);
                    existingData = JsonConvert.DeserializeObject<List<EnquiryFormViewModel>>(fileContent) ?? new List<EnquiryFormViewModel>();
                }
                
                var jsonData = System.IO.File.Exists(filePath) ? System.IO.File.ReadAllText(filePath) : "[]";

                var formDataList = JsonConvert.DeserializeObject<List<EnquiryFormViewModel>>(jsonData);

                formData.Id = formDataList.Count > 0 ? formDataList.Max(x => x.Id) + 1 : 1;
                // Add the new form data
                existingData.Add(formData);

                // Save the updated data to the JSON file
                System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(existingData, Formatting.Indented));

                return Json(new { success = true, message = "Data saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }


        [Route("Enquiry")]
        public ActionResult Enquiry()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetFormData(bool password=false)
        {
            try
            {
               
                if (!password)
                {
                    return Json(new { success = false, message = "Invalid password." }, JsonRequestBehavior.AllowGet);
                }

                // Path to the JSON file
                string filePath = Server.MapPath("~/App_Data/"+ EnuiryDataFileName);

                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                {
                    return Json(new { success = false, message = "No data available." }, JsonRequestBehavior.AllowGet);
                }

                // Read and parse the JSON file
                var fileContent = System.IO.File.ReadAllText(filePath);
                var formData = JsonConvert.DeserializeObject<List<EnquiryFormViewModel>>(fileContent);

                return Json(new { success = true, data = formData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteFormData(int id)
        {
            try
            {
                // Path to the JSON file
                string filePath = Server.MapPath("~/App_Data/"+ EnuiryDataFileName);

                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                {
                    return Json(new { success = false, message = "No data available to delete." });
                }

                // Read and parse the JSON file
                var fileContent = System.IO.File.ReadAllText(filePath);
                var formData = JsonConvert.DeserializeObject<List<EnquiryFormViewModel>>(fileContent);

                // Find the item with the specified ID
                var entryToRemove = formData.FirstOrDefault(x => x.Id == id); // Assuming `id` is passed to the method
                if (entryToRemove != null)
                {
                    // Remove the found entry
                    formData.Remove(entryToRemove);

                    // Save the updated data back to the file
                    System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(formData));

                    return Json(new { success = true, message = "Entry deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Entry not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateCompletionStatus(int id, bool isCompleted)
        {
            // Path to the JSON file
            string filePath = Server.MapPath("~/App_Data/" + EnuiryDataFileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return Json(new { success = false, message = "No data available to delete." });
            }

            // Read and parse the JSON file
            var fileContent = System.IO.File.ReadAllText(filePath);
            var formDataList = JsonConvert.DeserializeObject<List<EnquiryFormViewModel>>(fileContent);


            // Find the record by ID and update the completion status
            var record = formDataList.FirstOrDefault(x => x.Id == id);
            if (record != null)
            {
                record.IsCompleted = isCompleted;

                // Save the updated data back to the JSON file
                System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(formDataList));
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Record not found" });
        }

        [HttpPost]
        public JsonResult VerifyPassword(string password)
        {
            string correctPassword = "rg@2025"; // Set your desired password here
            if (password == correctPassword)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }

}

