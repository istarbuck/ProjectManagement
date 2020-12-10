using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRequest.Models;

namespace ProjectRequest.Controllers
{
    public class CalendarController : Controller
    {
        //
        // GET: /Calender/

        ProjectRequestEntities request = new ProjectRequestEntities();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SchoolEvents()
        {
            return View();
        }
        public ActionResult Project()
        {
            return View();
        }

        public ActionResult Task()
        {
            return View();
        }

        public ActionResult NewSchoolEvent()
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            IEnumerable<Location> location = request.Locations.OrderByDescending(i => i.name);

            return View(location);
        }

        public JsonResult GetEvents(string start, string end)
        {
            var fromDate = Convert.ToDateTime(start);

            var toDate = Convert.ToDateTime(end);

            var eventList = request.Calenders.Where(c => c.end <= toDate && c.start >= fromDate && c.eventType == null);

            var rows = eventList.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSchoolEvents(string start, string end)
        {
            var fromDate = Convert.ToDateTime(start);

            var toDate = Convert.ToDateTime(end);

            var eventList = request.Calenders.Where(c => c.end <= toDate && c.start >= fromDate && c.eventType == "School");

            var rows = eventList.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProjectEvents(string start, string end, string agentAssigned)
        {
            List<Calendar> projectList = new List<Calendar>();

            var fromDate = Convert.ToDateTime(start);

            var toDate = Convert.ToDateTime(end);

            var eventList = request.Requests.Where(c => c.dueDate <= toDate && c.dueDate >= fromDate);



            foreach (var project in eventList)
            {
                var assignedTo = request.RequestAssignments.Where(t => t.requestID == project.reuqestID).ToList();

                string assignments = " - ";

                foreach (var staff in assignedTo)
                {
                    assignments += staff.staffID + ", ";
                }

                if (agentAssigned == null || assignments.Contains(agentAssigned))
                {
                    Calendar calendar = new Calendar()
                    {
                        requestID = project.reuqestID,
                        start = Convert.ToDateTime(project.dueDate),
                        end = Convert.ToDateTime(project.dueDate),
                        title = project.projectName + assignments,
                        category = project.categoryID 
                    };

                    projectList.Add(calendar);
                }

           }

            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTaskEvents(string start, string end, string agentAssigned)
        {
            List<Calendar> projectList = new List<Calendar>();

            var fromDate = Convert.ToDateTime(start);

            var toDate = Convert.ToDateTime(end);

            var eventList = request.TaskLists.Where(c => c.DueDate <= toDate && c.DueDate >= fromDate);

            foreach (var project in eventList)
            {
                string assignments = " - ";

                //foreach (var staff in assignedTo)
                //{
                //    assignments += staff.staffID + ", ";
                //}

                var taskAssignments = request.TaskAssignments.Where(t => t.taskID == project.taskID);

                foreach (var assignment in taskAssignments)
                {
                    assignments += assignment.staffID + ", ";
                }

                if (agentAssigned == null || assignments.Contains(agentAssigned))
                {
                    Calendar calendar = new Calendar()
                    {
                        requestID = Convert.ToUInt16(project.requestID),
                        start = Convert.ToDateTime(project.DueDate),
                        end = Convert.ToDateTime(project.DueDate),
                        title = project.Task + assignments
                    };

                    projectList.Add(calendar);
                }
            }

            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return origin.AddSeconds(timestamp);
        }

        [AuthorizeUser]
        public ActionResult AddEvent()
        {
            string title = Request.Form["title"].ToString();
            bool delete;

            //string startDate = Request.Form["startDate"].ToString();
            DateTime StartDate = Convert.ToDateTime(Request.Form["startDate"].ToString());
            int startTime1 = Convert.ToInt16(Request.Form["startTime1"]);
            int startTime2 = Convert.ToInt16(Request.Form["startTime2"]);
            string startAmPm = Request.Form["startAmPm"].ToString();
            string startTime;

            //string endDate = Request.Form["endDate"].ToString();
            DateTime EndDate = Convert.ToDateTime(Request.Form["endDate"].ToString());
            int endTime1 = Convert.ToInt16(Request.Form["endTime1"]);
            int endTime2 = Convert.ToInt16(Request.Form["endTime2"]);
            string endAmPm = Request.Form["endAmPm"].ToString();
            string endTime;

            //bool video = Convert.ToBoolean(Request.Form["video"]);

            //string[] startDates = startDate.Split('/');
            //string[] endDates = endDate.Split('/');

            //if (startAmPm == "PM" && endTime1 < 12)
            //{
            //    endTime1 = endTime1 + 12;
            //}

            //DateTime StartDate = new DateTime(Convert.ToInt16(startDates[2]), Convert.ToInt16(startDates[0]), Convert.ToInt16(startDates[1]), startTime1, startTime2, 000);

            //if (endAmPm == "PM" && endTime1 < 12)
            //{
            //    endTime1 = endTime1 + 12;
            //}

            //DateTime EndDate = new DateTime(Convert.ToInt16(endDates[2]), Convert.ToInt16(endDates[0]), Convert.ToInt16(endDates[1]), endTime1, endTime2, 000);

            //if (EndDate > StartDate)
            //{
            //    EndDate = EndDate.AddDays(1);
            //}

            if (Request.Form["id"].ToString() == "0")
            {
                Calender calender = new Calender();

                calender.end = EndDate.AddHours(23).AddMinutes(59);
                calender.start = StartDate;
                calender.title = Request.Form["title"].ToString();
                calender.allDay = false;
                calender.location = "Creative Communications";
                //calender.allDay = Convert.ToBoolean(Request.Form["allDay"]);

                //if (video)
                //    calender.className = "videoEvent";

                request.AddToCalenders(calender);
                request.SaveChanges();
            }
            else
            {
                int eventID = Convert.ToInt16(Request.Form["id"]);

                var calender = request.Calenders.FirstOrDefault(c => c.id == eventID);

                if (Request.Form["delete"] != null)
                    delete = true;
                else
                    delete = false;

                if (delete)
                {
                    request.Calenders.DeleteObject(calender);
                }
                else
                {
                    calender.end = EndDate.AddHours(23).AddMinutes(59);
                    calender.start = StartDate;
                    calender.title = Request.Form["title"].ToString();
                    calender.allDay = false;
                    calender.location = "Creative Communications";
                    //calender.allDay = Convert.ToBoolean(Request.Form["allDay"]);


                }


                request.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [AuthorizeUser]
        public ActionResult AddSchoolEvent()
        {
            string title = Request.Form["title"].ToString();
            bool delete;

            //string startDate = Request.Form["startDate"].ToString();
            DateTime StartDate = Convert.ToDateTime(Request.Form["startDate"].ToString());

            //string endDate = Request.Form["endDate"].ToString();
            DateTime EndDate = Convert.ToDateTime(Request.Form["endDate"].ToString());

            Calender calender = new Calender();

            calender.end = EndDate.AddHours(23).AddMinutes(59);
            calender.start = StartDate;
            calender.title = Request.Form["title"].ToString();
            calender.location = Request.Form["location"].ToString();
            calender.allDay = false;
            calender.eventType = "School";
            
            request.AddToCalenders(calender);
            request.SaveChanges();

            request.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
