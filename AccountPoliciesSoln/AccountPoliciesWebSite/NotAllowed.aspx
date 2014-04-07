<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NotAllowed.aspx.cs" Inherits="NotAllowed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        You are not allowed to use the system.
    </h2>
    <p>
        Your IP has been blacklisted by our system.
    </p>
    <p>
        If you believe this is in error, please contact us requesting to remove your IP
        from our blacklist.
    </p>
    <p>
        Please include the below IP address in your email with your username.
    </p>
    <p>
        <h1>
            <asp:Label ID="lblIPAddress" runat="server" Font-Bold="true"/>
        </h1>
    </p>
    <p>
        Thank you
    </p>
</asp:Content>
