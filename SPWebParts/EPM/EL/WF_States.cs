using System.ComponentModel;

namespace SPWebParts.EPM.EL
{
    public enum WF_States
    {
        [Description("بداية التشغيل")]
        first_run,

        [Description("تم وضع الأهداف بواسطة الموظف")]
        Objectives_set_by_Emp,

        [Description("تم اعتماد الأهداف بواسطة مستشار التخطيط")]
        Objectives_approved_by_Planning_Consultant,

        [Description("تم رفض الأهداف بواسطة مستشار التخطيط")]
        Objectives_rejected_by_Planning_Consultant,

        [Description("تم اعتماد الأهداف بواسطة المدير المباشر")]
        Objectives_approved_by_DM,

        [Description("تم رفض الأهداف بواسطة المدير المباشر")]
        Objectives_rejected_by_DM
    }
}