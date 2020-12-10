<%@ Page Title="" Language="C#" MasterPageFile="~/MaterialDesign.master" AutoEventWireup="true" CodeBehind="RequestFormConfirm.aspx.cs" Inherits="ProjectRequest.RequestFormConfirm" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="myHead" ContentPlaceHolderID="head" runat="server">
    <title>Creative Communications Project Request</title>
</asp:Content>


<asp:Content ID="myHeader"  ContentPlaceHolderID="header" runat="server">
      Creative Communications Project Request
</asp:Content>

<asp:Content ID="myContent" ContentPlaceHolderID="content" runat="server">

<asp:Panel runat="server" CssClass="sectionGroup" GroupingText="Thank You">
<div class="row extraSpace">
<div class="col s12">
    <p class="noTopSpace">
        Your Project Request has been submitted.
    </p>

    <p>
        <%=approvalMessage %>
    </p>

    <p>
        You’ll be notified once a decision has been made.
    </p>

    <p><b><a href='https://secure.sullivan.edu/ProjectRequest/ProjectRequest/User/MyRequests'>View My Projects Requests</a></b></p>
    
</div>
</div>

</asp:Panel>

</asp:Content>