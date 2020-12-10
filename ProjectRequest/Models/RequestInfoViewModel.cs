using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectRequest.Models;

namespace ProjectRequest.Models
{
    public class RequestInfoViewModel
    {
        public IEnumerable<AnswerListViewModel> answerList { get; set; } 
        public IEnumerable<SubRequestInfo> subRequests { get; set; }
        public IEnumerable<Attachment> attachment { get; set; }
        public Request request { get; set; } 
        public IEnumerable<RequestAssignment> assignments { get; set; }
        public IEnumerable<Staff> staff { get; set; }
        public Category category { get; set; }
        public IEnumerable<Category> cateogires { get; set; }
        public IEnumerable<Location> location { get; set; }
        public IEnumerable<ProjectStatus> projectStatus { get; set; }

        public IEnumerable<TaskList> taskList { get; set; }
        public IEnumerable<TaskStatus> taskStatus { get; set; }
        public IEnumerable<Template> templateList { get; set; }

        public IEnumerable<TaskAssignment> taskAssignments { get; set; }
        public IEnumerable<PONum> poNum { get; set; }

        public Vendor vendor { get; set; }

        public IEnumerable<Vendor> vendorList { get; set; }

        public IEnumerable<vendorContact> vendorContacts { get; set; }

    }


}