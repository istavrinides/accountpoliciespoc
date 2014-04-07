<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="VerifyAccount.aspx.cs" Inherits="Account_VerifyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        <asp:Label ID="lblCannotVerify" runat="server" Visible="false">
        The verification link has expired or cannot verify the request. Please try registering again.
        </asp:Label>
        <asp:Label ID="lblVerified" runat="server" Visible="false">
        Request verified. Please provide the below details.
        </asp:Label>
    </h2>
    <asp:Panel runat="server" ID="pnlAdditonalInfo" Visible="false">
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
        </span>
        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
            ValidationGroup="RegisterUserValidationGroup" />
        <div class="accountInfo">
            <fieldset class="register">
                <legend>Account Information</legend>
                <p>
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                    <asp:TextBox ID="Password" runat="server" CssClass="textEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="RepeatPasswordLabel" runat="server" AssociatedControlID="RepeatPassword">Repeat Password:</asp:Label>
                    <asp:TextBox ID="RepeatPassword" runat="server" CssClass="textEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RepeatPasswordRequired" runat="server" ControlToValidate="Password"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="RepeatPasswordCompanre" runat="server" ControlToCompare="Password"
                        ControlToValidate="RepeatPassword" CssClass="failureNotification" ErrorMessage="Passwords must match."
                        ToolTip="Passwords must match." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                </p>
            </fieldset>
            <p class="submitButton">
                <asp:Button ID="CreateUserButton" runat="server" Text="Create Account" ValidationGroup="RegisterUserValidationGroup"
                    OnClick="CreateUserButton_Click" />
            </p>
        </div>
    </asp:Panel>
</asp:Content>
