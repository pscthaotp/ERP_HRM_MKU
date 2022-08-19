using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách học sinh - hàng hóa - Học phí")]
    public class Report_DongPhucHocPham_HocSinhHangHoa : StoreProcedureReport
    {
        // Fields...
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private bool _DaGiao;
        private CongTy _CongTy;

        [ModelDefault("Caption", "Từ ngày")]
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

        [ModelDefault("Caption", "Đã giao")]
        public bool DaGiao
        {
            get
            {
                return _DaGiao;
            }
            set
            {
                SetPropertyValue("DaGiao", ref _DaGiao, value);
            }
        }

        [ModelDefault("Caption", "Trường")]
        [DataSourceCriteria("LoaiTruong = 1")]
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

        public Report_DongPhucHocPham_HocSinhHangHoa(Session session) : base(session) { }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuNgay = NgayLapBaoCao.SetTime(Enum.Systems.SetTimeEnum.StartMonth);
            DenNgay = NgayLapBaoCao.SetTime(Enum.Systems.SetTimeEnum.EndMonth);
            CongTy = Common.CongTy(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@TuNgay", TuNgay);
            parameter[1] = new SqlParameter("@DenNgay", DenNgay);
            parameter[2] = new SqlParameter("@DaGiao", DaGiao);
            parameter[3] = new SqlParameter("@CongTy", CongTy.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_DongPhucHocPham_HocSinhHangHoa", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
