using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Web.Script.Serialization;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;

[Serializable]
public class FieldInfo
{
    public string labelID;
    public string labelText;
    public string controlID;
    public string controlText;
    public string CssClass;
    public bool visible;
    public string controlType;
    public string panelID;
    public List<FieldInfo> formInfo = new List<FieldInfo>();

    public string CreateEmailBody(List<FieldInfo> info)
    {
        string emailBody = "<html>";

        //find how many items are in the label list
        int listLength = info.Count;

        //loop through the list of labels and then draw the current selected label, and potentialy its control, on the image
        for (int i = 0; i < listLength; i++)
        {
            FieldInfo currentField = info[i];

            //emailBody += "<p>" + currentField. + " - " + currentField.controlType + "</p>";


            //If the label does not have an associated control then it is a stand alone label
            if ((currentField.controlType == "Label" || currentField.controlType == "Panel") && currentField.controlText == null)
            {
                if (currentField.CssClass == "header")
                    emailBody += "<h2>" + currentField.labelText + "</h2>";
                else if (currentField.CssClass == "pageBreak")
                    emailBody += "<p style='page-break-after:always'>End of page.</p>";
                else if (currentField.CssClass == "bold")
                    emailBody += "<p><b>" + currentField.labelText + "</b></p>";
                else 
                    emailBody += "<p>" + currentField.labelText + "</p>";
            }

            else if (currentField.controlType == "Literal")
            {
                emailBody += currentField.controlText;
            }

            //Used to draw labels and their associated controls
            else
            {
                emailBody += "<p style='margin: 0px;'>" + currentField.labelText + " <b>" + currentField.controlText + "</b></p>";
            }
        } //end for loop creting tiffs

        emailBody += "</html>";

        return emailBody;
    }

    public void SendEmail(string subject, string emailBody, List<string> TO, List<string> BCC, List<String> CC, string From, List<string> attatchments)
    {
        MailMessage mail = new MailMessage();
        SmtpClient mailer = new SmtpClient("owa13.Sullivan.edu");
        mailer.Port = 26;
        mail.Bcc.Add(new MailAddress("websupport@sullivan.edu"));
        //mail.Bcc.Add(new MailAddress("istarbuck@sullivan.edu"));
        mail.Body = emailBody;
        mail.Subject = subject;
        mail.IsBodyHtml = true;
        mailer.EnableSsl = false;
        mail.From = new MailAddress(From);

        foreach(var to in TO)
        {
            mail.To.Add(new MailAddress(to));
        }

        foreach (var bcc in BCC)
        {
            mail.Bcc.Add(new MailAddress(bcc));
        }

        foreach (var cc in CC)
        {
            mail.CC.Add(new MailAddress(cc));
        }

        foreach (string attatchment in attatchments)
        {
            mail.Attachments.Add(new Attachment(attatchment));
        }

        mailer.Send(mail);

        mailer.Dispose();

        mail.Attachments.Dispose();
        mail = null; 
    }

    public List<FieldInfo> WizardStepInfo(WizardStepBase wizardStep, List<FieldInfo> wizardInfo)
    {
        formInfo = wizardInfo;

        //Start by looping through each control in the Wizard Step
        foreach (Control control in wizardStep.Controls)
        {
            //If the control is a User Control then loop through each control inside the panel in the current user control
            if (control.GetType().ToString().EndsWith("ascx"))
            {
                foreach (Control currentControl in control.Controls)
                {
                    if (currentControl is Panel)
                    {
                        CreateFormInfo((Panel)currentControl);
                    }

                }

            }
        }//end foreach loop

        return formInfo;

    }// end AddRecords Method


