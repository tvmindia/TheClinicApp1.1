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
    </style>

    <%--Style ANd Script Files OF CAlenderControl--%>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/jquery-ui.js"></script>


    <script>

        $(document).ready(function () {

          
        });


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <%-- <div class="grey_sec">
                               
                                <ul class="top_right_links">
                                    <li>
                                        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="button1" OnClientClick="return PrintPanel();" />
                                    </li>
                                   
                                </ul>
                            </div>--%>
          

           <%--<a class="btn btn-primary button" id="addBtn" onclick="return PrintPanel();"><span>Print</span></></a>--%>
         
          
    
    
         
   

    <%--<asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary button"  OnClientClick="return PrintPanel();" />--%>


    <br />
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

</asp:Content>
