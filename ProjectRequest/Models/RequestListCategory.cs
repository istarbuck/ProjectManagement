using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRequest.Models
{
    public class RequestListCategory
    {
        public IEnumerable<RequestListViewModel> requestList { get; set; }
        public IEnumerable<Category> cateogires { get; set; }
        public IEnumerable<Location> location { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Attachment> attachments { get; set; }
        public IEnumerable<Staff> staffList { get; set; }
        public IEnumerable<ProjectStatus> projectStatus { get; set; }

    }
}