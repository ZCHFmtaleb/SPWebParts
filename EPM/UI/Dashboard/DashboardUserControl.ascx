<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardUserControl.ascx.cs" Inherits="EPM.UI.Dashboard.DashboardUserControl" %>


<SharePoint:CssRegistration runat="server" Name="/_layouts/15/EPM/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />

<div id="container" dir="rtl" style="text-align:right" >
<div id="div_For_Hiding_Mode" runat="server">
<div class="div_gvwSetObjectives" style="width:75% !important;">
<h3><asp:Label ID="lblActiveYear" runat="server" Text=""></asp:Label></h3>
 <asp:GridView ID="gvw_Dashboard" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
     BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  Width="100%" OnPageIndexChanging="gvw_Dashboard_PageIndexChanging" PageSize="30" OnRowDataBound="gvw_Dashboard_RowDataBound" >
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="EnglishName" HeaderText="الموظف" ReadOnly="True">
        <HeaderStyle Width="20%" />
        </asp:BoundField>
        <asp:BoundField DataField="ArabicName" HeaderText="الاسم العربى" ReadOnly="True">
        <HeaderStyle Width="20%" />
        </asp:BoundField>
        <asp:BoundField DataField="Department" HeaderText="الإدارة" ReadOnly="True">
        <HeaderStyle Width="20%" />
        </asp:BoundField>
        <asp:BoundField HeaderText="البريد الالكترونى" ReadOnly="True" DataField="Email">
        <HeaderStyle Width="20%" />
        </asp:BoundField>
        <asp:BoundField DataField="Status" HeaderText="الحالة" ReadOnly="True" SortExpression="Status" >
        <HeaderStyle Width="10%" />
        </asp:BoundField>
        <asp:BoundField DataField="EmpHierLvl" HeaderText="درجة التسلسل الإدارى" ReadOnly="True">
        <HeaderStyle Width="10%" />
        </asp:BoundField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</div>
    </div>
</div>
