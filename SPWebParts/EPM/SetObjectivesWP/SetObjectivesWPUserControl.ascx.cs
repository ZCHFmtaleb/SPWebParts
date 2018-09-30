using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPWebParts.EPM.SetObjectivesWP
{
    public partial class SetObjectivesWPUserControl : UserControl
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

        public DataTable tblObjectives
        {
            get
            {
                if (ViewState["tblObjectives"] != null)
                {
                    return (DataTable)ViewState["tblObjectives"];
                }
                else
                {
                    tblObjectives = new DataTable();
                    tblObjectives.Columns.Add("ObjName");
                    tblObjectives.Columns.Add("ObjWeight", typeof(Int32));
                    tblObjectives.Columns.Add("ObjQ");
                    tblObjectives.Columns.Add("StrDir_x003a_Title");
                    tblObjectives.Columns.Add("StrDir");
                    tblObjectives.Columns.Add("Status");
                    return tblObjectives;
                }
            }
            set
            {
                ViewState["tblObjectives"] = value;
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

        #endregion Properties

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                divSuccess.Visible = false;
                divApprovalSuccess.Visible = false;

                getEmp_from_QueryString_or_currentUser();

                Fill_Emp_Info();

                if (SPContext.Current.Web.CurrentUser.Email == intended_Emp.Emp_DM_email)
                {
                    btnSubmit.Visible = false;
                    btnApprove.Visible = true;
                }
                else if (SPContext.Current.Web.CurrentUser.Email == intended_Emp.Emp_email)
                {
                    btnSubmit.Visible = true;
                    btnApprove.Visible = false;
                }
                else
                {
                    btnSubmit.Visible = false;
                    btnApprove.Visible = false;
                }

                if (!IsPostBack)
                {
                    fill_ddlStrDir();

                    getPreviouslySavedObjectives();

                    Bind_Data_To_Controls();
                }
            });
        }

        protected void btnAddObjective_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                DataRow NewRow = tblObjectives.NewRow();
                NewRow["ObjName"] = txtObjName.Text; ;
                NewRow["ObjWeight"] = txtObjWeight.Text;
                NewRow["ObjQ"] = ddlObjQ.SelectedItem.Text;
                NewRow["StrDir_x003a_Title"] = ddlStrDir.SelectedItem.Text;
                NewRow["StrDir"] = ddlStrDir.SelectedItem.Value;
                tblObjectives.Rows.Add(NewRow);
                Bind_Data_To_Controls();
            });
        }

        protected void gvwSetObjectives_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwSetObjectives.EditIndex = e.NewEditIndex;
                Bind_Data_To_Controls();
                GridViewRow row = (GridViewRow)gvwSetObjectives.Rows[e.NewEditIndex];
                ((DropDownList)row.Cells[2].FindControl("ddlObjQ_gv")).SelectedValue = tblObjectives.Rows[e.NewEditIndex][2].ToString();
            });
        }

        protected void gvwSetObjectives_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwSetObjectives.EditIndex = -1;
                Bind_Data_To_Controls();
            });
        }

        protected void gvwSetObjectives_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                GridViewRow row = (GridViewRow)gvwSetObjectives.Rows[e.RowIndex];
                tblObjectives.Rows[e.RowIndex][0] = e.NewValues[0].ToString(); //((TextBox)row.Cells[0].Controls[0]).Text;
                tblObjectives.Rows[e.RowIndex][1] = e.NewValues[1].ToString();
                tblObjectives.Rows[e.RowIndex][2] = ((DropDownList)row.Cells[2].FindControl("ddlObjQ_gv")).SelectedValue;
                gvwSetObjectives.EditIndex = -1;
                Bind_Data_To_Controls();
            });
        }

        protected void gvwSetObjectives_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                tblObjectives.Rows.RemoveAt(e.RowIndex);
                Bind_Data_To_Controls();
            });
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (Page.IsValid)
                {
                    SaveToSP();
                }
            });
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        oWeb.AllowUnsafeUpdates = true;
                        SPList oList = oWeb.Lists["الأهداف"];

                        SPQuery qry = new SPQuery();
                        qry.Query =
                        @"   <Where>
                                        <Eq>
                                            <FieldRef Name='Emp' />
                                            <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                        </Eq>
                                    </Where>";
                        qry.ViewFieldsOnly = true;
                        qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' /><FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' /><FieldRef Name='ObjWeight' />";
                        SPListItemCollection listItems = oList.GetItems(qry);

                        foreach (SPListItem item in listItems)
                        {
                            SPListItem c = oList.GetItemById(item.ID);
                            c["Status"] = "معتمدة";
                            c.Update();
                        }

                        oWeb.AllowUnsafeUpdates = false;

                        lblStatus.Text = "تم اعتماد الأهداف";
                        divApprovalSuccess.Visible = true;
                        Notify_Emp_that_Goals_got_Approved();
                    }
                }
            });
        }

        #endregion Event Handlers

        #region Helper Methods

        #region Related to Load

        private void getEmp_from_QueryString_or_currentUser()
        {
            if (Request.QueryString["empid"] != null)
            {
                strEmpDisplayName = Request.QueryString["empid"].ToString();
                btnSubmit.Visible = false;
            }
            else
            {
                strEmpDisplayName = SPContext.Current.Web.CurrentUser.Name;          //"Test spuser_1"; //"sherif abdellatif";
            }
        }

        private void Fill_Emp_Info()
        {
            using (SPSite site = SPContext.Current.Site)
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPPrincipalInfo pinfo = SPUtility.ResolvePrincipal(web, strEmpDisplayName, SPPrincipalType.User, SPPrincipalSource.All, null, false);
                    //SPUser emp = web.Users[pinfo.LoginName];

                    lblEmpName.Text = pinfo.DisplayName;
                    intended_Emp.Emp_DisplayName = pinfo.DisplayName;
                    //lblEmpName.Text = emp.Name;

                    intended_Emp.Emp_email = pinfo.Email;

                    lblEmpJob.Text = pinfo.JobTitle;
                    intended_Emp.Emp_JobTitle = pinfo.JobTitle;
                    //lblEmpJob.Text = cUserProfile.GetProfileValueCollection("Title")[0].ToString();

                    lblEmpDept.Text = pinfo.Department;
                    intended_Emp.Emp_Department = pinfo.Department;
                    //lblEmpDept.Text = cUserProfile.GetProfileValueCollection("Department")[0].ToString();

                    SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                    UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                    UserProfile cUserProfile = userProfileMgr.GetUserProfile(pinfo.LoginName);
                    UserProfile DM_UserProfile = userProfileMgr.GetUserProfile(cUserProfile.GetProfileValueCollection("Manager")[0].ToString());
                    intended_Emp.Emp_DM_email = DM_UserProfile["WorkEmail"].ToString(); ;
                    lblEmpDM.Text = DM_UserProfile.DisplayName;
                    intended_Emp.Emp_DM_name = DM_UserProfile.DisplayName;
                }
            }
        }

        private void fill_ddlStrDir()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("التوجهات الاستراتيجية");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Title' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tblStrDir = listItems.GetDataTable();
                        }
                    }
                }
            });
        }

        private void getPreviouslySavedObjectives()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("الأهداف");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =
                            @"   <Where>
                                        <Eq>
                                            <FieldRef Name='Emp' />
                                            <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                        </Eq>
                                    </Where>";
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' /><FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' />
                                                            <FieldRef Name='ObjWeight' /><FieldRef Name='StrDir' /><FieldRef Name='StrDir_x003a_Title' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tblObjectives = listItems.GetDataTable();
                        }
                    }
                }
            });
        }

        private void Refresh_GoalsTotal()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (tblObjectives != null && tblObjectives.Rows.Count > 0)
                {
                    lblStatus.Text = "تحت المراجعة";

                    PercentageTotal = int.Parse(tblObjectives.Compute("Sum(ObjWeight)", string.Empty).ToString());
                    lbl_PercentageTotal.Text = PercentageTotal.ToString();
                    if (PercentageTotal == 100)
                    {
                        lbl_PercentageTotal.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lbl_PercentageTotal.BackColor = Color.Pink;
                    }

                    if (tblObjectives.Rows[0]["Status"].ToString() == "معتمدة")
                    {
                        lblStatus.Text = "تم اعتماد الأهداف";
                    }
                }
                else
                {
                    lbl_PercentageTotal.Text = "0";
                    lblStatus.Text = "لم يتم وضع الأهداف";
                }
            });
        }

        protected void Bind_Data_To_Controls()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwSetObjectives.DataSource = tblObjectives;
                gvwSetObjectives.DataBind();
                ddlStrDir.DataSource = tblStrDir;
                ddlStrDir.DataValueField = "ID";
                ddlStrDir.DataTextField = "Title";
                ddlStrDir.DataBind();
                Refresh_GoalsTotal();
            });
        }

        #endregion Related to Load

        #region Related to Save

        protected void cvld_PercentageTotal_ServerValidate(object source, ServerValidateEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (PercentageTotal == 100)
                    e.IsValid = true;
                else
                    e.IsValid = false;
            });
        }

        private void SaveToSP()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        oWeb.AllowUnsafeUpdates = true;
                        SPList oList = oWeb.Lists["الأهداف"];

                        #region Remove any previous objectives of same Emp

                        SPQuery qry = new SPQuery();
                        qry.Query =
                        @"   <Where>
                                        <Eq>
                                            <FieldRef Name='Emp' />
                                            <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                        </Eq>
                                    </Where>";
                        qry.ViewFieldsOnly = true;
                        qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' /><FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' /><FieldRef Name='ObjWeight' />";
                        SPListItemCollection listItems = oList.GetItems(qry);

                        foreach (SPListItem item in listItems)
                        {
                            oList.GetItemById(item.ID).Delete();
                        }

                        #endregion Remove any previous objectives of same Emp

                        #region Add the new (or updated) objectives

                        if (tblObjectives != null)
                        {
                            foreach (DataRow row in tblObjectives.Rows)
                            {
                                SPListItem oListItem = oList.AddItem();
                                oListItem["ObjName"] = row["ObjName"].ToString();
                                //oListItem["Status"] = "";
                                oListItem["Emp"] = SPContext.Current.Web.CurrentUser;
                                oListItem["ObjWeight"] = row["ObjWeight"].ToString();
                                oListItem["ObjQ"] = row["ObjQ"].ToString();
                                oListItem["ObjYear"] = DateTime.Now.Year + 1;
                                //oListItem["StrDir_x003a_Title"] = row["StrDir_x003a_Title"].ToString();
                                oListItem["StrDir"] = int.Parse(row["StrDir"].ToString());
                                oListItem.Update();
                            }
                            divSuccess.Visible = true;
                        }
                        else
                        {
                        }

                        #endregion Add the new (or updated) objectives

                        oWeb.AllowUnsafeUpdates = false;

                        Send_Email_to_DM();
                    }
                }
            });
        }

        #endregion Related to Save

        #region Related to Email

        private void Send_Email_to_DM()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_DM_email);
                headers.Add("cc", intended_Emp.Emp_email);
                headers.Add("subject", " قام " + intended_Emp.Emp_DisplayName + " بوضع الأهداف الفردية لعام " + (DateTime.Now.Year + 1));
                headers.Add("content-type", "text/html");
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append("<p dir=rtl >");
                bodyText.Append(" قام " + "\"" + intended_Emp.Emp_DisplayName + "\"" + " بوضع الأهداف الفردية لعام " + (DateTime.Now.Year + 1));
                bodyText.Append("<br />");
                bodyText.Append("الرجاء القيام بمراجعة الأهداف من خلال هذا الرابط واعتمادها ، او الاتصال به مباشرة لمناقشة أية تغييرات مقترحة");
                bodyText.Append("<br />");
                bodyText.Append("<a href=" + SPContext.Current.Web.Url + "/SitePages/%D9%86%D9%85%D9%88%D8%B0%D8%AC%20%D9%88%D8%B6%D8%B9%20%D8%A7%D9%84%D8%A3%D9%87%D8%AF%D8%A7%D9%81.aspx?empid=" + HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName) + "  >" + intended_Emp.Emp_DisplayName + "</a>");
                bodyText.Append("</p>");
                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        private void Notify_Emp_that_Goals_got_Approved()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_email);
                headers.Add("subject", " قام " + intended_Emp.Emp_DM_name + " باعتماد الأهداف الفردية الخاصة بك لعام " + (DateTime.Now.Year + 1));
                headers.Add("content-type", "text/html");
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append("<p dir=rtl >");
                bodyText.Append(" قام " + "\"" + intended_Emp.Emp_DM_name + "\"" + " باعتماد الأهداف الفردية الخاصة بك لعام " + (DateTime.Now.Year + 1));
                bodyText.Append("</p>");
                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        #endregion Related to Email

        #endregion Helper Methods
    }
}