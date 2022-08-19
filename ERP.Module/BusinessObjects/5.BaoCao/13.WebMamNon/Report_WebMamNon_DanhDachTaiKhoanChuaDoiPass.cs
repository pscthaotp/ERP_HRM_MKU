using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.WebMamNon
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "WebMamNon - Danh sách tài khoản chưa đổi pass")]
    public class Report_WebMamNon_DanhDachTaiKhoanChuaDoiPass : StoreProcedureReport
    {
        // Fields...
        private CongTy _CongTy;
        private Lop _Lop;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Công ty/Trường")]
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

        [ModelDefault("Caption", "Lớp")]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
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

        public Report_WebMamNon_DanhDachTaiKhoanChuaDoiPass(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[5];
            
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@TuNgay", TuNgay != DateTime.MinValue ? TuNgay.ToString("dd/MM/yyyy") : "");
            parameter[3] = new SqlParameter("@DenNgay", DenNgay != DateTime.MinValue ? DenNgay.ToString("dd/MM/yyyy") : "");
            parameter[4] = new SqlParameter("@Loai", "0");

            SqlCommand cmd = DataProvider.GetCommand("spd_Web_MamNon_DanhSachTaiKhoanChuaDoiPass", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
