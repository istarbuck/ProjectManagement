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
    public class RequestController : Controller
    {
        //
        // GET: /Default/
        
        public int PageSize = 50;

        public ViewResult Index(string dateStart, string dateEnd, string location, string category, string excludeCategory, string search,
            string assignedTo, string projectStatus, string excludeStatus = "Denied", int page = 1, string completed = "false", string approvers = "No")
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            IEnumerable<Request> info = request.Requests.OrderByDescending(i => i.dateRequested).Where(r => r.categoryID != "timeSheet");
            PagingInfo pageInfo = new PagingInfo();
            IEnumerable<Models.Attachment> attachmnets = request.Attachments;
            var staffList = request.Staffs.Select(s => s.staffID).ToList();

            Regex reg = new Regex(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$");

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

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

            if(approvers == "Yes")
            {
                var approverList = request.Approvers.Select(a => a.sullivanID).ToList();

                if(approverList.Contains(name))
                {
                    var departments = request.Approvers.Where(s => s.sullivanID == name).Select(s => s.department).ToList();

                    if(departments.Count() == 1)
                    {
                        info = info.Where(i => i.department == departments[0] && i.status == "Pending");
                    }
                    else if(departments.Count() == 2)
                    {
                        info = info.Where(i => i.department == departments[0] || i.department == departments[1] && i.status == "Pending");
                    }
                }

            }

            if (staffList.Contains(name))
            {
                ViewBag.Role = "Agent";
            }
            else if (approvers.Contains(name))
            {
                ViewBag.Role = "Approver";
            }
            else
            {
                ViewBag.Role = "User";
            }

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

            if (assignedTo != "00" && assignedTo != null)
            {
                var requestAssignment = request.RequestAssignments.Where(r => r.staffID == assignedTo).Select(r => r.requestID).ToList();

                info = info.Where(a => requestAssignment.Contains(a.reuqestID));
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

            ViewBag.Method = "Index";

            return View(requestListCategory);
        }

        [AuthorizeUser]
        public ViewResult MyProjects(string dateStart, string dateEnd, string location, string category, string search, 
            string searchPO, string excludeCategory, string assignedTo, string projectStatus, string excludeStatus = "Denied", int page = 1, string completed = "false")
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            IEnumerable<Request> info = request.Requests.OrderByDescending(i => i.dateRequested).Where(r => r.categoryID != "timeSheet");
            PagingInfo pageInfo = new PagingInfo();
            IEnumerable<Models.Attachment> attachmnets = request.Attachments;
            var staffList = request.Staffs.Select(s => s.staffID).ToList();

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

            if (staffList.Contains(name))
            {
                ViewBag.Role = "Agent";
            }
            else
            {
                ViewBag.Role = "User";
            }

            if (assignedTo == "00" || assignedTo == null)
            {
                assignedTo = name;
            }

            var requestAssignment = request.RequestAssignments.Where(r => r.staffID == assignedTo).Select(r => r.requestID).ToList();

            info = info.Where(a => requestAssignment.Contains(a.reuqestID));

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

        public ViewResult PrintMyProjects(string dateStart, string dateEnd, string location, string category, string search, string searchPO, string excludeCategory, int page = 1, string completed = "false")
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            IEnumerable<Request> info = request.Requests.OrderByDescending(i => i.dateRequested).Where(r => r.categoryID != "timeSheet");
            PagingInfo pageInfo = new PagingInfo();
            IEnumerable<Models.Attachment> attachmnets = request.Attachments;

            Regex reg = new Regex(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$");

            if (search != null && search.Count() > 0)
            {
                info = request.Requests.OrderByDescending(i => i.dateRequested).Where(r => r.projectName.ToLower().Contains(search.ToLower())
                    || r.email.ToLower().Contains(search.ToLower()) || r.poNumber.Contains(search) || r.doaName.ToLower().Contains(search.ToLower())
                    || r.name.ToLower().Contains(search.ToLower()));

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

            if (excludeCategory != "00" && excludeCategory != null)
                info = info.Where(i => i.categoryID != excludeCategory);

            if (dateStart != null && dateEnd != null && reg.IsMatch(dateStart) && reg.IsMatch(dateEnd))
            {
                try
                {
                    info = info.Where(i => i.dateRequested >= Convert.ToDateTime(dateStart) && i.dateRequested <= Convert.ToDateTime(dateEnd));
                }
                catch { }
            }

            if (search != null && search.Count() > 0)
            {
                info = info.Where(r => r.projectName.ToLower().Contains(search.ToLower()) || r.doaName.ToLower().Contains(search.ToLower()) || r.name.ToLower().Contains(search.ToLower()));

                if (reg.IsMatch(search))
                {
                    info = info.Where(r => r.dueDate == Convert.ToDateTime(search));
                }
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
                requestList = requestList,
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
                    searchPO = searchPO
                },
                location = request.Locations,
                attachments = attachmnets
            };

            ViewData["Categories"] = request.Categories;

            ViewBag.Method = "MyProjects";

            return View(requestListCategory);
        }

        [AuthorizeUser]
        public ViewResult TaskList(string dateStart, string dateEnd, string assignedTo, string status, string priority = "false", int page = 1, string completed = "false", string projectCompleted = "false" )
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            IEnumerable<TaskList> taskList = request.TaskLists.OrderBy(i => i.DueDate).Where(r => r.requestID != null);
            
            List<TaskView> taskView = new List<TaskView>();

            PagingInfo pageInfo = new PagingInfo();

            Regex reg = new Regex(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$");

            if (dateStart != null && reg.IsMatch(dateStart))
            {
                try
                {
                    taskList = taskList.Where(i => i.DueDate >= Convert.ToDateTime(dateStart));
                }
                catch { }
            }

            if (dateEnd != null && reg.IsMatch(dateEnd))
            {
                try
                {
                    taskList = taskList.Where(i => i.DueDate < Convert.ToDateTime(dateEnd).AddDays(1));
                }
                catch { }
            }

            if (completed == "false")
                taskList = taskList.Where(i => i.Status != "Completed");

            if (priority == "true")
                taskList = taskList.Where(i => i.priority == true);

            if (status != null && status != "00")
                taskList = taskList.Where(t => t.Status == status);

            foreach (var task in taskList)
            {
                List<string> staffAssignments = new List<string>();
                Request projectRequest;

                if (projectCompleted == "false")
                {
                    projectRequest = request.Requests.FirstOrDefault(r => r.reuqestID == task.requestID && r.completed == false && r.categoryID != "timeSheet");
                }
                else
                {
                    projectRequest = request.Requests.FirstOrDefault(r => r.reuqestID == task.requestID && r.categoryID != "timeSheet");
                }

                if (projectRequest != null)
                {
                    var taskAssignments = request.TaskAssignments.Where(t => t.taskID == task.taskID);

                    //if (assignedTo == null)
                    //{
                        foreach (var assignment in taskAssignments)
                        {
                            staffAssignments.Add(assignment.staffID);
                        }
                    //}
                    //else
                    //    staffAssignments.Add(assignedTo);

                    TaskView taskInfo = new TaskView
                    {
                        taskList = task,
                        staffList = staffAssignments,
                        request = projectRequest
                    };

                    taskView.Add(taskInfo);
                }
                
            }

            if (assignedTo != "00" && assignedTo != null)
                taskView = taskView.Where(i => i.staffList.Contains(assignedTo)).ToList();

            int pageTotal = taskView.Count();

            ViewBag.TaskTotal = pageTotal;

            TaskListInfo taskListInfo = new TaskListInfo()
            {
                taskView = taskView.Skip((page - 1) * PageSize).Take(PageSize),
                cateogires = request.Categories.Where(c => c.version == "1"),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = pageTotal,
                    dateStart = dateStart,
                    dateEnd = dateEnd,
                    completionStatus = pageInfo.PopulateStatus(),
                    assignedTo = assignedTo,
                    status = status,
                    completed = completed,
                    priority = priority,
                    projectCompleted = projectCompleted
                },
                location = request.Locations,
                staffList = request.Staffs,
                status = request.TaskStatus1
            };

            return View(taskListInfo);
        }

        public ViewResult PrintMyTaskList(string dateStart, string dateEnd, string assignedTo, string status, string priority = "false", int page = 1, string completed = "false")
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            IEnumerable<TaskList> taskList = request.TaskLists.OrderBy(i => i.DueDate).Where(r => r.requestID != null);

            List<TaskView> taskView = new List<TaskView>();

            PagingInfo pageInfo = new PagingInfo();

            Regex reg = new Regex(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$");



            if (dateStart != null && reg.IsMatch(dateStart))
            {
                try
                {
                    taskList = taskList.Where(i => i.DueDate >= Convert.ToDateTime(dateStart));
                }
                catch { }
            }

            if (dateEnd != null && reg.IsMatch(dateEnd))
            {
                try
                {
                    taskList = taskList.Where(i => i.DueDate <= Convert.ToDateTime(dateEnd));
                }
                catch { }
            }

            if (completed == "false")
                taskList = taskList.Where(i => i.Status != "Completed");

            if (priority == "true")
                taskList = taskList.Where(i => i.priority == true);

            if (status != null && status != "00")
                taskList = taskList.Where(t => t.Status == status);

            foreach (var task in taskList)
            {
                List<string> staffAssignments = new List<string>();
                Request projectRequest = request.Requests.FirstOrDefault(r => r.reuqestID == task.requestID && r.completed == false && r.categoryID != "timeSheet");

                if (projectRequest != null)
                {
                    var taskAssignments = request.TaskAssignments.Where(t => t.taskID == task.taskID);

                    //if (assignedTo == null)
                    //{
                    foreach (var assignment in taskAssignments)
                    {
                        staffAssignments.Add(assignment.staffID);
                    }
                    //}
                    //else
                    //    staffAssignments.Add(assignedTo);

                    TaskView taskInfo = new TaskView
                    {
                        taskList = task,
                        staffList = staffAssignments,
                        request = projectRequest
                    };

                    taskView.Add(taskInfo);
                }

            }

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            assignedTo = name;

            taskView = taskView.Where(i => i.staffList.Contains(assignedTo)).ToList();

            int pageTotal = taskView.Count();
            TaskListInfo taskListInfo = new TaskListInfo()
            {
                taskView = taskView,
                cateogires = request.Categories.Where(c => c.version == "1"),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = pageTotal,
                    dateStart = dateStart,
                    dateEnd = dateEnd,
                    completionStatus = pageInfo.PopulateStatus(),
                    assignedTo = assignedTo,
                    status = status,
                    completed = completed,
                    priority = priority
                },
                location = request.Locations,
                staffList = request.Staffs
            };

            return View(taskListInfo);
        }

        public ViewResult PrintCase(int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            Request projectRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            return View(projectRequest);
        }

        public ActionResult ViewRequest(int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            var currentRequst = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            if(currentRequst.categoryID == "multi")
            {
                return RedirectToAction("ViewCase", new { requestID = requestID });
            }

            var answers = request.Answers.Where(a => a.requestID == requestID);
            IEnumerable<Models.Attachment> attachments = request.Attachments.Where(a => a.requestID == requestID);
            IEnumerable<Staff> staff = request.Staffs;
            var staffList = request.Staffs.Select(s => s.staffID).ToList();
            IEnumerable<RequestAssignment> requestAssignment = request.RequestAssignments.Where(r => r.requestID == requestID);
            IEnumerable<TaskList> taskList = request.TaskLists.Where(r => r.requestID == requestID).OrderBy(t => t.orderNum);
            List<TaskAssignment> taskAssignment = new List<TaskAssignment>();
            List<AnswerListViewModel> myAnswerList = new List<AnswerListViewModel>();
            


            if (staffList.Contains(name))
            {
                ViewBag.Role = "Agent";
            }
            else
            {
                ViewBag.Role = "User";
            }

            foreach (var task in taskList)
            {
                var currentTasks = request.TaskAssignments.Where(t => t.taskID == task.taskID);

                foreach (var item in currentTasks)
                {
                    taskAssignment.Add(item);
                }

            }

            foreach (var answer in answers)
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

            
            var poNum = request.PONums.Where(r => r.requestID == requestID);

            var currentVendor = request.Vendors.FirstOrDefault(v => v.vendorID == currentRequst.vendor);

            IEnumerable<Vendor> vendorList = request.Vendors;

            IEnumerable<vendorContact> vendorContacts = request.vendorContacts.Where(v => v.vendorID == currentRequst.vendor);

            RequestInfoViewModel requestList = new RequestInfoViewModel
            {
                answerList = myAnswerList,
                attachment = attachments,
                request = currentRequst,
                staff = staff,
                assignments = requestAssignment,
                category = request.Categories.FirstOrDefault(c => c.categoryID == currentRequst.categoryID),
                cateogires = request.Categories.Where(c => c.version == "1"),
                location = request.Locations,
                taskList = taskList,
                taskStatus = request.TaskStatus1,
                templateList = request.Templates,
                taskAssignments = taskAssignment,
                poNum = poNum,
                vendor = currentVendor,
                vendorList = vendorList,
                vendorContacts = vendorContacts
            };

            ViewBag.Position = TempData["ScrollPosition"];

            return View(requestList);
        }

        public ActionResult ViewCase(int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            var subRequests = request.SubRequests.Where(a => a.requestID == requestID);
            IEnumerable<Models.Attachment> attachments = request.Attachments.Where(a => a.requestID == requestID);
            IEnumerable<Staff> staff = request.Staffs;
            var staffList = request.Staffs.Select(s => s.staffID).ToList();
            IEnumerable<RequestAssignment> requestAssignment = request.RequestAssignments.Where(r => r.requestID == requestID);
            var approvers = request.Approvers.Select(a => a.sullivanID).ToList();

            List<SubRequestInfo> mySubRequests = new List<SubRequestInfo>();

            var currentRequst = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            if (currentRequst.categoryID != "multi")
            {
                return RedirectToAction("ViewRequest", new { requestID = requestID });
            }

            if (staffList.Contains(name))
            {
                ViewBag.Role = "Agent";
            }
            else if(approvers.Contains(name))
            {
                ViewBag.Role = "Approver";
            }
            else
            {
                ViewBag.Role = "User";
            }

            foreach (var subRequest in subRequests)
            {
                var subAnswers = request.Answers.Where(a => a.subrequestID == subRequest.subRequestID).ToList();

                List<AnswerListViewModel> myAnswerList = new List<AnswerListViewModel>();
                IEnumerable<TaskList> myTaskList = request.TaskLists.Where(r => r.subRequestID == subRequest.subRequestID).OrderBy(t => t.orderNum);
                List<TaskAssignment> myTaskAssignments = new List<TaskAssignment>();
                var categoryInfo = request.Categories.FirstOrDefault(c => c.categoryID == subRequest.categoryID);

                foreach (var answer in subAnswers)
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

                foreach (var task in myTaskList)
                {
                    var currentTasks = request.TaskAssignments.Where(t => t.taskID == task.taskID);

                    foreach (var item in currentTasks)
                    {
                        myTaskAssignments.Add(item);
                    }

                }



                mySubRequests.Add(new SubRequestInfo
                {
                    subRequest = subRequest,
                    categoryName = categoryInfo.categoryName,
                    answerList = myAnswerList,
                    taskList = myTaskList,
                    taskAssignments = myTaskAssignments
                });
            }


            var poNum = request.PONums.Where(r => r.requestID == requestID);

            var currentVendor = request.Vendors.FirstOrDefault(v => v.vendorID == currentRequst.vendor);

            IEnumerable<Vendor> vendorList = request.Vendors;

            IEnumerable<vendorContact> vendorContacts = request.vendorContacts.Where(v => v.vendorID == currentRequst.vendor);

            RequestInfoViewModel requestList = new RequestInfoViewModel
            {
                subRequests = mySubRequests,
                attachment = attachments,
                request = currentRequst,
                staff = staff,
                assignments = requestAssignment,
                category = request.Categories.FirstOrDefault(c => c.categoryID == currentRequst.categoryID),
                cateogires = request.Categories.Where(c => c.version == "1"),
                location = request.Locations,
                taskStatus = request.TaskStatus1,
                templateList = request.Templates,
                //taskAssignments = taskAssignment,
                poNum = poNum,
                vendor = currentVendor,
                vendorList = vendorList,
                vendorContacts = vendorContacts
            };

            ViewBag.Position = TempData["ScrollPosition"];

            return View(requestList);
        }

        [AuthorizeUser]
        public ActionResult RequestInfo(int requestID, string currentTrackID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            var answers = request.Answers.Where(a => a.requestID == requestID);
            IEnumerable<Models.Attachment> attachments = request.Attachments.Where(a => a.requestID == requestID);
            IEnumerable<Staff> staff = request.Staffs;
            IEnumerable<RequestAssignment> requestAssignment = request.RequestAssignments.Where(r => r.requestID == requestID);
            IEnumerable<TaskList> taskList = request.TaskLists.Where(r => r.requestID == requestID).OrderBy(t => t.DueDate).OrderBy(t => t.orderNum);
            List<TaskAssignment> taskAssignment = new List<TaskAssignment>();
            List<AnswerListViewModel> myAnswerList = new List<AnswerListViewModel>();

            var currentRequst = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            if (currentRequst.categoryID == "multi")
            {
                return RedirectToAction("EditCase", new { requestID = requestID });
            }

            foreach (var task in taskList)
            {
                var currentTasks = request.TaskAssignments.Where(t => t.taskID == task.taskID);

                foreach (var item in currentTasks)
                {
                    taskAssignment.Add(item);
                }
                
            }

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

            var poNum = request.PONums.Where(r => r.requestID == requestID);

            var currentVendor = request.Vendors.FirstOrDefault(v => v.vendorID == currentRequst.vendor);

            IEnumerable<Vendor> vendorList = request.Vendors;

            IEnumerable<vendorContact> vendorContacts = request.vendorContacts.Where(v => v.vendorID == currentRequst.vendor);

            RequestInfoViewModel requestList = new RequestInfoViewModel
            {
                answerList = myAnswerList,
                attachment = attachments,
                request = currentRequst,
                staff = staff,
                assignments = requestAssignment,
                category = request.Categories.FirstOrDefault(c => c.categoryID == currentRequst.categoryID),
                cateogires = request.Categories.Where(c => c.version == "1"),
                location = request.Locations.OrderBy(l => l.name) ,
                taskList = taskList,
                taskStatus = request.TaskStatus1,
                templateList = request.Templates,
                taskAssignments = taskAssignment,
                poNum = poNum,
                projectStatus = request.ProjectStatus1.OrderBy(s => s.status),
                vendor = currentVendor,
                vendorList = vendorList,
                vendorContacts = vendorContacts
            };

            ViewBag.Position = TempData["ScrollPosition"];

            return View(requestList);
        }

        [AuthorizeUser]
        public ActionResult EditCase(int requestID, string currentTrackID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            var subRequests = request.SubRequests.Where(a => a.requestID == requestID);
            IEnumerable<Models.Attachment> attachments = request.Attachments.Where(a => a.requestID == requestID);
            IEnumerable<Staff> staff = request.Staffs.OrderBy(s => s.firstName);
            var staffList = request.Staffs.Select(s => s.staffID).ToList();
            IEnumerable<RequestAssignment> requestAssignment = request.RequestAssignments.Where(r => r.requestID == requestID);

            List<SubRequestInfo> mySubRequests = new List<SubRequestInfo>();

            var currentRequst = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            if (currentRequst.categoryID != "multi")
            {
                return RedirectToAction("RequestInfo", new { requestID = requestID });
            }

            if (staffList.Contains(name))
            {
                ViewBag.Role = "Agent";
            }
            else
            {
                ViewBag.Role = "User";
            }


            foreach (var subRequest in subRequests)
            {
                var subAnswers = request.Answers.Where(a => a.subrequestID == subRequest.subRequestID).ToList();

                List<AnswerListViewModel> myAnswerList = new List<AnswerListViewModel>();
                IEnumerable<TaskList> myTaskList = request.TaskLists.Where(r => r.subRequestID == subRequest.subRequestID).OrderBy(t => t.orderNum);
                List<TaskAssignment> myTaskAssignments = new List<TaskAssignment>();
                var categoryInfo = request.Categories.FirstOrDefault(c => c.categoryID == subRequest.categoryID);

                foreach (var answer in subAnswers)
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

                foreach (var task in myTaskList)
                {
                    var currentTasks = request.TaskAssignments.Where(t => t.taskID == task.taskID);

                    foreach (var item in currentTasks)
                    {
                        myTaskAssignments.Add(item);
                    }

                }

                mySubRequests.Add(new SubRequestInfo
                {
                    subRequest = subRequest,
                    categoryName = categoryInfo.categoryName,
                    answerList = myAnswerList,
                    taskList = myTaskList,
                    taskAssignments = myTaskAssignments
                });
            }


            var poNum = request.PONums.Where(r => r.requestID == requestID);

            var currentVendor = request.Vendors.FirstOrDefault(v => v.vendorID == currentRequst.vendor);

            IEnumerable<Vendor> vendorList = request.Vendors;

            IEnumerable<vendorContact> vendorContacts = request.vendorContacts.Where(v => v.vendorID == currentRequst.vendor);

            RequestInfoViewModel requestList = new RequestInfoViewModel
            {
                subRequests = mySubRequests,
                attachment = attachments,
                request = currentRequst,
                staff = staff,
                assignments = requestAssignment,
                category = request.Categories.FirstOrDefault(c => c.categoryID == currentRequst.categoryID),
                cateogires = request.Categories.Where(c => c.version == "2"),
                location = request.Locations,
                taskStatus = request.TaskStatus1,
                templateList = request.Templates,
                //taskAssignments = taskAssignment,
                poNum = poNum,
                projectStatus = request.ProjectStatus1.OrderBy(s => s.status),
                vendor = currentVendor,
                vendorList = vendorList,
                vendorContacts = vendorContacts
            };

            ViewBag.Position = TempData["ScrollPosition"];

            return View(requestList);
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult EditRequest(FormCollection form, HttpPostedFileBase uploadFile)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            FieldInfo cleaner = new FieldInfo();

            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                CreateDirectory(requestID);

                var fileName = System.IO.Path.GetFileName(uploadFile.FileName);

                //Clear harmfull characters from file name
                fileName = cleaner.CleanAttahcmentName(fileName);

                var path = System.IO.Path.Combine(Server.MapPath("~/Attachments/"), requestID.ToString() + "/" + fileName);
                uploadFile.SaveAs(path);

                Models.Attachment attachment = new Models.Attachment();
                attachment.requestID = requestID;
                attachment.filePath = requestID.ToString() + "/" + fileName;
                attachment.dateAdded = DateTime.Now;
                request.AddToAttachments(attachment);
                request.SaveChanges();
            }

            EditRequesterInfo(form);

            EditAssignment(form);
            EditRequestContent(form);

            var subrequests = request.SubRequests.FirstOrDefault(r => r.requestID == requestID);

            if (subrequests == null)
                EditTaskList(form);
            else
                EditTaskListNew(form);

            SendAddtionalInfo(requestID);

            TempData["ScrollPosition"] = Request.Form["scrollPosition"];

            if (subrequests == null)
            {

                if (Request.QueryString["position"] == null)
                    return RedirectToAction("RequestInfo", new { requestID = requestID });
                else
                    return RedirectToAction("RequestInfo", new { requestID = requestID, position = (string)Request.QueryString["position"] });
            }
            else
            {
                if (Request.QueryString["position"] == null)
                    return RedirectToAction("EditCase", new { requestID = requestID });
                else
                    return RedirectToAction("EditCase", new { requestID = requestID, position = (string)Request.QueryString["position"] });
            }
        }

        [AuthorizeUser]
        public void CloseRequest(int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            var editRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");
            MailMessage mail = new MailMessage();
            
            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;
            mail.From = new MailAddress("ProjectRequest@CC.com");

            //mail.To.Add("istarbuck@sullivan.edu");

            //if (editRequest.categoryID == "website")
            //    mail.CC.Add("hmobley@sullivan.edu");

            editRequest.completed = true;

            editRequest.status = "Completed";

            mail.To.Clear();
            try
            {
                mail.To.Add(editRequest.email);

                mail.Subject = "Creative Communications Project Request Completed";

                mail.Body = "Your request of '" + editRequest.projectName + "' has been been completed. Project number: " + editRequest.customID;
                mailer.Send(mail);
            }
            catch { }

            request.SaveChanges();
        }

        public ActionResult ApproveRequest(int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            var editRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            string name = GetApproverInfo();

            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");
            MailMessage mail = new MailMessage();

            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;
            mail.From = new MailAddress("ProjectRequest@CC.com");

            editRequest.status = "Approved";
            editRequest.approvedBy = name;
            editRequest.denialReason = Request.Form["approvalReason"].ToString();

            mail.To.Clear();
            try
            {
                mail.To.Add(editRequest.email);

                mail.Subject = "Creative Communications Project Request Approved";

                mail.Body = "<p>Your Creative Communications project request, '" + editRequest.projectName + "', has been approved. Project number: " + editRequest.reuqestID + "</p>" +
                            "<p>Please review any comments below. </p>" +
                            "<p>" + Request.Form["approvalReason"].ToString() + "</p>" +
                            "<p>If you have any questions, please email me at <a href='mailTo:snallen@sullivan.edu'></a>snallen@sullivan.edu.</p>";
                mailer.Send(mail);
            }
            catch { }

            request.SaveChanges();

            return RedirectToAction("ViewRequest", new { requestID = requestID });
        }

        public ActionResult DenyRequest(FormCollection form, int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            var editRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            string name = GetApproverInfo();

            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");
            MailMessage mail = new MailMessage();
        
            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;
            mail.From = new MailAddress("ProjectRequest@CC.com");

            string mailBody = "<html>";
            mailBody += "<p>Your Creative Communications project request, '" + editRequest.projectName + "' has been been denied by " + name + ". Project number: " + editRequest.reuqestID + "</p>";
            mailBody += "<p>Please review the comments below before re-submitting the project. </p>";
            mailBody += "<p>" + Request.Form["denialReason"].ToString() + "</p>"
                     + "<p>If you have any questions, please email me at <a href='mailTo:snallen@sullivan.edu'></a>snallen@sullivan.edu.</p>";
            mailBody += "</html>";

            editRequest.status = "Denied";
            editRequest.approvedBy = name;
            editRequest.denialReason = Request.Form["denialReason"].ToString();
            editRequest.completed = true;

            mail.To.Clear();

            mail.To.Add(editRequest.email);

            mail.Subject = "Creative Communications Project Request Denied";

            mail.Body = mailBody;
            mailer.Send(mail);

            request.SaveChanges();

            return RedirectToAction("ViewRequest", new { requestID = requestID });
        }

        public string GetApproverInfo()
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            var staff = request.Staffs.Select(c => c.staffID).ToList();

            var approvers = request.Approvers.Select(c => c.sullivanID).ToList();

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            if(staff.Contains(name))
            {
                var staffName = request.Staffs.FirstOrDefault(c => c.staffID == name);

                name = staffName.firstName + " " + staffName.lastName;
            }
            else if(approvers.Contains(name))
            {
                var approverName = request.Approvers.FirstOrDefault(c => c.sullivanID == name);

                name = approverName.name;
            }

            return name;
        }


        [AuthorizeUser]
        public void EditRequesterInfo(FormCollection form)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);
            name = name.Remove(0, 8).ToLower();

            IEnumerable<Staff> staff = request.Staffs;

            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            bool completed;
            
            if(Request.Form["completed"].ToString() == "Yes" || Request.Form["projectStatus"].ToString() == "Completed")
            {
                completed = true;
            }
            else
            {
                completed = false;
            }
           
            //Search for the Project you wish to update
            var editRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            if (completed)
            {
                CloseRequest(requestID);
            }
            else 
            {
                editRequest.completed = false;
                editRequest.status = Request.Form["projectStatus"].ToString();
            }

            try
            {
                editRequest.dateRequested = Convert.ToDateTime(Request.Form["date"]);
            }
            catch
            {}

            try
            {
                editRequest.dueDate = Convert.ToDateTime(Request.Form["dueDate"]);
            }
            catch {
                if (Request.Form["dueDate"].ToString().Count() < 2 || Request.Form["dueDate"] == null)
                {
                    editRequest.dueDate = null;
                }
            }

            editRequest.location = Request.Form["location"].ToString();
            editRequest.name = Request.Form["name"].ToString();
            editRequest.categoryID = Request.Form["category"].ToString();
            editRequest.department = Request.Form["department"].ToString();
            editRequest.projectName = Request.Form["projectName"].ToString();
            editRequest.email = Request.Form["email"].ToString();
            editRequest.contactInfo = Request.Form["contactInfo"].ToString();
            editRequest.additionalInfo = Request.Form["additionalInfo"].ToString();
            editRequest.vendor = Request.Form["vendor"].ToString();

            if (editRequest.categoryID != "multi" && editRequest.categoryID != "Multi")
            {
                try
                {
                    editRequest.detail = Request.Form["details"].ToString();
                    editRequest.comment = Request.Form["comments"].ToString();
                    editRequest.shipTo = Request.Form["shipTo"].ToString();
                    editRequest.subtotal = Request.Form["subtotal"].ToString();
                    editRequest.tax = Request.Form["tax"].ToString();
                    editRequest.total = Request.Form["total"].ToString();
                    editRequest.Quantity = Request.Form["Quantity"].ToString();
                    editRequest.printerSpecifications = Request.Form["printerSpecifications"].ToString();

                    try
                    {

                        editRequest.cost = Convert.ToDouble(Request.Form["cost"].ToString().Replace("$", ""));
                    }
                    catch
                    {
                        if (Request.Form["cost"].ToString().Count() < 2 || Request.Form["cost"] == null)
                        {
                            editRequest.cost = null;
                        }
                    }
                }
                catch { }
            }

            request.SaveChanges();

        }

        [AuthorizeUser]
        public void EditRequestContent(FormCollection form)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            var answers = request.Answers.Where(a => a.requestID == requestID);

            foreach (var answer in answers)
            {
                try
                {
                    var MasterAnswer = request.Answers.FirstOrDefault(a => a.AnswerID == answer.AnswerID);
                    MasterAnswer.AnswerText = Request.Form[MasterAnswer.AnswerID.ToString()].ToString();
                }
                catch{

                }

            }

            var attachments = request.Attachments.Where(a => a.requestID == requestID);

            foreach (var file in attachments)
            {
                try
                {
                    bool delete;

                    if (Request.Form["Delete_" + file.attachmentID] != null)
                        delete = true;
                    else
                        delete = false;

                    if (delete == true)
                    {
                        request.Attachments.DeleteObject(file);

                        

                        System.IO.File.Delete(@"C:\websites\secure.sullivan.edu\ProjectRequest\ProjectRequest\Attachments\" + file.filePath);
                    }

                }
                catch { }
            }

            request.SaveChanges();

        }


        public void CreateDirectory(int requestID)
        {
            string filePath = @"C:\websites\secure.sullivan.edu\ProjectRequest\ProjectRequest\Attachments\";

            if (!Directory.Exists(filePath + requestID.ToString()))
            {
                DirectoryInfo di = Directory.CreateDirectory(filePath + requestID.ToString());
            }
        }

        [AuthorizeUser]
        public void EditAssignment(FormCollection form)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            Staff lastName = request.Staffs.FirstOrDefault(s => s.staffID == name);

            string emailBody;

            IEnumerable<Staff> staff = request.Staffs;

            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            Dictionary<string, bool> staffing = new Dictionary<string, bool>();

            bool assignID = false;

            foreach (var employee in staff)
            {
                bool assigned;

                if (Request.Form["CB_" + employee.staffID] != null)
                {
                    assigned = true;
                    assignID = true;
                }
                else
                    assigned = false;

                staffing.Add(employee.staffID, assigned);
            }

            if (assignID)
                AssignID(requestID);

            foreach(var assignment in staffing)
            {
                var editRequestAssignment = request.RequestAssignments.FirstOrDefault(a => a.requestID == requestID && a.staffID == assignment.Key); 

                if(assignment.Value == true)
                {
                    if (editRequestAssignment == null)
                    {
                        RequestAssignment myRequest = new RequestAssignment();
                        var editRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

                        myRequest.requestID = requestID;
                        myRequest.staffID = assignment.Key;
                        request.AddToRequestAssignments(myRequest);
                        request.SaveChanges();

                        var projectName = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

                        emailBody = "<p>Hello CC Agent,</p>"
                       + "<p>You have been added to the following case: <b>" + projectName.projectName + "</b></p>";

                        if(editRequest.categoryID == "multi")
                        {
                            emailBody += "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/EditCase?requestID="
                            + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewCase?requestID="
                            + requestID.ToString() + "'>Click Here</a> to view the case</p>";
                        }
                        else
                        {
                            emailBody += "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                            + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewRequest?requestID="
                            + requestID.ToString() + "'>Click Here</a> to view the case</p>";
                        }

                        emailBody += "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                        SendEmail(assignment.Key + "@Sullivan.edu", emailBody, projectName.projectName);

                        if(assignment.Key.ToLower() == "schoate")
                        {
                            Trello card = new Trello();

                            try
                            {
                                card.CreateCard();
                            }
                            catch(Exception ex)
                            {
                                SendEmail("istarbuck@Sullivan.edu", ex.ToString(), "Trello Error");
                            }
                        }
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
            
        }

        [AuthorizeUser]
        public ActionResult EditTaskList(FormCollection form)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            Staff lastName = request.Staffs.FirstOrDefault(s => s.staffID == name);

            var requestInfo = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            var tasks = request.TaskLists.Where(r => r.requestID == requestID);

            foreach (var task in tasks)
            {
                try
                {
                    bool delete;
                    bool priority;
                    var currentTask = request.TaskLists.FirstOrDefault(a => a.taskID == task.taskID);

                    if (Request.Form["Delete_" + task.taskID] != null)
                        delete = true;
                    else
                        delete = false;

                    if (Request.Form["Prioritize_" + task.taskID] != null)
                        priority = true;
                    else
                        priority = false;

                    if (delete == false)
                    {
                        try
                        {
                            currentTask.orderNum = Convert.ToInt16(Request.Form["Order_" + task.taskID]);
                        }
                        catch { }

                        currentTask.Task = Request.Form["Task_" + task.taskID].ToString();
                        currentTask.Comments = Request.Form["Comments_" + task.taskID].ToString();

                        try
                        {
                            currentTask.DueDate = Convert.ToDateTime(Request.Form["DueDate_" + task.taskID]);
                        }
                        catch
                        {
                            if (Request.Form["DueDate_" + task.taskID].ToString().Count() < 2 || Request.Form["DueDate_" + task.taskID] == null)
                            {
                                currentTask.DueDate = null;
                            }
                        }

                        if (currentTask.Status != "Working")
                        {
                            if (Request.Form["Status_" + task.taskID].ToString() == "Working")
                            {
                                string assign = Request.Form["Assignment_" + task.taskID].ToString().Replace(" ", "");
                                assign = assign.Replace("  ", "");
                                string[] assignments = assign.Split(',');
                                string emailBody;
                                var projectName = request.Requests.FirstOrDefault(r => r.reuqestID == task.requestID);

                                emailBody = "<p>Hello CC Agent,</p>"
                                + "<p>You have been assigned the following task: <b>" + task.Task + "</b></p>"
                                + "<p>This task is from: <b>" + projectName.projectName + "</b> "
                                + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                                + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                                + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewRequest?requestID="
                                + requestID.ToString() + "'>Click Here</a> to view the case</p>"
                                + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                                foreach (var staff in assignments)
                                {
                                    if (staff.Length > 0)
                                        SendEmail(staff + "@Sullivan.edu", emailBody, projectName.projectName);
                                }

                            }
                        }

                        if (requestInfo.status == "Completed" || requestInfo.status == "On Hold")
                        {
                            if (currentTask.Status != "Completed" && Request.Form["Status_" + task.taskID].ToString() != "Completed")
                            {
                                currentTask.Status = requestInfo.status;
                            }
                            else
                            {
                                currentTask.Status = "Completed";
                            }
                        }
                        else
                        {
                            currentTask.Status = Request.Form["Status_" + task.taskID].ToString();
                        }
                        currentTask.priority = priority;

                        if (Request.Form["Status_" + task.taskID].ToString() != "Completed")
                            AssignTasks(currentTask, requestID, Request.Form["Assignment_" + task.taskID].ToString().Replace(" ", ""), Request.Form["Status_" + task.taskID].ToString());
                    }
                    else if (delete == true)
                    {
                        var taskAsignment = request.TaskAssignments.Where(t => t.taskID == task.taskID);
                        foreach (var assignment in taskAsignment)
                        {
                            request.TaskAssignments.DeleteObject(assignment);
                        }

                        request.TaskLists.DeleteObject(currentTask);
                    }

                }

                catch { }

            }// end task foreach

            request.SaveChanges();

            if (Request.Form["Task"] != null && Request.Form["Task"].ToString().Length > 0)
            {
                TaskList newTask = new TaskList();
                bool priority;

                newTask.requestID = requestID;
                try
                {
                    newTask.orderNum = Convert.ToInt16(Request.Form["Order"]);
                }
                catch{}

                newTask.Task = Request.Form["Task"].ToString();

                newTask.Comments = Request.Form["TaskComments"].ToString();

                try
                {
                    newTask.DueDate = Convert.ToDateTime(Request.Form["DateDue"]);
                }
                catch
                {
                    if (Request.Form["DateDue"].ToString().Count() < 2 || Request.Form["DateDue"] == null)
                    {
                        newTask.DueDate = null;
                    }
                }

                try
                {
                    newTask.Status = Request.Form["Status"].ToString();
                }
                catch { }

                if (Request.Form["Prioritize"] != null)
                    priority = true;
                else
                    priority = false;

                newTask.priority = priority;

                request.AddToTaskLists(newTask);
                request.SaveChanges();

                if (Request.Form["Status"].ToString() == "Working")
                {
                    string assign = Request.Form["Assignment"].ToString().Replace(" ", "");
                    assign = assign.Replace("  ", "");
                    string[] assignments = assign.Split(',');
                    string emailBody;
                    var projectName = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

                    emailBody = "<p>Hello CC Agent,</p>"
                    + "<p>You have been assigned the following task: <b>" + Request.Form["Task"].ToString() + "</b></p>"
                    + "<p>This task is from: <b>" + projectName.projectName + "</b> "
                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                    + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewRequest?requestID="
                    + requestID.ToString() + "'>Click Here</a> to view the case</p>"
                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                    foreach (var staff in assignments)
                    {
                        if (staff.Length > 0)
                            SendEmail(staff + "@Sullivan.edu", emailBody, projectName.projectName);
                    }

                }

                AssignTasks(newTask, requestID, Request.Form["Assignment"].ToString().Replace(" ", ""), Request.Form["Status"].ToString());
            }

            if (Request.Form["Template"].ToString() != "00")
            {
                int templateID = Convert.ToInt16(Request.Form["Template"]);

                var templateItems = request.TaskLists.Where(t => t.TemplateID == templateID);

                using (ProjectRequestEntities project = new ProjectRequestEntities())
                {
                    foreach (var task in templateItems)
                    {
                        string assignments = "";
                        TaskList newTask = new TaskList();
                        IEnumerable<TaskAssignment> taskAssignments = project.TaskAssignments.Where(t => t.taskID == task.taskID && t.templateID == templateID); 

                        newTask.requestID = requestID;
                        newTask.orderNum = task.orderNum;
                        newTask.Task = task.Task;
                        //newTask.DueDate = task.DueDate;
                        newTask.Status = task.Status;

                        project.AddToTaskLists(newTask);
                        project.SaveChanges();

                        foreach (var staff in taskAssignments)
                        {
                            assignments += staff.staffID + ",";
                        }

                        if (assignments.Length > 0)
                        {
                            AssignTasks(newTask, requestID, assignments.Replace(" ", ""), null);
                        }
                    }
                }
            
            }

            TempData["ScrollPosition"] = Request.Form["scrollPosition"];

            return RedirectToAction("RequestInfo", new { requestID = requestID });
        }

        [AuthorizeUser]
        public ActionResult EditTaskListNew(FormCollection form)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            Staff lastName = request.Staffs.FirstOrDefault(s => s.staffID == name);

            var requestInfo = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            var subRequests = request.SubRequests.Where(s => s.requestID == requestID);

            foreach (var subRequest in subRequests)
            {
                var tasks = request.TaskLists.Where(r => r.subRequestID == subRequest.subRequestID);

                foreach (var task in tasks)
                {
                    try
                    {
                        bool delete;
                        var currentTask = request.TaskLists.FirstOrDefault(a => a.taskID == task.taskID);

                        if (Request.Form["Delete_" + task.taskID] != null)
                            delete = true;
                        else
                            delete = false;


                        if (delete == false)
                        {
                            try
                            {
                                currentTask.orderNum = Convert.ToInt16(Request.Form["Order_" + task.taskID]);
                            }
                            catch { }

                            currentTask.Task = Request.Form["Task_" + task.taskID].ToString();
                            currentTask.Comments = Request.Form["Comments_" + task.taskID].ToString();

                            try
                            {
                                currentTask.DueDate = Convert.ToDateTime(Request.Form["DueDate_" + task.taskID]);
                            }
                            catch
                            {
                                if (Request.Form["DueDate_" + task.taskID].ToString().Count() < 2 || Request.Form["DueDate_" + task.taskID] == null)
                                {
                                    currentTask.DueDate = null;
                                }
                            }

                            if (currentTask.Status != "Working")
                            {
                                if (Request.Form["Status_" + task.taskID].ToString() == "Working")
                                {
                                    string assign = Request.Form["Assignment_" + task.taskID].ToString().Replace(" ", "");
                                    assign = assign.Replace("  ", "");
                                    string[] assignments = assign.Split(',');
                                    string emailBody;
                                    var projectName = request.Requests.FirstOrDefault(r => r.reuqestID == task.requestID);

                                    emailBody = "<p>Hello CC Agent,</p>"
                                    + "<p>You have been assigned the following task: <b>" + task.Task + "</b></p>"
                                    + "<p>This task is from: <b>" + projectName.projectName + "</b> "
                                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                                    + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewRequest?requestID="
                                    + requestID.ToString() + "'>Click Here</a> to view the case</p>"
                                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                                    foreach (var staff in assignments)
                                    {
                                        if (staff.Length > 0)
                                            SendEmail(staff + "@Sullivan.edu", emailBody, projectName.projectName);
                                    }

                                }
                            }

                            if (requestInfo.status == "Completed" || requestInfo.status == "On Hold")
                            {
                                if (currentTask.Status != "Completed" && Request.Form["Status_" + task.taskID].ToString() != "Completed")
                                {
                                    currentTask.Status = requestInfo.status;
                                }
                                else
                                {
                                    currentTask.Status = "Completed";
                                }
                            }
                            else
                            {
                                currentTask.Status = Request.Form["Status_" + task.taskID].ToString();
                            }


                            if (Request.Form["Status_" + task.taskID].ToString() != "Completed")
                                AssignTasks(currentTask, requestID, Request.Form["Assignment_" + task.taskID].ToString().Replace(" ", ""), Request.Form["Status_" + task.taskID].ToString());
                        }
                        else if (delete == true)
                        {
                            var taskAsignment = request.TaskAssignments.Where(t => t.taskID == task.taskID);
                            foreach (var assignment in taskAsignment)
                            {
                                request.TaskAssignments.DeleteObject(assignment);
                            }

                            request.TaskLists.DeleteObject(currentTask);
                        }

                    }

                    catch { }

                }// end task foreach

                if (Request.Form["Task_" + subRequest.subRequestID] != null && Request.Form["Task_" + subRequest.subRequestID].ToString().Length > 0)
                {
                    ProjectRequestEntities requestAssignment = new ProjectRequestEntities();

                    TaskList newTask = new TaskList();

                    newTask.requestID = requestID;
                    newTask.subRequestID = subRequest.subRequestID;
                    try
                    {
                        newTask.orderNum = Convert.ToInt16(Request.Form["Order_" + subRequest.subRequestID]);
                    }
                    catch { }

                    newTask.Task = Request.Form["Task_" + subRequest.subRequestID].ToString();

                    newTask.Comments = Request.Form["TaskComments_" + subRequest.subRequestID].ToString();

                    try
                    {
                        newTask.DueDate = Convert.ToDateTime(Request.Form["DateDue_" + subRequest.subRequestID]);
                    }
                    catch
                    {
                        if (Request.Form["DateDue_" + subRequest.subRequestID].ToString().Count() < 2 || Request.Form["DateDue_" + subRequest.subRequestID] == null)
                        {
                            newTask.DueDate = null;
                        }
                    }

                    try
                    {
                        newTask.Status = Request.Form["Status_" + subRequest.subRequestID].ToString();
                    }
                    catch { }

                    requestAssignment.AddToTaskLists(newTask);
                    requestAssignment.SaveChanges();

                    if (Request.Form["Status_" + subRequest.subRequestID].ToString() == "Working")
                    {
                        string assign = Request.Form["Assignment_" + subRequest.subRequestID].ToString().Replace(" ", "");
                        assign = assign.Replace("  ", "");
                        string[] assignments = assign.Split(',');
                        string emailBody;
                        var projectName = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

                        emailBody = "<p>" + Request.Form["Assignment_" + +subRequest.subRequestID].ToString().Replace(" ", "") + "</p>"
                        + "<p>You have been assigned the following task: <b>" + Request.Form["Task_" + subRequest.subRequestID].ToString() + "</b></p>"
                        + "<p>This task is from: <b>" + projectName.projectName + "</b> "
                        + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                        + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                        + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewRequest?requestID="
                        + requestID.ToString() + "'>Click Here</a> to view the case</p>"
                        + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                        foreach (var staff in assignments)
                        {
                            if (staff.Length > 0)
                                SendEmail(staff + "@Sullivan.edu", emailBody, projectName.projectName);
                        }

                    }

                    AssignTasks(newTask, requestID, Request.Form["Assignment_" + +subRequest.subRequestID].ToString().Replace(" ", ""), Request.Form["Status_" + subRequest.subRequestID].ToString());

                    requestAssignment.Dispose();
                }

            }// End subRequests foreach loop

            request.SaveChanges();

            TempData["ScrollPosition"] = Request.Form["scrollPosition"];

            request.Dispose();

            return RedirectToAction("EditCase", new { requestID = requestID });
        }

        [AuthorizeUser]
        public void AssignTasks(TaskList task, int requestID, string assignmentList, string status)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string name = Convert.ToString(User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            Staff lastName = request.Staffs.FirstOrDefault(s => s.staffID == name);

            var staffList = request.Staffs;
            string assign = assignmentList;
            assign = assign.Replace("  ", "");
            string[] assignments = assign.Split(',');
            Dictionary<string, bool> staffAssignments = new Dictionary<string, bool>();
            bool assignID = false;

            foreach (var staff in staffList)
            {
                if (assignments.Contains(staff.staffID.ToString()))
                {
                    staffAssignments.Add(staff.staffID.ToString(), true);
                    assignID = true;
                }
                else
                    staffAssignments.Add(staff.staffID.ToString(), false);
            }

            if (assignID) 
                AssignID(requestID);

            using (ProjectRequestEntities project = new ProjectRequestEntities())
            {
                foreach (var assignment in staffAssignments)
                {
                    string emailBody;

                    var editTaskAssignment = project.TaskAssignments.FirstOrDefault(a => a.taskID == task.taskID && a.staffID == assignment.Key);

                    if (assignment.Value == true)
                    {
                        if (editTaskAssignment == null)
                        {
                            TaskAssignment myTask = new TaskAssignment();

                            myTask.taskID = task.taskID;
                            myTask.staffID = assignment.Key;
                            project.AddToTaskAssignments(myTask);

                            project.SaveChanges();

                            var projectName = project.Requests.FirstOrDefault(r => r.reuqestID == task.requestID);

                            //if (status == "Working" || status == "Pending")
                            //{
                            //    emailBody = "<p>You have been assigned the following task: <b>" + task.Task + "</b></p>"
                            //    + "<p>This task is from: <b>" + projectName.projectName + "</b> "
                            //    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                            //    + requestID.ToString() + "'>Click Here</a> to view the project</p>"
                            //    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                            //    SendEmail(assignment.Key + "@Sullivan.edu", emailBody);
                            //}
                        }

                    }
                    else if (assignment.Value == false)
                    {
                        if (editTaskAssignment != null)
                        {
                            project.TaskAssignments.DeleteObject(editTaskAssignment);
                            project.SaveChanges();
                        }
                    }

                    var editRequestAssignment = project.RequestAssignments.FirstOrDefault(a => a.requestID == requestID && a.staffID == assignment.Key);

                    if (assignment.Value == true)
                    {
                        if (editRequestAssignment == null)
                        {
                            RequestAssignment myRequest = new RequestAssignment();

                            myRequest.requestID = requestID;
                            myRequest.staffID = assignment.Key;
                            project.AddToRequestAssignments(myRequest);

                            project.SaveChanges();

                            var projectName = project.Requests.FirstOrDefault(r => r.reuqestID == task.requestID);

                            emailBody = "<p>Hello CC Agent,</p>"
                            + "<p>You have been assigned to the following case: <b>" + projectName.projectName + "</b></p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                            + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewRequest?requestID="
                            + requestID.ToString() + "'>Click Here</a> to view the case</p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                            SendEmail(assignment.Key + "@Sullivan.edu", emailBody, projectName.projectName);
                        }

                    }
                }//end assignment foreach
            }//end using statement
        }

        public void AssignID(int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            Request project = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            string emailBody;
            MailMessage mail = new MailMessage();
            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");
            //mail.Bcc.Add(new MailAddress("websupport@sullivan.edu"));
            //mail.Bcc.Add(new MailAddress("istarbuck@sullivan.edu"));

            string name = Convert.ToString(User.Identity.Name);
            name = name.Remove(0, 8).ToLower();

            //mail.CC.Add(name + "@sullivan.edu");
            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;
            mail.From = new MailAddress("ProjectRequest@CC.com");

            if (project != null)
            {
                if (project.customID == null || project.customID.Count() < 2)
                {
                    project.customID = DateTime.Now.Year.ToString() + " " + project.location + " " + requestID.ToString();
                    project.status = "Assigned";

                    request.SaveChanges();

                    try
                    {
                        mail.To.Add(project.email);

                        mail.Subject = "Creative Communications Project Request Assigned";

                        mail.Body = "<p>Your Creative Communications project request, " + project.projectName + " , has now been assigned. </p>"
                        + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewCase?requestID=" + project.reuqestID + "'>Project Info</a></p>"
                        + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/User/MyRequests'>All my projects</a></p>"
                        + "<p>If you have any questions, please email me at <a href='mailTo:snallen@sullivan.edu'></a>snallen@sullivan.edu.</p>";
                        mailer.Send(mail);
                    }
                    catch { }


                }
            }
        }

        private void SendEmail(string To, string emailBody, string projectName)
        {
            MailMessage mail = new MailMessage();
            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");

            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;

            string name = Convert.ToString(User.Identity.Name);
            name = name.Remove(0, 8).ToLower();

            mail.From = new MailAddress("CCFiles@Truth.com");
            mail.To.Add(To);
            //mail.CC.Add("istarbuck@Sullivan.edu"); 
            mail.Body = emailBody;
            mail.Subject = "CC Case File Assignment: " + projectName;

            mailer.Send(mail);
        }

        private void SendAddtionalInfo(int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            string To = Request.Form["TO"].ToString();
            string emailBody = "<p>Hello CC Agent,</p>" + "<p>" + Request.Form["emailBody"].ToString() + "</p>";
            MailMessage mail = new MailMessage();
            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");

            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;

            mail.From = new MailAddress("CCFiles@Truth.com");

            string name = Convert.ToString(User.Identity.Name);
            name = name.Remove(0, 8).ToLower();
            Staff lastName = request.Staffs.FirstOrDefault(s => s.staffID == name);

            mail.CC.Add(name + "@Sullivan.edu"); 

            if (To.Length > 1 && emailBody.Length > 1)
            {
                var projectName = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

                To = To.Replace("  ", "");
                string[] recipients = To.Split(',');

                foreach (var recipient in recipients)
                {
                    if (recipient.Length > 1)
                    {
                        mail.To.Add(recipient + "@Sullivan.edu");
                    }
                }

                emailBody += "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                    + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewRequest?requestID="
                    + requestID.ToString() + "'>Click Here</a> to view the case</p>"
                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                mail.Body = emailBody;
                mail.Subject = "CC Case File Update: " + projectName.projectName;

                mailer.Send(mail);
            }
        }

        [HttpPost]
        public void SendExternalEmail(int requestID, string sendTo)
        {
            ProjectRequestEntities project = new ProjectRequestEntities();

            RequestInfo info = new Models.RequestInfo();

            RequestInfoViewModel modelInfo = info.GetRequestInfo(requestID);

            string To = sendTo;

            To = To.Replace("  ", "");
            string[] recipients = To.Split(','); 

            MailMessage mail = new MailMessage();
            SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");

            mail.IsBodyHtml = true;
            mailer.EnableSsl = false;

            mail.From = new MailAddress("CCFiles@Truth.com");

            string name = Convert.ToString(User.Identity.Name);
            name = name.Remove(0, 8).ToLower();
            Staff lastName = project.Staffs.FirstOrDefault(s => s.staffID == name);

            foreach (string emailTo in recipients)
            {
                if(emailTo.Length > 1)
                    mail.To.Add(emailTo);
            }

            

            mail.CC.Add(name + "@Sullivan.edu"); 

            string emailBody = "";
            emailBody += "<p><b>Completed? </b>" + modelInfo.request.completed.ToString() + "</p>"
    
                        + "<p><b>CT# </b>" + modelInfo.request.currentTrackID + "</p>"

                        + "<p><b>CC ID </b>" + modelInfo.request.customID + "</p>"

                        + "<p><b>Date Submitted: </b>" + modelInfo.request.dateRequested + "</p>"

                        + "<p><b>Due Date: </b>" + modelInfo.request.dueDate + "</p>"

                        + "<p><b>Requester Name: </b>" + modelInfo.request.name + "</p>"
    
                        + "<p><b>Email Address: </b>" + modelInfo.request.email + "</p>"

                        + "<p><b>Approved By: </b>" + modelInfo.request.doaName + "</p>"

                        + "<p><b>Project Name: </b>" + modelInfo.request.projectName + "</p>"

                        + "<p><b>Contact Info: </b>" + modelInfo.request.contactInfo + "</p>"
    
                        + "<p><b>Location: </b>" + modelInfo.request.location + "</p>"
    
                        + "<p><b>Category:</b>" + modelInfo.request.categoryID + "</p>";

                        int poCount = 0;   

                        foreach(var po in modelInfo.poNum)
                        {
                            poCount++;
 
                            emailBody += "<p><b>PO# @poCount:</b> @po.PONum1 </p>";
                        }


            emailBody += "<p><b>Cost: </b>" + modelInfo.request.cost.ToString() + "</p>"
    
                        + "<p><b>Details: </b>" + modelInfo.request.detail + "</p>"
    
                        + "<p><b>Comments: </b>" + modelInfo.request.comment + "</p>";   

                        foreach (var request in modelInfo.answerList)
                        {
                           emailBody += "<p><b>" + request.question + "</b>" + request.answer.AnswerText + "</p>";   
                        }

          emailBody += "<p><b>Additional Info</b>" + modelInfo.request.additionalInfo + "</p>"   

                        + "<h3>CC Task File</h3>"

                        + "<div class='datagrid'>"
                        + "<table>"
                        + "<thead><tr><th>Order</th><th>Task</th><th>Status</th><th>Due Date</th></tr></thead>"
                        + "<tbody>";


                        foreach (var task in modelInfo.taskList)
                        {

                            emailBody += "<tr>"
                                 + "<td>" + task.orderNum + "</td>"
                                + "<td>" + task.Task + "</td>"
                                + "<td>" + task.Status + "</td>"
                                + "<td>" + task.DueDate + "</td>"
                            + "</tr>";
                        }

                        emailBody += "</tbody>"

                                       + "</table>"

                                       + "</div>"

                                       + "</table>"
                                       + "</div>";


            mail.Body = emailBody;
            mail.Subject = "CC Case File Update: " + modelInfo.request.projectName;

            mailer.Send(mail);


        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult DeplicateRequest()
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            int originalRequestID = Convert.ToInt16(Request.QueryString["requestID"]);
            int requestID;

            Request project = request.Requests.FirstOrDefault(r => r.reuqestID == originalRequestID);

            var answers = request.Answers.Where(r => r.requestID == originalRequestID);

            var attachments = request.Attachments.Where(r => r.requestID == originalRequestID);

            var requestAssignments = request.RequestAssignments.Where(r => r.requestID == originalRequestID);

            var poNums = request.PONums.Where(r => r.requestID == originalRequestID);

            var taskList = request.TaskLists.Where(r => r.requestID == originalRequestID);

            Request newProject = new Request();

            newProject.categoryID = project.categoryID;
            newProject.name = project.name;
            newProject.location = project.location;
            newProject.contactInfo = project.contactInfo;
            newProject.dateRequested = DateTime.Now;
            newProject.completed = project.completed;
            newProject.doaName = project.doaName;
            newProject.doaAttachment = project.doaAttachment;
            newProject.projectName = project.projectName + " - Copy";
            newProject.email = project.email;
            newProject.templateID = project.templateID;
            newProject.additionalInfo = project.additionalInfo;
            newProject.comment = project.comment;
            newProject.poNumber = project.poNumber;
            newProject.detail = project.detail;
            newProject.cost = project.cost;
            newProject.dueDate = project.dueDate;
            newProject.customID = project.customID;
            newProject.status = project.status;
            newProject.subProjects = project.subProjects;

            request.AddToRequests(newProject);
            request.SaveChanges();

            requestID = newProject.reuqestID;

            //Assign New Custom ID
            try
            {
                //newProject.customID = project.customID.Replace(originalRequestID.ToString(), requestID.ToString());
                //request.SaveChanges();
            }
            catch { } 

            foreach (var answer in answers)
            {
                Answer newAnswer = new Answer();

                newAnswer.QuestionID = answer.QuestionID;
                newAnswer.AnswerText = answer.AnswerText;
                newAnswer.AnswerValue = answer.AnswerValue;
                newAnswer.requestID = requestID;

                request.AddToAnswers(newAnswer);
            }

            foreach (var attachment in attachments)
            {
                ProjectRequest.Models.Attachment newAttachment = new ProjectRequest.Models.Attachment();

                string fileDirectory = Server.MapPath("~/Attachments/");

                string newFilePath;

                if (attachment.filePath.StartsWith(originalRequestID.ToString()))
                {
                    newFilePath = attachment.filePath.Replace(originalRequestID.ToString(), requestID.ToString());
                }
                else
                {
                    newFilePath = requestID.ToString() + "/" + attachment.filePath;
                }

                CreateDirectory(requestID);

                System.IO.File.Copy(fileDirectory + attachment.filePath, fileDirectory + newFilePath.Replace("/", @"\"));

                newAttachment.requestID = requestID;
                newAttachment.filePath = newFilePath;
                newAttachment.caption = attachment.caption;
                newAttachment.staffID = attachment.staffID;
                newAttachment.dateAdded = attachment.dateAdded;

                request.AddToAttachments(newAttachment);

            }

            foreach (var requestAssignment in requestAssignments)
            {
                RequestAssignment newAssignment = new RequestAssignment();
                string emailBody;

                newAssignment.requestID = requestID;
                newAssignment.staffID = requestAssignment.staffID;

                request.AddToRequestAssignments(newAssignment);

                emailBody = "<p>Hello CC Agent,</p>"
                    + "<p>You have been added to the following case: <b>" + newProject.projectName + "</b></p>"
                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID="
                    + requestID.ToString() + "'>Click Here</a> to edit the case</p>"
                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewRequest?requestID="
                    + requestID.ToString() + "'>Click Here</a> to view the case</p>"
                    + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/MyProjects'>Click here to view all your assignments</p>";

                SendEmail(requestAssignment.staffID + "@Sullivan.edu", emailBody, newProject.projectName);

            }

            foreach (var poNum in poNums)
            {
                PONum newPO = new PONum();

                newPO.requestID = requestID;
                newPO.PONum1 = poNum.PONum1;

                request.AddToPONums(newPO);

            }

            foreach (var task in taskList)
            {
                TaskList newTask = new TaskList();

                int taskID = task.taskID;
                int newTaskID = task.taskID;

                var taskAssignments = request.TaskAssignments.Where(t => t.taskID == taskID);

                newTask.requestID = requestID;
                newTask.Task = task.Task;
                newTask.DueDate = task.DueDate;
                newTask.Status = task.Status;
                newTask.TemplateID = task.TemplateID;
                newTask.orderNum = task.orderNum;
                newTask.priority = task.priority;

                request.AddToTaskLists(newTask);

                newTaskID = newTask.taskID;

                var assignments = request.TaskAssignments.Where(t => t.taskID == task.taskID).ToList();

                foreach (var assignment in assignments)
                {
                    TaskAssignment newAssignment = new TaskAssignment();

                    newAssignment.staffID = assignment.staffID;
                    newAssignment.taskID = newTaskID;

                    request.AddToTaskAssignments(newAssignment);
                }


                //request.SaveChanges();

                //foreach (var taskAssignment in taskAssignments)
                //{
                //    TaskAssignment newAssignment = new TaskAssignment();

                //    newAssignment.staffID = taskAssignment.staffID;
                //    newAssignment.taskID = taskAssignment.taskID;
                //    newAssignment.templateID = task.TemplateID;


                //}

            }

            request.SaveChanges();

            return RedirectToAction("RequestInfo", new { requestID = requestID });
        }

        [HttpPost]
        public ActionResult CloseRequest()
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            int requestID = Convert.ToInt16(Request.QueryString["requestID"]);

            Request editRequest = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            var tasks = request.TaskLists.Where(r => r.requestID == requestID);

            foreach (var task in tasks)
            {
                task.Status = "Completed";
            }

            CloseRequest(requestID);

            request.SaveChanges();

            return RedirectToAction("ViewRequest", new { requestID = requestID });
        }

        [HttpGet]
        public string GetTrelloInfo(string id)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            int requestID = Convert.ToInt32(id);

            var requestInfo = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            string description = requestInfo.detail + " " + requestInfo.comment + " " + requestInfo.additionalInfo;

            IEnumerable<Models.Attachment> attachments = request.Attachments.Where(a => a.requestID == requestID);

            List<string> myAttachments = new List<string>();

            foreach(var file in attachments)
            {
                myAttachments.Add(file.filePath.Replace(" ", "%20"));
            }

            TrelloData data = new TrelloData
            {
                requestID = Convert.ToInt32(id),
                dueDate = requestInfo.dueDate.ToString(),
                description = description,
                cardTitle = requestInfo.projectName,
                attachment = myAttachments
            };

            return new JavaScriptSerializer().Serialize(data);
        }

    }
}
