using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ekol_Plast_MVC.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ekol_Plast_MVC.Data;
using Ekol_Plast_MVC.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Ekol_Plast_MVC.Controllers
{
    public class ContactController : Controller
    {    
        private readonly PonudeRepository _ponude;

        public ContactController(PonudeRepository ponude)
        {
            _ponude = ponude;
        }
        //public List<Contact> Ponude = new List<Contact>();

        public IActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("contact@ekolplast.mk");
                        mail.To.Add("elvirhrustemovic01@gmail.com");
                        mail.Subject = "Ponuda Ekol Plast";
                        mail.Body = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Body}\nNumber: {model.Number}";
                        mail.IsBodyHtml = false;

                        using (SmtpClient smtp = new SmtpClient("ekolplast.mk", 25))
                        {
                            smtp.Credentials = new System.Net.NetworkCredential("contact@ekolplast.mk", "Qki@j8685");
                            smtp.EnableSsl = true;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;

                            // Send the email asynchronously
                            await smtp.SendMailAsync(mail);
                        }
                    }

                    //Dodavanje ponude u bazu
                    await _ponude.Add(model);


                    // Redirect to a success page or return a success message
                    return RedirectToAction("Success");
                }

               

                catch (Exception ex)
                {
                    // Log or print detailed exception information
                    Console.WriteLine($"Exception Message: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }

                    // Handle the exception as needed
                    ModelState.AddModelError(string.Empty, "There was an error sending the email. Please try again later.");
                }
            }

            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Ponude()
        {
            var sveponude = await _ponude.GetAll();
            if (sveponude.Count() <= 0)
            {
                return NotFound();
            }
            return View(sveponude);
        }
    }
}
