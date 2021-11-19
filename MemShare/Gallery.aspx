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
    <tr> <td><asp:Button ID="btnGallery" runat="server" Text="View Album" OnClick="btnGallery_Click"
                 /></td><td>
                 <asp:Button ID="btnCreate" runat="server" Text="Create New Album" OnClick="btnCreate_Click"
                      /></td> </tr>
                        <tr><td></td><td></td></tr>
                      <tr><td></td><td></td></tr>
                       <tr><td></td><td></td></tr>
   </table>
     <table style="margin-top:30px">   
     <tr><td>Upload Image to Album :</td><td> <asp:FileUpload ID="FileUpload" runat="server" />
        </td></tr>
            <tr><td></td><td></td></tr>
            <tr><td></td><td><asp:Button ID="btnUpload" runat="server"
            Text="Upload image" onclick="btnUpload_Click" /></td></tr>
            <tr><td></td><td></td></tr>
     </table>
     <asp:ScriptManager ID="sctController" runat="server">
        </asp:ScriptManager>   
    <asp:UpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>  
   
        <asp:GridView ID="grdPhoto" runat="server" AutoGenerateColumns="false" DataKeyNames="PhotoID">
        <Columns>
       <asp:TemplateField>
                <ItemTemplate>
                <table>
                <tr>
                <td>
                    <asp:Image ID="imgPreview" runat="server" ImageUrl='<%# Eval("PhotoPath","/Images/{0}") %>' Height="300px" Width="300px" /></td>                  
                </tr>
                </table>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                 <table>
                 <tr><td>Enter Description :</td></tr>
                 <tr>
                    <td><asp:TextBox ID="txtDescription" runat="server"></asp:TextBox> </td> </tr>
                    </table>
                </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        </asp:GridView>
        </ContentTemplate>  
</asp:UpdatePanel>
         <table><tr><td><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td></tr></table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
