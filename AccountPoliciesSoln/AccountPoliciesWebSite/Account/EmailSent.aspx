<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmailSent.aspx.cs" Inherits="Account_EmailSent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        An email has been sent to the given email address
    </h2>
    <p>
        We have sent you an email with instructions on how to enable your account.
    </p>
    <p>
        Please use your current device to open the email and click on the link.
    </p>
    <p>
        Please note that the link is only valid for the next 10 minutes
    </p>
    <p>
        Thank you.
    </p>
</asp:Content>

