using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using EPM.EL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPM.UI.SelectEmp
{
    public partial class SelectEmpUserControl : UserControl
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

        public DataTable tblEmps
        {
            get
            {
                if (ViewState["tblEmps"] != null)
                {
                    return (DataTable)ViewState["tblEmps"];
                }
                else
                {
                    tblEmps = new DataTable();
                    tblEmps.Columns.Add("EmpName");
                    tblEmps.Columns.Add("EnglishName");
                    tblEmps.Columns.Add("EmpJob");
                    return tblEmps;
                }
            }
            set
            {
                ViewState["tblEmps"] = value;
            }
        }

        #endregion Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    strEmpDisplayName = SPContext.Current.Web.CurrentUser.Name;

                    if (!IsPostBack)
                    {
                        getEmps();

                        Bind_Data_To_Controls();
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        private void getEmps()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                    SPWeb spWeb = oSite.OpenWeb();
                    SPPrincipalInfo pinfo = SPUtility.ResolvePrincipal(spWeb, strEmpDisplayName, SPPrincipalType.User, SPPrincipalSource.All, null, false);
                    SPServiceContext serviceContext = SPServiceContext.GetContext(oSite);
                    UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                    UserProfile cUserProfile = userProfileMgr.GetUserProfile(pinfo.LoginName);

                    List<UserProfile> directReports = new List<UserProfile>(cUserProfile.GetDirectReports());
                    foreach (UserProfile up in directReports)
                    {
                        DataRow row = tblEmps.NewRow();

                        if (up.GetProfileValueCollection("AboutMe")[0] != null && up.GetProfileValueCollection("AboutMe")[0].ToString() != string.Empty)
                        {
                            row["EmpName"] = up.GetProfileValueCollection("AboutMe")[0].ToString();
                        }
                        else
                        {
                            row["EmpName"] = up.DisplayName;
                        }

                        row["EnglishName"] = up.DisplayName;


                        row["EmpJob"] = up.GetProfileValueCollection("Title")[0].ToString();
                        tblEmps.Rows.Add(row);
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        protected void Bind_Data_To_Controls()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwSelectEmp.DataSource = tblEmps;
                gvwSelectEmp.DataBind();
            });
        }

        protected void gvwSelectEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvwSelectEmp.SelectedRow;
                Response.Redirect(SPContext.Current.Web.Url + "/Pages/RateObjectivesEmp.aspx?empid=" + row.Cells[1].Text);
            }
            catch (Exception)
            {
            }
        }
    }
}
