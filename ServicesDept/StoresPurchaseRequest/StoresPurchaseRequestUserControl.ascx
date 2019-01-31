<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StoresPurchaseRequestUserControl.ascx.cs" Inherits="ServicesDept.StoresPurchaseRequest.StoresPurchaseRequestUserControl" %>


<SharePoint:CssRegistration runat="server" Name="/_layouts/15/SPWebParts/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />
  <link rel="stylesheet" href="/Style%20Library/jQueryUI/base/jquery-ui.css">
  <script type="text/javascript" src="/Style%20Library/jQueryUI/base/jquery-ui.js"></script>
    <style type="text/css">
    .ui-tabs { direction: rtl; }
    .ui-tabs .ui-tabs-nav li.ui-tabs-selected,
    .ui-tabs .ui-tabs-nav li.ui-state-default {float: right; }
    .ui-tabs .ui-tabs-nav li a { float: right; }
    </style>


  <script type="text/javascript">
      $(function () {
    $( "#tabs" ).tabs();
  } );
  </script>




<div id="container" dir="rtl" align="right"  style="border:none !important;">

<div id="page_head">
    <h1 runat="server" id="PageTitle">طلبات إدارة الخدمات </h1>
</div>

<div id="tabs">
  <ul>
    <li><a href="#tabs-1">طلب مخازن</a></li>
    <li><a href="#tabs-2">خدمات عامة وصيانة</a></li>
   <li><a href="#tabs-3">طلب نظافة</a></li>
   <li><a href="#tabs-4">طلب مواصلات</a></li>
    <li><a href="#tabs-5">طلب ضيافة</a></li>
   <li><a href="#tabs-6">طلبات أخرى</a></li>
  </ul>
  <div id="tabs-1">
    <div class="divAddGoal" runat="server" id="div_of_AddingGoal">
<div class="Form_Table_css">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate >
<table>
    <tr>
    <td style="width:36% !important;">مجموعة الصنف</td>
    <td>
        <asp:DropDownList ID="ddlCat" runat="server"  AutoPostBack="True" Width="300px" OnSelectedIndexChanged="ddlCat_SelectedIndexChanged" >
        </asp:DropDownList>
    </td>
        <td colspan="3">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100" DynamicLayout="False">
                <ProgressTemplate>
                    <img src="../_layouts/15/SPWebParts/spinner.gif" alt="جارى التحميل" width="40" height="40" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
    <tr>
        <td>الصنف المطلوب</td>
        <td>
            <asp:DropDownList ID="ddlPrimaryGoal" runat="server" Width="300px" AppendDataBoundItems="True" ValidationGroup="vg1">
                <asp:ListItem Selected="True" Value="0">اختر الصنف المطلوب</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv_ddlPrimaryGoal" runat="server" Display="Dynamic" ErrorMessage="الرجاء اختيار الصنف المطلوب" ControlToValidate="ddlPrimaryGoal" ForeColor="Red" ValidationGroup="vg1"
            InitialValue="0">*</asp:RequiredFieldValidator>
        </td>
    </tr>
</table>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlCat" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
<table class="Form_Table_css">
    
    <tr>
        <td>
            الكمية</td>
        <td>
            <asp:TextBox ID="txtQuantity" runat="server" MaxLength="3" Width="50px" ValidationGroup="vg1" Text="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_txtQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="الرجاء ادخال الكمية المطلوبة" ForeColor="Red" ValidationGroup="vg1">*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rgv_txtQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="لابد ان تكون القيمة المدخلة رقما صحيحا من 1 إلى 999" ForeColor="Red" ValidationGroup="vg1" MaximumValue="999" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
        </td>
    </tr>
    <tr>
        <td >ملاحظات</td>
        <td >
            <asp:TextBox ID="txtNotes" runat="server" MaxLength="255" Width="500px" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnAddItem" runat="server" Text="إضافة" OnClick="btnAddItem_Click"  ValidationGroup="vg1" Font-Size="Large" Height="50px" Width="100px" style="margin-top:20px;" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="vg1" />
        </td>
    </tr>
</table>

</div>
</div>

<div  class="div_gvwSetObjectives">
<asp:GridView ID="gvw_Requested_Items" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"   ValidateRequestMode="Disabled" OnRowCancelingEdit="gvw_Requested_Items_RowCancelingEdit" OnRowDeleting="gvw_Requested_Items_RowDeleting" OnRowEditing="gvw_Requested_Items_RowEditing" OnRowUpdating="gvw_Requested_Items_RowUpdating" >
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="ItemGeneralName" HeaderText="الصنف" ReadOnly="True" />
        <asp:TemplateField HeaderText="الكمية">
            <EditItemTemplate>
                 <asp:TextBox ID="txt_gv_Quantity" runat="server" Text='<%# Bind("Quantity") %>' ValidationGroup="vg3" MaxLength="3" Width="50px"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfv_txtQuantity" runat="server" ControlToValidate="txt_gv_Quantity" Display="Dynamic" ErrorMessage="الكمية" ForeColor="Red" ValidationGroup="vg3">*</asp:RequiredFieldValidator>
                 <asp:RangeValidator ID="rgv_txtQuantity" runat="server" ControlToValidate="txt_gv_Quantity" Display="Dynamic" ErrorMessage="رقم من 1 إلى 999" ForeColor="Red" ValidationGroup="vg3" MaximumValue="999" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="5%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ملاحظات">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Notes") %>' Width="400px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="35%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="تعديل">
            <EditItemTemplate>
                <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="تحديث" ValidationGroup="vg3" />
                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="تراجع" />
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" ValidationGroup="vg3" />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="تعديل" />
            </ItemTemplate>
            <HeaderStyle Width="19%" />
        </asp:TemplateField>
        <asp:CommandField ButtonType="Button" CancelText="الغاء" DeleteText="حذف" EditText="حذف" ShowDeleteButton="True" UpdateText="تحديث" HeaderText="حذف" ShowHeader="True" >
        <HeaderStyle Width="6%" />
        </asp:CommandField>
    </Columns>
    <EditRowStyle BackColor="White" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"  />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</div>
<asp:Button ID="btnSubmit" runat="server" Text="إرسال" Font-Size="Large" Height="50px" Width="100px" OnClick="btnSubmit_Click" ValidationGroup="vg2" style="margin-top:20px;" />
  </div>
  <div id="tabs-2">
    <p>طلب خدمات عامة وصيانة</p>
  </div>
  <div id="tabs-3">
    <p> طلب نظافة</p>
  </div>
    <div id="tabs-4">
    <p> طلب مواصلات</p>
  </div>
    <div id="tabs-5">
    <p> طلب ضيافة</p>
  </div>
    <div id="tabs-6">
    <p>طلبات أخرى</p>
  </div>
</div>



<div runat="server" id="divSuccess"  style="width:50%; background-color: rgb(0, 222, 149) !important; font-size:large; font-weight:bold;" visible ="false">
<asp:Label ID="lblSuccess" runat="server" Text="" ></asp:Label>
</div>
</div>
