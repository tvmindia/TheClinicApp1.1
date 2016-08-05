<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="IndividualReport .aspx.cs" Inherits="TheClinicApp1._1.Report.IndividualReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">



        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 10);
            return false;
        }
    </script>


    <style>
        table {
            border-collapse: collapse;
            border: 1px solid black;
            border-top: 1px solid white !important;
            border-right: 1px solid black;
            border-left: 1px solid black;
        }


            table th {
                border-top: 1px solid black;
                border-right: 1px solid black;
                border-left: 1px solid black;
                border-bottom: 1px solid black;
                font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
                font-size: 16px;
                font-weight: 100;
            }

            table td {
                width: 19%;
                height: auto !important;
                padding-left: 5px;
                margin: 5px 5px 5px 5px 5px;
                font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
                font-size: 14px;
            }

        tr.even td {
            background-color: #e1e6ef;
        }

        tr.odd td {
            background-color: #ffffff;
        }


        .footer {
            color: #0e3782;
            text-align: right;
        }

        .header {
            color: #0e3782;
        }

        p {
            font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
            font-size: 32px;
        }

        .Clinicname {
            font-family: 'caviardreams-regular';
        }

        .logo1 {
            margin: 15px 15px 15px 15px;
            width: 150px;
        }

        .button {
            display: inline-block;
            border-radius: 3px;
            background-color: #2D89EF;
            border: none;
            color: #FFFFFF;
            text-align: center;
            font-size: 16px;
            /*padding: 20px;*/
            /*padding-top:20px;*/
            /*padding-bottom:20px;*/
            /*position:center;*/
            width: 198px;
            /*height:30px;*/
            transition: all 0.5s;
            cursor: pointer;
            margin: 20px;
            font-family: Garamond;
        }


            .button span {
                cursor: pointer;
                display: inline-block;
                position: relative;
                transition: 0.5s;
            }

                .button span:after {
                    content: '»';
                    position: absolute;
                    opacity: 0;
                    top: 0;
                    right: -20px;
                    transition: 0.5s;
                }

            .button:hover span {
                padding-right: 25px;
            }

                .button:hover span:after {
                    opacity: 1;
                    right: 0;
                }


/*Advanced search css*/

        .noBorder {
            border: none !important;
            border-collapse: collapse;
        }

            .noBorder tr {
                border: none !important;
            }

            .noBorder td {
                border: none !important;
                width: 0;
            }

       
            .AddTolist {
  display: inline-block;
  border-radius: 3px;
  background-color: #2D89EF;
  border: none;
  color: #FFFFFF;
  text-align: center;
  font-size: 16px;
  /*padding: 20px;*/
  /*padding-top:20px;*/
  /*padding-bottom:20px;*/
  /*position:center;*/
  width: 100%;
  /*height:30px;*/
  transition: all 0.5s;
  cursor: pointer;
  margin: 2%;
  font-family:Garamond;
}

.AddTolist span {
  cursor: pointer;
  display: inline-block;
  position: relative;
  transition: 0.5s;
}

.AddTolist span:after {
  content: '»';
  position: absolute;
  opacity: 0;
  top: 0;
  right: -20px;
  transition: 0.5s;
}

.AddTolist:hover span {
  padding-right: 25px;
}

