using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn nhân viên")]
    public class HoSo_ChonDanhSachNhanVien : BaseObject
    {
        private ChuongTrinhDaoTao _ChuongTrinhDaoTao;
        private NguonKinhPhi _NguonKinhPhi;
        private QuocGia _QuocGia;
        private TruongDaoTao _TruongDaoTao;
        private DateTime _DuKienTuNgay;
        private DateTime _DuKienDenNgay;
        private decimal _TongChiPhiDuKien;
        private string _GhiChu;

        [ModelDefault("Caption", "Chương trình đào tạo")]       
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Dự kiến từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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

        [ModelDefault("Caption", "Danh sách nhân viên")]
        public XPCollection<DaoTao_ChonNhanVien> ListNhanVien { get; set; }
       
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListNhanVien = new XPCollection<DaoTao_ChonNhanVien>(Session, false);
        }

        public HoSo_ChonDanhSachNhanVien(Session session) : base(session) { }
    }

}
