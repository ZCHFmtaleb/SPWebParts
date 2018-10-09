using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPWebParts.EPM.SelectEmp
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
                    tblEmps.Columns.Add("EmpJob");
                    tblEmps.Columns.Add("EmpEvalStatus");
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
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {

                        SPPrincipalInfo pinfo = SPUtility.ResolvePrincipal(spWeb, strEmpDisplayName, SPPrincipalType.User, SPPrincipalSource.All, null, false);
                        //SPUser emp = web.Users[pinfo.LoginName];

                        //lblEmpName.Text = pinfo.DisplayName;
                        //intended_Emp.Emp_DisplayName = pinfo.DisplayName;
                        //lblEmpName.Text = emp.Name;

                        //intended_Emp.Emp_email = pinfo.Email;

                        //lblEmpJob.Text = pinfo.JobTitle;
                        //intended_Emp.Emp_JobTitle = pinfo.JobTitle;
                        //lblEmpJob.Text = cUserProfile.GetProfileValueCollection("Title")[0].ToString();

                        //lblEmpDept.Text = pinfo.Department;
                        //intended_Emp.Emp_Department = pinfo.Department;
                        //lblEmpDept.Text = cUserProfile.GetProfileValueCollection("Department")[0].ToString();

                        SPServiceContext serviceContext = SPServiceContext.GetContext(oSite);
                        UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                        UserProfile cUserProfile = userProfileMgr.GetUserProfile(pinfo.LoginName);
                        //UserProfile DM_UserProfile = userProfileMgr.GetUserProfile(cUserProfile.GetProfileValueCollection("Manager")[0].ToString());
                        //intended_Emp.Emp_DM_email = DM_UserProfile["WorkEmail"].ToString(); ;
                        //lblEmpDM.Text = DM_UserProfile.DisplayName;
                        //intended_Emp.Emp_DM_name = DM_UserProfile.DisplayName;


                        List<UserProfile> directReports = new List<UserProfile>(cUserProfile.GetDirectReports());

                        foreach (UserProfile up in directReports)
                        {
                            DataRow row = tblEmps.NewRow();
                            row["EmpName"] = up.DisplayName;
                            row["EmpJob"]=up.GetProfileValueCollection("Title")[0].ToString();
                            row["EmpEvalStatus"] = "لم يتم التقييم";
                            tblEmps.Rows.Add(row);
                        }

                        //================================================================================================================

                        //SPList spList = spWeb.Lists.TryGetList("الأهداف");
                        //if (spList != null)
                        //{
                        //    SPQuery qry = new SPQuery();
                        //    qry.Query =
                        //    @"   <Where>
                        //                <Eq>
                        //                    <FieldRef Name='Emp' />
                        //                    <Value Type='User'>" + strEmpDisplayName + @"</Value>
                        //                </Eq>
                        //            </Where>";
                        //    qry.ViewFieldsOnly = true;
                        //    qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' /><FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' />
                        //                                    <FieldRef Name='ObjWeight' /><FieldRef Name='StrDir' /><FieldRef Name='StrDir_x003a_Title' />";
                        //    SPListItemCollection listItems = spList.GetItems(qry);
                        //    tblEmps = listItems.GetDataTable();
                        //}
                    }
                }
            });
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
                Response.Redirect(SPContext.Current.Web.Url + "/Pages/نموذج%20تقييم%20الأهداف%20الفردية%20والكفاءات.aspx?empid=" + row.Cells[0].Text);
            }
            catch (Exception)
            {
            }
        }
    }
}