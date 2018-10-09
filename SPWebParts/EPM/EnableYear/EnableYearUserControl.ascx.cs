using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SPWebParts.EPM.EnableYear
{
    public partial class EnableYearUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Fill_Year();
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("year"));
                dt.Columns.Add(new DataColumn("status"));

                DataRow dr1 = dt.NewRow();
                dr1["year"] = "2017";
                dr1["status"] = "مغلق";
                dt.Rows.Add(dr1);

                DataRow dr2 = dt.NewRow();
                dr2["year"] = "2018";
                dr2["status"] = "التقييم السنوى";
                dt.Rows.Add(dr2);

                DataRow dr3 = dt.NewRow();
                dr3["year"] = "2019";
                dr3["status"] = "وضع الأهداف";
                dt.Rows.Add(dr3);

                DataRow dr4 = dt.NewRow();
                dr4["year"] = "2020";
                dr4["status"] = "لم يتم البدء";
                dt.Rows.Add(dr4);

                gvw_EPM_Years.DataSource = dt;
                gvw_EPM_Years.DataBind();
            }
        }

        private void Fill_Year()
        {
            ListItem[] LC = new ListItem[4];
            int Prev_Year  = DateTime.Now.Year-1;
            int Curr_Year = DateTime.Now.Year;

            LC[0] = new ListItem(Prev_Year.ToString());
            LC[1] = new ListItem(Curr_Year.ToString());
            LC[2] = new ListItem((Curr_Year+1).ToString());
            LC[3] = new ListItem((Curr_Year+2).ToString());

            ddlYear.Items.AddRange(LC);
            ddlYear.Items[1].Selected = true;
            ddlYear_Set_Goals.Items.AddRange(LC);
            ddlYear_Set_Goals.Items[1].Selected = true;

        }
    }
}
