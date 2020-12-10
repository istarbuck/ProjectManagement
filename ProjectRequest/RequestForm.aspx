<%@ Page Title="" Language="C#" MasterPageFile="~/MaterialDesign.master"AutoEventWireup="true" CodeBehind="RequestForm.aspx.cs" Inherits="ProjectRequest.RequestForm" MaintainScrollPositionOnPostback="true" Debug="true" %>

<asp:Content ID="myHead" ContentPlaceHolderID="head" runat="server">
    <title>Creative Communications Project Request</title>

    <style>

        #btnWebUpload input
        {
            margin-bottom: 0px;
        }

        .extraLabelSpace input[type="text"]
        {
            padding-top: 15px;
        }

    </style>

</asp:Content>


<asp:Content ID="myHeader"  ContentPlaceHolderID="header" runat="server">
    Creative Communications Project Request
</asp:Content>

<asp:Content ID="myContent" ContentPlaceHolderID="content" runat="server">

<%--<asp:Panel runat="server" ID="pnlBlackOut" CssClass="sectionGroup" GroupingText="Blackout Period" >

    <p class="warningLarge noTopSpace">We are currently in a blackout period and not accepting new projects as we prepare for 2017. If you have any questions or urgent requests, please contact <a href="mailto:TWhittinghill@sullivan.edu">Thomas Whittinghill</a>. Thanks for your patience!</p>


</asp:Panel>--%>

<asp:Panel runat="server" ID="mainPanel">



<asp:Panel runat="server" ID="pnlIntro" CssClass="sectionGroup" GroupingText="Preliminary Info" >

<asp:Label runat="server" ID="lblHello" CssClass="collegeHeader"></asp:Label>
<br /><br />

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">account_box</i>   
        <asp:TextBox runat="server" ID="tbFName" ></asp:TextBox>
        <asp:Label runat="server" ID="lblFName" AssociatedControlID="tbFName" Font-Bold="true" CssClass="active" >First Name:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: First Name" ControlToValidate="tbFName" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">account_box</i>   
        <asp:TextBox runat="server" ID="tbLName" ></asp:TextBox>
        <asp:Label runat="server" ID="lblLName" AssociatedControlID="tbLName" Font-Bold="true" CssClass="active">Last Name:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Last Name" ControlToValidate="tbLName" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblLocation" AssociatedControlID="ddlLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">mail</i>   
        <asp:TextBox runat="server" ID="tbEmail" OnTextChanged="tbEmail_TextChanged" AutoPostBack="true" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEmail" AssociatedControlID="tbEmail" Font-Bold="true" CssClass="active">Email Address:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Email" ControlToValidate="tbEmail" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator runat="server" ControlToValidate="tbEmail" ErrorMessage="xxx@xxx.xxx"  Display="Dynamic"
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">contact_phone</i>   
        <asp:TextBox runat="server" ID="tbContact" ></asp:TextBox>
        <asp:Label runat="server" ID="lblContact" AssociatedControlID="tbContact" Font-Bold="true" CssClass="active">Phone Number:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Contact Info" ControlToValidate="tbContact" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">border_color</i>   
        <asp:TextBox runat="server" ID="tbProjectName" ></asp:TextBox>
        <asp:Label runat="server" ID="lblProjectName" AssociatedControlID="tbProjectName" Font-Bold="true">Project Name:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Project Name" ControlToValidate="tbProjectName" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">live_help</i>   
        <asp:DropDownList runat="server" ID="ddlHelp" AutoPostBack="true" onselectedindexchanged="ddlHelp_SelectedIndexChanged" >
            <asp:ListItem Value="none">Please Select:</asp:ListItem>
            <asp:ListItem Value="website">I need something changed / created on the web</asp:ListItem>
            <%--<asp:ListItem Value="eBlast">I need an e-blast</asp:ListItem>--%>
            <asp:ListItem Value="event">I have an upcoming event and need a bunch of stuff!</asp:ListItem>
            <asp:ListItem Value="postcard">I need a postcard mailed</asp:ListItem>
            <asp:ListItem Value="cool">I have something cool to share!</asp:ListItem>
            <asp:ListItem Value="crew">I need a filming crew</asp:ListItem>
            <asp:ListItem Value="photographer">I need a photographer</asp:ListItem>
            <asp:ListItem Value="conference">I need display items for a conference</asp:ListItem>
            <asp:ListItem Value="studentServices">I need Student Services items</asp:ListItem>
            <asp:ListItem Value="other">I need something not listed here</asp:ListItem>
            <%--<asp:ListItem Value="notSure">I’m not sure what I need!</asp:ListItem>--%>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblHelp" AssociatedControlID="ddlHelp" Font-Bold="true">Okay, how can we help?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Help" ControlToValidate="ddlHelp" InitialValue="none" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


</div><!-- End Row -->

</asp:Panel>


<asp:Panel runat="server" ID="pnlDOA" CssClass="sectionGroup" GroupingText="Project Approval">

