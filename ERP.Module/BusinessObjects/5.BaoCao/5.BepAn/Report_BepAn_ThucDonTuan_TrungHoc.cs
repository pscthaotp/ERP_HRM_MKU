using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.BepAn;
using System.ComponentModel;
using DevExpress.Persistent.Validation;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Thực đơn tuần trung học")]
    public class Report_BepAn_ThucDonTuan_TrungHoc : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;
        private NhomDinhDuong _NhomDinhDuong;
        private NamHoc _NamHoc;
        private TuanHoc _TuanHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty/Trường")]
        [DataSourceCriteria("LoaiTruong = 1 or LoaiTruong = 2")]
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

        [ModelDefault("Caption", "Nhóm dinh dưỡng")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[Browsable(false)]
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
        
        [ModelDefault("Caption", "Tuần học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NamHoc.ListTuanHoc")]
        public TuanHoc TuanHoc
        {
            get
            {
                return _TuanHoc;
            }
            set
            {
                SetPropertyValue("TuanHoc", ref _TuanHoc, value);
            }
        }

   
        
        public Report_BepAn_ThucDonTuan_TrungHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@OID_TRUONG", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@Tuan",TuanHoc.SoThuTu);
            parameter[2] = new SqlParameter("@Khoi", NhomDinhDuong.MaQuanLy);
            parameter[3] = new SqlParameter("@TenNamHoc", NamHoc.TenNamHoc);

            SqlCommand cmd = DataProvider.GetCommand("spd_Web_PortalTrungHoc_ThucDonTuan", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
