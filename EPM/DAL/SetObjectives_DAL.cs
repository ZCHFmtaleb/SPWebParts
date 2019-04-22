using EPM.EL;
using Microsoft.SharePoint;
using System;
using System.Data;

namespace EPM.DAL
{
    public class SetObjectives_DAL
    {
        public static SPListItemCollection get_StrategicDirections()
        {
            SPListItemCollection listItems = null;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(spWeb.ServerRelativeUrl+"/Lists/StrDir");
                if (spList != null)
                {
                    SPQuery qry = new SPQuery();
                    qry.ViewFieldsOnly = true;
                    qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Title' />";
                    listItems = spList.GetItems(qry);
                }
            });
            return listItems;
        }

        public static SPListItemCollection get_PrimaryGoals(string str_dir_id)
        {
            SPListItemCollection listItems = null;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(spWeb.ServerRelativeUrl + "/Lists/LinesRelToStrDir");
                if (spList != null)
                {
                    SPQuery qry = new SPQuery();
                    qry.Query =
                    "<Where><Eq><FieldRef Name='RelStrDir_x003a__x0627__x0644__x' /><Value Type='Lookup' >" + str_dir_id + "</Value></Eq></Where>";
                    listItems = spList.GetItems(qry);
                }
            });
            DataTable test = listItems.GetDataTable();
            return listItems;
        }

        

        public static string get_Year_to_display_if_none_active()
        {
            string pActiveYear = DateTime.Today.Year.ToString();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(spWeb.ServerRelativeUrl + "/Lists/EPMYear");
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
            });

            return pActiveYear;
        }

        public static SPListItemCollection getPreviouslySavedObjectives(string strEmpDisplayName, string Active_Set_Goals_Year)
        {
            SPListItemCollection listItems = null;

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
                                                <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                             </Eq>
                                             <Eq>
                                                <FieldRef Name='ObjYear' />
                                                <Value Type='Text'>" + Active_Set_Goals_Year + @"</Value>
                                             </Eq>
                                          </And>
                                       </Where>";
                    listItems = spList.GetItems(qry);
                    DataTable test = listItems.GetDataTable();
                }
            });

            return listItems;
        }

        public static void SaveToSP(string strEmpDisplayName, string Active_Set_Goals_Year, DataTable tblObjectives, string login_name_to_convert_to_SPUser)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb oWeb = oSite.OpenWeb();
                oWeb.AllowUnsafeUpdates = true;
                SPList oList = oWeb.GetList(oWeb.ServerRelativeUrl + "/Lists/Objectives");

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
                        oListItem["Emp"] = SPContext.Current.Web.EnsureUser(login_name_to_convert_to_SPUser);
                        oListItem["ObjWeight"] = row["ObjWeight"].ToString();
                        oListItem["ObjQ"] = row["ObjQ"].ToString();
                        oListItem["ObjYear"] = Active_Set_Goals_Year;
                        oListItem["StrDirID"] = int.Parse(row["StrDirID"].ToString());
                        oListItem["PrimaryGoalID"] = int.Parse(row["PrimaryGoalID"].ToString());
                        oListItem["EmpHierLvl"] = row["EmpHierLvl"].ToString();

                        oListItem.Update();
                    }
                }
                else
                {
                }

                #endregion Add the new (or updated) objectives

                oWeb.AllowUnsafeUpdates = false;
            });
        }
    }
}