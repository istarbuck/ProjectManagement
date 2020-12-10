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
    public class TemplateController : Controller
    {
        //
        // GET: /Template/

        ProjectRequestEntities request = new ProjectRequestEntities();

        public ActionResult Index()
        {
            var templates = request.Templates;

            return View(templates);
        }

        public ActionResult DisplayTemplate(int template)
        {
            IEnumerable<Staff> staff = request.Staffs;
            IEnumerable<TaskList> taskList = request.TaskLists.Where(t => t.TemplateID == template).OrderBy(t => t.orderNum);
            List<TaskAssignment> taskAssignment = new List<TaskAssignment>();
            Template myTemplate = request.Templates.FirstOrDefault(t => t.templateID == template);
            int templateID = template;

            foreach (var task in taskList)
            {
                var currentTasks = request.TaskAssignments.Where(t => t.taskID == task.taskID);

                foreach (var item in currentTasks)
                {
                    taskAssignment.Add(item);
                }

            }

            Models.TemplateInfo templateInfo = new Models.TemplateInfo()
            {
                taskList = taskList,
                taskStatus = request.TaskStatus1,
                taskAssignments = taskAssignment,
                template = myTemplate,
                staff = staff
            };

            return View(templateInfo);
        }

        public ActionResult EditTemplate()
        {
            int templateID = 0;
            
            if(Request.Form["templateID"].ToString().Length > 0)
               templateID = Convert.ToInt16(Request.Form["templateID"]);

            var tasks = request.TaskLists.Where(r => r.TemplateID == templateID);

            

            if (templateID != 0)
            {
                var template = request.Templates.FirstOrDefault(t => t.templateID == templateID);

                template.templateName = Request.Form["templateName"].ToString();

                foreach (var task in tasks)
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
                        catch
                        {
                            //Response.Redirect(task.taskID.ToString());

                        }
                        currentTask.Task = Request.Form["Task_" + task.taskID].ToString();

                        currentTask.Comments = Request.Form["Comments_" + task.taskID].ToString();

                        try
                        {
                            currentTask.DueDate = Convert.ToDateTime(Request.Form["DueDate_" + task.taskID]);
                        }
                        catch { }
                        if(Request.Form["Status_" + task.taskID].ToString() != "00")
                            currentTask.Status = Request.Form["Status_" + task.taskID].ToString();

                        AssignTemplate(task, templateID, Request.Form["Assignment_" + task.taskID].ToString().Replace(" ", ""));
                    }
                    else if (delete == true)
                    {
                        var taskAsignment = request.TaskAssignments.Where(t => t.templateID == templateID && t.taskID == task.taskID);
                        foreach (var assignment in taskAsignment)
                        {
                            request.TaskAssignments.DeleteObject(assignment);
                        }

                        request.TaskLists.DeleteObject(currentTask);
                    }
                }// end task foreach

                request.SaveChanges();
            }
            else
            {
                Template newTemplate = new Template();
                newTemplate.templateID = templateID;
                newTemplate.templateName = Request.Form["templateName"].ToString();

                request.AddToTemplates(newTemplate);
                request.SaveChanges();

                templateID = newTemplate.templateID;
            }

            if (Request.Form["Task"] != null && Request.Form["Task"].ToString().Length > 0 && templateID != 0)
            {
                TaskList newTask = new TaskList();

                newTask.TemplateID = templateID;
                try
                {
                    newTask.orderNum = Convert.ToInt16(Request.Form["Order"]);
                }
                catch { }
                newTask.Task = Request.Form["Task"].ToString();

                newTask.Comments = Request.Form["Comments"].ToString();

                try
                {
                    newTask.DueDate = Convert.ToDateTime(Request.Form["DueDate"]);
                }
                catch { }
                if (Request.Form["Status"].ToString() != "00")
                    newTask.Status = Request.Form["Status"].ToString();

                request.AddToTaskLists(newTask);
                request.SaveChanges();

                AssignTemplate(newTask, templateID, Request.Form["Assignment"].ToString().Replace(" ", ""));
            }

            return RedirectToAction("DisplayTemplate", new { template = templateID });

        }

        private void AssignTemplate(TaskList task, int templateID, string assignmentList)
        {
            var staffList = request.Staffs;
            string assign = assignmentList;
            assign = assign.Replace("  ", "");
            string[] assignments = assign.Split(',');
            Dictionary<string, bool> staffAssignments = new Dictionary<string, bool>();

            foreach (var staff in staffList)
            {
                if (assignments.Contains(staff.staffID.ToString()))
                    staffAssignments.Add(staff.staffID.ToString(), true);
                else
                    staffAssignments.Add(staff.staffID.ToString(), false);
            }

            using (ProjectRequestEntities project = new ProjectRequestEntities())
            {
                foreach (var assignment in staffAssignments)
                {
                    var editTaskAssignment = project.TaskAssignments.FirstOrDefault(a => a.taskID == task.taskID && a.staffID == assignment.Key);

                    if (assignment.Value == true)
                    {
                        if (editTaskAssignment == null)
                        {
                            TaskAssignment myTask = new TaskAssignment();

                            myTask.templateID = templateID;
                            myTask.taskID = task.taskID;
                            myTask.staffID = assignment.Key;
                            project.AddToTaskAssignments(myTask);

                            project.SaveChanges();
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
                }//end assignment foreach
            }//end using statement
        }

    }
}
