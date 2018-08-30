<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetObjectivesWPUserControl.ascx.cs" Inherits="SPWebParts.EPM.SetObjectivesWP.SetObjectivesWPUserControl" %>

<style type="text/css">
    .auto-style1 {
        width: 50%;
    }
</style>

<div dir="rtl" align="right" >

<table class="auto-style1">
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

<br />
<br />

<asp:GridView ID="gvwSetObjectives" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="ObjName" HeaderText="اسم الهدف" HeaderStyle-Width="35%" />
        <asp:BoundField DataField="ObjWeight" HeaderText="وزن الهدف" />
        <asp:BoundField DataField="ObjQ" HeaderText="تاريخ تحقيق الهدف" />
        <asp:CommandField ButtonType="Button" CancelText="الغاء" DeleteText="حذف" EditText="تعديل" ShowEditButton="True" UpdateText="تحديث" />
        <asp:CommandField ButtonType="Button" CancelText="الغاء" DeleteText="حذف" EditText="تعديل" ShowDeleteButton="True" UpdateText="تحديث" />
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

<br />
<br />

<div align="center" >
<asp:Button ID="btnSubmit" runat="server" Text="إرسال" Font-Size="Large" Height="50px" Width="100px" />
</div>
</div>
