using System.ComponentModel;

namespace EPM.EL
{
    public enum WF_States
    {
        [Description("تم وضع الأهداف بواسطة الموظف")]
        Objectives_set_by_Emp,

        [Description("تم اعتماد الأهداف بواسطة المدير المباشر")]
        Objectives_approved_by_DM,

        [Description("تم رفض الأهداف بواسطة المدير المباشر")]
        Objectives_rejected_by_DM,

        [Description("تم اعتماد الأهداف بواسطة مدير الإدارة")]
        Objectives_approved_by_Dept_Head,

        [Description("تم رفض الأهداف بواسطة مدير الإدارة")]
        Objectives_rejected_by_Dept_Head


    }
}