using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace EPM.DAL
{
    public class EnableYear_DAL
    {
        public static SPListItemCollection get_Active_Years()
        {
            SPListItemCollection listItems = null;

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
                                                 <Value Type='Choice'>مفعل</Value>
                                              </Eq>
                                           </Where>";
                    qry.ViewFieldsOnly = true;
                    qry.ViewFields = @"<FieldRef Name='Title' /><FieldRef Name='Year' /><FieldRef Name='State' />";
                    listItems = spList.GetItems(qry);
                }
            });
            return listItems;
        }

        public static void Update_Year(string mode, string year, string new_state)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                    SPWeb spWeb = oSite.OpenWeb();
                    spWeb.AllowUnsafeUpdates = true;

                    SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "EPMYear"));  //SPList oList = oWeb.GetList("/Lists/EPMYear");
                    SPQuery qry = new SPQuery();
                    qry.Query =
                                    @"   <Where>
                                                    <Eq>
                                                        <FieldRef Name='Title' />
                                                        <Value Type='Text'>" + mode + @"</Value>
                                                    </Eq>
                                                </Where>";
                    SPListItemCollection listItems = spList.GetItems(qry);

                    SPListItem itemToUpdate = spList.GetItemById(listItems[0].ID);
                    itemToUpdate["Year"] = year;
                    itemToUpdate["State"] = new_state;
                    itemToUpdate.Update();

                    spWeb.AllowUnsafeUpdates = false;
                });
        }

        public static string get_Active_Set_Goals_Year()
        {
            string pActiveYear = "NoSetGoalsActiveYear";
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "EPMYear"));   //SPList spList = spWeb.GetList("/Lists/EPMYear");
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
            });

            return pActiveYear;
        }

        public static string read_Active_Rate_Goals_Year()
        {
            string pActiveYear = "NoRateGoalsActiveYear";
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
                            if (item["State"].ToString() == "مفعل" && item["Title"].ToString() == "البدء بتفعيل التقييم السنوى لسنة")
                            {
                                pActiveYear = item["Year"].ToString();
                            }
                        }
                    }
                }
            });

            return pActiveYear;
        }

    }
}