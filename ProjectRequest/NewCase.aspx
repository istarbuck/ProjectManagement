<%@ Page Title="" Language="C#" MasterPageFile="~/MaterialDesign.master" AutoEventWireup="true" CodeBehind="NewCase.aspx.cs" Inherits="ProjectRequest.NewCase" MaintainScrollPositionOnPostback="true" Debug="true" %>

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

        html body
        {
            padding-bottom: 50px;
        }
    </style>

</asp:Content>


<asp:Content ID="myHeader"  ContentPlaceHolderID="header" runat="server">
    Creative Communications Project Request
</asp:Content>

<asp:Content ID="myContent" ContentPlaceHolderID="content" runat="server">

<asp:Panel runat="server" ID="pnlBlackOut" CssClass="sectionGroup" GroupingText="Blackout Period" Visible="false">

<div class="row extraSpace">
<div class="col s12">
    <p class="noTopSpace">We are currently in a blackout period and not accepting new projects as we prepare for the merger. If you have any questions or urgent requests, please contact <a href="mailto:snallen@sullivan.edu">Sarah Allen</a>. Thanks for your patience!</p>
</div>
</div>

</asp:Panel>

<asp:Panel runat="server" ID="mainPanel" Visible="true">



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

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">work</i>   
        <asp:DropDownList runat="server" ID="ddlDepartment" AppendDataBoundItems="True"
            DatasourceID="SqlDataSource2" DataTextField="department" DataValueField="department" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlDepartment" Font-Bold="true">Your Department</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Department" ControlToValidate="ddlDepartment" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
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
        <asp:TextBox runat="server" ID="tbContact" CssClass="phone placeholder" placeholder="(###)###-####" ></asp:TextBox>
        <asp:Label runat="server" ID="lblContact" AssociatedControlID="tbContact" >Phone Number:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Contact Info" ControlToValidate="tbContact" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">border_color</i>   
        <asp:TextBox runat="server" ID="tbProjectName" ></asp:TextBox>
        <asp:Label runat="server" ID="lblProjectName" AssociatedControlID="tbProjectName" Font-Bold="true">Project Name:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Project Name" ControlToValidate="tbProjectName" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="row noSpace">
        <div class="col s12">
        <i class="material-icons prefix">assignment</i>
        <asp:Label runat="server" ID="paySchoolLabel" ClientIDMode="Static" CssClass="cbHeader">Please check each of the categories you need:</asp:Label>
        </div>
    </div>

    <div class="row cbList extraSpace">

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="animation" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="animation">Animation</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="billBoard" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="billBoard">Billboard</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">    
        <asp:CheckBox ID="businessCard" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="businessCard">Business Cards</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="E_Blast" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="E_Blast">E-Blast</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="flyer" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="flyer">Flyer</asp:Label>
        </div>

       <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="landingPage" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="landingPage">Landing Page</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="filmCrew" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="filmCrew">Photographer/Film Crew</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">    
        <asp:CheckBox ID="postcards" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="postcards">Postcard</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="poster" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="poster">Poster</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="printAd" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="printAd">Print Ad</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="radioSpot" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="radioSpot">Radio Spot</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="socialMedia" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="socialMedia">Social Media</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="specialEvent" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="specialEvent">Special Event Material</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="story" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="story">Story Write Up</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="targetedAd" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="targetedAd">Targeted Digital Ad</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="tShirt" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="tShirt">T-Shirt Designs</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="tvCommercial" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="tvCommercial">TV Commercial</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="webBanner" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"   />
        <asp:Label runat="server" AssociatedControlID="webBanner">Web Banner</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="website" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"   />
        <asp:Label runat="server" AssociatedControlID="website">Web Change/Creation</asp:Label>
        </div>

        <div class="input-field selectIcon col s12 m6 l4">
        <asp:CheckBox ID="other" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="ddlHelp_SelectedIndexChanged"  />
        <asp:Label runat="server" AssociatedControlID="other">I'm not sure what I need</asp:Label>
        </div>

    </div>


</div><!-- End Row -->

</asp:Panel>


