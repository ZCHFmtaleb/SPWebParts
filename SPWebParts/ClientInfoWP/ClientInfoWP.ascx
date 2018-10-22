<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientInfoWP.ascx.cs" Inherits="SPWebParts.ClientInfoWP.ClientInfoWP" %>


<script src="../_layouts/15/SPWebParts/jQuery.print.js" type="text/javascript"></script>

<script type="text/javascript">
	$(document).ready(function () {
		//$('div.row').hide();
	});

	function PrintdvPrint() {
		$('#<%= dvPrint.ClientID %>').print();
	}
</script>





<style type="text/css">
	
	 .s4-breadcrumb ul {
		display: none !important;
	} 

	 .ms-webpartPage-root {
		 border-spacing: 0px !important;
	 }
	   .ms-webpartzone-cell {
		 margin: 0px !important;
	 }
	.auto-style1 {
		width: 70%;
		border: none;
		margin-right:15%;
		margin-left:15%;
		margin-top:0px;
		margin-bottom:0px;
	}
	 .auto-style1 td{
	padding-right:5% !important;   
	padding-bottom : 10px !important;
	}

	.auto-style2 {
		/*width: 14%;*/
	}
	.auto-style3 td{
	   margin:0px 0px 0px 0px !important;
	   padding:0px 0px 0px 0px !important;
	}
	.txtPhoneStyle {
		direction:ltr;
	}
	.auto-style3 {
		/*width: 100%;*/
	}
	.auto-style5 {
		/*width: 20%;*/
	}
	.auto-style6 {
		/*width: 30%;*/
	}
	.auto-style7 {
		width: 100%;
		font-size:x-small;
		border-spacing:0;     
		border-collapse:collapse;

	}

	.auto-style7 td {
	border: 1px solid black;
	padding-right: 5px;
	}
	
 p.MsoNormal
	{margin-bottom:.0001pt;
	text-align:right;
	direction:rtl;
	unicode-bidi:embed;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
		margin-left: 0in;
		margin-right: 0in;
		margin-top: 0in;
	}
	</style>

<asp:Label ID="lblSuccess" runat="server" Visible="False" Font-Size="Large"></asp:Label>
<%--<asp:HyperLink ID="lnkRequestPage" runat="server" Visible="False" Font-Size="Large">[lnkRequestPage]</asp:HyperLink>--%>

<asp:Button ID="btnPrint" runat="server" Text="طباعة" Width="150px" BackColor="#E1F0FF" BorderColor="#B9DCFF" Font-Size="Large" Height="50px" Visible="False" UseSubmitBehavior="False" OnClientClick="PrintdvPrint()" CausesValidation="False" />

