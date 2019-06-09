<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetProgressUserControl.ascx.cs" Inherits="EPM.UI.SetProgress.SetProgressUserControl" %>


<SharePoint:CssRegistration runat="server" Name="/_layouts/15/EPM/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />
<script type="text/javascript" src="/_layouts/15/EPM/SetObjectives.js"></script>

<div id="container" dir="rtl" style="text-align:right" >
<div id="page_head">
    <h1>نسب إنجاز الأهداف الفردية لعام <asp:Label ID="lblActiveYear" runat="server" Text=""></asp:Label>  </h1>
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
                <asp:Label ID="slblEmpRank" runat="server" Text="الدرجة الوظيفية"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmpRank" runat="server" Text=""></asp:Label>
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

<div class="div_gvwSetObjectives" style="width:80% !important;">
<asp:GridView ID="gvwProgress" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" 
    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
    ValidateRequestMode="Disabled" OnRowEditing="gvwProgress_RowEditing" OnRowCancelingEdit="gvwProgress_RowCancelingEdit" OnRowUpdating="gvwProgress_RowUpdating" >
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="المعرف">
            <EditItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
            <ControlStyle Width="20px" />
            <HeaderStyle HorizontalAlign="Center" Width="5%" />
            <ItemStyle HorizontalAlign="Center" Width="5%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="السنة">
            <EditItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ObjYear") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ObjYear") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="5%" />
            <ItemStyle HorizontalAlign="Center" Width="5%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="اسم الهدف">
            <EditItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ObjName") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ObjName") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="35%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="وزن الهدف">
            <EditItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# Eval("ObjWeight","{0:0} %") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# Eval("ObjWeight","{0:0} %") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
            <ItemStyle HorizontalAlign="Center" Width="10%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="نسبة الإنجاز">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AccPercent") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("AccPercent") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="15%" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:CommandField ButtonType="Button" CancelText="تراجع" EditText="تعديل" ShowEditButton="True" ShowHeader="True" UpdateText="تحديث" HeaderStyle-Width="10%" >
<HeaderStyle Width="11%"></HeaderStyle>
        <ItemStyle Width="11%" />
        </asp:CommandField>
    </Columns>
    <EditRowStyle BackColor="White" />
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

<div>
    <h2><asp:Label ID="Label6" runat="server" Text="إضافة ملاحظات أو أعمال إضافية قمت بها : (فريق أو لجنة أو مشاركات اخرى)"></asp:Label></h2>
    <asp:TextBox ID="txtNote1" runat="server" TextMode="MultiLine" Rows="6" Width="50%"></asp:TextBox>
</div>
<div class="div_btnSubmit" style="margin-bottom:30px;">
<asp:Button ID="btnSubmit" runat="server" Text="إرسال" Font-Size="Large" Height="50px" Width="100px" OnClick="btnSubmit_Click" ValidationGroup="vg2" />
</div>

<div runat="server" id="divSuccess"  style="width:50%; background-color: rgb(0, 222, 149) !important; font-size:large; font-weight:bold;" visible ="false">
<asp:Label ID="lblSuccess" runat="server" Text="" ></asp:Label>
</div>

</div>