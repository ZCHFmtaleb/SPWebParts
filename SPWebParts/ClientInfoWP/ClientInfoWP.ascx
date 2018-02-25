<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientInfoWP.ascx.cs" Inherits="SPWebParts.ClientInfoWP.ClientInfoWP" %>


<script type="text/javascript">
    $(document).ready(function () {
        $('div.row').hide();
    });
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
        width: 14%;
    }
    .auto-style3 td{
       margin:0px 0px 0px 0px !important;
       padding:0px 0px 0px 0px !important;
    }
    .txtPhoneStyle {
        direction:ltr;
    }
</style>
<table align="right" class="auto-style1" dir="rtl">
    <tr>
        <td class="auto-style2">الاسم : </td>
        <td class="auto-style3">
            <asp:TextBox ID="txtArabicFullName" runat="server"></asp:TextBox>
        </td>
      
        <td class="auto-style3">
            رقم الهوية : 
        </td>
        <td class="auto-style3">
            <asp:TextBox ID="txtIDNumber" runat="server"></asp:TextBox>
        </td>
          <td rowspan="8" valign="top">
            <asp:Image ID="imgPhotography" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="auto-style2">رقم الجوال  : </td>
        <td class="auto-style3">
            <asp:TextBox ID="txtPhone" runat="server" CssClass="txtPhoneStyle" Columns="15" MaxLength="15"></asp:TextBox>
        </td>
        <td class="auto-style3">
            نوع المساعدة : 
        </td>
        <td class="auto-style3">
            <asp:DropDownList ID="ddlAidType" runat="server" Width="150px">
                <asp:ListItem Selected="True">متنوع</asp:ListItem>
                <asp:ListItem>تعليم</asp:ListItem>
                <asp:ListItem>صحة</asp:ListItem>
            </asp:DropDownList>
        </td>
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
        <td class="auto-style3" style="padding-right:0px;">
            <asp:TextBox ID="txtRequiredAmount" runat="server" Columns="10" MaxLength="10">5000</asp:TextBox>
        </td>
        <td class="auto-style3" style="padding-right:0px;">
            تاريخ الاستحقاق :  </td>
        <td class="auto-style3" style="padding-right:0px;">
            <SharePoint:DateTimeControl ID="dtcDueDate" runat="server" DateOnly="True" />
        </td>
    </tr>

    <tr>
        <td class="auto-style2">حالة الطلب : </td>
        <!--   NewColumn1  -->
        <td class="auto-style3">
            <asp:DropDownList ID="ddlAidRequestStatus" runat="server" Width="150px">
                <asp:ListItem Selected="True">غير مكتمل</asp:ListItem>
                <asp:ListItem>مكتمل</asp:ListItem>
                <asp:ListItem>مرفوض</asp:ListItem>
                <asp:ListItem>موافقة اللجنة</asp:ListItem>
                <asp:ListItem>الموافقة النهائية</asp:ListItem>
                <asp:ListItem> مدفوع</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="auto-style3">
            مدة الإقامة بالسنوات : </td>
        <td class="auto-style3">
            <asp:TextBox ID="txtResidencyYears" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">توصية اللجنة : </td>
        <!--    _x0645__x062f__x0629__x0020__x06    -->
        <td class="auto-style3">
            <asp:TextBox ID="txtPanelOpinion" runat="server" width="100%"></asp:TextBox>
        </td>
        <td class="auto-style3">
            مبلغ الموافقة : </td>
        <td class="auto-style3">
            <asp:TextBox ID="txtApprovedAmount" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
        </td>
    </tr>



    <tr>
        <td colspan="4" align="center">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="حفظ" Width="150px" BackColor="#E1F0FF" BorderColor="#B9DCFF" Font-Size="Large" Height="50px" />
        </td>
    </tr>

    <tr>
        <td colspan="4" align="center">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="False" RenderMode="Inline">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label><asp:HyperLink ID="lnkRequestPage" runat="server"></asp:HyperLink>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>

</table>

