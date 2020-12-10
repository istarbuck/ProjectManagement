using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRequest.Models
{
    public class SubRequestInfo
    {
        public SubRequest subRequest { get; set; }

        public string categoryName { get; set; }

        public IEnumerable<AnswerListViewModel> answerList { get; set; }
        public IEnumerable<TaskList> taskList { get; set; }
        public IEnumerable<TaskAssignment> taskAssignments { get; set; }
    }
}