    public List<FieldInfo> CreateFormInfo(Panel panel)
    {
        string controlText;

        if (panel.GroupingText != null && panel.GroupingText != "")
        {
            formInfo.Add(new FieldInfo()
            {
                labelID = ((Panel)panel).ID,
                labelText = ((Panel)panel).GroupingText,
                controlID = null,
                CssClass = "header",
                visible = ((Panel)panel).Visible,
                controlType = "Panel",
                controlText = null,
                panelID = ((Panel)panel).ID
            });
        }

        //the page is warpped in a pnael, cycle through it to get all the labels on the page
        foreach (Control currentControl in panel.Controls)
        {
            //if the current selected control is a panel you need to cycle through this panel to get all the controls in side of it
            if (currentControl is Panel)
                CreateFormInfo((Panel)currentControl);
            else if (currentControl is UserControl)
            {
                foreach (Control currentControl2 in currentControl.Controls)
                {
                    if (currentControl2 is Panel)
                    {
                        CreateFormInfo((Panel)currentControl2);
                    }
                }
            }

            else if (currentControl is UpdatePanel)
                UpdatePanelLabels((UpdatePanel)currentControl);


            //if the current selected control if a label, then add its ID to the label list
            else if ((currentControl is Label) && (!(currentControl is RequiredFieldValidator)) && (!(currentControl is RegularExpressionValidator)))
            {
                if (((Label)currentControl).AssociatedControlID == "" || ((Label)currentControl).AssociatedControlID == null)
                {
                    if (((Label)currentControl).CssClass != "Hidden")
                    {
                        formInfo.Add(new FieldInfo()
                        {
                            labelID = ((Label)currentControl).ID,
                            labelText = ((Label)currentControl).Text,
                            controlID = null,
                            CssClass = ((Label)currentControl).CssClass,
                            visible = ((Label)currentControl).Visible,
                            controlType = "Label",
                            controlText = null,
                            panelID = panel.ID
                        });
                    }
                }
                else
                {
                    controlText = GetControlText(((Label)currentControl).AssociatedControlID, panel);
                    if (controlText != null && controlText.Length > 0)
                    {
                        formInfo.Add(new FieldInfo()
                        {
                            labelID = ((Label)currentControl).ID,
                            labelText = ((Label)currentControl).Text,
                            controlID = ((Label)currentControl).AssociatedControlID,
                            CssClass = ((Label)currentControl).CssClass,
                            visible = ((Label)currentControl).Visible,
                            controlType = ((Label)currentControl).AssociatedControlID.GetType().ToString(),
                            controlText = controlText,
                            panelID = panel.ID
                        });
                    }
                }
            }
            else if (currentControl is Literal)
            {
                formInfo.Add(new FieldInfo()
                {
                    labelID = null,
                    labelText = null,
                    controlID = currentControl.ID,
                    CssClass = null,
                    visible = ((Literal)currentControl).Visible,
                    controlType = "Literal",
                    controlText = ((Literal)currentControl).Text,
                    panelID = panel.ID
                });
            }
        }

        return formInfo;
    }

    public void UpdatePanelLabels(UpdatePanel panel)
    {
        string controlText;

        foreach (Control currentControl in panel.ContentTemplateContainer.Controls)
        {

            //if the current selected control is a panel you need to cycle through this panel to get all the controls in side of it
            if (currentControl is Panel)
                CreateFormInfo((Panel)currentControl);

            ////if the current selected control if a label, then add its ID to the label list
            //else if ((currentControl is Label) && (!(currentControl is RequiredFieldValidator)) && (!(currentControl is RegularExpressionValidator)))
            //{
            //    controlText = GetControlText(((Label)currentControl).AssociatedControlID, panel);
            //    labelList.Add(new FieldInfo()
            //    {
            //        labelID = ((Label)currentControl).ID,
            //        labelText = ((Label)currentControl).Text,
            //        controlID = ((Label)currentControl).AssociatedControlID,
            //        CssClass = ((Label)currentControl).CssClass,
            //        visible = ((Label)currentControl).Visible,
            //        controlType = ((Label)currentControl).AssociatedControlID.GetType().ToString(),
            //        controlText = controlText
            //    });
            //}
        }
    }

