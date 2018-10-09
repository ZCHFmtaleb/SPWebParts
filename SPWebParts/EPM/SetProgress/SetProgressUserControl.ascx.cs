using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPWebParts.EPM.SetProgress
{
    public partial class SetProgressUserControl : UserControl
    {
        #region Properties

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
                    tblObjectives.Columns.Add("ID");
                    tblObjectives.Columns.Add("ObjYear");
                    tblObjectives.Columns.Add("ObjName");
                    tblObjectives.Columns.Add("ObjWeight");
                    tblObjectives.Columns.Add("AccPercent");
                    return tblObjectives;
                }
            }
            set
            {
                ViewState["tblObjectives"] = value;
            }
        }

        #endregion Properties

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                 {
                     divSuccess.Visible = false;

                     getEmp_from_QueryString_or_currentUser();

                     Fill_Emp_Info();

                     if (!IsPostBack)
                     {
                         getPreviouslySavedObjectives();

                         Bind_Data_To_Controls();
                     }
                 });
            }
            catch (Exception)
            {
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                  {
                      if (Page.IsValid)
                      {
                          SaveToSP();
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
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        oWeb.AllowUnsafeUpdates = true;
                        SPList oList = oWeb.Lists["الأهداف"];

                        #region Add the new (or updated) objectives

                        if (tblObjectives != null)
                        {
                            foreach (DataRow row in tblObjectives.Rows)
                            {
                                SPListItem oListItem = oList.GetItemById(int.Parse(row["ID"].ToString()));
                                
                                oListItem["AccPercent"] = row["AccPercent"].ToString();
                                //oListItem["Status"] = "";
                                //oListItem["Emp"] = SPContext.Current.Web.CurrentUser;
                                //oListItem["ObjWeight"] = row["ObjWeight"].ToString();
                                //oListItem["ObjQ"] = row["ObjQ"].ToString();
                                //oListItem["ObjYear"] = DateTime.Now.Year + 1;
                                //oListItem["StrDir_x003a_Title"] = row["StrDir_x003a_Title"].ToString();
                                //oListItem["StrDir"] = int.Parse(row["StrDir"].ToString());

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

        private void getPreviouslySavedObjectives()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("الأهداف");
                        SPQuery qry = new SPQuery();
                        qry.Query =
                        @"   <Where>
                                          <Eq>
                                             <FieldRef Name='Emp' />
                                              <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                          </Eq>
                                       </Where>";
                        qry.ViewFieldsOnly = true;
                        qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjWeight' /><FieldRef Name='AccPercent' />";
                        SPListItemCollection listItems = spList.GetItems(qry);
                        tblObjectives = listItems.GetDataTable();
                    }
                }
            });
        }

        protected void Bind_Data_To_Controls()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwProgress.DataSource = tblObjectives;
                gvwProgress.DataBind();
            });
        }

        #endregion Related to Load

        #endregion Helper Methods

        protected void gvwProgress_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwProgress.EditIndex = e.NewEditIndex;
                Bind_Data_To_Controls();
            });
        }

        protected void gvwProgress_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwProgress.EditIndex = -1;
                Bind_Data_To_Controls();
            });
        }

        protected void gvwProgress_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                GridViewRow row = (GridViewRow)gvwProgress.Rows[e.RowIndex];
                tblObjectives.Rows[e.RowIndex][4] = e.NewValues[3].ToString(); //((TextBox)row.Cells[0].Controls[0]).Text;
                gvwProgress.EditIndex = -1;
                Bind_Data_To_Controls();
            });
        }
    }
}