<div id="divContainer" runat="server">
<table align="right" class="auto-style1" dir="rtl">
	<tr>
		<td class="auto-style2">الاسم : </td>
		<td class="auto-style6">
			<asp:TextBox ID="txtArabicFullName" runat="server"></asp:TextBox>
		</td>
	  
		<td class="auto-style5">
			الجنسية :</td>
		<td class="auto-style3">
			<asp:TextBox ID="txtNationality" runat="server"></asp:TextBox>
		</td>
		  <td rowspan="24" valign="top">
			<asp:Image ID="imgPhotography" runat="server" />
		</td>
	</tr>
	<tr>
		<td class="auto-style2">الحالة الاجتماعية :</td>
		<td class="auto-style6">
			<asp:TextBox ID="txtMaritalStatus" runat="server"></asp:TextBox>
		</td>
	  
		<td class="auto-style5">
			العمر :</td>
		<td class="auto-style3">
			<asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
			سنة</td>
	</tr>
	<tr>
		<td class="auto-style2">رقم الهوية : 
		</td>
		<td class="auto-style3" colspan="3" >
			<asp:TextBox ID="txtIDNumber" runat="server"></asp:TextBox>
		</td>
	  
	</tr>
	<tr>
		<td class="auto-style2">عدد افراد الأسرة :</td>
		<td class="auto-style6">
			<asp:TextBox ID="txtFamilySize" runat="server"></asp:TextBox>
		</td>
	  
		<td class="auto-style5">
			الوظيفة :</td>
		<td class="auto-style3">
			<asp:TextBox ID="txtJob" runat="server"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="auto-style2">مدة الإقامة بالدولة : </td>
		<td class="auto-style6">
			<asp:TextBox ID="txtResidencyYears" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
		&nbsp;سنة</td>
	  
		<td class="auto-style5">
			عنوان السكن :</td>
		<td class="auto-style3">
			<asp:TextBox ID="txtHomeAddress" runat="server"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="auto-style2">رقم الجوال  : </td>
		<td class="auto-style6">
			<asp:TextBox ID="txtPhone" runat="server" CssClass="txtPhoneStyle" Columns="15" MaxLength="15"></asp:TextBox>
		</td>
	  
		<td class="auto-style5">
			هاتف المنزل:</td>
		<td class="auto-style3">
			<asp:TextBox ID="txtHomePhone" runat="server"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="auto-style2">عدد الابناء فى الدراسة :</td>
		<td class="auto-style6">
			<asp:TextBox ID="txtNoOfStudyingSons" runat="server"></asp:TextBox>
		</td>
	  
		<td class="auto-style5">
			اجمالى الدخل :</td>
		<td class="auto-style3">
			<asp:TextBox ID="txtIncome" runat="server"></asp:TextBox>
			درهم
		</td>
	</tr>
	<tr>
		<td class="auto-style2">نوع السكن :</td>
		<td class="auto-style6">
			<asp:DropDownList ID="ddlHouseType" runat="server" Width="100px">
				<asp:ListItem Selected="True">ايجار</asp:ListItem>
				<asp:ListItem>ملك</asp:ListItem>
				<asp:ListItem>أخرى</asp:ListItem>
			</asp:DropDownList>
		</td>
	  
		<td class="auto-style5">
			&nbsp;</td>
		<td class="auto-style3">
			&nbsp;</td>
	</tr>
	<tr>
		<td class="auto-style2">المصروفات الشهرية :</td>
		<td class="auto-style3" colspan="3">
			<table class="auto-style3">
				<tr>
					<td align="center" width="100">ايجار السكن </td>
					<td align="center" width="100">فواتير</td>
					<td align="center" width="100">استقطاعات بنوك</td>
					<td align="center" width="100">مصاريف الإعاشة</td>
					<td align="center" width="100">مدارس / جامعات </td>
					<td align="center" width="100">أخرى </td>
				</tr>
				<tr>
					<td align="center">
						<asp:TextBox ID="txtRent" runat="server" Width="100px"></asp:TextBox>
					</td>
					<td align="center">
						<asp:TextBox ID="txtBills" runat="server" Width="100px"></asp:TextBox>
					</td>
					<td align="center">
						<asp:TextBox ID="txtBanks" runat="server" Width="100px"></asp:TextBox>
					</td>
					<td align="center">
						<asp:TextBox ID="txtLivingExpenses" runat="server" Width="100px"></asp:TextBox>
					</td>
				   <td align="center">
						<asp:TextBox ID="txtEduExpenses" runat="server" Width="100px"></asp:TextBox>
					</td>
					<td align="center">
						<asp:TextBox ID="txtOtherExpenses" runat="server" Width="100px"></asp:TextBox>
					</td>
				</tr>
			</table>
		</td>
	  
	</tr>
	<tr>
		<td align="right" colspan="2" >هل سبق وأن حصلت على مساعدة من مؤسسة زايد للأعمال الخيرية ؟&nbsp;&nbsp;
			<asp:DropDownList ID="ddlIsPreviousZayedAid" runat="server">
				<asp:ListItem Selected="True">لا</asp:ListItem>
				<asp:ListItem>نعم</asp:ListItem>
			</asp:DropDownList>
		</td>
		<td colspan="2">
			&nbsp;</td>
	</tr>
	<tr>
		<td class="auto-style2">إذا كانت الإجابة بنعــم ففــي أي عــام </td>
		<td class="auto-style6">
			<asp:TextBox ID="txtPreviousZayedAidYear" runat="server"></asp:TextBox>
		</td>
		<td class="auto-style5">
			وكـم بلغت قيمــة المساعــدة </td>
		<td class="auto-style3">
			<asp:TextBox ID="txtPreviousZayedAidAmount" runat="server"></asp:TextBox>
		</td>
	</tr>
	
	<tr>
		<td colspan="2">	هل سبق وأن حصلت على مساعدة من  جهة أخرى  ؟ &nbsp;&nbsp;
			<asp:DropDownList ID="ddlIsPreviousOtherOrgAid" runat="server">
				<asp:ListItem Selected="True">لا</asp:ListItem>
				<asp:ListItem>نعم</asp:ListItem>
			</asp:DropDownList>
		</td>
		<td class="auto-style5">
			&nbsp;</td>
		<td class="auto-style3">
			&nbsp;</td>
	</tr>

	<tr>
		<td class="auto-style2">إذا كانت الإجابة بنعــم فاذكر اسم الجهة  </td>
		<td class="auto-style6">
			<asp:TextBox ID="txtOtherOrgAidName" runat="server"></asp:TextBox>
		</td>
		<td class="auto-style5">
			وكـم بلغت قيمــة المساعــدة </td>
		<td class="auto-style3">
			<asp:TextBox ID="txtOtherOrgAidAmount" runat="server"></asp:TextBox>
		</td>
	</tr>

	<tr>
		<td colspan="2">	في حال الموافقة اذكر اسم الجهة التي تود أن يصدر شيك المساعدة باسمها : 
			</td>
		<td class="auto-style5" colspan="2">
			<asp:TextBox ID="txtChequeOrgName" runat="server" Width="360px"></asp:TextBox>
		</td>
	</tr>

	<tr>
		<td class="auto-style2">نوع المساعدة : 
		</td>
		<td class="auto-style6">
			<asp:DropDownList ID="ddlAidType" runat="server" Width="150px">
				<asp:ListItem Selected="True">متنوع</asp:ListItem>
				<asp:ListItem>تعليم</asp:ListItem>
				<asp:ListItem>صحة</asp:ListItem>
			</asp:DropDownList>
		</td>
		<td class="auto-style5">
			&nbsp;</td>
		<td class="auto-style3">
			&nbsp;</td>
	</tr>

	<tr>
		<td colspan="2" style="text-align:right;padding:0px 0px 0px 0px;"><strong>بيانات خاصة بطلبات العلاج الطبى :</strong></td>
		<td class="auto-style5">
			&nbsp;</td>
		<td class="auto-style3">
			&nbsp;</td>
	</tr>

	<tr>
		<td class="auto-style2">نوع المرض :</td>
		<td>
			<asp:TextBox ID="txtIllnessDesc" runat="server" Width="400px"></asp:TextBox>
		</td>
		<td class="auto-style5">
			التكلفة :</td>
		<td class="auto-style3">
			<asp:TextBox ID="txtMedCost" runat="server"></asp:TextBox>
		</td>
	</tr>

	<tr>
		<td class="auto-style2">مكان العلاج :</td>
		<td>
			<asp:TextBox ID="txtHospital" runat="server" Width="400px"></asp:TextBox>
		</td>
		<td class="auto-style5">
			&nbsp;</td>
		<td class="auto-style3">
			&nbsp;</td>
	</tr>
	<tr>
		<td class="auto-style2">تفاصيل المساعدة : </td>
		<!-- _x062a__x0641__x0627__x0635__x06  -->
		<td class="auto-style3" colspan="3">
			<asp:TextBox ID="txtAidRequestDetails" runat="server" Rows="4" TextMode="MultiLine" Width="100%"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="auto-style2">المبلغ المطلوب :  </td>
		<!--  _x062a__x0627__x0631__x064a__x06   -->
		<td class="auto-style6" style="padding-right:0px;">
			<asp:TextBox ID="txtRequiredAmount" runat="server" Columns="10" MaxLength="10">0</asp:TextBox>
		</td>
		<td class="auto-style5" style="padding-right:0px;">
			تاريخ الاستحقاق :  </td>
		<td class="auto-style3" style="padding-right:0px;">
			<SharePoint:DateTimeControl ID="dtcDueDate" runat="server" DateOnly="True" />
		</td>
	</tr>

	<tr>
		<td class="auto-style2">حالة الطلب : </td>
		<!--   NewColumn1  -->
		<td class="auto-style6">
			<asp:DropDownList ID="ddlAidRequestStatus" runat="server" Width="300px">
				<asp:ListItem Selected="True">غير مكتمل</asp:ListItem>
				<asp:ListItem>مكتمل</asp:ListItem>
				<asp:ListItem>مرفوض</asp:ListItem>
				<asp:ListItem> للعرض على اللجنة</asp:ListItem>
				<asp:ListItem>للعرض على المستشار الطبي</asp:ListItem>
				<asp:ListItem>موافقة اللجنة</asp:ListItem>
				<asp:ListItem>الموافقة النهائية</asp:ListItem>
				<asp:ListItem> مدفوع</asp:ListItem>
			</asp:DropDownList>
		</td>
		<td class="auto-style5">
			&nbsp;</td>
		<td class="auto-style3">
			&nbsp;</td>
	</tr>
	<tr>
		<td class="auto-style2">توصية اللجنة : </td>
		<!--    _x0645__x062f__x0629__x0020__x06    -->
		<td class="auto-style6">
			<asp:TextBox ID="txtPanelOpinion" runat="server" width="250px"></asp:TextBox>
		</td>
		<td class="auto-style5">
			مبلغ الموافقة : </td>
		<td class="auto-style3">
			<asp:TextBox ID="txtApprovedAmount" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
		</td>
	</tr>
	<tr>
	<td style="background-color:#EFFAF0">إرفاق ملف :</td>
	<td colspan="3">
		<asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
		</td>  
	</tr>
	<tr>
		<td colspan="4" align="center">

			<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="حفظ" Width="150px" BackColor="#E1F0FF" BorderColor="#B9DCFF" Font-Size="Large" Height="50px" />

			<%--<asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="False" RenderMode="Inline">
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
				</Triggers>
				<ContentTemplate>
				</ContentTemplate>
			</asp:UpdatePanel>--%>
		</td>
	</tr>
	