    //Used to get the Text value of a control so the CreateTiff function knows what to draw on the image
    protected string GetControlText(string controlId, Panel currentPanel)
    {
        try
        {
            Control control = currentPanel.FindControl(controlId);

            //check to see what type of control is selected so you can get the appropiate text value
            if (control is TextBox)
                return ((TextBox)control).Text;

            else if (control is DropDownList)
            {
                if (((DropDownList)control).SelectedIndex > 0)
                {
                    return ((DropDownList)control).SelectedItem.Text;
                }
                else
                    return null;

            }

            else if (control is Literal)
                return ((Literal)control).Text;

            else if (control is RadioButtonList)
            {
                if (((RadioButtonList)control).SelectedIndex > -1)
                    return ((RadioButtonList)control).SelectedItem.Text;
                else
                    return null;
            }

            else if (control is CheckBox)
            {
                if (((CheckBox)control).Checked)
                    return "Yes";
                else
                    return "No";
            }

            else if (control is HiddenField)
            {
                return SigJsonToImage(((HiddenField)control).Value.ToString(), controlId);
            }

            else
                return "Information could not be found.";
        }
        catch(Exception ex)
        {
            //Response.Redirect(controlId);
            return ex.ToString();
        }
    }

    static string CleanInput(string strIn)
    {
        // Replace invalid characters with empty strings.
        try
        {
            return Regex.Replace(strIn, @"[^\w\.@-\\%]", "", RegexOptions.None);
        }
        // If we timeout when replacing invalid characters, 
        // we should return Empty.
        catch 
        {
            return String.Empty;
        }
    }

    //The following three methods save the E-Sig image
    public string SigJsonToImage(string json, string controlID)
    {
        Color PenColor = Color.FromArgb(20, 83, 148);
        float PenWidth = 2;
        string imageName;
        var signatureImage = GetBlankCanvas();

        imageName = "ESig_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond + ".tiff";
        if (!string.IsNullOrEmpty(json))
        {
            using (Graphics signatureGraphic = Graphics.FromImage(signatureImage))
            {
                signatureGraphic.SmoothingMode = SmoothingMode.AntiAlias;
                var pen = new Pen(PenColor, PenWidth);
                var serializer = new JavaScriptSerializer();
                // Next line may throw System.ArgumentException if the string
                // is an invalid json primitive for the SignatureLine structure
                var lines = serializer.Deserialize<List<SignatureLine>>(json);
                foreach (var line in lines)
                {
                    signatureGraphic.DrawLine(pen, line.lx, line.ly, line.mx, line.my);
                }

                //agreement1.Text = imageName;
                signatureImage.Save(HttpContext.Current.Server.MapPath("ESigImages/" + imageName), ImageFormat.Tiff);

                string imagePath = "http://testsecure.sullivan.edu/FinancialPlanning/ESigImages/" + imageName;

                if (controlID == "output")
                    HttpContext.Current.Session["ESig"] = HttpContext.Current.Server.MapPath("Tiff/" + imageName);

                else if (controlID == "output2")
                    HttpContext.Current.Session["ESigParent"] = HttpContext.Current.Server.MapPath("Tiff/" + imageName);

                return "<img src='" + imagePath + "' />";
            }

        }
        else
            return "THis didn't work. ";
    }



    private Bitmap GetBlankCanvas()
    {
        int CanvasWidth = 450;
        int CanvasHeight = 80;
        Color BackgroundColor = Color.White;

        Bitmap blankImage = new Bitmap(CanvasWidth, CanvasHeight);
        blankImage.MakeTransparent();
        using (var signatureGraphic = Graphics.FromImage(blankImage))
        {
            signatureGraphic.Clear(BackgroundColor);
        }
        return blankImage;
    }

    private class SignatureLine
    {
        public int lx { get; set; }
        public int ly { get; set; }
        public int mx { get; set; }
        public int my { get; set; }
    }

    //Remove Harmfull characters from attachments
    public string CleanAttahcmentName(string fileName)
    {
        List<string> naughtyChar = new List<string>();

        naughtyChar.Add("<");
        naughtyChar.Add(">");
        naughtyChar.Add("?");
        naughtyChar.Add("&");
        naughtyChar.Add("*");
        naughtyChar.Add("@");

        foreach(string naughtiness in naughtyChar)
        {
            fileName = fileName.Replace(naughtiness, "");
        }

        return fileName;
    }


}