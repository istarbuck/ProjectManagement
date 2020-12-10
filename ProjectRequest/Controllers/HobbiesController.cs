using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRequest.Models;

namespace ProjectRequest.Controllers
{
    public class HobbiesController : Controller
    {
        //
        // GET: /Hobbies/
        ProjectRequestEntities request = new ProjectRequestEntities();

        public ViewResult Index()
        {
            return View();
        }


        public ViewResult Pets()
        {
            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            var staff = request.Staffs;

            List<Pets> petInfo = new List<Pets>();

            foreach (var currentStaff in staff)
            {
                var petAttachments = request.Attachments.Where(a => a.staffID == currentStaff.staffID);

                if (petAttachments.Count() > 0 && petAttachments != null)
                {
                    petInfo.Add(new Pets
                    {
                        staffID = currentStaff.staffID,
                        attachments = petAttachments
                    });
                }
            }

            var fullName = staff.FirstOrDefault(n => n.staffID == name);

            ViewBag.Name = fullName.firstName + " " + fullName.lastName;

            return View(petInfo);
        }


        public ActionResult SavePetPic(HttpPostedFileBase file)
        {
            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            if (file != null && file.ContentLength > 1)
            {
                string fileName = "Pet_" + System.IO.Path.GetFileName(file.FileName);

                file.SaveAs(@"C:\websites\secure.sullivan.edu\ProjectRequest\ProjectRequest\Attachments\" + fileName);

                Attachment attachment = new Attachment();

                attachment.filePath = fileName;
                attachment.staffID = name;

                request.AddToAttachments(attachment);
                request.SaveChanges();

            }

            return RedirectToAction("Pets");
        }
    }
}