<div class="row">

    <div class="col s12 extraSpace">
        <p class="noTopSpace">In order to move forward on your project as efficiently as possible, we first need to verify that it has been approved by the right person. 
        Please select the executive director or director of admissions who approved the project from the drop-down box, and then upload an email or 
        other document indicating their approval of the project. </p>
    </div>

    <div class="input-field col selectIcon s12 m6 l6">
        <i class="material-icons prefix">person</i>  
        <asp:DropDownList runat="server" ID="ddlDOA" AutoPostBack="true" OnSelectedIndexChanged="VerifyDOA" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource2" DataTextField="doaName" DataValueField="doaID" >
        <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblDOA" AssociatedControlID="ddlDOA">Who approved this project?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDOA" InitialValue="00" ErrorMessage="Required: Project Approver"></asp:RequiredFieldValidator>
    </div>

</div>

<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ProjectRequestConnectionString %>" 
    SelectCommand="SELECT [doaID], [doaName] FROM [DOA] ORDER BY [doaName]">
</asp:SqlDataSource>


<div class="upload">
<div class="row extraSpace">

    <div class="col s6">
        <asp:Label runat="server"><b>Please upload written documentation of project approval:</b></asp:Label>
        <asp:FileUpload ID="doaUpload" runat="server" />
        <asp:Button ID="btnDoaUpload" runat="server" Text="Upload File" CssClass="btn waves-effect"
            CausesValidation="false" onclick="btndoaUpload_Click" />
    </div>

    <div class="col s6">
        <p class="collegeHeader noTopSpace">Your Uploaded File: </p>
        <asp:Label ID="lbldoaUpload" runat="server" CssClass="warningLarge"><b>You have not uploaded any files</b></asp:Label>
    </div>

</div>
</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlWebChange" CssClass="sectionGroup" GroupingText="Web Change Info">

<div class="row extraSpace">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">web</i>   
        <asp:TextBox runat="server" ID="tbWebSite"></asp:TextBox>
        <asp:Label runat="server" ID="lblWebSite" AssociatedControlID="tbWebSite">Which website? (We need the URL)</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Which web site?" ControlToValidate="tbWebSite" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12"> 
        <i class="material-icons prefix">description</i>   
        <asp:TextBox runat="server" ID="tbHappen" TextMode="MultiLine" CssClass="materialize-textarea" ></asp:TextBox>
        <asp:Label runat="server" ID="lblHappen" AssociatedControlID="tbHappen" Font-Bold="true">What needs to happen?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What needs to happen?" ControlToValidate="tbHappen" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

    <asp:Panel runat="server" ID="pnlWebAttachment" CssClass="upload" >

    <div class="row extraSpace">

        <div class="col s6">
            <asp:Label runat="server"><b>Upload your files.</b></asp:Label>
            <asp:FileUpload ID="webUpload" runat="server" />

            <asp:Button ID="btnWebUpload" runat="server" Text="Upload File" CssClass="btn waves-effect"
            CausesValidation="false" onclick="btnWebUpload_Click" />
        </div>

        <div class="col s6">
            <p class="collegeHeader noTopSpace">Your Uploaded Files</p>
            <asp:Label ID="noWebUpload" runat="server" CssClass="warningLargeLarge">You have not uploaded any files</asp:Label>
            <asp:BulletedList ID="webUploadList" runat="server" BulletStyle="Disc" Font-Size="Large">
            </asp:BulletedList>
        </div>

    </div>

    </asp:Panel>

</asp:Panel>

<asp:Panel runat="server" ID="pnlEvent" CssClass="sectionGroup" GroupingText="Event Info">

<div class="row">
    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbEventDate" OnTextChanged="tbEventDate_TextChanged" AutoPostBack="true" ClientIDMode="Static" CssClass="datePicker"></asp:TextBox>
        <asp:Label runat="server" ID="lblEventDate" AssociatedControlID="tbEventDate" Font-Bold="true">When is your event?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Date" ControlToValidate="tbEventDate" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>


<asp:Panel runat="server" ID="pnlEventItems">

<div class="row cbList extraSpace">

    <div class="input-field selectIcon col s12 m6 l4">
    <asp:CheckBox runat="server" ID="cbMailer" AutoPostBack="True" OnCheckedChanged="cbMailer_CheckedChanged" />
    <asp:Label runat="server" ID="lblMailer" AssociatedControlID="cbMailer">Mailer (60 business days required)</asp:Label>
    </div>

    <div class="input-field selectIcon col s12 m6 l4">
    <asp:CheckBox runat="server" ID="cbFlyers" AutoPostBack="True" OnCheckedChanged="cbFlyers_CheckedChanged" />
    <asp:Label runat="server" ID="lblFlyers" AssociatedControlID="cbFlyers">Flyers (10 business days required)</asp:Label>
    </div>

    <div class="input-field selectIcon col s12 m6 l4">
    <asp:CheckBox runat="server" ID="cbPosters" AutoPostBack="True" OnCheckedChanged="cbPosters_CheckedChanged" />
    <asp:Label runat="server" ID="lblPosters" AssociatedControlID="cbPosters">Posters (10 business days required)</asp:Label>
    </div>

    <div class="input-field selectIcon col s12 m6 l4">
    <asp:CheckBox runat="server" ID="cbSignage" AutoPostBack="True" OnCheckedChanged="cbPosters_CheckedChanged" />
    <asp:Label runat="server" ID="lblSignage" AssociatedControlID="cbSignage">Signage (10 business days required)</asp:Label>
    </div>

    <div class="input-field selectIcon col s12 m6 l4">
    <asp:CheckBox runat="server" ID="cbEBlast" AutoPostBack="True" OnCheckedChanged="cbEBlast_CheckedChanged" />
    <asp:Label runat="server" ID="lblEBlast" AssociatedControlID="cbEBlast">E-Blast (5 business days required)</asp:Label>
    </div>

