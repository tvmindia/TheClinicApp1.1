<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="IndividualReport .aspx.cs" Inherits="TheClinicApp1._1.Report.IndividualReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>

      table {
    border-collapse: collapse;
    border: 1px solid black;
    border-top:1px solid white!important;
    border-right:1px solid black;
    border-left:1px solid black;
}


table th{

     border-top:1px solid black;
     border-right:1px solid black;
     border-left:1px solid black;
     border-bottom:1px solid black;
     font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:16px;
    font-weight:100;
}
table td{
    width:19%;
    height:auto!important;
    padding-left:5px;
    margin:5px 5px 5px 5px 5px;
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:14px;
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

            .footer{
                color:#0e3782;
                text-align:right;
            }

             .header{
                color:#0e3782;
               
            }

             p{
                 font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
                 font-size:32px;

             }
             .Clinicname{
                 font-family:'caviardreams-regular';
             }
             .logo1{
                 margin:15px 15px 15px 15px;
                 width:150px;
             }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    

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




</asp:Content>
