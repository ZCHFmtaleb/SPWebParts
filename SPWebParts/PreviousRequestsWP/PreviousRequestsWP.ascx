<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreviousRequestsWP.ascx.cs" Inherits="SPWebParts.PreviousRequestsWP.PreviousRequestsWP" %>

<script type="text/javascript">
    $(document).ready(function () {
        $('div.row').hide();
    });
</script>

<style type="text/css">
    .s4-breadcrumb ul {
        display: none !important;
    }
    .centerHeaderText th {
        text-align: center;
    }
</style>

<asp:GridView ID="grdPreviousRequests" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText">
     <HeaderStyle  HorizontalAlign="Right" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="معرف الطلب">
        <HeaderStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="_x0627__x0644__x0625__x0633__x06" HeaderText="الاسم العربى" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="EIDCardNumber" HeaderText="رقم الهوية" >
        <HeaderStyle HorizontalAlign="Center" Width="150px" />
        </asp:BoundField>
        <asp:BoundField DataField="Created" HeaderText="تاريخ الإنشاء" DataFormatString="{0: dd/MMM/yyyy}" >
        <HeaderStyle Width="150px" />
        </asp:BoundField>
        <asp:BoundField DataField="NewColumn1" HeaderText="حالة الطلب" >
        <HeaderStyle Width="150px" />
        </asp:BoundField>
        <asp:HyperLinkField Target="_blank" Text="عرض السجل الكامل" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="http://zf-sp/orgchart/ProgramsDepartment/Lists/AidRequests/Item/displayifs.aspx?ID={0}">
        <HeaderStyle Width="150px" />
        </asp:HyperLinkField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" HorizontalAlign="Center" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>