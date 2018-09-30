<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RateObjectivesEmpWPUserControl.ascx.cs" Inherits="SPWebParts.EPM.RateObjectivesEmpWP.RateObjectivesEmpWPUserControl" %>

<SharePoint:CssRegistration runat="server" Name="/_layouts/15/SPWebParts/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />


<div id="container" dir="rtl" align="right" >

<div id="page_head">
    <h1>نموذج تقييم الأهداف الفردية والكفاءات لعام  <span class="Next_Year"></span> </h1>
    <h2>الحالة : <asp:Label ID="lblStatus" runat="server" Text="لم يتم التقييم" ForeColor="Blue"></asp:Label> </h2>
</div>

<div id="divEmpInfo" class="divEmpInfo">
<table class="tbl_EmpInfo">
    <tr>
        <td>
        <asp:Label ID="slblEmpName" runat="server" Text="اسم الموظف"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="slblEmpJob" runat="server" Text="الوظيفة"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblEmpJob" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="slblEmpDept" runat="server" Text="الإدارة"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblEmpDept" runat="server" Text=""></asp:Label>
        </td>
    </tr>
        <tr>
        <td>
            <asp:Label ID="slblEmpDM" runat="server" Text="المدير المباشر"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblEmpDM" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
</div>

</div>
