using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.Commons;
using ERP.Module.BaoCao.Custom;

namespace ERP.Module.Report.CCDC
{
    [NonPersistent]
    [ModelDefault("Caption", "Tình hình tăng Công cụ dụng cụ - CCDC")]
    public class Report_BaoCaoTinhHinhGhiTangCCDC : StoreProcedureReport
    {
        private DateTime _tuNgay = DateTime.Now.Date;
        private DateTime _denNgay = DateTime.Now.Date.AddMonths(1);

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _tuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _tuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _denNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _denNgay, value);
            }
        }

        public Report_BaoCaoTinhHinhGhiTangCCDC(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TuNgay", TuNgay);
            param[1] = new SqlParameter("@DenNgay", DenNgay);
            param[2] = new SqlParameter("@CongTy", Common.CongTy(Session).Oid.ToString());
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_CCDC_BaoCaoGhiTangCCDCTuNgayDenNgay", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
