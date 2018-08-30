using System;
using System.Data;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tblObjectives = new DataTable();
                tblObjectives.Columns.Add("ObjName");
                tblObjectives.Columns.Add("ObjWeight");
                tblObjectives.Columns.Add("ObjQ");
            }
            Bind_Data_To_Controls();
        }

        protected void btnAddObjective_Click(object sender, EventArgs e)
        {
            DataRow NewRow = tblObjectives.NewRow();
            NewRow["ObjName"]    = txtObjName.Text; ;
            NewRow["ObjWeight"]  = txtObjWeight.Text;
            NewRow["ObjQ"]          = ddlObjQ.SelectedItem.Text;
            tblObjectives.Rows.Add(NewRow);
            Bind_Data_To_Controls();
        }

        protected void Bind_Data_To_Controls()
        {
            gvwSetObjectives.DataSource = tblObjectives;
            gvwSetObjectives.DataBind();
        }
    }
}