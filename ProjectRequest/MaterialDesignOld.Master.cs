using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectRequest
{
    public partial class MaterialDesignOld : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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