<asp:Panel runat="server" ID="pnlAnimation" CssClass="sectionGroup" GroupingText="Animation" Visible="false">

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">hourglass_full</i>   
        <asp:TextBox runat="server" ID="tbVideoLength"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbVideoLength">How long would you like the video to be?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Video Length" ControlToValidate="tbVideoLength" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">assignment</i>   
        <asp:TextBox runat="server" ID="tbVideoScript"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbVideoScript">Do you have a script for the video/voice over?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Video Script" ControlToValidate="tbVideoScript" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">wc</i>   
        <asp:TextBox runat="server" ID="tbVideoAudience"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbVideoAudience">Target Audience</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Video Audience" ControlToValidate="tbVideoAudience" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

   <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">webcam</i>   
        <asp:TextBox runat="server" ID="tbVideoUse"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbVideoUse">How will the video be used? (Facebook, website, at a presentation, etc)</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Video Use" ControlToValidate="tbVideoUse" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">
    
    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">video_library</i>   
        <asp:TextBox runat="server" ID="tbVideoExample"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbVideoExample">Do you have an example?(URL)</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Video Example" ControlToValidate="tbVideoExample" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12">
        <i class="material-icons prefix">comments</i>
        <asp:TextBox runat="server" ID="tbVideoInfo" TextMode="MultiLine" CssClass="materialize-textarea" ></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbVideoInfo" CssClass="optional">Please provide any other info you have about the animation.</asp:Label>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlBillboard" CssClass="sectionGroup" GroupingText="Bill Board" Visible="false">

<div class="row">

  <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlBillBoardLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="Label4" AssociatedControlID="ddlBillBoardLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlBillBoardLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">class</i>   
        <asp:TextBox runat="server" ID="tbBillBoardSchool"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbBillBoardSchool">What School/Program</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Billboard School/Program" ControlToValidate="tbBillBoardSchool" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbBillBoardTime"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbBillBoardTime">How long will it be up?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Billboard Time Up" ControlToValidate="tbBillBoardTime" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbBillboardDueDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbBillboardDueDate">Due Date</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Billboard Due Date" ControlToValidate="tbBillboardDueDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlBusinessCards" CssClass="sectionGroup" GroupingText="Business Cards" Visible="false">

<div class="row">

   <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlBusinessCardsLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlBusinessCardsLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlBusinessCardsLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>


    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">account_box</i>   
        <asp:TextBox runat="server" ID="tbBusinessCardsName"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbBusinessCardsName">Name</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Name" ControlToValidate="tbBusinessCardsName" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">account_circle</i>   
        <asp:TextBox runat="server" ID="tbBusinessCardsTitle"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbBusinessCardsTitle">Title</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Title" ControlToValidate="tbBusinessCardsTitle" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">mail</i>   
        <asp:TextBox runat="server" ID="tbBusinessCardsEmail"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbBusinessCardsEmail">E-mail Address</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Email Address" ControlToValidate="tbBusinessCardsEmail" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">city</i>   
        <asp:TextBox runat="server" ID="tbBusinessCardAddress"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbBusinessCardAddress">Address</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Address" ControlToValidate="tbBusinessCardAddress" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">phone</i>   
        <asp:TextBox runat="server" ID="tbBusinessCardsPhone1"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbBusinessCardsPhone1">Phone Numbers (Up To Five)</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Phone Number" ControlToValidate="tbBusinessCardsPhone1" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlEBlast" CssClass="sectionGroup" GroupingText="E-Blast" Visible="false">

<div class="row">

     <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlEBlastLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlEBlastLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlEBlastLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbEBlastSendDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbEBlastSendDate">When would you like it to go out?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: E-Blast Date" ControlToValidate="tbEBlastSendDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">
    
    <div class="input-field col s12"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbEBlastUse" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbEBlastUse">What would you like to promote/What is this for?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: E-Blast Promote" ControlToValidate="tbEBlastUse" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="tbEBlastAudeince" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Alumni</asp:ListItem>
            <asp:ListItem>Current Students</asp:ListItem>
            <asp:ListItem>Local Community</asp:ListItem>
            <asp:ListItem>Prospective Students/Leads</asp:ListItem>
            <asp:ListItem>Other</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="tbEBlastAudeince" Font-Bold="true">Who is your intended audience</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: E-Blast Audience" ControlToValidate="tbEBlastAudeince" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field topSpace col s12 m6 l6"> 
        <i class="material-icons prefix">assignment</i>
        <asp:Label runat="server" AssociatedControlID="tbEBlastList" CssClass="active">Do you have a list?</asp:Label>
        <div class="radio">
        <asp:RadioButtonList ID="tbEBlastList" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="tbEBlastList_SelectedIndexChanged" AutoPostBack="true" >
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbEBlastList" ErrorMessage="Required: List" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<asp:Panel runat="server" ID="pnlEBlastUpload" Visible="false">
<div class="row extraSpace">
    <div class="col s12">

    <p class="noTopSpace warning">Please upload your list in the upload field below.</p>

    </div>
