<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="UnderConstruction.aspx.cs" Inherits="TheClinicApp1._1.Login.UnderConstruction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style>
.btn
{
    width:200px;
    height:30px;
    background-color:#59a9f6;
}
.ErrorMsg
{
  color:#a5560a;
  font-size:20px;
  text-align:center;
 /*padding:50px 50px 50px 50px !important;*/
 margin-top:50px !important;
}
.ErrorDiv
{
    background-color:#e4dfdf;
padding: 250px 650px 500px 250px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Button runat="server" CssClass="btn" ID="btn_Login" Text="Back To Login" OnClick="btn_Login_Click" />
    <div class="ErrorDiv">
    <div class="col-lg-12">
        <div class="col-lg-2">
            <asp:Image runat="server" ImageUrl="~/images/dbError.png" />
        </div>
        <div class="col-lg-10">
             <asp:Label runat="server" CssClass="ErrorMsg" ID="lblErrorMsg" Text="DATABASE ERROR: CONNECTION FAILED! <br/>Unable to connect to the database..!!!"></asp:Label>
        </div>
    </div>
        </div>
   
<%--  machine name :   <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />

  system name:   <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>--%>
</asp:Content>
