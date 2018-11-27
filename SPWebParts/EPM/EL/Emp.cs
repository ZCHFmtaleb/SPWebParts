using Microsoft.SharePoint;
using System;

namespace SPWebParts.EPM.EL
{
    [Serializable]
    public class Emp
    {
        public string Emp_DisplayName;
        public string Emp_ArabicName;
        public string Emp_Rank;
        public string Emp_JobTitle;
        public string Emp_email;
        public string Emp_Department;
        public string Emp_DM_email;
        public string Emp_DM_name;
        public string login_name_to_convert_to_SPUser;
    }
}