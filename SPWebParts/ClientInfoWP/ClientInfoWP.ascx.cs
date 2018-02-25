using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace SPWebParts.ClientInfoWP
{
    [ToolboxItemAttribute(false)]
    public partial class ClientInfoWP : WebPart
    {
        public const string ListName = "AidRequests";
        public string cid;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            get_cid();

            if (cid != "0")
            {
                ClientInfo cInfo = getClientInfo_by_ClientID(cid);
                if (!Page.IsPostBack)
                {
                    PopulateControls(cInfo);
                }
            }
        }

        private void get_cid()
        {
            if (HttpContext.Current.Request.QueryString["cid"] != null)
            {
                cid = HttpContext.Current.Request.QueryString["cid"];
            }
            else
            {
                cid = "0";
            }
        }

        private ClientInfo getClientInfo_by_ClientID(string cid)
        {
            ClientInfo c1 = new ClientInfo();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = site.OpenWeb())
                    {
                        try
                        {
                            SPList spList = spWeb.Lists.TryGetList("Clients");
                            if (spList != null)
                            {
                                SPQuery qry = new SPQuery();
                                qry.Query =
                                @"   <Where>
                                          <Eq>
                                             <FieldRef Name='ID' />
                                             <Value Type='Counter'>" + cid + @"</Value>
                                          </Eq>
                                       </Where>";
                                qry.ViewFields = @"<FieldRef Name='ArabicFullName' /><FieldRef Name='MobilePhoneNumber' /><FieldRef Name='IDNumber' /><FieldRef Name='Photography' />";
                                SPListItemCollection listItems = spList.GetItems(qry);
                                c1.ArabicFullName = listItems[0]["ArabicFullName"].ToString();
                                c1.Phone = listItems[0]["MobilePhoneNumber"].ToString();
                                c1.IDNumber = listItems[0]["IDNumber"].ToString();
                                c1.Photography = listItems[0]["Photography"].ToString().Split(',')[0];
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            });
            return c1;
        }

        private void PopulateControls(ClientInfo cInfo)
        {
            txtArabicFullName.Text = cInfo.ArabicFullName;
            txtIDNumber.Text = cInfo.IDNumber;
            txtPhone.Text = cInfo.Phone;
            imgPhotography.ImageUrl = cInfo.Photography;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                try
                {
                    using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList list = web.Lists.TryGetList(ListName);
                            if (list != null)
                            {
                                SPListItem NewItem = list.Items.Add();
                                {
                                    web.AllowUnsafeUpdates = true;

                                    NewItem["ClientID"] = cid;
                                    NewItem["_x0627__x0644__x0625__x0633__x06"] = txtArabicFullName.Text;
                                    NewItem["EIDCardNumber"] = txtIDNumber.Text;
                                    NewItem["Phone"] = txtPhone.Text;
                                    NewItem["_x0646__x0648__x0639__x0020__x06"] = ddlAidType.SelectedItem.Text;
                                    NewItem["_x062a__x0641__x0627__x0635__x06"] = txtAidRequestDetails.Text;
                                    NewItem["_x062a__x0627__x0631__x064a__x06"] = dtcDueDate.SelectedDate;

                                    NewItem["_x0642__x064a__x0645__x0629__x00"] = txtRequiredAmount.Text;
                                    NewItem["NewColumn1"] = ddlAidRequestStatus.SelectedItem.Text;
                                    NewItem["_x0645__x062f__x0629__x0020__x06"] = txtResidencyYears.Text;
                                    NewItem["_x062a__x0648__x0635__x064a__x06"] = txtPanelOpinion.Text;
                                    NewItem["_x0645__x0628__x0644__x063a__x00"] = txtApprovedAmount.Text;

                                    NewItem.Update();// means "Update Changes" , used for both Insert and Update. If ID is empty , it Inserts , otherwise if ID has value , it Updates

                                    web.AllowUnsafeUpdates = false;
                                    if (NewItem.ID != 0)
                                    {
                                        lblSuccess.Text = "تم تسجيل الطلب بنجاح.   ";
                                        lnkRequestPage.Text = "رقم الطلب " + NewItem.ID.ToString();
                                        lnkRequestPage.NavigateUrl = SPContext.Current.Web.Url + "/Lists/" + ListName + "/Item/displayifs.aspx?ID=" + NewItem.ID.ToString();
                                        lblSuccess.BackColor = ColorTranslator.FromHtml("#d0ffc6");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblSuccess.Text = "حدث الخطأ التالى اثناء محاولة إضافة الطلب : " + ex.Message;
                    lblSuccess.BackColor = ColorTranslator.FromHtml("#ffbfbf");
                }
            });
        }
    }
}