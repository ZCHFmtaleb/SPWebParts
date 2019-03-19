namespace ServicesDeptTabs.EL
{
    public enum ServicesRequestTypes
    {
        Stationery,
        Maintenance,
        Cleaning,
        Transport,
        Event,
        Other
    }

    public class ServicesRequestTypesUtils
    {
        public static string get_Enum_Arabic(ServicesRequestTypes pT)
        {
            switch (pT)
            {
                case ServicesRequestTypes.Stationery:
                    return "مخازن وقرطاسية";

                case ServicesRequestTypes.Maintenance:
                    return "خدمات عامة وصيانة";

                case ServicesRequestTypes.Cleaning:
                    return "نظافة";

                case ServicesRequestTypes.Transport:
                    return "مواصلات";

                case ServicesRequestTypes.Event:
                    return "ضيافة";

                case ServicesRequestTypes.Other:
                    return "أخرى";

                default:
                    return "أخرى";
            }
        }
    }
}