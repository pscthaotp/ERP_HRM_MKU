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
using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Extends;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bảng phân bổ doanh thu - Học phí")]
    public class Report_HocPhi_BangPhanBoDoanhThu : StoreProcedureReport, ICongTy
    {
        // Fields...
        private NamHoc _NamHoc;
        private CongTy _CongTy;
        private LoaiPhi _LoaiPhi;

        [ModelDefault("Caption", "Năm học")]
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

        [ModelDefault("Caption", "Trường")]
        [DataSourceCriteria("LoaiTruong = 1")]
        [ImmediatePostData]
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
        [ModelDefault("Caption", "Loại phí")]
        [DataSourceCriteria("NhomDinhPhi = 0 and NgungSuDung <> 1")]
        public LoaiPhi LoaiPhi
        {
            get
            {
                return _LoaiPhi;
            }
            set
            {
                SetPropertyValue("LoaiPhi", ref _LoaiPhi, value);
            }
        }
    
        public Report_HocPhi_BangPhanBoDoanhThu(Session session) : base(session) { }

  
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
            parameter[1] = new SqlParameter("@CongTy", CongTy.Oid);
            parameter[2] = new SqlParameter("@LoaiPhi", LoaiPhi != null ? LoaiPhi.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocPhi_BangTinhPhanBoDoanhThu", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
