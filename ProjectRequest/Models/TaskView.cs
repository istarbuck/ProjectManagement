using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectRequest.Models;

namespace ProjectRequest.Models
{
    public class TaskView
    {
        public TaskList taskList { get; set; }
        public List<string> staffList { get; set; }
        public Request request { get; set; }

    }
}