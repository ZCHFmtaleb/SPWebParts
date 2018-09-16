using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPWebParts.EPM.SetObjectivesWP
{
    public partial class SetObjectivesWPUserControl : UserControl
    {
        #region Properties

        public DataTable tblObjectives
        {
            get
            {
                if (ViewState["tblObjectives"]!= null)
                {
                    return (DataTable)ViewState["tblObjectives"];
                }
                else
                {
                    tblObjectives = new DataTable();
                    tblObjectives.Columns.Add("ObjName");
                    tblObjectives.Columns.Add("ObjWeight", typeof(Int32));
                    tblObjectives.Columns.Add("ObjQ");
                    return tblObjectives;
                }
                
            }
            set
            {
                ViewState["tblObjectives"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            divSuccess.Visible = false;

            getEmp_from_QueryString_or_currentUser();

            Fill_Emp_Info();

            if (!IsPostBack)
            {
                getPreviouslySavedObjectives();

                Bind_Data_To_Controls();
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
                strEmpDisplayName = SPContext.Current.Web.CurrentUser.Name;          //"Test spuser_1"; //"sherif abdellatif";
            }
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
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' /><FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' /><FieldRef Name='ObjWeight' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tblObjectives = listItems.GetDataTable();
                        }
                    }
                }
            });
        }

        private void Fill_Emp_Info()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = SPContext.Current.Site)
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPPrincipalInfo pinfo = SPUtility.ResolvePrincipal(web, strEmpDisplayName, SPPrincipalType.User, SPPrincipalSource.All, null, false);
                        //SPUser emp = web.Users[pinfo.LoginName];

                        lblEmpName.Text = pinfo.DisplayName;
                        //lblEmpName.Text = emp.Name;

                        lblEmpJob.Text = pinfo.JobTitle;
                        //lblEmpJob.Text = cUserProfile.GetProfileValueCollection("Title")[0].ToString();
                        lblEmpDept.Text = pinfo.Department;
                        //lblEmpDept.Text = cUserProfile.GetProfileValueCollection("Department")[0].ToString();

                        SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                        UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                        UserProfile cUserProfile = userProfileMgr.GetUserProfile(pinfo.LoginName);
                        UserProfile DM_UserProfile = userProfileMgr.GetUserProfile(cUserProfile.GetProfileValueCollection("Manager")[0].ToString());
                        lblEmpDM.Text = DM_UserProfile.DisplayName;
                    }
                }
            });
        }

        protected void btnAddObjective_Click(object sender, EventArgs e)
        {
            DataRow NewRow = tblObjectives.NewRow();
            NewRow["ObjName"] = txtObjName.Text; ;
            NewRow["ObjWeight"] = txtObjWeight.Text;
            NewRow["ObjQ"] = ddlObjQ.SelectedItem.Text;
            tblObjectives.Rows.Add(NewRow);
            Bind_Data_To_Controls();
        }

        protected void Bind_Data_To_Controls()
        {
            gvwSetObjectives.DataSource = tblObjectives;
            gvwSetObjectives.DataBind();
            Refresh_GoalsTotal();
        }

        private void Refresh_GoalsTotal()
        {
            if (tblObjectives != null && tblObjectives.Rows.Count > 0)
            {
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
            }
            else
            {
                lbl_PercentageTotal.Text = "0";
            }
        }

        protected void gvwSetObjectives_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            tblObjectives.Rows.RemoveAt(e.RowIndex);
            Bind_Data_To_Controls();
        }

        protected void gvwSetObjectives_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvwSetObjectives.EditIndex = e.NewEditIndex;
            Bind_Data_To_Controls();
            GridViewRow row = (GridViewRow)gvwSetObjectives.Rows[e.NewEditIndex];
            ((DropDownList)row.Cells[2].FindControl("ddlObjQ_gv")).SelectedValue = tblObjectives.Rows[e.NewEditIndex][2].ToString();
        }

        protected void gvwSetObjectives_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvwSetObjectives.Rows[e.RowIndex];
            tblObjectives.Rows[e.RowIndex][0] = e.NewValues[0].ToString(); //((TextBox)row.Cells[0].Controls[0]).Text;
            tblObjectives.Rows[e.RowIndex][1] = e.NewValues[1].ToString();
            tblObjectives.Rows[e.RowIndex][2] = ((DropDownList)row.Cells[2].FindControl("ddlObjQ_gv")).SelectedValue;
            gvwSetObjectives.EditIndex = -1;
            Bind_Data_To_Controls();
        }

        protected void gvwSetObjectives_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvwSetObjectives.EditIndex = -1;
            Bind_Data_To_Controls();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveToSP();
            }
           
        }

        private void SaveToSP()
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
                            oListItem["Status"] = "جديد";
                            oListItem["Emp"] = SPContext.Current.Web.CurrentUser;
                            oListItem["ObjWeight"] = row["ObjWeight"].ToString();
                            oListItem["ObjQ"] = row["ObjQ"].ToString();
                            oListItem["ObjYear"] = DateTime.Now.Year + 1;
                            oListItem.Update();
                        }
                        divSuccess.Visible = true;
                    }
                    else
                    {
                    }

                    #endregion Add the new (or updated) objectives

                    oWeb.AllowUnsafeUpdates = false;
                }
            }
        }

        protected void cvld_PercentageTotal_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (PercentageTotal==100)
                e.IsValid = true;
            else
                e.IsValid = false;
        }
    }
}