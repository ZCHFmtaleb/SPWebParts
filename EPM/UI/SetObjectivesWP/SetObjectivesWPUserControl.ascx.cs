using EPM.Controllers;
using EPM.DAL;
using EPM.EL;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPM.UI.SetObjectivesWP
{
    public partial class SetObjectivesWPUserControl : UserControl
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

                    #region Check if setting goals period is closed, and if true, make ReadOnly mode

                    if (Active_Set_Goals_Year == "NoSetGoalsActiveYear")
                    {
                        Active_Set_Goals_Year = SetObjectives_DAL.get_Year_to_display_if_none_active();
                        Make_Read_Only_Mode();
                    }

                    #endregion Check if setting goals period is closed, and if true, make ReadOnly mode

                    #region Bind Objectives Year

                    lblActiveYear.Text = Active_Set_Goals_Year;

                    #endregion Bind Objectives Year

                    #region Identify Current User , get his informatiion , and Bind it

                    strEmpDisplayName = getEmp_from_QueryString_or_currentUser();

                    intended_Emp = Emp_DAL.get_Emp_Info(intended_Emp, strEmpDisplayName);
                    bind_Emp_Info();

                    #endregion Identify Current User , get his informatiion , and Bind it

                    Bind_DDLs();

                    tblObjectives = SetObjectives_DAL.getPreviouslySavedObjectives(strEmpDisplayName, Active_Set_Goals_Year).GetDataTable();

                    #region Check if goals status is not "rejected" , make ReadOnly mode

                    if (tblObjectives.Rows.Count>0)
                    {
                        string currunt_status = tblObjectives.Rows[0]["Status"].ToString();
                        if (currunt_status != "Objectives_rejected_by_DM" && currunt_status != "Objectives_rejected_by_Dept_Head")
                        {
                            Make_Read_Only_Mode();
                        }
                    }

                    #endregion

                    Refresh_Objectives_grid();
                }
            });
        }

        protected void ddlStrDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPrimaryGoal.Items.Clear();
            ddlPrimaryGoal.Items.Add(new ListItem("اختر الهدف الرئيسى", "0"));
            string str_dir = ddlStrDir.SelectedValue.ToString();
            ddlPrimaryGoal.DataSource = SetObjectives_DAL.get_PrimaryGoals(str_dir).GetDataTable();
            ddlPrimaryGoal.DataBind();
        }

        protected void btnAddObjective_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    DataRow NewRow = tblObjectives.NewRow();
                    NewRow["ObjName"] = txtObjName.Text; ;
                    NewRow["ObjWeight"] = txtObjWeight.Text;
                    NewRow["ObjQ"] = ddlObjQ.SelectedItem.Text;
                    NewRow["_x0645__x0639__x0631__x0641__x00"] = ddlStrDir.SelectedItem.Text;
                    NewRow["StrDir"] = ddlStrDir.SelectedItem.Value;
                    NewRow["PrimaryGoal_x003a__x0627__x0633_"] = ddlPrimaryGoal.SelectedItem.Text;
                    NewRow["PrimaryGoal"] = ddlPrimaryGoal.SelectedItem.Value;
                    NewRow["ObjYear"] = Active_Set_Goals_Year.ToString();
                    tblObjectives.Rows.Add(NewRow);
                    Refresh_Objectives_grid();
                });
            }
            catch (Exception)
            {
            }
        }

        #region Objectives_grid

        protected void gvwSetObjectives_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    gvwSetObjectives.EditIndex = e.NewEditIndex;
                    Refresh_Objectives_grid();
                    GridViewRow row = (GridViewRow)gvwSetObjectives.Rows[e.NewEditIndex];
                    ((DropDownList)row.Cells[2].FindControl("ddlObjQ_gv")).SelectedValue = tblObjectives.Rows[e.NewEditIndex][2].ToString();
                });
            }
            catch (Exception)
            {
            }
        }

        protected void gvwSetObjectives_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    gvwSetObjectives.EditIndex = -1;
                    Refresh_Objectives_grid();
                });
            }
            catch (Exception)
            {
            }
        }

        protected void gvwSetObjectives_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    GridViewRow row = (GridViewRow)gvwSetObjectives.Rows[e.RowIndex];
                    tblObjectives.Rows[e.RowIndex]["ObjName"] = e.NewValues[0].ToString();
                    string newObjWeight = e.NewValues[1].ToString().Replace("%", "");
                    tblObjectives.Rows[e.RowIndex]["ObjWeight"] = int.Parse(newObjWeight);
                    tblObjectives.Rows[e.RowIndex]["ObjQ"] = ((DropDownList)row.Cells[3].FindControl("ddlObjQ_gv")).SelectedValue;
                    gvwSetObjectives.EditIndex = -1;
                    Refresh_Objectives_grid();
                });
            }
            catch (Exception)
            {
            }
        }

        protected void gvwSetObjectives_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    tblObjectives.Rows.RemoveAt(e.RowIndex);
                    Refresh_Objectives_grid();
                });
            }
            catch (Exception)
            {
            }
        }

        #endregion Objectives_grid

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    if (Page.IsValid)
                    {
                        SetObjectives_DAL.SaveToSP(strEmpDisplayName, Active_Set_Goals_Year, tblObjectives, intended_Emp.login_name_to_convert_to_SPUser);
                        Show_Success_Message("تم حفظ الأهداف بنجاح");
                        Emailer.Send_Objs_Added_Email_to_DM(intended_Emp,Active_Set_Goals_Year);
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        #region Helpers

        public void Make_Read_Only_Mode()
        {
            div_of_AddingGoal.Visible = false;
            btnSubmit.Visible = false;
            foreach (DataControlField d in gvwSetObjectives.Columns)
            {
                if (d.HeaderText == "تعديل" || d.HeaderText == "حذف")
                {
                    d.Visible = false;
                }
            }
        }

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

        protected void Bind_DDLs()
        {
            ddlStrDir.DataSource = SetObjectives_DAL.get_StrategicDirections().GetDataTable();
            ddlStrDir.DataBind();
        }

        protected void Refresh_Objectives_grid()
        {
            
            gvwSetObjectives.DataSource = tblObjectives;
            gvwSetObjectives.DataBind();
            Refresh_GoalsTotal();
        }

        private void Refresh_GoalsTotal()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (tblObjectives != null && tblObjectives.Rows.Count > 0)
                {
                    lbl_PercentageTotal.Text = tblObjectives.Compute("Sum(ObjWeight)", string.Empty).ToString();
                    if (lbl_PercentageTotal.Text == "100")
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
            });
        }

        protected void cvld_Number_of_Objs_ServerValidate(object source, ServerValidateEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (gvwSetObjectives.Rows.Count >= 3 && gvwSetObjectives.Rows.Count <= 7)
                    e.IsValid = true;
                else
                    e.IsValid = false;
            });
        }

        protected void cvld_PercentageTotal_ServerValidate(object source, ServerValidateEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (lbl_PercentageTotal.Text == "100")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            });
        }

        private void Show_Success_Message(string m)
        {
            divSuccess.Visible = true;
            lblSuccess.Text = m;
        }

        #endregion Helpers
    }
}