</div>
</asp:Panel>

<asp:Panel runat="server" ID="pnlEBlastList" Visible="false">
<div class="row">
    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">assignment</i>   
        <asp:DropDownList runat="server" ID="ddlEBlastList" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Alumni</asp:ListItem>
            <asp:ListItem>Current Students</asp:ListItem>
            <asp:ListItem>Local Community</asp:ListItem>
            <asp:ListItem>Prospective Students/Leads</asp:ListItem>
            <asp:ListItem>Other</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlEBlastList" Font-Bold="true">How will the list be supplied?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: E-Blast List" ControlToValidate="ddlEBlastList" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>
</asp:Panel>

</asp:Panel>

<asp:Panel runat="server" ID="pnlFilmCrew" CssClass="sectionGroup" GroupingText="Film Crew" Visible="false">

<div class="row">

     <div class="input-field topSpace col s12 m6 l6"> 
        <i class="material-icons prefix">photo_camera</i>
        <asp:Label runat="server" AssociatedControlID="cbFilmCrewPhotographer" CssClass="active">Photographer or Film Crew</asp:Label>
        <div class="radio">
        <asp:RadioButtonList ID="cbFilmCrewPhotographer" runat="server" RepeatDirection="Horizontal" >
            <asp:ListItem>Photographer</asp:ListItem>
            <asp:ListItem>Film Crew</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="cbFilmCrewPhotographer" ErrorMessage="Photographer or Film Crew" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbFilmCrewReason"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbFilmCrewReason">Reason Needed</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Reason Needed" ControlToValidate="tbFilmCrewReason" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbFilmCrewDateNeeded"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbFilmCrewDateNeeded">Date Needed</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Date Needed" ControlToValidate="tbFilmCrewDateNeeded" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">watch_later</i>   
        <asp:TextBox runat="server" ID="tbFilmCrewTime"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbFilmCrewTime">How long are they needed? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Time Needed" ControlToValidate="tbFilmCrewTime" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlFlyer" CssClass="sectionGroup" GroupingText="Flyer" Visible="false">

<div class="row">

    <div class="col s12">
    <p class="collegeHeader noTopSpace">Size 8.5 x 11 inches</p>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">class</i>   
        <asp:TextBox runat="server" ID="tbFlyerEvent"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbFlyerEvent">What event/program</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Flyer event/program" ControlToValidate="tbFlyerEvent" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbFlyerDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbFlyerDate" CssClass="optional">Event Date</asp:Label>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">watch_later</i>   
        <asp:TextBox runat="server" ID="tbFlyerTime"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbFlyerTime" CssClass="optional">Time</asp:Label>
    </div>

        <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlFlyerLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="Label7" AssociatedControlID="ddlFlyerLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlFlyerLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">assessment</i>   
        <asp:TextBox runat="server" ID="tbFlyerQuantity"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbFlyerQuantity">Quantity</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Flyer Quantity" ControlToValidate="tbFlyerQuantity" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbPreferredDelivaryDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPreferredDelivaryDate">Preferred Delivery Date</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Flyer Preferred Delivery Date" ControlToValidate="tbPreferredDelivaryDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12">
        <i class="material-icons prefix">comments</i>
        <asp:TextBox runat="server" ID="tbFlyerDetailedInfo" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbFlyerDetailedInfo" Font-Bold="true">Detailed Information</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Flyer Detailed Information" ControlToValidate="tbFlyerDetailedInfo" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlLandingPage" CssClass="sectionGroup" GroupingText="Landing Page" Visible="false">

<div class="row">

    <div class="input-field topSpace col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>
        <asp:Label runat="server" AssociatedControlID="cbLandingPageEvent" CssClass="active">Is this page for an event?</asp:Label>
        <div class="radio">
        <asp:RadioButtonList ID="cbLandingPageEvent" runat="server" RepeatDirection="Horizontal" >
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="cbLandingPageEvent" ErrorMessage="Required: Landing Page Event?" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field topSpace col s12 m6 l6"> 
        <i class="material-icons prefix">web</i>
        <asp:Label runat="server" AssociatedControlID="cbLandingPageContact" CssClass="active">Do you need a contact form available on the page?</asp:Label>
        <div class="radio">
        <asp:RadioButtonList ID="cbLandingPageContact" runat="server" RepeatDirection="Horizontal" >
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="cbLandingPageContact" ErrorMessage="Required: Contact Form?" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlPoster" CssClass="sectionGroup" GroupingText="Poster" Visible="false">

