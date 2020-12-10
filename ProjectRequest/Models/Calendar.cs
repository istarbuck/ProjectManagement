using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRequest.Models
{
    public class Calendar
    {
        public int requestID { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string category {get; set; }
        public IEnumerable<AnswerListViewModel> answerList { get; set; }
        public string assignedTo { get; set; }

    }
}