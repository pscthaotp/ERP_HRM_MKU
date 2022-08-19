using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.PMS.BusinessObjects;

namespace ERP.Module.PMS.CauHinh.HeSo
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    //[ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Quản lý hệ số")]
    public class QuanLyHeSo : ThongTinChungPMS
    {
        [Aggregated]
        [ModelDefault("Caption", "Hệ số giảng dạy ngoài giờ")]
        [Association("QuanLyHeSo-ListHeSoGiangDayNgoaiGio")]
        public XPCollection<HeSoGiangDay_NgoaiGio> ListHeSoGiangDayNgoaiGio
        {
            get
            {
                return GetCollection<HeSoGiangDay_NgoaiGio>("ListHeSoGiangDayNgoaiGio");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số lớp đông")]
        [Association("QuanLyHeSo-ListHeSoLopDong")]
        public XPCollection<HeSoLopDong> ListHeSoLopDong
        {
            get
            {
                return GetCollection<HeSoLopDong>("ListHeSoLopDong");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số tín chỉ")]
        [Association("QuanLyHeSo-ListHeSoTinChi")]
        public XPCollection<HeSoTinChi> ListHeSoTinChi
        {
            get
            {
                return GetCollection<HeSoTinChi>("ListHeSoTinChi");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số cơ sở")]
        [Association("QuanLyHeSo-ListHeSoCoSo")]
        public XPCollection<HeSoCoSo> ListHeSoCoSo
        {
            get
            {
                return GetCollection<HeSoCoSo>("ListHeSoCoSo");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số chức danh")]
        [Association("QuanLyHeSo-ListHeSoChucDanh")]
        public XPCollection<HeSoChucDanh> ListHeSoChucDanh
        {
            get
            {
                return GetCollection<HeSoChucDanh>("ListHeSoChucDanh");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Hệ số chức danh (Nhân viên)")]
        [Association("QuanLyHeSo-ListHeSo_ChucDanhNhanVien")]
        public XPCollection<HeSo_ChucDanhNhanVien> ListHeSo_ChucDanhNhanVien
        {
            get
            {
                return GetCollection<HeSo_ChucDanhNhanVien>("ListHeSo_ChucDanhNhanVien");
            }
        }
       
        [Aggregated]
        [ModelDefault("Caption", "Hệ số ngôn ngữ")]
        [Association("QuanLyHeSo-ListHeSoNgonNgu")]
        public XPCollection<HeSoNgonNgu> ListHeSoNgonNgu
        {
            get
            {
                return GetCollection<HeSoNgonNgu>("ListHeSoNgonNgu");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số ngành học")]
        [Association("QuanLyHeSo-ListHeSoNganhHoc")]
        public XPCollection<HeSoNganhHoc> ListHeSoNganhHoc
        {
            get
            {
                return GetCollection<HeSoNganhHoc>("ListHeSoNganhHoc");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số cố vấn học tập")]
        [Association("QuanLyHeSo-ListHeSoCoVanHocTap")]
        public XPCollection<HeSoCoVanHocTap> ListHeSoCoVanHocTap
        {
            get
            {
                return GetCollection<HeSoCoVanHocTap>("ListHeSoCoVanHocTap");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số bậc đào tạo")]
        [Association("QuanLyHeSo-ListHeSoBacDaoTao")]
        public XPCollection<HeSoBacDaoTao> ListHeSoBacDaoTao
        {
            get
            {
                return GetCollection<HeSoBacDaoTao>("ListHeSoBacDaoTao");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Hệ số gảng dạy theo dự án")]
        [Association("QuanLyHeSo-ListHeSoGiangDayTheoDuAn")]
        public XPCollection<HeSoGiangDayTheoDuAn> ListHeSoGiangDayTheoDuAn
        {
            get
            {
                return GetCollection<HeSoGiangDayTheoDuAn>("ListHeSoGiangDayTheoDuAn");
            }
        }

        public QuanLyHeSo(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
