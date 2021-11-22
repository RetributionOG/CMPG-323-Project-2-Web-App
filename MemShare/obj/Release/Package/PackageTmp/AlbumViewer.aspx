<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumViewer.aspx.cs" Inherits="MemShare.AlbumViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>
     table tr td
     {
             text-align: center;
             margin-bottom:10px;
     }
     a{ text-decoration: none;}
     img
     {
     padding: 4px;
    border: 2px solid #000;
    border-radius: 12px;
    margin: 5px;
     }
     </style>
    <link href="css/colorbox.css" rel="stylesheet" type="text/css" />
                        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
                        <script src="js/jquery.colorbox.js"></script>
        <script>
            $(document).ready(function () {
                $(".test").colorbox({ rel: 'group1', transition: "none", width: "700px " });
            });
        </script>
    <link href="Container-sm/OverallStyleSheet.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:30px">
    <table>
    <tr><td><asp:Button ID="btnAlbum" runat="server" Text="Create New Album" OnClick="btnAlbum_Click"/></td><td></td><td><asp:Button ID="btnMore" runat="server" Text="Add More Photos" OnClick="btnMore_Click"/> </td></tr>
   <tr><td></td><td></td><td></td></tr>
    <tr><td></td><td></td><td></td></tr>
    </table>
        
        <asp:DataList ID="dlimage" runat="server" RepeatDirection="Horizontal" DataKeyField="AlbumId" RepeatColumns="8" onitemcommand="dlimage_ItemCommand">
         <ItemTemplate>
         <table>
         <tr><td> <a href='<%# Eval("PhotoPath","{0}") %>' class='test' title='<%# Eval("photo") %>'><asp:Image ID="img"  runat="server" ImageUrl='<%# Eval("PhotoPath","{0}") %>'  Height="150px" Width="200px"/>
                 </a></td></tr>
          <tr><td><asp:LinkButton ID="lbtDescription" runat="server" CommandArgument='<%# Eval("PhotoPath") %>' CommandName="Download" Text='<%# Eval("PhotoId") %>'></asp:LinkButton></asp:Label></td></tr>
         </table>             
                  </ItemTemplate>  
        </asp:DataList>                           
 </div>
    </form>
</body>
</html>