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
    public partial class NewCase : System.Web.UI.Page
    {
        string filePath = @"C:\websites\secure.sullivan.edu\ProjectRequest\ProjectRequest\Attachments\";
        string name;
        List<string> deleteAttachments = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ProjectRequestEntities request = new ProjectRequestEntities();

                var staffList = request.Staffs.Select(s => s.staffID).ToList();

                name = Convert.ToString(User.Identity.Name);

                name = name.Remove(0, 8).ToLower();

                ViewState["name"] = name;

                lblHello.Text = "Hello " + name;

                if (staffList.Contains(name))
                {
                    var ccAgent = request.Staffs.FirstOrDefault(s => s.staffID == name);

                    ddlDepartment.Items.Insert(1, new ListItem("Creative", "Creative"));
                    ddlDepartment.Items[1].Selected = true;

                    tbFName.Text = ccAgent.firstName;
                    tbLName.Text = ccAgent.lastName;
                    tbEmail.Text = name + "@Sullivan.edu";
                    ddlLocation.SelectedIndex = 2;
                    ViewState["CCAgent"] = true;
                    //pnlBlackOut.Visible = false;
                    //mainPanel.Visible = true;
                }
                else
                {
                    pnlBillboard.Visible = false;
                    pnlTvCommercial.Visible = false;
                    pnlWebBanner.Visible = false;
                    //pnlBlackOut.Visible = true;
                    //mainPanel.Visible = false;
                }

                if(name == "lajones")
                {
                    pnlBlackOut.Visible = false;
                    mainPanel.Visible = true;
                }

                btnSubmit.Visible = false;

                Session.Abandon();
            }
        }

        protected void ddlHelp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(website.Checked || billBoard.Checked || businessCard.Checked || postcards.Checked || flyer.Checked || landingPage.Checked || filmCrew.Checked || poster.Checked || printAd.Checked || radioSpot.Checked
                || specialEvent.Checked || story.Checked || targetedAd.Checked || tShirt.Checked ||tvCommercial.Checked || animation.Checked || webBanner.Checked || E_Blast.Checked || socialMedia.Checked)
            {
                pnlAttachments.Visible = true;
                btnSubmit.Visible = true;
            }
            else
            {
                pnlAttachments.Visible = false;
                btnSubmit.Visible = false;
            }

            if (animation.Checked)
                pnlAnimation.Visible = true;
            else
                pnlAnimation.Visible = false;

            if (website.Checked)
                pnlWebChange.Visible = true;
            else
                pnlWebChange.Visible = false;

            if (billBoard.Checked)
                pnlBillboard.Visible = true;
            else
                pnlBillboard.Visible = false;

            if (businessCard.Checked)
                pnlBusinessCards.Visible = true;
            else
                pnlBusinessCards.Visible = false;

            if (postcards.Checked)
                pnlPostcards.Visible = true;
            else
                pnlPostcards.Visible = false;

            if (flyer.Checked)
                pnlFlyer.Visible = true;
            else
                pnlFlyer.Visible = false;

            if (landingPage.Checked)
                pnlLandingPage.Visible = true;
            else
                pnlLandingPage.Visible = false;

            if (filmCrew.Checked)
                pnlFilmCrew.Visible = true;
            else
                pnlFilmCrew.Visible = false;

            if (poster.Checked)
                pnlPoster.Visible = true;
            else
                pnlPoster.Visible = false;

            if (printAd.Checked)
                pnlPrintAd.Visible = true;
            else
                pnlPrintAd.Visible = false;

            if (radioSpot.Checked)
                pnlRadioSpot.Visible = true;
            else
                pnlRadioSpot.Visible = false;

            if (specialEvent.Checked)
                pnlSpecialEvent.Visible = true;
            else
                pnlSpecialEvent.Visible = false;

            if (story.Checked)
                pnlStory.Visible = true;
            else
                pnlStory.Visible = false;

            if (targetedAd.Checked)
                pnlTargetedAd.Visible = true;
            else
                pnlTargetedAd.Visible = false;

            if (tShirt.Checked)
                pnlTShirt.Visible = true;
            else
                pnlTShirt.Visible = false;

            if (tvCommercial.Checked)
                pnlTvCommercial.Visible = true;
            else
                pnlTvCommercial.Visible = false;

            if (webBanner.Checked)
                pnlWebBanner.Visible = true;
            else
                pnlWebBanner.Visible = false;

            if (E_Blast.Checked)
                pnlEBlast.Visible = true;
            else
                pnlEBlast.Visible = false;

            if (other.Checked)
                pnlOther.Visible = true;
            else
                pnlOther.Visible = false;

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            List<string> Attatchtment = new List<string>();
            FieldInfo cleaner = new FieldInfo();

            if (Upload.PostedFile.FileName.Length > 1)
            {
                string fileName = System.IO.Path.GetFileName(Upload.PostedFile.FileName);

                //Clear harmfull characters from file name
                fileName = cleaner.CleanAttahcmentName(fileName);

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
                int requestID;

                ProjectRequestEntities newCase = new ProjectRequestEntities();

                var DOAs = newCase.DOAs.Select(d => d.doaID).ToList();

                var staffList = newCase.Staffs.Select(s => s.staffID).ToList();

                var approverList = newCase.Approvers.Select(a => a.sullivanID).ToList();

                //Send out email and insert into database
                //try
                //{

                    requestID = DataInsert();

                    formInfo = fieldInfo.CreateFormInfo(pnlIntro);

                    if (animation.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlAnimation);
                        CreateSubRequest(requestID, "animation", pnlAnimation);
                    }

                    if (website.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlWebChange);
                        CreateSubRequest(requestID, "website", pnlWebChange);
                    }

                    if (billBoard.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlBillboard);
                        CreateSubRequest(requestID, "billBoard", pnlBillboard);
                    }

                    if (businessCard.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlBusinessCards);
                        CreateSubRequest(requestID, "businessCard", pnlBusinessCards);
                    }

                    if (postcards.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlPostcards);
                        CreateSubRequest(requestID, "postcards", pnlPostcards);
                    }

                    if (flyer.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlFlyer);
                        CreateSubRequest(requestID, "flyer", pnlFlyer);
                    }

                    if (landingPage.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlLandingPage);
                        CreateSubRequest(requestID, "landingPage", pnlLandingPage);
                    }

                    if (filmCrew.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlFilmCrew);
                        CreateSubRequest(requestID, "filmCrew", pnlFilmCrew);
                    }

                    if (poster.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlPoster);
                        CreateSubRequest(requestID, "poster", pnlPoster);
                    }

                    if (printAd.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlPrintAd);
                        CreateSubRequest(requestID, "printAd", pnlPrintAd);
                    }

                    if (radioSpot.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlRadioSpot);
                        CreateSubRequest(requestID, "radioSpot", pnlRadioSpot);
                    }

                    if (specialEvent.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlSpecialEvent);
                        CreateSubRequest(requestID, "specialEvent", pnlSpecialEvent);
                    }

                    if (story.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlStory);
                        CreateSubRequest(requestID, "story", pnlStory);
                    }

                    if (targetedAd.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlTargetedAd);
                        CreateSubRequest(requestID, "targetedAd", pnlTargetedAd);
                    }

                    if (tShirt.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlTShirt);
                        CreateSubRequest(requestID, "tShirt", pnlTShirt);
                    }

                    if (tvCommercial.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlTvCommercial);
                        CreateSubRequest(requestID, "tvCommercial", pnlTvCommercial);
                    }

                    if (webBanner.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlWebBanner);
                        CreateSubRequest(requestID, "webBanner", pnlWebBanner);
                    }

                    if (E_Blast.Checked)
                    {
                        formInfo = fieldInfo.CreateFormInfo(pnlEBlast);
                        CreateSubRequest(requestID, "E_Blast", pnlEBlast);
                    }

                    if (socialMedia.Checked)
                    {
                        CreateSubRequest(requestID, "socialMedia", null);
                    }

                    foreach (var file in deleteAttachments)
                    {
                        File.Delete(file);
                    }

                    emailBody = fieldInfo.CreateEmailBody(formInfo);


                    if (ViewState["allAttachments"] != null)
                    {
                        attatchments = (List<string>)ViewState["allAttachments"];
                    }

                    //fieldInfo.SendEmail("Project Request", emailBody, To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);
                //}
                //catch (Exception ex)
                //{
                //    To.Clear();
                //    Cc.Clear();
                //    To.Add("istarbuck@sullivan.edu");

                //    fieldInfo.SendEmail("PR EMail Error", ex.ToString(), To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);
                //}

                Session.Abandon();

                //Send confirmation email
                try
                {
                    To.Clear();
                    To.Add(tbEmail.Text);
                    Cc.Clear();
                    Bcc.Clear();
                    attatchments.Clear();

                    if (staffList.Contains((string)ViewState["name"]))
                    {
                        fieldInfo.SendEmail("Creative Communication Project Submitted",
                        "<p>" + tbProjectName.Text + " has been put in the oven and is now baking.</p><p><img src='https://www.wiseguysbuffalo.com/wp-content/uploads/2016/12/pizzaGuyColor.png'></p>", To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);

                        Response.Redirect("https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/EditCase?requestID=" + (string)ViewState["RequestID"]);
                    }
                    else if(DOAs.Contains((string)ViewState["name"]) || approverList.Contains((string)ViewState["name"]))
                    {
                        fieldInfo.SendEmail("Creative Communication Project Submitted",
                        "<p>" + tbProjectName.Text + " has been submitted.</p>", To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);

                        Response.Redirect("https://secure.sullivan.edu/ProjectRequest/ProjectRequest/User/MyRequests");
                    }
                    else
                    {
                        var approvers = newCase.Approvers.Where(d => d.department == ddlDepartment.SelectedItem.Text);

                        string approverName = "";

                        To.Clear();

                        foreach(var name in approvers)
                        {
                            To.Add(name.email);

                            approverName += name.name + ", ";
                        }

                        approverName = approverName.Trim();
                        approverName = approverName.TrimEnd(',');

                        fieldInfo.SendEmail("Creative Communication Project Approval Needed",
                            "<html><body> <p>Hello " + approverName + "</p>"
                            + "<p><b>" + tbFName.Text + " " + tbLName.Text + "</b> has submitted a project request to Creative Communications. Please review <b>" + tbProjectName.Text 
                            + "</b> and determine if it should be approved or denied. Work on the project will not be started unless it has been approved.</p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewCase?requestID=" + (string)ViewState["RequestID"] + "'>Project Info</a></p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request?projectStatus=Pending&approvers=Yes'>All projects pending approval</a></p></body></html>",
                            To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);

                        To.Clear();
                        To.Add(tbEmail.Text);

                        fieldInfo.SendEmail("Creative Communication Project Submitted",
                            "<html><body><p>Thank You!</p>"
                            + "<p>Your Creative Communications project request, " + tbProjectName.Text + ", has been submitted.</p>"
                            + "<p>Your project is currently pending approval from " + approverName + ".</p>"
                            + "<p>You’ll be notified once a decision has been made.</p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/Request/ViewCase?requestID=" + (string)ViewState["RequestID"] + "'>Project Info</a></p>"
                            + "<p><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/User/MyRequests'>All my projects</a></p>"
                            + "<p>If you have any questions, please email me at <a href='mailTo:snallen@sullivan.edu'></a>snallen@sullivan.edu.</p></body></html>",
                            To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);

                        Session["Approver"] = approverName;

                        Response.Redirect("RequestFormConfirm.aspx");

                    }

                }
                catch { }

            }//Ensure Page is Valid
        }

        public int DataInsert()
        {
            int requestID;
            string emailBody;

            List<string> To = new List<string>();
            List<string> Bcc = new List<string>();
            List<string> Cc = new List<string>();
            FieldInfo fieldInfo = new FieldInfo();
            List<string> attatchments = new List<string>();

            using (ProjectRequestEntities projectRequest = new ProjectRequestEntities())
            {
                ProjectRequestEntities newCase = new ProjectRequestEntities();
                Models.Request request = new Models.Request();

                var DOAs = newCase.DOAs.Select(d => d.doaID).ToList();

                var staffList = newCase.Staffs.Select(s => s.staffID).ToList();

                var approverList = newCase.Approvers.Select(a => a.sullivanID).ToList();

                request.name = tbFName.Text + " " + tbLName.Text;
                request.location = ddlLocation.SelectedItem.Text;
                request.contactInfo = tbContact.Text;
                request.dateRequested = DateTime.Now;
                request.completed = false;
                request.subProjects = 1;
                request.projectName = tbProjectName.Text;
                request.categoryID = "multi";
                request.department = ddlDepartment.SelectedItem.Text;

                request.email = tbEmail.Text;
                request.additionalInfo = tbAdditionalInfo.Text;

                if(DOAs.Contains((string)ViewState["name"]) || staffList.Contains((string)ViewState["name"]) || approverList.Contains((string)ViewState["name"]))
                {
                    request.status = "Approved";
                }
                else
                {
                    request.status = "Pending";

                    var approvers = newCase.Approvers.Where(d => d.department == ddlDepartment.SelectedItem.Text);

                    string approverName = "";

                    foreach (var name in approvers)
                    {
                        approverName += name.name + ", ";
                    }

                    approverName = approverName.Trim();
                    approverName = approverName.TrimEnd(',');

                    request.doaName = approverName;
                }


                projectRequest.AddToRequests(request);
                projectRequest.SaveChanges();

                requestID = request.reuqestID;

                ViewState["RequestID"] = requestID.ToString();

                projectRequest.Dispose();

                LoadAttachments(requestID);

                //}
                //catch (Exception ex)
                //{
                //    To.Clear();
                //    Cc.Clear();
                //    To.Add("istarbuck@sullivan.edu");

                //    fieldInfo.SendEmail("PR Database Error for " + category, ex.ToString(), To, Bcc, Cc, "ProjectRequest@CC.edu", attatchments);
                //}
            }

            return requestID;
        }

        public void CreateSubRequest(int requestID, string category, Panel selectedPanel)
        {
            Models.SubRequest subrequest = new Models.SubRequest();

            int subRequestID;

            using (ProjectRequestEntities projectRequest = new ProjectRequestEntities())
            {

                subrequest.requestID = requestID;
                subrequest.categoryID = category;
                subRequestID = subrequest.subRequestID;

                projectRequest.AddToSubRequests(subrequest);
                projectRequest.SaveChanges();

                subRequestID = subrequest.subRequestID;

                if (category != "socialMedia")
                {
                    InsertData(selectedPanel, subRequestID);
                }
            }
        }

        public void InsertData(Panel panel, int subRequestID)
        {
            if (panel.ID != "pnlStoryPic")
            {
                //the page is warpped in a pnael, cycle through it to get all the labels on the page
                foreach (Control control in panel.Controls)
                {
                    ProjectRequestEntities projectRequest = new ProjectRequestEntities();
                    Answer answer = new Answer();

                    answer.subrequestID = subRequestID;

                    //if the current selected control is a panel you need to cycle through this panel to get all the controls in side of it
                    if (control is Panel)
                        InsertData((Panel)control, subRequestID);

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

            ViewState["allAttachments"] = allAttachments;

            projectRequest.Dispose();
        }

        public void VerifyDOA(object sender, EventArgs e)
        {

        }

        protected void tbEmail_TextChanged(object sender, EventArgs e)
        {
            tbEmail.Text = tbEmail.Text.Replace(" ", "");
        }

        protected void ddlRadioPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRadioPurpose.SelectedItem.Text == "Specific Program")
                pnlRadioProgram.Visible = true;
            else
                pnlRadioProgram.Visible = false;
        }

        protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPurpose.SelectedItem.Text == "Specific Program")
                pnlTVProgram.Visible = true;
            else
                pnlTVProgram.Visible = false;
        }

        protected void ddlTargtedAdAudience_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTargtedAdAudience.SelectedItem.Text == "Other")
                pnlTargtedAdOther.Visible = true;
            else
                pnlTargtedAdOther.Visible = false;
        }

        protected void ddlTargetedAdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlTargetedAdListNoList.Visible = false;
            pnlTargetedAdListYesList.Visible = false;

            if (ddlTargetedAdList.SelectedItem.Text == "Yes")
                pnlTargetedAdListYesList.Visible = true;

            else if (ddlTargetedAdList.SelectedItem.Text == "No")
                pnlTargetedAdListNoList.Visible = true;

        }

        protected void ddlTargtedAdGoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTargtedAdGoal.SelectedItem.Text == "Other")
                pnlTargtedAdGoalOther.Visible = true;
            else
                pnlTargtedAdGoalOther.Visible = false;
        }

        protected void tbEBlastList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tbEBlastList.SelectedItem.Text == "Yes")
            {
                pnlEBlastUpload.Visible = true;
                pnlEBlastList.Visible = false;
            }
            else
            {
                pnlEBlastUpload.Visible = false;
                pnlEBlastList.Visible = true;
            }
        }
    }
}