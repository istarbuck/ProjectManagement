using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRequest.Models;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace ProjectRequest.Controllers
{
    public class AssignmentController : Controller
    {
        ProjectRequestEntities request = new ProjectRequestEntities();
        public int PageSize = 100;


        [Authorize]
        public ViewResult Index(string dateStart, string dateEnd, string location, string category, int page = 1, string completed = "false")
        {
            IEnumerable<Request> info = request.Requests.OrderByDescending(i => i.dateRequested);
            PagingInfo pageInfo = new PagingInfo();
            IEnumerable<Models.Attachment> attachmnets = request.Attachments;

            Regex reg = new Regex(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$");

           string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

           var requestAssignment = request.RequestAssignments.Where(r => r.staffID == name).Select(r => r.requestID).ToList();
 
           info = info.Where(a => requestAssignment.Contains(a.reuqestID));

            if (completed == "false")
                info = info.Where(i => i.completed == false);
            else if (completed == "true")
                info = info.Where(i => i.completed == true);


            if (location != "00" && location != null)
                info = info.Where(i => i.location == location);

            if (category != "00" && category != null)
                info = info.Where(i => i.categoryID == category);

            if (dateStart != null && dateEnd != null && reg.IsMatch(dateStart) && reg.IsMatch(dateEnd))
            {
                try
                {
                    info = info.Where(i => i.dateRequested >= Convert.ToDateTime(dateStart) && i.dateRequested <= Convert.ToDateTime(dateEnd));
                }
                catch { }
            }

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

            RequestListCategory requestListCategory = new RequestListCategory
            {
                requestList = requestList.Skip((page - 1) * PageSize).Take(PageSize),
                cateogires = request.Categories,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = pageTotal,
                    dateStart = dateStart,
                    dateEnd = dateEnd,
                    category = category,
                    completed = completed,
                    location = location,
                    completionStatus = pageInfo.PopulateStatus(),
                },
                location = request.Locations,
                attachments = attachmnets
            };

            ViewData["Categories"] = request.Categories;

            return View(requestListCategory);
        }

        public ViewResult RequestInfo(int requestID, string currentTrackID)
        {
            var answers = request.Answers.Where(a => a.requestID == requestID);
            IEnumerable<Models.Attachment> attachments = request.Attachments.Where(a => a.requestID == requestID);
            IEnumerable<Staff> staff = request.Staffs;
            IEnumerable<RequestAssignment> requestAssignment = request.RequestAssignments.Where(r => r.requestID == requestID);

            List<AnswerListViewModel> myAnswerList = new List<AnswerListViewModel>();


            foreach(var answer in answers)
            {
                var question = request.Questions.FirstOrDefault(q => q.questionID == answer.QuestionID);

                if (question != null)
                {
                    AnswerListViewModel list = new AnswerListViewModel
                    {
                        answer = answer,
                        question = question.Question1
                    };

                    myAnswerList.Add(list);
                }
            }

            var currentRequst = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            RequestInfoViewModel requestList = new RequestInfoViewModel
            {
                answerList = myAnswerList,
                attachment = attachments,
                request = currentRequst,
                staff = staff,
                assignments = requestAssignment,
                category = request.Categories.FirstOrDefault(c => c.categoryID == currentRequst.categoryID),
                cateogires = request.Categories,
                location = request.Locations
            };

            return View(requestList);
        }

        [HttpPost]
        public ActionResult SearchCurrentTrack(string currentTrackID)
        {
            var currentTrackRequest = request.Requests.FirstOrDefault(c => c.currentTrackID == currentTrackID);

            if (currentTrackRequest != null)
                return RedirectToAction("RequestInfo", new { requestID = currentTrackRequest.reuqestID });
            else
                return View();
        }

        [HttpPost]
        public ActionResult EditRequest(FormCollection form)
        {
            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            EditRequesterInfo(form);
            EditRequestContent(form);
            EditAssignment(form);

            return RedirectToAction("RequestInfo", new { requestID = requestID });
        }


        public void EditRequesterInfo(FormCollection form)
        {
            IEnumerable<Staff> staff = request.Staffs;

            string emailBody;
            MailMessage mail = new MailMessage();
            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");
            //mail.Bcc.Add(new MailAddress("websupport@sullivan.edu"));
            //mail.Bcc.Add(new MailAddress("istarbuck@sullivan.edu"));

            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;
            mail.From = new MailAddress("ProjectRequest@CC.com");


            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            string currentTrackID = Request.Form["CT#"].ToString();
            string completed = Request.Form["completed"].ToString();

            Dictionary<string, bool> staffing = new Dictionary<string, bool>();

            var editRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            if (editRequest.currentTrackID == null && currentTrackID != null && currentTrackID.Length > 3)
            {
                //mail.To.Add("ekuhn@sullivan.edu");

                emailBody = "<p>Current Track ID assigned for " + editRequest.projectName + ". <a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                    + requestID.ToString() + "'>Click Here</a> to view the project</p>";

                mail.Body = emailBody;
                mail.Subject = "Creative Communications Current Track ID Assigned";

                mailer.Send(mail);

                mail.To.Clear();
                try
                {
                    mail.To.Add(editRequest.email);

                    mail.Subject = "Creative Communications Projeject Request Received";

                    mail.Body = "Your request of '" + editRequest.projectName + "' has been received by Creative Communications. Project number " + currentTrackID + ".";
                    mailer.Send(mail);
                }
                catch { }

               
            }

            editRequest.currentTrackID = currentTrackID;

            if (completed == "Yes" && editRequest.completed == false)
            {
                editRequest.completed = true;

                mail.To.Clear();
                try
                {
                    mail.To.Add(editRequest.email);
                    mail.To.Add("istarbuck@sullivan.edu");

                    mail.Subject = "Creative Communications Project Request Completed";

                    mail.Body = "Your request of '" + editRequest.projectName + "' has been been completed. Project number " + currentTrackID + ".";
                    mailer.Send(mail);
                }
                catch { }
            }
            else if (completed == "No")
                editRequest.completed = false;

            editRequest.contactInfo = Request.Form["contactInfo"].ToString();
            editRequest.dateRequested = Convert.ToDateTime(Request.Form["date"]);
            editRequest.location = Request.Form["location"].ToString();
            editRequest.name = Request.Form["name"].ToString();
            editRequest.categoryID = Request.Form["category"].ToString();
            editRequest.doaName = Request.Form["doaName"].ToString();
            editRequest.projectName = Request.Form["projectName"].ToString();
            editRequest.email = Request.Form["email"].ToString();
            editRequest.additionalInfo = Request.Form["additionalInfo"].ToString();
            editRequest.comment = Request.Form["comments"].ToString();

            request.SaveChanges();
        }


        public void EditRequestContent(FormCollection form)
        {
            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            var answers = request.Answers.Where(a => a.requestID == requestID);

            foreach (var answer in answers)
            {
                var MasterAnswer = request.Answers.FirstOrDefault(a => a.AnswerID == answer.AnswerID);
                MasterAnswer.AnswerText = Request.Form[MasterAnswer.AnswerID.ToString()].ToString();

            }

            request.SaveChanges();

        }


        public void EditAssignment(FormCollection form)
        {
            string emailBody;
            MailMessage mail = new MailMessage();
            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");
            //mail.Bcc.Add(new MailAddress("websupport@sullivan.edu"));
            //mail.Bcc.Add(new MailAddress("istarbuck@sullivan.edu"));

            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;
            mail.From = new MailAddress("ProjectRequest@CC.com");

            IEnumerable<Staff> staff = request.Staffs;

            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            Dictionary<string, bool> staffing = new Dictionary<string, bool>();

            foreach (var employee in staff)
            {
                bool assigned;

                if (Request.Form["CB_" + employee.staffID] != null)
                    assigned = true;
                else
                    assigned = false;

                staffing.Add(employee.staffID, assigned);
            }

            foreach(var assignment in staffing)
            {
                var editRequestAssignment = request.RequestAssignments.FirstOrDefault(a => a.requestID == requestID && a.staffID == assignment.Key); 

                if(assignment.Value == true)
                {
                    if (editRequestAssignment == null)
                    {
                        RequestAssignment myRequest = new RequestAssignment();

                        myRequest.requestID = requestID;
                        myRequest.staffID = assignment.Key;
                        request.AddToRequestAssignments(myRequest);

                        mail.To.Add(assignment.Key + "@Sullivan.edu");

                        request.SaveChanges();
                    }
                    
                }
                else if(assignment.Value == false)
                {
                    if (editRequestAssignment != null)
                    {
                        request.RequestAssignments.DeleteObject(editRequestAssignment);
                        request.SaveChanges();
                    }
                }

                
            }

            if (mail.To.Count > 0)
            {

                emailBody = "<p>You have been assigned a project. <a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                    + requestID.ToString() + "'>Click Here</a> to view the project";

                emailBody += "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Assignment/Index'>Click here to view all your assignments</p>";

                mail.Body = emailBody;
                mail.Subject = "Creative Communications Project Assignment";

                mailer.Send(mail);
            }
            
        }
    }


    //    [Authorize]
    //    public ViewResult RequestInfo(int requestID)
    //    {
    //        var answers = request.Answers.Where(a => a.requestID == requestID);
    //        IEnumerable<Models.Attachment> attachments = request.Attachments.Where(a => a.requestID == requestID);
    //        IEnumerable<Staff> staff = request.Staffs;
    //        IEnumerable<RequestAssignment> requestAssignment = request.RequestAssignments.Where(r => r.requestID == requestID);

    //        List<AnswerListViewModel> myAnswerList = new List<AnswerListViewModel>();


    //        foreach (var answer in answers)
    //        {
    //            var question = request.Questions.FirstOrDefault(q => q.questionID == answer.QuestionID);

    //            if (question != null)
    //            {
    //                AnswerListViewModel list = new AnswerListViewModel
    //                {
    //                    answer = answer,
    //                    question = question.Question1
    //                };

    //                myAnswerList.Add(list);
    //            }
    //        }

    //        var currentRequst = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

    //        RequestInfoViewModel requestList = new RequestInfoViewModel
    //        {
    //            answerList = myAnswerList,
    //            attachment = attachments,
    //            request = currentRequst,
    //            staff = staff,
    //            assignments = requestAssignment,
    //            category = request.Categories.FirstOrDefault(c => c.categoryID == currentRequst.categoryID)

    //        };

    //        return View(requestList);
    //    }

    //    [HttpPost]
    //    public ActionResult RequestInfo(FormCollection form)
    //    {
    //        IEnumerable<Staff> staff = request.Staffs;

    //        int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

    //        string currentTrackID = Request.Form["currentTrackID"].ToString();
    //        string completed = Request.Form["completed"].ToString();

    //        Dictionary<string, bool> staffing = new Dictionary<string, bool>();

    //        foreach (var employee in staff)
    //        {
    //            bool assigned;

    //            if (Request.Form["CB_" + employee.staffID] != null)
    //                assigned = true;
    //            else
    //                assigned = false;



    //            staffing.Add(employee.staffID, assigned);
    //        }

    //        var editRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

    //        editRequest.currentTrackID = currentTrackID;
    //        if (completed == "Yes")
    //            editRequest.completed = true;
    //        else if (completed == "No")
    //            editRequest.completed = false;

    //        request.SaveChanges();

    //        foreach (var assignment in staffing)
    //        {
    //            var editRequestAssignment = request.RequestAssignments.FirstOrDefault(a => a.requestID == requestID && a.staffID == assignment.Key);

    //            if (assignment.Value == true)
    //            {
    //                if (editRequestAssignment == null)
    //                {
    //                    RequestAssignment myRequest = new RequestAssignment();

    //                    myRequest.requestID = requestID;
    //                    myRequest.staffID = assignment.Key;
    //                    request.AddToRequestAssignments(myRequest);

    //                    request.SaveChanges();
    //                }

    //            }
    //            else if (assignment.Value == false)
    //            {
    //                if (editRequestAssignment != null)
    //                {
    //                    request.RequestAssignments.DeleteObject(editRequestAssignment);
    //                    request.SaveChanges();
    //                }
    //            }


    //        }

    //        return RedirectToAction("RequestInfo", new { requestID = requestID });
    //    }

    //}
}
