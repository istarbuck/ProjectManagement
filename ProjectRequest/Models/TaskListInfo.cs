using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectRequest.Models;

namespace ProjectRequest.Models
{
    public class TaskListInfo
    {
        public IEnumerable<TaskView> taskView { get; set; }
        public IEnumerable<Category> cateogires { get; set; }
        public IEnumerable<Location> location { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Staff> staffList { get; set; }
        public IEnumerable<TaskStatus> status { get; set; }
    }
}