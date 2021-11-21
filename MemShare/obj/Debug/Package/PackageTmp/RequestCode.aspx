<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestCode.aspx.cs" Inherits="MemShare.RequestCode" %>

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
    <div class="loginbox">
        <form runat="server">
            &nbsp;
            <img src="Images/Login.png" alt="Alternate Text" class="logo" />
            <h2>Reset Password</h2>
            <asp:Label Text="Email" CssClass="lblEmail" runat="server" />
            <asp:Label ID="lblNotExist" CssClass="validation" runat="server" BackColor="Red" ForeColor="#FFD909"/>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtEmail" placeholder="Enter Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" style="left: 89px; top: 126px;" ErrorMessage="Please provide your email" ControlToValidate="txtEmail" CssClass="validators" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ErrorMessage="Invalid Email Format" ControlToValidate="txtEmail" style="left: 89px; top: 126px;" CssClass="validators" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True"></asp:RegularExpressionValidator>
            <asp:Button ID="btnSendCode" Text="Send Code" CssClass="btnSubmit" runat="server" OnClick="btnSendCode_Click" />
            <asp:Button ID="btnCancel" Text="Back" CssClass="btnSubmit" runat="server" CausesValidation="false" OnClick="btnCancel_Click" />
        </form>
    </div>
</body>
</html>
