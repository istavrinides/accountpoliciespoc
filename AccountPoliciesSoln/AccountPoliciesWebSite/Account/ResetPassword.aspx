<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="Account_ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        <asp:Label ID="lblCannotVerify" runat="server" Visible="false">
        The password reset link has expired or cannot verify the request. Please try requesting a password reset again.
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
            ValidationGroup="ResetPasswordValidationGroup" />
        <div class="accountInfo">
            <fieldset class="register">
                <legend>Account Information</legend>
                <p>
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                    <asp:TextBox ID="Password" runat="server" CssClass="textEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                        ValidationGroup="ResetPasswordValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="RepeatPasswordLabel" runat="server" AssociatedControlID="RepeatPassword">Repeat Password:</asp:Label>
                    <asp:TextBox ID="RepeatPassword" runat="server" CssClass="textEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RepeatPasswordRequired" runat="server" ControlToValidate="Password"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                        ValidationGroup="ResetPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="RepeatPasswordCompanre" runat="server" ControlToCompare="Password"
                        ControlToValidate="RepeatPassword" CssClass="failureNotification" ErrorMessage="Passwords must match."
                        ToolTip="Passwords must match." ValidationGroup="ResetPasswordValidationGroup">*</asp:CompareValidator>
                </p>
            </fieldset>
            <p class="submitButton">
                <asp:Button ID="ResetPasswordButton" runat="server" Text="Reset Password" 
                    ValidationGroup="ResetPasswordValidationGroup" onclick="ResetPasswordButton_Click"
                     />
            </p>
        </div>
    </asp:Panel>
</asp:Content>