</div>

</asp:Panel>


<div class="row">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">schedule</i>
        <asp:TextBox runat="server" ID="tbEventTime" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEventTime" AssociatedControlID="tbEventTime" Font-Bold="true">Times of the event?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Time" ControlToValidate="tbEventTime" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">account_balance</i>
        <asp:TextBox runat="server" ID="tbEventCampuses" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEventCampuses" AssociatedControlID="tbEventCampuses" Font-Bold="true">Which campuses are involved?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Time" ControlToValidate="tbEventCampuses" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">accessibility</i>
        <asp:TextBox runat="server" ID="tbEventInvites" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEventInvites" AssociatedControlID="tbEventInvites" Font-Bold="true">Who's invited?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Invites" ControlToValidate="tbEventInvites" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">supervisor_account</i>
        <asp:DropDownList runat="server" ID="ddlCVGroup2" AutoPostBack="true" onselectedindexchanged="ddlCVGroup2_SelectedIndexChanged" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblCVGroup2" AssociatedControlID="ddlCVGroup2" Font-Bold="true">Do you have a CampusVue group pulled?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: CV Group" ControlToValidate="ddlCVGroup2" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>



</div>


    <asp:Panel runat="server" ID="pnlCVGroupYes2">

    <div class="row extraSpace">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">supervisor_account</i>
            <asp:TextBox runat="server" ID="tbCVGroupYes2" ></asp:TextBox>
            <asp:Label runat="server" ID="lblCVGroupYes2" AssociatedControlID="tbCVGroupYes2" Font-Bold="true">What's the group name?</asp:Label>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Group Name" ControlToValidate="tbCVGroupYes2" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row extraSpace upload">

        <div class="col s6 extraSpace">
            <asp:Label runat="server"><b>Upload an Excel file with your group</b></asp:Label>
            <asp:FileUpload ID="cvUpload2" runat="server" />
            <asp:Button ID="btnCvUpload2" runat="server" Text="Upload File" CssClass="btn waves-effect"
                CausesValidation="false" onclick="btnCvUpload2_Click" />
        </div>

        <div class="col s6 extraSpace">
            <p class="collegeHeader noTopSpace">Your Uploaded File: </p>
            <asp:Label ID="lblCVUpload2" runat="server" CssClass="warningLarge"><b>You have not uploaded any files</b></asp:Label>
        </div>

    </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlCVGroupNo2">

    <div class="row">

    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">file_upload</i>
        <asp:DropDownList runat="server" ID="tbCVGroupNo2"  >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblCVGroupNo2" AssociatedControlID="tbCVGroupNo2" Font-Bold="true">Would you like us to pull the group for you? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Pull CV group" ControlToValidate="tbCVGroupNo2" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    </div>

    </asp:Panel>

<div class="row">

     <div class="input-field col s12">
        <i class="material-icons prefix">assignment_ind</i>
        <asp:TextBox runat="server" ID="tbEventReason" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblEventReason" AssociatedControlID="tbEventReason" Font-Bold="true">Why would someone come to this event?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Reason" ControlToValidate="tbEventReason" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12">
        <i class="material-icons prefix">movie_filter</i>
        <asp:TextBox runat="server" ID="tbEventCoolness" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblEventCoolness" AssociatedControlID="tbEventCoolness" Font-Bold="true">What’s the coolest thing happening at this event?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Coolness" ControlToValidate="tbEventCoolness" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>



<asp:Panel runat="server" ID="pnlEBlast" CssClass="sectionGroup" GroupingText="E-Blast Info">

