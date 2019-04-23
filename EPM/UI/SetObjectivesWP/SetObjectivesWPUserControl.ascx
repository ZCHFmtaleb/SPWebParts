<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetObjectivesWPUserControl.ascx.cs" Inherits="EPM.UI.SetObjectivesWP.SetObjectivesWPUserControl" %>


<SharePoint:CssRegistration runat="server" Name="/_layouts/15/EPM/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />
<script type="text/javascript" src="/_layouts/15/EPM/SetObjectives.js"></script>

<div id="container" dir="rtl" style="text-align:right" >

<div style="text-align:left;">
    <asp:Label ID="EmpHierLvl" runat="server" Text="EmpHierLvl" Font-Size="XX-Small" ></asp:Label>
</div>

<div id="page_head">
    <h1 runat="server" id="PageTitle">نموذج وضع الأهداف الفردية لعام   <asp:Label ID="lblActiveYear" runat="server" Text=""></asp:Label></h1>
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
        <%--<tr>
            <td>
                <asp:Label ID="slblEmpRank" runat="server" Text="الدرجة الوظيفية"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmpRank" runat="server" Text=""></asp:Label>
            </td>
        </tr>--%>

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


<div id="container_for_making_blank" runat="server">
 
<div class="divAddGoal" runat="server" id="div_of_AddingGoal">

<div class="Form_Table_css">
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate >
<table>
    <tr>
    <td style="width:36% !important;">التوجه الاستراتيجى</td>
    <td>
        <asp:DropDownList ID="ddlStrDir" runat="server" OnSelectedIndexChanged="ddlStrDir_SelectedIndexChanged" AutoPostBack="True" Width="300px" AppendDataBoundItems="True" DataValueField="ID" DataTextField="Title">
            <asp:ListItem Selected="True" Value="0">اختر التوجه الاستراتيجى</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfv_ddlStrDir" runat="server" Display="Dynamic" ErrorMessage="الرجاء اختيار التوجه الاستراتيجى" ControlToValidate="ddlStrDir" ForeColor="Red" ValidationGroup="vg1"
            InitialValue="0">*</asp:RequiredFieldValidator>
    </td>
        <td colspan="3">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100" DynamicLayout="False">
                <ProgressTemplate>
                    <img src="../_layouts/15/EPM/spinner.gif" alt="جارى التحميل" width="40" height="40" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
    <tr>
        <td>الهدف الرئيسى</td>
        <td>
            <asp:DropDownList ID="ddlPrimaryGoal" runat="server" Width="300px" AppendDataBoundItems="True" DataTextField="Title" DataValueField="ID">
                <asp:ListItem Selected="True" Value="0">اختر الهدف الرئيسى</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv_ddlPrimaryGoal" runat="server" Display="Dynamic" ErrorMessage="الرجاء اختيار الهدف الرئيسى" ControlToValidate="ddlPrimaryGoal" ForeColor="Red" ValidationGroup="vg1"
            InitialValue="0">*</asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlStrDir" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
</div>
<table class="Form_Table_css">
    <tr>
        <td >اسم الهدف الفرعى</td>
        <td ><asp:TextBox ID="txtObjName" runat="server" MaxLength="255" Width="500px" ValidationGroup="vg1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_txtObjName" runat="server" ControlToValidate="txtObjName" Display="Dynamic" ErrorMessage="اسم الهدف مطلوب" ForeColor="Red" ValidationGroup="vg1">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            وزن الهدف</td>
        <td>
            <asp:TextBox ID="txtObjWeight" runat="server" MaxLength="3" Width="50px" ValidationGroup="vg1"></asp:TextBox>
            % 
            <asp:RequiredFieldValidator ID="rfv_txtObjWeight" runat="server" ControlToValidate="txtObjWeight" Display="Dynamic" ErrorMessage="وزن الهدف مطلوب" ForeColor="Red" ValidationGroup="vg1">*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rgv_txtObjWeight" runat="server" ControlToValidate="txtObjWeight" Display="Dynamic" ErrorMessage="لابد ان تكون القيمة المدخلة رقم من 1 الى 100" ForeColor="Red" ValidationGroup="vg1" MaximumValue="100" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
        </td>
    </tr>
    <tr>
        <td>تاريخ تحقيق الهدف</td>
        <td>
            <asp:DropDownList ID="ddlObjQ" runat="server" Width="55px">
                <asp:ListItem Selected="True">Q1</asp:ListItem>
                <asp:ListItem>Q2</asp:ListItem>
                <asp:ListItem>Q3</asp:ListItem>
                <asp:ListItem>Q4</asp:ListItem>
            </asp:DropDownList>

