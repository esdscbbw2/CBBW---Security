using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBBW.BOL.TADA;
using CBBW.BOL.Tour;

namespace CBBW.DAL.DataSync
{
    public class RulesDataSync
    {
        #region Version 2 Changes
        public DataTable getTourRules(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText,ref string pMsg)
        {
            try
            {
                SortDirection = SortDirection.Substring(0, 1).ToUpper();
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection;
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar, 250);
                para[paracount++].Value = SearchText;                
                using (SQLHelper sql = new SQLHelper("[RUL].[GetTourRulesV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable createNewTourRuleV2(TourRuleSaveInfo trd, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[20];
                para[paracount] = new SqlParameter("@EntryDate", SqlDbType.Date);
                para[paracount++].Value = trd.EntryDate;
                para[paracount] = new SqlParameter("@EntryTime", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.EntryTime;
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = trd.EffectiveDate;
                para[paracount] = new SqlParameter("@PublicTranDelay_HalfDA", SqlDbType.Float);
                para[paracount++].Value = trd.PublicTranDelay_HalfDA;
                para[paracount] = new SqlParameter("@OtherTranDelay_HalfDA", SqlDbType.Float);
                para[paracount++].Value = trd.OtherTranDelay_HalfDA;
                para[paracount] = new SqlParameter("@PublicTranDelay_FullDA", SqlDbType.Float);
                para[paracount++].Value = trd.PublicTranDelay_FullDA;
                para[paracount] = new SqlParameter("@OtherTranDelay_FullDA", SqlDbType.Float);
                para[paracount++].Value = trd.OtherTranDelay_FullDA;
                para[paracount] = new SqlParameter("@NightPunch_From", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.NightPunch_From;
                para[paracount] = new SqlParameter("@NightPunch_To", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.NightPunch_To;
                para[paracount] = new SqlParameter("@EarlyMorningPunch_From", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.EarlyMorningPunch_From;
                para[paracount] = new SqlParameter("@EarlyMorningPunch_To", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.EarlyMorningPunch_To;
                para[paracount] = new SqlParameter("@MaxDayAllowed", SqlDbType.Int);
                para[paracount++].Value = trd.MaxDayAllowed;
                para[paracount] = new SqlParameter("@MaxTraveltime_ComVeh_50km", SqlDbType.Float);
                para[paracount++].Value = trd.MaxTraveltime_ComVeh_50km;
                para[paracount] = new SqlParameter("@MaxTraveltime_PubTran_50km", SqlDbType.Float);
                para[paracount++].Value = trd.MaxTraveltime_PubTran_50km;
                para[paracount] = new SqlParameter("@GracePeriod_200km", SqlDbType.Float);
                para[paracount++].Value = trd.GracePeriod_200km;
                para[paracount] = new SqlParameter("@MinutesGracePeriodAllowed", SqlDbType.Bit);
                para[paracount++].Value = trd.MinutesGracePeriodAllowed==1?true:false;
                para[paracount] = new SqlParameter("@LICAllowTour", SqlDbType.Bit);
                para[paracount++].Value = trd.LICAllowTour==1?true:false;
                para[paracount] = new SqlParameter("@ServiceTypeCodes", SqlDbType.NVarChar, 50);
                para[paracount++].Value = trd.ServiceTypeCodes;
                para[paracount] = new SqlParameter("@ServiceTypeTexts", SqlDbType.NVarChar);
                para[paracount++].Value = trd.ServiceTypeTexts;
                para[paracount] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                para[paracount++].Value = trd.CreatedBy;
                using (SQLHelper sql = new SQLHelper("[RUL].[SetToursRuleV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getServiceTypesFromEffectiveDate(DateTime EffectiveDate, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];                
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = EffectiveDate;                
                using (SQLHelper sql = new SQLHelper("[RUL].[getServiceTypesFromEffectiveDate]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable GetTourInfoForServiceType(string ServiceTypes, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@ServiceTypeCode", SqlDbType.NVarChar,10);
                para[paracount++].Value = ServiceTypes;
                using (SQLHelper sql = new SQLHelper("[RUL].[getTourInfoForServiceType]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getLastTourInfoFromServiceTypeCodes(string serviceTypeCodes, int IsView, DateTime EffectiveDate, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[3];
                para[paracount] = new SqlParameter("@ServiceTypeCodes", SqlDbType.NVarChar,50);
                para[paracount++].Value = serviceTypeCodes;
                para[paracount] = new SqlParameter("@IsView", SqlDbType.Int);
                para[paracount++].Value = IsView;
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = EffectiveDate;
                using (SQLHelper sql = new SQLHelper("[RUL].[getLastTourInfoFromServiceTypeCodes]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getTourInfoFromEffectiveDate(DateTime EffectiveDate, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = EffectiveDate;
                using (SQLHelper sql = new SQLHelper("[RUL].[getTourInfoFromEffectiveDate]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable FinalSubmitToursRuleV2(DateTime EffectiveDate, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = EffectiveDate;
                using (SQLHelper sql = new SQLHelper("[RUL].[FinalSubmitToursRuleV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable RemoveToursRuleV2(DateTime EffectiveDate, string ServiceTypeCodes, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = EffectiveDate;
                para[paracount] = new SqlParameter("@ServiceTypeCodes", SqlDbType.NVarChar,50);
                para[paracount++].Value = ServiceTypeCodes;
                using (SQLHelper sql = new SQLHelper("[RUL].[RemoveToursRuleV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable getTADARules(int DisplayLength, int DisplayStart, int SortColumn,
            string SortDirection, string SearchText, ref string pMsg)
        {
            try
            {
                SortDirection = SortDirection.Substring(0, 1).ToUpper();
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[5];
                para[paracount] = new SqlParameter("@DisplayLength", SqlDbType.Int);
                para[paracount++].Value = DisplayLength;
                para[paracount] = new SqlParameter("@DisplayStart", SqlDbType.Int);
                para[paracount++].Value = DisplayStart;
                para[paracount] = new SqlParameter("@sortCol", SqlDbType.Int);
                para[paracount++].Value = SortColumn;
                para[paracount] = new SqlParameter("@SortDir", SqlDbType.NVarChar, 1);
                para[paracount++].Value = SortDirection;
                para[paracount] = new SqlParameter("@Search", SqlDbType.NVarChar, 250);
                para[paracount++].Value = SearchText;
                using (SQLHelper sql = new SQLHelper("[RUL].[GetTADARulesV2]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DateTime? getLastEffectiveDatePartiallyFilled(int RuleType,ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("SELECT [RUL].[GetLastEffectiveDatePartiallyFilled]("+ RuleType + ")", CommandType.Text))
                {
                    return DateTime.Parse(sql.ExecuteScaler(ref pMsg).ToString());
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        #endregion
        public DataTable getToursRules(ref string pMsg)
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [RUL].[getToursRules]()", CommandType.Text))
                {
                    return sql.GetDataTable(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getLastToursRule(ref string pMsg)
        {
            try
            {                
                using (SQLHelper sql = new SQLHelper("[RUL].[getLastToursRule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getToursRuleByID(int ID,ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@ID", SqlDbType.Int);
                para[paracount++].Value = ID;
                using (SQLHelper sql = new SQLHelper("[RUL].[getToursRuleByID]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para,ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable removeTourRule(int ID, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@ID", SqlDbType.Int);
                para[paracount++].Value = ID;
                using (SQLHelper sql = new SQLHelper("[RUL].[RemoveToursRule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }

            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        
        public DataTable createNewTourRule(TourRuleDetails trd,ref string pMsg)
        {
            try
            {
                CommonTable ct = new CommonTable(trd.SelectedServiceTypeIds);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[20];
                para[paracount] = new SqlParameter("@EntryDate", SqlDbType.Date);
                para[paracount++].Value = trd.EntryDate;
                para[paracount] = new SqlParameter("@EntryTime", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.EntryTime;
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = trd.EffectiveDate;
                para[paracount] = new SqlParameter("@PublicTranDelay_HalfDA", SqlDbType.Float);
                para[paracount++].Value = trd.PublicTranDelay_HalfDA;
                para[paracount] = new SqlParameter("@OtherTranDelay_HalfDA", SqlDbType.Float);
                para[paracount++].Value = trd.OtherTranDelay_HalfDA;
                para[paracount] = new SqlParameter("@PublicTranDelay_FullDA", SqlDbType.Float);
                para[paracount++].Value = trd.PublicTranDelay_FullDA;
                para[paracount] = new SqlParameter("@OtherTranDelay_FullDA", SqlDbType.Float);
                para[paracount++].Value = trd.OtherTranDelay_FullDA;
                para[paracount] = new SqlParameter("@NightPunch_From", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.NightPunch_From;
                para[paracount] = new SqlParameter("@NightPunch_To", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.NightPunch_To;
                para[paracount] = new SqlParameter("@EarlyMorningPunch_From", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.EarlyMorningPunch_From;
                para[paracount] = new SqlParameter("@EarlyMorningPunch_To", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.EarlyMorningPunch_To;
                para[paracount] = new SqlParameter("@ReadRule1", SqlDbType.Bit);
                para[paracount++].Value = trd.ReadRule1;
                para[paracount] = new SqlParameter("@ReadRule2", SqlDbType.Bit);
                para[paracount++].Value = trd.ReadRule2;
                para[paracount] = new SqlParameter("@ReadRule3", SqlDbType.Bit);
                para[paracount++].Value = trd.ReadRule3;
                para[paracount] = new SqlParameter("@ReadRule4", SqlDbType.Bit);
                para[paracount++].Value = trd.ReadRule4;
                para[paracount] = new SqlParameter("@MaxDayAllowed", SqlDbType.Int);
                para[paracount++].Value = trd.MaxDayAllowed;
                para[paracount] = new SqlParameter("@MaxTraveltime_ComVeh_50km", SqlDbType.Float);
                para[paracount++].Value = trd.MaxTraveltime_ComVeh_50km;
                para[paracount] = new SqlParameter("@MaxTraveltime_PubTran_50km", SqlDbType.Float);
                para[paracount++].Value = trd.MaxTraveltime_PubTran_50km;
                para[paracount] = new SqlParameter("@GracePeriod_200km", SqlDbType.Float);
                para[paracount++].Value = trd.GracePeriod_200km;
                para[paracount] = new SqlParameter("@UDTServiceTypes", SqlDbType.Structured);
                para[paracount++].Value = ct.UDTable;
                using (SQLHelper sql = new SQLHelper("[RUL].[SetToursRule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch(Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable IsValidTourRule(TourRuleDetails trd, ref string pMsg)
        {
            try
            {
                CommonTable categories = new CommonTable(trd.SelectedServiceTypeIds);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = trd.EffectiveDate;
                para[paracount] = new SqlParameter("@UDTServiceTypes", SqlDbType.Structured);
                para[paracount++].Value = categories.UDTable;
                using (SQLHelper sql = new SQLHelper("[RUL].[IsValidTourRule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }

        public DataTable getTADARules(ref string pMsg) 
        {
            try
            {
                using (SQLHelper sql = new SQLHelper("Select * from [RUL].[getTADARules]()", CommandType.Text))
                {
                    return sql.GetDataTable(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getLastTADARules(ref string pMsg)
        {
            try
            {                
                using (SQLHelper sql = new SQLHelper("[RUL].[getLastTADARule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataSet getTADARulesByID(int ID, ref string pMsg) 
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@ID", SqlDbType.Int);
                para[paracount++].Value = ID;
                using (SQLHelper sql = new SQLHelper("[RUL].[getTADARuleByID]", CommandType.StoredProcedure))
                {
                    return sql.GetDataSet(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable removeTADARule(int ID, ref string pMsg)
        {
            try
            {
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[1];
                para[paracount] = new SqlParameter("@ID", SqlDbType.Int);
                para[paracount++].Value = ID;
                using (SQLHelper sql = new SQLHelper("[RUL].[RemoveTADARule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }

        }
        public DataTable createNewTADARule(TADARuleDetails trd, ref string pMsg)
        {
            try
            {
                CommonTable categories = new CommonTable(trd.SelectedCategoryIds);
                CommonTable pubtran = new CommonTable(trd.PubTranOptions);
                CommonTable comptran = new CommonTable(trd.CompTranOptions);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[24];
                para[paracount] = new SqlParameter("@EntryDate", SqlDbType.Date);
                para[paracount++].Value = trd.EntryDate;
                para[paracount] = new SqlParameter("@EntryTime", SqlDbType.NVarChar, 15);
                para[paracount++].Value = trd.EntryTime;
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = trd.EffectiveDate;
                para[paracount] = new SqlParameter("@ConnectingID", SqlDbType.NVarChar);
                para[paracount++].Value = trd.NewConnectingID;
                para[paracount] = new SqlParameter("@MinHoursForHalfDA", SqlDbType.Int);
                para[paracount++].Value = trd.MinHoursForHalfDA;
                para[paracount] = new SqlParameter("@MinKmsForDA", SqlDbType.Int);
                para[paracount++].Value = trd.MinKmsForDA;
                para[paracount] = new SqlParameter("@LodgingExpOnCompAcco", SqlDbType.Bit);
                para[paracount++].Value = trd.LodgingExpOnCompAcco;
                para[paracount] = new SqlParameter("@LocalConvEligibility", SqlDbType.Bit);
                para[paracount++].Value = trd.LocalConvEligibility;
                para[paracount] = new SqlParameter("@DepuStaffDAEligibility", SqlDbType.Bit);
                para[paracount++].Value = trd.DepuStaffDAEligibility;
                para[paracount] = new SqlParameter("@ExtraDAApplicability", SqlDbType.Bit);
                para[paracount++].Value = trd.ExtraDAApplicability;

                para[paracount] = new SqlParameter("@IsDAActualSpend", SqlDbType.Bit);
                para[paracount++].Value = trd.TADAParam.IsDAActualSpend;
                para[paracount] = new SqlParameter("@IsLodgingAllowed", SqlDbType.Bit);
                para[paracount++].Value = trd.TADAParam.IsLodgingAllowed;
                para[paracount] = new SqlParameter("@Metro_DAPerDay", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.Metro_DAPerDay;
                para[paracount] = new SqlParameter("@City_DAPerDay", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.City_DAPerDay;
                para[paracount] = new SqlParameter("@Town_DAPerDay", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.Town_DAPerDay;
                para[paracount] = new SqlParameter("@Metro_MaxLodgingExp", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.Metro_MaxLodgingExp;
                para[paracount] = new SqlParameter("@City_MaxLodgingExp", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.City_MaxLodgingExp;
                para[paracount] = new SqlParameter("@Town_MaxLodgingExp", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.Town_MaxLodgingExp;
                para[paracount] = new SqlParameter("@Metro_MaxLocalConv", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.Metro_MaxLocalConv;
                para[paracount] = new SqlParameter("@City_MaxLocalConv", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.City_MaxLocalConv;
                para[paracount] = new SqlParameter("@Town_MaxLocalConv", SqlDbType.Int);
                para[paracount++].Value = trd.TADAParam.Town_MaxLocalConv;

                para[paracount] = new SqlParameter("@UDTCategories", SqlDbType.Structured);
                para[paracount++].Value = categories.UDTable;
                para[paracount] = new SqlParameter("@UDTPublicTrans", SqlDbType.Structured);
                para[paracount++].Value = pubtran.UDTable;
                para[paracount] = new SqlParameter("@UDTCompTrans", SqlDbType.Structured);
                para[paracount++].Value = comptran.UDTable;
                using (SQLHelper sql = new SQLHelper("[RUL].[SetTADARule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        public DataTable IsValidTADARule(TADARuleDetails trd, ref string pMsg)
        {
            try
            {
                CommonTable categories = new CommonTable(trd.SelectedCategoryIds);
                int paracount = 0;
                SqlParameter[] para = new SqlParameter[2];
                para[paracount] = new SqlParameter("@EffectiveDate", SqlDbType.Date);
                para[paracount++].Value = trd.EffectiveDate;
                para[paracount] = new SqlParameter("@UDTCategories", SqlDbType.Structured);
                para[paracount++].Value = categories.UDTable;
                using (SQLHelper sql = new SQLHelper("[RUL].[IsValidTADARule]", CommandType.StoredProcedure))
                {
                    return sql.GetDataTable(para, ref pMsg);
                }
            }
            catch (Exception ex) { pMsg = ex.Message; return null; }
        }
        
    }
}