<div class="row extraSpace">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbEBlastDate" OnTextChanged="tbEBlastDate_TextChanged" AutoPostBack="true" ClientIDMode="Static" CssClass="datePicker"></asp:TextBox>
        <asp:Label runat="server" ID="lblEBlastDate" AssociatedControlID="tbEBlastDate" Font-Bold="true">When would you like it to be sent? (5 days are required)</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: E-Blast Date" ControlToValidate="tbEBlastDate" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">mail</i>
        <asp:TextBox runat="server" ID="tbEBlastEmail" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEBlastEmail" AssociatedControlID="tbEBlastEmail" Font-Bold="true">What email address would you like listed as the sender?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: E-Blast Email" ControlToValidate="tbEBlastEmail" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12">
        <i class="material-icons prefix">assignment</i>
        <asp:TextBox runat="server" ID="tbEBlastPurpose" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblEBlastPurpose" AssociatedControlID="tbEBlastPurpose" Font-Bold="true">What’s the purpose of the e-blast?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: E-Blast Purpose" ControlToValidate="tbEBlastPurpose" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>



    <div class="col s12">
        <asp:Label runat="server" ID="lblEblastURLs" CssClass="miniHeader" >Our new email blast template gives readers the option to view your school/department/office's social media accounts. Please provide the links to the appropriate Facebook, Twitter, LinkedIn, YouTube, Instagram, etc. accounts that readers should be directed to from this email.</asp:Label>
        <br /><br />
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">web</i>
        <asp:TextBox runat="server" ID="tbFacebook" ></asp:TextBox>
        <asp:Label runat="server" ID="lblFacebook" AssociatedControlID="tbFacebook" Font-Bold="true">Facebook:</asp:Label>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">web</i>
        <asp:TextBox runat="server" ID="tbTwitter" ></asp:TextBox>
        <asp:Label runat="server" ID="lblTwitter" AssociatedControlID="tbTwitter" Font-Bold="true">Twitter:</asp:Label>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">web</i>
        <asp:TextBox runat="server" ID="tbLinkedIn" ></asp:TextBox>
        <asp:Label runat="server" ID="lblLinkedIn" AssociatedControlID="tbLinkedIn" Font-Bold="true">LinkedIn:</asp:Label>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">web</i>
        <asp:TextBox runat="server" ID="tbYouTube" ></asp:TextBox>
        <asp:Label runat="server" ID="lblYouTube" AssociatedControlID="tbYouTube" Font-Bold="true">YouTube:</asp:Label>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">web</i>
        <asp:TextBox runat="server" ID="tbInstagram" ></asp:TextBox>
        <asp:Label runat="server" ID="lblInstagram" AssociatedControlID="tbInstagram" Font-Bold="true">Instagram:</asp:Label>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">web</i>
        <asp:TextBox runat="server" ID="tbAdditionalUrls" TextMode="MultiLine" CssClass="materialize-textarea" ></asp:TextBox>
        <asp:Label runat="server" ID="lblAdditionalUrls" AssociatedControlID="tbAdditionalUrls" Font-Bold="true">Additional URLs:</asp:Label>
    </div>
</div>

<div class="row">

    <div class="col s12">
        <asp:Label runat="server" ID="lblEblastPersonalInfo" CssClass="miniHeader">Please list the street address, phone number and email address would you like listed in the footer of this email blast.</asp:Label>
        <br /><br />
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">location_city</i>
        <asp:TextBox runat="server" ID="tbStreetAddress" ></asp:TextBox>
        <asp:Label runat="server" ID="lblStreetAddress" AssociatedControlID="tbStreetAddress" CssClass="optional">Street Address:</asp:Label>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">phone</i>
        <asp:TextBox runat="server" ID="tbPhoneNumber" ></asp:TextBox>
        <asp:Label runat="server" ID="lblPhoneNumber" AssociatedControlID="tbPhoneNumber"  CssClass="optional">Phone Number:</asp:Label>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">mail</i>
        <asp:TextBox runat="server" ID="tbEmailAddress" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEmailAddress" AssociatedControlID="tbEmailAddress"  CssClass="optional">Email Address:</asp:Label>
    </div>

    <div class="input-field selectIcon selectLarge col s12 m6 l6">
        <i class="material-icons prefix">folder_open</i>
        <asp:DropDownList runat="server" ID="ddlCVActivity" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblCVActivity" AssociatedControlID="ddlCVActivity" Font-Bold="true">Do you need a CampusVue activity created for this e-blast?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: CV Activity" ControlToValidate="ddlCVActivity" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">supervisor_account</i>
        <asp:DropDownList runat="server" ID="ddlCVGroup" AutoPostBack="true" onselectedindexchanged="ddlCVGroup_SelectedIndexChanged" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblCVGroup" AssociatedControlID="ddlCVGroup" Font-Bold="true">Do you have a CampusVue group pulled?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: CV Group" ControlToValidate="ddlCVGroup" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

<asp:Panel runat="server" ID="pnlCVGroupNo">

<div class="row">
    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">file_upload</i>
        <asp:DropDownList runat="server" ID="tbCVGroupNo" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblCVGroupNo" AssociatedControlID="tbCVGroupNo" Font-Bold="true">Would you like us to pull the group for you? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Pull CV group" ControlToValidate="tbCVGroupNo" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
</div>

</asp:Panel>

</div>

<asp:Panel runat="server" ID="pnlCVGroupYes">

<div class="row extraSpace">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">supervisor_account</i>
        <asp:TextBox runat="server" ID="tbCVGroupYes" ></asp:TextBox>
        <asp:Label runat="server" ID="lblCVGroupYes" AssociatedControlID="tbCVGroupYes" Font-Bold="true">What's the group name?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Group Name" ControlToValidate="tbCVGroupYes" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

    <div class="row extraSpace upload">

        <div class="col s6 extraSpace">
            <asp:Label runat="server"><b>Upload an Excel file with your group</b></asp:Label>
            <asp:FileUpload ID="cvUpload" runat="server" />
            <asp:Button ID="btnCvUpload" runat="server" Text="Upload File" CssClass="btn waves-effect" 
                CausesValidation="false" onclick="btnCvUpload_Click" />
        </div>

        <div class="col s6 extraSpace">
            <div class="upload">
                <p class="collegeHeader noTopSpace">Your Uploaded File:</p>
                <asp:Label ID="lblCVUpload" runat="server" CssClass="warningLarge">You have not uploaded any files</asp:Label>
            </div>
        </div>

    </div>

