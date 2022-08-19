using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.NghiepVu.NhanSu.DaoTao
{
    [ImageName("BO_QuanLyDaoTao")]
    [ModelDefault("Caption", "Đăng ký đào tạo")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique("DangKyDaoTao", DefaultContexts.Save, "QuanLyDaoTao;ChuongTrinhDaoTao;DuKienTuNgay")]
    public class DangKyDaoTao : BaseObject
    {
        // Fields...
        
        //private KhoaDaoTao _KhoaDaoTao;  
        private ChuongTrinhDaoTao _ChuongTrinhDaoTao;
        private NguonKinhPhi _NguonKinhPhi;         
        private QuocGia _QuocGia;
        private TruongDaoTao _TruongDaoTao;
        private DateTime _DuKienTuNgay;
        private DateTime _DuKienDenNgay;
        private decimal _TongChiPhiDuKien;
        private QuanLyDaoTao _QuanLyDaoTao;
        private string _GhiChu;

        [Browsable(false)]
        [Association("QuanLyDaoTao-ListDangKyDaoTao")]
        public QuanLyDaoTao QuanLyDaoTao
        {
            get
            {
                return _QuanLyDaoTao;
            }
            set
            {
                SetPropertyValue("QuanLyDaoTao", ref _QuanLyDaoTao, value);
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

        [ImmediatePostData]
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
                    //TruongDaoTao = null;
                    UpdateTruongDaoTaoList();
                }
            }
        }

        [DataSourceProperty("TruongList")]
        [ModelDefault("Caption", "Trường đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        //[ModelDefault("Caption", "Khóa đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong='LUH'")]
        //public KhoaDaoTao KhoaDaoTao
        //{
        //    get
        //    {
        //        return _KhoaDaoTao;
        //    }
        //    set
        //    {
        //        SetPropertyValue("KhoaDaoTao", ref _KhoaDaoTao, value);
        //    }
        //}

        [ModelDefault("Caption", "Nguồn kinh phí")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Dự kiến từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DuKienTuNgay
        {
            get
            {
                return _DuKienTuNgay;
            }
            set
            {
                SetPropertyValue("DuKienTuNgay", ref _DuKienTuNgay, value);             
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Dự kiến đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DuKienDenNgay
        {
            get
            {
                return _DuKienDenNgay;
            }
            set
            {
                SetPropertyValue("DuKienDenNgay", ref _DuKienDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Tổng chi phí dự kiến")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public decimal TongChiPhiDuKien
        {
            get
            {
                return _TongChiPhiDuKien;
            }
            set
            {
                SetPropertyValue("TongChiPhiDuKien", ref _TongChiPhiDuKien, value);
            }
        }

        [Size(300)]
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

        //[Browsable(false)]
        //public string Caption
        //{
        //    get
        //    {
        //        string trinhDo = TrinhDoChuyenMon != null ? TrinhDoChuyenMon.TenTrinhDoChuyenMon : "";
        //        string chuyenMon = ChuyenMonDaoTao != null ? ChuyenMonDaoTao.TenChuyenMonDaoTao : "";
        //        string truong = TruongDaoTao != null ? TruongDaoTao.TenTruongDaoTao : "";
        //        return string.Format("{0} {1} - {2}", trinhDo, chuyenMon, truong);
        //    }
        //}

        [Browsable(false)]
        public string Caption
        {
            get
            {
                string chuongtrinh = ChuongTrinhDaoTao != null ? ChuongTrinhDaoTao.TenChuongTrinh : "";                
                string truong = TruongDaoTao != null ? TruongDaoTao.TenTruongDaoTao : "";
                return string.Format("{0}-{1}", chuongtrinh, truong);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách nhân viên")]
        [Association("DangKyDaoTao-ListChiTietDangKyDaoTao")]
        public XPCollection<ChiTietDangKyDaoTao> ListChiTietDangKyDaoTao
        {
            get
            {
                return GetCollection<ChiTietDangKyDaoTao>("ListChiTietDangKyDaoTao");
            }
        }

        public DangKyDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //QuocGia = HamDungChung.GetCurrentQuocGia(Session);
        }

        [Browsable(false)]
        public XPCollection<TruongDaoTao> TruongList { get; set; }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateTruongDaoTaoList();

        }   

        private void UpdateTruongDaoTaoList()
        {
            if (TruongList == null)
                TruongList = new XPCollection<TruongDaoTao>(Session);
            TruongList.Criteria = CriteriaOperator.Parse("QuocGia=?", QuocGia);
        }

        public bool IsExists(ThongTinNhanVien nhanVien)
        {
            foreach (ChiTietDangKyDaoTao item in ListChiTietDangKyDaoTao)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }
    }

}