.AddTolist:hover span:after {
  opacity: 1;
  right: 0;
}


    </style>

    <%--Style ANd Script Files OF CAlenderControl--%>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/jquery-ui.js"></script>


    <script>


        //----- * Global Variables * ----------//

        var Conditions = [];
        var isPostBack = <%=Convert.ToString(Page.IsPostBack).ToLower()%>;
        var IsAddButtonClicked = false;
        var IsRemoveButtonClicked = false;

        $(document).ready(function () {
            
            //--- Managing advanced search panel hide - show

            if (isPostBack) 
            {
                $("#paneldiv").show();
                $("#searchIcon").css("display", "none");
                
                $("#UpIcon").css("display", "inline");
            }
           

            $("#UpIcon").click(function () {
                $("#paneldiv").slideUp("slow", function () {

                    //$("#searchIcon").css("display", "inline");

                    $("#searchIcon").fadeIn("slow");
                    $("#UpIcon").css("display", "none");
                })

            });


            // $("#paneldiv").css("display", "none");

            //$("#UpIcon").css("display", "none");

            $("#searchIcon").click(function () {

                $("#searchIcon").css("display", "none");
                $("#paneldiv").slideDown("slow");

                $("#UpIcon").css("display", "inline");


                $("#UpIcon").click(function () {
                    $("#paneldiv").slideUp("slow", function () {

                        //$("#searchIcon").css("display", "inline");

                        $("#searchIcon").fadeIn("slow");
                        $("#UpIcon").css("display", "none");
                    })

                });
            });


        });

        ///---------- *RemoveConditionsFromArray*-----------------//

        //This function is called while clicking remove button
        //Clicked item will be removed from datastructure (conditions -array) by its index
        //List will be recreated using datastructure
        //IsRemoveButtonClicked will set to true , to identify remove click
        function RemoveConditionsFromArray(i)
        {debugger;
            Conditions.splice(i,1);

            $('#ulConditions li').remove();

            IsRemoveButtonClicked = true;

            MakeListUsingArray();
        }


        ///---------- *MakeListUsingArray*-----------------//
        
        //List is created using this function
       
        function MakeListUsingArray()
        {
            $('li:contains("No Search Conditions Added!")').remove();


            if (  isPostBack && document.getElementById('<%= hdnArray.ClientID %>').value != "" && Conditions.length == 0 && IsRemoveButtonClicked== false) 
            {
                var ArrayContent = document.getElementById('<%= hdnArray.ClientID %>').value ;
                var array = ArrayContent.split(",");
                
                Conditions = array;
            }

            var WhereCondition = [];

            var ul = document.getElementById("ulConditions");
           
            if (  (isPostBack == false || Conditions.length >0)   || (IsAddButtonClicked == true) ) 
            {
                var li = document.createElement("LI");
               
            }

            for (var i = 0; i < Conditions.length; i++)
            {
                if ( (isPostBack && IsAddButtonClicked == false) || (IsRemoveButtonClicked==true && IsAddButtonClicked == false) ) 
                {
                    var li = document.createElement("LI");

                }

                li.id = "lstCondition"+i;
              
              //  li.innerHTML = '<span class="conditionli" title="Remove this condition" onclick="RemoveLi('+i+')">-</span>'+" "+ Conditions[i] ;

               // li.innerHTML ='<img style="cursor: pointer; width: 11px; height: 11px;" src="../Images/delete-cross.png" title="Remove this condition" onclick="RemoveConditionsFromArray('+i+')"/>&nbsp;&nbsp;'+""+ Conditions[i] ;
                li.innerHTML =  '<div style="flex-flow: wrapfloat: inherit;display: inline-flex;width:100%"><div style="width:90%;overflow:hidden;text-overflow:hidden" >'+Conditions[i]+'</div>'+" " +'<div  style="width:10%"><img style="cursor: pointer; width: 70%; height: 11px;" src="../Images/delete-cross.png" title="Remove this condition" onclick="RemoveConditionsFromArray('+i+')"/></div></div>';
                
                column = Conditions[i].split("=")[0];
                value = Conditions[i].split("=")[1];
                value = value.trim();

                var Condition = column + " LIKE '"+value+"%'"
                WhereCondition.push(Condition);

                document.getElementById("ulConditions").appendChild(li);
            }


            if (IsRemoveButtonClicked == true) 
            {
                IsRemoveButtonClicked = false;
            }
            if (IsAddButtonClicked == true) 
            {
                IsAddButtonClicked = false;
            }

            document.getElementById('<%= hdnWhereConditions.ClientID %>').value =  WhereCondition;
            document.getElementById('<%= hdnArray.ClientID %>').value =  Conditions;

            if ( $('#ulConditions li').length == 0) 
            {
                var li = document.createElement("LI");
                li.innerText = "No Search Conditions Added!";
                document.getElementById("ulConditions").appendChild(li);
            } 


        }


        ///---------- *AddCondition*-----------------//

        ///Function Is called when clicking on ADD button
        // Condition is generated by concatinating the selected column value with textbox value
        //Condition will be pushed to codition array
        //Using This array as datastructure , list will be recreated
        // IsAddButtonClicked global variable will set to true, to identify add button click

        function AddCondition() {
           
            if (($('#<%=ddlColumns.ClientID%>').val().trim() != "--Select--")) {

                if (document.getElementById('<%= txtvalue.ClientID %>').value != "") {

                    var selectoption = document.getElementById("<%=ddlColumns.ClientID %>");
                    var SelectedColumn = selectoption.options[selectoption.selectedIndex].text;
                    var value = document.getElementById('<%= txtvalue.ClientID %>').value;

                    var cndtion = SelectedColumn + " = "+value ;

                    Conditions.push(cndtion)

                    IsAddButtonClicked = true;

                    MakeListUsingArray();

                }

                else {
                    alert("Please enter a value");
                }


            }

            else {
                alert("Please select a column");
            }


        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 15px"></div>

    <table class="noBorder">
        <tr>
            <td>

                

            </td>
        </tr>
    </table>

    <div class="col-lg-12">

                    <div class="col-lg-1"></div>
                      <div class="col-lg-10">

                           <span class="tooltip2" style="float:right">
                                            <img src="../Images/gggg.png" id="UpIcon" style="cursor: pointer; display: none"  width="20" height="20" align="right"  />
                                            <span class="tooltiptext2">Hide Advanced Search</span>
                                        </span>

                    
                           <span class="tooltip2" style="float:right">
                                           <img src="../Images/Search.png" width="20" height="20" id="searchIcon" style="cursor: pointer"  align="right"  />
                                            <span class="tooltiptext2"> Advanced Search</span>
                                        </span>

                
                          </div>
        <div class="col-lg-1"></div>
                </div>

    <div id="paneldiv" style="width: 100%; display: none;" class="col-lg-12">

        <div class="col-lg-1"></div>

        <div class="col-lg-10" >

<%------------------------ * ADVANCED SEARCH * ------------------%>

            <fieldset style="border-radius: 15px!important;
    border: 2px solid #c7d4f3!important;">

               <legend style="font-family:caviardreams-regular;color:#89a7ef;width:20%;margin-left:2%" >Advanced Search</legend>

                <div class="col-lg-8">

                    <div class="row field_row">
                        <div class="col-lg-5 ">
                            <label for="name">Search Column</label>
                            <asp:DropDownList ID="ddlColumns" runat="server" Width="100%" Height="31px" CssClass="drop"></asp:DropDownList>
                        </div>
                        <div class="col-lg-5 ">
                            <label for="name">Value</label>
                            <asp:TextBox ID="txtvalue" runat="server" Width="100%"></asp:TextBox>
                        </div>

                        <div class="col-lg-2 ">
                            <label for="name" style="color:white">Add</label>

                            
                           <a class="btn btn-primary AddTolist" id="addBtn" onclick="return AddCondition();"><span>Add</span></></a>              
                                                



                            <%--<img id="imgAddIcon" src="../Images/plus1.png" onclick="return AddCondition();" alt="" style="cursor: pointer; width: 15px; height: 15px;" />--%>
                        </div>

                    </div>

                </div>


                <div class="col-lg-4">
                    
                     <label for="name" style="text-align:center;color:white">Conditions</label>

                     <ul id="ulConditions">
                                <li  >No Search Conditions Added!</li>
                                </ul>

                </div>

               <div class="col-lg-12"  >
            
          

                  <ul class="top_right_links" style="float:right">
                                    <li>
                                      <asp:Button ID="btnSearch" runat="server" CssClass="button1" Text="SEARCH" Style="width: 100%;background-color:#89a7ef;color:white;background-image:url('../images/magnifier-tool.png')" OnClick="btnSearch_Click"  />
                                    </li>
                                    
                                </ul>
             

         </div>
               
          </fieldset>

        </div>

         



      
         </div>


    <%------------------------ * PRINT * ------------------%>

    <%-- <div class="grey_sec">
                               
                                <ul class="top_right_links">
                                    <li>
                                        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="button1" OnClientClick="return PrintPanel();" />
                                    </li>
                                   
                                </ul>
                            </div>--%>


    <%--<a class="btn btn-primary button" id="addBtn" onclick="return PrintPanel();"><span>Print</span></></a>--%>

    <%--<asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary button"  OnClientClick="return PrintPanel();" />--%>

    <asp:Panel ID="pnlContents" runat="server">

        <div class="col-lg-12">

            <div class="col-lg-1"></div>
            <div class="col-lg-10">
                <asp:PlaceHolder ID="PlaceHolder3" runat="server" />
            </div>



        </div>

        <div class="col-lg-12">

            <div class="col-lg-1"></div>
            <div class="col-lg-10">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
            </div>



        </div>

        <div class="col-lg-12">

            <div class="col-lg-1"></div>
            <div class="col-lg-10">

                <asp:PlaceHolder ID="PlaceHolder2" runat="server" />

            </div>
            <div class="col-lg-1"></div>
        </div>


    </asp:Panel>

    <input id="hdnWhereConditions" type="hidden" value="" runat="server" />
    <input id="hdnArray" type="hidden" value="" runat="server" />
</asp:Content>