</asp:Panel>

    <div class="row extraSpace">

    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">account_balance</i>
        <asp:DropDownList runat="server" ID="ddlEBlastCampus" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Sullivan Louisville</asp:ListItem>
            <asp:ListItem>Sullivan Lexington</asp:ListItem>
            <asp:ListItem>Sullivan Fort Knox</asp:ListItem>
            <asp:ListItem>Sullivan Online</asp:ListItem>
            <asp:ListItem>Sullivan Louisa</asp:ListItem>
            <asp:ListItem>Sullivan Northern Kentucky</asp:ListItem>
            <asp:ListItem>Spencerian Louisville</asp:ListItem>
            <asp:ListItem>Spencerian Lexington</asp:ListItem>
            <asp:ListItem>SCTD</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblEBlastCampus" AssociatedControlID="ddlEBlastCampus" Font-Bold="true">What campus is the e-blast for?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Student Campus" ControlToValidate="ddlEBlastCampus" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">mail</i>
        <asp:TextBox runat="server" ID="tbEBlastFor" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEBlastFor" AssociatedControlID="tbEBlastFor" Font-Bold="true">Who is this mailer or e-blast intended for?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: E-Blast intended for" ControlToValidate="tbEBlastFor" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    </div>

    <div class="row extraSpace">

        <div class="input-field extraLabelSpace col s12 m6 l6">
        <i class="material-icons prefix">date_range</i>
        <asp:TextBox runat="server" ID="tbEBlastData" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEBlastData" AssociatedControlID="tbEBlastData" Font-Bold="true">How far back do you want the data to go? (ie. When lead was first received).</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: How far back for e-blast" ControlToValidate="tbEBlastData" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <div class="input-field extraLabelSpace col s12 m6 l6">
        <i class="material-icons prefix">group</i>
        <asp:TextBox runat="server" ID="tbEBlastType" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEBlastType" AssociatedControlID="tbEBlastType" Font-Bold="true">Any particular student type you would like to exclude? International, re-entry etc. Please be specific. </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: How far back for e-blast" ControlToValidate="tbEBlastGeo" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

   </div>

   <div class="row">

        <div class="input-field extraLabelSpace col s12">
        <i class="material-icons prefix">public</i>
        <asp:TextBox runat="server" ID="tbEBlastGeo" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEBlastGeo" AssociatedControlID="tbEBlastGeo" Font-Bold="true">Are their geographical constraints (ie. Only in KY, Lou,Lex etc.) Please note that for mailers discretion may be used to reduce the number of mail recipients (not e-blasts) if list comes back over 4,000.</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: How far back for e-blast" ControlToValidate="tbEBlastGeo" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">bookmark</i>
        <asp:TextBox runat="server" ID="tbEBlastProgram" ></asp:TextBox>
        <asp:Label runat="server" ID="lblEBlastProgram" AssociatedControlID="tbEBlastProgram" Font-Bold="true">Are there any specific program requests?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: How far back for e-blast" ControlToValidate="tbEBlastProgram" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

   </div>


</asp:Panel>

<asp:Panel runat="server" ID="pnlPostcard" CssClass="sectionGroup" GroupingText="Postcard Info">

<div class="row">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">description</i>
        <asp:TextBox runat="server" ID="tbMailPurpose" ></asp:TextBox>
        <asp:Label runat="server" ID="lblMailPurpose" AssociatedControlID="tbMailPurpose" Font-Bold="true">What’s the purpose of this mailing?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Mail Purpose" ControlToValidate="tbMailPurpose" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">account_circle</i>
        <asp:TextBox runat="server" ID="tbMailReception" ></asp:TextBox>
        <asp:Label runat="server" ID="lblMailReception" AssociatedControlID="tbMailReception" Font-Bold="true">Who needs to receive it?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Mail Reception" ControlToValidate="tbMailReception" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
</div>
<br />

