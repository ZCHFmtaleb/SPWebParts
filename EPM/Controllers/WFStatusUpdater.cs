using EPM.EL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace EPM.Controllers
{
    internal class WFStatusUpdater
    {
        public static void Change_State_to(WF_States pNew_state, string strEmpDisplayName, string Active_Set_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                spWeb.AllowUnsafeUpdates = true;
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "Objectives")); //SPList oList = oWeb.GetList("/Lists/Objectives");
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
                SPListItemCollection listItems = spList.GetItems(qry);

                foreach (SPListItem item in listItems)
                {
                    SPListItem itemToUpdate = spList.GetItemById(item.ID);
                    itemToUpdate["Status"] = pNew_state.ToString();
                    itemToUpdate.Update();
                }

                spWeb.AllowUnsafeUpdates = false;
            });
        }
    }
}