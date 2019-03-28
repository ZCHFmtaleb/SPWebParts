using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPM.EL;

namespace EPM.Controllers
{
    class WFStatusUpdater
    {


        private void Change_State_to(WF_States pNew_state, string strEmpDisplayName, string Active_Set_Goals_Year)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb oWeb = oSite.OpenWeb();
                oWeb.AllowUnsafeUpdates = true;
                SPList oList = oWeb.Lists["الأهداف"];
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
                qry.ViewFields = @"<FieldRef Name='ID' /><FieldRef Name='ObjName' /><FieldRef Name='Status' /><FieldRef Name='Emp' /><FieldRef Name='ObjQ' /><FieldRef Name='ObjYear' /><FieldRef Name='ObjType' /><FieldRef Name='ObjWeight' />";
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
