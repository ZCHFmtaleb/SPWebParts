<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetObjectivesWPUserControl.ascx.cs" Inherits="SPWebParts.EPM.SetObjectivesWP.SetObjectivesWPUserControl" %>

<SharePoint:CssRegistration runat="server" Name="/_layouts/15/SPWebParts/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />
<script type="text/javascript" src="/_layouts/15/SPWebParts/SetObjectives.js"></script>

<div id="container" dir="rtl" align="right" >
<div id="page_head">
    <h1>نموذج وضع الأهداف الفردية لعام  <span class="Next_Year"></span> </h1>
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

<div class="divAddGoal">
<table class="Form_Table_css">
    <tr>
        <td>اسم الهدف</td>
        <td>وزن الهدف</td>
        <td>تاريخ تحقيق الهدف</td>
        <td rowspan="2" valign="bottom">

<asp:Button ID="btnAddObjective" runat="server" Text="إضافة هدف" OnClick="btnAddObjective_Click" />

        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtObjName" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtObjWeight" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:DropDownList ID="ddlObjQ" runat="server">
                <asp:ListItem Selected="True">Q1</asp:ListItem>
                <asp:ListItem>Q2</asp:ListItem>
                <asp:ListItem>Q3</asp:ListItem>
                <asp:ListItem>Q4</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
</div>

<div id="divPercentageTotal" style="font-family: 'Times New Roman', Times, serif !important;">
    <h3>مجموع اوزان الأهداف  :   <asp:Label ID="lbl_PercentageTotal" runat="server" Text="0" BackColor="Pink"></asp:Label> % </h3>
</div>

<div class="div_gvwSetObjectives">
<asp:GridView ID="gvwSetObjectives" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnRowDeleting="gvwSetObjectives_RowDeleting" >
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="ObjName" HeaderText="اسم الهدف" HeaderStyle-Width="35%" >
<HeaderStyle Width="40%" ></HeaderStyle>
        </asp:BoundField>
        <asp:BoundField DataField="ObjWeight" HeaderText="وزن الهدف" >
        <HeaderStyle  Width="15%" />
        </asp:BoundField>
        <asp:BoundField DataField="ObjQ" HeaderText="تاريخ تحقيق الهدف" >
        <HeaderStyle Width="15%" />
        </asp:BoundField>
        <asp:CommandField ButtonType="Button" CancelText="الغاء" DeleteText="حذف" EditText="تعديل" ShowEditButton="True" UpdateText="تحديث" HeaderText="تعديل" ShowHeader="True" >
        <HeaderStyle Width="15%" />
        </asp:CommandField>
        <asp:CommandField ButtonType="Button" CancelText="الغاء" DeleteText="حذف" EditText="حذف" ShowDeleteButton="True" UpdateText="تحديث" HeaderText="حذف" ShowHeader="True" >
        <HeaderStyle Width="15%" />
        </asp:CommandField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</div>




<div class="div_btnSubmit" >
<asp:Button ID="btnSubmit" runat="server" Text="إرسال" Font-Size="Large" Height="50px" Width="100px" />
</div>
</div>