<div class="row">

    <div class="col s12">
    <p class="collegeHeader noTopSpace">Size 11 x 17 inches</p>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">class</i>   
        <asp:TextBox runat="server" ID="tbPosterEvent"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPosterEvent">What event/program</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Poster event/program" ControlToValidate="tbPosterEvent" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbPosterDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPosterDate" CssClass="optional">Event Date</asp:Label>
    </div>

</div>


<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">watch_later</i>   
        <asp:TextBox runat="server" ID="tbPosterTime"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPosterTime" CssClass="optional">Time</asp:Label>
    </div>

        <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlPosterLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlPosterLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlPosterLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">assessment</i>   
        <asp:TextBox runat="server" ID="tbPosterQuantity"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPosterQuantity">Quantity</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Poster Quantity" ControlToValidate="tbPosterQuantity" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbPosterDeliveryDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPosterDate">Preferred Delivery Date</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Poster Preferred Delivery Date" ControlToValidate="tbPosterDeliveryDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12">
        <i class="material-icons prefix">comments</i>
        <asp:TextBox runat="server" ID="tbPosterDetailedInfo" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPosterDetailedInfo" Font-Bold="true">Detailed Information</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Poster Detailed Information" ControlToValidate="tbPosterDetailedInfo" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlPostcards" CssClass="sectionGroup" GroupingText="Postcard" Visible="false">

<div class="row">

    <div class="col s12">
    <p class="collegeHeader noTopSpace">Size 6 x 9 inches</p>
    </div>

    <div class="input-field col s12"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbPostcardPurpose" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPostcardPurpose">What is the postcard for?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Postcard Purpose" ControlToValidate="tbPostcardPurpose" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

     <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">city</i>   
        <asp:TextBox runat="server" ID="tbPostcardTo"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPostcardTo">Who will be receiving this postcard?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Receiving Postcard" ControlToValidate="tbPostcardTo" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

     <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">markunread_mailbox</i>   
        <asp:TextBox runat="server" ID="tbPostcardArrival"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPostcardArrival">When should it be in mailboxes?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Delivery Date" ControlToValidate="tbPostcardArrival" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

     <div class="input-field topSpace col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>
        <asp:Label runat="server" AssociatedControlID="tbPostcardEvent" CssClass="active">Is this postcard for an event?</asp:Label>
        <div class="radio">
        <asp:RadioButtonList ID="tbPostcardEvent" runat="server" RepeatDirection="Horizontal" >
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPostcardEvent" ErrorMessage="Required: Posrcard event" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <asp:Panel runat="server" ID="pnlPostcardEvent">

       <div class="input-field col s12 m6 l6"> 
            <i class="material-icons prefix">event</i>   
            <asp:TextBox runat="server" ID="tbPostcardEventDate"></asp:TextBox>
            <asp:Label runat="server" AssociatedControlID="tbPostcardEventDate">Event Date</asp:Label>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Postcard Event Date" ControlToValidate="tbPostcardEventDate" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

        <div class="input-field col s12 m6 l6"> 
            <i class="material-icons prefix">time</i>   
            <asp:TextBox runat="server" ID="tbPostcardsEventTime"></asp:TextBox>
            <asp:Label runat="server" AssociatedControlID="tbPostcardsEventTime">Event Time</asp:Label>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Postcard Event Time" ControlToValidate="tbPostcardsEventTime" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

   <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlPostcardsEventLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="Label9" AssociatedControlID="ddlPostcardsEventLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlPostcardsEventLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field topSpace col s12 m6 l6"> 
        <i class="material-icons prefix">assignment</i>
        <asp:Label runat="server" AssociatedControlID="rblPostcardsPL" CssClass="active">Would you like a purchased list?</asp:Label>
        <div class="radio">
        <asp:RadioButtonList ID="rblPostcardsPL" runat="server" RepeatDirection="Horizontal" >
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="rblPostcardsPL" ErrorMessage="Required: Postcard Purchased List" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    </asp:Panel>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbPostcardDetails"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPostcardDetails">Detailed Information</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Postcard Details" ControlToValidate="tbPostcardDetails" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row extraSpace">
    <div class="col s12">

    <p class="noTopSpace warning">Please upload any additional documents you have for this project in the upload field below.</p>

    </div>
