<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoGallery.aspx.cs" Inherits="MemShare.PhotoGallery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                //Examples of how to assign the ColorBox event to elements
                $(".test").colorbox({ rel: 'group1', transition: "none", width: "700px " });
            });
        </script>


</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:30px">
    <table>
    <tr><td><asp:Button ID="btnalbum" runat="server" Text="Create New Album" /></td><td></td><td><asp:Button ID="Button1" runat="server" Text="Add More photo" /> </td></tr>
   <tr><td></td><td></td><td></td></tr>
    <tr><td></td><td></td><td></td></tr>
    </table>
        
        <asp:DataList ID="dlimage" runat="server" RepeatDirection="Horizontal" DataKeyField="AlbumId" RepeatColumns="5">
         <ItemTemplate>
         <table>
         <tr><td> <a href='<%# Eval("Photo","Images/{0}") %>' class='test' title='<%# Eval("photo") %>'><asp:Image ID="img"  runat="server" ImageUrl='<%# Eval("Photo","/Images/{0}") %>'  Height="150px" Width="200px"/>
                 </a></td></tr>
          <tr><td><asp:Label ID="lbldescription" runat="server" Text='<%# Eval("photo") %>'></asp:Label></td></tr>
         </table>             
                  </ItemTemplate>  
        </asp:DataList>                           
 </div>
    </form>
</body>
</html>