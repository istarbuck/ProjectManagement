using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRequest.Models
{
    public class TemplateInfo
    {
        public IEnumerable<TaskList> taskList { get; set; }
        public IEnumerable<TaskStatus> taskStatus { get; set; }
        public Template template { get; set; }

        public IEnumerable<TaskAssignment> taskAssignments { get; set; }
        public IEnumerable<Staff> staff { get; set; }
    }
}