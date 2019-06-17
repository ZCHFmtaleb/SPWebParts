using EPM.Controllers;
using EPM.DAL;
using EPM.EL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPM.UI.RateObjectivesEmpWP
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

        public DataTable tbl_Rated_Skills
        {
            get
            {
                if (ViewState["tbl_Rated_Skills"] != null)
                {
                    return (DataTable)ViewState["tbl_Rated_Skills"];
                }
                else
                {
                    tbl_Rated_Skills = new DataTable();
                    tbl_Rated_Skills.Columns.Add("Title");
                    tbl_Rated_Skills.Columns.Add("Rating");
                    return tbl_Rated_Skills;
                }
            }
            set
            {
                ViewState["tbl_Rated_Skills"] = value;
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
                    tbl_Std_Skills = new DataTable();
                    tbl_Std_Skills.Columns.Add("Title");
                    tbl_Std_Skills.Columns.Add("Rating");
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

        public bool ReadOnly_Mode
        {
            get
            {
                if (ViewState["ReadOnly_Mode"] != null)
                {
                    return (bool)ViewState["ReadOnly_Mode"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["ReadOnly_Mode"] = value;
            }
        }

        #endregion Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                divSuccess.Visible = false;

                #region Check for year to use

                Active_Rate_Goals_Year = EnableYear_DAL.read_Active_Rate_Goals_Year();
                if (Active_Rate_Goals_Year == "NoRateGoalsActiveYear")
                {
                    Active_Rate_Goals_Year = read_Year_to_display_if_none_active();
                }
                lblActiveYear.Text = Active_Rate_Goals_Year;

                #endregion Check for year to use

                #region Check for Emp to use (QueryString or current logged-in user)

                if (Request.QueryString["empid"] != null)
                {
                    strEmpDisplayName = Request.QueryString["empid"].ToString();
                }
                else
                {
                    strEmpDisplayName = SPContext.Current.Web.CurrentUser.Name;          //"Test spuser_1"; //"sherif abdellatif";
                }

                intended_Emp = Emp_DAL.get_Emp_Info(strEmpDisplayName);
                bind_Emp_Info();

                #endregion Check for Emp to use (QueryString or current logged-in user)

                if (!IsPostBack)
                {
                    ReadOnly_Mode = false;

                    #region Check current WorkFlow Status And current logged-in user To decide if ReadOnly_Mode

                    tblObjectives = SetObjectives_DAL.getPreviouslySavedObjectives(strEmpDisplayName, Active_Rate_Goals_Year).GetDataTable();
                    string st = tblObjectives.Rows[0]["Status"].ToString().Trim().ToLower();
                    string p1 = WF_States.Objectives_ProgressSet_by_Emp.ToString().Trim().ToLower();
                    string p2 = WF_States.ObjsAndSkills_Rated.ToString().Trim().ToLower();

                    if (st == p1 || st == p2)
                    {
                        lblProgressNotSet_Warning.Visible = false;
                    }
                    else
                    {
                        lblProgressNotSet_Warning.Visible = true;
                    }

                    if (st == p2)
                    {
                        ReadOnly_Mode = true;
                    }
                    else if (strEmpDisplayName == SPContext.Current.Web.CurrentUser.Name)
                    {
                        ReadOnly_Mode = true;
                    }

                    #endregion Check current WorkFlow Status And current logged-in user To decide if ReadOnly_Mode

                    #region Data Binding To UI Controls

                    txtNote1.Text = SetObjectives_DAL.getPreviouslySavedNote1(strEmpDisplayName, Active_Rate_Goals_Year);
                    EvalNotes notes = SetObjectives_DAL.getPreviouslySavedEvalNotes(strEmpDisplayName, Active_Rate_Goals_Year);
                    txtNote_ReasonForRating1or5.Text = notes.ReasonForRating1or5;
                    txtNote_RecommendedCourses.Text = notes.RecommendedCourses;

                    if (!Check_If_Emp_and_Year_saved_before())
                    {
                        getStandardSkills();
                    }

                    Bind_Data_To_Controls();

                    #endregion Data Binding To UI Controls
                }

                if (ReadOnly_Mode == true)
                {
                    Make_ReadOnly_Mode();
                }
            });
        }

        private void Make_ReadOnly_Mode()
        {
            foreach (GridViewRow row in gvwRate.Rows)
            {
                DropDownList ddlObjRating = row.FindControl("ddlObjRating") as DropDownList;
                ddlObjRating.Enabled = false;
            }
            foreach (GridViewRow row in gvw_Std_Skills.Rows)
            {
                DropDownList ddl_Std_Skill_Rating = row.FindControl("ddl_Std_Skill_Rating") as DropDownList;
                ddl_Std_Skill_Rating.Enabled = false;
            }

            txtNote1.ReadOnly = true;
            txtNote_ReasonForRating1or5.ReadOnly = true;
            txtNote_RecommendedCourses.ReadOnly = true;
            btnSubmit.Visible = false;
        }

        private void getStandardSkills()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "List4"));   //"الكفاءات"
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

        private bool Check_If_Emp_and_Year_saved_before()
        {
            bool result = false;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "SkillsRating"));  //SPList oList = oWeb.GetList("/Lists/SkillsRating");

                #region Get any previous Ratings of same Emp and Year

                SPQuery qry = new SPQuery();
                qry.Query =
                @"   <Where>
                                  <And>
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
                                     <Eq>
                                        <FieldRef Name='deleted' />
                                        <Value Type='Number'>0</Value>
                                     </Eq>
                                  </And>
                               </Where>";
                qry.ViewFieldsOnly = true;
                qry.ViewFields = @"<FieldRef Name='Title' /><FieldRef Name='Rating' />";
                SPListItemCollection listItems = spList.GetItems(qry);

                if (listItems.Count > 0)
                {
                    tbl_Rated_Skills = listItems.GetDataTable();
                    result = true;
                }

                #endregion Get any previous Ratings of same Emp and Year
            });

            return result;
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

        private string read_Year_to_display_if_none_active()
        {
            string pActiveYear = ((DateTime.Today.Year) - 1).ToString();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "EPMYear")); //SPList spList = spWeb.GetList("/Lists/EPMYear");
                if (spList != null)
                {
                    SPQuery qry = new SPQuery();
                    qry.Query =
                                  @"   <Where>
                                              <Eq>
                                                 <FieldRef Name='State' />
                                                 <Value Type='Choice'>عرض فقط</Value>
                                              </Eq>
                                           </Where>";
                    qry.ViewFieldsOnly = true;
                    qry.ViewFields = @"<FieldRef Name='Title' /><FieldRef Name='Year' /><FieldRef Name='State' />";
                    SPListItemCollection listItems = spList.GetItems(qry);

                    if (listItems.Count > 0)
                    {
                        foreach (SPListItem item in listItems)
                        {
                            if (item["State"].ToString() == "عرض فقط" && item["Title"].ToString() == "سنة عرض التقييم (فى حالة عدم وجود عام مفعل)")
                            {
                                pActiveYear = item["Year"].ToString();
                            }
                        }
                    }
                }
            });

            return pActiveYear;
        }

        protected void Bind_Data_To_Controls()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    gvwRate.DataSource = tblObjectives;

                    if (tbl_Rated_Skills.Rows.Count > 0)
                    {
                        gvw_Std_Skills.DataSource = tbl_Rated_Skills;
                    }
                    else
                    {
                        gvw_Std_Skills.DataSource = tbl_Std_Skills;
                    }

                    gvwRate.DataBind();
                    gvw_Std_Skills.DataBind();
                });
            }
            catch (Exception)
            {
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (Page.IsValid)
                {
                    SaveToSP();
                    Show_Success_Message("تم حفظ التقييم بنجاح");
                    WFStatusUpdater.Change_State_to(WF_States.ObjsAndSkills_Rated, strEmpDisplayName, Active_Rate_Goals_Year);
                    Emailer.Send_ObjsAndSkills_Rated_Email_to_Emp(intended_Emp, Active_Rate_Goals_Year);
                }
            });
        }

        private void SaveToSP()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                spWeb.AllowUnsafeUpdates = true;
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "SkillsRating"));

                #region Remove any previous Ratings of same Emp and Year, by updating "deleted" to 1

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
                qry.ViewFields = @"<FieldRef Name='ID' />";
                SPListItemCollection listItems = spList.GetItems(qry);

                foreach (SPListItem item in listItems)
                {
                    SPListItem itemToUpdate = spList.GetItemById(item.ID);
                    itemToUpdate["deleted"] = 1;
                    itemToUpdate.Update();
                }

                #endregion Remove any previous Ratings of same Emp and Year, by updating "deleted" to 1

                #region Add the new Ratings

                foreach (GridViewRow row in gvw_Std_Skills.Rows)
                {
                    SPListItem oListItem = spList.AddItem();
                    oListItem["Title"] = row.Cells[0].Text;
                    var dd = row.Cells[1].FindControl("ddl_Std_Skill_Rating") as DropDownList;
                    oListItem["Rating"] = int.Parse(dd.SelectedValue);
                    oListItem["Emp"] = SPContext.Current.Web.EnsureUser(intended_Emp.login_name_to_convert_to_SPUser);
                    oListItem["ObjYear"] = Active_Rate_Goals_Year;
                    oListItem.Update();
                }

                #endregion Add the new Ratings

                spWeb.AllowUnsafeUpdates = false;

                foreach (GridViewRow row in gvwRate.Rows)
                {
                    DropDownList ddlObjRating = row.FindControl("ddlObjRating") as DropDownList;
                    tblObjectives.Rows[row.RowIndex]["AccRating"] = ddlObjRating.SelectedItem.Text;
                }

                SetProgress_DAL.Update_Objectives("AccRating", tblObjectives);

                EvalNotes evalnotes = new EvalNotes();
                evalnotes.ReasonForRating1or5 = txtNote_ReasonForRating1or5.Text;
                evalnotes.RecommendedCourses = txtNote_RecommendedCourses.Text;

                SetProgress_DAL.Save_or_Update_Objs_EvalNotes(intended_Emp.login_name_to_convert_to_SPUser, Active_Rate_Goals_Year, evalnotes);
            });
        }

        protected void gvw_Std_Skills_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dd = e.Row.Cells[1].FindControl("ddl_Std_Skill_Rating") as DropDownList;
                if (null != dd)
                {
                    if (tbl_Rated_Skills.Rows.Count > 0)
                    {
                        dd.SelectedValue = tbl_Rated_Skills.Rows[e.Row.RowIndex]["Rating"].ToString();
                    }
                    else
                    {
                        dd.SelectedValue = "3";
                    }
                }
            }
        }

        private void Show_Success_Message(string m)
        {
            divSuccess.Visible = true;
            lblSuccess.Text = m;
        }
    }
}