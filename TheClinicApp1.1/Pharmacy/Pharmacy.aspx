<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Pharmacy.aspx.cs" Inherits="TheClinicApp1._1.Pharmacy.Pharmacy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>


    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
   
    <script src="../js/jquery-ui.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/fileinput.js"></script>

    <!-- #main-container -->
        <script>
            function BindMedunitbyMedicneName(ControlNo) 
            {
                
  
                if (ControlNo >= 0) {
                    var MedicineName = document.getElementById('txtMedName' + ControlNo).value;
                }
                if (MedicineName != "") 
                {
                    PageMethods.MedDetails(MedicineName, OnSuccess, onError);       
                }    
                function OnSuccess(response, userContext, methodName) 
                {        
                    if (ControlNo >= 0) 
                    {                  
                        document.getElementById('txtMedUnit' + ControlNo).value = response;                       
                    }   
                }  
                function onError(response, userContext, methodName) {       
                }
            }

            function focuscontrol(ControlNo)
            {
                document.getElementById('txtMedDos' + ControlNo).focus();
            }                 
           

            function autocompleteonfocus(controlID)
            {
                //---------* Medicine auto fill, it also filters the medicine that has been already saved  *----------//
                
                var topcount =Number(document.getElementById('<%=hdnRowCount.ClientID%>').value)+Number(1);
 
            if (topcount==0)
            {
                var ac=null; 
                ac = <%=listFilter %>;
                $( "#txtMedName0").autocomplete({
                    source: ac
                });
            }
            else
            {
                var ac=null;
                ac = <%=listFilter %>;
                var i=0;
                while(i<=topcount)
                {
                    if (i==0)
                    {
                        var item=  document.getElementById('txtMedName'+i).value                                  
                        var result = ac.filter(function(elem){
                            return elem != item; 
                        });
                    }
                    else
                    {
                        if (document.getElementById('txtMedName'+i) != null)
                        {
                            var item=  document.getElementById('txtMedName'+i).value 
                            result = result.filter(function(elem){
                                return elem != item; 
                            }); 
                        }
                    }
                    i++;
                }            
                            
                $( "#txtMedName"+controlID).autocomplete({
                    source: result
                });
            }
            } 
            </script>


    <div class="main_body">

        <div class="left_part">
            <div class="logo"><a href="#">
                <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy" class="active"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin"><a name="hello" onclick="selectTile('admin','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Admin</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>


        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>
                Pharmacy...
            </div>
            <div class="icon_box">
                <a class="patient_list" data-toggle="modal" data-target="#patient_list"><span title="Patient List" data-toggle="tooltip" data-placement="left">
                    <img src="../images/patient_list.png" /></span></a>
            </div>
            <div class="grey_sec">
                <div class="search_div">
                    <input class="field" type="search" placeholder="Search here..." />
                    <input class="button" type="submit" value="Search" />
                </div>
                <ul class="top_right_links">
                    <li>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btnSave_Click" /></li>
                    <li><a class="new" href="#"><span></span>New</a></li>
                </ul>
            </div>
            <div class="right_form">

                <div class="token_id_card">
                    <div class="name_field">
                        <img src="../images/UploadPic1.png" width="80" height="80" /><asp:Label ID="lblPatientName" runat="server" Text="Patient_Name"></asp:Label></div>
                    <div class="light_grey">
                        <div class="col3_div">
                            <asp:Label ID="lblAgeCount" runat="server" Text="Age"></asp:Label>
                        </div>
                        <div class="col3_div">
                            <asp:Label ID="lblGenderDis" runat="server" Text="Gender"></asp:Label>
                        </div>
                        <div class="col3_div">
                            <asp:Label ID="lblFileNum" runat="server" Text="File No"></asp:Label>
                        </div>
                    </div>
                    <div class="card_white">
                        <div class="field_label">
                            <label>Doctor</label><asp:Label ID="lblDoctor" runat="server" Text=""></asp:Label></div>
                    </div>
                </div>


                <div class="prescription_grid">
                    <table class="table" style="width: 100%; border: 0;">
                        <tbody>
                            <tr>
                                <th>Medicine</th>
                                <th>Quantity</th>
                                <th>Unit</th>
                                <th>Dosage</th>
                                <th>Timing</th>
                                <th>Days</th>
                            </tr>
                             <tr>
                                    <td>
                                        <input id="txtMedName0" type="text" placeholder="Medicine" class="input" onblur="BindMedunitbyMedicneName('0')" onfocus="autocompleteonfocus(0)" /></td>
                                    <td>
                                        <input id="txtMedQty0" type="text" placeholder="Qty" class="input" onblur="focuscontrol(0)" /></td>
                                    <td>
                                        <input id="txtMedUnit0" class="input" readonly="true" type="text" placeholder="Unit" /></td>
                                    <td>
                                        <input id="txtMedDos0" type="text" placeholder="Dosage" class="input" /></td>
                                    <td>
                                        <input id="txtMedTime0" type="text" placeholder="Timing" class="input" /></td>
                                    <td>
                                        <input id="txtMedDay0" type="text" placeholder="Days" class="input" /></td>
                                    <td style="background: #E6E5E5">
                                        <input type="button" value="-" class="bt1" onclick="ClearAndRemove1()" style="width: 20px;" /></td>
                                    <td style="background: #E6E5E5">
                                        <input type="button" id="btAdd" onclick="clickAdd(0); this.style.visibility = 'hidden';" value="+" class="bt1" style="width: 20px" />
                                    </td>
                                    <td style="background-color: transparent">
                                        <input id="hdnDetailID0" type="hidden" />
                                        <input id="hdnQty0" type="hidden" /></td>
                                </tr>
                        </tbody>
                    </table>
                    <div id="maindiv">
                    </div>
                </div>

            </div>

        </div>


        
                <asp:HiddenField ID="hdnXmlData" runat="server" />
               <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                <asp:HiddenField ID="hdnTextboxValues" runat="server" />             
                <asp:HiddenField ID="hdnRemovedIDs" runat="server" />
    </div>






    <!-- Modal -->
    <div id="patient_list" class="modal fade" role="dialog">
        <div class="modal-dialog" style="height: 600px;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Patient List</h4>
                </div>
                <div class="modal-body" style="height: 500px; overflow-y: scroll; overflow-x: hidden;">

                    <asp:GridView ID="GridViewPharmacylist" runat="server" AutoGenerateColumns="False" DataKeyNames="PatientID">

                        <Columns>
                            <asp:TemplateField ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtn" runat="server" ImageUrl="../images/paper.png" CommandArgument='<%# Eval("PatientID")+"|" + Eval("DoctorID") %>' OnCommand="ImgBtn_Command" ImageAlign="Middle" BorderColor="White" formnovalidate />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Doctor" DataField="DOCNAME" />
                            <asp:BoundField HeaderText="Token No" DataField="TokenNo" />
                            <asp:BoundField HeaderText="Patient Name" DataField="Name" />
                            <asp:BoundField HeaderText="DoctorID" Visible="false" DataField="DoctorID" />
                            <asp:BoundField HeaderText="PatientID" Visible="false" DataField="PatientID" />

                        </Columns>
                    </asp:GridView>


                </div>
            </div>

        </div>
    </div>



    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/Dynamicgrid.js"></script>

    <script>
        var test = jQuery.noConflict();
        test(document).ready(function () {

            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });

        });
    </script>
</asp:Content>
