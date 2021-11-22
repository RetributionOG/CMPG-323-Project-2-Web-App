<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="MemShare.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Container-sm/OverallStyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .btnSign {
            z-index: 1;
            left: 41px;
            top: 381px;
            position: absolute;
        }
    </style>
</head>
<body>
    <div class="resetPassword">
        <form runat="server">
            &nbsp;
            <asp:Label ID="lblError" runat="server" BackColor="Red" ForeColor="#FFD909" style="z-index: 1; left: 70px; top: -110px; position: absolute"></asp:Label>
            <img src="Images/Login.png" alt="Alternate Text" class="logo" />
            <h2>New Password</h2>          
            <asp:Label Text="Password:" CssClass="lblPassword" runat="server" />
            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtPassword" placeholder="********" TextMode="Password"/>
            <asp:Label Text="Confirm Password:" CssClass="lblPassword" runat="server" />
            <asp:TextBox ID="txtPasswordConfirm" runat="server" CssClass="txtPassword" placeholder="********" TextMode="Password"/>
            <asp:Button ID="btnResetPassword" Text="Reset" CssClass="btnSubmit" runat="server" OnClick="btnResetPassword_Click" />
            <asp:Button ID="btnCancel" Text="Back" CssClass="btnSubmit" runat="server" CausesValidation="false" OnClick="btnCancel_Click" />
        </form>
    </div>
</body>
</html>
