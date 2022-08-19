using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.Systems;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.HocSinh.Lops;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.TKB.ChuongTrinhNgoaiKhoa;
using ERP.Module.DanhMuc.HocPhi;

namespace ERP.Module.Report.HocSinh.HocSinh
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Theo tổng hợp ngoại khóa - Học sinh")]
    public class Report_HocSinh_TheoDoiMonNgoaiKhoa_TongHop : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private NamHoc _NamHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }
        public Report_HocSinh_TheoDoiMonNgoaiKhoa_TongHop(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CongTy", CongTy.Oid);
            parameter[1] = new SqlParameter("@NamHoc", NamHoc.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocSinh_TheoDoiMonNgoaiKhoa_TongHop", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }
    }

}
