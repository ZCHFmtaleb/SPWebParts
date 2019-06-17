using EPM.DAL;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPM.UI.Dashboard
{
    public partial class DashboardUserControl : UserControl
    {
        #region Properties

        public string DashBoard_Year
        {
            get
            {
                if (ViewState["DashBoard_Year"] != null)
                {
                    return ViewState["DashBoard_Year"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["DashBoard_Year"] = value;
            }
        }

        public DataTable tbl_Emps_App_Status
        {
            get
            {
                if (ViewState["tbl_Emps_App_Status"] != null)
                {
                    return (DataTable)ViewState["tbl_Emps_App_Status"];
                }
                else
                {
                    tbl_Emps_App_Status = new DataTable();
                    tbl_Emps_App_Status.Columns.Add("EnglishName");
                    tbl_Emps_App_Status.Columns.Add("Status");
                    tbl_Emps_App_Status.Columns.Add("Email");
                    tbl_Emps_App_Status.Columns.Add("ArabicName");
                    tbl_Emps_App_Status.Columns.Add("Department");
                    tbl_Emps_App_Status.Columns.Add("EmpHierLvl");
                    return tbl_Emps_App_Status;
                }
            }
            set
            {
                ViewState["tbl_Emps_App_Status"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (!IsPostBack)
                {
                    DashBoard_Year = EnableYear_DAL.get_Active_Set_Goals_Year();
                    if (DashBoard_Year == "NoSetGoalsActiveYear")
                    {
                        DashBoard_Year = DateTime.Now.Year.ToString();
                    }
                    lblActiveYear.Text = "متابعة وضع الأهداف والتقييم لسنة " + DashBoard_Year;
                    tbl_Emps_App_Status = Dashboard_DAL.get_Dashboard_DT(DashBoard_Year);
                    Bind_Data_To_Grid();
                }
            });
        }

        protected void gvw_Dashboard_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvw_Dashboard.PageIndex = e.NewPageIndex;
            Bind_Data_To_Grid();
        }

        private void Bind_Data_To_Grid()
        {
            tbl_Emps_App_Status.DefaultView.Sort = "Status Asc";
            gvw_Dashboard.DataSource = tbl_Emps_App_Status;
            gvw_Dashboard.DataBind();
        }

        protected void gvw_Dashboard_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            int EmpHierLvl = 0;
            string Status = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrWhiteSpace(e.Row.Cells[5].Text) && e.Row.Cells[5].Text != "&nbsp;")
                {
                    EmpHierLvl = int.Parse(e.Row.Cells[5].Text);

                    Status = e.Row.Cells[4].Text;

                    if (EmpHierLvl == 1)
                    {
                        if (Status == "Objectives_set_by_Emp" || Status == "Objectives_approved_by_DM")
                        {
                            e.Row.BackColor = Color.Cornsilk;
                        }
                        else if (Status == "Objectives_approved_by_Dept_Head")
                        {
                            e.Row.BackColor = Color.GreenYellow;
                        }
                    }
                    else if (EmpHierLvl == 2 || EmpHierLvl == 3)
                    {
                        if (Status == "Objectives_set_by_Emp")
                        {
                            e.Row.BackColor = Color.Cornsilk;
                        }
                        else if (Status == "Objectives_approved_by_DM")
                        {
                            e.Row.BackColor = Color.GreenYellow;
                        }
                    }
                }
            }
        }
    }
}