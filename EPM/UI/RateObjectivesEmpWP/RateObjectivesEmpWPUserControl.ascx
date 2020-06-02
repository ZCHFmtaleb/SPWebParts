<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RateObjectivesEmpWPUserControl.ascx.cs" Inherits="EPM.UI.RateObjectivesEmpWP.RateObjectivesEmpWPUserControl" %>

<SharePoint:CssRegistration runat="server" Name="/_layouts/15/EPM/EPMStyle.css" After="/Style%20Library/css/ShareBoot.css" />

<div id="container" dir="rtl" style="text-align:right" >

<div style="text-align:left;">
    <asp:Label ID="lblEmpRank" runat="server" Text="lblEmpRank" Font-Size="XX-Small" ></asp:Label>
</div>

<div id="page_head">
<h1 runat="server" id="PageTitle">تقييم الأهداف والكفاءات لعام <asp:Label ID="lblActiveYear" runat="server" Text=""></asp:Label></h1>
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

<h3 runat="server" visible="false" id="lblNoObjectivesFound_Warning"> عذرا لم يتم العثور على أية أهداف خاصة بالموظف للسنة المذكورة  </h3>
<asp:Label ID="lblProgressNotSet_Warning" runat="server" Text="تنبيه : لم يقم الموظف بوضع نسب إنجاز الأهداف بعد. يرجى التواصل معه قبل إجراء التقييم" BackColor="#FFFFCC" Visible="False"></asp:Label>

<div class="div_gvwSetObjectives" style="width:50% !important;">
<asp:GridView ID="gvwRate" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Width="100%"
	BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
	ValidateRequestMode="Disabled" >
	<AlternatingRowStyle BackColor="White" />
	<Columns>
		<asp:TemplateField HeaderText="المعرف" Visible="False">
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
		<asp:TemplateField HeaderText="الهدف الفرعى">
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
				<asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("AccPercent","{0:0} %") %>'></asp:TextBox>
			</EditItemTemplate>
			<ItemTemplate>
				<asp:Label ID="Label1" runat="server" Text='<%# Eval("AccPercent","{0:0} %") %>'></asp:Label>
			</ItemTemplate>
			<HeaderStyle Width="15%" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:TemplateField>
		<asp:TemplateField HeaderText="تقييم المدير المباشر">
			<EditItemTemplate>
				<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
			</EditItemTemplate>
			<ItemTemplate>
				<asp:DropDownList ID="ddlObjRating" runat="server" Width="60px" CssClass="ObjRate" SelectedValue='<%# String.IsNullOrEmpty(Eval("AccRating").ToString()) ? "4" : Eval("AccRating") %>' >
					<asp:ListItem>1</asp:ListItem>
					<asp:ListItem>2</asp:ListItem>
					<asp:ListItem>3</asp:ListItem>
					<asp:ListItem>4</asp:ListItem>
					<asp:ListItem>5</asp:ListItem>
				</asp:DropDownList>
			</ItemTemplate>
			<HeaderStyle Width="15%" />
			<ItemStyle Width="15%"  HorizontalAlign="Center"/>
		</asp:TemplateField>
		<asp:CommandField ButtonType="Button" CancelText="تراجع" EditText="تعديل" ShowEditButton="True" ShowHeader="True" UpdateText="تحديث" HeaderStyle-Width="10%" Visible="False" >
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
<h4 runat="server" id="header1">ملاحظات أو أعمال إضافية</h4>
<asp:TextBox ID="txtNote1" runat="server" TextMode="MultiLine" Rows="6" Width="50%" ReadOnly="True"></asp:TextBox>   

<h2 runat="server" id="header2">الكفاءات </h2>
<div class="div_gvwSetObjectives" style="width:50% !important;">
<asp:Label ID="lbl_invalid_rank" runat="server" Text="تعذر تحميل الكفاءات المناسبة نظرا لعدم وجود الدرجة الوظيفية فى سجل الموظف" Visible="False"></asp:Label>
<asp:GridView ID="gvw_Std_Skills" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False"  BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvw_Std_Skills_RowDataBound" Width="100%"   >
		<AlternatingRowStyle BackColor="White" />
		<Columns>
			<asp:BoundField DataField="Title" HeaderText="مسمى الكفاءة">
			<HeaderStyle Width="50%" />
			<ItemStyle Width="50%" />
			</asp:BoundField>
			<asp:TemplateField HeaderText="تقييم المدير المباشر">
				<EditItemTemplate>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:DropDownList ID="ddl_Std_Skill_Rating" runat="server" Width="60px" CssClass="SkillRate">
					<asp:ListItem>1</asp:ListItem>
					<asp:ListItem>2</asp:ListItem>
					<asp:ListItem>3</asp:ListItem>
					<asp:ListItem>4</asp:ListItem>
				</asp:DropDownList>
				</ItemTemplate>
                <ItemStyle HorizontalAlign="Center"/>
			</asp:TemplateField>
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
</div>

<h2 style="color:blue !important;text-decoration:underline;">نتيجة التقييم :</h2>
<h5>	استناداً إلى متطلبات الوظيفة، يرجى الإشارة إلى درجة التقييم المناسب لأداء الموظف خلال الفترة المشمولة بالتقرير:</h5>

<div class="div_gvwSetObjectives" style="width:50% !important;">
<table class="BorderedTable" style="border-style: solid; border-width: thin; text-align:center;width:100%" >
		<tr style="background-color:#507CD1; Color:White;">
			<th style="border:1px solid black !important;">متوسط تقدير الاهداف</th>
			<th style="border:1px solid black !important;">متوسط تقدير الكفاءات</th>
			<th style="border:1px solid black !important;">النتيجة النهائية </th>
			<th style="border:1px solid black !important;">مسمى الدرجة</th>
		</tr>
		<tr>
			<td style="border:1px solid black !important;" id="Avg_of_ObjRates">4</td>
			<td style="border:1px solid black !important;" id="Avg_of_SkillRates" >4</td>
			<td style="border:1px solid black !important;" id="Final_Result">4</td>
			<td style="border:1px solid black !important;" id="Commentary" >أداء عالي الكفــاءة / يفوق التوقعات</td>
		</tr>
 </table>
</div>


<h5>
إذا كانت النتيجة النهائية  (1) أو (5) أعلاه، يرجى توضيح الأسباب:
</h5>
<asp:TextBox ID="txtNote_ReasonForRating1or5" runat="server" TextMode="MultiLine" Rows="6" Width="50%"></asp:TextBox>
<h5>
	التوصيات والبرامج التدريبية الضرورية لزيادة الإنتاجية وتطوير الأداء ( اختياري ) :
</h5>
<asp:TextBox ID="txtNote_RecommendedCourses" runat="server" TextMode="MultiLine" Rows="6" Width="50%"></asp:TextBox>


<div class="div_btnSubmit"  style="margin-bottom:30px;">
<asp:Button ID="btnSubmit" runat="server" Text="إرسال" Font-Size="Large" Height="50px" Width="100px" OnClick="btnSubmit_Click"  />
<asp:Button ID="btnHRApprove" runat="server" Text="إعتماد لجنة الموارد البشرية" Font-Size="Medium" Height="50px" Width="200px" visible="false" OnClick="btnHRApprove_Click" BackColor="#C1FFC1"  />
</div>

<div runat="server" id="divSuccess"  style="width:50%; background-color: rgb(0, 222, 149) !important; font-size:large; font-weight:bold;" visible ="false">
<asp:Label ID="lblSuccess" runat="server" Text="" ></asp:Label>
</div>

</div>

<script type="text/javascript" src="/_layouts/15/EPM/SetObjectives.js"></script>