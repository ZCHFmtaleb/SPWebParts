using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.EL;
using EPM.DAL;

namespace EPM.UI.SetProgress
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
                    tblObjectives.Columns.Add("AccPercent", typeof(string));
                    return tblObjectives;
                }
            }
            set
            {
                ViewState["tblObjectives"] = value;
            }
        }

        public string Active_Rate_Goals_Year
        {
            get
            {
                if (ViewState["Active_Rate_Goals_Year"] != null)
                {
                    return ViewState["Active_Rate_Goals_Year"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["Active_Rate_Goals_Year"] = value;
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

                    intended_Emp = Emp_DAL.get_Emp_Info(strEmpDisplayName);
                    bind_Emp_Info();

                    if (!IsPostBack)
                    {
                        Active_Rate_Goals_Year =EnableYear_DAL. read_Active_Rate_Goals_Year();
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
                        Show_Success_Message("تم حفظ الأهداف بنجاح");
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
                SPList oList = oWeb.GetList(oWeb.ServerRelativeUrl + "/Lists/Objectives");

                #region Add the new (or updated) objectives

                if (tblObjectives != null)
                {
                    foreach (DataRow row in tblObjectives.Rows)
                    {
                        SPListItem oListItem = oList.GetItemById(int.Parse(row["ID"].ToString()));

                        oListItem["AccPercent"] = row["AccPercent"].ToString();
                        oListItem.Update();
                    }
                    divSuccess.Visible = true;
                }
                else
                {
                }
                #endregion Add the new (or updated) objectives

                oWeb.AllowUnsafeUpdates = false;

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
            lblEmpDM.Text = intended_Emp.DM_name;
        }

        private void getPreviouslySavedObjectives()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(spWeb.ServerRelativeUrl + "/Lists/Objectives");
                SPQuery qry = new SPQuery();
                qry.Query =
                @"   <Where>
                                          <And>
                                          <Eq>
                                             <FieldRef Name='Emp' />
                                              <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                          </Eq>
                                           <Eq>
                                                <FieldRef Name='ObjYear' />
                                                <Value Type='Text'>" + Active_Rate_Goals_Year + @"</Value>
                                             </Eq>
                                            </And>
                                       </Where>";
                qry.ViewFieldsOnly = true;
                qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjWeight' /><FieldRef Name='AccPercent' />";
                SPListItemCollection listItems = spList.GetItems(qry);
                tblObjectives = listItems.GetDataTable();
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
                tblObjectives.Rows[e.RowIndex][4] = e.NewValues[3].ToString().Replace("%", "");
                gvwProgress.EditIndex = -1;
                Bind_Data_To_Controls();
            });
        }

        private void Show_Success_Message(string m)
        {
            divSuccess.Visible = true;
            lblSuccess.Text = m;
        }
    }
}
