using EPM.DAL;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;

namespace EPM.UI.Dashboard
{
    public partial class DashboardUserControl : UserControl
    {
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
                    return tbl_Emps_App_Status;
                }
            }
            set
            {
                ViewState["tbl_Emps_App_Status"] = value;
            }
        }

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
                        Bind_Data_To_Grid();
                    }
                    else
                    {
                        div_For_Hiding_Mode.InnerHtml = "عذرا لايمكن عرض هذه الصفحة نظرا لإنتهاء فترة وضع الأهداف وعدم وجود عام مفعل حاليا";
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
    }
}