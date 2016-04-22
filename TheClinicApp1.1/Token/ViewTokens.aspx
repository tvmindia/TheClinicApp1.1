<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="ViewTokens.aspx.cs" Inherits="TheClinicApp1._1.Token.ViewTokens" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>


                            <asp:GridView ID="GridViewTokenlist"  runat="server" AutoGenerateColumns="False" CssClass="footable" DataKeyNames="UniqueId">
                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnDelete" runat="server" ImageUrl="~/images/Cancel.png" Width="25px" OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click1" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Doctor Name" DataField="DOCNAME" />
                                    <asp:BoundField HeaderText="Token No" DataField="TokenNo" />
                                    <asp:BoundField HeaderText="Patient Name" DataField="Name" />
                                    <asp:BoundField HeaderText="Time" DataField="DateTime" />

                                </Columns>

                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
</asp:Content>
