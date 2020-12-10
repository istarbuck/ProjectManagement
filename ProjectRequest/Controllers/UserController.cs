using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRequest.Models;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace ProjectRequest.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public int PageSize = 50;

        public ActionResult Index()
        {


            return View();
        }

        public ViewResult MyRequests(string dateStart, string dateEnd, string location, string category, string search,
            string searchPO, string excludeCategory, string assignedTo, string projectStatus, string excludeStatus, int page = 1, string completed = "false")
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            IEnumerable<Request> info = request.Requests.OrderByDescending(i => i.dateRequested).Where(r => r.categoryID != "timeSheet");
            PagingInfo pageInfo = new PagingInfo();
            IEnumerable<Models.Attachment> attachmnets = request.Attachments;

            Regex reg = new Regex(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$");

            if (search != null && search.Count() > 0)
            {
                info = request.Requests.OrderByDescending(i => i.dateRequested).Where(r => r.projectName.ToLower().Contains(search.ToLower())
                    || r.email.ToLower().Contains(search.ToLower()) || r.doaName.ToLower().Contains(search.ToLower())
                    || r.name.ToLower().Contains(search.ToLower()) || r.customID.Contains(search));

                if (reg.IsMatch(search))
                {
                    info = request.Requests.Where(r => r.dueDate == Convert.ToDateTime(search));
                }

                info = info.Where(r => r.categoryID != "timeSheet");
            }

            if (searchPO != null && searchPO.Count() > 0)
            {
                var poNum = request.PONums.Where(p => p.PONum1 == searchPO).Select(p => p.requestID).ToList();
                info = info.Where(p => poNum.Contains(p.reuqestID));
            }

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            info = info.Where(a => a.email != null);

            info = info.Where(a => a.email.Contains(name));

            if (completed == "false")
                info = info.Where(i => i.completed == false);
            else if (completed == "true")
                info = info.Where(i => i.completed == true);

            if (location != "00" && location != null)
                info = info.Where(i => i.location == location);

            if (category != "00" && category != null)
                info = info.Where(i => i.categoryID == category);

            if (excludeCategory != "00" && excludeCategory != null)
                info = info.Where(i => i.categoryID != excludeCategory);

            if (projectStatus != "00" && projectStatus != null)
                info = info.Where(i => i.status == projectStatus);

            if (excludeStatus != "00" && excludeStatus != null)
                info = info.Where(i => i.status != excludeStatus);

            if (dateStart != null && dateEnd != null && reg.IsMatch(dateStart) && reg.IsMatch(dateEnd))
            {
                try
                {
                    info = info.Where(i => i.dateRequested >= Convert.ToDateTime(dateStart) && i.dateRequested < Convert.ToDateTime(dateEnd).AddDays(1));
                }
                catch { }
            }

            //if (search != null && search.Count() > 0)
            //{
            //    info = info.Where(r => r.projectName.ToLower().Contains(search.ToLower()) || r.doaName.ToLower().Contains(search.ToLower()) 
            //        || r.name.ToLower().Contains(search.ToLower()) || r.customID.Contains(search));

            //    if (reg.IsMatch(search))
            //    {
            //        info = info.Where(r => r.dueDate == Convert.ToDateTime(search));
            //    }
            //}



            List<RequestListViewModel> requestList = new List<RequestListViewModel>();

            foreach (var item in info)
            {
                RequestListViewModel myList = new RequestListViewModel()
                {
                    Request = item,
                    Category = request.Categories.FirstOrDefault(c => c.categoryID == item.categoryID)
                };

                requestList.Add(myList);
            }

            int pageTotal = requestList.Count();

            int subCases = Convert.ToInt16(requestList.Sum(r => r.Request.subProjects));

            ViewBag.CaseTotal = pageTotal;
            ViewBag.SubCases = subCases;

            RequestListCategory requestListCategory = new RequestListCategory
            {
                requestList = requestList.Skip((page - 1) * PageSize).Take(PageSize),
                cateogires = request.Categories.Where(c => c.version == "1"),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = pageTotal,
                    dateStart = dateStart,
                    dateEnd = dateEnd,
                    category = category,
                    excludeCategory = excludeCategory,
                    completed = completed,
                    location = location,
                    completionStatus = pageInfo.PopulateStatus(),
                    search = search,
                    searchPO = searchPO,
                    assignedTo = assignedTo,
                    projectStatus = projectStatus,
                    excludeStatus = excludeStatus
                },
                location = request.Locations,
                attachments = attachmnets,
                staffList = request.Staffs,
                projectStatus = request.ProjectStatus1.OrderBy(s => s.status)
            };

            ViewData["Categories"] = request.Categories;

            ViewBag.Method = "MyProjects";

            return View(requestListCategory);
        }

    }
}
