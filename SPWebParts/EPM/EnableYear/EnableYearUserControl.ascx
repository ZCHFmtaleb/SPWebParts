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
        <td style="padding-left:20px;"><h4>  البدء بتفعيل التقييم السنوى لسنة </h4></td>
        <td> <asp:DropDownList ID="ddl_Eval_Year" runat="server" Font-Bold="True"></asp:DropDownList> </td>
        <td style="padding-right:20px;" > 
         <asp:Button ID="btnActivate_Eval_Year" runat="server" Text="تفعيل" Height="25px" Width="50px" OnClick="btnActivate_Eval_Year_Click" Style="padding:0px 0px 0px 0px; font-weight:bold;"/>&nbsp;&nbsp;&nbsp; 
         </td>
    </tr>
</table>
<br />
<table>
    <tr>
        <td style="padding-left:20px;"><h4>  البدء بتفعيل وضع الأهداف لسنة </h4></td>
        <td> <asp:DropDownList ID="ddl_Set_Goals_Year" runat="server" Font-Bold="True"></asp:DropDownList> </td>
        <td style="padding-right:20px;" > <asp:Button ID="btnActivate_Set_Goals_Year" runat="server" Text="تفعيل" Height="25px" Width="50px" OnClick="btnActivate_Set_Goals_Year_Click" Style="padding:0px 0px 0px 0px; font-weight:bold;" /> &nbsp;&nbsp;&nbsp; 
         </td>
    </tr>
</table>
<br />
<table>
    <tr>
        <td style="padding-left:20px;"><h4>  سنة عرض الأهداف (فى حالة عدم وجود عام مفعل): </h4></td>
        <td> <asp:DropDownList ID="ddl_Year_to_display_if_none_active" runat="server" Font-Bold="True"></asp:DropDownList> </td>
        <td style="padding-right:20px;" > <asp:Button ID="btn_Year_to_display_if_none_active" runat="server" Text="حفظ" Height="25px" Width="50px" Style="padding:0px 0px 0px 0px; font-weight:bold;" OnClick="btn_Year_to_display_if_none_active_Click" /> &nbsp;&nbsp;&nbsp; 
         </td>
    </tr>
</table>
<br />
<br />
<h3>الأعوام المفعلة حاليا :</h3>
<div class="div_gvwSetObjectives" style="width:50% !important;">
    <asp:GridView ID="gvw_EPM_Years" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="100%" OnRowCommand="gvw_EPM_Years_RowCommand" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="المرحلة" >
            <HeaderStyle Width="50%" />
            </asp:BoundField>
            <asp:BoundField DataField="Year" HeaderText="السنة" >
            <HeaderStyle Width="25%" />
            </asp:BoundField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="btnYearClosure" runat="server" CausesValidation="false" CommandName="YearClosure" Text="إغلاق" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
                <HeaderStyle Width="25%" />
            </asp:TemplateField>
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

