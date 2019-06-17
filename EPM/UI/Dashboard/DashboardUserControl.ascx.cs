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
                    Active_Set_Goals_Year = EnableYear_DAL.get_Active_Set_Goals_Year();
                    tbl_Emps_App_Status = Dashboard_DAL.get_Dashboard_DT(Active_Set_Goals_Year);
                    if (Active_Set_Goals_Year != "NoSetGoalsActiveYear")
                    {
                        lblActiveYear.Text = "متابعة وضع الأهداف والتقييم لسنة " + Active_Set_Goals_Year;
                        Bind_Data_To_Grid();
                    }
                    else
                    {
                        //div_For_Hiding_Mode.InnerHtml = "عذرا لايمكن عرض هذه الصفحة نظرا لإنتهاء فترة وضع الأهداف وعدم وجود عام مفعل حاليا";
                        lblActiveYear.Text = "متابعة وضع الأهداف والتقييم لسنة " + DateTime.Now.Year.ToString();
                        Bind_Data_To_Grid();
                    }
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