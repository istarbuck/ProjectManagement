using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectRequest.Models;

namespace ProjectRequest.Models
{
    public class RequestListViewModel
    {
        public Request Request { get; set; }
        public Category Category { get; set; }
    }
}