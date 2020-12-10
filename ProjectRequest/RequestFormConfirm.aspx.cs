using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectRequest.Models;

namespace ProjectRequest
{
    public partial class RequestFormConfirm : System.Web.UI.Page
    {
        public string approvalMessage = "Your project is currently appending approval.";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Approver"] != null)
            {
                approvalMessage = "Your project is currently pending approval from " + (string)Session["Approver"] + ".";
            }
        }
    }
}