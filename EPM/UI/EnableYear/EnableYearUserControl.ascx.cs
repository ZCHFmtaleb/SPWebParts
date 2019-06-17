using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPM.UI.EnableYear
{
    public partial class EnableYearUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Fill_Years_ddl();
                    Refresh_Active_years_grid();
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnActivate_Eval_Year_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.EnableYear_DAL.Update_Year("البدء بتفعيل التقييم السنوى لسنة", ddl_Eval_Year.SelectedItem.Text, "مفعل");
                Refresh_Active_years_grid();
            }
            catch (Exception)
            {
            }
        }

        protected void btnActivate_Set_Goals_Year_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.EnableYear_DAL.Update_Year("البدء بتفعيل وضع الأهداف لسنة", ddl_Set_Goals_Year.SelectedItem.Text, "مفعل");
                Refresh_Active_years_grid();
            }
            catch (Exception)
            {
            }
        }

        protected void gvw_EPM_Years_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "YearClosure")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvw_EPM_Years.Rows[index];

                    DAL.EnableYear_DAL.Update_Year(row.Cells[0].Text, string.Empty, "مغلق");
                    Refresh_Active_years_grid();
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btn_Year_to_display_if_none_active_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.EnableYear_DAL.Update_Year("سنة عرض الأهداف (فى حالة عدم وجود عام مفعل)", ddl_Year_to_display_if_none_active.SelectedItem.Text, "عرض فقط");
            }
            catch (Exception)
            {
            }
        }

        protected void btn_Rating_to_display_if_none_active_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.EnableYear_DAL.Update_Year("سنة عرض التقييم (فى حالة عدم وجود عام مفعل)", ddl_Rating_to_display_if_none_active.SelectedItem.Text, "عرض فقط");
            }
            catch (Exception)
            {
            }
        }

        #region Helpers

        private void Fill_Years_ddl()
        {
            ListItem[] LC = new ListItem[4];
            int Prev_Year = DateTime.Now.Year - 1;
            int Curr_Year = DateTime.Now.Year;

            LC[0] = new ListItem(Prev_Year.ToString());
            LC[1] = new ListItem(Curr_Year.ToString());
            LC[2] = new ListItem((Curr_Year + 1).ToString());
            LC[3] = new ListItem((Curr_Year + 2).ToString());

            ListItem[] LC_Rating = new ListItem[4];

            LC_Rating[0] = new ListItem(Prev_Year.ToString());
            LC_Rating[1] = new ListItem(Curr_Year.ToString());
            LC_Rating[2] = new ListItem((Curr_Year + 1).ToString());
            LC_Rating[3] = new ListItem((Curr_Year + 2).ToString());

            ddl_Eval_Year.Items.AddRange(LC);
            ddl_Eval_Year.Items[1].Selected = true;
            ddl_Set_Goals_Year.Items.AddRange(LC);
            ddl_Set_Goals_Year.Items[1].Selected = true;
            ddl_Year_to_display_if_none_active.Items.AddRange(LC);
            ddl_Year_to_display_if_none_active.Items[1].Selected = true;
            ddl_Rating_to_display_if_none_active.Items.AddRange(LC_Rating);
            ddl_Rating_to_display_if_none_active.Items[1].Selected = true;
        }

        private void Refresh_Active_years_grid()
        {
            SPListItemCollection ActiveYears = DAL.EnableYear_DAL.get_Active_Years();
            Fill_Active_years_grid(ActiveYears);
        }

        protected void Fill_Active_years_grid(SPListItemCollection ActiveYears)
        {
            btnActivate_Eval_Year.Enabled = true;
            btnActivate_Set_Goals_Year.Enabled = true;

            foreach (SPListItem item in ActiveYears)
            {
                if (item["State"].ToString() == "مفعل" && item["Title"].ToString() == "البدء بتفعيل التقييم السنوى لسنة")
                {
                    btnActivate_Eval_Year.Enabled = false;
                }
                else if (item["State"].ToString() == "مفعل" && item["Title"].ToString() == "البدء بتفعيل وضع الأهداف لسنة")
                {
                    btnActivate_Set_Goals_Year.Enabled = false;
                }
            }

            gvw_EPM_Years.DataSource = ActiveYears.GetDataTable();
            gvw_EPM_Years.DataBind();
        }

        #endregion Helpers
    }
}