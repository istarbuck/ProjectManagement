using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectRequest.Models;                

namespace ProjectRequest.Models
{
    public class TimeSheetInfo
    {
        public IEnumerable<Request> requests { get; set; }
        public IEnumerable<Chore> chores { get; set; }
        public IEnumerable<Location> locations { get; set; }
        public TimeSheet timeSheet { get; set; }
        public string assignedProjects { get; set; }
        public string completedProjects { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}