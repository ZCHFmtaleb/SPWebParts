using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using ServicesDeptTabs.EL;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicesDeptTabs.ServicesRequestsAll
{
    public partial class ServicesRequestsAllUserControl : UserControl
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

        public string Planning_Consultant_Email
        {
            get
            {
                if (ViewState["Planning_Consultant_Email"] != null)
                {
                    return ViewState["Planning_Consultant_Email"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["Planning_Consultant_Email"] = value;
            }
        }

        public string Active_Set_Goals_Year
        {
            get
            {
                if (ViewState["Active_Set_Goals_Year"] != null)
                {
                    return ViewState["Active_Set_Goals_Year"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["Active_Set_Goals_Year"] = value;
            }
        }

        public DataTable tbl_Requested_Items
        {
            get
            {
                if (ViewState["tbl_Requested_Items"] != null)
                {
                    return (DataTable)ViewState["tbl_Requested_Items"];
                }
                else
                {
                    tbl_Requested_Items = new DataTable();
                    tbl_Requested_Items.Columns.Add("ItemGeneralName"); // اسم الصنف العام
                    tbl_Requested_Items.Columns.Add("Quantity", typeof(Int32)); // الكمية
                    tbl_Requested_Items.Columns.Add("Notes"); // ملاحظات
                    return tbl_Requested_Items;
                }
            }
            set
            {
                ViewState["tbl_Requested_Items"] = value;
            }
        }

        public DataTable tblStrDir
        {
            get
            {
                if (ViewState["tblStrDir"] != null)
                {
                    return (DataTable)ViewState["tblStrDir"];
                }
                else
                {
                    tblStrDir = new DataTable();
                    tblStrDir.Columns.Add("ID");
                    tblStrDir.Columns.Add("Title");
                    return tblStrDir;
                }
            }
            set
            {
                ViewState["tblStrDir"] = value;
            }
        }

        public DataTable tblPrimaryGoal
        {
            get
            {
                if (ViewState["tblPrimaryGoal"] != null)
                {
                    return (DataTable)ViewState["tblPrimaryGoal"];
                }
                else
                {
                    tblPrimaryGoal = new DataTable();
                    tblPrimaryGoal.Columns.Add("Title");
                    return tblPrimaryGoal;
                }
            }
            set
            {
                ViewState["tblPrimaryGoal"] = value;
            }
        }

        public DataTable tblSecondaryGoal
        {
            get
            {
                if (ViewState["tblSecondaryGoal"] != null)
                {
                    return (DataTable)ViewState["tblSecondaryGoal"];
                }
                else
                {
                    tblSecondaryGoal = new DataTable();
                    tblSecondaryGoal.Columns.Add("ID");
                    tblSecondaryGoal.Columns.Add("Title");
                    return tblSecondaryGoal;
                }
            }
            set
            {
                ViewState["tblSecondaryGoal"] = value;
            }
        }

        public string strEmpDisplayName
        {
            get
            {
                return ViewState["strEmpDisplayName"].ToString();
            }
            set
            {
                ViewState["strEmpDisplayName"] = value;
            }
        }

        public int PercentageTotal
        {
            get
            {
                if (ViewState["PercentageTotal"] != null)
                {
                    return (int)ViewState["PercentageTotal"];
                }
                else
                {
                    PercentageTotal = 0;
                    return PercentageTotal;
                }
            }
            set
            {
                ViewState["PercentageTotal"] = value;
            }
        }

        //public WF_States current_state
        //{
        //    get
        //    {
        //        if (ViewState["current_state"] != null)
        //        {
        //            return (WF_States)ViewState["current_state"];
        //        }
        //        else
        //        {
        //            return WF_States.first_run;
        //        }
        //    }
        //    set
        //    {
        //        ViewState["current_state"] = value;
        //    }
        //}

        #endregion Properties

        #region Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    //divSuccess.Visible = false;

                    getEmp_from_QueryString_or_currentUser();

                    intended_Emp = get_Emp_Info(new Emp(), strEmpDisplayName);
                    bind_Emp_Info();

                    if (!IsPostBack)
                    {
                        //  fill_ddlCat();

                        Bind_Data_To_Controls();

                        //  ddlCat_SelectedIndexChanged(new object(), new EventArgs());

                        //  Planning_Consultant_Email = Emp_DAL.get_Planning_Consultant_Email();
                    }
                });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        public Emp get_Emp_Info(Emp pIntended_Emp, string strEmpDisplayName)
        {
            try
            {
                SPSite site = SPContext.Current.Site;
                SPWeb web = site.OpenWeb();
                SPPrincipalInfo pinfo = SPUtility.ResolvePrincipal(web, strEmpDisplayName, SPPrincipalType.User, SPPrincipalSource.All, null, false);
                SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                UserProfile cUserProfile = userProfileMgr.GetUserProfile(pinfo.LoginName);
                pIntended_Emp.login_name_to_convert_to_SPUser = pinfo.LoginName;

                pIntended_Emp.Emp_DisplayName = pinfo.DisplayName;
                if (cUserProfile.GetProfileValueCollection("AboutMe")[0] != null)
                {
                    pIntended_Emp.Emp_ArabicName = cUserProfile.GetProfileValueCollection("AboutMe")[0].ToString();
                }
                else
                {
                    pIntended_Emp.Emp_ArabicName = string.Empty;
                }

                pIntended_Emp.Emp_email = pinfo.Email;
                //lblEmpName.Text = emp.Name;

                pIntended_Emp.Emp_JobTitle = pinfo.JobTitle;
                //lblEmpJob.Text = cUserProfile.GetProfileValueCollection("Title")[0].ToString();

                string dA = get_Dept_Arabic_Name(pinfo.Department);
                pIntended_Emp.Emp_Department = string.IsNullOrEmpty(dA) ? pinfo.Department : dA;
                //lblEmpDept.Text = cUserProfile.GetProfileValueCollection("Department")[0].ToString();

                if (cUserProfile.GetProfileValueCollection("Fax")[0] != null)
                {
                    pIntended_Emp.Emp_Rank = cUserProfile.GetProfileValueCollection("Fax")[0].ToString();
                }
                else
                {
                    pIntended_Emp.Emp_Rank = string.Empty;
                }

                UserProfile DM_UserProfile = userProfileMgr.GetUserProfile(cUserProfile.GetProfileValueCollection("Manager")[0].ToString());
                pIntended_Emp.DM_name = DM_UserProfile.DisplayName;
                pIntended_Emp.DM_email = DM_UserProfile["WorkEmail"].ToString();
            }
            catch (Exception)
            {
            }
            return pIntended_Emp;
        }

        private string get_Dept_Arabic_Name(string department)
        {
            string dArabic = string.Empty;
            switch (department)
            {
                case "Support Services Department":
                    dArabic = "إدارة الخدمات المساندة";
                    break;

                case "Projects Department":
                    dArabic = "إدارة المشاريع";
                    break;
            }
            return dArabic;
        }
        private void getEmp_from_QueryString_or_currentUser()
        {
            if (Request.QueryString["empid"] != null)
            {
                strEmpDisplayName = Request.QueryString["empid"].ToString();
                //btnSubmit.Visible = false;
            }
            else
            {
                strEmpDisplayName = SPContext.Current.Web.CurrentUser.Name;
            }
        }

        private void bind_Emp_Info()
        {
            //if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
            //{
            //    lblEmpName.Text = intended_Emp.Emp_ArabicName;
            //}
            //else
            //{
            //    lblEmpName.Text = intended_Emp.Emp_DisplayName;
            //}

            //lblEmpDept.Text = intended_Emp.Emp_Department;
            //lblEmpJob.Text = intended_Emp.Emp_JobTitle;
            //lblEmpRank.Text = intended_Emp.Emp_Rank;
            //lblEmpDM.Text = intended_Emp.Emp_DM_name;
        }

        //private void fill_ddlCat()
        //{
        //    SPSecurity.RunWithElevatedPrivileges(delegate ()
        //    {
        //        SPSite oSite = new SPSite(SPContext.Current.Web.Url);
        //        SPWeb spWeb = oSite.OpenWeb();
        //        SPList spList = spWeb.Lists.TryGetList("أصناف المخازن");

        //        SPFieldChoice CatChoice = (SPFieldChoice)spList.Fields["المجموعة"];
        //        for (int i = 0; i < CatChoice.Choices.Count; i++)
        //        {
        //            ddlCat.Items.Add(CatChoice.Choices[i].ToString());
        //        }
        //    });
        //}

        //protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ddlPrimaryGoal.Items.Clear();
        //        ddlPrimaryGoal.Items.Add(new ListItem("اختر الصنف المطلوب", "0"));
        //        // fill_ddlPrimaryGoal();
        //        ddlPrimaryGoal.DataSource = tblPrimaryGoal;
        //        ddlPrimaryGoal.DataValueField = "Title";
        //        ddlPrimaryGoal.DataTextField = "Title";
        //        ddlPrimaryGoal.DataBind();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void fill_ddlPrimaryGoal()
        //{
        //    SPSecurity.RunWithElevatedPrivileges(delegate ()
        //    {
        //        SPSite oSite = new SPSite(SPContext.Current.Web.Url);
        //        SPWeb spWeb = oSite.OpenWeb();
        //        SPList spList = spWeb.Lists.TryGetList("أصناف المخازن");
        //        if (spList != null)
        //        {
        //            SPQuery qry = new SPQuery();
        //            qry.Query =
        //            @"   <Where>
        //                            <Eq>
        //                                <FieldRef Name='Category' />
        //                                <Value Type='Choice'>" + ddlCat.SelectedItem.Text + @"</Value>
        //                            </Eq>
        //                        </Where>";
        //            qry.ViewFieldsOnly = true;
        //            qry.ViewFields = @"<FieldRef Name='Title' />";
        //            SPListItemCollection listItems = spList.GetItems(qry);
        //            tblPrimaryGoal = listItems.GetDataTable();
        //        }
        //    });
        //}

        protected void Bind_Data_To_Controls()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                //gvw_Requested_Items.DataSource = tbl_Requested_Items;
                //gvw_Requested_Items.DataBind();
            });
        }

        #endregion Load

        #region Grid

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    DataRow NewRow = tbl_Requested_Items.NewRow();

                    //NewRow["ItemGeneralName"] = ddlPrimaryGoal.SelectedItem.Text;
                    //NewRow["Quantity"] = int.Parse(txtQuantity.Text);
                    NewRow["Notes"] = txtNotes.Text;

                    tbl_Requested_Items.Rows.Add(NewRow);
                    Bind_Data_To_Controls();
                });
            }
            catch (Exception)
            {
            }
        }

        //protected void gvw_Requested_Items_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    try
        //    {
        //        SPSecurity.RunWithElevatedPrivileges(delegate ()
        //        {
        //            gvw_Requested_Items.EditIndex = e.NewEditIndex;
        //            Bind_Data_To_Controls();
        //            // GridViewRow row = (GridViewRow)gvw_Requested_Items.Rows[e.NewEditIndex];
        //            // ((DropDownList)row.Cells[2].FindControl("ddlObjQ_gv")).SelectedValue = tblObjectives.Rows[e.NewEditIndex][2].ToString();
        //        });
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //protected void gvw_Requested_Items_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        SPSecurity.RunWithElevatedPrivileges(delegate ()
        //        {
        //            GridViewRow row = (GridViewRow)gvw_Requested_Items.Rows[e.RowIndex];
        //            tbl_Requested_Items.Rows[e.RowIndex]["Quantity"] = int.Parse(e.NewValues[0].ToString());
        //            tbl_Requested_Items.Rows[e.RowIndex]["Notes"] = Convert.ToString(e.NewValues[1]);
        //            gvw_Requested_Items.EditIndex = -1;
        //            Bind_Data_To_Controls();
        //        });
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //protected void gvw_Requested_Items_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    try
        //    {
        //        SPSecurity.RunWithElevatedPrivileges(delegate ()
        //        {
        //            gvw_Requested_Items.EditIndex = -1;
        //            Bind_Data_To_Controls();
        //        });
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //protected void gvw_Requested_Items_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        SPSecurity.RunWithElevatedPrivileges(delegate ()
        //        {
        //            tbl_Requested_Items.Rows.RemoveAt(e.RowIndex);
        //            Bind_Data_To_Controls();
        //        });
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        #endregion Grid

        #region Save

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    if (Page.IsValid)
                    {
                        string b_guid = SaveToSP();
                        //Show_Success_Message("تم حفظ الطلب بنجاح");
                        //Send_Request_Email(ServicesRequestTypes.Stationery, b_guid);
                        Send_Serivces_Request_Email_to_DM_To_Approve(b_guid);
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        private string SaveToSP()
        {
            string b_guid = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb oWeb = oSite.OpenWeb();
                oWeb.AllowUnsafeUpdates = true;
                SPList oList = oWeb.GetList(oWeb.ServerRelativeUrl + "/Lists/PurchasingStoresRequests");

                if (tbl_Requested_Items != null)
                {
                    b_guid = Guid.NewGuid().ToString();

                    foreach (DataRow row in tbl_Requested_Items.Rows)
                    {
                        SPListItem oListItem = oList.AddItem();

                        oListItem["Emp"] = SPContext.Current.Web.EnsureUser(intended_Emp.login_name_to_convert_to_SPUser);
                        oListItem["EmpArabicName"] = string.IsNullOrEmpty(intended_Emp.Emp_ArabicName) ? "غير متاح" : intended_Emp.Emp_ArabicName;
                        oListItem["Dept"] = intended_Emp.Emp_Department;
                        oListItem["ItemGeneralName"] = row["ItemGeneralName"].ToString();
                        oListItem["Quantity"] = Convert.ToInt32(row["Quantity"]);
                        oListItem["Notes"] = row["Notes"].ToString();
                        oListItem["RequestBatchGuid"] = b_guid;
                        oListItem["Status"] = "جديد";

                        oListItem.Update();
                    }
                }
                else
                {
                }

                oWeb.AllowUnsafeUpdates = false;
            });
            return b_guid;
        }

        //private void Show_Success_Message(string m)
        //{
        //    divSuccess.Visible = true;
        //    lblSuccess.Text = m;
        //}

        #endregion Save

        #region Email

        private void Send_Request_Email(ServicesRequestTypes pRType, string b_guid)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string layoutsPath = SPUtility.GetVersionedGenericSetupPath("TEMPLATE\\Layouts\\ServicesDeptTabs\\", 15);
                string html = File.ReadAllText(layoutsPath + "Serivces_Request_Email.html");
                StringBuilder bodyText = new StringBuilder(html);

                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                string t = ServicesRequestTypesUtils.get_Enum_Arabic(pRType);

                bodyText.Replace("#EmpName#", n);
                bodyText.Replace("#Dept#", intended_Emp.Emp_Department);
                bodyText.Replace("#Type#", t);

                bodyText.Replace("#RequestURL#", "<a href=" + SPContext.Current.Web.Url + "/Pages/StoresRequestView.aspx?rid=" + b_guid + " >عرض الطلب</a>");

                StringDictionary headers = new StringDictionary();
                headers.Add("to", Get_Request_Receiver_Email(t));
                headers.Add("cc", "sherif@zayed.org.ae");
                headers.Add("subject", " قام " + "\"" + n + "\"" + " بعمل طلب جديد من قسم الخدمات ");
                headers.Add("content-type", "text/html");

                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        private string Get_Request_Receiver_Email(string pT)
        {
            string email = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.Lists.TryGetList("مسؤولى طلبات الخدمات");
                SPQuery qry = new SPQuery();
                qry.Query =
                              @"   <Where>
                                              <Eq>
                                                 <FieldRef Name='Title' />
                                                 <Value Type='Text'>" + pT + @"</Value>
                                              </Eq>
                                           </Where>";
                qry.ViewFieldsOnly = true;
                qry.ViewFields = @"<FieldRef Name='Email' />";
                SPListItemCollection listItems = spList.GetItems(qry);

                email = listItems[0][0].ToString();
            });
            return email;
        }

        private void Send_Serivces_Request_Email_to_DM_To_Approve(string b_guid)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string layoutsPath = SPUtility.GetVersionedGenericSetupPath("TEMPLATE\\Layouts\\ServicesDeptTabs\\", 15);
                string html = File.ReadAllText(layoutsPath + "Serivces_Request_Email_to_DM_To_Approve.html");
                StringBuilder bodyText = new StringBuilder(html);

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

        #endregion Email
    }
}