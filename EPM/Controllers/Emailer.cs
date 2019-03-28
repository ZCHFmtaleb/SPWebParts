using EPM.EL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EPM.Controllers
{
    public class Emailer
    {

        static string layoutsPath = SPUtility.GetVersionedGenericSetupPath("TEMPLATE\\Layouts\\EPM\\", 15);

        public static  void Send_Objs_Added_Email_to_DM(Emp intended_Emp, string Active_Set_Goals_Year)
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
                #endregion

                bodyText.Replace("#EmpName#", n);
                bodyText.Replace("#Active_Set_Goals_Year#", Active_Set_Goals_Year);
                string encoded_name = HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName);
                bodyText.Replace("#Link#", "<a href=" + SPContext.Current.Web.Url + "/Pages/ApproveObjectives.aspx?empid=" + encoded_name + "  >" + n + "</a>");

                #region Prepare Headers
                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_DM_email);
                headers.Add("subject", " قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                headers.Add("content-type", "text/html"); 
                #endregion

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        public static  void Send_Rej_Email_to_Emp(Emp intended_Emp)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_email);
                headers.Add("subject", "طلب تعديلات على الأهداف");
                headers.Add("content-type", "text/html");
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append("<p dir=rtl >");
                bodyText.Append("السلام عليكم ورحمة الله"); bodyText.Append("<br />");
                bodyText.Append("تحية طيبة وبعد:"); bodyText.Append("<br />");
                bodyText.Append("الرجاء القيام بإجراء التعديلات التالية على الأهداف الخاصة بك ، وإعادة ارسالها مرة أخرى:"); bodyText.Append("<br />"); bodyText.Append("<br />");
                //bodyText.Append(txtRequired_Mods.Text.Replace(Environment.NewLine, "<br />")); bodyText.Append("<br />"); bodyText.Append("<br />");
                bodyText.Append("</p>");
                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        public static  void Notify_Emp_that_Objs_finally_approved(Emp intended_Emp, string Active_Set_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_email);
                headers.Add("subject", "تم اعتماد الأهداف الخاصة بك لعام " + Active_Set_Goals_Year);
                headers.Add("content-type", "text/html");
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append("<p dir=rtl >");
                bodyText.Append("السلام عليكم ورحمة الله"); bodyText.Append("<br />");
                bodyText.Append("تحية طيبة وبعد:"); bodyText.Append("<br />");
                bodyText.Append("تهانينا. لقد تم اعتماد الأهداف الخاصة بك بشكل نهائى");
                bodyText.Append("<br />");
                bodyText.Append("يمكنك مراجعة الأهداف الخاصة بك فى أى وقت وذلك من خلال الرابط التالى:");
                bodyText.Append("<br />");
                bodyText.Append("<a href=\"" + SPContext.Current.Web.Url + "/Pages/SetObjectives.aspx\" >" + "نموذج وضع الأهداف" + " </a>");
                bodyText.Append("<br />");
                bodyText.Append("<br />");
                bodyText.Append("وشكرا جزيلا لحسن تعاونكم");
                bodyText.Append("</p>");
                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }
    }
}