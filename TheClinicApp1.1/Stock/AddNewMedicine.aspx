<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AddNewMedicine.aspx.cs" Inherits="TheClinicApp1._1.Stock.AddNewMedicine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script>  
      $(document).ready(function () {
                //images that represents medicine name duplication hide and show
                var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
                LnameImage.style.display = "none";
                var errLname = document.getElementById('<%=errorLnames.ClientID %>');
                errLname.style.display = "none";
                });


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


       <div class="grey_sec">
           <ul class="top_right_links"  ><li><a class="save" href="#"><span></span>Save</a></li><li><a class="new" href="#"><span></span>New</a></li></ul>

</div>
           <div class="tab_table">   


          <table class="table"  border="0" style="width:100%">

               <tr>
            <th>Medicine Name</th>
            <th>Medicine Code</th>
            <th>Category</th>
                   <th>Unit</th>
                   <th>Reorder Quantity</th>
          </tr>


        <tr>
          
            <td><asp:TextBox ID="txtmedicineName" runat="server" onchange="CheckMedicineNameDuplication(this)"></asp:TextBox>
                <asp:Image ID="imgWebLnames" runat="server" ToolTip="Login Name is Available" ImageUrl="~/Images/Check.png" Width="10%" Height="10%"  />
                                        
      <asp:Image ID="errorLnames" runat="server" ToolTip="Login Name is Unavailable" ImageUrl="~/Images/newClose.png"  />
            </td>


            
       


      
            <td><asp:TextBox ID="txtCode" runat="server"></asp:TextBox></td>
          
        
            <td><asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true"></asp:DropDownList></td>
       
            <td><asp:TextBox ID="txtUnit" runat="server"></asp:TextBox></td>
            

        
           
            <td><asp:TextBox ID="txtOrderQuantity" runat="server"></asp:TextBox></td>
            
        </tr>
              
    </table>
           </div>


</asp:Content>
