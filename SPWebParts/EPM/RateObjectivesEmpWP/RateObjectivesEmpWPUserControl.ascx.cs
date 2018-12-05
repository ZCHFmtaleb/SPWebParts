using Microsoft.SharePoint;
using SPWebParts.EPM.DAL;
using SPWebParts.EPM.EL;
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

                     intended_Emp = Emp_DAL.get_Emp_Info(intended_Emp, strEmpDisplayName);
                     bind_Emp_Info();

                     if (!IsPostBack)
                     {
                         getPreviouslySavedObjectives();

                         getStandardSkills();

                         Bind_Data_To_Controls();
                     }
                 });
            }
            catch (Exception)
            {
            }
        }

        private void getStandardSkills()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
            SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                        SPList spList = spWeb.Lists.TryGetList("الكفاءات");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            int rank = 0;
                            bool parse_succeeded = int.TryParse(intended_Emp.Emp_Rank, out rank);
                            if (parse_succeeded)
                            {
                                if (rank <= 2)
                                {
                                    qry.Query =
                            @"   <Where>
                                      <Eq>
                                         <FieldRef Name='_x0627__x0644__x062f__x0631__x06' />
                                         <Value Type='Choice'>2 وما فوق</Value>
                                      </Eq>
                                   </Where>";
                                }
                                else if (rank >= 3)
                                {
                                    qry.Query =
                            @"   <Where>
                                      <Eq>
                                         <FieldRef Name='_x0627__x0644__x062f__x0631__x06' />
                                         <Value Type='Choice'>3 وما دون</Value>
                                      </Eq>
                                   </Where>";
                                }
                            }
                            else
                            {
                                gvw_Std_Skills.Visible = false;
                                lbl_invalid_rank.Visible = true;
                            }

                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Title' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tbl_Std_Skills = listItems.GetDataTable();
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

        private void bind_Emp_Info()
        {
            if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
            {
                lblEmpName.Text = intended_Emp.Emp_ArabicName;
            }
            else
            {
                lblEmpName.Text = intended_Emp.Emp_DisplayName;
            }

            lblEmpDept.Text = intended_Emp.Emp_Department;
            lblEmpJob.Text = intended_Emp.Emp_JobTitle;
            lblEmpRank.Text = intended_Emp.Emp_Rank;
            lblEmpDM.Text = intended_Emp.Emp_DM_name;
        }

        private void getPreviouslySavedObjectives()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                  {
                      SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                          SPWeb spWeb = oSite.OpenWeb();
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
                  });
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void Bind_Data_To_Controls()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                   {
                       gvwRate.DataSource = tblObjectives;
                       gvw_Std_Skills.DataSource = tbl_Std_Skills;
                       //gvw_Lead_Skills.DataSource = tbl_Lead_Skills;
                       gvwRate.DataBind();
                       gvw_Std_Skills.DataBind();
                       //gvw_Lead_Skills.DataBind();
                   });
            }
            catch (Exception)
            {

                throw;
            }
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