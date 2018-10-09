<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnableYearUserControl.ascx.cs" Inherits="SPWebParts.EPM.EnableYear.EnableYearUserControl" %>

<SharePoint:CssRegistration runat="server" Name="/_layouts/15/SPWebParts/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />



<div id="container" dir="rtl" align="right" >
<div id="page_head">
    <h1>تفعيل التقييم السنوى </h1>
</div>

<table>
    <tr>
        <td style="padding-left:20px;"><h4>  البدء بتفعيل التقييم السنوى للعام </h4></td>
        <td> <asp:DropDownList ID="ddlYear" runat="server" Font-Bold="True"></asp:DropDownList> </td>
        <td style="padding-right:20px;" > 
         <asp:Button ID="btnActivate" runat="server" Text="تفعيل" Font-Size="Large" Height="50px" Width="100px" />&nbsp;&nbsp;&nbsp; 
         <asp:Button ID="btnClose" runat="server" Text="إغلاق" Font-Size="Large" Height="50px" Width="100px" />  
        </td>
    </tr>
</table>
<br />
<table>
    <tr>
        <td style="padding-left:20px;"><h4>  البدء بتفعيل وضع الأهداف للعام </h4></td>
        <td> <asp:DropDownList ID="ddlYear_Set_Goals" runat="server" Font-Bold="True"></asp:DropDownList> </td>
        <td style="padding-right:20px;" > <asp:Button ID="btnActivate_Set_Goals" runat="server" Text="تفعيل" Font-Size="Large" Height="50px" Width="100px" /> &nbsp;&nbsp;&nbsp; 
         <asp:Button ID="btnClose_Set_Goals" runat="server" Text="إغلاق" Font-Size="Large" Height="50px" Width="100px" />     
        </td>
    </tr>
</table>
<br />
<br />
<div class="div_gvwSetObjectives" style="width:50% !important;">
    <asp:GridView ID="gvw_EPM_Years" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="400px" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="year" HeaderText="السنة" />
            <asp:BoundField DataField="status" HeaderText="المرحلة" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" Font-Bold="True" Font-Size="Large" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
</div>

</div>

