using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ProjectRequest.Models;

namespace ProjectRequest.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

        [Authorize]
        public ActionResult Index()
        {
            ProjectRequestEntities project = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            Staff staff = project.Staffs.FirstOrDefault(s => s.staffID == name);
            IEnumerable<Category> category = project.Categories.OrderBy(c => c.categoryName);

            if (staff != null)
            {
                ViewBag.Name = staff.lastName;

                return View(category);
            }
            else
                RedirectToAction("ActionResult");

            return View(staff);
        }

        public ActionResult Denied()
        {
            return View();
        }

    }
}
