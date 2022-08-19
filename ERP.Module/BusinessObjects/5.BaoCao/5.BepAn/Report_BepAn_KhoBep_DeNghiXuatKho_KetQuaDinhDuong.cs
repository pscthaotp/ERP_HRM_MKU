using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.BepAn;
using ERP.Module.Enum.BepAn;
using ERP.Module.NghiepVu.BepAn.KhoBep;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Kết quả dinh dưỡng - Đề nghị xuất kho")]
    [Appearance("Enabled_SoLuong", TargetItems = "SoLuong", Enabled = false, Criteria = "LoaiChotSuatAn <> 0")]
    public class Report_BepAn_KhoBep_DeNghiXuatKho_KetQuaDinhDuong : StoreProcedureReport
    {
        private Bep_DeNghiXuatKho _Bep_DeNghiXuatKho;

        private CongTy _CongTyLap;
        private DateTime _NgayLapTuNgay;
        private DateTime _NgayLapDenNgay;

        private CongTy _CongTyApDung;
        private DateTime _NgayApDungTuNgay;
        private DateTime _NgayApDungDenNgay;

        private LoaiChotSuatAnTuChonEnum _LoaiChotSuatAn;
        private decimal _SoLuong;
        private string _NhomDinhDuong;
        private string _LoaiKhungGioTKB;
        private MonAnThongTin _MonAnThongTin;
        private ThucPham _ThucPham;
        private LoaiThucPhamTatCaEnum _LoaiThucPham;
        private KhoChoTatCaEnum _KhoCho;

        [ModelDefault("Caption", "Đề nghị xuất kho")]
        [ImmediatePostData]
        public Bep_DeNghiXuatKho Bep_DeNghiXuatKho
        {
            get
            {
                return _Bep_DeNghiXuatKho;
            }
            set
            {
                SetPropertyValue("Bep_DeNghiXuatKho", ref _Bep_DeNghiXuatKho, value);
            }
        }

        [ModelDefault("Caption", "Công ty/Trường lập")]
        [ImmediatePostData]
        public CongTy CongTyLap
        {
            get
            {
                return _CongTyLap;
            }
            set
            {
                SetPropertyValue("CongTyLap", ref _CongTyLap, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày lập từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayLapTuNgay
        {
            get
            {
                return _NgayLapTuNgay;
            }
            set
            {
                SetPropertyValue("NgayLapTuNgay", ref _NgayLapTuNgay, value);
                if (!IsLoading) { NgayLapDenNgay = NgayLapTuNgay; }
            }
        }

        [ModelDefault("Caption", "Ngày lập đến ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayLapDenNgay
        {
            get
            {
                return _NgayLapDenNgay;
            }
            set
            {
                SetPropertyValue("NgayLapDenNgay", ref _NgayLapDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Công ty/Trường áp dụng")]
        [ImmediatePostData]
        public CongTy CongTyApDung
        {
            get
            {
                return _CongTyApDung;
            }
            set
            {
                SetPropertyValue("CongTyApDung", ref _CongTyApDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày áp dụng từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayApDungTuNgay
        {
            get
            {
                return _NgayApDungTuNgay;
            }
            set
            {
                SetPropertyValue("NgayApDungTuNgay", ref _NgayApDungTuNgay, value);
                if (!IsLoading) { NgayApDungDenNgay = NgayApDungTuNgay; }
            }
        }

        [ModelDefault("Caption", "Ngày áp dụng đến ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayApDungDenNgay
        {
            get
            {
                return _NgayApDungDenNgay;
            }
            set
            {
                SetPropertyValue("NgayApDungDenNgay", ref _NgayApDungDenNgay, value);
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

        [ModelDefault("Caption", "Thông tin món ăn")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Thực phẩm")]
        public ThucPham ThucPham
        {
            get
            {
                return _ThucPham;
            }
            set
            {
                SetPropertyValue("ThucPham", ref _ThucPham, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại thực phẩm")]
        public LoaiThucPhamTatCaEnum LoaiThucPham
        {
            get
            {
                return _LoaiThucPham;
            }
            set
            {
                SetPropertyValue("LoaiThucPham", ref _LoaiThucPham, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kho/Chợ")]
        public KhoChoTatCaEnum KhoCho
        {
            get
            {
                return _KhoCho;
            }
            set
            {
                SetPropertyValue("KhoCho", ref _KhoCho, value);
            }
        }

        public Report_BepAn_KhoBep_DeNghiXuatKho_KetQuaDinhDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiThucPham = LoaiThucPhamTatCaEnum.TatCa;
            KhoCho = KhoChoTatCaEnum.TatCa;
            LoaiChotSuatAn = LoaiChotSuatAnTuChonEnum.Nau;
            //NgayLapTuNgay = Common.GetServerCurrentTime();
            //NgayApDungTuNgay = Common.GetServerCurrentTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[16];
            parameter[0] = new SqlParameter("@CongTyLap", CongTyLap != null ? CongTyLap.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@NgayLapTuNgay", NgayLapTuNgay != null && NgayLapTuNgay != DateTime.MinValue ? NgayLapTuNgay.ToString("dd/MM/yyyy") : "");
            parameter[2] = new SqlParameter("@NgayLapDenNgay", NgayLapDenNgay != null && NgayLapDenNgay != DateTime.MinValue ? NgayLapDenNgay.ToString("dd/MM/yyyy") : "");
            parameter[3] = new SqlParameter("@CongTyApDung", CongTyApDung != null ? CongTyApDung.Oid : Guid.Empty);
            parameter[4] = new SqlParameter("@NgayApDungTuNgay", NgayApDungTuNgay != null && NgayApDungTuNgay != DateTime.MinValue ? NgayApDungTuNgay.ToString("dd/MM/yyyy") : "");
            parameter[5] = new SqlParameter("@NgayApDungDenNgay", NgayApDungDenNgay != null && NgayApDungDenNgay != DateTime.MinValue ? NgayApDungDenNgay.ToString("dd/MM/yyyy") : "");
            parameter[6] = new SqlParameter("@NhomDinhDuong", NhomDinhDuong);
            parameter[7] = new SqlParameter("@LoaiKhungGioTKB", LoaiKhungGioTKB);
            parameter[8] = new SqlParameter("@MonAnThongTin", MonAnThongTin != null ? MonAnThongTin.Oid : Guid.Empty);
            parameter[9] = new SqlParameter("@ThucPham", ThucPham != null ? ThucPham.Oid : Guid.Empty);
            parameter[10] = new SqlParameter("@LoaiThucPham", LoaiThucPham);
            parameter[11] = new SqlParameter("@Bep_DeNghiXuatKho", Bep_DeNghiXuatKho != null ? Bep_DeNghiXuatKho.Oid : Guid.Empty);
            parameter[12] = new SqlParameter("@KhoCho", KhoCho);
            parameter[13] = new SqlParameter("@LoaiChot", LoaiChotSuatAn);
            parameter[14] = new SqlParameter("@SoLuong", SoLuong);
            parameter[15] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_KhoBep_DeNghiXuatKho_KetQuaDinhDuong", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}