<div class="row">

    <div class="input-field inputLarge col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbMailDate" ClientIDMode="Static" CssClass="datePicker" 
                AutoPostBack="true" ontextchanged="tbMailDate_TextChanged"></asp:TextBox>
        <asp:Label runat="server" ID="lblMailDate" AssociatedControlID="tbMailDate" Font-Bold="true">When does it need to be in mailboxes?(60 business days required )</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Mail Date" ControlToValidate="tbMailDate" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">supervisor_account</i>
        <asp:DropDownList runat="server" ID="ddlCVGroup3" AutoPostBack="true" onselectedindexchanged="ddlCVGroup3_SelectedIndexChanged" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblCVGroup3" AssociatedControlID="ddlCVGroup3" Font-Bold="true">Do you have a CampusVue group pulled?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: CV Group" ControlToValidate="ddlCVGroup3" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


    <asp:Panel runat="server" ID="pnlCVGroupNo3">

        <div class="input-field selectIcon col s12 m6 l6">
            <i class="material-icons prefix">file_upload</i>
            <asp:DropDownList runat="server" ID="tbCVGroupNo3">
                <asp:ListItem Value="00">Please Select:</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:DropDownList>
            <asp:Label runat="server" ID="lblCVGroupNo3" AssociatedControlID="tbCVGroupNo3" Font-Bold="true">Would you like us to pull the group for you? </asp:Label>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Pull CV group" ControlToValidate="tbCVGroupNo3" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>


    </asp:Panel>


</div>

    <asp:Panel runat="server" ID="pnlCVGroupYes3">

    <div class="row extraSpace">

        <div class="input-field selectIcon col s12 m6 l6">
            <i class="material-icons prefix">supervisor_account</i>
            <asp:TextBox runat="server" ID="tbCVGroupYes3" ></asp:TextBox>
            <asp:Label runat="server" ID="lblCVGroupYes3" AssociatedControlID="tbCVGroupYes3" Font-Bold="true">What's the group name?</asp:Label>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Group Name" ControlToValidate="tbCVGroupYes3" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

    </div>

    <div class="row upload">
        <div class="col s6 extraSpace">
            <asp:Label runat="server"><b>Upload an Excel file with your group</b></asp:Label>
            <asp:FileUpload ID="cvUpload3" runat="server" />
            <asp:Button ID="btnCvUpload3" runat="server" Text="Upload File" CssClass="btn waves-effect" 
                CausesValidation="false" onclick="btnCvUpload3_Click" />
        </div>

        <div class="col s6 extraSpace">
            <p class="collegeHeader noTopSpace">Your Uploaded File: </p>
            <asp:Label ID="lblCVUpload3" runat="server" CssClass="warningLarge">You have not uploaded any files</asp:Label>
        </div>
    </div>

    </asp:Panel>



</asp:Panel>


<asp:Panel runat="server" ID="pnlCool" CssClass="sectionGroup" GroupingText="Coolness Info">

<div class="row extraSpace">

    <div class="input-field col s12">
        <i class="material-icons prefix">supervisor_account</i>
        <asp:TextBox runat="server" ID="tbStory" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblStory" AssociatedControlID="tbStory" Font-Bold="true">What's the story?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Cool story info" ControlToValidate="tbStory" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">supervisor_account</i>
        <asp:DropDownList runat="server" ID="ddlStoryPic" AutoPostBack="true" onselectedindexchanged="ddlStoryPic_SelectedIndexChanged" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblStoryPic" AssociatedControlID="ddlStoryPic" Font-Bold="true">Got pictures?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Story Pic" ControlToValidate="ddlStoryPic" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


</div>


<asp:Panel runat="server" ID="pnlStoryPic">

<div class="row upload">

    <div class="col s6 extraSpace">
        <asp:Label runat="server"><b>Upload your pictures.</b></asp:Label>
        <asp:FileUpload ID="picUpload" runat="server" />
        <asp:Button ID="btnPicUpload" runat="server" Text="Upload File" CssClass="btn waves-effect"
            CausesValidation="false" onclick="btnPicUpload_Click" />
    </div>

    <div class="col s6 extraSpace">
        <p class="collegeHeader noTopSpace">Your Uploaded Files</p>
        <asp:Label ID="noUpload" runat="server" CssClass="warningLarge">You have not uploaded any files</asp:Label>
        <asp:BulletedList ID="picUploadList" runat="server" BulletStyle="Disc" Font-Size="Large">
        </asp:BulletedList>
    </div>

</div>

</asp:Panel>

</asp:Panel>

<asp:Panel runat="server" ID="pnlFilmCrew" CssClass="sectionGroup" GroupingText="Filming Crew Info">

<div class="row">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbFilmWhen" ClientIDMode="Static" CssClass="datePicker"></asp:TextBox>
        <asp:Label runat="server" ID="lblFilmWhen" AssociatedControlID="tbFilmWhen" Font-Bold="true">When are the crew needed?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: When's the party" ControlToValidate="tbFilmWhen" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12">
        <i class="material-icons prefix">movie</i>
        <asp:TextBox runat="server" ID="tbFilmWhat" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblFilmWhat" AssociatedControlID="tbFilmWhat" Font-Bold="true">What's going on? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What's going on" ControlToValidate="tbFilmWhat" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlPhotographer" CssClass="sectionGroup" GroupingText="Photographer Info">

<div class="row">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbPhotoWhen" ClientIDMode="Static" CssClass="datePicker"></asp:TextBox>
        <asp:Label runat="server" ID="lblPhotoWhen" AssociatedControlID="tbPhotoWhen" Font-Bold="true">When’s the party? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: When's the party" ControlToValidate="tbPhotoWhen" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12">
        <i class="material-icons prefix">photo_camera</i>
        <asp:TextBox runat="server" ID="tbPhotoWhat" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblPhotoWhat" AssociatedControlID="tbPhotoWhat" Font-Bold="true">What's going on? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What's going on" ControlToValidate="tbPhotoWhat" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlConference" CssClass="sectionGroup" GroupingText="Conference Info">

