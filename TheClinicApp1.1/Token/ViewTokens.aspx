<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewTokens.aspx.cs" Inherits="TheClinicApp1._1.Token.ViewTokens" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <style>
table{
    border-collapse: collapse;    
    width: 100%;
    
    }

table td {
    text-align: left;
    height:auto;
    
    }
table td{
    width:auto;
    height:auto;
    
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:14px;
    font-weight:200;
    padding-left:4px;
}
table td+td+td{
    width:150px;
    height:auto;
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:14px;
    font-weight:200;
    padding-left:4px;
   
}
/*.modal table tr:nth-child(even){background-color: #f2f2f2}*/

table th {
    background-color: #5681e6;
    text-align: center;
    color: white;
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:16px;
}
</style>
    
    <script src="../js/DeletionConfirmation.js"></script>
   
    <asp:HiddenField ID="hdnCount" runat="server" />
    <asp:GridView ID="GridViewTokenlist" runat="server" AutoGenerateColumns="False" DataKeyNames="UniqueId">
    
       <Columns>
          <asp:TemplateField HeaderText="">
             <ItemTemplate>
              <asp:ImageButton ID="ImgBtnDelete" style="border:none!important" runat="server" ImageUrl="~/images/Deleteicon1.png"  OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click" />
               </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField HeaderText="Doctor Name" DataField="DOCNAME" />
                  <asp:BoundField HeaderText="Token No" DataField="TokenNo" />
                  <asp:BoundField HeaderText="Patient Name" DataField="Name" />
                   <asp:BoundField HeaderText="Time" DataField="Date" />
                 </Columns>

       </asp:GridView>
             
</asp:Content>
