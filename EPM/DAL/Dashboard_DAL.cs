using EPM.EL;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;

namespace EPM.DAL
{
    public class Dashboard_DAL
    {
        public static DataTable get_Dashboard_DT(string Active_Set_Goals_Year)
        {
            DataTable Dashboard = new DataTable();
            Dashboard.Columns.Add("EnglishName");
            Dashboard.Columns.Add("Status");
            Dashboard.Columns.Add("Email");
            Dashboard.Columns.Add("ArabicName");
            Dashboard.Columns.Add("Department");

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
            SPSite site = SPContext.Current.Site;
                SPWeb web = site.RootWeb;

                SortedList<string, int> sl = new SortedList<string, int>();
                sl.Add("administrator",1);
                sl.Add("Everyone",2);
                sl.Add(@"NT AUTHORITY\authenticated users",3);
                sl.Add(@"NT AUTHORITY\LOCAL SERVICE",4);
                sl.Add("System Account",5);
                sl.Add("ZF eServices",6);
                sl.Add("Administrator",7);


                foreach (SPUser sp in web.SiteUsers)
                {
                    if (string.IsNullOrEmpty(sp.Email))
                    {
                        continue;
                    }
                    else if (sl.ContainsKey(sp.Name))
                    {
                        continue;
                    }
                    else
                    {
                        string status = get_Emp_Application_Status(sp, Active_Set_Goals_Year);
                        Emp emp = Emp_DAL.get_Emp_Info(sp.Name);
                        DataRow NewRow = Dashboard.NewRow();
                        NewRow["EnglishName"] = sp.Name;
                        NewRow["Status"] = status;
                        NewRow["Email"] = sp.Email;
                        NewRow["ArabicName"] = emp.Emp_ArabicName;
                        NewRow["Department"] = emp.Emp_Department;

                        Dashboard.Rows.Add(NewRow);
                    }
                }
            });
            return Dashboard;
        }

        private static string get_Emp_Application_Status(SPUser sp, string Active_Set_Goals_Year)
        {
            string Status = string.Empty;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(spWeb.ServerRelativeUrl + "/Lists/Objectives");
                if (spList != null)
                {
                    SPQuery qry = new SPQuery();
                    qry.Query =
                    @"   <Where>
                                          <And>
                                             <Eq>
                                                <FieldRef Name='Emp' />
                                                <Value Type='User'>" + sp.Name + @"</Value>
                                             </Eq>
                                             <Eq>
                                                <FieldRef Name='ObjYear' />
                                                <Value Type='Text'>" + Active_Set_Goals_Year + @"</Value>
                                             </Eq>
                                          </And>
                                       </Where>";
                    qry.ViewFieldsOnly = true;
                    qry.ViewFields = @"<FieldRef Name='Status' />";
                    SPListItemCollection result = spList.GetItems(qry);

                    if (result.Count ==0)
                    {
                        Status= "Objectives not set";
                    }
                    else
                    {
                        Status = result[0]["Status"].ToString();
                    }
                }
            });

            return Status;
        }
    }
}