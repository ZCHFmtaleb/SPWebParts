﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientInfoWP.ascx.cs" Inherits="SPWebParts.ClientInfoWP.ClientInfoWP" %>

<style type="text/css">
    .auto-style1 {
        width: 70%;
        border:2px solid;
        margin-right:15%;
        margin-left:15%;
        margin-top:0px;
        margin-bottom:0px;
    }
    .auto-style2 {
        width: 20%;
    }
    .auto-style3 {
        width: 40%;
    }
</style>
<table align="right" class="auto-style1" dir="rtl">
    <tr>
        <td class="auto-style2">الاسم : </td>
        <td class="auto-style3">
            <asp:Label ID="lblArabicFullName" runat="server" Text="ArabicFullName"></asp:Label>
        </td>
        <td rowspan="3">
            <asp:Image ID="imgPhotography" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="auto-style2">الاسم الانجليزى : </td>
        <td class="auto-style3">
            <asp:Label ID="lblEngName" runat="server" Text="EngName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">رقم الهوية : </td>
        <td class="auto-style3">
            <asp:Label ID="lblIDNumber" runat="server" Text="IDNumber"></asp:Label>
        </td>
    </tr>
</table>
