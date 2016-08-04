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



        /*border-collapse:separate;
    border-spacing:0 20px; */


        /*.tab, tr, td, th {
           
           
            border: none;
           
        }

             .tab th {
                 
                border: none;
                border-collapse: collapse;
                text-align: left;
                background-color: white;
                color: black;
                text-decoration: underline;
            }*/

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


        .myListBox {
            border-style: none;
            border-width: 0px;
            border: none;
            font-size: 12pt;
            font-family: Verdana;
        }

        .conditionli {
            border: 1px solid darkgrey;
            padding: 0px 6px 4px 6px;
            background-color: lightgray;
            position: relative;
            cursor: pointer;
        }
    </style>

    <%--Style ANd Script Files OF CAlenderControl--%>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/jquery-ui.js"></script>


    <script>

        var Conditions = [];

        var isPostBack = <%=Convert.ToString(Page.IsPostBack).ToLower()%>;

        var IsAddButtonClicked = false;
        var IsRemoveButtonClicked = false;

        $(document).ready(function () {
            debugger;

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

                debugger;


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


            
            //$(".deleteMe").on("click", function(){
            //    debugger;


            //    $(this).closest("li").remove(); 
            //});
           


        });


        


        function RemoveCondition() {
            debugger;

            var c=  $(this);


            $(this).closest("li").remove(); 
<%--            var ClosestTr = document.getElementById('btRemove').closest('tr');
           // ClosestTr.remove();
            var tdContent = ClosestTr.innerText;


            var searchColumn = tdContent.split('=')[0];
            var searchValue =  tdContent.split('=')[1].trim() ;


       var Conditions = document.getElementById('<%=hdnWhereConditions.ClientID %>').value;
            var IndividualCondition=   Conditions.split(',');
            var html ='';--%>
        }

        //for (var i = 0; i < IndividualCondition.length; i++) {
    
        //    var Column=   IndividualCondition[i].split('LIKE')[0];
        //    var Value =  IndividualCondition[i].split('LIKE')[1];

        //    Value = Value.replace("%", "");
        //    Value = Value.replace("'","");
        //    Value = Value.trim();

        //    if (Column != searchColumn && Value) 
        //    {
    
                


        //    html = '<tr ><td >' + Column + " = "+Value + '</td><td style="width:10px" ><input type="button" id="btRemove" onclick="RemoveCondition()" class="bt1" value="-"  accesskey="-" /></td></tr>';

        //    }

        //    $("#SearchConditions").append(html);

        //}

        //var html = '<tr ><td >' + SelectedColumn + " = "+value + '</td><td style="width:10px" ><input type="button" id="btRemove" onclick="RemoveCondition()" class="bt1" value="-"  accesskey="-" /></td></tr>';

        //$("#SearchConditions").append(html);







        //var ClosestTr = document.getElementById('btRemove').closest('tr');
        //ClosestTr.remove();
        //var tdContent = ClosestTr.innerText;


        //var searchColumn = tdContent.split('=')[0];
        //var searchValue = "'" + tdContent.split('=')[1].trim() + "%'";

           

        //for (var i = 0; i < Conditions.length; i++) {
    
        //    var c=   Conditions[i].split('LIKE')[0];
        //    var d =  Conditions[i].split('LIKE')[1];


        //    if (searchColumn == c && searchValue == d) {
    
        //        Conditions.splice( i, 1 );


        //    }

        //}




        <%-- for (var i = 0; i < Conditions.length; i++) {
                var individualCondition = Conditions[i].split('LIKE');

                if (individualCondition[0].trim() == searchColumn.trim() && individualCondition[1].trim() == searchValue) {
                    alert(document.getElementById('<%=hdnWhereConditions.ClientID %>').value);--%>

        <%-- if ( document.getElementById('<%=hdnWhereConditions.ClientID %>').value.indexOf(" OR "+Conditions[i]) >0) {
                        document.getElementById('<%=hdnWhereConditions.ClientID %>').value = document.getElementById('<%=hdnWhereConditions.ClientID %>').value.replace(" OR " +Conditions[i], "");
                    }

                    if (document.getElementById('<%=hdnWhereConditions.ClientID %>').value.indexOf(Conditions[i] + " OR ") > 0)
                    {
                        document.getElementById('<%=hdnWhereConditions.ClientID %>').value = document.getElementById('<%=hdnWhereConditions.ClientID %>').value.replace(" OR " + Conditions[i], "");
                    }--%>

        <%--  document.getElementById('<%=hdnWhereConditions.ClientID %>').value = document.getElementById('<%=hdnWhereConditions.ClientID %>').value.replace(Conditions[i], "");
                    alert(document.getElementById('<%=hdnWhereConditions.ClientID %>').value);--%>

        //  }

        //  }




        // var condition = tdContent.replace("=","LIKE")

        // var afterComma = condition.substr(condition.indexOf(",") + 1);

        <%--          var  condition = tdContent.split('=')[0] + " LIKE " + "'"+tdContent.split('=')[1]+"'%";

          if (document.getElementById('<%=hdnWhereConditions.ClientID %>').value.indexOf(condition) > 0) {
              alert(1);
          }--%>


        // document.getElementById('<%=hdnWhereConditions.ClientID %>').value.split('OR');


        // condition = condition + "%'";

        <%--alert(document.getElementById('<%=hdnWhereConditions.ClientID %>').value);
            alert(document.getElementById('<%=hdnWhereConditions.ClientID %>').value.indexOf(condition));

            if (document.getElementById('<%=hdnWhereConditions.ClientID %>').value.indexOf(condition) > 0)
            {
                alert(1);
            }--%>
          
         <%--   if (document.getElementById('<%=hdnWhereConditions.ClientID %>').value.search(condition) > 0) {
                alert(1);
            }--%>
        

        //$('div.deleteMe').click(function(){
        //    alert(333);
        //});

        function RemoveLi(i)
        {debugger;
            Conditions.splice(i,1);

            $('ul li').remove();
            IsRemoveButtonClicked = true;

            // document.getElementById("ulConditions").removeChild();
            MakeListUsingArray();
        }



        function MakeListUsingArray()
        {
            debugger;

           

            //if (isPostBack)
            //{
            //    document.getElementById("ulConditions").removeChild(li);
            //}


            if (  isPostBack && document.getElementById('<%= hdnArray.ClientID %>').value != "" && Conditions.length == 0 && IsRemoveButtonClicked== false) 
            {
                var ArrayContent = document.getElementById('<%= hdnArray.ClientID %>').value ;
                var array = ArrayContent.split(",");
                
                Conditions = array;
            }

            var WhereCondition = [];

            var ul = document.getElementById("ulConditions");
           
            //var list = ul.children.length;
            //alert(list);



            if (  (isPostBack == false || Conditions.length >0)   || (IsAddButtonClicked == true) ) 
            {
                var li = document.createElement("LI");
               

                //if (IsAddButtonClicked == true) 
                //{
                //    IsAddButtonClicked = false;
                //}

            }

            //for (var i = 0; i < Conditions.length; i++)
            //{

            //    var node = document.createElement("LI");
            //    var textnode = document.createTextNode(Conditions[i]);
            //    node.appendChild(textnode);
            //    document.getElementById("ulConditions").appendChild(node);
            //}


            for (var i = 0; i < Conditions.length; i++)
            {
                if ( (isPostBack && IsAddButtonClicked == false) || (IsRemoveButtonClicked==true && IsAddButtonClicked == false) ) 
                {
                    var li = document.createElement("LI");

                   
                }

                li.id = "lstCondition"+i;
              
              //  li.innerHTML = '<span class="conditionli" title="Remove this condition" onclick="RemoveLi('+i+')">-</span>'+" "+ Conditions[i] ;

                li.innerHTML ='<img src="../Images/Deleteicon1.png" title="Remove this condition" onclick="RemoveLi('+i+')"/>'+" "+ Conditions[i] ;
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


           
        }



        function AddCondition() {
            debugger;


            if (($('#<%=ddlColumns.ClientID%>').val().trim() != "--Select--")) {

                if (document.getElementById('<%= txtvalue.ClientID %>').value != "") {

                    var selectoption = document.getElementById("<%=ddlColumns.ClientID %>");
                    var SelectedColumn = selectoption.options[selectoption.selectedIndex].text;
                    var value = document.getElementById('<%= txtvalue.ClientID %>').value;

                    var cndtion = SelectedColumn + " = "+value ;

                    Conditions.push(cndtion)

                    //document.getElementById('<%= hdnArray.ClientID %>').value =  Conditions;

                    IsAddButtonClicked = true;

                    MakeListUsingArray();

                    <%--var selectoption = document.getElementById("<%=ddlColumns.ClientID %>");
                    var SelectedColumn = selectoption.options[selectoption.selectedIndex].text;

                    var li = document.createElement("LI");  
                    var value = document.getElementById('<%= txtvalue.ClientID %>').value;

                    var cndtion = SelectedColumn + " = "+value ;

                    Conditions.push(cndtion);--%>

                    // var html =  ' <li onclick="RemoveLi()">'+cndtion+'</li>'
                    //li.innerHTML = cndtion;
                    //li.onclick = function() {
                    //        this.parentNode.removeChild(this);
                    
                    //            }
                    //document.getElementById("ulConditions").appendChild(li);

                    
                    <%-- var Condition = SelectedColumn + " LIKE '"+value+"%'";
                    Conditions.push( Condition);
                 document.getElementById('<%= hdnWhereConditions.ClientID %>').value = Conditions;--%>

                    //  var html = '<tr ><td >' + SelectedColumn + " = "+value + '</td><td style="width:10px" ><input type="button" id="btRemove" onclick="RemoveCondition()" class="bt1" value="-"  accesskey="-" /></td></tr>';

                    //$("#SearchConditions").append(html);

                    //  Conditions.push(SelectedColumn + "=" + value + "%");

            



                

              <%--  if (document.getElementById('<%= hdnWhereConditions.ClientID %>').value == "") {

                    document.getElementById('<%= hdnWhereConditions.ClientID %>').value = document.getElementById('<%= hdnWhereConditions.ClientID %>').value + SelectedColumn + " LIKE '" + value + "%'";
                }
                else {
                    document.getElementById('<%= hdnWhereConditions.ClientID %>').value = document.getElementById('<%= hdnWhereConditions.ClientID %>').value + " OR " + SelectedColumn + " LIKE '" + value + "%'";
                }--%>


                }

                else {
                    alert("Please enter a value");
                }


            }

            else {
                alert("Please select a column");
            }


        }

        function AddListItemsToArray()
        {
            debugger;

            var list =   document.getElementById("ulConditions");
            var items = list.getElementsByTagName("li");


            for (var i = 0; i < list.length; i++) 
            {
                debugger;

                var liText = items[i].innerHTML;
            }
        }



    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 15px"></div>

    <table class="noBorder">
        <tr>
            <td>

                <img src="../Images/gggg.png" id="UpIcon" style="cursor: pointer; display: none" title="Hide search box" width="20" height="20" align="right" />
                <img src="../Images/Search.png" width="20" height="20" id="searchIcon" style="cursor: pointer" title="Advanced Search" align="right" />


            </td>
        </tr>
    </table>






    <div id="paneldiv" style="width: 100%; display: none;" class="col-lg-12">



        <div class="col-lg-1"></div>

        <div class="col-lg-10" >

<%------------------------ * ADVANCED SEARCH * ------------------%>

            <fieldset style="border-radius: 15px!important;
    border: 2px solid #c7d4f3!important;">

               <legend style="font-family:caviardreams-regular;color:#2196F3;width:20%;" >Advanced Search</legend>

                <div class="col-lg-5">


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
                            <label for="name">Add</label>

                            <img id="imgAddIcon" src="../Images/plus1.png" onclick="return AddCondition();" alt="" style="cursor: pointer; width: 15px; height: 15px;" />
                        </div>

                    </div>





                </div>


                <div class="col-lg-4">

                    <table class="noBorder">
                        <tr>
                            <td>

                                <%-- <asp:BulletedList ID="lstSearchConditions" BulletStyle="Disc" DisplayMode="LinkButton" runat="server">

                                   
                                </asp:BulletedList>--%>

                                <ul id="ulConditions">
                                
                                </ul>



                                <%--<table id="SearchConditions" style="border: none ! important; width: 40%; text-align: right">
                                </table>--%>

                            </td>
                        </tr>

                                

                                <%--<asp:ImageButton ID="imgbtnSearch" runat="server" OnClick="imgbtnSearch_Click" ImageUrl="../Images/Search.png" Style="cursor: pointer; width: 15px; height: 15px;" ToolTip="Search" />--%>

                            
                       
                    </table>

                    
                </div>

               
              <div class="col-lg-3">  <asp:Button ID="btnSearch" runat="server" Class="button" Text="SEARCH" Style="width: 40%" OnClick="btnSearch_Click" /></div>
          </fieldset>

        </div>

        <div class="col-lg-1"></div>

        <%--<table class="noBorder" style="width: 100%; background-color: #c5cdde;">

            <tr>

                <td style="width: 50%">
                    

                </td>

                <td style="width: 50%">
                    

                </td>
            </tr>

        </table>--%>
    </div>



    <%--<div class="col-lg-12">
        <div style="height:15px"></div>
            <div class="col-lg-1" ></div>
            <div class="col-lg-7">
                Column Name  
            </div>

          <div class="col-lg-4" style="width:10%">

              Value 
          </div>

        </div>--%>






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
