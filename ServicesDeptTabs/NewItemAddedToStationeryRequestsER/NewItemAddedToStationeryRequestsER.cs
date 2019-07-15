using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using ServicesDeptTabs.EL;
using ServicesDeptTabs.DAL;

namespace ServicesDeptTabs.NewItemAddedToStationeryRequestsER
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class NewItemAddedToStationeryRequestsER : SPItemEventReceiver
    {
        public override void ItemAdded(SPItemEventProperties properties)
        {
            SPListItem currentItem = properties.ListItem;
            string EmpName =  currentItem["Emp"].ToString();
            string b_guid = currentItem["RequestBatchGuid"].ToString();
            Send_Serivces_Request_Email_to_DM_To_Approve(EmpName, b_guid);
        }


        private void Send_Serivces_Request_Email_to_DM_To_Approve(string EmpName, string b_guid)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string layoutsPath = SPUtility.GetVersionedGenericSetupPath("TEMPLATE\\Layouts\\ServicesDeptTabs\\", 15);
                string html = File.ReadAllText(layoutsPath + "Serivces_Request_Email_to_DM_To_Approve.html");
                StringBuilder bodyText = new StringBuilder(html);

                Emp intended_Emp = Emp_DAL.get_Emp_Info(EmpName);

                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                bodyText.Replace("#EmpName#", n);
                string link_to_view_request = "<a href=" + SPContext.Current.Web.Url + "/Pages/StoresRequestView.aspx?rid=" + b_guid + " > رابط الطلب </a>";
                bodyText.Replace("#RequestURL#", link_to_view_request);

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.DM_email);
                headers.Add("subject", " قام " + "\"" + n + "\"" + " بعمل طلب جديد من قسم الخدمات ");
                headers.Add("content-type", "text/html");

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }


    }
}