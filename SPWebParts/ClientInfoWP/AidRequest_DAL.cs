using Microsoft.SharePoint;
using System;
using System.Data;

namespace SPWebParts.ClientInfoWP
{
    internal class AidRequest_DAL
    {
        public static AidRequest get_Request_Details_by_ID(string pReqID)
        {
            AidRequest r1 = new AidRequest();

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList spList = web.Lists.TryGetList("AidRequests");
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =
                            @"   <Where>
                          <Eq>
                             <FieldRef Name='ID' />
                             <Value Type='Counter'>"+ pReqID +@"</Value>
                          </Eq>
                       </Where>";
                            qry.ViewFieldsOnly = true;
                            qry.ViewFields = @"<FieldRef Name='Income' /><FieldRef Name='OtherExpenses' /><FieldRef Name='Banks' /><FieldRef Name='OtherOrgAidName' /><FieldRef Name='ChequeOrgName' />
                                               <FieldRef Name='_x0627__x0644__x0625__x0633__x06' /><FieldRef Name='_x0627__x0644__x062f__x0648__x06' /><FieldRef Name='MaritalStatus' />
                                               <FieldRef Name='UnifiedNumber' /><FieldRef Name='Age' /><FieldRef Name='Title' /><FieldRef Name='_x0642__x064a__x0645__x0629__x00' />
                                               <FieldRef Name='_x0627__x0644__x0645__x062f__x06' /><FieldRef Name='Phone' /><FieldRef Name='EIDCardNumber' /><FieldRef Name='Job' /><FieldRef Name='Rent' />
                                               <FieldRef Name='_x062a__x0627__x0631__x064a__x06' /><FieldRef Name='_x062a__x0641__x0627__x0635__x06' /><FieldRef Name='MedCost' />
                                               <FieldRef Name='_x062a__x0648__x0635__x064a__x06' /><FieldRef Name='Nationality' /><FieldRef Name='NewColumn1' /><FieldRef Name='_x0631__x0642__x0645__x0020__x061' />
                                               <FieldRef Name='PreviousZayedAidYear' /><FieldRef Name='FamilySize' /><FieldRef Name='NoOfStudyingSons' /><FieldRef Name='HomeAddress' />
                                               <FieldRef Name='Bills' /><FieldRef Name='PreviousZayedAidAmount' /><FieldRef Name='OtherOrgAidAmount' /><FieldRef Name='_x0645__x0628__x0644__x063a__x00' />
                                               <FieldRef Name='EduExpenses' /><FieldRef Name='_x0645__x062f__x0629__x0020__x06' /><FieldRef Name='IsPreviousOtherOrgAid' /><FieldRef Name='IsPreviousZayedAid' />
                                               <FieldRef Name='LivingExpenses' /><FieldRef Name='Hospital' /><FieldRef Name='HouseType' /><FieldRef Name='HomePhone' /><FieldRef Name='IllnessDesc' />
                                               <FieldRef Name='_x0646__x0648__x0639__x0020__x06' /><FieldRef Name='ClientID' />";

                            DataTable tblReqData= spList.GetItems(qry).GetDataTable();

                            r1.ArabicFullName = tblReqData.Rows[0]["_x0627__x0644__x0625__x0633__x06"].ToString();
                            r1.EIDCardNumber = tblReqData.Rows[0]["EIDCardNumber"].ToString();

                            r1.Phone = tblReqData.Rows[0]["Phone"].ToString();

                            r1.Phone = tblReqData.Rows[0]["Phone"].ToString();
                            r1.AidType = tblReqData.Rows[0]["_x0646__x0648__x0639__x0020__x06"].ToString();
                            r1.AidRequestDetails = tblReqData.Rows[0]["_x062a__x0641__x0627__x0635__x06"].ToString();
                            r1.DueDate = DateTime.Parse(tblReqData.Rows[0]["_x062a__x0627__x0631__x064a__x06"].ToString());

                            r1.RequiredAmount = tblReqData.Rows[0]["_x0642__x064a__x0645__x0629__x00"].ToString();
                            r1.AidRequestStatus = tblReqData.Rows[0]["NewColumn1"].ToString();
                            r1.ResidencyYears = tblReqData.Rows[0]["_x0645__x062f__x0629__x0020__x06"].ToString();
                            r1.PanelOpinion = tblReqData.Rows[0]["_x062a__x0648__x0635__x064a__x06"].ToString();
                            r1.ApprovedAmount = tblReqData.Rows[0]["_x0645__x0628__x0644__x063a__x00"].ToString();

                            //=============================================================================

                            r1.Nationality = tblReqData.Rows[0]["Nationality"].ToString();
                            r1.MaritalStatus = tblReqData.Rows[0]["MaritalStatus"].ToString();
                            r1.Age = tblReqData.Rows[0]["Age"].ToString();
                            r1.FamilySize = tblReqData.Rows[0]["FamilySize"].ToString();
                            r1.Job = tblReqData.Rows[0]["Job"].ToString();
                            r1.HomeAddress = tblReqData.Rows[0]["HomeAddress"].ToString();
                            r1.HomePhone = tblReqData.Rows[0]["HomePhone"].ToString();
                            r1.NoOfStudyingSons = tblReqData.Rows[0]["NoOfStudyingSons"].ToString();
                            r1.Income = tblReqData.Rows[0]["Income"].ToString();
                            r1.HouseType = tblReqData.Rows[0]["HouseType"].ToString();

                            r1.Rent = tblReqData.Rows[0]["Rent"].ToString();
                            r1.Bills = tblReqData.Rows[0]["Bills"].ToString();
                            r1.Banks = tblReqData.Rows[0]["Banks"].ToString();
                            r1.LivingExpenses = tblReqData.Rows[0]["LivingExpenses"].ToString();
                            r1.EduExpenses = tblReqData.Rows[0]["EduExpenses"].ToString();
                            r1.OtherExpenses = tblReqData.Rows[0]["OtherExpenses"].ToString();

                            r1.PreviousZayedAid = tblReqData.Rows[0]["IsPreviousZayedAid"].ToString();
                            r1.PreviousZayedAidYear = tblReqData.Rows[0]["PreviousZayedAidYear"].ToString();
                            r1.PreviousZayedAidAmount = tblReqData.Rows[0]["PreviousZayedAidAmount"].ToString();

                            r1.IsPreviousOtherOrgAid = tblReqData.Rows[0]["IsPreviousOtherOrgAid"].ToString();
                            r1.OtherOrgAidName = tblReqData.Rows[0]["OtherOrgAidName"].ToString();
                            r1.OtherOrgAidAmount = tblReqData.Rows[0]["OtherOrgAidAmount"].ToString();

                            r1.ChequeOrgName = tblReqData.Rows[0]["ChequeOrgName"].ToString();
                            //============================= Medical Aid Request Fields ============================================

                            r1.IllnessDesc = tblReqData.Rows[0]["IllnessDesc"].ToString();
                            r1.Hospital = tblReqData.Rows[0]["Hospital"].ToString();
                            r1.MedCost = tblReqData.Rows[0]["MedCost"].ToString();
                        }
                    }
                }
            });

            return r1;
        }
    }
}