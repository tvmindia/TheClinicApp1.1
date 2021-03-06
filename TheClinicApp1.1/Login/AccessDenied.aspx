﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="TheClinicApp1._1.Login.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>

    <link href="../css/main.css" rel="stylesheet" />
    <div class="main_body">

        <div class="left_part">
            <div class="logo"><a href="#">
                 <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a></div>
            <ul runat="server" ClientIDMode="Static" class="menu" id="list">
                <li id="patients" runat="server"  ClientIDMode="Static"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Registration</span></a></li>
                <li id="Appoinments" runat="server"  ClientIDMode="Static"><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
                <li id="token" runat="server"  ClientIDMode="Static"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor" runat="server"  ClientIDMode="Static"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor's OP</span></a></li>
                <li id="pharmacy" runat="server"  ClientIDMode="Static"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"  runat="server"  ClientIDMode="Static"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin"  runat="server"  ClientIDMode="Static" ><a name="hello" onclick="selectTile('<%=admin.ClientID %>','<%=RoleName %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="Repots" runat="server"  ClientIDMode="Static"><a name="hello" href="../Report/ReportsList.aspx" onclick="selectTile('Repots','')"><span class="icon report"></span><span class="text">Reports</span></a></li>
                <li id="master"  runat="server"  ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Masters</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>
        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">nav</a>
                Sorry, you are not authorized to access this page!<ul class="top_right_links">
                    <li>
                        <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                    <li>
                        <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClick="LogoutButton_Click" formnovalidate /></li>
                </ul>
            </div>
            <div class="content padding-t-100">
                <img src="../images/403.png" alt="403- Access denied" style="opacity: 0.4; margin: auto; height: 100%; width: 100%; padding-top: 10%; background-color: #FFFFFF" />
            </div>
        </div>
    </div>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script>
        var test = jQuery.noConflict();
        test(document).ready(function () {
            activateTabSelection('<%=From%>');
              test('.nav_menu').click(function () {
                  test(".main_body").toggleClass("active_close");
              });

        });

    </script>



</asp:Content>
