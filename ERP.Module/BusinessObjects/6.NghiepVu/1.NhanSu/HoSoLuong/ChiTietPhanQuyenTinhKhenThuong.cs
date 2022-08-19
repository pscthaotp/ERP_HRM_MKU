using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.DanhMuc.NhanSu;

//
namespace ERP.Module.NghiepVu.NhanSu.HoSoLuong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Chi tiết phân quyền tính khen thưởng")]
    [DefaultProperty("LoaiKhenThuongPhucLoi.TenLoai")]
    [ImageName("BO_Category")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietPhanQuyenTinhKhenThuong.Unique", DefaultContexts.Save, "PhanQuyenTinhLuong;LoaiKhenThuongPhucLoi")]
    public class ChiTietPhanQuyenTinhKhenThuong : BaseObject
    {
        private LoaiKhenThuongPhucLoi _LoaiKhenThuongPhucLoi;
        private PhanQuyenTinhLuong _PhanQuyenTinhLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Phân quyền tính lương")]
        [Association("PhanQuyenTinhLuong-ListChiTietPhanQuyenTinhKhenThuong")]
        public PhanQuyenTinhLuong PhanQuyenTinhLuong
        {
            get
            {
                return _PhanQuyenTinhLuong;
            }
            set
            {
                SetPropertyValue("PhanQuyenTinhLuong", ref _PhanQuyenTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Công thức tính khen thưởng")]
        public LoaiKhenThuongPhucLoi LoaiKhenThuongPhucLoi
        {
            get
            {
                return _LoaiKhenThuongPhucLoi;
            }
            set
            {
                SetPropertyValue("LoaiKhenThuongPhucLoi", ref _LoaiKhenThuongPhucLoi, value);
            }
        }

        public ChiTietPhanQuyenTinhKhenThuong(Session session): base(session)
        {
        }
    }
}
