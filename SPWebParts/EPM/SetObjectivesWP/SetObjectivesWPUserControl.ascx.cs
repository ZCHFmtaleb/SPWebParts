using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

namespace SPWebParts.EPM.SetObjectivesWP
{
    public partial class SetObjectivesWPUserControl : UserControl
    {
        public DataTable tblObjectives
        {
            get
            {
                return (DataTable)ViewState["tblObjectives"];
            }
            set
            {
                ViewState["tblObjectives"] = value;
            }
        }

        public int PercentageTotal
        {
            get
            {
                return (int)ViewState["PercentageTotal"];
            }
            set
            {
                ViewState["PercentageTotal"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Fill_Current_User_Info();

            if (!IsPostBack)
            {
                tblObjectives = new DataTable();
                tblObjectives.Columns.Add("ObjName");
                tblObjectives.Columns.Add("ObjWeight", typeof(Int32));
                tblObjectives.Columns.Add("ObjQ");
            }

            Bind_Data_To_Controls();
        }

        private void Fill_Current_User_Info()
        {
            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            lblEmpName.Text = currentUser.Name;
            using (SPSite site = SPContext.Current.Site)
            {
                SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                UserProfile cUserProfile = userProfileMgr.GetUserProfile(currentUser.LoginName);
                lblEmpJob.Text = cUserProfile.GetProfileValueCollection("Title")[0].ToString();
                lblEmpDept.Text = cUserProfile.GetProfileValueCollection("Department")[0].ToString();
                UserProfile DM_UserProfile = userProfileMgr.GetUserProfile(cUserProfile.GetProfileValueCollection("Manager")[0].ToString());
                lblEmpDM.Text = DM_UserProfile.DisplayName;
            }
        }

        protected void btnAddObjective_Click(object sender, EventArgs e)
        {
            DataRow NewRow = tblObjectives.NewRow();
            NewRow["ObjName"] = txtObjName.Text; ;
            NewRow["ObjWeight"] = txtObjWeight.Text;
            NewRow["ObjQ"] = ddlObjQ.SelectedItem.Text;
            tblObjectives.Rows.Add(NewRow);
            Bind_Data_To_Controls();
        }

        protected void Bind_Data_To_Controls()
        {
            gvwSetObjectives.DataSource = tblObjectives;
            gvwSetObjectives.DataBind();
            Refresh_GoalsTotal();
        }

        private void Refresh_GoalsTotal()
        {
            if (tblObjectives!= null && tblObjectives.Rows.Count>0)
            {
                PercentageTotal =int.Parse(tblObjectives.Compute("Sum(ObjWeight)", string.Empty).ToString());
                lbl_PercentageTotal.Text = PercentageTotal.ToString();
                if (PercentageTotal==100)
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
        }

        protected void gvwSetObjectives_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            tblObjectives.Rows.RemoveAt(e.RowIndex);
            Bind_Data_To_Controls();
        }
    }
}