</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlPrintAd" CssClass="sectionGroup" GroupingText="Print Ad" Visible="false">

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">account_box</i>   
        <asp:TextBox runat="server" ID="tbPrintPublication"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPrintPublication">Name of Publication</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Name of Publication" ControlToValidate="tbPrintPublication" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">photo_size_select_large</i>   
        <asp:TextBox runat="server" ID="tbPrintAdSize"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPrintAdSize">Ad Size</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Ad Size" ControlToValidate="tbPrintAdSize" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

     <div class="input-field topSpace col s12 m6 l6"> 
        <i class="material-icons prefix">invert_colors</i>
        <asp:Label runat="server" AssociatedControlID="cbPrintColor" CssClass="active">Color or Black and White</asp:Label>
        <div class="radio">
        <asp:RadioButtonList ID="cbPrintColor" runat="server" RepeatDirection="Horizontal" >
            <asp:ListItem>Color</asp:ListItem>
            <asp:ListItem>Black and White</asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="cbPrintColor" ErrorMessage="Required: Color or Black and White" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbPrintDueDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbPrintDueDate">Due Date</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Print Due Date" ControlToValidate="tbPrintDueDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field selectIcon col s12 m6 l6"> 
    <i class="material-icons prefix">account_balance</i>   
    <asp:DropDownList runat="server" ID="tbPrintSchool" >
        <asp:ListItem Value="00">Please Select:</asp:ListItem>
        <asp:ListItem >Sullivan Louisville</asp:ListItem>
        <asp:ListItem >Sullivan Lexington</asp:ListItem>
        <asp:ListItem >Sullivan Fort Knox</asp:ListItem>
        <asp:ListItem >Sullivan Louisa Center for Learning</asp:ListItem>
        <asp:ListItem >Sullivan Northern Kentucky</asp:ListItem>
        <asp:ListItem >SCTD</asp:ListItem>
        <asp:ListItem >Spencerian Louisville</asp:ListItem>
        <asp:ListItem >Spencerian Lexington</asp:ListItem>
    </asp:DropDownList>
    <asp:Label runat="server" AssociatedControlID="tbPrintSchool" Font-Bold="true">Which School/Campus</asp:Label>
    <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: School/Campus" ControlToValidate="tbPrintSchool" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlRadioSpot" CssClass="sectionGroup" GroupingText="Radio Spot" Visible="false">

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">radio</i>   
        <asp:TextBox runat="server" ID="tbRadio"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbRadio">What area and station?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What area and station" ControlToValidate="tbRadio" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">account_balance</i>
        <asp:DropDownList runat="server" ID="ddlRadioSchool"  >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem >Sullivan Louisville</asp:ListItem>
            <asp:ListItem >Sullivan Lexington</asp:ListItem>
            <asp:ListItem >Sullivan Fort Knox</asp:ListItem>
            <asp:ListItem >Sullivan Louisa Center for Learning</asp:ListItem>
            <asp:ListItem >Sullivan Northern Kentucky</asp:ListItem>
            <asp:ListItem >SCTD</asp:ListItem>
            <asp:ListItem >Spencerian Louisville</asp:ListItem>
            <asp:ListItem >Spencerian Lexington</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="Label1" AssociatedControlID="ddlRadioSchool" Font-Bold="true">Which School</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Which School" ControlToValidate="ddlRadioSchool" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">comments</i>
        <asp:DropDownList runat="server" ID="ddlRadioPurpose" AutoPostBack="true" OnSelectedIndexChanged="ddlRadioPurpose_SelectedIndexChanged"  >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem >Get the Name Out</asp:ListItem>
            <asp:ListItem>Quarter Start</asp:ListItem>
            <asp:ListItem >Specific Program</asp:ListItem>
            <asp:ListItem >Upcoming Event</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="Label2" AssociatedControlID="ddlRadioPurpose" Font-Bold="true">What is its purpose</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What is the purpose" ControlToValidate="ddlRadioPurpose" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <asp:Panel runat="server" ID="pnlRadioProgram" Visible="false">

        <div class="input-field col s12 m6 l6"> 
            <i class="material-icons prefix">class</i>   
            <asp:TextBox runat="server" ID="tbRadioProgram"></asp:TextBox>
            <asp:Label runat="server" AssociatedControlID="tbRadioProgram">Which Program?</asp:Label>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Which Program" ControlToValidate="tbRadioProgram" Display="Dynamic">
            </asp:RequiredFieldValidator>
        </div>

    </asp:Panel>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbRadioDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbRadioDate">Preferred Due Date</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Preferred Due Date" ControlToValidate="tbRadioDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlSpecialEvent" CssClass="sectionGroup" GroupingText="Special Event Material" Visible="false">

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">account_box</i>   
        <asp:TextBox runat="server" ID="tbSpecialEventName"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbSpecialEventName">What is this event called?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Name" ControlToValidate="tbSpecialEventName" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbSpecialEventPurpose"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbSpecialEventPurpose">What is the purpose of the event?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Purpose" ControlToValidate="tbSpecialEventPurpose" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

     <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbSpecialEventDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbSpecialEventDate">Date of Event</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Date" ControlToValidate="tbSpecialEventDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">watch_later</i>   
        <asp:TextBox runat="server" ID="tbSpecialEventTime"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbSpecialEventTime">Time of Event</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Time" ControlToValidate="tbSpecialEventTime" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

     <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbSpecialEventNeed"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbSpecialEventNeed">What do you need?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Items Needed" ControlToValidate="tbSpecialEventNeed" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbSpecialEventDueDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbSpecialEventDueDate">Preferred Due Date</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Event Preferred Due Date" ControlToValidate="tbSpecialEventDueDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlStory" CssClass="sectionGroup" GroupingText="Story Write Up" Visible="false">

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbStoryAbout"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbStoryAbout">What is the story about?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What is story about" ControlToValidate="tbStoryAbout" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">description</i>   
        <asp:TextBox runat="server" ID="tbStoryFocus"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbStoryFocus">What needs to be the focus?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Story Focus" ControlToValidate="tbStoryFocus" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbStoryDueDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbStoryDueDate">Prefered Due Date</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Prefered Due Date" ControlToValidate="tbStoryDueDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlTargetedAd" CssClass="sectionGroup" GroupingText="Targeted Digital Ad" Visible="false">

