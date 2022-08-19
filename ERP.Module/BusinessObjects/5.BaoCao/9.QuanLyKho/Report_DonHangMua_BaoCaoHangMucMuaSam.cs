using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.QuanLyKho.Khos;
using ERP.Module.NghiepVu.QuanLyKho.HangHoas;

namespace ERP.Module.Report.HocSinh.QuanLyKho
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách đơn hàng mua - Kho")]
    public class Report_DonHangMua_BaoCaoHangMucMuaSam : StoreProcedureReport,ICongTy
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private HangHoa _HangHoa;
        private CongTy _CongTy;

        [ModelDefault("Caption", "Từ ngày")]
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

        [ModelDefault("Caption", "Hàng hóa")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HangHoa HangHoa
        {
            get
            {
                return _HangHoa;
            }
            set
            {
                SetPropertyValue("HangHoa", ref _HangHoa, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        public Report_DonHangMua_BaoCaoHangMucMuaSam(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);

            DateTime ngay = Common.GetServerCurrentTime();

            TuNgay = new DateTime(ngay.Year, 1, 1);
            DenNgay = TuNgay.AddMonths(3);

            //Hỗ trợ xuất report theo Quý
            if (4 <= ngay.Month && ngay.Month < 7)
            {
                TuNgay = new DateTime(ngay.Year, 4, 1);
                DenNgay = TuNgay.AddMonths(3);
            }
            else if (7 <= ngay.Month && ngay.Month < 10)
            {
                TuNgay = new DateTime(ngay.Year, 7, 1);
                DenNgay = TuNgay.AddMonths(3);
            }
            else if (10 <= ngay.Month && ngay.Month <= 12)
            {
                TuNgay = new DateTime(ngay.Year, 10, 1);
                DenNgay = TuNgay.AddMonths(3);
            }
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@TuNgay", TuNgay);
            param[1] = new SqlParameter("@DenNgay", DenNgay);
            param[2] = new SqlParameter("@HangHoa", HangHoa.Oid);
            param[3] = new SqlParameter("@CongTy", CongTy.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_DonHangMua_BaoCaoHangMucMuaSam", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
