using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using SPWebParts.EPM.EL;

namespace SPWebParts.EPM.DAL
{
    public class Emp_DAL
    {
        public static Emp get_Emp_Info(Emp intended_Emp, string strEmpDisplayName)
        {
            try
            {
                SPSite site = SPContext.Current.Site;
                SPWeb web = site.OpenWeb();
                        SPPrincipalInfo pinfo = SPUtility.ResolvePrincipal(web, strEmpDisplayName, SPPrincipalType.User, SPPrincipalSource.All, null, false);
                        SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                        UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                        UserProfile cUserProfile = userProfileMgr.GetUserProfile(pinfo.LoginName);
                        intended_Emp.login_name_to_convert_to_SPUser = pinfo.LoginName;

                        intended_Emp.Emp_DisplayName = pinfo.DisplayName;
                        if (cUserProfile.GetProfileValueCollection("AboutMe")[0] != null)
                        {
                            intended_Emp.Emp_ArabicName = cUserProfile.GetProfileValueCollection("AboutMe")[0].ToString();
                        }
                        else
                        {
                            intended_Emp.Emp_ArabicName = string.Empty;
                        }

                        intended_Emp.Emp_email = pinfo.Email;
                        //lblEmpName.Text = emp.Name;

                        intended_Emp.Emp_JobTitle = pinfo.JobTitle;
                        //lblEmpJob.Text = cUserProfile.GetProfileValueCollection("Title")[0].ToString();

                        intended_Emp.Emp_Department = pinfo.Department;
                        //lblEmpDept.Text = cUserProfile.GetProfileValueCollection("Department")[0].ToString();

                        if (cUserProfile.GetProfileValueCollection("Fax")[0] != null)
                        {
                            intended_Emp.Emp_Rank = cUserProfile.GetProfileValueCollection("Fax")[0].ToString();
                        }
                        else
                        {
                            intended_Emp.Emp_Rank = string.Empty;
                        }

                        UserProfile DM_UserProfile = userProfileMgr.GetUserProfile(cUserProfile.GetProfileValueCollection("Manager")[0].ToString());
                        intended_Emp.Emp_DM_name = DM_UserProfile.DisplayName;
                        intended_Emp.Emp_DM_email = DM_UserProfile["WorkEmail"].ToString();
            }
            catch (System.Exception)
            {
            }
            return intended_Emp;
        }
    }
}