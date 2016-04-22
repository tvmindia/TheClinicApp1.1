<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="OutOfStock.aspx.cs" Inherits="TheClinicApp1._1.Stock.OutOfStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <asp:GridView ID="gvOutOfStock1" runat="server"  AutoGenerateColumns="False" >
                                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                    <Columns>
                                        <%--  <asp:BoundField DataField="MedicineID" HeaderText="MedicineID" />--%>
                                        <asp:BoundField DataField="MedicineName" HeaderText="Medicine Name" />
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                          <asp:BoundField DataField="CategoryName" HeaderText="Category Name"/>
                                        <asp:BoundField DataField="Qty" HeaderText="Existing Quantity" ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="ReOrderQty" HeaderText="ReOrder Quantity" ItemStyle-HorizontalAlign="Right" />
                                      
                                    </Columns>
                                    
                                </asp:GridView>

</asp:Content>
