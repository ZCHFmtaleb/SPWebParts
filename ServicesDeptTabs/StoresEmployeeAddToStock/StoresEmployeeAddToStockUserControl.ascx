<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StoresEmployeeAddToStockUserControl.ascx.cs" Inherits="ServicesDeptTabs.StoresEmployeeAddToStock.StoresEmployeeAddToStockUserControl" %>

<SharePoint:CssRegistration runat="server" Name="/_layouts/15/ServicesDeptTabs/newCSS/StoresEmployeeAddToStock.css" After="/Style%20Library/css/ShareBoot.css" />

<div id="container" dir="rtl" style="border: none !important; text-align: right">

<div>
        <table id="AddTable">
            <tr>
                <td class="TableRightColumn">مجموعة الصنف</td>
                <td>
                    <select id="ddlCat" style="width: 200px" onchange="GetItemsOfSelectedCat('Inventory')">
                        <option value="-1">اختر مجموعة الصنف</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="TableRightColumn">الصنف </td>
                <td>
                    <select id="ddlItem" style="width: 350px">
                        <option value="-1">اختر الصنف </option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="TableRightColumn">الكمية</td>
                <td>
                    <div id="txtQuantity"></div>
                </td>
            </tr>
        </table>
</div>

<div style="width:50%; text-align:center">
          <input id="btnAddToStock" type="button" value="أضف إلى المخزون"  style="font-size:x-large;"/>
</div>

</div><!-- end of container -->


<!-- ===========================Ref=============================================================================== -->
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
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/MyJS/ReadCategories.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/MyJS/GetItemsOfSelectedCat.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/MyJS/AddToStock.js"></script>
<script type="text/javascript" src="/_layouts/15/ServicesDeptTabs/MyJS/PageLoad_StoresEmployeeAddToStock.js"></script>
<!-- ===========================End of MyJS=============================================================================== -->

<script src="/_layouts/15/ServicesDeptTabs/polyfill.min.js"></script>
