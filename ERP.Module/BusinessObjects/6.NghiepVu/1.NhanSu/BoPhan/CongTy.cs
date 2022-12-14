using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Drawing;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.HeThong;
using ERP.Module.Enum.NhanSu;
using ERP.Module.CauHinhChungs;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Enum.TuyenSinh_PT;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.PMS.DanhMuc;

namespace ERP.Module.NghiepVu.NhanSu.BoPhans
{
    [ImageName("BO_Category")]
    [DefaultProperty("TenBoPhan")]
    [ModelDefault("Caption", "Thông tin Trường")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "MaQuanLy;TenBoPhan")]
    public class CongTy : BoPhan
    {
        private string _Email;
        private string _Fax;
        private string _WebSite;
        private int _NamThanhLap;
        private string _TenVietTat;
        private string _DonViChuQuan;
        private bool _SuDungPOS;
        private bool _ToChucThi;
        private string _MaSoThue;
        private string _SoGiayPhepKinhDoanh;
        private DiaChi _DiaChi;
        private string _DienThoai;
        private ThongTinChung _ThongTinChung;
        private HeDaoTao _HeDaoTao;
        private MocTinhThueTNCN _MocTinhThueTNCN;
        private MocQuyDoiThuNhapKhongThue _MocQuyDoiThuNhapKhongThue;
        private LoaiTruongEnum _LoaiTruong = LoaiTruongEnum.NA;
        //
        private string _URLLogo;

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Tên viết hoa")]
        public string TenVietHoa
        {
            get
            {
                if (!String.IsNullOrEmpty(TenBoPhan))
                    return TenBoPhan.ToUpper();
                return string.Empty;
            }
        }

        [ModelDefault("Caption", "Tên viết tắt")]
        public string TenVietTat
        {
            get
            {
                return _TenVietTat;
            }
            set
            {
                SetPropertyValue("TenVietTat", ref _TenVietTat, value);
            }
        }
        [ModelDefault("Caption", "Sử dụng POS")]
        public bool SuDungPOS
        {
            get
            {
                return _SuDungPOS;
            }
            set
            {
                SetPropertyValue("SuDungPOS", ref _SuDungPOS, value);
            }
        }
        [ModelDefault("Caption","Tổ chức thi")]
        public bool ToChucThi
        {
            get
            {
                return _ToChucThi;
            }
            set
            {
                SetPropertyValue("ToChucThi", ref _ToChucThi, value);
            }
        }
        [ModelDefault("Caption", "Đơn vị chủ quản")]
        public string DonViChuQuan
        {
            get
            {
                return _DonViChuQuan;
            }
            set
            {
                SetPropertyValue("DonViChuQuan", ref _DonViChuQuan, value);
            }
        }

        [ModelDefault("Caption", "Năm thành lập")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamThanhLap
        {
            get
            {
                return _NamThanhLap;
            }
            set
            {
                SetPropertyValue("NamThanhLap", ref _NamThanhLap, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Địa chỉ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi DiaChi
        {
            get
            {
                return _DiaChi;
            }
            set
            {
                SetPropertyValue("DiaChi", ref _DiaChi, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại")]
        public string DienThoai
        {
            get
            {
                return _DienThoai;
            }
            set
            {
                SetPropertyValue("DienThoai", ref _DienThoai, value);
            }
        }

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

        public string Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                SetPropertyValue("Fax", ref _Fax, value);
            }
        }

        public string WebSite
        {
            get
            {
                return _WebSite;
            }
            set
            {
                SetPropertyValue("WebSite", ref _WebSite, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Thông tin chung")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public ThongTinChung ThongTinChung
        {
            get
            {
                return _ThongTinChung;
            }
            set
            {
                SetPropertyValue("ThongTinChung", ref _ThongTinChung, value);
            }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get
            {
                return _HeDaoTao;
            }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return _MaSoThue;
            }
            set
            {
                SetPropertyValue("MaSoThue", ref _MaSoThue, value);
            }
        }

        [ModelDefault("Caption", "Số giấy phép KD")]
        public string SoGiayPhepKinhDoanh
        {
            get
            {
                return _SoGiayPhepKinhDoanh;
            }
            set
            {
                SetPropertyValue("SoGiayPhepKinhDoanh", ref _SoGiayPhepKinhDoanh, value);
            }
        }

        [ModelDefault("Caption", "Loại trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiTruongEnum LoaiTruong
        {
            get
            {
                return _LoaiTruong;
            }
            set
            {
                SetPropertyValue("LoaiTruong", ref _LoaiTruong, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Mốc tính thuế TNCN")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public MocTinhThueTNCN MocTinhThueTNCN
        {
            get
            {
                return _MocTinhThueTNCN;
            }
            set
            {
                SetPropertyValue("MocTinhThueTNCN", ref _MocTinhThueTNCN, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Mốc quy đổi TNKT")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public MocQuyDoiThuNhapKhongThue MocQuyDoiThuNhapKhongThue
        {
            get
            {
                return _MocQuyDoiThuNhapKhongThue;
            }
            set
            {
                SetPropertyValue("MocQuyDoiThuNhapKhongThue", ref _MocQuyDoiThuNhapKhongThue, value);
            }
        }

        [Browsable(false)]
        public string URLLogo
        {
            get
            {
                return _URLLogo;
            }
            set
            {
                SetPropertyValue("URLLogo", ref _URLLogo, value);
            }
        }

        [Delayed]
        [ModelDefault("Caption", "Logo")]
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(ImageValueConverter))]
        public Image Logo
        {
            get
            {
                return GetDelayedPropertyValue<Image>("Logo");
            }
            set
            {
                if (!IsLoading)
                {
                    //
                    if (string.IsNullOrEmpty(MaQuanLy))
                    {
                        DialogUtil.ShowError("Vui lòng lưu dữ liệu trước khi tải hình !!!");
                        value = null;
                        //
                        return;
                    }
                    //
                    string urlHinh = MaQuanLy + ".png";
                    if (UploadImage.UploadImageToServer(value, urlHinh, 5) && value != null)
                    {
                        URLLogo = urlHinh;
                    }
                    else
                    {
                        value = null;
                        URLLogo = "";
                    }
                }

                if (value != null)
                    SetDelayedPropertyValue<Image>("Logo", new Bitmap(value));
                else
                    SetDelayedPropertyValue<Image>("Logo", value);

            }
        }

        [Association("ThongTinChung-ListNguoiSuDung")]
        [ModelDefault("Caption", "Danh sách người dùng")]
        public XPCollection<SecuritySystemUser_Custom> ListNguoiSuDung
        {
            get
            {
                return GetCollection<SecuritySystemUser_Custom>("ListNguoiSuDung");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Tài khoản ngân hàng")]
        [Association("CongTy-ListTaiKhoanNganHang")]
        public XPCollection<TaiKhoanNganHang> ListTaiKhoanNganHang
        {
            get
            {
                return GetCollection<TaiKhoanNganHang>("ListTaiKhoanNganHang");
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public CauHinhChung CauHinhChung
        {
            get
            {
                CriteriaOperator filter = CriteriaOperator.Parse("CongTy=? ", Oid);
                //
                return Session.FindObject<CauHinhChung>(filter);
            }
        }

        public CongTy(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            LoaiBoPhan = LoaiBoPhanEnum.CongTy;
            //
            LoaiTruong = LoaiTruongEnum.NA;
        }
    }
}
