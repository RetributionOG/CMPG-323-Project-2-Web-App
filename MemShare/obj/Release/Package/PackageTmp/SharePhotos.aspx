<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SharePhotos.aspx.cs" Inherits="MemShare.SharePhotos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        a{ text-decoration: none;}
        .auto-style2 {
            width: 647px;
        }
        .auto-style3 {
            width: 547px;
        }
        </style>
    <link href="Container-sm/OverallStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label1" runat="server" Text="PhotoId: "></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtPhotoId" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:Button ID="btnShare" runat="server" Text="Share" OnClick="btnShare_Click" />
                </td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="4">
                    <asp:Label ID="Label2" runat="server" Text="Share To: "></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtShare" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
        
        <asp:DataList ID="dlimage1" runat="server" RepeatDirection="Horizontal" DataKeyField="AlbumId" RepeatColumns="5">
         <ItemTemplate>
         <table>
         <tr><td> <a href='<%# Eval("PhotoPath","{0}") %>' class='test' title='<%# Eval("photoId") %>'><asp:Image ID="img"  runat="server" ImageUrl='<%# Eval("PhotoPath","{0}") %>'  Height="150px" Width="200px"/>
                 </a></td></tr>
          <tr><td><asp:Label ID="lbldescription" runat="server" Text='<%# Eval("photoId") %>'></asp:Label></td></tr>
         </table>             
                  </ItemTemplate>  
        </asp:DataList>                           
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