<div class="row">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbConferenceWhen" ClientIDMode="Static" CssClass="datePicker"></asp:TextBox>
        <asp:Label runat="server" ID="lblConferenceWhen" AssociatedControlID="tbConferenceWhen" Font-Bold="true">When do you need these? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: When is the conference" ControlToValidate="tbConferenceWhen" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12">
        <i class="material-icons prefix">assignment</i>
        <asp:TextBox runat="server" ID="tbConferenceItems" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblConferenceItems" AssociatedControlID="tbConferenceItems" Font-Bold="true">What items do you need?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Items needed" ControlToValidate="tbConferenceItems" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>




</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlStudentServices" CssClass="sectionGroup" GroupingText="Student Services">

<div class="row">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbStudentServicesWhen" ClientIDMode="Static" CssClass="datePicker"></asp:TextBox>
        <asp:Label runat="server" ID="lblStudentServicesWhen" AssociatedControlID="tbStudentServicesWhen" Font-Bold="true">When do you need these? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: When do you need items" ControlToValidate="tbStudentServicesWhen" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12">
        <i class="material-icons prefix">assignment</i>
        <asp:TextBox runat="server" ID="tbStudentServicesItems" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblStudentServicesItems" AssociatedControlID="tbStudentServicesItems" Font-Bold="true">What items do you need?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Items needed" ControlToValidate="tbStudentServicesItems" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12">
        <i class="material-icons prefix">description</i>
        <asp:TextBox runat="server" ID="tbStudentServicesMessage" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" ID="lblStudentServicesMessage" AssociatedControlID="tbStudentServicesMessage" Font-Bold="true">Tell us about the messaging on these items:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Message needed" ControlToValidate="tbStudentServicesMessage" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>


<asp:Panel runat="server" ID="pnlNotSure" CssClass="sectionGroup" GroupingText="Not Sure">

<div class="row extraSpace">
    <div class="col s12">
        <p class="miniHeader noTopSpace">Please provide us with what information you can, and we will get back to you as soon as we can.</p>
    </div>
</div>

</asp:Panel>

</asp:Panel>

<asp:Panel runat="server" ID="pnlOther" CssClass="sectionGroup" GroupingText="Request Information">

<div class="row">

    <div class="input-field col s12">
        <i class="material-icons prefix">description</i>
        <asp:TextBox runat="server" ID="tbOther" TextMode="MultiLine" CssClass="materialize-textarea" ></asp:TextBox>
        <asp:Label runat="server" ID="lblOther" AssociatedControlID="tbOther"  >What do you need?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What do you need?" ControlToValidate="tbOther" Display="Dynamic"></asp:RequiredFieldValidator>

    </div>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbOtherDueDate"  ClientIDMode="Static" CssClass="datePicker"></asp:TextBox>
        <asp:Label runat="server" id="lblOtherDueDate" AssociatedControlID="tbOtherDueDate" >Due Date:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Due Date" ControlToValidate="tbOtherDueDate" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlCreative" CssClass="sectionGroup" GroupingText="Creative Project">

<div class="row">

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">event</i>
        <asp:TextBox runat="server" ID="tbDueDate"  ClientIDMode="Static" CssClass="datePicker"></asp:TextBox>
        <asp:Label runat="server" id="lblDueDate" AssociatedControlID="tbDueDate" CssClass="optional">Due Date:</asp:Label>
    </div>
    
    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>
        <asp:DropDownList runat="server" ID="ddlVendor" AppendDataBoundItems="True" DataSourceID="SqlDataSource3" DataTextField="publication" DataValueField="vendorID"   >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblVendor" AssociatedControlID="ddlVendor" CssClass="optional">Vendor</asp:Label>
    </div>

