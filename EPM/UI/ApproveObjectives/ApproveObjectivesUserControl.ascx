<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApproveObjectivesUserControl.ascx.cs" Inherits="EPM.UI.ApproveObjectives.ApproveObjectivesUserControl" %>



<SharePoint:CssRegistration runat="server" Name="/_layouts/15/EPM/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />
<script type="text/javascript" src="/_layouts/15/EPM/SetObjectives.js"></script>

<div id="container" dir="rtl" style="text-align:right">

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

<div class="div_gvwSetObjectives" style="width:80% !important;">
<asp:GridView ID="gvwSetObjectives" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
    ValidateRequestMode="Disabled" >
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="StrDir" HeaderText="StrDir" Visible="False" />
        <asp:BoundField DataField="PrimaryGoal" HeaderText="PrimaryGoal" Visible="False" />
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
        <asp:BoundField DataField="ObjYear" HeaderText="السنة" ReadOnly="True">
        <HeaderStyle Width="10%" />
        </asp:BoundField>
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


<div runat="server" id ="div_Mods"> 
<div id="div_Required_Mods">
<asp:Label ID="lblRequired_Mods" runat="server" Text="فى حالة عدم الموافقة على الأهداف المذكورة (طلب تعديلات) ، يرجى ذكر الملاحظات/التعديلات المطلوبة :" ></asp:Label>
</div>
<div>
    <asp:TextBox ID="txtRequired_Mods" runat="server" Rows="8" TextMode="MultiLine" Width="50%" ValidationGroup="vgMod" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfv_txtRequired_Mods" runat="server" ControlToValidate="txtRequired_Mods" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="vgMod">الرجاء ذكر التعديلات المطلوبة</asp:RequiredFieldValidator>
</div>
<asp:Button ID="btnApprove" runat="server" Text="اعتماد" Font-Size="Large" Height="50px" Width="100px" OnClick="btnApprove_Click"  />
<asp:Button ID="btnReject" runat="server" Text="طلب تعديلات" Height="50px" Width="100px"  ValidationGroup="vgMod" style="margin-right:75px !important;font-size:small !important;" Font-Bold="True" OnClick="btnReject_Click"/>
<asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" ValidationGroup="vgMod" />
</div>

<div runat="server" id="divSuccess"  style="width:50%; background-color: rgb(0, 222, 149) !important; font-size:large; font-weight:bold;" visible ="false">
<asp:Label ID="lblSuccess" runat="server" Text="" ></asp:Label>
</div>

</div>


