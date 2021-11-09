<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="MemShare.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Container-sm/OverallStyleSheet.css" rel="stylesheet" />

</head>
<body>
    <form id="frmSignUp" runat="server">
        <div class="background_box">
            <asp:Label ID="lblError" runat="server" BackColor="Red" ForeColor="#FFD909" style="z-index: 1; left: calc(47% - 47px); top: calc(20px/2); position: absolute"></asp:Label>
            <img src="Images/Login.png" alt="Alternate Text" class="signUpLogo"/>
            <h2>Sign Up Here</h2>
            <asp:Label ID="lblInstructions" Text="Please fill in the fields below (* are required)" style="font-size: calc(5px + (18-5) * ((100vw - 300px) / (1600 - 300))); left: calc(35% - 35px); top: calc(15% - 15px); position:absolute;" runat="server"></asp:Label>
            <asp:Label Text="Name:*" style="left: calc(20% - 20px); top: calc(25% - 25px); position: absolute;" CssClass="lblName" runat="server" />
            <asp:TextBox ID="txtName" runat="server" style="left: calc(19.75% - 19.75px); top: calc(27% - 27px); width:calc(20% - 20px); position: absolute;" CssClass="txtName" placeholder="Enter Name"/>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" style="left: calc(25% - 25px); top: calc(25% - 25px);" ErrorMessage="Please provide your name" ControlToValidate="txtName" CssClass="validators" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:Label Text="Surname:*" style="left: calc(65% - 65px); top: calc(25% - 25px); position: absolute;" CssClass="lblName" runat="server" />
            <asp:TextBox ID="txtSurname" style="left: calc(65% - 65px); top: calc(27% - 27px); width:calc(20% - 20px); position: absolute;" runat="server" CssClass="txtName" placeholder="Enter Surname"/>
            <asp:RequiredFieldValidator ID="rfvSurname" runat="server" style="left: calc(72% - 72px); top: calc(25% - 25px);" ErrorMessage="Please provide your surname" ControlToValidate="txtSurname" CssClass="validators" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:Label Text="Email:*" style="left: calc(65% - 65px); top: calc(42% - 42px); position: absolute;" CssClass="lblEmail" runat="server" />
            <asp:TextBox ID="txtEmail" runat="server" style="left: calc(64.75% - 64.75px); top: calc(44% - 44px); width:calc(20% - 20px); position: absolute;" CssClass="txtEmail" placeholder="Enter Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" style="left: calc(70% - 70px); top: calc(42% - 42px);" ErrorMessage="Please provide your email" ControlToValidate="txtEmail" CssClass="validators" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Invalid Email Format" ControlToValidate="txtEmail" style="left: calc(70% - 70px); top: calc(42% - 42px); position: absolute;" CssClass="validators" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True"></asp:RegularExpressionValidator>
            <asp:Label Text="Password:*" style="left: calc(43% - 43px); top: calc(60% - 60px); position: absolute;" CssClass="lblPassword" runat="server" />
            <asp:TextBox ID="txtPassword" style="left: calc(43% - 43px); top: calc(62% - 62px); width:calc(20% - 20px); position: absolute;" runat="server" CssClass="txtPassword" placeholder="********" TextMode="Password"/>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" style="left: calc(51% - 51px); top: calc(60% - 60px);" ErrorMessage="Please provide password" ControlToValidate="txtPassword" CssClass="validators" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:Label ID="lblCaptcha" runat="server" AssociatedControlID="CaptchaCode" style="left: calc(43% - 43px); top: calc(75% - 75px); position: absolute;">Retype the characters from the picture:</asp:Label>
            <BotDetect:WebFormsCaptcha ID="msgCaptcha" UserInputID="CaptchaCode" runat="server" style="left: calc(43% - 43px); top: calc(78% - 78px); position: absolute;"/>
            <asp:TextBox ID="CaptchaCode" runat="server" style="left: calc(47.5% - 47.5px); top: calc(85% - 85px); position: absolute;"></asp:TextBox>
            <asp:Label ID="lblCaptchaError" runat="server" style="left: calc(47.5% - 47.5px); top: calc(88% - 88px); position: absolute;" ForeColor="#EC0000"/>
            <asp:Button ID="btnSubmit" Text="Sign Up" style="left: calc(34% - 34px); top: calc(93% - 93px); width:calc(40% - 40px); position: absolute;" CssClass="btnSubmit" runat="server" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" Text="Cancel" CausesValidation="false" style="left: calc(34% - 34px); top: calc(100% - 100px); width:calc(40% - 40px); position: absolute;" CssClass="btnSubmit" runat="server" OnClick="btnCancel_Click" />
        </div>
    </form>
</body>
</html>
