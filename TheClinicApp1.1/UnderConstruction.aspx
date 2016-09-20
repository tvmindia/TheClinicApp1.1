<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="UnderConstruction.aspx.cs" Inherits="TheClinicApp1._1.Login.UnderConstruction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style>
.btn
{
    width:200px;
    background-color:#0094ff;
}
.ErrorMsg
{
  color:#a5560a;
  font-size:20px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Button runat="server" CssClass="btn" ID="btn_Login" Text="Back To Login" OnClick="btn_Login_Click" />
    <div class="col-lg-12">
        <div class="col-lg-6">
            <asp:Image runat="server" />
        </div>
        <div class="col-lg-6">
             <asp:Label runat="server" CssClass="ErrorMsg" ID="lblErrorMsg" Text="Db error"></asp:Label>
        </div>
    </div>
   
<%--  machine name :   <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />

  system name:   <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>--%>
</asp:Content>
