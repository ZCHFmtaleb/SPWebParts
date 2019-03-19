<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StoresRequestViewUserControl.ascx.cs" Inherits="ServicesDeptTabs.StoresRequestView.StoresRequestViewUserControl" %>

<style type="text/css">
    .auto-style1 {
        text-align: center;
    }
</style>

<SharePoint:CssRegistration runat="server" Name="/_layouts/15/ServicesDeptTabs/ServicesDeptTabsStyle.css" After="/Style%20Library/css/ShareBoot.css" />
<script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
<script type="text/javascript" src="/_layouts/15/sp.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/JSOM.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/Scripts/sprestlib-ui.bundle.js"></script>

<div id="container" dir="rtl"  runat="server">

    <table class="tbl_EmpInfo">
        <tr>
            <td class="c1">مقدم الطلب :</td>
            <td><asp:Label ID="lblEmpName" runat="server" Text="lblEmpName"></asp:Label></td>
            <td class="c3" rowspan="3">حالة الطلب :</td>
            <td class="c4" rowspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="c1">الإدارة :</td>
            <td><asp:Label ID="lblDept" runat="server" Text="lblDept"></asp:Label></td>
        </tr>
        <tr>
            <td class="c1">تاريخ الطلب :</td>
            <td><asp:Label ID="lbl_ReqDate" runat="server" Text="lbl_ReqDate"></asp:Label></td>
        </tr>
    </table>

    <asp:GridView ID="gvw_Items" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowHeaderWhenEmpty="True" BorderColor="Black" BorderStyle="Solid" CssClass="gvw_Items">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ItemGeneralName" HeaderText="اسم الصنف ">
            <HeaderStyle Width="50%" />
            </asp:BoundField>
            <asp:BoundField DataField="Quantity" HeaderText="الكمية">
            <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Notes" HeaderText="ملاحظات">
            <HeaderStyle Width="40%" />
            </asp:BoundField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" Font-Bold="True" Height="30px" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
   
</div>






<p class="auto-style1">
                <asp:Button ID="btnDMapprove" runat="server" Text="موافقة" OnClick="btnDMapprove_Click" />
                <asp:Button ID="btn_SD_approve" runat="server" Text="موافقة إدارة الخدمات" OnClick="btn_SD_approve_Click"  />
                </p>







