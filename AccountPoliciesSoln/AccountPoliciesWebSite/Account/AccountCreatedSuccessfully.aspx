<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AccountCreatedSuccessfully.aspx.cs" Inherits="Account_AccountCreatedSuccessfully" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        <asp:Label ID="lblCannotVerify" runat="server">
        The account was created successfully. You are logged on and can use the system.
        </asp:Label>
    </h2>
</asp:Content>
