using Microsoft.SharePoint;

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
                SPList spList = spWeb.GetList("/Lists/EPMYear");
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
                    SPWeb oWeb = oSite.OpenWeb();
                    oWeb.AllowUnsafeUpdates = true;

                    SPList oList = oWeb.GetList("/Lists/EPMYear");
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
                });
        }
    }
}