using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace SPWebParts.PreviousRequestsWP
{
    [ToolboxItemAttribute(false)]
    public partial class PreviousRequestsWP : WebPart
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string hid = get_hid();
            if (hid != "0")
            {
                DataTable tblPreviousRequests = get_PreviousRequests_by_hid(hid);
                if (!Page.IsPostBack)
                {
                    grdPreviousRequests.DataSource = tblPreviousRequests;
                    grdPreviousRequests.DataBind();
                }
            }
        }

        private DataTable get_PreviousRequests_by_hid(string hid)
        {
            DataTable results = new DataTable();

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = site.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("AidRequests");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =
                             @"   <Where>
                                      <Eq>
                                         <FieldRef Name='EIDCardNumber' />
                                         <Value Type='Text'>"+hid+@"</Value>
                                      </Eq>
                                   </Where>";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            results = listItems.GetDataTable();
                        }
                    }
                }
            });

            return results;
        }

        private string get_hid()
        {
            if (HttpContext.Current.Request.QueryString["hid"] != null)
            {
                return HttpContext.Current.Request.QueryString["hid"];
            }
            else
            {
                return "0";
            }
        }
    }
}