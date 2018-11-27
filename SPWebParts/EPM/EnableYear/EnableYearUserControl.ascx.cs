using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPWebParts.EPM.EnableYear
{
    public partial class EnableYearUserControl : UserControl
    {
        public DataTable tblActiveYears
        {
            get
            {
                if (ViewState["tblActiveYears"] != null)
                {
                    return (DataTable)ViewState["tblActiveYears"];
                }
                else
                {
                    tblActiveYears = new DataTable();
                    tblActiveYears.Columns.Add(new DataColumn("Title"));
                    tblActiveYears.Columns.Add(new DataColumn("Year"));
                    tblActiveYears.Columns.Add(new DataColumn("State"));
                    return tblActiveYears;
                }
            }
            set
            {
                ViewState["tblActiveYears"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Fill_Year();
                get_Active_Years();
            }
        }

        private void Fill_Year()
        {
            ListItem[] LC = new ListItem[4];
            int Prev_Year = DateTime.Now.Year - 1;
            int Curr_Year = DateTime.Now.Year;

            LC[0] = new ListItem(Prev_Year.ToString());
            LC[1] = new ListItem(Curr_Year.ToString());
            LC[2] = new ListItem((Curr_Year + 1).ToString());
            LC[3] = new ListItem((Curr_Year + 2).ToString());

            ddl_Eval_Year.Items.AddRange(LC);
            ddl_Eval_Year.Items[1].Selected = true;
            ddl_Set_Goals_Year.Items.AddRange(LC);
            ddl_Set_Goals_Year.Items[1].Selected = true;
            ddl_Year_to_display_if_none_active.Items.AddRange(LC);
            ddl_Year_to_display_if_none_active.Items[1].Selected = true;
        }

        private void get_Active_Years()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("سنة التقييم");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =
                                          @"   <Where>
                                              <Eq>
                                                 <FieldRef Name='State' />
                                                 <Value Type='Choice'>مفعل</Value>
                                              </Eq>
                                           </Where>";
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='Title' /><FieldRef Name='Year' /><FieldRef Name='State' />";
                            SPListItemCollection listItems = spList.GetItems(qry);

                            btnActivate_Eval_Year.Visible = true;
                            btnActivate_Set_Goals_Year.Visible = true;

                            foreach (SPListItem item in listItems)
                            {
                                if (item["State"].ToString() == "مفعل" && item["Title"].ToString() == "البدء بتفعيل التقييم السنوى لسنة")
                                {
                                    btnActivate_Eval_Year.Visible = false;
                                }
                                else if (item["State"].ToString() == "مفعل" && item["Title"].ToString() == "البدء بتفعيل وضع الأهداف لسنة")
                                {
                                    btnActivate_Set_Goals_Year.Visible = false;
                                }
                            }

                            tblActiveYears = listItems.GetDataTable();
                            gvw_EPM_Years.DataSource = tblActiveYears;
                            gvw_EPM_Years.DataBind();
                        }
                    }
                }
            });
        }

        protected void btnActivate_Eval_Year_Click(object sender, EventArgs e)
        {
            Update_Year("البدء بتفعيل التقييم السنوى لسنة", ddl_Eval_Year.SelectedItem.Text, "مفعل");
            get_Active_Years();
        }

        protected void btnActivate_Set_Goals_Year_Click(object sender, EventArgs e)
        {
            Update_Year("البدء بتفعيل وضع الأهداف لسنة", ddl_Set_Goals_Year.SelectedItem.Text, "مفعل");
            get_Active_Years();
        }

        public void Update_Year(string mode, string year, string new_state)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        oWeb.AllowUnsafeUpdates = true;

                        SPList oList = oWeb.Lists["سنة التقييم"];
                        SPQuery qry = new SPQuery();
                        qry.Query =
                                        @"   <Where>
                                                    <Eq>
                                                        <FieldRef Name='Title' />
                                                        <Value Type='Text'>" + mode + @"</Value>
                                                    </Eq>
                                                </Where>";
                        SPListItemCollection listItems = oList.GetItems(qry);

                        SPListItem itemToUpdate = oList.GetItemById(listItems[0].ID);
                        itemToUpdate["Year"] = year;
                        itemToUpdate["State"] = new_state;
                        itemToUpdate.Update();

                        oWeb.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

        protected void gvw_EPM_Years_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "YearClosure")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvw_EPM_Years.Rows[index];

                Update_Year(row.Cells[0].Text, string.Empty, "مغلق");
                get_Active_Years();
            }
        }

        protected void btn_Year_to_display_if_none_active_Click(object sender, EventArgs e)
        {
            Update_Year("سنة عرض الأهداف (فى حالة عدم وجود عام مفعل)", ddl_Year_to_display_if_none_active.SelectedItem.Text, "عرض فقط");
        }
    }
}