<div class="row">

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlTargetedAdLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlTargetedAdLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlTargetedAdLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">class</i>   
        <asp:TextBox runat="server" ID="tbTargetedAdProgram"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTargetedAdProgram">Deparment/Program</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Targetd Ad Program" ControlToValidate="tbTargetedAdProgram" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">assignment</i>   
        <asp:TextBox runat="server" ID="tbTargetedAdPromote"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTargetedAdPromote">What would you like to promote?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Targeted ad promote" ControlToValidate="tbTargetedAdPromote" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbTargetedAdEndDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTargetedAdEndDate">What is the end date for this campaign?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Targeted ad end date" ControlToValidate="tbTargetedAdEndDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">wc</i>   
        <asp:DropDownList runat="server" ID="ddlTargtedAdAudience" AutoPostBack="true" OnSelectedIndexChanged="ddlTargtedAdAudience_SelectedIndexChanged" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Prospective Students/Leads</asp:ListItem>
            <asp:ListItem>Current Students</asp:ListItem>
            <asp:ListItem>Alumni</asp:ListItem>
            <asp:ListItem>Local Community</asp:ListItem>
            <asp:ListItem>Other</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlTargtedAdAudience" Font-Bold="true">Who is your intended audience?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Audience" ControlToValidate="ddlTargtedAdAudience" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <asp:Panel runat="server" ID="pnlTargtedAdOther" Visible="false">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbTargetedAdAudienceOther"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTargetedAdAudienceOther">Intended Audience Details</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Targeted ad other" ControlToValidate="tbTargetedAdAudienceOther" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    </asp:Panel>

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">assignment</i>   
        <asp:DropDownList runat="server" ID="ddlTargetedAdList" AutoPostBack="true" OnSelectedIndexChanged="ddlTargetedAdList_SelectedIndexChanged" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlTargetedAdList" Font-Bold="true">Do you have a list for this audience?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Audience" ControlToValidate="ddlTargetedAdList" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <asp:Panel runat="server" ID="pnlTargetedAdListNoList" Visible="false">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">wc</i>   
        <asp:TextBox runat="server" ID="tbTargetedAdDemographic"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTargetedAdDemographic">What status, demographic, or interest criteria should this audience meet? </asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Targeted ad demographic" ControlToValidate="tbTargetedAdDemographic" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    </asp:Panel>

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">assignment</i>   
        <asp:DropDownList runat="server" ID="ddlTargtedAdGoal" AutoPostBack="true" OnSelectedIndexChanged="ddlTargtedAdGoal_SelectedIndexChanged">
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem>Awareness</asp:ListItem>
            <asp:ListItem>Event Attendance</asp:ListItem>
            <asp:ListItem>Inquiries</asp:ListItem>
            <asp:ListItem>Lead Generation</asp:ListItem>
            <asp:ListItem>Offer Claims</asp:ListItem>
            <asp:ListItem>Webpage Visits</asp:ListItem>
            <asp:ListItem>Other</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlTargtedAdGoal" Font-Bold="true">What kind of results are you hoping for?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Audience" ControlToValidate="ddlTargtedAdGoal" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <asp:Panel runat="server" ID="pnlTargtedAdGoalOther" Visible="false">

     <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbTargetedAdGoal"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTargetedAdGoal">Goal</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Targeted Ad Goal" ControlToValidate="tbTargetedAdGoal" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    </asp:Panel>

