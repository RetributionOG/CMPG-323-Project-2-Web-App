<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MemShare.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .roundedcorner{
            font-size:11pt;
            margin-left:auto;
            margin-right:auto;
            margin-top:1px;
            margin-bottom:1px;
            padding:3px;
            border-top:1px solid;
            border-left:1px solid;
            border-right:1px solid;
            border-bottom:1px solid;
            -moz-border-radius:8px;
            -webkit-border-radius:8px;
        }
        .background {
            background-color:black;
            /*filter:alpha(opacity = 90);*/
            opacity: 0.8;


        }
        .popup{
            background-color: aqua;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top:10px;
            padding-left:10px;
            width:400px;
            height:300px;


        }
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 527px;
        }
        .auto-style3 {
            width: 241px;
        }
        </style>
    <title></title>
    </head>
<body style="height: 587px; width: 1309px">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblWelcome" runat="server" Text="Welcome"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnViewPhotos" runat="server" Text="View photos" OnClick="btnViewPhotos_Click" />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAddPhoto" runat="server" CssClass="roundedcorner" Font-Size="Small" Text="Add photo" OnClick="btnAddPhoto_Click1" Height="32px" Width="121px" />
                    <asp:Button ID="btnAlbums" runat="server" OnClick="btnAlbums_Click" Text="View Albums" />
                </td>
                <td class="auto-style3">&nbsp;&nbsp;
                    <asp:Button ID="btnViewMetaData" runat="server" OnClick="btnViewMetaData_Click" Text="View Meta data" />
                </td>
                <td>
                    <asp:Button ID="btnDeleteMetaData" runat="server" OnClick="btnDeleteMetaData_Click" Text="Delete MetaData" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Enter photo id: "></asp:Label>
                    <asp:TextBox ID="txtphotoId" runat="server"></asp:TextBox>
&nbsp;&nbsp;
                    <asp:Button ID="btnDeletePhoto" runat="server" OnClick="btnDeletePhoto_Click" Text="Delete photo" />
                </td>
                <td class="auto-style3">
                    <asp:Label ID="lblGeolocation" runat="server" Text="Geolocation: "></asp:Label>
&nbsp;&nbsp; </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Label ID="lblTags" runat="server" Text="Tags: "></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Label ID="lblCaptureDate" runat="server" Text="Capture date: "></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Label ID="lblCaptureBy" runat="server" Text="Capture by: "></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />

    </div>
        <div>
            <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>
                <image src ="Images/<%#Eval("Photo")%>" height ="150" width ="180" />
            </ItemTemplate>
        </asp:DataList>
        </div>
    </form>
    </body>
</html>
