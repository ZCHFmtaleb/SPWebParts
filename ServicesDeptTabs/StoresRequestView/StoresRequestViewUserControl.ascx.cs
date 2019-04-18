using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using ServicesDeptTabs.EL;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace ServicesDeptTabs.StoresRequestView
{
    public partial class StoresRequestViewUserControl : UserControl
    {
        #region Properties

        public Emp intended_Emp
        {
            get
            {
                if (ViewState["intended_Emp"] != null)
                {
                    return (Emp)ViewState["intended_Emp"];
                }
                else
                {
                    intended_Emp = new Emp();
                    return intended_Emp;
                }
            }
            set
            {
                ViewState["intended_Emp"] = value;
            }
        }

        #endregion Properties

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string rid = get_Request_ID_from_QueryString();

                if (rid != string.Empty)
                {
                    get_Request_Data_by_id(rid);
                }
                else
                {
                    container.InnerHtml = "لم يتم العثور على الطلب المحدد ، برجاء التحقق من معرف الطلب";
                    return;
                }
            }
        }

        private void Hide_Approve_Buttons_According_To_Status(string status)
        {
            switch (status)
            {
                case "جديد":
                    btnDMapprove.Visible = true;
                    btn_SD_approve.Visible = false;
                    break;

                case "تم اعتماد المدير المباشر":
                    btnDMapprove.Visible = false;
                    btn_SD_approve.Visible = true;
                    break;

                case "تم اعتماد إدارة الخدمات":
                    btnDMapprove.Visible = false;
                    btn_SD_approve.Visible = false;
                    break;
            }
        }

        private string get_Request_ID_from_QueryString()
        {
            if (Request.QueryString["rid"] != null)
            {
                return Request.QueryString["rid"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private void get_Request_Data_by_id(string rid)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(spWeb.ServerRelativeUrl + "/Lists/PurchasingStoresRequests");
                if (spList != null)
                {
                    SPQuery qry = new SPQuery();
                    qry.Query =
                    @"   <Where>
                          <Eq>
                             <FieldRef Name='RequestBatchGuid' />
                             <Value Type='Text'>" + rid + @"</Value>
                          </Eq>
                       </Where>";
                    qry.ViewFieldsOnly = true;
                    qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='RequestBatchGuid' /><FieldRef Name='ItemGeneralName' /><FieldRef Name='Dept' /><FieldRef Name='Status' />
                                                   <FieldRef Name='Quantity' /><FieldRef Name='Emp' /><FieldRef Name='EmpArabicName' /><FieldRef Name='Notes' /><FieldRef Name='Created' />";
                    SPListItemCollection listItems = spList.GetItems(qry);

                    if (listItems.Count > 0)
                    {
                        lblEmpName.Text = string.IsNullOrEmpty(listItems[0]["EmpArabicName"].ToString()) ? listItems[0]["Emp"].ToString() : listItems[0]["EmpArabicName"].ToString();
                        lblDept.Text = listItems[0]["Dept"].ToString();
                        lbl_ReqDate.Text = DateTime.Parse(listItems[0]["Created"].ToString()).Date.ToString("yyyy-MM-dd");
                        gvw_Items.DataSource = listItems.GetDataTable();
                        gvw_Items.DataBind();

                        string status = listItems[0]["Status"].ToString();
                        Hide_Approve_Buttons_According_To_Status(status);
                    }
                    else
                    {
                        container.InnerHtml = "لم يتم العثور على أى عناصر خاصة بهذا الطلب ، برجاء التحقق من معرف الطلب";
                        return;
                    }
                }
            });
        }

        #endregion Load

        #region Save

        protected void btnDMapprove_Click(object sender, EventArgs e)
        {
            string RequestBatchGuid = get_Request_ID_from_QueryString();

            Update_Request_Status(RequestBatchGuid, "تم اعتماد المدير المباشر");

            Send_Email_To_Services_Department(RequestBatchGuid);
        }

        protected void btn_SD_approve_Click(object sender, EventArgs e)
        {
            string RequestBatchGuid = get_Request_ID_from_QueryString();

            Update_Request_Status(RequestBatchGuid, "تم اعتماد إدارة الخدمات");

            Send_approval_confirmation_Email_To_Requester_Emp(RequestBatchGuid);
        }

        public void Update_Request_Status(string b_guid, string new_Status)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                ZFDataContext context = new ZFDataContext("http://localhost:1111");
                var itemsToUpdate = from r in context.PurchasingStoresRequests where r.RequestBatchGuid == b_guid select r;
                foreach (var t in itemsToUpdate)
                {
                    t.Status = new_Status;
                }
                context.SubmitChanges();
            });
        }

        #endregion Save

        #region Email

        private void Send_Email_To_Services_Department(string b_guid)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string layoutsPath = SPUtility.GetVersionedGenericSetupPath("TEMPLATE\\Layouts\\ServicesDeptTabs\\", 15);
                string html = File.ReadAllText(layoutsPath + "Serivces_Request_Email.html");
                StringBuilder bodyText = new StringBuilder(html);

                bodyText.Replace("#EmpName#", lblEmpName.Text);
                bodyText.Replace("#Dept#", lblDept.Text);
                bodyText.Replace("#RequestURL#", "<a href=" + SPContext.Current.Web.Url + "/Pages/StoresRequestView.aspx?rid=" + b_guid + " > رابط الطلب </a>");

                StringDictionary headers = new StringDictionary();
                headers.Add("to", "zfes@zayed.org.ae");
                headers.Add("cc", "sherif@zayed.org.ae");
                headers.Add("subject", " قام " + "\"" + lblEmpName.Text + "\"" + " بعمل طلب جديد من قسم الخدمات ");
                headers.Add("content-type", "text/html");

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        private void Send_approval_confirmation_Email_To_Requester_Emp(string requestBatchGuid)
        {
            throw new NotImplementedException();
        }

        #endregion Email
    }
}