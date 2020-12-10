 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRequest.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public string dateStart;
        public string dateEnd;
        public string location;
        public string category;
        public string completed;
        public string assignedTo;
        public string status;
        public string excludeCategory;
        public string search;
        public string searchPO;
        public string priority;
        public string projectStatus;
        public string excludeStatus;
        public string projectCompleted;
        public string approvers;

        public Dictionary<string, string> completionStatus = new Dictionary<string, string>();

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        public Dictionary<string, string> PopulateStatus()
        {
            completionStatus.Add("false", "Uncompleted Request");
            completionStatus.Add("true", "Completed Requests");
            completionStatus.Add("all", "All Requests");

            return completionStatus;
        }
    }
}