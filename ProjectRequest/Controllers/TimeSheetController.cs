using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRequest.Models;
using System.Text.RegularExpressions;

namespace ProjectRequest.Controllers
{
    public class TimeSheetController : Controller
    {
        //
        // GET: /TimeSheet/
        

        public ActionResult Index()
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);
            name = name.Remove(0, 8).ToLower();
            int sheetsNeeded;
            DateTime sheetDate;
            IEnumerable<TimeSheet> timeSheet = request.TimeSheets.Where(t => t.staffID == name && t.completed == false).OrderByDescending(t => t.sheetDate);

            TimeSheet currentSheet;

            var allSheets = request.TimeSheets.Where(t => t.staffID == name).OrderByDescending(t => t.sheetDate);
            if (allSheets == null || allSheets.Count() < 1)
            {
                TimeSheet mySheet = new TimeSheet();

                mySheet.sheetDate = DateTime.Today;
                mySheet.staffID = name;
                mySheet.completed = false;

                request.AddToTimeSheets(mySheet);
                request.SaveChanges();
            }

            currentSheet = allSheets.FirstOrDefault();

            //if (timeSheet == null || timeSheet.Count() < 1)
            //{
            //    var sheetsCompleted = request.TimeSheets.Where(t => t.staffID == name && t.completed == true).OrderByDescending(t => t.sheetDate);

            //    if (sheetsCompleted == null || sheetsCompleted.Count() < 1)
            //    {
            //        var todaySheet = request.TimeSheets.FirstOrDefault(t => t.sheetDate == DateTime.Today && t.completed == true);

            //        if (todaySheet == null)
            //        {
            //            TimeSheet mySheet = new TimeSheet();

            //            mySheet.sheetDate = DateTime.Today;
            //            mySheet.staffID = name;
            //            mySheet.completed = false;

            //            request.AddToTimeSheets(mySheet);
            //            request.SaveChanges();

            //            currentSheet = mySheet;
            //        }
            //        else
            //            currentSheet = todaySheet;
            //    }
            //    else
            //    {
            //        var lastSheetCompleted = sheetsCompleted.FirstOrDefault();

            //        currentSheet = lastSheetCompleted;
            //    }
                    
            //}

            //else
            //{
            //    currentSheet = timeSheet.FirstOrDefault(t => t.staffID == name);
            //}

            sheetDate = currentSheet.sheetDate.GetValueOrDefault();

            sheetDate = sheetDate.AddDays(1);

            sheetsNeeded = (int)DateTime.Today.Subtract(sheetDate).TotalDays;

            for (var startDate = 1; startDate <= sheetsNeeded + 1; startDate++)
            {
                if (sheetDate.DayOfWeek != DayOfWeek.Saturday && sheetDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    var sheet = request.TimeSheets.FirstOrDefault(t => t.sheetDate == sheetDate && t.staffID == name);

                    if (sheet == null)
                    {
                        TimeSheet mySheet = new TimeSheet();

                        mySheet.sheetDate = sheetDate;
                        mySheet.staffID = name;
                        mySheet.completed = false;

                        request.AddToTimeSheets(mySheet);
                        request.SaveChanges();

                        sheetDate = sheetDate.AddDays(1);
                    }
                    else
                    {
                        sheetDate = sheetDate.AddDays(1);
                    }

                }
                else
                    sheetDate = sheetDate.AddDays(1);
            }

            IEnumerable<TimeSheet> lateSheets = request.TimeSheets.Where(t => t.staffID == name && t.completed == false).OrderByDescending(t => t.sheetDate);

