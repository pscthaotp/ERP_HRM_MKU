using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NonPersistentObjects.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.DaoTao
{
    [ImageName("BO_QuanLyDaoTao")]
    [DefaultProperty("DangKyDaoTao")]
    [ModelDefault("Caption", "Duyệt đăng ký đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyDaoTao;DangKyDaoTao")]
    public class DuyetDangKyDaoTao : BaseObject
    {        
        private QuanLyDaoTao _QuanLyDaoTao;     
        private DangKyDaoTao _DangKyDaoTao;
        private ChuongTrinhDaoTao _ChuongTrinhDaoTao;
        private NguonKinhPhi _NguonKinhPhi;
        private QuocGia _QuocGia;
        private TruongDaoTao _TruongDaoTao;
        private DateTime _DuKienTuNgay;
        private DateTime _DuKienDenNgay;
        private decimal _TongChiPhiDuKien;           
        private string _GhiChu;

        [Browsable(false)]
        [Association("QuanLyDaoTao-ListDuyetDangKyDaoTao")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Đăng ký đào tạo")]
        public DangKyDaoTao DangKyDaoTao
        {
            get
            {
                return _DangKyDaoTao;
            }
            set
            {
                SetPropertyValue("DangKyDaoTao", ref _DangKyDaoTao, value);               
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
            }
        }
       
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách nhân viên")]
        [Association("DuyetDangKyDaoTao-ListChiTietDuyetDangKyDaoTao")]
        public XPCollection<ChiTietDuyetDangKyDaoTao> ListChiTietDuyetDangKyDaoTao
        {
            get
            {
                return GetCollection<ChiTietDuyetDangKyDaoTao>("ListChiTietDuyetDangKyDaoTao");
            }
        }

        public DuyetDangKyDaoTao(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();                   
        }       

        public bool IsExists(ThongTinNhanVien nhanVien)
        {
            foreach (ChiTietDuyetDangKyDaoTao item in ListChiTietDuyetDangKyDaoTao)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }

        public void CreateListChiTietDuyetDangKyDaoTao(DaoTao_ChonNhanVien item)
        {
            ChiTietDuyetDangKyDaoTao chiTiet = new ChiTietDuyetDangKyDaoTao(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietDuyetDangKyDaoTao.Add(chiTiet);
            //this.ListChiTietDuyetDangKyDaoTao.Reload();
        }
    }

}
