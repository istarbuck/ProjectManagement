using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRequest.Models
{
    public class TrelloData
    {
        public int requestID { get; set; }

        public string cardTitle { get; set; }

        public string dueDate { get; set; }

        public string description { get; set; }

        public IEnumerable<string> attachment { get; set; }
    }
}