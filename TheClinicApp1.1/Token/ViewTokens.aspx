<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewTokens.aspx.cs" Inherits="TheClinicApp1._1.Token.ViewTokens" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 

    <script src="../js/DeletionConfirmation.js"></script>

     


                            <asp:GridView ID="GridViewTokenlist"  runat="server" AutoGenerateColumns="False" CssClass="footable" DataKeyNames="UniqueId">
                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnDelete" runat="server" ImageUrl="~/images/Cancel.png"   OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Doctor Name" DataField="DOCNAME" />
                                    <asp:BoundField HeaderText="Token No" DataField="TokenNo" />
                                    <asp:BoundField HeaderText="Patient Name" DataField="Name" />
                                    <asp:BoundField HeaderText="Time" DataField="DateTime" />

                                </Columns>

                            </asp:GridView>
             
</asp:Content>
