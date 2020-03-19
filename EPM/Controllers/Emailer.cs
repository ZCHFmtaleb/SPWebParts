using EPM.EL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;

namespace EPM.Controllers
{
    public class Emailer
    {
        private static string layoutsPath = SPUtility.GetVersionedGenericSetupPath("TEMPLATE\\Layouts\\EPM\\", 15);

        public static void Send_Objs_Added_Email_to_DM(Emp intended_Emp, string Active_Set_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string html = File.ReadAllText(layoutsPath + "Send_Objs_Added_Email_to_DM.html");
                StringBuilder bodyText = new StringBuilder(html);

                #region If Arabic name not found, use English name

                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                #endregion If Arabic name not found, use English name

                bodyText.Replace("#EmpName#", n);
                bodyText.Replace("#Active_Set_Goals_Year#", Active_Set_Goals_Year);
                string encoded_name = HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName);
                bodyText.Replace("#Link#", "<a href=" + SPContext.Current.Web.Url + "/Pages/ApproveObjectives.aspx?empid=" + encoded_name + "  >" + n + "</a>");

                #region Prepare Headers

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.DM_email);
                headers.Add("subject", " قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                headers.Add("content-type", "text/html");

                #endregion Prepare Headers

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        public static void Send_Objs_Approved_Email_to_Dept_Head(Emp intended_Emp, string Active_Set_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string html = File.ReadAllText(layoutsPath + "Send_Objs_Approved_Email_to_Dept_Head.html");
                StringBuilder bodyText = new StringBuilder(html);

                #region If Arabic name not found, use English name

                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                #endregion If Arabic name not found, use English name

                bodyText.Replace("#EmpName#", n);
                bodyText.Replace("#Active_Set_Goals_Year#", Active_Set_Goals_Year);
                string encoded_name = HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName);
                bodyText.Replace("#Link#", "<a href=" + SPContext.Current.Web.Url + "/Pages/ApproveObjectives.aspx?empid=" + encoded_name + "  >" + n + "</a>");

                #region Prepare Headers

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Dept_Head_email);
                headers.Add("subject", " قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                headers.Add("content-type", "text/html");

                #endregion Prepare Headers

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        public static void Send_Rej_Email_to_Emp(Emp intended_Emp, string Required_Mods)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string html = File.ReadAllText(layoutsPath + "Send_Rej_Email_to_Emp.html");
                StringBuilder bodyText = new StringBuilder(html);

                #region If Arabic name not found, use English name

                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                #endregion If Arabic name not found, use English name

                string lined_Required_Mods = Required_Mods.Replace(Environment.NewLine, "<br />");

                bodyText.Replace("#Required_Mods#", lined_Required_Mods);

                #region Prepare Headers

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_email);
                headers.Add("subject", "طلب تعديلات على الأهداف");
                headers.Add("content-type", "text/html");

                #endregion Prepare Headers

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        public static void Notify_Emp_that_Objs_finally_approved(Emp intended_Emp, string Active_Set_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string html = File.ReadAllText(layoutsPath + "Notify_Emp_that_Objs_finally_approved.html");
                StringBuilder bodyText = new StringBuilder(html);

                #region If Arabic name not found, use English name

                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                #endregion If Arabic name not found, use English name

                bodyText.Replace("#Active_Set_Goals_Year#", Active_Set_Goals_Year);

                #region Prepare Headers

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_email);
                headers.Add("subject", "تم اعتماد الأهداف السنوية الخاصة بك");
                headers.Add("content-type", "text/html");

                #endregion Prepare Headers

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        public static void Send_Objs_ProgressSetByEmp_Email_to_DM(Emp intended_Emp, string Active_Rate_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string html = File.ReadAllText(layoutsPath + "Send_Objs_ProgressSetByEmp_Email_to_DM.html");
                StringBuilder bodyText = new StringBuilder(html);

                #region If Arabic name not found, use English name

                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                #endregion If Arabic name not found, use English name

                bodyText.Replace("#EmpName#", n);
                bodyText.Replace("#Active_Rate_Goals_Year#", Active_Rate_Goals_Year);
                string encoded_name = HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName);
                bodyText.Replace("#Link#", "<a href=" + SPContext.Current.Web.Url + "/Pages/RateObjectivesEmp.aspx?empid=" + encoded_name + "  >" + n + "</a>");

                #region Prepare Headers

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.DM_email);
                headers.Add("subject", " قام " + "\"" + n + "\"" + " بوضع نسب إنجاز الأهداف الفردية لعام " + Active_Rate_Goals_Year);
                headers.Add("content-type", "text/html");

                #endregion Prepare Headers

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        public static void Send_ObjsAndSkills_Rated_Email_to_Emp(Emp intended_Emp, string Active_Rate_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string html = File.ReadAllText(layoutsPath + "Send_ObjsAndSkills_Rated_Email_to_Emp.html");
                StringBuilder bodyText = new StringBuilder(html);

                #region If Arabic name not found, use English name

                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                #endregion If Arabic name not found, use English name

                bodyText.Replace("#Active_Rate_Goals_Year#", Active_Rate_Goals_Year);
                string encoded_name = HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName);
             // bodyText.Replace("#Link#", "<a href=" + SPContext.Current.Web.Url + "/Pages/RateObjectivesEmp.aspx?empid=" + encoded_name + "  >" + n + "</a>");
                bodyText.Replace("#Link#", "<a href=" + SPContext.Current.Web.Url + "/Pages/RateObjectivesEmp.aspx" + "  >" + n + "</a>");

                #region Prepare Headers

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_email);
                headers.Add("subject", " تم وضع تقييم الأهداف والكفاءات الخاصة بكم لسنة " + Active_Rate_Goals_Year);
                headers.Add("content-type", "text/html");

                #endregion Prepare Headers

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }
    }
}