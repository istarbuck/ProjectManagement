using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRequest.Models
{
    public class RequestInfo
    {
        public RequestInfoViewModel GetRequestInfo(int requestID)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();

            var answers = request.Answers.Where(a => a.requestID == requestID);
            IEnumerable<Models.Attachment> attachments = request.Attachments.Where(a => a.requestID == requestID);
            IEnumerable<Staff> staff = request.Staffs;
            IEnumerable<RequestAssignment> requestAssignment = request.RequestAssignments.Where(r => r.requestID == requestID);
            IEnumerable<TaskList> taskList = request.TaskLists.Where(r => r.requestID == requestID).OrderBy(t => t.orderNum);
            List<TaskAssignment> taskAssignment = new List<TaskAssignment>();
            List<AnswerListViewModel> myAnswerList = new List<AnswerListViewModel>();

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

            var currentRequst = request.Requests.FirstOrDefault(r => r.reuqestID == requestID);
            var poNum = request.PONums.Where(r => r.requestID == requestID);

            RequestInfoViewModel requestList = new RequestInfoViewModel
            {
                answerList = myAnswerList,
                attachment = attachments,
                request = currentRequst,
                staff = staff,
                assignments = requestAssignment,
                category = request.Categories.FirstOrDefault(c => c.categoryID == currentRequst.categoryID),
                cateogires = request.Categories,
                location = request.Locations,
                taskList = taskList,
                taskStatus = request.TaskStatus1,
                templateList = request.Templates,
                taskAssignments = taskAssignment,
                poNum = poNum
            };

            return requestList;
        }

    }
}