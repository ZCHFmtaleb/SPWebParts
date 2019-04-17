using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDeptTabs.EL
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
        public string DM_email;
        public string DM_name;
        public string Dept_Head_email;
        public string Dept_Head_name;
        public string login_name_to_convert_to_SPUser;
        public string EmpHierLvl;
    }
}
