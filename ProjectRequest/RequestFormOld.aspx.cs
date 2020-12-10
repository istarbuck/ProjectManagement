using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectRequest.Models;

namespace ProjectRequest
{
    public partial class RequestFormOld : System.Web.UI.Page
    {
        string filePath = @"C:\websites\secure.sullivan.edu\ProjectRequest\ProjectRequest\Attachments\";
        string name;
        List<string> deleteAttachments = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Control> dynamicControls = new List<Control>();

            if (Session["dynamicControls"] != null)
            {
                dynamicControls = (List<Control>)Session["dynamicControls"];

                foreach (Control control in dynamicControls)
                {
                    pnlStoryPic.Controls.Add(control);
                }
            }

            if (!Page.IsPostBack)
            {
                ProjectRequestEntities request = new ProjectRequestEntities();

                name = Convert.ToString(User.Identity.Name);

                name = name.Remove(0, 8).ToLower();

                ViewState["name"] = name;

                lblHello.Text = "Hello " + name;

                var staffList = request.Staffs.Select(s => s.staffID).ToList();

                if (staffList.Contains(name))
                {
                    var ccAgent = request.Staffs.FirstOrDefault(s => s.staffID == name);

                    ddlHelp.Items.Insert(1, new ListItem("I need something creative", "creative"));

                    tbFName.Text = ccAgent.firstName;
                    tbLName.Text = ccAgent.lastName;
                    tbEmail.Text = name + "@Sullivan.edu";
                    ddlLocation.SelectedIndex = 2;
                    ViewState["CCAgent"] = true;

                    if (name == "ccole")
                        tbContact.Text = "Messenger Hedgehogs";
                    else
                        tbContact.Text = "Phone / Email / In Person";

                    //pnlBlackOut.Visible = true;
                }
                //else
                //{
                //    mainPanel.Visible = false;
                //}

                btnSubmit.Visible = false;

                //pnlPO1.Visible = false;
                //pnlPO2.Visible = false;
                //pnlPO3.Visible = false;
                //pnlPO4.Visible = false;
                //pnlPO5.Visible = false;
                //pnlPO6.Visible = false;
                //pnlPO7.Visible = false;
                //pnlPO8.Visible = false;

                pnlCool.Visible = false;
                pnlCVGroupNo.Visible = false;
                pnlCVGroupNo2.Visible = false;
                pnlCVGroupNo3.Visible = false;
                pnlCVGroupYes.Visible = false;
                pnlCVGroupYes2.Visible = false;
                pnlCVGroupYes3.Visible = false;
                pnlEBlast.Visible = false;
                pnlEvent.Visible = false;
                pnlFilmCrew.Visible = false;
                pnlNotSure.Visible = false;
                pnlPhotographer.Visible = false;
                pnlPostcard.Visible = false;
                pnlStoryPic.Visible = false;
                pnlWebChange.Visible = false;
                pnlStudentServices.Visible = false;
                pnlConference.Visible = false;
                pnlEventItems.Visible = false;
                pnlDOA.Visible = false;
                pnlAttachments.Visible = false;
                pnlOther.Visible = false;
                pnlCreative.Visible = false;

                Session.Abandon();
            }
        }
        protected void ddlHelp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)ViewState["name"];

            pnlWebChange.Visible = false;
            pnlEBlast.Visible = false;
            pnlEvent.Visible = false;
            pnlPostcard.Visible = false;
            pnlCool.Visible = false;
            pnlFilmCrew.Visible = false;
            pnlPhotographer.Visible = false;
            pnlNotSure.Visible = false;
            pnlStudentServices.Visible = false;
            pnlConference.Visible = false;
            pnlOther.Visible = false;
            btnSubmit.Visible = false;
            pnlCreative.Visible = false;

            ProjectRequestEntities request = new ProjectRequestEntities();

            var DOAs = request.DOAs.Select(d => d.doaID).ToList();

            var staffList = request.Staffs.Select(s => s.staffID).ToList();

            if (DOAs.Contains(name))
            {
                var doaName = request.DOAs.FirstOrDefault(d => d.doaID == name);
                ViewState["DOA"] = doaName.doaName;
                pnlDOA.Visible = false;

                DisplayPanels();
            }
            else if (staffList.Contains(name))
            {
                var staff = request.Staffs.FirstOrDefault(s => s.staffID == name);
                ViewState["Staff"] = staff.firstName + " " + staff.lastName;
                pnlDOA.Visible = false;

                DisplayPanels();
            }
            else if (ddlHelp.SelectedValue == "website" || ddlHelp.SelectedValue == "creative")
            {
                pnlDOA.Visible = false;

                DisplayPanels();
            }
            else
            {
                pnlDOA.Visible = true;
            }

            request.Dispose();
        }

        private void DisplayPanels()
        {
            pnlAttachments.Visible = true;
            btnSubmit.Visible = true;

            if (ddlHelp.SelectedValue == "website")
            {
                pnlWebChange.Visible = true;
                pnlAttachments.Visible = false;
            }
            else if (ddlHelp.SelectedValue == "eBlast")
                pnlEBlast.Visible = true;
            else if (ddlHelp.SelectedValue == "event")
                pnlEvent.Visible = true;
            else if (ddlHelp.SelectedValue == "postcard")
                pnlPostcard.Visible = true;
            else if (ddlHelp.SelectedValue == "cool")
                pnlCool.Visible = true;
            else if (ddlHelp.SelectedValue == "crew")
                pnlFilmCrew.Visible = true;
            else if (ddlHelp.SelectedValue == "photographer")
                pnlPhotographer.Visible = true;
            else if (ddlHelp.SelectedValue == "notSure")
                pnlNotSure.Visible = true;
            else if (ddlHelp.SelectedValue == "conference")
                pnlConference.Visible = true;
            else if (ddlHelp.SelectedValue == "studentServices")
                pnlStudentServices.Visible = true;
            else if (ddlHelp.SelectedValue == "other")
                pnlOther.Visible = true;
            else if (ddlHelp.SelectedValue == "creative")
                pnlCreative.Visible = true;
        }

        protected void ddlCVGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCVGroupNo.Visible = false;
            pnlCVGroupYes.Visible = false;

            if (ddlCVGroup.SelectedItem.Text == "Yes")
                pnlCVGroupYes.Visible = true;
            else
                pnlCVGroupNo.Visible = true;
        }

        protected void ddlCVGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCVGroupNo2.Visible = false;
            pnlCVGroupYes2.Visible = false;

            if (ddlCVGroup2.SelectedItem.Text == "Yes")
                pnlCVGroupYes2.Visible = true;
            else
                pnlCVGroupNo2.Visible = true;
        }

        protected void ddlCVGroup3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCVGroupNo3.Visible = false;
            pnlCVGroupYes3.Visible = false;

            if (ddlCVGroup3.SelectedItem.Text == "Yes")
                pnlCVGroupYes3.Visible = true;
            else
                pnlCVGroupNo3.Visible = true;
        }

        protected void btnCvUpload_Click(object sender, EventArgs e)
        {
            if (cvUpload.PostedFile.FileName.Length > 1)
            {
                string fileName = System.IO.Path.GetFileName(cvUpload.PostedFile.FileName);

                //Save the Upload file and add it to the attachtments list, which then gets saved to a ViewState 
                //string filePath = @"C:\websites\secure.sullivan.edu\forms\temp\" + fileName;

                cvUpload.SaveAs(filePath + fileName);

                ViewState["CVAttatchment"] = fileName;

                //Populate a bullit item list so the user knows what files have been uploaded
                lblCVUpload.Text = fileName;
            }
        }

        protected void btnCvUpload2_Click(object sender, EventArgs e)
        {
            if (cvUpload2.PostedFile.FileName.Length > 1)
            {
                string fileName = System.IO.Path.GetFileName(cvUpload2.PostedFile.FileName);

                //Save the Upload file and add it to the attachtments list, which then gets saved to a ViewState 
                //string filePath = @"C:\websites\secure.sullivan.edu\forms\temp\" + fileName;

                cvUpload2.SaveAs(filePath + fileName);

                ViewState["CVAttatchment"] = fileName;

                //Populate a bullit item list so the user knows what files have been uploaded
                lblCVUpload2.Text = fileName;
            }
        }

        protected void btnCvUpload3_Click(object sender, EventArgs e)
        {
            if (cvUpload3.PostedFile.FileName.Length > 1)
            {
                string fileName = System.IO.Path.GetFileName(cvUpload3.PostedFile.FileName);

                //Save the Upload file and add it to the attachtments list, which then gets saved to a ViewState 
                //string filePath = @"C:\websites\secure.sullivan.edu\forms\temp\" + fileName;

                cvUpload3.SaveAs(filePath + fileName);

                ViewState["CVAttatchment"] = fileName;

                //Populate a bullit item list so the user knows what files have been uploaded
                lblCVUpload3.Text = fileName;
            }
        }

        protected void btnWebUpload_Click(object sender, EventArgs e)
        {
            List<string> webAttatchtment = new List<string>();

            if (webUpload.PostedFile.FileName.Length > 1)
            {
                string fileName = System.IO.Path.GetFileName(webUpload.PostedFile.FileName);

                //Save the Upload file and add it to the attachtments list, which then gets saved to a ViewState 
                //string filePath = @"C:\websites\secure.sullivan.edu\forms\temp\" + fileName;

                webUpload.SaveAs(filePath + fileName);

                if (ViewState["WebAttatchment"] != null)
                {
                    //If not null then set the records dictionary eual to the ViewState
                    webAttatchtment = (List<string>)ViewState["WebAttatchment"];
                }

                webAttatchtment.Add(fileName);

                ViewState["WebAttatchment"] = webAttatchtment;

                webUploadList.Items.Add(fileName);

                noWebUpload.Visible = false;
            }
        }

        protected void btndoaUpload_Click(object sender, EventArgs e)
        {
            if (doaUpload.PostedFile.FileName.Length > 1)
            {
                string fileName = System.IO.Path.GetFileName(doaUpload.PostedFile.FileName);

                //Save the Upload file and add it to the attachtments list, which then gets saved to a ViewState 
                //string filePath = @"C:\websites\secure.sullivan.edu\forms\temp\" + fileName;

                doaUpload.SaveAs(filePath + fileName);

                ViewState["DOAAttachment"] = fileName;

                //Populate a bullit item list so the user knows what files have been uploaded
                lbldoaUpload.Text = fileName;

                VerifyDOA(this, e);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            List<string> Attatchtment = new List<string>();

            if (Upload.PostedFile.FileName.Length > 1)
            {
                string fileName = System.IO.Path.GetFileName(Upload.PostedFile.FileName);

                //Save the Upload file and add it to the attachtments list, which then gets saved to a ViewState 
                //string filePath = @"C:\websites\secure.sullivan.edu\forms\temp\" + fileName;

                Upload.SaveAs(filePath + fileName);

                if (ViewState["Attatchment"] != null)
                {
                    //If not null then set the records dictionary eual to the ViewState
                    Attatchtment = (List<string>)ViewState["Attatchment"];
                }

                Attatchtment.Add(fileName);

                ViewState["Attatchment"] = Attatchtment;

                UploadList.Items.Add(fileName);

                noUpload2.Visible = false;
            }
        }

        protected void btnPicUpload_Click(object sender, EventArgs e)
        {
            List<string> picAttatchtment = new List<string>();
            Label lblPic = new Label();
            TextBox tbPic = new TextBox();
            Literal ltlPic = new Literal();
            List<Control> dynamicControls = new List<Control>();
            int picNum = 0;

            ltlPic.Text = "<br />";

            if (picUpload.PostedFile.FileName.Length > 1)
            {
                string fileName = System.IO.Path.GetFileName(picUpload.PostedFile.FileName);

                //Save the Upload file and add it to the attachtments list, which then gets saved to a ViewState 
                //string filePath = @"C:\websites\secure.sullivan.edu\forms\temp\" + fileName;

                picUpload.SaveAs(filePath + fileName);

                if (ViewState["Attatchment"] != null)
                {
                    //If not null then set the records dictionary eual to the ViewState
                    picAttatchtment = (List<string>)ViewState["Attatchment"];
                }

                picAttatchtment.Add(fileName);

                ViewState["Attatchment"] = picAttatchtment;

                //if (ViewState["picNum"] != null)
                //{
                //    picNum = (int)ViewState["picNum"];
                //    picNum += 1;
                //}
                //else
                //{
                //    picNum = 1;
                //}

                //lblPic.ID = "lblPicCaption" + picNum;
                //lblPic.Text = fileName;
                //lblPic.AssociatedControlID = "tbPicCaption" + picNum;


                //tbPic.ID = "tbPicCaption" + picNum;

                //if (Session["dynamicControls"] != null)
                //    dynamicControls = (List<Control>)Session["dynamicControls"];

                //dynamicControls.Add(lblPic);
                //dynamicControls.Add(tbPic);
                //dynamicControls.Add(ltlPic);

                //ViewState["picNum"] = picNum;
                //Session["dynamicControls"] = dynamicControls;

                //pnlStoryPic.Controls.Add(lblPic);
                //pnlStoryPic.Controls.Add(tbPic);
                //pnlStoryPic.Controls.Add(ltlPic);

                //Populate a bullit item list so the user knows what files have been uploaded
                noUpload.Visible = false;
                picUploadList.Items.Add(fileName);

            }
        }
        protected void ddlStoryPic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStoryPic.SelectedItem.Text == "Yes")
                pnlStoryPic.Visible = true;
            else
                pnlStoryPic.Visible = false;
        }

        public static bool GetBusinessDays(DateTime start, DateTime end, int minBusDays)
        {
            List<DateTime> holidays = new List<DateTime>();
            holidays.Add(new DateTime(2015, 11, 27));
            holidays.Add(new DateTime(2015, 11, 26));
            holidays.Add(new DateTime(2015, 12, 21));
            holidays.Add(new DateTime(2015, 12, 22));
            holidays.Add(new DateTime(2015, 12, 23));
            holidays.Add(new DateTime(2015, 12, 24));
            holidays.Add(new DateTime(2015, 12, 25));
            holidays.Add(new DateTime(2016, 1, 1));
            holidays.Add(new DateTime(2016, 1, 18));
            holidays.Add(new DateTime(2016, 5, 30));
            holidays.Add(new DateTime(2016, 7, 4));
            holidays.Add(new DateTime(2016, 9, 5));
            holidays.Add(new DateTime(2016, 11, 24));
            holidays.Add(new DateTime(2016, 11, 25));

            if (start.DayOfWeek == DayOfWeek.Saturday)
            {
                start = start.AddDays(2);
            }
            else if (start.DayOfWeek == DayOfWeek.Sunday)
            {
                start = start.AddDays(1);
            }

            if (end.DayOfWeek == DayOfWeek.Saturday)
            {
                end = end.AddDays(-1);
            }
            else if (end.DayOfWeek == DayOfWeek.Sunday)
            {
                end = end.AddDays(-2);
            }

            if (end.Date.ToShortDateString() == "11/26/2015" || end.Date.ToShortDateString() == "11/27/2015")
                return false;

            int diff = (int)end.Subtract(start).TotalDays;

            //int result = diff / 7 * 5 + diff % 7;

            //if (end.DayOfWeek < start.DayOfWeek)
            //{
            //    result = result - 2;
            //}

            double weeks = diff / 7;

            double result = diff - weeks * 2;

            if (end.DayOfWeek > start.DayOfWeek)
            {
                result = result + 2;
            }

            //return result;

            foreach (DateTime holiday in holidays)
            {
                if (holiday >= start && holiday <= end)
                    result = result - 1;
            }

            if (result > minBusDays)
                return true;
            else
                return false;
        }
        protected void tbEBlastDate_TextChanged(object sender, EventArgs e)
        {
            //Response.Redirect(Convert.ToString(GetBusinessDays(DateTime.Now, Convert.ToDateTime(tbEBlastDate.Text), 5)));

            if (!GetBusinessDays(DateTime.Now, Convert.ToDateTime(tbEBlastDate.Text), 6))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "alert('A minimum of 5 business days is required.')", true);
                tbEBlastDate.Text = null;
            }
        }
        protected void cbMailer_CheckedChanged(object sender, EventArgs e)
        {
            if (tbEventDate.Text.Length > 0)
            {
                if (!GetBusinessDays(DateTime.Now, Convert.ToDateTime(tbEventDate.Text), 60))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "alert('A minimum of 60 business days is required.')", true);
                    cbMailer.Checked = false;
                    pnlPostcard.Visible = false; ;
                }
                else
                    pnlPostcard.Visible = true;
            }
        }

        protected void cbFlyers_CheckedChanged(object sender, EventArgs e)
        {
            if (tbEventDate.Text.Length > 0)
            {
                if (!GetBusinessDays(DateTime.Now, Convert.ToDateTime(tbEventDate.Text), 10))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "alert('A minimum of 10 business days is required.')", true);
                    cbFlyers.Checked = false;
                }
            }
        }

        protected void cbPosters_CheckedChanged(object sender, EventArgs e)
        {
            if (tbEventDate.Text.Length > 0)
            {
                if (!GetBusinessDays(DateTime.Now, Convert.ToDateTime(tbEventDate.Text), 10))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "alert('A minimum of 10 business days is required.')", true);
                    cbPosters.Checked = false;
                }
            }
        }
        protected void cbSignage_CheckedChanged(object sender, EventArgs e)
        {
            if (tbEventDate.Text.Length > 0)
            {
                if (!GetBusinessDays(DateTime.Now, Convert.ToDateTime(tbEventDate.Text), 10))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "alert('A minimum of 10 business days is required.')", true);
                    cbSignage.Checked = false;
                }

            }
        }
        protected void cbEBlast_CheckedChanged(object sender, EventArgs e)
        {
            if (tbEventDate.Text.Length > 0)
            {
                if (!GetBusinessDays(DateTime.Now, Convert.ToDateTime(tbEventDate.Text), 6))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "alert('A minimum of 5 business days is required.')", true);
                    cbEBlast.Checked = false;
                    pnlEBlast.Visible = false;
                }
                else
                    pnlEBlast.Visible = true;
            }
        }

        protected void tbMailDate_TextChanged(object sender, EventArgs e)
        {
            if (tbMailDate.Text.Length > 0)
            {
                if (!GetBusinessDays(DateTime.Now, Convert.ToDateTime(tbMailDate.Text), 60))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "alert('A minimum of 60 business days is required.')", true);
                    tbMailDate.Text = null;
                }
            }
        }

        protected void tbEventDate_TextChanged(object sender, EventArgs e)
        {
            pnlEventItems.Visible = true;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                List<FieldInfo> formInfo = new List<FieldInfo>();
                FieldInfo fieldInfo = new FieldInfo();
                string emailBody;
                List<string> To = new List<string>();
                List<string> Bcc = new List<string>();
                List<string> Cc = new List<string>();
                List<string> attatchments = new List<string>();
                string location = ddlLocation.SelectedItem.Text;

                Panel selectedPanel;

                if (ddlHelp.SelectedValue == "website")
                    selectedPanel = pnlWebChange;
                else if (ddlHelp.SelectedValue == "eBlast")
                    selectedPanel = pnlEBlast;
                else if (ddlHelp.SelectedValue == "event")
                    selectedPanel = pnlEvent;
                else if (ddlHelp.SelectedValue == "postcard")
                    selectedPanel = pnlPostcard;
                else if (ddlHelp.SelectedValue == "cool")
                    selectedPanel = pnlCool;
                else if (ddlHelp.SelectedValue == "crew")
                    selectedPanel = pnlFilmCrew;
                else if (ddlHelp.SelectedValue == "photographer")
                    selectedPanel = pnlPhotographer;
                else if (ddlHelp.SelectedValue == "notSure")
                    selectedPanel = pnlNotSure;
                else if (ddlHelp.SelectedValue == "conference")
                    selectedPanel = pnlConference;
                else if (ddlHelp.SelectedValue == "studentServices")
                    selectedPanel = pnlStudentServices;
                else if (ddlHelp.SelectedValue == "other")
                    selectedPanel = pnlOther;
                else if (ddlHelp.SelectedValue == "creative")
                    selectedPanel = pnlCreative;
                else
                    selectedPanel = mainPanel;

                //Send out email and insert into database
                try
                {

                    formInfo = fieldInfo.CreateFormInfo(pnlIntro);
                    formInfo = fieldInfo.CreateFormInfo(selectedPanel);
                    formInfo = fieldInfo.CreateFormInfo(pnlAttachments);

                    if (ddlHelp.SelectedValue == "event")
                    {
                        if (cbEBlast.Checked)
                            formInfo = fieldInfo.CreateFormInfo(pnlEBlast);

                        if (cbMailer.Checked)
                            formInfo = fieldInfo.CreateFormInfo(pnlPostcard);
                    }

                    if (ViewState["DOA"] == null && ViewState["Staff"] == null && ddlHelp.SelectedValue != "website")
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlDOA);
                    }

                    //Insert into database
                    DataInsert(selectedPanel, ddlHelp.SelectedValue);

                    if (ddlHelp.SelectedValue == "event")
                    {
                        if (cbEBlast.Checked)
                            DataInsert(pnlEBlast, "eBlast");

                        if (cbMailer.Checked)
                            DataInsert(pnlPostcard, "postcard");
                    }

                    foreach (var file in deleteAttachments)
                    {
                        File.Delete(file);
                    }

                    emailBody = fieldInfo.CreateEmailBody(formInfo);
                    emailBody += "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request'>Project List</a></p>";


                    if (ddlHelp.SelectedValue == "website")
                    {
                        To.Add("hmobley@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "crew")
                    {
                        To.Add("Ahutchings@sullivan.edu");
                        To.Add("Rmccameron@sullivan.edu");
                        To.Add("CShain@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "postcard")
                    {
                        To.Add("mjarboe@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "event")
                    {
                        To.Add("hmobley@sullivan.edu");
                        To.Add("mjarboe@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "cool")
                    {
                        To.Add("cnatale@tmjeff.com");
                        To.Add("nsiegel@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "photographer")
                    {
                        To.Add("mjarboe@sullivan.edu");
                        To.Add("CShain@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "conference")
                    {
                        To.Add("mjarboe@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "studentServices")
                    {
                        To.Add("mjarboe@sullivan.edu");
                        To.Add("CShain@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "other")
                    {
                        To.Add("hmobley@sullivan.edu");
                        To.Add("mjarboe@sullivan.edu");
                    }
                    else if (ddlHelp.SelectedValue == "creative")
                    {
                        To.Add("mjarboe@sullivan.edu");
                    }

                    if (ViewState["allAttachments"] != null)
                    {
                        attatchments = (List<string>)ViewState["allAttachments"];
                    }

                    fieldInfo.SendEmail("Project Request", emailBody, To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);
                }
                catch (Exception ex)
                {
                    To.Clear();
                    Cc.Clear();
                    To.Add("istarbuck@sullivan.edu");

                    fieldInfo.SendEmail("PR EMail Error for " + ddlHelp.SelectedValue, ex.ToString(), To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);
                }

                Session.Abandon();

                //Send confirmation email
                try
                {
                    To.Clear();
                    To.Add(tbEmail.Text);
                    Cc.Clear();
                    Bcc.Clear();
                    attatchments.Clear();

                    fieldInfo.SendEmail("Creative Communication Project Submitted", tbProjectName.Text + " has been submitted to Creative Communications.", To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);
                }
                catch { }

                //Redirect to confirmation or confirmation page
                if (ViewState["CCAgent"] != null)
                {
                    if (ViewState["RequestID"] != null)
                    {
                        Response.Redirect("https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/RequestInfo?requestID=" + (string)ViewState["RequestID"]);
                    }
                    else
                    {
                        Response.Redirect("https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/Index");
                    }
                }
                else
                {
                    Response.Redirect("RequestFormConfirm.aspx");
                }
            }//Ensure Page is Valid
        }

        public void DataInsert(Panel selectedPanel, string category)
        {
            int requestID = 0;
            string emailBody;
            List<string> To = new List<string>();
            List<string> Bcc = new List<string>();
            List<string> Cc = new List<string>();
            FieldInfo fieldInfo = new FieldInfo();
            List<string> attatchments = new List<string>();

            using (ProjectRequestEntities projectRequest = new ProjectRequestEntities())
            {
                Models.Request request = new Models.Request();

                try
                {
                    request.categoryID = category;
                    request.name = tbFName.Text + " " + tbLName.Text;
                    request.location = ddlLocation.SelectedItem.Text;
                    request.contactInfo = tbContact.Text;
                    request.dateRequested = DateTime.Now;
                    request.completed = false;
                    request.status = "Just Submitted";
                    request.subProjects = 1;

                    try
                    {
                        request.projectName = tbProjectName.Text;
                    }
                    catch { }

                    request.email = tbEmail.Text;
                    request.additionalInfo = tbAdditionalInfo.Text;

                    if (ddlHelp.SelectedValue == "creative")
                    {
                        try
                        {
                            request.dueDate = Convert.ToDateTime(tbDueDate.Text);
                        }
                        catch { }
                        //request.poNumber = tbPO.Text;
                        request.detail = tbDetails.Text;
                        request.comment = tbComments.Text;
                        request.vendor = ddlVendor.SelectedValue;
                        try
                        {
                            request.cost = Convert.ToDouble(tbCost);
                        }
                        catch { }
                    }

                    projectRequest.AddToRequests(request);
                    projectRequest.SaveChanges();

                    //if (ddlPO.SelectedValue != "00" && ddlPO.SelectedValue != "0")
                    //{
                    //    int POTotal = Convert.ToInt16(ddlPO.SelectedValue);

                    //    for (int i = 1; i <= POTotal; i++)
                    //    {
                    //        ProjectRequestEntities project = new ProjectRequestEntities();
                    //        PONum po = new PONum();
                    //        Control control = Page.Master.FindControl("MainContent").FindControl("tbPO" + i.ToString());

                    //        po.requestID = request.reuqestID;
                    //        po.PONum1 = ((TextBox)control).Text;

                    //        project.AddToPONums(po);
                    //        project.SaveChanges();
                    //    }
                    //}

                    requestID = request.reuqestID;

                    ViewState["RequestID"] = requestID.ToString();

                    if (ddlHelp.SelectedValue != "creative")
                        InsertData(selectedPanel, requestID);

                    projectRequest.Dispose();

                    LoadAttachments(requestID);
                }
                catch (Exception ex)
                {
                    To.Clear();
                    Cc.Clear();
                    To.Add("istarbuck@sullivan.edu");

                    fieldInfo.SendEmail("PR Database Error for " + ddlHelp.SelectedValue, ex.ToString(), To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);
                }
            }
        }

        public void CreateDirectory(int requestID)
        {
            if (!Directory.Exists(filePath + requestID.ToString()))
            {
                DirectoryInfo di = Directory.CreateDirectory(filePath + requestID.ToString());
            }
        }

        public void LoadAttachments(int requestID)
        {
            ProjectRequestEntities projectRequest = new ProjectRequestEntities();
            List<Control> dynamicControls = new List<Control>();
            List<string> allAttachments = new List<string>();

            List<string> To = new List<string>();
            List<string> Bcc = new List<string>();
            List<string> Cc = new List<string>();
            FieldInfo fieldInfo = new FieldInfo();
            List<string> attatchments = new List<string>();
            


            int listNum;

            if (ViewState["CVAttatchment"] != null)
            {
                string fileName = (string)ViewState["CVAttatchment"];

                CreateDirectory(requestID);

                Attachment attachment = new Attachment();

                attachment.filePath = requestID.ToString() + "/" + fileName;
                attachment.requestID = requestID;

                projectRequest.AddToAttachments(attachment);
                projectRequest.SaveChanges();

                System.IO.File.Copy(filePath + fileName, filePath + requestID.ToString() + @"\" + fileName);

                allAttachments.Add(filePath + requestID.ToString() + @"\" + fileName);

                deleteAttachments.Add(filePath + fileName);
            }

            if (ViewState["WebAttatchment"] != null)
            {
                string fileName;

                CreateDirectory(requestID);

                List<string> webAttachment = new List<string>();
                webAttachment = (List<string>)ViewState["WebAttatchment"];

                foreach (var file in webAttachment)
                {
                    fileName = file;

                    Attachment attachment = new Attachment();

                    attachment.filePath = requestID.ToString() + "/" + fileName;
                    attachment.requestID = requestID;

                    projectRequest.AddToAttachments(attachment);
                    projectRequest.SaveChanges();

                    System.IO.File.Copy(filePath + fileName, filePath + requestID.ToString() + @"\" + fileName);

                    allAttachments.Add(filePath + requestID.ToString() + @"\" + fileName);

                    deleteAttachments.Add(filePath + fileName);
                }
            }

            if (ViewState["Attatchment"] != null)
            {
                string fileName;

                CreateDirectory(requestID);

                List<string> Attachment = new List<string>();
                Attachment = (List<string>)ViewState["Attatchment"];

                foreach (var file in Attachment)
                {
                    fileName = file;

                    Attachment attachment = new Attachment();

                    attachment.filePath = requestID.ToString() + "/" + fileName;
                    attachment.requestID = requestID;

                    projectRequest.AddToAttachments(attachment);
                    projectRequest.SaveChanges();

                    System.IO.File.Copy(filePath + fileName, filePath + requestID.ToString() + @"\" + fileName);

                    allAttachments.Add(filePath + requestID.ToString() + @"\" + fileName);

                    deleteAttachments.Add(filePath + fileName);
                }
            }

            var request = projectRequest.Requests.FirstOrDefault(r => r.reuqestID == requestID);

            if (request != null)
            {
                if (ViewState["DOA"] == null && ViewState["Staff"] == null && (ddlHelp.SelectedValue != "website" && ddlHelp.SelectedValue != "creative"))
                {
                    try
                    {
                        CreateDirectory(requestID);

                        string fileName = (string)ViewState["DOAAttachment"];

                        request.doaName = ddlDOA.SelectedItem.Text;

                        if (!File.Exists(filePath + requestID.ToString() + @"\" + fileName))
                        {
                            request.doaAttachment = requestID.ToString() + "/" + fileName;

                            System.IO.File.Copy(filePath + fileName, filePath + requestID.ToString() + @"\" + fileName);

                            allAttachments.Add(filePath + requestID.ToString() + @"\" + fileName);

                            deleteAttachments.Add(filePath + fileName);
                        }
                    }
                    catch(Exception ex)
                    {
                        To.Clear();
                        Cc.Clear();
                        To.Add("istarbuck@sullivan.edu");

                        fieldInfo.SendEmail("PR Database Error for " + ddlHelp.SelectedValue, ex.ToString(), To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);

                        request.doaName = ddlDOA.SelectedItem.Text;
                        request.doaAttachment = "File not found";
                    }
                }
                else
                {
                    if (ViewState["DOA"] != null)
                        request.doaName = (string)ViewState["DOA"];

                    else if (ViewState["Staff"] != null)
                        request.doaName = (string)ViewState["Staff"];

                    if (ddlHelp.SelectedValue != "website" && ddlHelp.SelectedValue != "creative")
                        request.doaAttachment = "Submitted by DOA or Staff";
                    else
                        request.doaAttachment = "Approval not needed";
                }

                projectRequest.SaveChanges();
            }

            ViewState["allAttachments"] = allAttachments;

            projectRequest.Dispose();


            //if (Session["dynamicControls"] != null)
            //{
            //    CreateDirectory(requestID);

            //    dynamicControls = (List<Control>)Session["dynamicControls"];

            //    listNum = dynamicControls.Count();

            //    for (int i = 0; i < listNum; i++)
            //    {

            //        if (dynamicControls[i] is Label)
            //        {
            //            Attachment attachment = new Attachment();

            //            attachment.requestID = requestID;

            //            attachment.filePath = ((Label)dynamicControls[i]).Text;

            //            attachment.caption = ((TextBox)dynamicControls[i + 1]).Text;

            //            projectRequest.AddToAttachments(attachment);
            //            projectRequest.SaveChanges();
            //        }


            //    }
            //}


        }

        public void InsertData(Panel panel, int requestID)
        {
            if (panel.ID != "pnlStoryPic")
            {
                //the page is warpped in a pnael, cycle through it to get all the labels on the page
                foreach (Control control in panel.Controls)
                {
                    ProjectRequestEntities projectRequest = new ProjectRequestEntities();
                    Answer answer = new Answer();

                    answer.requestID = requestID;

                    //if the current selected control is a panel you need to cycle through this panel to get all the controls in side of it
                    if (control is Panel)
                        InsertData((Panel)control, requestID);

                    else if (control is TextBox || control is DropDownList || control is RadioButtonList || control is CheckBox)
                    {
                        //check to see what type of control is selected so you can get the appropiate text value
                        if (control is TextBox)
                        {
                            answer.AnswerText = ((TextBox)control).Text;
                            answer.QuestionID = control.ID;
                        }

                        else if (control is DropDownList)
                        {
                            if (((DropDownList)control).SelectedIndex > 0)
                            {
                                answer.AnswerText = ((DropDownList)control).SelectedItem.Text;
                                answer.AnswerValue = ((DropDownList)control).SelectedValue;
                            }
                            else
                                answer.AnswerText = "No Answer";

                            answer.QuestionID = control.ID;
                        }

                        else if (control is RadioButtonList)
                        {
                            if (((RadioButtonList)control).SelectedIndex > -1)
                            {
                                answer.AnswerText = ((RadioButtonList)control).SelectedItem.Text;
                                answer.AnswerValue = ((RadioButtonList)control).SelectedValue;
                            }
                            else
                                answer.AnswerText = "No Answer";

                            answer.QuestionID = control.ID;
                        }

                        else if (control is CheckBox)
                        {
                            if (((CheckBox)control).Checked)
                                answer.AnswerText = "Yes";
                            else
                                answer.AnswerText = "No";

                            answer.QuestionID = control.ID;
                        }

                        else
                        {
                            answer.AnswerText = "No Answer";
                            answer.QuestionID = control.ID;
                        }

                        projectRequest.AddToAnswers(answer);
                        projectRequest.SaveChanges();

                        projectRequest.Dispose();
                    }
                }
            }
        }

        public void VerifyDOA(object sender, EventArgs e)
        {
            if (ddlDOA.SelectedValue != "00" && ViewState["DOAAttachment"] != null)
            {
                ddlHelp.Focus();
                DisplayPanels();
            }
            else
            {
                ddlDOA.Focus();
            }
        }

        protected void tbEmail_TextChanged(object sender, EventArgs e)
        {
            tbEmail.Text = tbEmail.Text.Replace(" ", "");
        }

        //protected void ddlPO_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pnlPO1.Visible = false;
        //    pnlPO2.Visible = false;
        //    pnlPO3.Visible = false;
        //    pnlPO4.Visible = false;
        //    pnlPO5.Visible = false;
        //    pnlPO6.Visible = false;
        //    pnlPO7.Visible = false;
        //    pnlPO8.Visible = false;

        //    if (ddlPO.SelectedValue == "1")
        //    {
        //        pnlPO1.Visible = true;
        //    }
        //    else if (ddlPO.SelectedValue == "2")
        //    {
        //        pnlPO1.Visible = true;
        //        pnlPO2.Visible = true;
        //    }
        //    else if (ddlPO.SelectedValue == "3")
        //    {
        //        pnlPO1.Visible = true;
        //        pnlPO2.Visible = true;
        //        pnlPO3.Visible = true;
        //    }
        //    else if (ddlPO.SelectedValue == "4")
        //    {
        //        pnlPO1.Visible = true;
        //        pnlPO2.Visible = true;
        //        pnlPO3.Visible = true;
        //        pnlPO4.Visible = true;
        //    }
        //    else if (ddlPO.SelectedValue == "5")
        //    {
        //        pnlPO1.Visible = true;
        //        pnlPO2.Visible = true;
        //        pnlPO3.Visible = true;
        //        pnlPO4.Visible = true;
        //        pnlPO5.Visible = true;
        //    }
        //    else if (ddlPO.SelectedValue == "6")
        //    {
        //        pnlPO1.Visible = true;
        //        pnlPO2.Visible = true;
        //        pnlPO3.Visible = true;
        //        pnlPO4.Visible = true;
        //        pnlPO5.Visible = true;
        //        pnlPO6.Visible = true;
        //    }
        //    else if (ddlPO.SelectedValue == "7")
        //    {
        //        pnlPO1.Visible = true;
        //        pnlPO2.Visible = true;
        //        pnlPO3.Visible = true;
        //        pnlPO4.Visible = true;
        //        pnlPO5.Visible = true;
        //        pnlPO6.Visible = true;
        //        pnlPO7.Visible = true;
        //    }
        //    else if (ddlPO.SelectedValue == "8")
        //    {
        //        pnlPO1.Visible = true;
        //        pnlPO2.Visible = true;
        //        pnlPO3.Visible = true;
        //        pnlPO4.Visible = true;
        //        pnlPO5.Visible = true;
        //        pnlPO6.Visible = true;
        //        pnlPO7.Visible = true;
        //        pnlPO8.Visible = true;
        //    }
        //}

    }
}