</div>

<div class="row">

    <div class="input-field col s12"> 
        <i class="material-icons prefix">comments</i>   
        <asp:TextBox runat="server" ID="tbTargtedAdMoreInfo" TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTargtedAdMoreInfo" CssClass="optional">Anything else we should know about this campaign?</asp:Label>
    </div>

</div>


    <asp:Panel runat="server" ID="pnlTargetedAdListYesList" Visible="false">

        <div class="row extraSpace">
        <div class="col s12">

        <p class="noTopSpace warning">Please upload your list in an Excel or .csv file below. Be sure to include as much of the following info as possible: 
            first name, last name, gender, email address(es), phone number(s), date of birth, city, state, & zip code. </p>

        </div>
        </div>

    </asp:Panel>


</asp:Panel>

<asp:Panel runat="server" ID="pnlTShirt" CssClass="sectionGroup" GroupingText="T-Shirt Designs" Visible="false">

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:TextBox runat="server" ID="tbTShirtSchool"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTShirtSchool">What school?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What school" ControlToValidate="tbTShirtSchool" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">class</i>   
        <asp:TextBox runat="server" ID="tbTShirtProgram"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTShirtProgram">What program/event</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Program/Event" ControlToValidate="tbTShirtProgram" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:TextBox runat="server" ID="tbTShirtLook"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTShirtLook">How would you like it to look?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: T-Shirt appearance" ControlToValidate="tbTShirtLook" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">file_upload</i>
        <asp:DropDownList runat="server" ID="ddlTShirtDesign"  >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem >Design on Front</asp:ListItem>
            <asp:ListItem >Design on Back</asp:ListItem>
            <asp:ListItem >Design on Front and Back</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="Label3" AssociatedControlID="ddlTShirtDesign" Font-Bold="true">Where would you like the design?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Design Location" ControlToValidate="ddlTShirtDesign" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">wallpaper</i>   
        <asp:TextBox runat="server" ID="tbTShirtColor"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTShirtColor">What color t-shirt?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: T-Shirt color" ControlToValidate="tbTShirtColor" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">font_download</i>   
        <asp:TextBox runat="server" ID="tbTShirtColorAmount"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTShirtColorAmount">How many color for the print?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: How many colors for print" ControlToValidate="tbTShirtColorAmount" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">text_format</i>   
        <asp:TextBox runat="server" ID="tbTShirtColorPrint"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTShirtColorPrint">What color print?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: What color print?" ControlToValidate="tbTShirtColorPrint" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">event</i>   
        <asp:TextBox runat="server" ID="tbTShirtDueDate"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTShirtDueDate">Preferred Due Date</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Preferred Due Date" ControlToValidate="tbTShirtDueDate" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlTvCommercial" CssClass="sectionGroup" GroupingText="TV Commercial" Visible="false">

<div class="row">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">tv</i>   
        <asp:TextBox runat="server" ID="tbTVArea"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTVArea">What area and station?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Area and Station" ControlToValidate="tbTVArea" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">account_balance</i>
        <asp:DropDownList runat="server" ID="ddlTVSchool"  >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem >Sullivan Louisville</asp:ListItem>
            <asp:ListItem >Sullivan Lexington</asp:ListItem>
            <asp:ListItem >Sullivan Fort Knox</asp:ListItem>
            <asp:ListItem >Sullivan Louisa Center for Learning</asp:ListItem>
            <asp:ListItem >Sullivan Northern Kentucky</asp:ListItem>
            <asp:ListItem >SCTD</asp:ListItem>
            <asp:ListItem >Spencerian Louisville</asp:ListItem>
            <asp:ListItem >Spencerian Lexington</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlTVSchool" Font-Bold="true">Which School</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Which School" ControlToValidate="ddlTVSchool" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

