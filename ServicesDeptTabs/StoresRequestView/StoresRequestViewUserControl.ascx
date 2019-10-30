<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StoresRequestViewUserControl.ascx.cs" Inherits="ServicesDeptTabs.StoresRequestView.StoresRequestViewUserControl" %>

<SharePoint:CssRegistration runat="server" Name="/_layouts/15/ServicesDeptTabs/newCSS/StoresRequestView.css" After="/Style%20Library/css/ShareBoot.css" />

<div id="container" dir="rtl" >
    <div style="padding-bottom:1%">
    <table class="tblRequesterInfo">
        <tr>
            <td class="c1">مقدم الطلب :</td>
            <td>
                <span ID="lblEmpName" ></span>
            </td>
        </tr>
        <tr>
            <td class="c1">الإدارة :</td>
            <td>
                <span ID="lblDept" ></span>
            </td>
        </tr>
        <tr>
            <td class="c1">تاريخ الطلب :</td>
            <td>
                <span ID="lbl_ReqDate" ></span>
            </td>
        </tr>
    </table>
    </div>

 <div id="jqxgrid">
 </div>

<div class="rowZF" style="padding-top:3%; padding-bottom:3%;" id="decision_div">
   <div class="column">
     <input id="btnDMReject" type="button" value="رفض"  style="font-size:x-large;display:none"/>
     <input id="btnServicesDivisionHeadReject" type="button" value="رفض القسم"  style="font-size:x-large;display:none"/>
  </div>
  <div class="column">
    <input id="btnDMapprove" type="button" value="موافقة"  style="font-size:x-large;position:absolute;right:30%;display:none"/>
    <input id="btnServicesDivisionHeadApprove" type="button" value="موافقة القسم"  style="font-size:x-large;position:absolute;right:30%;display:none"/>
    <img src="/_layouts/15/ServicesDeptTabs/CheckMark.png" alt="" style="display:none;width:113px;height:93px;position: absolute;right:30%; z-index:-1" id="CheckMark"/><br />
    <div id="successText" style="display:none;position:absolute;right:30%;">تم إعتماد الطلب بنجاح</div>
  </div>
  
</div>
<div id="divRejectReasons" style="display:none;">
<div><h4>الرجاء ذكر أسباب رفض الطلب (سيتم إرسال تنويه بها لمقدم الطلب) </h4></div>
<textarea id="txtRejectReasons" rows="6" style="width:50%" ></textarea> <span id="reqStar" style="color:red">*</span>
<input id="btnDMRejectSubmit" type="button" value="إرسال"  style="font-size:x-large;" disabled />
<input id="btnServicesDivisionHeadRejectSubmit" type="button" value="إرسال"  style="font-size:x-large;" disabled />
</div>



</div>
<!-- ========================================Ref==================================-->
<link rel="stylesheet" href="/Style%20Library/jQueryUI/base/jquery-ui.css">
<script type="text/javascript" src="/Style%20Library/jQueryUI/base/jquery-ui.js"></script>
<link rel="stylesheet" href="/_layouts/15/ServicesDeptTabs/jqwidgets/styles/jqx.base.css" type="text/css" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta name="viewport" content="width=device-width, initial-scale=1 maximum-scale=1 minimum-scale=1" />
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqscripts/jquery-1.12.4.min.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxcore.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxdata.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxbuttons.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxscrollbar.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxmenu.js"></script>

<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxgrid.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxgrid.edit.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxgrid.selection.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxgrid.columnsresize.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxgrid.filter.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxgrid.sort.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxgrid.pager.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxgrid.grouping.js"></script>

<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxlistbox.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxdropdownlist.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxcheckbox.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxcalendar.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxnumberinput.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxdatetimeinput.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/globalization/globalize.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/generatedata.js"></script>

<script src="/_layouts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/_layouts/sp.core.js" type="text/javascript"></script>
<script src="/_layouts/sp.runtime.js" type="text/javascript"></script>
<script src="/_layouts/sp.js" type="text/javascript"></script>

<script src="/_layouts/15/ServicesDeptTabs/sweetalert2.all.min.js"></script>

<!-- ===========================MyJS=============================================================================== -->
<script src="/_layouts/15/ServicesDeptTabs/MyJS/sprestlib.bundle.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/sendEmail.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/CheckRequestStatusAndShowHideControlsAccordingly.js"></script>
<script  src="/_layouts/15/ServicesDeptTabs/MyJS/PageLoad_StoresRequestView.js"></script>
<script  src="/_layouts/15/ServicesDeptTabs/MyJS/OnDMapprove.js"></script>
<script  src="/_layouts/15/ServicesDeptTabs/MyJS/OnDMReject.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/OnServicesDivisionHeadApprove.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/OnServicesDivisionHeadReject.js"></script>
<!-- ===========================End of MyJS=============================================================================== -->

<script src="/_layouts/15/ServicesDeptTabs/polyfill.min.js"></script>

