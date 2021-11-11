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
        </style>
    <title></title>
    </head>
<body style="height: 587px; width: 1309px">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblWelcome" runat="server" Text="Welcome"></asp:Label>
        <br />
        <asp:Button ID="btnAddPhoto" runat="server" CssClass="roundedcorner" Font-Size="Larger" Text="Add photo" OnClick="btnAddPhoto_Click1" Height="41px" Width="160px" />
        <br />
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <br />
        <br />
        <asp:Button ID="btnViewPhotos" runat="server" Text="View photos" OnClick="btnViewPhotos_Click" />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" Height="143px" Width="190px" />
        <br />

    </div>
        <div>
            <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>
                <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("PhotoId") %>'></asp:Label>--%>
                <image src ="Images/<%#Eval("Photo")%>" height ="150" width ="180" />
            </ItemTemplate>
        </asp:DataList>
        </div>
    </form>
    </body>
</html>
