using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Enum.BepAn;
using ERP.Module.NghiepVu.BepAn.KhoBep;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Kết quả dinh dưỡng tuần - Đơn đặt hàng dự trù")]
    [Appearance("Enabled_SoLuong", TargetItems = "SoLuong", Enabled = false, Criteria = "LoaiChotSuatAn <> 0")]
    public class Report_BepAn_KhoBep_DonDatHangDuTru_KetQuaDinhDuongTuan : StoreProcedureReport
    {
        private Bep_DonDatHangDuTru _Bep_DonDatHangDuTru;
        
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private TuanHoc _TuanHoc;
        private string _NhomDinhDuong;
        private string _LoaiKhungGioTKB;
        private LoaiChotSuatAnTuChonEnum _LoaiChotSuatAn;
        private decimal _SoLuong;

        [ModelDefault("Caption", "Đơn đặt hàng dự trù")]
        [ImmediatePostData]
        public Bep_DonDatHangDuTru Bep_DonDatHangDuTru
        {
            get
            {
                return _Bep_DonDatHangDuTru;
            }
            set
            {
                SetPropertyValue("Bep_DonDatHangDuTru", ref _Bep_DonDatHangDuTru, value);
            }
        }
        
        [ModelDefault("Caption", "Công ty/Trường")]
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

        [ModelDefault("Caption", "Năm học")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    TuanHoc = null;
                }
            }
        }

        [ModelDefault("Caption", "Tuần học áp dụng")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
        [ModelDefault("Caption", "Nhóm dinh dưỡng")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.BepAn.chkComboxEdit_NhomDinhDuong")]
        public string NhomDinhDuong
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

        [ModelDefault("Caption", "Loại khung giờ")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.BepAn.chkComboxEdit_LoaiKhungGioTKB")]
        public string LoaiKhungGioTKB
        {
            get
            {
                return _LoaiKhungGioTKB;
            }
            set
            {
                SetPropertyValue("LoaiKhungGioTKB", ref _LoaiKhungGioTKB, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại chốt suất ăn")]
        public LoaiChotSuatAnTuChonEnum LoaiChotSuatAn
        {
            get
            {
                return _LoaiChotSuatAn;
            }
            set
            {
                SetPropertyValue("LoaiChotSuatAn", ref _LoaiChotSuatAn, value);
            }
        }

        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoLuong
        {
            get
            {
                return _SoLuong;
            }
            set
            {
                SetPropertyValue("SoLuong", ref _SoLuong, value);
            }
        }
        
        public Report_BepAn_KhoBep_DonDatHangDuTru_KetQuaDinhDuongTuan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = Common.GetCurrentNamHoc(Session);
            TuanHoc = Common.GetCurrentTuanHoc(Session);
            LoaiChotSuatAn = LoaiChotSuatAnTuChonEnum.DuTru;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[9];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@TuanHoc", TuanHoc != null ? TuanHoc.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@NhomDinhDuong", NhomDinhDuong);
            parameter[4] = new SqlParameter("@LoaiKhungGioTKB", LoaiKhungGioTKB);
            parameter[5] = new SqlParameter("@LoaiChot", LoaiChotSuatAn);
            parameter[6] = new SqlParameter("@SoLuong", SoLuong);
            parameter[7] = new SqlParameter("@Bep_DonDatHangDuTru", Bep_DonDatHangDuTru != null ? Bep_DonDatHangDuTru.Oid : Guid.Empty);
            parameter[8] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_KhoBep_DonDatHangDuTru_KetQuaDinhDuongTuan", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}
