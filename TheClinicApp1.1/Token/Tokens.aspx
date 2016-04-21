<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Tokens.aspx.cs" Inherits="TheClinicApp1._1.Token.Tokens" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>
    <style>
        .hello {
            font-size: 30px;
            font-family: 'Footlight MT';
            font-weight: bold;
        }
    </style>
    <script src="../Scripts/DeletionConfirmation.js"></script>
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/Dynamicgrid.js"></script>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />

    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/Dynamicgrid.js"></script>

    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/jquery-ui.js"></script>

    <script>
        $(document).ready(function () {

            debugger;
            $('.nav_menu').click(function () {
                
                $(".main_body").toggleClass("active_close");
            });

            debugger;
              
         
            var ac=null;
            ac = <%=listFilter %>;
            $( "#txtSearch" ).autocomplete({
                source: ac
            });       
        });
    </script>


    <script>
        
        function bindPatientDetails()
        {
            debugger;

  
          
            var PatientName = document.getElementById("txtSearch").value;
       
                     
            var file=PatientName.split('📝')      
            var file1=file[1]
            
            if (PatientName!="")

            { 
                                  
                PageMethods.PatientDetails(file1, OnSuccess, onError);  
            }

            function OnSuccess(response, userContext, methodName) 
            {   
                debugger;         
                var string1 = new Array();
                string1 = response.split('|');
               
                document.getElementById('<%=hdnfileID.ClientID%>').value=string1[0];
                document.getElementById('<%=lblFileNo.ClientID%>').innerHTML=string1[0];
                document.getElementById('<%=lblPatientName.ClientID%>').innerHTML=string1[1];
                document.getElementById('<%=lblAge.ClientID%>').innerHTML=string1[2];
                document.getElementById('<%=lblGender.ClientID%>').innerHTML=string1[3];
                document.getElementById('<%=lblAddress.ClientID%>').innerHTML=string1[4];
                document.getElementById('<%=lblMobile.ClientID%>').innerHTML=string1[5];
                document.getElementById('<%=lblEmail.ClientID%>').innerHTML=string1[6];
                document.getElementById('<%=HiddenPatientID.ClientID%>').value=string1[7];
                document.getElementById('<%=HiddenClinicID.ClientID%>').value=string1[8];

                document.getElementById('txtSearch').value="";//clearin the earch box

                document.getElementById('DropDownDoctor').style.visibility= 'visible';
            }          
            function onError(response, userContext, methodName)
            {                   
            }         
        }


    </script>





    <!-- #main-container -->
    <div class="main_body">
        <div class="left_part">
            <div class="logo"><a href="#">
                <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                <li id="token" class="active"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
            </ul>

            <p class="copy">
                <asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>


        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">nav</a>
                Need Tokens..
            </div>
            <div class="icon_box">
                <a class="all_token_link" data-toggle="modal" data-target="#all_token"><span title="All Tokens" data-toggle="tooltip" data-placement="left">
                    <img src="../images/tokens.png" /></span></a>
            </div>
            <div class="grey_sec">
                <div class="search_div">
                    <input class="field" id="txtSearch" onblur="bindPatientDetails()" name="txtSearch" type="search" placeholder="Search here..." />
                    <input class="button" onserverclick="btnSearch_ServerClick" runat="server" type="submit" value="Search" />
                </div>
                <ul class="top_right_links">
                    <li><a class="book_token" runat="server" id="btnBookToken" onserverclick="btnBookToken_ServerClick"><span></span>Book Token</a></li>
                    <li><a class="new" href="Tokens.aspx"><span></span>New</a></li>
                </ul>
            </div>

            <div class="right_form">

                <div class="token_id_card">
                    <div class="name_field">
                        <asp:Label ID="lblPatientName" runat="server" Text="Name"></asp:Label><span class="generate_token"><asp:Label ID="lblToken" runat="server" Text="_"></asp:Label></span></div>
                    <div class="light_grey">
                        <div class="col3_div">Age<span><asp:Label ID="lblAge" runat="server" Font-Size="Large"></asp:Label></span></div>
                        <div class="col3_div">Gender<span><asp:Label ID="lblGender" runat="server" Font-Size="Large"></asp:Label></span></div>
                        <div class="col3_div">File No<span><asp:Label ID="lblFileNo" runat="server" Font-Size="Large"></asp:Label></span></div>
                    </div>
                    <div class="card_white">
                        <div class="field_label">
                            <label>Address</label><asp:Label ID="lblAddress" runat="server"></asp:Label></div>
                        <div class="field_label">
                            <label>Mobile</label><asp:Label ID="lblMobile" runat="server"></asp:Label></div>
                        <div class="field_label">
                            <label>Email</label>
                            <a href="mailto: demo@test.com">
                                <asp:Label ID="lblEmail" runat="server"></asp:Label></a></div>
                        <div class="field_label">
                            <label>Last visit</label><asp:Label ID="lblLastVisit" runat="server"></asp:Label></div>

                        <div class="field_label" id="BookedDoctorName" visible="false" runat="server">
                            <label>Doctor</label><asp:Label ID="lblDoctor" runat="server"></asp:Label></div>
                        <br />
                        <br />
                        <div class="field_label" id="DropDownDoctor" style="visibility: hidden">
                            <label>Doctor</label><asp:DropDownList ID="ddlDoctor" Width="60%" runat="server"></asp:DropDownList></div>
                    </div>
                </div>

            </div>

        </div>
    </div>

    <asp:HiddenField ID="HiddenPatientID" runat="server" />
    <asp:HiddenField ID="HiddenClinicID" runat="server" />
     <asp:HiddenField ID="hdnfileID" runat="server" />

    <!-- Modal -->
    <div id="all_token" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                    <h3 class="modal-title">Today's Patient Bookings</h3>
                </div>
                <div class="modal-body" style="width:100%;height:100%;overflow-x:auto;">
                    <%--<h4>Today's Patient Bookings</h4>--%>
                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                             
                                     <asp:GridView ID="GridViewTokenlist" runat="server" AutoGenerateColumns="False"  CssClass="footable" DataKeyNames="UniqueId">
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                            <Columns>
                                  <asp:TemplateField HeaderText="">
                                                  <ItemTemplate>
                                                       <asp:ImageButton ID="ImgBtnDelete" runat="server"  ImageUrl="~/images/Cancel.png"   Width="25px" OnClientClick="return  ConfirmDelete();"  OnClick="ImgBtnDelete_Click1"/>

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
                 

                    <%--    <h4>Doctor 2</h4>
        <table class="table" width="100%" border="0">
          <tr>
            <th>Token No</th>
            <th>Name</th>
            <th>Time</th>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
        </table>--%>
                </div>
            </div>

        </div>
    </div>


</asp:Content>
