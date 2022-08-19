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
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.TSCD
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo tăng Tài Sản - TSCD")]
    public class Report_BaoCaoTangTaiSan : StoreProcedureReport, ICongTy
    {
        private DateTime _TuNgay = DateTime.Now.Date;
        private DateTime _DenNgay = DateTime.Now.Date.AddMonths(1);
        private CongTy _CongTy;
        private bool _TaiSan = true;
        private bool _CongCu = true;

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
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
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }
        
        [ModelDefault("Caption", "Công ty")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [ModelDefault("Caption", "Tài sản")]
        public bool TaiSan
        {
            get
            {
                return _TaiSan;
            }
            set
            {
                SetPropertyValue("TaiSan", ref _TaiSan, value);
            }
        }

        [ModelDefault("Caption", "Công cụ")]
        public bool CongCu
        {
            get
            {
                return _CongCu;
            }
            set
            {
                SetPropertyValue("CongCu", ref _CongCu, value);
            }
        }

        public Report_BaoCaoTangTaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@TuNgay", TuNgay);
            param[1] = new SqlParameter("@DenNgay", DenNgay);
            param[2] = new SqlParameter("@TaiSan", TaiSan);
            param[3] = new SqlParameter("@CongCu", CongCu);
            param[4] = new SqlParameter("@CongTy", CongTy.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TSCD_BaoCaoTangTaiSan", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