            return View(lateSheets);
        }

        public ActionResult CreateTimeSheet(string startDate, string endDate, string assignedProjects = "true", string completed = "false")
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            int sheetID = Convert.ToInt32(Request.QueryString["sheetID"]);

            string name = Convert.ToString(User.Identity.Name);
            name = name.Remove(0, 8).ToLower();

            IEnumerable<Chore> chores = request.Chores.Where(c => c.sheetID == sheetID);

            IEnumerable<Request> requests = request.Requests;

            IEnumerable<Location> locations = request.Locations.OrderBy(l => l.name);

            TimeSheet timeSheet = request.TimeSheets.FirstOrDefault(t => t.sheetID == sheetID);

            Regex reg = new Regex(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$");

            if (startDate != null && reg.IsMatch(startDate))
            {
                try
                {
                    requests = requests.Where(i => i.dateRequested >= Convert.ToDateTime(startDate));
                }
                catch { }
            }

            if (endDate != null && reg.IsMatch(endDate))
            {
                try
                {
                    requests = requests.Where(i => i.dateRequested < Convert.ToDateTime(endDate).AddDays(1));
                }
                catch { }
            }

            if (completed == "false")
                requests = requests.Where(i => i.completed == false);

            if (assignedProjects == "true")
            {
                var assignments = request.RequestAssignments.Where(r => r.staffID == name).Select(r => r.requestID).ToList();
                requests = requests.Where(r => assignments.Contains(r.reuqestID));
            }

            TimeSheetInfo sheetInfo = new TimeSheetInfo
            {
                requests = requests,
                chores = chores,
                locations = locations,
                timeSheet = timeSheet,
                startDate = startDate,
                endDate = endDate,
                completedProjects = completed,
                assignedProjects = assignedProjects
            };

            return View(sheetInfo);
        }

        public ActionResult EditTimeSheet(int sheetID, string startDate, string endDate, string assignedProjects = "true", string completed = "false")
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            //sheetID = Convert.ToInt32(Request.QueryString["sheetID"]);

            var chores = request.Chores.Where(c => c.sheetID == sheetID);
            var timeSheet = request.TimeSheets.FirstOrDefault(t => t.sheetID == sheetID);
            

            bool delete;
            bool complete;

            if (Request.Form["complete"] != null)
                complete = true;
            else
                complete = false;

            timeSheet.completed = complete;

            foreach (var chore in chores)
            {
                var currentRequest = request.Requests.FirstOrDefault(r => r.reuqestID == chore.requestID);

                if (Request.Form["Delete_" + chore.choreID] != null)
                    delete = true;
                else
                    delete = false;

                if (delete == false)
                {
                    chore.requestID = Convert.ToInt16(Request.Form["Request_" + chore.choreID]);
                    chore.chore1 = Request.Form["Chore_" + chore.choreID].ToString();
                    chore.timeSpent = Convert.ToDecimal(Request.Form["TimeSpent_" + chore.choreID]);
                    chore.name = currentRequest.projectName;
                }
                else
                {
                    request.DeleteObject(chore);
                }

            }

            request.SaveChanges();

            if (Request.Form["Request"] != null && Request.Form["Request"].ToString().Count() > 0)
            {
                int requestID = Convert.ToInt16(Request.Form["Request"]);

                var currentRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

                Chore chore = new Chore();

                chore.sheetID = sheetID;
                chore.requestID = Convert.ToInt16(Request.Form["Request"]);
                chore.chore1 = Request.Form["Chore"].ToString();
                chore.timeSpent = Convert.ToDecimal(Request.Form["TimeSpent"]);
                chore.name = currentRequest.projectName;

                request.AddToChores(chore);
                request.SaveChanges();
            }

            return RedirectToAction("CreateTimeSheet", new { sheetID = sheetID, startDate = startDate, endDate = endDate, assignedProjects = assignedProjects, completed = completed });
        }

        public void LookUpSheet()
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);
            name = name.Remove(0, 8).ToLower();
            try
            {
                DateTime sheetDate = Convert.ToDateTime(Request.Form["sheetDate"]);

                var timeSheet = request.TimeSheets.FirstOrDefault(t => t.sheetDate == sheetDate && t.staffID == name);

                if (timeSheet != null)
                    Response.Redirect("~/TimeSheet/CreateTimeSheet?sheetID=" + timeSheet.sheetID.ToString());
                else
                    Response.Redirect("~/TimeSheet/Index?sheet=Fail");
            }

            catch
            {
                Response.Redirect("~/TimeSheet/Index?sheet=Fail");
            }


        }

        public string AddTime(string id, string description, string time)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            try
            {
                if (description.Count() > 0 && time.Count() > 0)
                {
                    string name = Convert.ToString(User.Identity.Name);
                    name = name.Remove(0, 8).ToLower();

                    int requestID = Convert.ToInt16(id);
                    var date = DateTime.Now.Date;

                    var sheetName = request.TimeSheets.FirstOrDefault(d => d.sheetDate == date && d.staffID == name);

                    if (sheetName == null)
                    {
                        int sheetsNeeded;
                        DateTime sheetDate;
                        IEnumerable<TimeSheet> timeSheet = request.TimeSheets.Where(t => t.staffID == name && t.completed == false).OrderByDescending(t => t.sheetDate);

                        TimeSheet currentSheet;

                        var allSheets = request.TimeSheets.Where(t => t.staffID == name).OrderByDescending(t => t.sheetDate);
                        if (allSheets == null || allSheets.Count() < 1)
                        {
                            TimeSheet mySheet = new TimeSheet();

                            mySheet.sheetDate = DateTime.Today;
                            mySheet.staffID = name;
                            mySheet.completed = false;

                            request.AddToTimeSheets(mySheet);
                            request.SaveChanges();
                        }

                        currentSheet = allSheets.FirstOrDefault();

                        sheetDate = currentSheet.sheetDate.GetValueOrDefault();

                        sheetDate = sheetDate.AddDays(1);

                        sheetsNeeded = (int)DateTime.Today.Subtract(sheetDate).TotalDays;

                        for (var startDate = 1; startDate <= sheetsNeeded + 1; startDate++)
                        {
                            if (sheetDate.DayOfWeek != DayOfWeek.Saturday && sheetDate.DayOfWeek != DayOfWeek.Sunday)
                            {
                                var sheet = request.TimeSheets.FirstOrDefault(t => t.sheetDate == sheetDate && t.staffID == name);

                                if (sheet == null)
                                {
                                    TimeSheet mySheet = new TimeSheet();

                                    mySheet.sheetDate = sheetDate;
                                    mySheet.staffID = name;
                                    mySheet.completed = false;

                                    request.AddToTimeSheets(mySheet);
                                    request.SaveChanges();

                                    sheetDate = sheetDate.AddDays(1);
                                }
                                else
                                {
                                    sheetDate = sheetDate.AddDays(1);
                                }

                            }
                            else
                                sheetDate = sheetDate.AddDays(1);
                        }

                        sheetName = request.TimeSheets.FirstOrDefault(d => d.sheetDate == date && d.staffID == name);
                    }

                    var currentRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

                    Chore chore = new Chore();

                    chore.chore1 = description;
                    chore.requestID = requestID;
                    chore.sheetID = sheetName.sheetID;
                    chore.timeSpent = Convert.ToDecimal(time);
                    chore.name = currentRequest.projectName;

                    request.AddToChores(chore);
                    request.SaveChanges();

                    return "Your report has been successfully filed.";
                }// end validation if
                else
                    return "Please fill out description and time fields";
            }// end try

            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}
