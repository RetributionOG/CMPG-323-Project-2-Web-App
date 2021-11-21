<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Albums.aspx.cs" Inherits="MemShare.Albums" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <style>
   table tr td
   {
           text-align: center;
   }
   a
   {
           text-decoration: none;
    font-weight: bold;
    font-size: 18px;
    color: #000;
}
   </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:40px;">
     <table>
        <tr><td>
            <asp:Button ID="btnback" runat="server" Text="Go Back" OnClick="btnback_Click" />
            </td><td></td></tr>
           <tr><td></td><td></td></tr>
    <tr><td>Create Album :</td><td> <asp:TextBox ID="txtalbumname" runat="server"></asp:TextBox></td></tr>
     <tr><td></td><td></td></tr>
    <tr><td>Upload Cover :</td><td> <asp:FileUpload ID="albumcover" runat="server" /></td></tr>
     <tr><td></td><td></td></tr>
    <tr><td>&nbsp;</td><td> <asp:Button ID="btncreate" runat="server" Text="Create Album" OnClick="btnCreate_Click"
             /></td></tr>
    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Enter Album Name:"></asp:Label>
        </td><td>
            <asp:TextBox ID="txtDelete" runat="server"></asp:TextBox>
            <asp:Button ID="btnDeleteAlbum" runat="server" OnClick="btnDeleteAlbum_Click" Text="Delete Album" />
        </td></tr>
    <tr><td></td><td></td></tr>
    </table>
    <asp:DataList ID="dlImages" runat="server" RepeatDirection="Horizontal" DataKeyField="AlbumId" RepeatColumns="8"
            onitemcommand="dlImages_ItemCommand">         
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />         
        <ItemTemplate> 
        <table>
        <tr><td><asp:Image ID="imgEmp" runat="server" Width="300px" Height="300px" ImageUrl='<%# Bind("AlbumCover") %>' style="padding:10px"/></td></tr>
          <tr><td> <asp:LinkButton ID="linkItemDetails" runat="server" Text='<%# Bind("AlbumName") %>'></asp:LinkButton>
             </td></tr>
        </table>
           </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
