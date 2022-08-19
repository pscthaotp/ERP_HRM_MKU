using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.BepAn;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Recipe")]
    public class Report_BepAn_Recipe : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private NhomDinhDuong _NhomDinhDuong;
        private LoaiMonAn _LoaiMonAn;
        private MonAnThongTin _MonAnThongTin;
        private DateTime _Ngay;

        [ModelDefault("Caption", "Công ty/Trường")]
        [DataSourceCriteria("LoaiTruong = 1 or LoaiTruong = 2")]
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

        [ModelDefault("Caption", "Nhóm dinh dưỡng")]
        public NhomDinhDuong NhomDinhDuong
        {
            get
            {
                return _NhomDinhDuong;
            }
            set
            {
                SetPropertyValue("NhomDinhDuong", ref _NhomDinhDuong, value);
            }
        }

        [ModelDefault("Caption", "Loại món ăn")]
        public LoaiMonAn LoaiMonAn
        {
            get
            {
                return _LoaiMonAn;
            }
            set
            {
                SetPropertyValue("LoaiMonAn", ref _LoaiMonAn, value);
            }
        }

        [ModelDefault("Caption", "Món ăn")]
        public MonAnThongTin MonAnThongTin
        {
            get
            {
                return _MonAnThongTin;
            }
            set
            {
                SetPropertyValue("MonAnThongTin", ref _MonAnThongTin, value);
            }
        }
        
        [ModelDefault("Caption", "Ngày")]
        public DateTime Ngay
        {
            get
            {
                return _Ngay;
            }
            set
            {
                SetPropertyValue("Ngay", ref _Ngay, value);
            }
        }

        public Report_BepAn_Recipe(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //CongTy = Common.CongTy(Session);
            Ngay = Common.GetServerCurrentTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@NhomDinhDuong", NhomDinhDuong != null ? NhomDinhDuong.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@LoaiMonAn", LoaiMonAn != null ? LoaiMonAn.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@MonAnThongTin", MonAnThongTin != null ? MonAnThongTin.Oid : Guid.Empty);
            parameter[4] = new SqlParameter("@Ngay", Ngay != null && Ngay != DateTime.MinValue ? Ngay.ToString("dd/MM/yyyy") : "");
            parameter[5] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BepAn_Recipe", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
