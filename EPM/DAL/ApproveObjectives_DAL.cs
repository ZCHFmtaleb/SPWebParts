using EPM.Controllers;
using EPM.EL;

namespace EPM.DAL
{
    public class ApproveObjectives_DAL
    {
        public static void Update_Status_To_Apporoved_by_DM(string strEmpDisplayName, string Active_Set_Goals_Year)
        {
            WFStatusUpdater.Change_State_to(WF_States.Objectives_approved_by_DM, strEmpDisplayName, Active_Set_Goals_Year);
        }

        public static void Update_Status_To_Rejected_by_DM(string strEmpDisplayName, string Active_Set_Goals_Year)
        {
            WFStatusUpdater.Change_State_to(WF_States.Objectives_rejected_by_DM, strEmpDisplayName, Active_Set_Goals_Year);
        }

        public static void Update_Status_To_Apporoved_by_Dept_Head(string strEmpDisplayName, string Active_Set_Goals_Year)
        {
            WFStatusUpdater.Change_State_to(WF_States.Objectives_approved_by_Dept_Head, strEmpDisplayName, Active_Set_Goals_Year);
        }

        public static void Update_Status_To_Rejected_by_Dept_Head(string strEmpDisplayName, string Active_Set_Goals_Year)
        {
            WFStatusUpdater.Change_State_to(WF_States.Objectives_rejected_by_Dept_Head, strEmpDisplayName, Active_Set_Goals_Year);
        }
    }
}