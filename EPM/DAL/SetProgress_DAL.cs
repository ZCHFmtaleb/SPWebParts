using EPM.EL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Data;

namespace EPM.DAL
{
    public class SetProgress_DAL
    {
        public static void Save_or_Update_Objs_ProgressNote(string login_name_to_convert_to_SPUser, string Active_Rate_Goals_Year, string Note1)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                spWeb.AllowUnsafeUpdates = true;
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "ObjsRatingNotesByEmp"));
                SPUser user = SPContext.Current.Web.EnsureUser(login_name_to_convert_to_SPUser);
                string strEmpDisplayName = user.Name;

                #region Check if there is already a record for same Employee

                SPQuery qry = new SPQuery();
                qry.Query =
                @"   <Where>
                                          <And>
                                             <Eq>
                                                <FieldRef Name='Emp' />
                                                <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                             </Eq>
                                             <Eq>
                                                <FieldRef Name='ObjsYear' />
                                                <Value Type='Text'>" + Active_Rate_Goals_Year + @"</Value>
                                             </Eq>
                                          </And>
                                       </Where>";
                SPListItemCollection results = spList.GetItems(qry);

                #endregion Check if there is already a record for same Employee

                if (results.Count > 0)
                {
                    SPListItem oListItem = spList.GetItemById(int.Parse(results[0]["ID"].ToString()));
                    oListItem["Note1"] = Note1;
                    oListItem.Update();
                }
                else
                {
                    SPListItem oListItem = spList.AddItem();
                    oListItem["Emp"] = user;
                    oListItem["ObjsYear"] = Active_Rate_Goals_Year;
                    oListItem["Note1"] = Note1;
                    oListItem.Update();
                }

                spWeb.AllowUnsafeUpdates = false;
            });
        }

        public static void Save_or_Update_Objs_EvalNotes(string login_name_to_convert_to_SPUser, string Active_Rate_Goals_Year, EvalNotes evalnotes)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                spWeb.AllowUnsafeUpdates = true;
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "ObjsEvalNotes"));
                SPUser user = SPContext.Current.Web.EnsureUser(login_name_to_convert_to_SPUser);
                string strEmpDisplayName = user.Name;

                #region Check if there is already a record for same Employee

                SPQuery qry = new SPQuery();
                qry.Query =
                @"   <Where>
                                          <And>
                                             <Eq>
                                                <FieldRef Name='Emp' />
                                                <Value Type='User'>" + strEmpDisplayName + @"</Value>
                                             </Eq>
                                             <Eq>
                                                <FieldRef Name='ObjsYear' />
                                                <Value Type='Text'>" + Active_Rate_Goals_Year + @"</Value>
                                             </Eq>
                                          </And>
                                       </Where>";
                SPListItemCollection results = spList.GetItems(qry);

                #endregion Check if there is already a record for same Employee

                if (results.Count > 0)
                {
                    SPListItem oListItem = spList.GetItemById(int.Parse(results[0]["ID"].ToString()));
                    oListItem["Note_ReasonForRating1or5"] = evalnotes.ReasonForRating1or5;
                    oListItem["Note_RecommendedCourses"] = evalnotes.RecommendedCourses;
                    oListItem.Update();
                }
                else
                {
                    SPListItem oListItem = spList.AddItem();
                    oListItem["Emp"] = SPContext.Current.Web.EnsureUser(login_name_to_convert_to_SPUser);
                    oListItem["ObjsYear"] = Active_Rate_Goals_Year;
                    oListItem["Note_ReasonForRating1or5"] = evalnotes.ReasonForRating1or5;
                    oListItem["Note_RecommendedCourses"] = evalnotes.RecommendedCourses;
                    oListItem.Update();
                }

                spWeb.AllowUnsafeUpdates = false;
            });
        }

        public static void Update_Objectives(string columnToBeUpdated, DataTable tblObjectives)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                spWeb.AllowUnsafeUpdates = true;
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "Objectives"));

                if (tblObjectives != null)
                {
                    foreach (DataRow row in tblObjectives.Rows)
                    {
                        SPListItem oListItem = spList.GetItemById(int.Parse(row["ID"].ToString()));

                        oListItem[columnToBeUpdated] = row[columnToBeUpdated].ToString();
                        oListItem.Update();
                    }
                }
                else
                {
                }

                spWeb.AllowUnsafeUpdates = false;
            });
        }
    }
}