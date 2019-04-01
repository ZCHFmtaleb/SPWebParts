using EPM.Controllers;
using EPM.DAL;
using EPM.EL;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;

namespace EPM.UI.ApproveObjectives
{
    public partial class ApproveObjectivesUserControl : UserControl
    {
        #region Properties

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
                    tblObjectives.Columns.Add("ObjName");
                    tblObjectives.Columns.Add("ObjWeight", typeof(Int32));
                    tblObjectives.Columns.Add("ObjQ");
                    tblObjectives.Columns.Add("_x0645__x0639__x0631__x0641__x00");
                    tblObjectives.Columns.Add("StrDir");
                    tblObjectives.Columns.Add("PrimaryGoal_x003a__x0627__x0633_");
                    tblObjectives.Columns.Add("PrimaryGoal");
                    tblObjectives.Columns.Add("Status");
                    tblObjectives.Columns.Add("ObjYear");
                    return tblObjectives;
                }
            }
            set
            {
                ViewState["tblObjectives"] = value;
            }
        }

        #endregion Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (!IsPostBack)
                {
                    Active_Set_Goals_Year = SetObjectives_DAL.get_Active_Set_Goals_Year();

                    #region Identify to-be-evaluated-user, Get his informatiion , and Bind it

                    strEmpDisplayName = getEmp_from_QueryString_or_currentUser();

                    intended_Emp = Emp_DAL.get_Emp_Info(intended_Emp, strEmpDisplayName);
                    bind_Emp_Info();

                    #endregion Identify to-be-evaluated-user, Get his informatiion , and Bind it

                    tblObjectives = SetObjectives_DAL.getPreviouslySavedObjectives(strEmpDisplayName, Active_Set_Goals_Year).GetDataTable();

                    #region Check goals status and make ReadOnly mode

                    if (tblObjectives.Rows.Count > 0)
                    {
                        string currunt_status = tblObjectives.Rows[0]["Status"].ToString();
                        string currunt_user_email = SPContext.Current.Web.CurrentUser.Email;
                        if (currunt_status == "Objectives_set_by_Emp" && currunt_user_email == intended_Emp.DM_email)
                        {
                            Show_Approve_Reject_Controls();
                        }
                        else if (currunt_status == "Objectives_approved_by_DM" && currunt_user_email == intended_Emp.Dept_Head_email)
                        {
                            Show_Approve_Reject_Controls();
                        }
                        else
                        {
                            Make_Read_Only_Mode();
                        }
                    }

                    #endregion Check goals status and make ReadOnly mode

                    Refresh_Objectives_grid();
                }
            });
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string currunt_user_email = SPContext.Current.Web.CurrentUser.Email;
                if (currunt_user_email == intended_Emp.DM_email)
                {
                    ApproveObjectives_DAL.Update_Status_To_Apporoved_by_DM(intended_Emp.Emp_DisplayName, Active_Set_Goals_Year);
                    Emailer.Send_Objs_Approved_Email_to_Dept_Head(intended_Emp, Active_Set_Goals_Year);
                }
                else if (currunt_user_email == intended_Emp.Dept_Head_email)
                {
                    ApproveObjectives_DAL.Update_Status_To_Apporoved_by_Dept_Head(intended_Emp.Emp_DisplayName, Active_Set_Goals_Year);
                    Emailer.Notify_Emp_that_Objs_finally_approved(intended_Emp, Active_Set_Goals_Year);
                }

                Show_Success_Message("تم اعتماد الأهداف بنجاح");
            });
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string currunt_user_email = SPContext.Current.Web.CurrentUser.Email;
                if (currunt_user_email == intended_Emp.DM_email)
                {
                    ApproveObjectives_DAL.Update_Status_To_Rejected_by_DM(intended_Emp.Emp_DisplayName, Active_Set_Goals_Year);
                }
                else if (currunt_user_email == intended_Emp.Dept_Head_email)
                {
                    ApproveObjectives_DAL.Update_Status_To_Rejected_by_Dept_Head(intended_Emp.Emp_DisplayName, Active_Set_Goals_Year);
                }

                Emailer.Send_Rej_Email_to_Emp(intended_Emp);
                Show_Success_Message("تم طلب التعديلات بنجاح");
            });
        }

        #region Helpers

        private string getEmp_from_QueryString_or_currentUser()
        {
            string name = string.Empty;

            if (Request.QueryString["empid"] != null)
            {
                name = Request.QueryString["empid"].ToString();
            }
            else
            {
                name = SPContext.Current.Web.CurrentUser.Name;
            }

            return name;
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

        private void Show_Approve_Reject_Controls()
        {
            div_Mods.Visible = true;
        }

        public void Make_Read_Only_Mode()
        {
            div_Mods.Visible = false;
        }

        protected void Refresh_Objectives_grid()
        {
            gvwSetObjectives.DataSource = tblObjectives;
            gvwSetObjectives.DataBind();
        }

        private void Show_Success_Message(string m)
        {
            divSuccess.Visible = true;
            lblSuccess.Text = m;
        }

        #endregion Helpers
    }
}