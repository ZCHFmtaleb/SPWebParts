using EPM.EL;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace EPM.DAL
{
    internal class Emp_DAL
    {
        public static Emp get_Emp_Info(string strEmpDisplayName)
        {

            Emp intended_Emp = new Emp();

            SPSecurity.RunWithElevatedPrivileges(delegate ()
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

                if (pinfo.Department != null)
                {
                    intended_Emp.Emp_Department = pinfo.Department;
                }
                else
                {
                    intended_Emp.Emp_Department = string.Empty;
                }
                //lblEmpDept.Text = cUserProfile.GetProfileValueCollection("Department")[0].ToString();

                /*I used the "Fax" field to store the "Emp_Rank" becuase there is no "Emp_Rank" field in SharePoint profile fields */
                // الدرجة الوظيفية وهى من 1-14 حيث أنها تؤدى إلى اختلاف طريقة التقييم
                object rank = cUserProfile.GetProfileValueCollection("Fax")[0];
                if (rank != null && rank.ToString() != string.Empty)
                {
                    intended_Emp.Emp_Rank = cUserProfile.GetProfileValueCollection("Fax")[0].ToString();
                }
                else
                {
                    intended_Emp.Emp_Rank = "4";  
                }



                string DM_value = string.Empty;
                string Dept_Head_value = string.Empty;
                if (cUserProfile.GetProfileValueCollection("Manager")[0]!= null)
                {
                    DM_value = cUserProfile.GetProfileValueCollection("Manager")[0].ToString();
                }

                
                if (DM_value != string.Empty)
                {
                    UserProfile DM_UserProfile = userProfileMgr.GetUserProfile(DM_value);
                    intended_Emp.DM_name = DM_UserProfile.DisplayName;
                    intended_Emp.DM_email = DM_UserProfile["WorkEmail"].ToString();

                    if (DM_UserProfile.GetProfileValueCollection("Manager")[0] != null)
                    {
                        Dept_Head_value= DM_UserProfile.GetProfileValueCollection("Manager")[0].ToString();
                    }

                    if (Dept_Head_value != string.Empty)
                    {
                        UserProfile Dept_Head_UserProfile = userProfileMgr.GetUserProfile(Dept_Head_value);
                        intended_Emp.Dept_Head_name = Dept_Head_UserProfile.DisplayName;
                        intended_Emp.Dept_Head_email = Dept_Head_UserProfile["WorkEmail"].ToString();

                        //============================================
                        // "testsp@zayed.org.ae" is the test "General Director" 
                        SPGroup grp = web.SiteGroups["المدير العام"];
                        if (intended_Emp.Dept_Head_email == grp.Users[0].Email || intended_Emp.Dept_Head_email == "testsp@zayed.org.ae")
                        {
                            intended_Emp.EmpHierLvl = "2";
                        }
                        else
                        {
                            intended_Emp.EmpHierLvl = "1";
                        }

                        //=============================================
                    }
                    else
                    {
                        intended_Emp.EmpHierLvl = "3";
                    }
                }
                else
                {
                    intended_Emp.EmpHierLvl = "4";
                }
            });
            return intended_Emp;
        }
    }
}