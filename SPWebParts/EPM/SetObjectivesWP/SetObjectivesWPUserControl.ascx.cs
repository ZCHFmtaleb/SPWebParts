using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using SPWebParts.EPM.DAL;
using SPWebParts.EPM.EL;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPWebParts.EPM.SetObjectivesWP
{
    public partial class SetObjectivesWPUserControl : UserControl
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

        public string Planning_Consultant_Email
        {
            get
            {
                if (ViewState["Planning_Consultant_Email"] != null)
                {
                    return ViewState["Planning_Consultant_Email"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["Planning_Consultant_Email"] = value;
            }
        }

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

        public DataTable tblStrDir
        {
            get
            {
                if (ViewState["tblStrDir"] != null)
                {
                    return (DataTable)ViewState["tblStrDir"];
                }
                else
                {
                    tblStrDir = new DataTable();
                    tblStrDir.Columns.Add("ID");
                    tblStrDir.Columns.Add("Title");
                    return tblStrDir;
                }
            }
            set
            {
                ViewState["tblStrDir"] = value;
            }
        }

        public DataTable tblPrimaryGoal
        {
            get
            {
                if (ViewState["tblPrimaryGoal"] != null)
                {
                    return (DataTable)ViewState["tblPrimaryGoal"];
                }
                else
                {
                    tblPrimaryGoal = new DataTable();
                    tblPrimaryGoal.Columns.Add("ID");
                    tblPrimaryGoal.Columns.Add("Title");
                    return tblPrimaryGoal;
                }
            }
            set
            {
                ViewState["tblPrimaryGoal"] = value;
            }
        }

        public DataTable tblSecondaryGoal
        {
            get
            {
                if (ViewState["tblSecondaryGoal"] != null)
                {
                    return (DataTable)ViewState["tblSecondaryGoal"];
                }
                else
                {
                    tblSecondaryGoal = new DataTable();
                    tblSecondaryGoal.Columns.Add("ID");
                    tblSecondaryGoal.Columns.Add("Title");
                    return tblSecondaryGoal;
                }
            }
            set
            {
                ViewState["tblSecondaryGoal"] = value;
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

        public int PercentageTotal
        {
            get
            {
                if (ViewState["PercentageTotal"] != null)
                {
                    return (int)ViewState["PercentageTotal"];
                }
                else
                {
                    PercentageTotal = 0;
                    return PercentageTotal;
                }
            }
            set
            {
                ViewState["PercentageTotal"] = value;
            }
        }

        public WF_States current_state
        {
            get
            {
                if (ViewState["current_state"] != null)
                {
                    return (WF_States)ViewState["current_state"];
                }
                else
                {
                    return WF_States.first_run;
                }
            }
            set
            {
                ViewState["current_state"] = value;
            }
        }

        #endregion Properties

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
               {
                   divSuccess.Visible = false;

                   if (!IsPostBack)
                   {
                       Active_Set_Goals_Year = read_Active_Set_Goals_Year();
                       if (Active_Set_Goals_Year == "NoSetGoalsActiveYear")
                       {
                           Active_Set_Goals_Year = read_Year_to_display_if_none_active();
                           Make_InActive_Mode();
                       }
                   }

                   lblActiveYear.Text = Active_Set_Goals_Year;
                   lblActiveYear2.Text = Active_Set_Goals_Year;

                   getEmp_from_QueryString_or_currentUser();

                   intended_Emp = Emp_DAL.get_Emp_Info(intended_Emp, strEmpDisplayName);
                   bind_Emp_Info();

                   if (!IsPostBack)
                   {
                       fill_ddlStrDir();

                       getPreviouslySavedObjectives();

                       Bind_Data_To_Controls();

                       Planning_Consultant_Email = Emp_DAL.get_Planning_Consultant_Email();

                       Check_Current_WF_State_and_Actor();
                   }
               });
        }

        protected void ddlStrDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPrimaryGoal.Items.Clear();
            ddlPrimaryGoal.Items.Add(new ListItem("اختر الهدف الرئيسى", "0"));
            fill_ddlPrimaryGoal();
            ddlPrimaryGoal.DataSource = tblPrimaryGoal;
            ddlPrimaryGoal.DataValueField = "ID";
            ddlPrimaryGoal.DataTextField = "Title";
            ddlPrimaryGoal.DataBind();
        }

        protected void btnAddObjective_Click(object sender, EventArgs e)
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
                Bind_Data_To_Controls();
            });
        }

        protected void gvwSetObjectives_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwSetObjectives.EditIndex = e.NewEditIndex;
                Bind_Data_To_Controls();
                GridViewRow row = (GridViewRow)gvwSetObjectives.Rows[e.NewEditIndex];
                ((DropDownList)row.Cells[2].FindControl("ddlObjQ_gv")).SelectedValue = tblObjectives.Rows[e.NewEditIndex][2].ToString();
            });
        }

        protected void gvwSetObjectives_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwSetObjectives.EditIndex = -1;
                Bind_Data_To_Controls();
            });
        }

        protected void gvwSetObjectives_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                GridViewRow row = (GridViewRow)gvwSetObjectives.Rows[e.RowIndex];
                tblObjectives.Rows[e.RowIndex]["ObjName"] = e.NewValues[0].ToString();
                string newObjWeight = e.NewValues[1].ToString().Replace("%", "");
                tblObjectives.Rows[e.RowIndex]["ObjWeight"] = int.Parse(newObjWeight);
                tblObjectives.Rows[e.RowIndex]["ObjQ"] = ((DropDownList)row.Cells[3].FindControl("ddlObjQ_gv")).SelectedValue;
                gvwSetObjectives.EditIndex = -1;
                Bind_Data_To_Controls();
            });
        }

        protected void gvwSetObjectives_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                tblObjectives.Rows.RemoveAt(e.RowIndex);
                Bind_Data_To_Controls();
            });
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
             {
                 if (Page.IsValid)
                 {
                     SaveToSP();
                     Change_State_to(WF_States.Objectives_set_by_Emp);
                     Show_Success_Message("تم حفظ الأهداف بنجاح");
                     Send_Objs_Added_Email_to_Planning_Consultant();
                 }
             });
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (Page.IsValid)
                {
                    SaveToSP();
                    Show_Success_Message("تم اعتماد الأهداف بنجاح");
                    if (SPContext.Current.Web.CurrentUser.Email == Planning_Consultant_Email)
                    {
                        Change_State_to(WF_States.Objectives_approved_by_Planning_Consultant);
                        Send_Objs_Added_Email_to_DM();
                    }
                    else
                    {
                        Change_State_to(WF_States.Objectives_approved_by_DM);
                        Notify_Emp_that_Objs_finally_approved();
                    }
                }
            });
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Send_Rej_Email_to_Emp();
            Change_State_to(WF_States.Objectives_rejected_by_Planning_Consultant);
            Show_Success_Message("تم ارسال بريد الكترونى بالتعديلات المطلوبة");
        }

        #endregion Event Handlers

        #region Related to Load

        private void Check_Current_WF_State_and_Actor()
        {
            if (current_state == WF_States.Objectives_set_by_Emp)
            {
                if (SPContext.Current.Web.CurrentUser.Email == Planning_Consultant_Email)
                {
                    btnSubmit.Visible = false;
                    btnApprove.Visible = true;
                    lblRequired_Mods.Visible = true;
                    txtRequired_Mods.Visible = true;
                    btnReject.Visible = true;
                }
                else
                {
                    Make_Read_Only_Mode();
                }
            }
            else if (current_state == WF_States.Objectives_approved_by_Planning_Consultant)
            {
                if (SPContext.Current.Web.CurrentUser.Email == intended_Emp.Emp_DM_email)
                {
                    btnSubmit.Visible = false;
                    btnApprove.Visible = true;
                    lblRequired_Mods.Visible = true;
                    txtRequired_Mods.Visible = true;
                    btnReject.Visible = true;
                }
                else
                {
                    Make_Read_Only_Mode();
                }
            }
            else if (current_state == WF_States.Objectives_rejected_by_Planning_Consultant)
            {
                if (SPContext.Current.Web.CurrentUser.Email == intended_Emp.Emp_email)
                {
                }
                else
                {
                    Make_Read_Only_Mode();
                }
            }
            else if (current_state == WF_States.Objectives_approved_by_DM)
            {
                Make_Read_Only_Mode();
            }
            else if (current_state == WF_States.Objectives_rejected_by_DM)
            {
                if (SPContext.Current.Web.CurrentUser.Email == intended_Emp.Emp_email)
                {
                }
                else
                {
                    Make_Read_Only_Mode();
                }
            }
            else
            {
            }
        }

        private void Make_Read_Only_Mode()
        {
            div_of_AddingGoal.Visible = false;
            lblRequired_Mods.Visible = false;
            txtRequired_Mods.Visible = false;
            btnSubmit.Visible = false;
            btnApprove.Visible = false;

            foreach (DataControlField d in gvwSetObjectives.Columns)
            {
                if (d.HeaderText == "تعديل" || d.HeaderText == "حذف")
                {
                    d.Visible = false;
                }
            }
        }

        private void getEmp_from_QueryString_or_currentUser()
        {
            if (Request.QueryString["empid"] != null)
            {
                strEmpDisplayName = Request.QueryString["empid"].ToString();
                btnSubmit.Visible = false;
            }
            else
            {
                strEmpDisplayName = SPContext.Current.Web.CurrentUser.Name;
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
            lblEmpDM.Text = intended_Emp.Emp_DM_name;
        }

        private void fill_ddlStrDir()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("التوجهات الاستراتيجية");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Title' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tblStrDir = listItems.GetDataTable();

                            ddlStrDir.DataSource = tblStrDir;
                            ddlStrDir.DataValueField = "ID";
                            ddlStrDir.DataTextField = "Title";
                            ddlStrDir.DataBind();
                        }
                    }
                }
            });
        }

        private void fill_ddlPrimaryGoal()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("الأهداف الرئيسية");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =
                            @"   <Where>
                                        <Eq>
                                            <FieldRef Name='_x0627__x0644__x062a__x0648__x060' />
                                            <Value Type='Integer'>" + ddlStrDir.SelectedValue.ToString() + @"</Value>
                                        </Eq>
                                    </Where>";
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Title' />";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            tblPrimaryGoal = listItems.GetDataTable();
                        }
                    }
                }
            });
        }

        private string read_Active_Set_Goals_Year()
        {
            string pActiveYear = "NoSetGoalsActiveYear";
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

                            if (listItems.Count > 0)
                            {
                                foreach (SPListItem item in listItems)
                                {
                                    if (item["State"].ToString() == "مفعل" && item["Title"].ToString() == "البدء بتفعيل وضع الأهداف لسنة")
                                    {
                                        pActiveYear = item["Year"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            });

            return pActiveYear;
        }

        private string read_Year_to_display_if_none_active()
        {
            string pActiveYear = DateTime.Today.Year.ToString();
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
                                    if (item["State"].ToString() == "عرض فقط" && item["Title"].ToString() == "سنة عرض الأهداف (فى حالة عدم وجود عام مفعل)")
                                    {
                                        pActiveYear = item["Year"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            });

            return pActiveYear;
        }

        private void Make_InActive_Mode()
        {
            PageTitle.InnerText = " عرض الاهداف الفردية لعام " + Active_Set_Goals_Year;
            div_of_AddingGoal.Visible = false;
            div_Mods.Visible = false;
            divButtons.Visible = false;
            foreach (DataControlField d in gvwSetObjectives.Columns)
            {
                if (d.HeaderText == "تعديل" || d.HeaderText == "حذف")
                {
                    d.Visible = false;
                }
            }
        }

        private void getPreviouslySavedObjectives()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb spWeb = oSite.OpenWeb())
                    {
                        SPList spList = spWeb.Lists.TryGetList("الأهداف");
                        if (spList != null)
                        {
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
                                                <Value Type='Text'>" + Active_Set_Goals_Year + @"</Value>
                                             </Eq>
                                          </And>
                                       </Where>";
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' /><FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' />
                                                            <FieldRef Name='ObjWeight' /><FieldRef Name='StrDir' /><FieldRef Name='_x0645__x0639__x0631__x0641__x00' /><FieldRef Name='PrimaryGoal' /><FieldRef Name='PrimaryGoal_x003a__x0627__x0633_' />";
                            SPListItemCollection listItems = spList.GetItems(qry);

                            if (listItems.Count > 0)
                            {
                                tblObjectives = listItems.GetDataTable();
                                string strCurrent_State = tblObjectives.Rows[0]["Status"].ToString();
                                Enum.TryParse(strCurrent_State, out WF_States p_current_state);
                                current_state = p_current_state;
                            }
                        }
                    }
                }
            });
        }

        private void Refresh_GoalsTotal()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                if (tblObjectives != null && tblObjectives.Rows.Count > 0)
                {
                    PercentageTotal = int.Parse(tblObjectives.Compute("Sum(ObjWeight)", string.Empty).ToString());
                    lbl_PercentageTotal.Text = PercentageTotal.ToString();
                    if (PercentageTotal == 100)
                    {
                        lbl_PercentageTotal.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        lbl_PercentageTotal.BackColor = Color.Pink;
                    }

                    if (tblObjectives.Rows[0]["Status"].ToString() == "معتمدة")
                    {
                    }
                }
                else
                {
                    lbl_PercentageTotal.Text = "0";
                }
            });
        }

        protected void Bind_Data_To_Controls()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                gvwSetObjectives.DataSource = tblObjectives;
                gvwSetObjectives.DataBind();
                Refresh_GoalsTotal();
            });
        }

        #endregion Related to Load

        #region Related to Save

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
                if (PercentageTotal == 100)
                    e.IsValid = true;
                else
                    e.IsValid = false;
            });
        }

        private void SaveToSP()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        oWeb.AllowUnsafeUpdates = true;
                        SPList oList = oWeb.Lists["الأهداف"];

                        #region Remove any previous objectives of same Emp and same year

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
                                                <Value Type='Text'>" + Active_Set_Goals_Year + @"</Value>
                                             </Eq>
                                          </And>
                                       </Where>";
                        qry.ViewFieldsOnly = true;
                        qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' />
                                                       <FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' /><FieldRef Name='ObjWeight' />";
                        SPListItemCollection listItems = oList.GetItems(qry);

                        foreach (SPListItem item in listItems)
                        {
                            oList.GetItemById(item.ID).Delete();
                        }

                        #endregion Remove any previous objectives of same Emp and same year

                        #region Add the new (or updated) objectives

                        if (tblObjectives != null)
                        {
                            foreach (DataRow row in tblObjectives.Rows)
                            {
                                SPListItem oListItem = oList.AddItem();
                                oListItem["ObjName"] = row["ObjName"].ToString();
                                oListItem["Status"] = WF_States.Objectives_set_by_Emp.ToString();
                                oListItem["Emp"] = SPContext.Current.Web.EnsureUser(intended_Emp.login_name_to_convert_to_SPUser);
                                oListItem["ObjWeight"] = row["ObjWeight"].ToString();
                                oListItem["ObjQ"] = row["ObjQ"].ToString();
                                oListItem["ObjYear"] = Active_Set_Goals_Year;
                                oListItem["StrDir"] = int.Parse(row["StrDir"].ToString());
                                oListItem["PrimaryGoal"] = int.Parse(row["PrimaryGoal"].ToString());
                                oListItem.Update();
                            }
                        }
                        else
                        {
                        }

                        #endregion Add the new (or updated) objectives

                        oWeb.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

        private void Change_State_to(WF_States pNew_state)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        oWeb.AllowUnsafeUpdates = true;
                        SPList oList = oWeb.Lists["الأهداف"];
                        SPQuery qry = new SPQuery();
                        qry.Query =
                        @"   <Where>
                                        <Eq>
                                            <FieldRef Name='Emp' />
                                            <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                        </Eq>
                                    </Where>";
                        qry.ViewFieldsOnly = true;
                        qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' /><FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' /><FieldRef Name='ObjWeight' />";
                        SPListItemCollection listItems = oList.GetItems(qry);

                        foreach (SPListItem item in listItems)
                        {
                            SPListItem itemToUpdate = oList.GetItemById(item.ID);
                            itemToUpdate["Status"] = pNew_state.ToString();
                            itemToUpdate.Update();
                        }

                        oWeb.AllowUnsafeUpdates = false;
                    }
                }
            });
        }

        private void Show_Success_Message(string m)
        {
            divSuccess.Visible = true;
            lblSuccess.Text = m;
        }

        #endregion Related to Save

        #region Related to Email

        private void Send_Objs_Added_Email_to_Planning_Consultant()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                StringDictionary headers = new StringDictionary();
                headers.Add("to", Planning_Consultant_Email);
                headers.Add("subject", " قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                headers.Add("content-type", "text/html");
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append("<p dir=rtl >");
                bodyText.Append("السلام عليكم ورحمة الله"); bodyText.Append("<br />");
                bodyText.Append("تحية طيبة وبعد:"); bodyText.Append("<br />");
                bodyText.Append(" قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                bodyText.Append("<br />");
                bodyText.Append("الرجاء القيام بمراجعة الأهداف من خلال هذا الرابط واعتمادها");
                bodyText.Append("<br />");
                bodyText.Append("<a href=" + SPContext.Current.Web.Url + "/Pages/نموذج%20وضع%20الأهداف.aspx?empid=" + HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName) + "  >" + n + "</a>");
                bodyText.Append("</p>");
                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        private void Send_Objs_Added_Email_to_DM()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_DM_email);
                headers.Add("subject", " قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                headers.Add("content-type", "text/html");
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append("<p dir=rtl >");
                bodyText.Append("السلام عليكم ورحمة الله"); bodyText.Append("<br />");
                bodyText.Append("تحية طيبة وبعد:"); bodyText.Append("<br />");
                bodyText.Append(" قام " + "\"" + n + "\"" + " بوضع الأهداف الفردية لعام " + Active_Set_Goals_Year);
                bodyText.Append("<br />");
                bodyText.Append("وتمت مراجعتها واعتمادها مبدئيا بواسطة مستشار التخطيط الاستراتيجى وتطوير الأداء ");
                bodyText.Append("<br />");
                bodyText.Append("الرجاء القيام بمراجعة الأهداف من خلال هذا الرابط واعتمادها نهائيا");
                bodyText.Append("<br />");
                bodyText.Append("<a href=" + SPContext.Current.Web.Url + "/Pages/نموذج%20وضع%20الأهداف.aspx?empid=" + HttpUtility.UrlEncode(intended_Emp.Emp_DisplayName) + "  >" + n + "</a>");
                bodyText.Append("</p>");
                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        private void Send_Rej_Email_to_Emp()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_email);
                headers.Add("subject", "طلب تعديلات على الأهداف");
                headers.Add("content-type", "text/html");
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append("<p dir=rtl >");
                bodyText.Append("السلام عليكم ورحمة الله"); bodyText.Append("<br />");
                bodyText.Append("تحية طيبة وبعد:"); bodyText.Append("<br />");
                bodyText.Append("الرجاء القيام بإجراء التعديلات التالية على الأهداف الخاصة بك ، وإعادة ارسالها مرة أخرى:"); bodyText.Append("<br />"); bodyText.Append("<br />");
                bodyText.Append(txtRequired_Mods.Text.Replace(Environment.NewLine, "<br />")); bodyText.Append("<br />"); bodyText.Append("<br />");
                bodyText.Append("</p>");
                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        private void Notify_Emp_that_Objs_finally_approved()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string n = string.Empty;
                if (intended_Emp.Emp_ArabicName != null && intended_Emp.Emp_ArabicName != string.Empty)
                {
                    n = intended_Emp.Emp_ArabicName;
                }
                else
                {
                    n = intended_Emp.Emp_DisplayName;
                }

                StringDictionary headers = new StringDictionary();
                headers.Add("to", intended_Emp.Emp_email);
                headers.Add("subject", "تم اعتماد الأهداف الخاصة بك لعام " + Active_Set_Goals_Year);
                headers.Add("content-type", "text/html");
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append("<p dir=rtl >");
                bodyText.Append("السلام عليكم ورحمة الله"); bodyText.Append("<br />");
                bodyText.Append("تحية طيبة وبعد:"); bodyText.Append("<br />");
                bodyText.Append("تهانينا. لقد تم اعتماد الأهداف الخاصة بك بشكل نهائى");
                bodyText.Append("<br />");
                bodyText.Append("يمكنك مراجعة الأهداف الخاصة بك فى أى وقت وذلك من خلال الرابط التالى:");
                bodyText.Append("<br />");
                bodyText.Append("<a href=\"" + SPContext.Current.Web.Url + "/Pages/نموذج%20وضع%20الأهداف.aspx\" >" + "نموذج وضع الأهداف" + " </a>");
                bodyText.Append("<br />");
                bodyText.Append("<br />");
                bodyText.Append("وشكرا جزيلا لحسن تعاونكم");
                bodyText.Append("</p>");
                SPUtility.SendEmail(SPContext.Current.Web, headers, bodyText.ToString());
            });
        }

        #endregion Related to Email
    }
}