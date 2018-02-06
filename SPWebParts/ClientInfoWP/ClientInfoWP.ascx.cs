using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace SPWebParts.ClientInfoWP
{
    [ToolboxItemAttribute(false)]
    public partial class ClientInfoWP : WebPart
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string ClientID = HttpContext.Current.Request.QueryString["cid"];
            ClientInfo cInfo = getClientInfo_by_ClientID(ClientID);
            PopulateControls(cInfo);
        }

        private void PopulateControls(ClientInfo cInfo)
        {
            lblArabicFullName.Text = cInfo.ArabicFullName;
            lblIDNumber.Text = cInfo.IDNumber;
            lblEngName.Text = cInfo.EngName;
            imgPhotography.ImageUrl = cInfo.Photography;

        }

        private ClientInfo getClientInfo_by_ClientID(string clientID)
        {
            ClientInfo c1 = new ClientInfo();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb spWeb = site.OpenWeb())
                    {
                        try
                        {
                            SPList spList = spWeb.Lists.TryGetList("TestForClientsListInstance1");
                            if (spList != null)
                            {
                                SPQuery qry = new SPQuery();
                                qry.Query =
                                @"   <Where>
                                          <Eq>
                                             <FieldRef Name='ID' />
                                             <Value Type='Counter'>" + clientID + @"</Value>
                                          </Eq>
                                       </Where>";
                                qry.ViewFields = @"<FieldRef Name='ArabicFullName' /><FieldRef Name='FullName' /><FieldRef Name='IDNumber' /><FieldRef Name='Photography' />";
                                SPListItemCollection listItems = spList.GetItems(qry);
                                c1.ArabicFullName = listItems[0]["ArabicFullName"].ToString();
                                c1.EngName = listItems[0]["FullName"].ToString();
                                c1.IDNumber = listItems[0]["IDNumber"].ToString();
                                c1.Photography = listItems[0]["Photography"].ToString().Split(',')[0];
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            });
            return c1;
        }
    }
}