</table>
 </div>

<div id="dvPrint" style="clear:both;" runat="server" visible="false" > 

	<table align="right" class="auto-style7" dir="rtl" style="border:1px solid black;">
		<tr>
			<td align="center" colspan="4">
				<asp:Label ID="lblRequestPage" runat="server" Visible="False" Font-Size="Large" style="float:right;" ForeColor="Blue"></asp:Label>
				<asp:Image ID="imgClientPhoto" runat="server" style="float:right;clear:right;"/> 
				<asp:Image ID="imgLogo" runat="server" ImageUrl="../_layouts/15/SPWebParts/logo.jpg" Width="75px" style="float:left;clear:left;"/>
				<asp:Image ID="Image1" runat="server" style="float:left;" ImageUrl="../_layouts/15/SPWebParts/FormNumber.gif" Width="100px"/>
				<span dir="RTL" lang="AR-AE" style="font-size: 18.0pt; font-family: &quot;PT Bold Heading&quot;; mso-ascii-font-family: &quot;Times New Roman&quot;; mso-fareast-font-family: &quot;Times New Roman&quot;; mso-hansi-font-family: &quot;Times New Roman&quot;; color: #006600; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-AE"><span class="auto-style8" style="mso-spacerun: yes">&nbsp;</span><span class="auto-style8">نمـوذج طلب مساعدة إنسانية </span></span>
				<br /><asp:Label ID="lblAidType" runat="server" Font-Size="Large" ForeColor="Black"></asp:Label>
			   
			</td>
		</tr>
		<tr>
			<td style="width:25%;">الاسم :</td>
			<td style="width:25%;">
				<asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
			</td>
			<td style="width:25%;"> الجنسية :</td>
			<td style="width:25%;">
				<asp:Label ID="lblNationality" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>الحالة الاجتماعية :</td>
			<td><asp:Label ID="lblMaritalStatus" runat="server" Text="Label"></asp:Label></td>
			<td> العمر :</td>
			<td><asp:Label ID="lblAge" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr>
			<td>رقم الهوية : 
		</td>
			<td>
				<asp:Label ID="lblIDNumber" runat="server" Text="Label"></asp:Label>
			</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>عدد افراد الأسرة :</td>
			<td>
				<asp:Label ID="lblFamilySize" runat="server" Text="Label"></asp:Label>
			</td>
			<td>الوظيفة :</td>
			<td>
				<asp:Label ID="lblJob" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>مدة الإقامة بالدولة : </td>
			<td>
				<asp:Label ID="lblResidencyYears" runat="server" Text="Label"></asp:Label>
			</td>
			<td>عنوان السكن :</td>
			<td>
				<asp:Label ID="lblHomeAddress" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>رقم الجوال  : </td>
			<td>
				<asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label>
			</td>
			<td>هاتف المنزل:</td>
			<td>
				<asp:Label ID="lblHomePhone" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>عدد الابناء فى الدراسة :</td>
			<td>
				<asp:Label ID="lblNoOfStudyingSons" runat="server" Text="Label"></asp:Label>
			</td>
			<td>اجمالى الدخل :</td>
			<td>
				<asp:Label ID="lblIncome" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>نوع السكن :</td>
			<td>
				<asp:Label ID="lblHouseType" runat="server" Text="Label"></asp:Label>
			</td>
			<td>&nbsp;</td>
			<td>
				
			</td>
		</tr>
		<tr>
			<td>المصروفات الشهرية :</td>
			<td colspan="3" align="right">
			<table >
				<tr>
					<td align="center" width="100">ايجار السكن </td>
					<td align="center" width="100">فواتير</td>
					<td align="center" width="100">استقطاعات بنوك</td>
					<td align="center" width="100">مصاريف الإعاشة</td>
					<td align="center" width="100">مدارس / جامعات </td>
					<td align="center" width="100">أخرى </td>
				</tr>
				<tr>
					<td align="center">
						<asp:Label ID="lblRent" runat="server" Text="Label"></asp:Label>
					</td>
					<td align="center">
						<asp:Label ID="lblBills" runat="server" Text="Label"></asp:Label>
					</td>
					<td align="center">
						<asp:Label ID="lblBanks" runat="server" Text="Label"></asp:Label>
					</td>
					<td align="center">
						<asp:Label ID="lblLivingExpenses" runat="server" Text="Label"></asp:Label>
					</td>
				   <td align="center">
					   <asp:Label ID="lblEduExpenses" runat="server" Text="Label"></asp:Label>
					</td>
					<td align="center">
						<asp:Label ID="lblOtherExpenses" runat="server" Text="Label"></asp:Label>
					</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td colspan="2">هل سبق وأن حصلت على مساعدة من مؤسسة زايد للأعمال الخيرية ؟&nbsp;&nbsp;</td>
			<td colspan="2" align="right">
				<asp:Label ID="lblIsPreviousZayedAid" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td >إذا كانت الإجابة بنعــم ففــي أي عــام </td>
			 <td>
				 <asp:Label ID="lblPreviousZayedAidYear" runat="server" Text="Label"></asp:Label>
			</td>
			<td>وكـم بلغت قيمــة المساعــدة </td>
			<td align="right">
				<asp:Label ID="lblPreviousZayedAidAmount" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td colspan="2" >هل سبق وأن حصلت على مساعدة من جهة أخرى ؟ </td>
			<td colspan="2">
				<asp:Label ID="lblIsPreviousOtherOrgAid" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td >إذا كانت الإجابة بنعــم فاذكر اسم الجهة  </td>
			 <td>
				 <asp:Label ID="lblOtherOrgAidName" runat="server" Text="Label"></asp:Label>
			</td>
			<td>وكـم بلغت قيمــة المساعــدة</td>
			<td align="right">
				<asp:Label ID="lblOtherOrgAidAmount" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td colspan="2" >في حال الموافقة اذكر اسم الجهة التي تود أن يصدر شيك المساعدة باسمها : 
			</td>
			<td colspan="2">
				<asp:Label ID="lblChequeOrgName" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr>
			<td colspan="4" >

				<h6 class="auto-style8">الوثائق المطلوبة  : </h6>
				<ul style="font-size: xx-small">
					<li>رسالة موجهه إلى مدير عام المؤسسة يشرح فيها الحالة .</li>
					<li>صورة من جوازات السفر ، وصورة الهوية ، وصورة الإقامة ،  وصورة من خلاصة القيد ( للمواطنين) .</li>
					<li>صورة شخصية حديثة .</li>
					<li>شهادة إثبات حالة اجتماعية للأرامل والمطلقـــات . </li>
					<li>شهادة راتب من جهة العمل .</li>
					<li>صورة من عقد إيجار السكن .</li>
					<li>أية مستندات أخرى يريد مقدم الطلب إرفاقها لتوضح حالته .</li>
				</ul>


				<h6 class="auto-style8">ملاحظات  :</h6>
				<ul style="font-size: xx-small">
					<li>	المؤسسة غير مسؤولة عن إعادة أوراق التقديـم إلى مقــدم الطــــلب .</li>
					<li>	تعبئة هذا النموذج لا يُشكل أي التزام من طرف المؤسسة بالموافقة على المساعدة .</li>
					<li>	إذا لم يتم الإتصال بك خلال 3 أشهر من تاريخ تقديم الطلب فتقبل اعتذار المؤسسة .</li>
				</ul>

			</td>
		</tr>


		 <tr>
			<td colspan="4" align="center" >
				
				<h5><b>أتعهد أنا الموقع أدناه بأن كافة المعلومات التي ذكرتها صحيحة</b></h5>
				
			</td>
		</tr>


		 <tr>
			<td colspan="4" align="left" >
				
				<table style="width:50%;" dir="rtl">
					<tr>
						<td style="width:30%; text-align:right;">اسم مقدم الطلب : </td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td style="width:30%; text-align:right;">التوقيع:</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td style="width:30%; text-align:right;">التاريخ:</td>
						<td>&nbsp;</td>
					</tr>
				</table>
				
			</td>
		</tr>

		<tr>
			<td colspan="4" align="center" >
				
هاتف : 0097126577577	     فاكس : 0097126577570	ص.ب 41355   أبو ظبي – الإمارات العربية المتحدة

			</td>
				</tr>

		<tr>
			<td colspan="4" align="center">
				<b>Web: </b>www.zayed.org.ae	 <b>e-mail: </b>zadfou@zayed.org.ae
			</td>
				</tr>


	</table>

</div>