</div>

<div class="row">

     <div class="input-field selectIcon col s12 m6 l6">
        <i class="material-icons prefix">file_upload</i>
        <asp:DropDownList runat="server" ID="ddlPurpose" AutoPostBack="true" OnSelectedIndexChanged="ddlPurpose_SelectedIndexChanged">
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
            <asp:ListItem >Get the name out</asp:ListItem>
            <asp:ListItem >Upcoming Event</asp:ListItem>
            <asp:ListItem >Specific Program</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="Label5" AssociatedControlID="ddlPurpose" Font-Bold="true">What is the purpose?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Which School" ControlToValidate="ddlPurpose" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <asp:Panel runat="server" ID="pnlTVProgram" Visible="false">

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">class</i>   
        <asp:TextBox runat="server" ID="tbTVProgram"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbTVProgram">Which Program?</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Program" ControlToValidate="tbTVProgram" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

    </asp:Panel>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlWebBanner" CssClass="sectionGroup" GroupingText="Web Banner Info" Visible="false">

<div class="row">

    <div class="input-field selectIcon col s12 m6 l6"> 
        <i class="material-icons prefix">account_balance</i>   
        <asp:DropDownList runat="server" ID="ddlWebBannerLocation" AppendDataBoundItems="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="locationID" >
            <asp:ListItem Value="00">Please Select:</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" AssociatedControlID="ddlWebBannerLocation" Font-Bold="true">Location</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Location" ControlToValidate="ddlWebBannerLocation" InitialValue="00" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>

    <div class="input-field col s12 m6 l6"> 
        <i class="material-icons prefix">store</i>   
        <asp:TextBox runat="server" ID="tbWebBannerFor"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="tbWebBannerFor">Who is this for? (IE El Toro, Pandora)</asp:Label>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required: Area and Station" ControlToValidate="tbWebBannerFor" Display="Dynamic">
        </asp:RequiredFieldValidator>
    </div>

</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlWebChange" CssClass="sectionGroup" GroupingText="Web Change Info" Visible="false">

<div class="row">

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

<div class="row extraSpace">
    <div class="col s12">

    <p class="noTopSpace warning">Please update any additional documents you have for this project in the upload field below.</p>

    </div>
</div>

</asp:Panel>

<asp:Panel runat="server" ID="pnlOther" CssClass="sectionGroup" GroupingText="I'm not sure what I need" Visible="false">

<div class="row">

    <div class="col s12 extraSpace">
    <p class="noTopSpace confirmationText">Please contat <a href="mailTo:snallen@sullivan.edu">Sarah Allen</a></p>
    </div>

</div>

</asp:Panel>


<asp:Panel runat="server" ID="pnlAttachments" CssClass="sectionGroup" GroupingText="Additional Information" Visible="false">
<br />
<div class="row extraSpace">

    <div class="input-field col s12">
        <i class="material-icons prefix">description</i>
        <asp:TextBox runat="server" ID="tbAdditionalInfo" TextMode="MultiLine" CssClass="materialize-textarea" ></asp:TextBox>
        <asp:Label runat="server" ID="lblAdditionalInfo" AssociatedControlID="tbAdditionalInfo" CssClass="optional">Please include additional information for this project, such as details that will be needed for the completion of the project. For bulleted lists, please use the upload field below. </asp:Label>
    </div>

</div>

<div class="row upload">

    <div class="col s6 extraSpace">
        <asp:Label runat="server" CssClass="uploadLabel"  ><b>Please upload any additional information for this project, such as details that will be needed for the completion of the project.</b></asp:Label>
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



</asp:Panel>


<div class="right-align">
    <asp:Button runat="server" ID="btnSubmit" Text="Submit Project Request" onclick="btnSubmit_Click" CssClass="btn waves-effect" />
</div>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ProjectRequestConnectionString %>" 
    SelectCommand="SELECT [locationID], [name] FROM [Location] ORDER BY [name]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ProjectRequestConnectionString %>" 
    SelectCommand="SELECT [department] FROM [Departments] ORDER BY [department]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ProjectRequestConnectionString %>" 
    SelectCommand="SELECT [publication], [vendorID] FROM [Vendor] ORDER BY [publication]">
</asp:SqlDataSource>

</asp:Content>