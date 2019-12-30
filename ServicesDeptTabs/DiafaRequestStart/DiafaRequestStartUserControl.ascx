<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiafaRequestStartUserControl.ascx.cs" Inherits="ServicesDeptTabs.DiafaRequestStart.DiafaRequestStartUserControl" %>

<meta charset="utf-8" />
<meta name="viewport" content="width=device-width,initial-scale=1,shrink-to-fit=no" />
<link rel="stylesheet" href="https://www.jqwidgets.com/public/jqwidgets/styles/jqx.base.css" />
<link rel="stylesheet" href="https://www.jqwidgets.com/public/jqwidgets/styles/jqx.material-purple.css" />
<link rel="stylesheet" href="https://www.jqwidgets.com/public/jqwidgets/styles/jqx.arctic.css" />
<link rel="stylesheet" href="https://www.jqwidgets.com/public/jqwidgets/styles/jqx.material.css" />
<link href="/_layouts/15/DiafaRequestStart/static/css/main.638c7769.chunk.css" rel="stylesheet" />

<div id="app"></div>
<script>
    !(function (l) {
        function e(e) {
            for (
                var r, t, n = e[0], o = e[1], u = e[2], f = 0, i = [];
                f < n.length;
                f++
            )
                (t = n[f]), p[t] && i.push(p[t][0]), (p[t] = 0);
            for (r in o)
                Object.prototype.hasOwnProperty.call(o, r) && (l[r] = o[r]);
            for (s && s(e); i.length;) i.shift()();
            return c.push.apply(c, u || []), a();
        }
        function a() {
            for (var e, r = 0; r < c.length; r++) {
                for (var t = c[r], n = !0, o = 1; o < t.length; o++) {
                    var u = t[o];
                    0 !== p[u] && (n = !1);
                }
                n && (c.splice(r--, 1), (e = f((f.s = t[0]))));
            }
            return e;
        }
        var t = {},
            p = { 1: 0 },
            c = [];
        function f(e) {
            if (t[e]) return t[e].exports;
            var r = (t[e] = { i: e, l: !1, exports: {} });
            return l[e].call(r.exports, r, r.exports, f), (r.l = !0), r.exports;
        }
        (f.m = l),
            (f.c = t),
            (f.d = function (e, r, t) {
                f.o(e, r) ||
                    Object.defineProperty(e, r, { enumerable: !0, get: t });
            }),
            (f.r = function (e) {
                "undefined" != typeof Symbol &&
                    Symbol.toStringTag &&
                    Object.defineProperty(e, Symbol.toStringTag, { value: "Module" }),
                    Object.defineProperty(e, "__esModule", { value: !0 });
            }),
            (f.t = function (r, e) {
                if ((1 & e && (r = f(r)), 8 & e)) return r;
                if (4 & e && "object" == typeof r && r && r.__esModule) return r;
                var t = Object.create(null);
                if (
                    (f.r(t),
                        Object.defineProperty(t, "default", { enumerable: !0, value: r }),
                        2 & e && "string" != typeof r)
                )
                    for (var n in r)
                        f.d(
                            t,
                            n,
                            function (e) {
                                return r[e];
                            }.bind(null, n)
                        );
                return t;
            }),
            (f.n = function (e) {
                var r =
                    e && e.__esModule
                        ? function () {
                            return e.default;
                        }
                        : function () {
                            return e;
                        };
                return f.d(r, "a", r), r;
            }),
            (f.o = function (e, r) {
                return Object.prototype.hasOwnProperty.call(e, r);
            }),
            (f.p = "/");
        var r = (window.webpackJsonp = window.webpackJsonp || []),
            n = r.push.bind(r);
        (r.push = e), (r = r.slice());
        for (var o = 0; o < r.length; o++) e(r[o]);
        var s = n;
        a();
    })([]);
</script>
<script src="/_layouts/15/DiafaRequestStart/static/js/2.86e9986a.chunk.js"></script>
<script src="/_layouts/15/DiafaRequestStart/static/js/main.78432235.chunk.js"></script>
