<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="MemShare.Gallery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:50px">
   <table>
    <tr> <td><asp:Button ID="btngallery" runat="server" Text="View Album" OnClick="btngallery_Click1"
                 /></td><td>
                 <asp:Button ID="btncreate" runat="server" Text="Create Album" OnClick="btncreate_Click1"
                      /></td> </tr>
                        <tr><td></td><td></td></tr>
                      <tr><td></td><td></td></tr>
                       <tr><td></td><td></td></tr>
   </table>
     <table style="margin-top:30px">   
     <tr><td>Upload Image to Album :</td><td> <asp:FileUpload ID="FileUpload1" runat="server" />
        </td></tr>
            <tr><td></td><td></td></tr>
            <tr><td></td><td><asp:Button ID="Button1" runat="server"
            Text="Upload image" onclick="Button1_Click" /></td></tr>
            <tr><td></td><td></td></tr>
     </table>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>   
    <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>  
   
        <asp:GridView ID="grdphoto" runat="server" AutoGenerateColumns="false" DataKeyNames="PhotoID">
        <Columns>
       <asp:TemplateField>
                <ItemTemplate>
                <table>
                <tr>
                <td>
                    <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("Photo","/Images/{0}") %>' Height="300px" Width="300px" /></td>                  
                </tr>
                </table>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                 <table>
                 <tr><td>Enter Description :</td></tr>
                 <tr>
                    <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> </td> </tr>
                    </table>
                </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        </asp:GridView>
        </ContentTemplate>  
</asp:UpdatePanel>
         <table><tr><td><asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" /></td></tr></table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
