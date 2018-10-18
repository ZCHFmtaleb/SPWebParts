using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Text;
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
            if (HttpContext.Current.Request.QueryString["reprint"] != null)
            {
                string reprintReqID = HttpContext.Current.Request.QueryString["reprint"];
                Prepare_Printing(reprintReqID);
            }

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
                                    #region Save to SP

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

                                    //================================== New Fields ============================================
                                    NewItem["Nationality"] = txtNationality.Text;
                                    NewItem["MaritalStatus"] = txtMaritalStatus.Text;
                                    NewItem["Age"] = txtAge.Text;
                                    NewItem["FamilySize"] = txtFamilySize.Text;
                                    NewItem["Job"] = txtJob.Text;
                                    NewItem["HomeAddress"] = txtHomeAddress.Text;
                                    NewItem["HomePhone"] = txtHomePhone.Text;
                                    NewItem["NoOfStudyingSons"] = txtNoOfStudyingSons.Text;
                                    NewItem["Income"] = txtIncome.Text;
                                    NewItem["HouseType"] = ddlHouseType.SelectedItem.Text;

                                    NewItem["Rent"] = txtRent.Text;
                                    NewItem["Bills"] = txtBills.Text;
                                    NewItem["Banks"] = txtBanks.Text;
                                    NewItem["LivingExpenses"] = txtLivingExpenses.Text;
                                    NewItem["EduExpenses"] = txtEduExpenses.Text;
                                    NewItem["OtherExpenses"] = txtOtherExpenses.Text;

                                    NewItem["IsPreviousZayedAid"] = ddlIsPreviousZayedAid.SelectedItem.Text;
                                    NewItem["PreviousZayedAidYear"] = txtPreviousZayedAidYear.Text;
                                    NewItem["PreviousZayedAidAmount"] = txtPreviousZayedAidAmount.Text;

                                    NewItem["IsPreviousOtherOrgAid"] = ddlIsPreviousOtherOrgAid.SelectedItem.Text;
                                    NewItem["OtherOrgAidName"] = txtOtherOrgAidName.Text;
                                    NewItem["OtherOrgAidAmount"] = txtOtherOrgAidAmount.Text;

                                    NewItem["ChequeOrgName"] = txtChequeOrgName.Text;
                                    //============================= Medical Aid Request Fields ============================================

                                    NewItem["IllnessDesc"] = txtIllnessDesc.Text;
                                    NewItem["Hospital"] = txtHospital.Text;
                                    NewItem["MedCost"] = txtMedCost.Text;

                                    //===============================================================================================
                                    if (FileUpload1.HasFile)
                                    {
                                        NewItem.Attachments.Add(FileUpload1.FileName, FileUpload1.FileBytes);
                                    }

                                    NewItem.Update();// means "Update Changes" , used for both Insert and Update. If ID is empty , it Inserts , otherwise if ID has value , it Updates
                                    web.AllowUnsafeUpdates = false;

                                    #endregion Save to SP

                                    if (NewItem.ID != 0)
                                    {
                                        lblSuccess.Visible = true;
                                        lblRequestPage.Visible = true;

                                        lblSuccess.Text = "تم تسجيل الطلب بنجاح.   ";
                                        lblRequestPage.Text = "رقم الطلب " + NewItem.ID.ToString();
                                        //lnkRequestPage.NavigateUrl = SPContext.Current.Web.Url + "/Lists/" + ListName + "/Item/displayifs.aspx?ID=" + NewItem.ID.ToString();
                                        lblSuccess.BackColor = ColorTranslator.FromHtml("#d0ffc6");

                                        if (ddlAidRequestStatus.SelectedItem.Text.Trim() == "للعرض على المستشار الطبي")
                                        {
                                            string Request_Details_URL = SPContext.Current.Web.Url + "/Lists/" + ListName + "/Item/displayifs.aspx?ID=" + NewItem.ID.ToString();
                                            string Request_Number = NewItem.ID.ToString();
                                            string Med_Email = get_Med_Email();
                                            Send_Email_To_Medical_Consultant(Request_Details_URL, Request_Number, Med_Email);
                                        }

                                        Prepare_Printing(NewItem.ID.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblSuccess.Visible = true;
                    lblSuccess.Text = "حدث الخطأ التالى اثناء محاولة إضافة الطلب : " + ex.Message;
                    lblSuccess.BackColor = ColorTranslator.FromHtml("#ffbfbf");
                }
            });
        }

        private void Prepare_Printing(string pReqID)
        {
            divContainer.Visible = false;
            btnPrint.Visible = true;
            dvPrint.Visible = true;

            AidRequest r1= AidRequest_DAL. get_Request_Details_by_ID(pReqID);

            imgClientPhoto.ImageUrl = "/orgchart/ProgramsDepartment//ClientsImages/" + r1.EIDCardNumber + ".jpg";
            lblAidType.Text = r1.AidType;                
            lblName.Text = r1.ArabicFullName;
            lblNationality.Text = r1.Nationality;
            lblMaritalStatus.Text = r1.MaritalStatus;
            lblAge.Text = r1.Age;
            lblIDNumber.Text = r1.EIDCardNumber;
            lblFamilySize.Text = r1.FamilySize;
            lblJob.Text = r1.Job;
            lblResidencyYears.Text = r1.ResidencyYears;
            lblHomeAddress.Text = r1.HomeAddress;
            lblPhone.Text = r1.Phone;
            lblHomePhone.Text = r1.HomePhone;
            lblNoOfStudyingSons.Text = r1.NoOfStudyingSons;
            lblIncome.Text = r1.Income;
            lblHouseType.Text = r1.HouseType;

            lblRent.Text = r1.Rent;
            lblBills.Text = r1.Bills;
            lblBanks.Text = r1.Banks;
            lblLivingExpenses.Text = r1.LivingExpenses;
            lblEduExpenses.Text = r1.EduExpenses;
            lblOtherExpenses.Text = r1.OtherExpenses;

            lblIsPreviousZayedAid.Text = r1.PreviousZayedAid;
            lblPreviousZayedAidYear.Text = r1.PreviousZayedAidYear;
            lblPreviousZayedAidAmount.Text = r1.PreviousZayedAidAmount;

            lblIsPreviousOtherOrgAid.Text = r1.IsPreviousOtherOrgAid;
            lblOtherOrgAidName.Text = r1.OtherOrgAidName;
            lblOtherOrgAidAmount.Text = r1.OtherOrgAidAmount;
            lblChequeOrgName.Text = r1.ChequeOrgName;
        }

        private string get_Med_Email()
        {
            string Med_Email = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("المستشار الطبى");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='Title' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            Med_Email = listItems[0]["Title"].ToString();
                        }
                    }
                }
            });
            return Med_Email;
        }

        private void Send_Email_To_Medical_Consultant(string Request_Details_URL, string Request_Number, string Med_Email)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                StringDictionary headers = new StringDictionary();
                headers.Add("to", Med_Email);
                headers.Add("subject", "طلب علاج طبى جديد - رقم " + Request_Number);
                headers.Add("content-type", "text/html");

                StringBuilder bodyText = new StringBuilder();

                bodyText.Append("<p dir=rtl >");
                bodyText.Append("تم تقديم طلب مساعدة (علاج طبى) جديد ، الرجاء الإطلاع على تفاصيل الطلب ومعاينة الملفات المرفقة من خلال الرابط ادناه. ");
                bodyText.Append("<br />");
                bodyText.Append("ثم القيام بتحرير العنصر لاضافة رأى سيادتكم وذلك من خلال حقل \"تعليق المستشار الطبى\" ");
                bodyText.Append("<br />");
                bodyText.Append("<a href=" + Request_Details_URL + "  >" + "عرض تفاصيل الطلب" + "</a>");
                bodyText.Append("</p>");

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }
    }
}