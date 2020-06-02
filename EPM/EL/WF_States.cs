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
        Objectives_rejected_by_Dept_Head,

        [Description("تم وضع نسب إنجاز الأهداف بواسطة الموظف")]
        Objectives_ProgressSet_by_Emp,

        [Description("تم وضع تقييم الأهداف والكفاءات")]
        ObjsAndSkills_Rated,

        [Description("تم اعتماد لجنة الموارد البشرية")]
        ApprovedBy_HRCommittee

    }
}