<asp:Button ID="btnAddObjective" runat="server" Text="إضافة هدف" OnClick="btnAddObjective_Click" CssClass="btnAddObjective_css" ValidationGroup="vg1" />

        </td>
    </tr>
    <tr>
        <td colspan="2">* الرجاء تعبئة من 3 الى 7 اهداف فقط</td>
    </tr>
</table>
</div>

<div id="divPercentageTotal" style="font-family: 'Times New Roman', Times, serif !important;">
    <h3>مجموع اوزان الأهداف  :   <asp:Label ID="lbl_PercentageTotal" runat="server" Text="0" BackColor="Pink"></asp:Label> %   
                 <asp:CustomValidator ID="cvld_PercentageTotal" runat="server" ErrorMessage="لابد أن يكون مجموع اوزان الأهداف 100" Display="Dynamic" ForeColor="Red" OnServerValidate="cvld_PercentageTotal_ServerValidate" ValidationGroup="vg2">*</asp:CustomValidator>
                 <asp:CustomValidator ID="cvld_Number_of_Objs" runat="server" ErrorMessage="لابد أن يكون عدد الأهداف من 3 الى 7" Display="Dynamic" ForeColor="Red" OnServerValidate="cvld_Number_of_Objs_ServerValidate" ValidationGroup="vg2">*</asp:CustomValidator>
    </h3>
</div>

<div class="div_gvwSetObjectives" style="width:80% !important;">
<asp:GridView ID="gvwSetObjectives" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnRowDeleting="gvwSetObjectives_RowDeleting" OnRowEditing="gvwSetObjectives_RowEditing" OnRowCancelingEdit="gvwSetObjectives_RowCancelingEdit" OnRowUpdating="gvwSetObjectives_RowUpdating" ValidateRequestMode="Disabled" >
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="StrDirID" HeaderText="StrDir" Visible="False" />
        <asp:BoundField DataField="PrimaryGoalID" HeaderText="PrimaryGoal" Visible="False" />
        <asp:TemplateField HeaderText="التوجه الاستراتيجى">
            <EditItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("StrDirID_x003a__x0627__x0644__x0") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("StrDirID_x003a__x0627__x0644__x0") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="12%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="الهدف الرئيسى">
            <EditItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Eval("PrimaryGoalID_x003a__x0627__x064") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Eval("PrimaryGoalID_x003a__x0627__x064") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="15%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="اسم الهدف">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ObjName") %>' Width="400px"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ObjName") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="35%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="وزن الهدف">
            <EditItemTemplate>
                <asp:TextBox ID="txt_gv_ObjWeight" runat="server" Text='<%# Bind("ObjWeight") %>' ValidationGroup="vg3" MaxLength="3" Width="50px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_gv_txtObjWeight" runat="server" ControlToValidate="txt_gv_ObjWeight" Display="Dynamic" ErrorMessage="" ForeColor="Red" ValidationGroup="vg3">وزن الهدف مطلوب</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rgv_gv_txtObjWeight" runat="server" ControlToValidate="txt_gv_ObjWeight" Display="Dynamic" ErrorMessage="" ForeColor="Red" ValidationGroup="vg3" MaximumValue="100" MinimumValue="1" Type="Integer">رقم من 1 الى 100 فقط</asp:RangeValidator>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# Bind("ObjWeight") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="5%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="الفصل">
            <EditItemTemplate>
                <asp:DropDownList ID="ddlObjQ_gv" runat="server">
                    <asp:ListItem>Q1</asp:ListItem>
                    <asp:ListItem>Q2</asp:ListItem>
                    <asp:ListItem>Q3</asp:ListItem>
                    <asp:ListItem>Q4</asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ObjQ") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="5%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="تعديل">
            <EditItemTemplate>
                <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="تحديث" ValidationGroup="vg3" />
                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="تراجع" />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="تعديل" />
            </ItemTemplate>
            <HeaderStyle Width="14%" />
        </asp:TemplateField>
        <asp:CommandField ButtonType="Button" CancelText="الغاء" DeleteText="حذف" EditText="حذف" ShowDeleteButton="True" UpdateText="تحديث" HeaderText="حذف" ShowHeader="True" >
        <HeaderStyle Width="6%" />
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

<div class="div_btnSubmit" style="margin-bottom:30px;" runat ="server" id="divButtons">
<asp:Button ID="btnSubmit" runat="server" Text="إرسال" Font-Size="Large" Height="50px" Width="100px" OnClick="btnSubmit_Click" ValidationGroup="vg2" />
</div>

<div class="div_val">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="vg1" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ValidationGroup="vg2" />
</div>

<div runat="server" id="divSuccess"  style="width:50%; background-color: rgb(0, 222, 149) !important; font-size:large; font-weight:bold;" visible ="false">
<asp:Label ID="lblSuccess" runat="server" Text="" ></asp:Label>
</div>

</div>

</div>
