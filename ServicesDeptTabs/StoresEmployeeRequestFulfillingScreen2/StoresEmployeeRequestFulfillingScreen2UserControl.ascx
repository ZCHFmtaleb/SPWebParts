<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StoresEmployeeRequestFulfillingScreen2UserControl.ascx.cs" Inherits="ServicesDeptTabs.StoresEmployeeRequestFulfillingScreen2.StoresEmployeeRequestFulfillingScreen2UserControl" %>


<SharePoint:CssRegistration runat="server" Name="/_layouts/15/ServicesDeptTabs/newCSS/StoresEmployeeRequestFulfillingScreen.css" After="/Style%20Library/css/ShareBoot.css" />


<div id="wrapper" dir="rtl" >
    <div id="header">
        <table class="tblRequesterInfo">
            <tr>
                <td class="c1">مقدم الطلب :</td>
                <td>
                    <span id="lblEmpName"></span>
                </td>
            </tr>
            <tr>
                <td class="c1">الإدارة :</td>
                <td>
                    <span id="lblDept"></span>
                </td>
            </tr>
            <tr>
                <td class="c1">تاريخ الطلب :</td>
                <td>
                    <span id="lbl_ReqDate"></span>
                </td>
            </tr>
        </table>
    </div>
    <div id="content">
        <div id="jqxgrid">
        </div>
    </div>

    <span style="display:none" id="hidden_Title"></span>
    <span style="display:none" id="hidden_Quantity"></span>
    <span style="display:none" id="hidden_Notes"></span>
    <span style="display:none" id="hidden_ID"></span>
<div id="popupWindow" role="dialog" class="jqx-rc-all jqx-rc-all-light jqx-window jqx-window-light jqx-popup jqx-popup-light jqx-widget jqx-widget-light jqx-widget-content jqx-widget-content-light" tabindex="0" hidefocus="true" style="outline: none; width: 250px; height: 218px; min-height: 50px; max-height: 1200px; min-width: 100px; max-width: 1200px; top: 101px; left: 68px; z-index: 1801; display: none;">
            <div class="jqx-window-header jqx-window-header-light jqx-widget-header jqx-widget-header-light jqx-disableselect jqx-disableselect-light jqx-rc-t jqx-rc-t-light" tabindex="0" style="position: relative; width: 236px; cursor: move;"><div style="float: left; direction: ltr; margin-top: 0px;">Edit</div><div class="jqx-window-close-button-background jqx-window-close-button-background-light" style="visibility: visible; width: 16px; height: 16px; margin-right: 7px; margin-left: 0px; position: absolute; right: 0px;"><div class="jqx-window-close-button jqx-window-close-button-light jqx-icon-close jqx-icon-close-light" style="width: 100%; height: 100%;"></div></div><div class="jqx-window-collapse-button-background jqx-window-collapse-button-background-light" style="visibility: hidden; width: 16px; height: 16px; margin-right: 7px; margin-left: 0px; position: absolute; right: 16px;"><div class="jqx-window-collapse-button jqx-window-collapse-button-light jqx-icon-arrow-up jqx-icon-arrow-up-light" style="width: 100%; height: 100%; top: 0px;"></div></div></div>
           <div style="overflow: hidden; width: 250px; height: 186px;" class="jqx-window-content jqx-window-content-light jqx-widget-content jqx-widget-content-light jqx-rc-b jqx-rc-b-light" tabindex="0">
                <table>
                    <tbody><tr>
                        <td align="right">Category &nbsp;</td>
                        <td align="left">
                             <select id="ddlCat" style="width:350px" >
                                    <option  value="Select Category">Select Category</option>
                                </select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Item &nbsp;</td>
                        <td align="left">
                              <select id="ddlItem" style="width:350px" >
                                    <option  value="(Please select category first)">(Please select category first)</option>
                                </select>
                        </td>
                    </tr>
                    <tr>
                        <td align="right"></td>
                        <td style="padding-top: 10px;" align="right"><input style="margin-right: 5px;" type="button" id="Save" value="Save" role="button" class="jqx-rc-all jqx-rc-all-light jqx-button jqx-button-light jqx-widget jqx-widget-light jqx-fill-state-normal jqx-fill-state-normal-light" aria-disabled="false"><input id="Cancel" type="button" value="Cancel" role="button" class="jqx-rc-all jqx-rc-all-light jqx-button jqx-button-light jqx-widget jqx-widget-light jqx-fill-state-normal jqx-fill-state-normal-light" aria-disabled="false"></td>
                    </tr>
                    <tr>
                        <td colspan="2" ><span id="item_not_selected_error" style="color:red"></span></td>
                    </tr>
                </tbody></table>
            </div>
       </div>



    <div id="footer">
        <div class="rowZF" style="padding-top: 3%; padding-bottom: 3%;" id="decision_div">
            <div class="column">
                <button id="btnAsif_Mark_as_Failed" type="button">لم يتم توفير الطلب <br /> Can't be Fulfilled</button>
            </div>
            <div class="column">
                <button id="btnAsif_Mark_as_Fulfilled" type="button">تم توفير الطلب <br /> Mark as Fulfilled</button>
                <img src="/_layouts/15/ServicesDeptTabs/CheckMark.png" alt="" style="display: none; width: 113px; height: 93px; position: absolute; right: 30%; z-index: -1" id="CheckMark" />
                <br />
                <div id="successText" style="display: none; position: absolute; right: 30%;">تم إعتماد الطلب بنجاح</div>
            </div>

        </div>
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
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/jqxwindow.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/jqwidgets/generatedata.js"></script>

<script src="/_layouts/15/ServicesDeptTabs/sweetalert2.all.min.js"></script>

<!-- ===========================MyJS=============================================================================== -->
<script src="/_layouts/15/ServicesDeptTabs/MyJS/sprestlib.bundle.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/sendEmail.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/Read_ItemSpecificName_Categories.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/Read_ItemSpecificNames.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/Mark_as_Fulfilled.js"></script>
<script src="/_layouts/15/ServicesDeptTabs/MyJS/PageLoad_StoresEmployeeFulfillingScreen2.js"></script>
<!-- ===========================End of MyJS=============================================================================== -->

<script src="/_layouts/15/ServicesDeptTabs/polyfill.min.js"></script>

