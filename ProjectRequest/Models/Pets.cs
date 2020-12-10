using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRequest.Models
{
    public class Pets
    {
        public string staffID { get; set; }
        public IEnumerable<Attachment> attachments { get; set; }
    }
}