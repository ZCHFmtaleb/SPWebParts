using EPM.EL;
using Microsoft.SharePoint;

namespace EPM.Controllers
{
    internal class WFStatusUpdater
    {
        public static void Change_State_to(WF_States pNew_state, string strEmpDisplayName, string Active_Set_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb oWeb = oSite.OpenWeb();
                oWeb.AllowUnsafeUpdates = true;
                SPList oList = oWeb.GetList(oWeb.ServerRelativeUrl + "/Lists/Objectives");
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
                qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='Status' />";
                SPListItemCollection listItems = oList.GetItems(qry);

                foreach (SPListItem item in listItems)
                {
                    SPListItem itemToUpdate = oList.GetItemById(item.ID);
                    itemToUpdate["Status"] = pNew_state.ToString();
                    itemToUpdate.Update();
                }

                oWeb.AllowUnsafeUpdates = false;
            });
        }
    }
}