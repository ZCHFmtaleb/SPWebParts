using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.DAL
{
    public class SetProgress_DAL
    {
        public static void SaveObjsNote1(string login_name_to_convert_to_SPUser, string Active_Rate_Goals_Year, string Note1)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPSite oSite = new SPSite(SPContext.Current.Web.Url);
                SPWeb spWeb = oSite.OpenWeb();
                spWeb.AllowUnsafeUpdates = true;
                SPList spList = spWeb.GetList(SPUrlUtility.CombineUrl(spWeb.ServerRelativeUrl, "lists/" + "ObjsRatingNotesByEmp"));   


                        SPListItem oListItem = spList.AddItem();

                        oListItem["Emp"] = SPContext.Current.Web.EnsureUser(login_name_to_convert_to_SPUser);
                        oListItem["ObjsYear"] = Active_Rate_Goals_Year;
                        oListItem["Note1"] = Note1;

                        oListItem.Update();

                spWeb.AllowUnsafeUpdates = false;
            });
        }
    }
}
