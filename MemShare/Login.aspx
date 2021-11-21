﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MemShare.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Container-sm/OverallStyleSheet.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .btnSign {
            z-index: 1;
            left: 41px;
            top: 381px;
            position: absolute;

        }
        .auto-style1 {
            position: absolute;
            color: #ff0000;
            left: 88px;
            top: 155px;
        }
        .auto-style2 {
            position: absolute;
            color: #ff0000;
            left: 90px;
            top: 155px;
        }
    </style>
</head>
<body>
    <div class="loginbox">
        <form runat="server">
            &nbsp;
            <asp:Label ID="lblError" runat="server" BackColor="Red" ForeColor="#FFD909" style="z-index: 1; left: 63px; top: -120px; position: absolute"></asp:Label>
            &nbsp;<img src="Images/Login.png" alt="Alternate Text" class="logo" /><h2>Welcome to MemShare. Please log in</h2>
            <asp:Label Text="Email" CssClass="lblEmail" runat="server" />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtEmail" placeholder="Enter Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please provide your email" ControlToValidate="txtEmail" CssClass="auto-style1" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Invalid Email Format" ControlToValidate="txtEmail" CssClass="auto-style2" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True"></asp:RegularExpressionValidator>
            <asp:Label Text="Password" CssClass="lblPassword" runat="server" />
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please provide password" ControlToValidate="txtPassword" CssClass="validators" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtPassword" placeholder="********" TextMode="Password"/>
            <asp:Button ID="btnSubmit" Text="Sign In" CssClass="btnSubmit" runat="server" OnClick="btnSubmit_Click" />
            <asp:LinkButton Text="Forgot Password?" CssClass="btnForget" runat="server" OnClick="btnForgotPassword_Click" CausesValidation="false"/>
            <asp:LinkButton Text="Create New Account" CssClass="btnForget" runat="server" OnClick="btnSignup_Click" CausesValidation="false"/>
        </form>
    </div>
</body>
</html>
