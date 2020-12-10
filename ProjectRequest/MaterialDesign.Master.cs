using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectRequest
{
    public partial class MaterialDesign : System.Web.UI.MasterPage
    {
        public string name;
        public string role;

        protected void Page_Load(object sender, EventArgs e)
        {
            name = Convert.ToString(Page.User.Identity.Name);

            name = name.Remove(0, 8).ToLower();

            ProjectRequest.Models.ProjectRequestEntities requestInfo = new ProjectRequest.Models.ProjectRequestEntities();

            var staff = requestInfo.Staffs.Select(s => s.staffID).ToList();

            if (staff.Contains(name))
            {
                role = "Agent";
                pnlAgentMenu.Visible = true;
                pnlUserMenu.Visible = false;
            }
            else
            {
                role = "User";
                pnlAgentMenu.Visible = false;
                pnlUserMenu.Visible = true;
            }

            if (!Page.IsPostBack)
            {
                System.Web.HttpBrowserCapabilities browser = Request.Browser;



                if (browser.Browser == "InternetExplorer")
                {
                    lblIEWarning.Visible = true;
                }
                else
                {
                    lblIEWarning.Visible = false;
                }
            }
        }
    }
}