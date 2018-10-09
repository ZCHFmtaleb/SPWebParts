using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;

namespace SPWebParts.EPM.RateObjectivesEmpWP
{
    public partial class RateObjectivesEmpWPUserControl : UserControl
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
                    tblObjectives.Columns.Add("ID");
                    tblObjectives.Columns.Add("ObjName");
                    tblObjectives.Columns.Add("ObjWeight", typeof(Int32));
                    tblObjectives.Columns.Add("AccPercent");
                    return tblObjectives;
                }
            }
            set
            {
                ViewState["tblObjectives"] = value;
            }
        }

        public DataTable tbl_Std_Skills
        {
            get
            {
                if (ViewState["tbl_Std_Skills"] != null)
                {
                    return (DataTable)ViewState["tbl_Std_Skills"];
                }
                else
                {
                    tblObjectives = new DataTable();
                    tblObjectives.Columns.Add("ID");
                    tblObjectives.Columns.Add("Title");
                    return tbl_Std_Skills;
                }
            }
            set
            {
                ViewState["tbl_Std_Skills"] = value;
            }
        }

        public DataTable tbl_Lead_Skills
        {
            get
            {
                if (ViewState["tbl_Lead_Skills"] != null)
                {
                    return (DataTable)ViewState["tbl_Lead_Skills"];
                }
                else
                {
                    tblObjectives = new DataTable();
                    tblObjectives.Columns.Add("ID");
                    tblObjectives.Columns.Add("Title");
                    return tbl_Lead_Skills;
                }
            }
            set
            {
                ViewState["tbl_Lead_Skills"] = value;
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

        #endregion Properties

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

                         getStandardSkills();

                         getLeadSkills();

                         Bind_Data_To_Controls();
                     }
                 });
            }
            catch (Exception)
            {
              
            }
        }

        private void getLeadSkills()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("الكفاءات");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =
                            @"   <Where>
                                      <Eq>
                                         <FieldRef Name='CompType' />
                                         <Value Type='Choice'>كفاءة قيادية</Value>
                                      </Eq>
                                   </Where>";
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Title' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tbl_Lead_Skills = listItems.GetDataTable();
                        }
                    }
                }
            });
        }

        private void getStandardSkills()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("الكفاءات");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =
                            @"   <Where>
                                      <Eq>
                                         <FieldRef Name='CompType' />
                                         <Value Type='Choice'>كفاءة أساسية</Value>
                                      </Eq>
                                   </Where>";
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Title' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tbl_Std_Skills = listItems.GetDataTable();
                        }
                    }
                }
            });
        }

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
                    SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                    UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                    UserProfile cUserProfile = userProfileMgr.GetUserProfile(pinfo.LoginName);
                    //SPUser emp = web.Users[pinfo.LoginName];

                    lblEmpName.Text = pinfo.DisplayName;
                    intended_Emp.Emp_DisplayName = pinfo.DisplayName;
                    //lblEmpName.Text = emp.Name;

                    intended_Emp.Emp_email = pinfo.Email;

                    lblEmpJob.Text = pinfo.JobTitle;
                    //intended_Emp.Emp_JobTitle = pinfo.JobTitle;
                    lblEmpJob.Text = cUserProfile.GetProfileValueCollection("Title")[0].ToString();

                    lblEmpDept.Text = pinfo.Department;
                    //intended_Emp.Emp_Department = pinfo.Department;
                    lblEmpDept.Text = cUserProfile.GetProfileValueCollection("Department")[0].ToString();

                
                    
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
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='ObjWeight' /><FieldRef Name='AccPercent' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tblObjectives = listItems.GetDataTable();
                        }
                    }
                }
            });
        }

        protected void Bind_Data_To_Controls()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwRate.DataSource = tblObjectives;
                gvw_Std_Skills.DataSource = tbl_Std_Skills;
                gvw_Lead_Skills.DataSource = tbl_Lead_Skills;
                gvwRate.DataBind();
                gvw_Std_Skills.DataBind();
                gvw_Lead_Skills.DataBind();
            });
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (Page.IsValid)
                {
                    //SaveToSP();
                }
            });
        }
    }
}