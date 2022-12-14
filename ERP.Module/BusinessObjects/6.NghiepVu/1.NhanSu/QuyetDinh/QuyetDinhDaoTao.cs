using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.DaoTao;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    //Lưu ý: Khi tạo quyết định đào tạo thì không tạo quá trình đào tạo do:
    // chưa nhận được bằng. chỉ tạo quá trình đào tạo trong quyết định công
    // nhận đào tạo. Khi xóa quyết định đào tạo thì kiểm tra xem có quyết
    // định công nhận đào tạo và quá trình đào tạo không, nếu có thì xóa.
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định đào tạo")]
    public class QuyetDinhDaoTao : QuyetDinh
    {
        private DuyetDangKyDaoTao _DuyetDangKyDaoTao;
        private ChuongTrinhDaoTao _ChuongTrinhDaoTao;
        private DateTime _DenNgay;
        private DateTime _TuNgay;       
        private NguonKinhPhi _NguonKinhPhi;
        private string _TruongHoTro;
        private QuocGia _QuocGia;       
        private TruongDaoTao _TruongDaoTao;
        private decimal _TongChiPhi;
        private string _ThoiGianDaoTao;      

        [ImmediatePostData]
        [ModelDefault("Caption", "Đăng ký đào tạo")]
        public DuyetDangKyDaoTao DuyetDangKyDaoTao
        {
            get
            {
                return _DuyetDangKyDaoTao;
            }
            set
            {
                SetPropertyValue("DuyetDangKyDaoTao", ref _DuyetDangKyDaoTao, value);
                if (!IsLoading && value != null && value.DangKyDaoTao != null)
                {
                    ChuongTrinhDaoTao = value.ChuongTrinhDaoTao;
                    TruongDaoTao = value.TruongDaoTao;
                    QuocGia = value.QuocGia;
                    NguonKinhPhi = value.NguonKinhPhi;
                    TuNgay = value.DuKienTuNgay;
                    DenNgay = value.DuKienDenNgay;
                    TongChiPhi = value.TongChiPhiDuKien;
                }
            }
        }

        [ModelDefault("Caption", "Chương trình đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChuongTrinhDaoTao ChuongTrinhDaoTao
        {
            get
            {
                return _ChuongTrinhDaoTao;
            }
            set
            {
                SetPropertyValue("ChuongTrinhDaoTao", ref _ChuongTrinhDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Nguồn kinh phí")]
        public NguonKinhPhi NguonKinhPhi
        {
            get
            {
                return _NguonKinhPhi;
            }
            set
            {
                SetPropertyValue("NguonKinhPhi", ref _NguonKinhPhi, value);
            }
        }

        [ModelDefault("Caption", "Trường hỗ trợ")]
        public string TruongHoTro
        {
            get
            {
                return _TruongHoTro;
            }
            set
            {
                SetPropertyValue("TruongHoTro", ref _TruongHoTro, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
                if (!IsLoading)
                {
                    UpdateTruongList();
                    TruongDaoTao = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường đào tạo")]
        [DataSourceProperty("TruongList")]        
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

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                DenNgay = value;
                if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                {
                    ThoiGianDaoTao = string.Format("Từ {0:d} đến {1:d}", TuNgay, DenNgay);
                }
                else if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue)
                {
                    ThoiGianDaoTao = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                {
                    ThoiGianDaoTao = string.Format("Từ {0:d} đến {1:d}", TuNgay, DenNgay);
                }
                else if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue)
                {
                    ThoiGianDaoTao = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thời gian đào tạo")]
        public string ThoiGianDaoTao
        {
            get
            {
                return _ThoiGianDaoTao;
            }
            set
            {
                SetPropertyValue("ThoiGianDaoTao", ref _ThoiGianDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Tổng chi phí")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public decimal TongChiPhi
        {
            get
            {
                return _TongChiPhi;
            }
            set
            {
                SetPropertyValue("TongChiPhi", ref _TongChiPhi, value);
            }
        }

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách nhân viên")]
        [Association("QuyetDinhDaoTao-ListChiTietQuyetDinhDaoTao")]
        public XPCollection<ChiTietQuyetDinhDaoTao> ListChiTietQuyetDinhDaoTao
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhDaoTao>("ListChiTietQuyetDinhDaoTao");
            }
        }

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Lịch đào tạo")]
        [Association("QuyetDinhDaoTao-ListLichDaoTao")]
        public XPCollection<LichDaoTao> ListLichDaoTao
        {
            get
            {
                return GetCollection<LichDaoTao>("ListLichDaoTao");
            }
        }

        public QuyetDinhDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            QuocGia = Common.GetQuocGia(Session, "việt nam");
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhDaoTao;
            //
            QuyetDinhMoi = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateTruongList();
        }

        [Browsable(false)]
        public XPCollection<TruongDaoTao> TruongList { get; set; }

        private void UpdateTruongList()
        {
            if (TruongList == null)
                TruongList = new XPCollection<TruongDaoTao>(Session);
            TruongList.Criteria = CriteriaOperator.Parse("QuocGia=?", QuocGia);
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }
}
