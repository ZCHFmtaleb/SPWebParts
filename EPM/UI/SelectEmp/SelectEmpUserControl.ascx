<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectEmpUserControl.ascx.cs" Inherits="EPM.UI.SelectEmp.SelectEmpUserControl" %>



<SharePoint:CssRegistration runat="server" Name="/_layouts/15/EPM/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />
<script type="text/javascript" src="/_layouts/15/EPM/SetObjectives.js"></script>

<style type="text/css">
th{
      text-align:center !important; 
    }
</style>

<div id="container" dir="rtl" align="right" >
<div id="page_head">
</div>

<asp:GridView ID="gvwSelectEmp" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" 
    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
    ValidateRequestMode="Disabled" OnSelectedIndexChanged="gvwSelectEmp_SelectedIndexChanged" >
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField HeaderText="اسم الموظف" DataField="EmpName" >
        <HeaderStyle HorizontalAlign="Center" Width="40%" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="EnglishName" HeaderText="الاسم الانجليزى" />
        <asp:BoundField HeaderText="الوظيفة" DataField="EmpJob" >
        <HeaderStyle Width="30%" />
        </asp:BoundField>
        <asp:CommandField ButtonType="Button" HeaderStyle-Width="10%" SelectText="اضف التقييم" ShowSelectButton="True" ShowHeader="True" >
<HeaderStyle Width="10%"></HeaderStyle>
        </asp:CommandField>
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"  />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>


</div>