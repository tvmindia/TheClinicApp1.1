<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ViewAllRegistration.aspx.cs" Inherits="TheClinicApp1._1.Registration.ViewAllRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <script>
      function fill(Patient) {
          debugger;
          parent.getPatientId(Patient); 
      }
  </script>
    <div class="main_body">
      <asp:GridView ID="dtgViewAllRegistration" runat="server" AutoGenerateColumns="False" style="text-align:center;width:100%;" CellPadding="4" ForeColor="#333333" GridLines="None" Height="100%" AllowPaging="true" OnPageIndexChanging="dtgViewAllRegistration_PageIndexChanging" PageSize="5">
      <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
      <Columns>
      <asp:TemplateField>
       <ItemTemplate>
      <asp:ImageButton ID="ImgBtnUpdate" runat="server" ImageUrl="~/images/Pencil-01.png" CommandName="Comment" CommandArgument='<%# Eval("PatientID")+"|" + Eval("Name") + "|" + Eval("Address")+"|"+ Eval("Phone")+"|"+ Eval("Email")+"|"+Eval("DOB")+"|"+Eval("Gender")+"|"+Eval("MaritalStatus")+"|"+Eval("image")+"|"+Eval("ImageType")%>' OnClientClick="Fill(hello);" formnovalidate />
    </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
         <ItemTemplate>
      <asp:ImageButton ID="ImgBtnDelete" runat="server" ImageUrl="~/Images/Cancel.png" CommandName="CommentDelete" CommandArgument='<%# Eval("PatientID")%>' OnClientClick="return confirm('Deletion Confirmation \n\n\n\n\ Are you sure you want to delete this item ?');" OnCommand="ImgBtnDelete_Command" formnovalidate />
         </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
         <HeaderStyle HorizontalAlign="Center" />
      </asp:TemplateField>
          <asp:BoundField DataField="Name" HeaderText="Patient Name">
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
          </asp:BoundField>
          <asp:BoundField DataField="Address" HeaderText="Address">
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
       </asp:BoundField>
          <asp:BoundField DataField="Phone" HeaderText="Phone">
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
           </asp:BoundField>
        <asp:BoundField DataField="Email" HeaderText="Email">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
            </asp:BoundField>
            </Columns>
                            <EditRowStyle HorizontalAlign="Center" BackColor="#0080AA"></EditRowStyle>

                            <FooterStyle BackColor="#0080AA" ForeColor="White" Font-Bold="True"></FooterStyle>

                            <HeaderStyle BackColor="#0080AA" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle HorizontalAlign="Justify" ForeColor="#0000ff" BackColor="RoyalBlue"></PagerStyle>

                            <RowStyle BackColor="#EFF3FB"></RowStyle>

                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                            <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                            <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                            <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                            <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                        </asp:GridView>
        </div>
</asp:Content>
