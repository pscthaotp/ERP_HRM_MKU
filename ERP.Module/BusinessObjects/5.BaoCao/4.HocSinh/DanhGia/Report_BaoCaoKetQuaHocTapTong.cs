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

namespace ERP.Module.Report.HocSinh.DanhGia
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo kết quả học tập tổng")]
    public class Report_BaoCaoKetQuaHocTapTong : StoreProcedureReport
    {
        private DateTime _TuNgay = DateTime.Now.Date;
        private DateTime _DenNgay = DateTime.Now.Date.AddMonths(0);

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

        public Report_BaoCaoKetQuaHocTapTong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@TuNgay", TuNgay);
            parameter[1] = new SqlParameter("@DenNgay", DenNgay);
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_DanhGia_BaoCaoKetQuaHocTapTong", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}
