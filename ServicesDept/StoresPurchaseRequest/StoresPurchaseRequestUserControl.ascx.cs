using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicesDept.StoresPurchaseRequest
{
    public partial class StoresPurchaseRequestUserControl : UserControl
    {
        #region Properties

        //public Emp intended_Emp
        //{
        //    get
        //    {
        //        if (ViewState["intended_Emp"] != null)
        //        {
        //            return (Emp)ViewState["intended_Emp"];
        //        }
        //        else
        //        {
        //            intended_Emp = new Emp();
        //            return intended_Emp;
        //        }
        //    }
        //    set
        //    {
        //        ViewState["intended_Emp"] = value;
        //    }
        //}

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
                    divSuccess.Visible = false;

                    getEmp_from_QueryString_or_currentUser();

                  //  intended_Emp = Emp_DAL.get_Emp_Info(intended_Emp, strEmpDisplayName);
                    bind_Emp_Info();

                    if (!IsPostBack)
                    {
                        fill_ddlCat();

                        Bind_Data_To_Controls();

                        ddlCat_SelectedIndexChanged(new object(), new EventArgs());

                      //  Planning_Consultant_Email = Emp_DAL.get_Planning_Consultant_Email();
                    }
                });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void getEmp_from_QueryString_or_currentUser()
        {
            if (Request.QueryString["empid"] != null)
            {
                strEmpDisplayName = Request.QueryString["empid"].ToString();
                btnSubmit.Visible = false;
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

        private void fill_ddlCat()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.Lists.TryGetList("أصناف المخازن");

                SPFieldChoice CatChoice = (SPFieldChoice)spList.Fields["المجموعة"];
                for (int i = 0; i < CatChoice.Choices.Count; i++)
                {
                    ddlCat.Items.Add(CatChoice.Choices[i].ToString());
                }
            });
        }

        protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlPrimaryGoal.Items.Clear();
                ddlPrimaryGoal.Items.Add(new ListItem("اختر الصنف المطلوب", "0"));
                fill_ddlPrimaryGoal();
                ddlPrimaryGoal.DataSource = tblPrimaryGoal;
                ddlPrimaryGoal.DataValueField = "Title";
                ddlPrimaryGoal.DataTextField = "Title";
                ddlPrimaryGoal.DataBind();
            }
            catch (Exception)
            {
            }
        }

        private void fill_ddlPrimaryGoal()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.Lists.TryGetList("أصناف المخازن");
                if (spList != null)
                {
                    SPQuery qry = new SPQuery();
                    qry.Query =
                    @"   <Where>
                                    <Eq>
                                        <FieldRef Name='Category' />
                                        <Value Type='Choice'>" + ddlCat.SelectedItem.Text + @"</Value>
                                    </Eq>
                                </Where>";
                    qry.ViewFieldsOnly = true;
                    qry.ViewFields = @"<FieldRef Name='Title' />";
                    SPListItemCollection listItems = spList.GetItems(qry);
                    tblPrimaryGoal = listItems.GetDataTable();
                }
            });
        }

        protected void Bind_Data_To_Controls()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvw_Requested_Items.DataSource = tbl_Requested_Items;
                gvw_Requested_Items.DataBind();
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

                    NewRow["ItemGeneralName"] = ddlPrimaryGoal.SelectedItem.Text;
                    NewRow["Quantity"] = int.Parse(txtQuantity.Text);
                    NewRow["Notes"] = txtNotes.Text;

                    tbl_Requested_Items.Rows.Add(NewRow);
                    Bind_Data_To_Controls();
                });
            }
            catch (Exception)
            {
            }
        }

        protected void gvw_Requested_Items_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    gvw_Requested_Items.EditIndex = e.NewEditIndex;
                    Bind_Data_To_Controls();
                    // GridViewRow row = (GridViewRow)gvw_Requested_Items.Rows[e.NewEditIndex];
                    // ((DropDownList)row.Cells[2].FindControl("ddlObjQ_gv")).SelectedValue = tblObjectives.Rows[e.NewEditIndex][2].ToString();
                });
            }
            catch (Exception)
            {
            }
        }

        protected void gvw_Requested_Items_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    GridViewRow row = (GridViewRow)gvw_Requested_Items.Rows[e.RowIndex];
                    tbl_Requested_Items.Rows[e.RowIndex]["Quantity"] = int.Parse(e.NewValues[0].ToString());
                    tbl_Requested_Items.Rows[e.RowIndex]["Notes"] = Convert.ToString(e.NewValues[1]);
                    gvw_Requested_Items.EditIndex = -1;
                    Bind_Data_To_Controls();
                });
            }
            catch (Exception)
            {
            }
        }

        protected void gvw_Requested_Items_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    gvw_Requested_Items.EditIndex = -1;
                    Bind_Data_To_Controls();
                });
            }
            catch (Exception)
            {
            }
        }

        protected void gvw_Requested_Items_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    tbl_Requested_Items.Rows.RemoveAt(e.RowIndex);
                    Bind_Data_To_Controls();
                });
            }
            catch (Exception)
            {
            }
        }

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
                        SaveToSP();
                        Show_Success_Message("تم حفظ الطلب بنجاح");
                        Send_Serivces_Request_Email_to_DM_To_Approve();
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        private void SaveToSP()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb oWeb = oSite.OpenWeb();
                oWeb.AllowUnsafeUpdates = true;
                SPList oList = oWeb.Lists["طلبات إدارة الخدمات - مخازن"];

                if (tbl_Requested_Items != null)
                {
                    string b_guid = Guid.NewGuid().ToString();

                    foreach (DataRow row in tbl_Requested_Items.Rows)
                    {
                        SPListItem oListItem = oList.AddItem();

                        //oListItem["Emp"] = SPContext.Current.Web.EnsureUser(intended_Emp.login_name_to_convert_to_SPUser);
                        //oListItem["Dept"] = intended_Emp.Emp_Department;
                        oListItem["ItemGeneralName"] = row["ItemGeneralName"].ToString();
                        oListItem["Quantity"] = Convert.ToInt32(row["Quantity"]);
                        oListItem["Notes"] = row["Notes"].ToString();
                        oListItem["RequestBatchGuid"] = b_guid;
                        oListItem["Status"] = "new";

                        oListItem.Update();
                    }
                }
                else
                {
                }

                oWeb.AllowUnsafeUpdates = false;
            });
        }

        private void Show_Success_Message(string m)
        {
            divSuccess.Visible = true;
            lblSuccess.Text = m;
        }

        #endregion Save

        #region Email

        private void Send_Serivces_Request_Email_to_DM_To_Approve()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {

                string html = File.ReadAllText(@"{SharePointRoot}\Template\Layouts\ServicesDept\Serivces_Request_Email_to_DM_To_Approve.html");

                StringBuilder sb = new StringBuilder();


                //string n = string.Empty;
                //if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                //{
                //    n = intended_Emp.Emp_ArabicName;
                //}
                //else
                //{
                //    n = intended_Emp.Emp_DisplayName;
                //}

                //StringDictionary headers = new StringDictionary();
                //headers.Add("to", intended_Emp.Emp_DM_email);
                //headers.Add("subject", " قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                //headers.Add("content-type", "text/html");
                //StringBuilder bodyText = new StringBuilder();
                //bodyText.Append("<p dir=rtl >");
                //bodyText.Append("السلام عليكم ورحمة الله"); bodyText.Append("<br />");
                //bodyText.Append("تحية طيبة وبعد:"); bodyText.Append("<br />");
                //bodyText.Append(" قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                //bodyText.Append("<br />");
                //bodyText.Append("وتمت مراجعتها واعتمادها مبدئيا بواسطة مستشار التخطيط الاستراتيجى وتطوير الأداء ");
                //bodyText.Append("<br />");
                //bodyText.Append("الرجاء القيام بمراجعة الأهداف من خلال هذا الرابط واعتمادها نهائيا");
                //bodyText.Append("<br />");
                //bodyText.Append("<a href=" + SPContext.Current.Web.Url + "/Pages/SetObjectives.aspx?empid=" + HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName) + "  >" + n + "</a>");
                //bodyText.Append("</p>");
                //SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        #endregion Email
    }
}