<%--    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">exposure</i>
        <asp:DropDownList runat="server" ID="ddlPO"  OnSelectedIndexChanged="ddlPO_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem Value="0">None</asp:ListItem>
            <asp:ListItem Value="1">One</asp:ListItem>
            <asp:ListItem Value="2">Two</asp:ListItem>
            <asp:ListItem Value="3">Three</asp:ListItem>
            <asp:ListItem Value="4">Four</asp:ListItem>
            <asp:ListItem Value="5">Five</asp:ListItem>
            <asp:ListItem Value="6">Six</asp:ListItem>
            <asp:ListItem Value="7">Seven</asp:ListItem>
            <asp:ListItem Value="8">Eight</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" id="lblPO" AssociatedControlID="ddlPO">Number of PO#s:</asp:Label>
    </div>

    <asp:Panel runat="server" ID="pnlPO1">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">filter_1</i>
            <asp:TextBox runat="server" ID="tbPO1" ></asp:TextBox>
            <asp:Label runat="server" id="lblPO1" AssociatedControlID="tbPO1">PO# One:</asp:Label>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlPO2">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">filter_2</i>
            <asp:TextBox runat="server" ID="tbPO2" ></asp:TextBox>
            <asp:Label runat="server" id="lblPO2" AssociatedControlID="tbPO2">PO# Two:</asp:Label>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlPO3">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">filter_3</i>
            <asp:TextBox runat="server" ID="tbPO3" ></asp:TextBox>
            <asp:Label runat="server" id="lblPO3" AssociatedControlID="tbPO3">PO# Three:</asp:Label>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlPO4">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">filter_4</i>
            <asp:TextBox runat="server" ID="tbPO4" ></asp:TextBox>
            <asp:Label runat="server" id="lblPO4" AssociatedControlID="tbPO4">PO# Four:</asp:Label>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlPO5">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">filter_5</i>
            <asp:TextBox runat="server" ID="tbPO5" ></asp:TextBox>
            <asp:Label runat="server" id="lblPO5" AssociatedControlID="tbPO5">PO# Five:</asp:Label>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlPO6">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">filter_6</i>
            <asp:TextBox runat="server" ID="tbPO6" ></asp:TextBox>
            <asp:Label runat="server" id="lblPO6" AssociatedControlID="tbPO6">PO# Six:</asp:Label>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlPO7">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">filter_7</i>
            <asp:TextBox runat="server" ID="tbPO7" ></asp:TextBox>
            <asp:Label runat="server" id="lblPO7" AssociatedControlID="tbPO7">PO# Seven:</asp:Label>
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="pnlPO8">

        <div class="input-field col s12 m6 l6">
            <i class="material-icons prefix">filter_8</i>
            <asp:TextBox runat="server" ID="tbPO8" ></asp:TextBox>
            <asp:Label runat="server" id="lblPO8" AssociatedControlID="tbPO8">PO# Eight:</asp:Label>
        </div>

    </asp:Panel>--%>

    <div class="input-field col s12 m6 l6">
        <i class="material-icons prefix">local_atm</i>
        <asp:TextBox runat="server" ID="tbCost" ></asp:TextBox>
        <asp:Label runat="server" id="lblCost" AssociatedControlID="tbCost" CssClass="optional">Cost (number only):</asp:Label>
        <asp:RegularExpressionValidator runat="server" ControlToValidate="tbCost" ErrorMessage="Please enter numbers only" ValidationExpression="^[0-9]([.,][0-9]{1,3})?$"></asp:RegularExpressionValidator>
    </div>

    <div class="input-field col s12">
        <i class="material-icons prefix">description</i>
        <asp:TextBox runat="server" ID="tbDetails" TextMode="MultiLine" CssClass="materialize-textarea" ></asp:TextBox>
        <asp:Label runat="server" id="lblDetails" AssociatedControlID="tbDetails" CssClass="optional">Details:</asp:Label>
    </div>
    
    <div class="input-field col s12">
        <i class="material-icons prefix">comments</i>
        <asp:TextBox runat="server" ID="tbComments" TextMode="MultiLine" CssClass="materialize-textarea" ></asp:TextBox>
        <asp:Label runat="server" id="lblComments" AssociatedControlID="tbComments" CssClass="optional">Comments:</asp:Label>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlAttachments" CssClass="sectionGroup" GroupingText="Additional Information">
<br /><br />


<div class="row extraSpace">

    <div class="input-field col s12">
        <i class="material-icons prefix">description</i>
        <asp:TextBox runat="server" ID="tbAdditionalInfo" TextMode="MultiLine" CssClass="materialize-textarea" ></asp:TextBox>
        <asp:Label runat="server" ID="lblAdditionalInfo" AssociatedControlID="tbAdditionalInfo" CssClass="optional">Please upload any additional information for this project, such as details that will be needed for the completion of the project. If you wish to include a bulleted list, please use the upload field below. </asp:Label>
    </div>

</div>

<div class="row upload">

    <div class="col s6 extraSpace">
        <asp:Label runat="server"><b>Please upload any additional information for this project, such as details that will be needed for the completion of the project.</b></asp:Label>
        <asp:FileUpload ID="Upload" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload File" CssClass="btn waves-effect"
            CausesValidation="false" onclick="btnUpload_Click" />
    </div>


    <div class="col s6 extraSpace">
        <p class="collegeHeader noTopSpace">Your Uploaded Files</p>
        <asp:Label ID="noUpload2" runat="server" CssClass="warningLarge">You have not uploaded any files</asp:Label>
        <asp:BulletedList ID="UploadList" runat="server" BulletStyle="Disc" Font-Size="Large">
        </asp:BulletedList>
    </div>

</div>

</asp:Panel>


<div class="right-align">
    <asp:Button runat="server" ID="btnSubmit" Text="Submit Project Request" onclick="btnSubmit_Click" CssClass="btn waves-effect" />
</div>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ProjectRequestConnectionString %>" 
    SelectCommand="SELECT [locationID], [name] FROM [Location] ORDER BY [name]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ProjectRequestConnectionString %>" 
    SelectCommand="SELECT [publication], [vendorID] FROM [Vendor] ORDER BY [publication]">
</asp:SqlDataSource>

</asp:Content>