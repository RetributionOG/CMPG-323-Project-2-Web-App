<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="MemShare.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 221px;
        }
        .auto-style3 {
            width: 237px;
        }
        .auto-style4 {
            width: 100%;
        }
        img
        {
            padding: 4px;
            border: 2px solid #000;
            border-radius: 12px;
            margin: 5px;
        }
    </style>
    <link href="Container-sm/OverallStyleSheet.css" rel="stylesheet" />
    <link href="css/colorbox.css" rel="stylesheet" type="text/css" />
                        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
                        <script src="js/jquery.colorbox.js"></script>
        <script>
            $(document).ready(function () {
                $(".test").colorbox({ rel: 'group1', transition: "none", width: "700px " });
            });
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style4">
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label1" runat="server" Text="Enter term to search by:"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtSearchTerm" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"  />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label2" runat="server" Text="Select by what category to search:"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:RadioButtonList ID="rblSearchField" runat="server" AutoPostBack="True">
                            <asp:ListItem>Geographic Location</asp:ListItem>
                            <asp:ListItem>Tags</asp:ListItem>
                            <asp:ListItem>Captured Date (yyyy/MM/dd)</asp:ListItem>
                            <asp:ListItem>Captured By</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Go Back" />
                    </td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <div>
         <asp:DataList ID="dlimage" runat="server" RepeatDirection="Horizontal" DataKeyField="photoId" RepeatColumns="8">
         <ItemTemplate>
         <table>
         <tr><td> <a href='<%# Eval("PhotoPath","{0}") %>' class='test' title='<%# Eval("photo") %>'><asp:Image ID="img"  runat="server" ImageUrl='<%# Eval("PhotoPath","{0}") %>'  Height="150px" Width="200px"/>
                 </a></td></tr>
          <tr><td><asp:Label ID="lblDescription" runat="server" CommandArgument='<%# Eval("PhotoPath") %>' CommandName="Download" Text='<%# Eval("Photo") %>'></asp:Label></td></tr>
         </table>             
                  </ItemTemplate>  
        </asp:DataList> 
        </div>
    </form>
</body>
</html>
