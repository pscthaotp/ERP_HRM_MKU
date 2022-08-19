using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.GiayTo;

namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_Resume")]
    [DefaultProperty("HoTen")]
    [ModelDefault("Caption", "Hồ sơ")]
    [Appearance("HoSo.TruongNuoc", TargetItems = "SoHoChieu;NgayCapHoChieu;NoiCapHoChieu;", Visibility = ViewItemVisibility.Hide, Criteria = "QuocTich.TenQuocGia like '%Việt Nam%'")]
    [Appearance("HoSo.NgoaiNuoc", TargetItems = "CMND;NgayCap;NoiCap", Visibility = ViewItemVisibility.Hide, Criteria = "QuocTich.TenQuocGia not like '%Việt Nam%'")]
    [RuleCombinationOfPropertiesIsUnique("HoSo.Unique1", DefaultContexts.Save, "CMND;OidHoSoCha", "Nhân viên đã tồn tại trong hệ thống. Liên hệ quản trị hệ thống HRM EDU !", TargetCriteria = "OidHoSoCha='00000000-0000-0000-0000-000000000000'")]
    [RuleCombinationOfPropertiesIsUnique("HoSo.Unique2", DefaultContexts.Save, "SoHoChieu;OidHoSoCha", "Nhân viên đã tồn tại trong hệ thống. Liên hệ quản trị hệ thống HRM EDU !", TargetCriteria = "OidHoSoCha='00000000-0000-0000-0000-000000000000'")]    
    public class HoSo : BaseObject
    {
        private string _MaTapDoan;
        private string _MaNhanVien;
        private string _MaHoSo;
        private string _Ho;
        private string _Ten;
        private string _TenGoiKhac;
        private GioiTinhEnum _GioiTinh;
        private DateTime _NgaySinh;
        private DiaChi _NoiSinh;
        private string _CMND;
        private DateTime _NgayCap;
        private TinhThanh _NoiCap;
        private DiaChi _QueQuan;
        private DiaChi _DiaChiThuongTru;
        private DiaChi _NoiOHienNay;
        private string _Email;
        private string _EmailNoiBo;
        private string _DienThoaiDiDong;
        private string _DienThoaiNhaRieng;
        private string _DienThoaiNoiBo;
        private QuocGia _QuocTich;
        private TinhTrangHonNhan _TinhTrangHonNhan;
        private DanToc _DanToc;
        private TonGiao _TonGiao;
        private SucKhoe _SucKhoe;
        private int _CanNang;
        private int _ChieuCao;
        private NhomMau _NhomMau;
        private DateTime _NgayHetHan;
        private string _NoiCapHoChieu;
        private DateTime _NgayCapHoChieu;
        private string _SoHoChieu;
        private string _GhiChu;
        private HinhThucTuyenDungEnum _HinhThucTuyenDung;
        private LoaiHoSoEnum _LoaiHoSo;
        private bool _CheckUngVien = false;
        //
        //Khóa ngoại để biết hồ sơ được copy từ hồ sơ cha nào
        private Guid _OidHoSoCha;

        [ModelDefault("Caption", "Mã tập đoàn")]
        //[RuleUniqueValue("", DefaultContexts.Save, TargetCriteria = "OidHoSoCha is null")]
        public string MaTapDoan
        {
            get
            {
                return _MaTapDoan;
            }
            set
            {
                SetPropertyValue("MaTapDoan", ref _MaTapDoan, value);
            }
        }

        [ModelDefault("Caption", "Mã nhân sự")]
        [ModelDefault("DisplayFormat", "#####")]
        [ModelDefault("EditMask", "#####")]
        //[RuleUniqueValue("", DefaultContexts.Save)]
        [ModelDefault("AllowEdit","False")]
        public string MaNhanVien
        {
            get
            {
                return _MaNhanVien;
            }
            set
            {
                SetPropertyValue("MaNhanVien", ref _MaNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Mã hồ sơ")]
        //[RuleUniqueValue("", DefaultContexts.Save)]
        public string MaHoSo
        {
            get
            {
                return _MaHoSo;
            }
            set
            {
                SetPropertyValue("MaHoSo", ref _MaHoSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Họ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Ho
        {
            get
            {
                return _Ho;
            }
            set
            {
                SetPropertyValue("Ho", ref _Ho, value);
                OnChanged("HoTen");
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                SetPropertyValue("Ten", ref _Ten, value);
                OnChanged("HoTen");
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Họ và tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string HoTen
        {
            get
            {
                return String.Format("{0} {1}", Ho, Ten);
            }
        }

        [ModelDefault("Caption", "Tên gọi khác")]
        public string TenGoiKhac
        {
            get
            {
                return _TenGoiKhac;
            }
            set
            {
                SetPropertyValue("TenGoiKhac", ref _TenGoiKhac, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Nơi sinh")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "LoaiHoSo=0 OR LoaiHoSo is null")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiSinh
        {
            get
            {
                return _NoiSinh;
            }
            set
            {
                SetPropertyValue("NoiSinh", ref _NoiSinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
                if (!IsLoading)
                    AfterGioiTinhChanged();
            }
        }

        [ModelDefault("Caption", "Số CMND*")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "QuocTich.TenQuocGia like '%Việt Nam%'", SkipNullOrEmptyValues = false)]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp*")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "(LoaiHoSo=0 OR LoaiHoSo is null) and QuocTich.TenQuocGia like '%Việt Nam%'")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
                if (!IsLoading)
                {
                    if (value != DateTime.MinValue)
                        NgayHetHan = value.AddYears(15);
                    else
                        NgayHetHan = DateTime.MinValue;
                }
            }
        }

        [ModelDefault("Caption", "Nơi cấp*")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "(LoaiHoSo=0 OR LoaiHoSo is null) and QuocTich.TenQuocGia like '%Việt Nam%'")]
        public TinhThanh NoiCap
        {
            get
            {
                return _NoiCap;
            }
            set
            {
                SetPropertyValue("NoiCap", ref _NoiCap, value);
            }
        }

        [ModelDefault("Caption", "Số hộ chiếu*")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "QuocTich.TenQuocGia not like '%Việt Nam%'", SkipNullOrEmptyValues = false)]
        public string SoHoChieu
        {
            get
            {
                return _SoHoChieu;
            }
            set
            {
                SetPropertyValue("SoHoChieu", ref _SoHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp hộ chiếu*")]
        public DateTime NgayCapHoChieu
        {
            get
            {
                return _NgayCapHoChieu;
            }
            set
            {
                SetPropertyValue("NgayCapHoChieu", ref _NgayCapHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp hộ chiếu*")]
        public string NoiCapHoChieu
        {
            get
            {
                return _NoiCapHoChieu;
            }
            set
            {
                SetPropertyValue("NoiCapHoChieu", ref _NoiCapHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn*")]
        public DateTime NgayHetHan
        {
            get
            {
                return _NgayHetHan;
            }
            set
            {
                SetPropertyValue("NgayHetHan", ref _NgayHetHan, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Quê quán")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi QueQuan
        {
            get
            {
                return _QueQuan;
            }
            set
            {
                SetPropertyValue("QueQuan", ref _QueQuan, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Địa chỉ thường trú")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi DiaChiThuongTru
        {
            get
            {
                return _DiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru", ref _DiaChiThuongTru, value);
                if(!IsLoading)
                {
                    if (value != null)
                    {
                        //Nơi ở hiện nay
                        CreateNoiOHienNay(value);
                        //Quê quán
                        CreateQueQuan(value);
                    }
                }
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Nơi ở hiện nay")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiHoSo=0 OR LoaiHoSo is null")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiOHienNay
        {
            get
            {
                return _NoiOHienNay;
            }
            set
            {
                SetPropertyValue("NoiOHienNay", ref _NoiOHienNay, value);
            }
        }

        [ModelDefault("Caption", "Email cá nhân")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        [ModelDefault("Caption", "Email nội bộ")]
        public string EmailNoiBo
        {
            get
            {
                return _EmailNoiBo;
            }
            set
            {
                SetPropertyValue("EmailNoiBo", ref _EmailNoiBo, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong
        {
            get
            {
                return _DienThoaiDiDong;
            }
            set
            {
                SetPropertyValue("DienThoaiDiDong", ref _DienThoaiDiDong, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại nhà riêng")]
        public string DienThoaiNhaRieng
        {
            get
            {
                return _DienThoaiNhaRieng;
            }
            set
            {
                SetPropertyValue("DienThoaiNhaRieng", ref _DienThoaiNhaRieng, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại nội bộ")]
        public string DienThoaiNoiBo
        {
            get
            {
                return _DienThoaiNoiBo;
            }
            set
            {
                SetPropertyValue("DienThoaiNoiBo", ref _DienThoaiNoiBo, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng hôn nhân")]
        public TinhTrangHonNhan TinhTrangHonNhan
        {
            get
            {
                return _TinhTrangHonNhan;
            }
            set
            {
                SetPropertyValue("TinhTrangHonNhan", ref _TinhTrangHonNhan, value);
            }
        }

        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc
        {
            get
            {
                return _DanToc;
            }
            set
            {
                SetPropertyValue("DanToc", ref _DanToc, value);
            }
        }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao
        {
            get
            {
                return _TonGiao;
            }
            set
            {
                SetPropertyValue("TonGiao", ref _TonGiao, value);
            }
        }

        [ModelDefault("Caption", "Sức khỏe")]
        public SucKhoe SucKhoe
        {
            get
            {
                return _SucKhoe;
            }
            set
            {
                SetPropertyValue("SucKhoe", ref _SucKhoe, value);
            }
        }

        [ModelDefault("Caption", "Chiều cao (Cm)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int ChieuCao
        {
            get
            {
                return _ChieuCao;
            }
            set
            {
                SetPropertyValue("ChieuCao", ref _ChieuCao, value);
            }
        }

        [ModelDefault("Caption", "Cân nặng (Kg)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int CanNang
        {
            get
            {
                return _CanNang;
            }
            set
            {
                SetPropertyValue("CanNang", ref _CanNang, value);
            }
        }

        [ModelDefault("Caption", "Nhóm máu")]
        public NhomMau NhomMau
        {
            get
            {
                return _NhomMau;
            }
            set
            {
                SetPropertyValue("NhomMau", ref _NhomMau, value);
            }
        }       

        [ImmediatePostData]
        [ModelDefault("Caption", "Quốc tịch")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuocGia QuocTich
        {
            get
            {
                return _QuocTich;
            }
            set
            {
                SetPropertyValue("QuocTich", ref _QuocTich, value);
            }
        }

        [Size(4000)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Oid Hồ sơ cha")]
        public Guid OidHoSoCha
        {
            get
            {
                return _OidHoSoCha;
            }
            set
            {
                SetPropertyValue("OidHoSoCha", ref _OidHoSoCha, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Giấy tờ")]
        [Association("HoSo-ListGiayToHoSo")]
        public XPCollection<GiayToHoSo> ListGiayToHoSo
        {
            get
            {
                return GetCollection<GiayToHoSo>("ListGiayToHoSo");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách văn bằng")]
        [Association("HoSo-ListVanBang")]
        public XPCollection<VanBang> ListVanBang
        {
            get
            {
                return GetCollection<VanBang>("ListVanBang");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách chứng chỉ")]
        [Association("HoSo-ListChungChi")]
        public XPCollection<ChungChi> ListChungChi
        {
            get
            {
                return GetCollection<ChungChi>("ListChungChi");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách ngoại ngữ")]
        [Association("HoSo-ListNgoaiNgu")]
        public XPCollection<TrinhDoNgoaiNguKhac> ListNgoaiNgu
        {
            get
            {
                return GetCollection<TrinhDoNgoaiNguKhac>("ListNgoaiNgu");
            }
        }

        [ModelDefault("Caption", "Hình thức tuyển dụng")]
        public HinhThucTuyenDungEnum HinhThucTuyenDung
        {
            get
            {
                return _HinhThucTuyenDung;
            }
            set
            {
                SetPropertyValue("HinhThucTuyenDung", ref _HinhThucTuyenDung, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Loại hồ sơ")]
        public LoaiHoSoEnum LoaiHoSo
        {
            get
            {
                return _LoaiHoSo;
            }
            set
            {
                SetPropertyValue("LoaiHoSo", ref _LoaiHoSo, value);
            }
        }

        [Browsable(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Check ứng viên")]
        public bool CheckUngVien
        {
            get
            {
                return _CheckUngVien;
            }
            set
            {
                SetPropertyValue("CheckUngVien", ref _CheckUngVien, value);
            }
        }

        public HoSo(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NoiSinh = new DiaChi(Session);
            QueQuan = new DiaChi(Session);
            DiaChiThuongTru = new DiaChi(Session);
            NoiOHienNay = new DiaChi(Session);
            GioiTinh = GioiTinhEnum.Nam;
            DanToc = Session.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc like ?", "%Kinh%"));
            TonGiao = Session.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao like ?", "Không"));
            QuocTich = Session.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia like ?", "việt nam"));
            HinhThucTuyenDung = HinhThucTuyenDungEnum.ThiTuyen;            
        }

        protected virtual void AfterGioiTinhChanged()
        { }

        void CreateNoiOHienNay(DiaChi diaChiThuongTru)
        {
            if (NoiOHienNay == null)
                NoiOHienNay = new DiaChi(Session);
            //
            NoiOHienNay.QuocGia = Session.GetObjectByKey<QuocGia>(diaChiThuongTru.QuocGia != null ? diaChiThuongTru.QuocGia.Oid : Guid.Empty);
            NoiOHienNay.TinhThanh = Session.GetObjectByKey<TinhThanh>(diaChiThuongTru.TinhThanh != null ?  diaChiThuongTru.TinhThanh.Oid : Guid.Empty);
            NoiOHienNay.QuanHuyen = Session.GetObjectByKey<QuanHuyen>(diaChiThuongTru.QuanHuyen != null ? diaChiThuongTru.QuanHuyen.Oid : Guid.Empty);
            NoiOHienNay.XaPhuong = Session.GetObjectByKey<XaPhuong>(diaChiThuongTru.XaPhuong != null ?  diaChiThuongTru.XaPhuong.Oid : Guid.Empty);
            NoiOHienNay.FullDiaChi = diaChiThuongTru.FullDiaChi;
        }
        void CreateQueQuan(DiaChi diaChiThuongTru)
        {
            if (QueQuan == null)
                QueQuan = new DiaChi(Session);
            //
            QueQuan.QuocGia = Session.GetObjectByKey<QuocGia>(diaChiThuongTru.QuocGia != null ? diaChiThuongTru.QuocGia.Oid : Guid.Empty);
            QueQuan.TinhThanh = Session.GetObjectByKey<TinhThanh>(diaChiThuongTru.TinhThanh != null ? diaChiThuongTru.TinhThanh.Oid : Guid.Empty);
            QueQuan.QuanHuyen = Session.GetObjectByKey<QuanHuyen>(diaChiThuongTru.QuanHuyen != null ? diaChiThuongTru.QuanHuyen.Oid : Guid.Empty);
            QueQuan.XaPhuong = Session.GetObjectByKey<XaPhuong>(diaChiThuongTru.XaPhuong != null ? diaChiThuongTru.XaPhuong.Oid : Guid.Empty);
            QueQuan.FullDiaChi = diaChiThuongTru.FullDiaChi;
        }
    }
}
