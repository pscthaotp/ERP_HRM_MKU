using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.Data.SqlClient;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Commons;
using DevExpress.Data.Filtering;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [DefaultProperty("HoTen")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Ứng viên")]
    [Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "CMND;QuanLyTuyenDung")]
    public class UngVien : HoSo
    { 
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool IsEnable = true;
        private string _SoBaoDanh;
        private string _KinhNghiem;
        private string _ChungChiKhac;
        private string _CoQuanCu;
        private int _NamTotNghiep;
        private bool _ThiNgoaiNgu;
        private bool _ChuyenCongTac;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private NhuCauTuyenDung _NhuCauTuyenDung;
        private DateTime _NgayDuTuyen;
        
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ChuyenNganhDaoTao _ChuyenNganhDaoTao;
        private TruongDaoTao _TruongDaoTao;
        private HinhThucDaoTao _HinhThucDaoTao;
        
        private TrinhDoVanHoa _TrinhDoVanHoa;
        private ChuongTrinhHoc _ChuongTrinhHoc;
        private TrinhDoTinHoc _TrinhDoTinHoc;
        private NgoaiNgu _NgoaiNgu;
        private TrinhDoNgoaiNgu _TrinhDoNgoaiNgu;      

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListUngVien")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thông tin nhân viên cũ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "OidHoSoCha != '00000000-0000-0000-0000-000000000000'")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    CapNhatHoSoUngVien();
                }
            }
        }

        [ModelDefault("Caption", "Số báo danh")]
        //[ModelDefault("AllowEdit", "False")]
        //[RuleUniqueValue("", DefaultContexts.Save)]
        public string SoBaoDanh
        {
            get
            {
                return _SoBaoDanh;
            }
            set
            {
                SetPropertyValue("SoBaoDanh", ref _SoBaoDanh, value);
            }
        }

        [ModelDefault("Caption", "Ngày dự tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayDuTuyen
        {
            get
            {
                return _NgayDuTuyen;
            }
            set
            {
                SetPropertyValue("NgayDuTuyen", ref _NgayDuTuyen, value);
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        [ModelDefault("AllowEdit", "False")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao
        {
            get
            {
                return _ChuyenNganhDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenNganhDaoTao", ref _ChuyenNganhDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return _TruongDaoTao;
            }
            set
            {
                SetPropertyValue("TruongDaoTao", ref _TruongDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _HinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTao", ref _HinhThucDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return _NamTotNghiep;
            }
            set
            {
                SetPropertyValue("NamTotNghiep", ref _NamTotNghiep, value);
            }
        }

        [ModelDefault("Caption", "Trình độ văn hóa")]
        public TrinhDoVanHoa TrinhDoVanHoa
        {
            get
            {
                return _TrinhDoVanHoa;
            }
            set
            {
                SetPropertyValue("TrinhDoVanHoa", ref _TrinhDoVanHoa, value);
            }
        }

        [ModelDefault("Caption", "Hiện đang theo học")]
        public ChuongTrinhHoc ChuongTrinhHoc
        {
            get
            {
                return _ChuongTrinhHoc;
            }
            set
            {
                SetPropertyValue("ChuongTrinhHoc", ref _ChuongTrinhHoc, value);
            }
        }

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc
        {
            get
            {
                return _TrinhDoTinHoc;
            }
            set
            {
                SetPropertyValue("TrinhDoTinHoc", ref _TrinhDoTinHoc, value);               
            }
        }

        [ModelDefault("Caption", "Ngoại ngữ chính")]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return _NgoaiNgu;
            }
            set
            {
                SetPropertyValue("NgoaiNgu", ref _NgoaiNgu, value);                
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ chính")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return _TrinhDoNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TrinhDoNgoaiNgu", ref _TrinhDoNgoaiNgu, value);
            }
        }      

        [ModelDefault("Caption", "Chứng chỉ khác")]
        public String ChungChiKhac
        {
            get
            {
                return _ChungChiKhac;
            }
            set
            {
                SetPropertyValue("ChungChiKhac", ref _ChungChiKhac, value);
            }
        }     

        [ModelDefault("Caption", "Cơ quan cũ")]
        public string CoQuanCu
        {
            get
            {
                return _CoQuanCu;
            }
            set
            {
                SetPropertyValue("CoQuanCu", ref _CoQuanCu, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Kinh nghiệm")]
        public string KinhNghiem
        {
            get
            {
                return _KinhNghiem;
            }
            set
            {
                SetPropertyValue("KinhNghiem", ref _KinhNghiem, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vị trí ứng tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListNhuCauTuyenDung")]
        public NhuCauTuyenDung NhuCauTuyenDung
        {
            get
            {
                return _NhuCauTuyenDung;
            }
            set
            {
                SetPropertyValue("NhuCauTuyenDung", ref _NhuCauTuyenDung, value);               
            }
        }

        [ModelDefault("Caption", "Chuyển công tác")]
        public bool ChuyenCongTac
        {
            get
            {
                return _ChuyenCongTac;
            }
            set
            {
                SetPropertyValue("ChuyenCongTac", ref _ChuyenCongTac, value);
            }
        }

        [ModelDefault("Caption", "Thi ngoại ngữ")]
        public bool ThiNgoaiNgu
        {
            get
            {
                return _ThiNgoaiNgu;
            }
            set
            {
                SetPropertyValue("ThiNgoaiNgu", ref _ThiNgoaiNgu, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public UngVien(Session session) : base(session) { }      

        protected override void OnLoaded()
        {
            base.OnLoaded();

            CriteriaOperator filter = CriteriaOperator.Parse("UngVien=? && GCRecord IS NULL", this.Oid);
            ChiTietVongTuyenDung chiTietVongTuyenDung = Session.FindObject<ChiTietVongTuyenDung>(filter);
            if (chiTietVongTuyenDung != null)
                IsEnable = false;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiHoSo = LoaiHoSoEnum.UngVien;
            GioiTinh = GioiTinhEnum.Nam;
            NgayDuTuyen = Common.GetServerCurrentTime();
            CheckUngVien = true;
            UpdateNhanVienList();
        }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = CriteriaOperator.Parse("TinhTrang.DaNghiViec and OidHoSoCha = '00000000-0000-0000-0000-000000000000'");

            if (!String.IsNullOrEmpty(CMND))
                NVList.Criteria = CriteriaOperator.Parse("TinhTrang.DaNghiViec and CMND like %?%", CMND);
        }

        private void CapNhatHoSoUngVien()
        {
            OidHoSoCha = ThongTinNhanVien.Oid;            
            Ho = ThongTinNhanVien.Ho;
            Ten = ThongTinNhanVien.Ten;
            TenGoiKhac = ThongTinNhanVien.TenGoiKhac;
            GioiTinh = ThongTinNhanVien.GioiTinh;
            NgaySinh = ThongTinNhanVien.NgaySinh;
            NoiSinh = ThongTinNhanVien.NoiSinh;
            QueQuan = ThongTinNhanVien.QueQuan;
            CMND = ThongTinNhanVien.CMND;
            NgayCap = ThongTinNhanVien.NgayCap;
            NoiCap = ThongTinNhanVien.NoiCap;
            NgayHetHan = ThongTinNhanVien.NgayHetHan;
            QuocTich = ThongTinNhanVien.QuocTich;
            DanToc = ThongTinNhanVien.DanToc;
            TonGiao = ThongTinNhanVien.TonGiao;
            CanNang = ThongTinNhanVien.CanNang;
            ChieuCao = ThongTinNhanVien.ChieuCao;
            NhomMau = ThongTinNhanVien.NhomMau;
            SucKhoe = ThongTinNhanVien.SucKhoe;
            TinhTrangHonNhan = ThongTinNhanVien.TinhTrangHonNhan;
            DiaChiThuongTru = ThongTinNhanVien.DiaChiThuongTru;
            NoiOHienNay = ThongTinNhanVien.NoiOHienNay;
            Email = ThongTinNhanVien.Email;
            DienThoaiDiDong = ThongTinNhanVien.DienThoaiDiDong;
            DienThoaiNhaRieng = ThongTinNhanVien.DienThoaiNhaRieng;

            TrinhDoChuyenMon = ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon;
            ChuyenNganhDaoTao =ThongTinNhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao;
            TruongDaoTao = ThongTinNhanVien.NhanVienTrinhDo.TruongDaoTao;
            HinhThucDaoTao = ThongTinNhanVien.NhanVienTrinhDo.HinhThucDaoTao;     
            TrinhDoVanHoa = ThongTinNhanVien.NhanVienTrinhDo.TrinhDoVanHoa;       
            TrinhDoTinHoc = ThongTinNhanVien.NhanVienTrinhDo.TrinhDoTinHoc;
            NgoaiNgu = ThongTinNhanVien.NhanVienTrinhDo.NgoaiNgu;
            TrinhDoNgoaiNgu = ThongTinNhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu;
            ChuyenCongTac = true;
            CoQuanCu = ThongTinNhanVien.CongTy.TenBoPhan;
            KinhNghiem = String.Concat("Đảm nhiệm vị trí ", ThongTinNhanVien.ChucDanh != null ? ThongTinNhanVien.ChucDanh.TenChucDanh : "", " tại ", ThongTinNhanVien.BoPhan.TenBoPhan, " - ", ThongTinNhanVien.CongTy.TenBoPhan);

            foreach (VanBang item in ThongTinNhanVien.ListVanBang)
            {
                VanBang vanBang = new VanBang(Session);
                vanBang.ChuyenNganhDaoTao = item.ChuyenNganhDaoTao;
                vanBang.DiemTrungBinh = item.DiemTrungBinh;
                vanBang.GhiChu = item.GhiChu;
                vanBang.HinhThucDaoTao = item.HinhThucDaoTao;
                vanBang.NamTotNghiep = item.NamTotNghiep;
                vanBang.NgayCapBang = item.NgayCapBang;
                vanBang.TrinhDoChuyenMon = item.TrinhDoChuyenMon;
                vanBang.TruongDaoTao = item.TruongDaoTao;
                vanBang.XepLoai = item.XepLoai;
                this.ListVanBang.Add(vanBang);
            }

            foreach (TrinhDoNgoaiNguKhac item in ThongTinNhanVien.ListNgoaiNgu)
            {
                TrinhDoNgoaiNguKhac ngoaiNguKhac = new TrinhDoNgoaiNguKhac(Session);
                ngoaiNguKhac.Diem = item.Diem;
                ngoaiNguKhac.GhiChu = item.GhiChu;
                ngoaiNguKhac.GiayToList = item.GiayToList;
                ngoaiNguKhac.NgayCap = item.NgayCap;
                ngoaiNguKhac.NgoaiNgu = item.NgoaiNgu;
                ngoaiNguKhac.NoiCap = item.NoiCap;
                ngoaiNguKhac.TrinhDoNgoaiNgu = item.TrinhDoNgoaiNgu;
                ngoaiNguKhac.XepLoai = item.XepLoai;
                this.ListNgoaiNgu.Add(ngoaiNguKhac);
            }

            foreach (ChungChi item in ThongTinNhanVien.ListChungChi)
            {
                ChungChi chungChi = new ChungChi(Session);
                chungChi.Diem = item.Diem;
                chungChi.GhiChu = item.GhiChu;
                chungChi.LoaiChungChi = item.LoaiChungChi;
                chungChi.NgayCap = item.NgayCap;
                chungChi.NoiCap = item.NoiCap;
                chungChi.TenChungChi = item.TenChungChi;
                chungChi.XepLoai = item.XepLoai;
                this.ListChungChi.Add(chungChi);
            }        
        }
    }

}
