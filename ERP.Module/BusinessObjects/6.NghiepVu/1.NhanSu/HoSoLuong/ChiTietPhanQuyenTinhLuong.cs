using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;

namespace ERP.Module.NghiepVu.NhanSu.HoSoLuong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Chi tiết phân quyền tính lương")]
    [DefaultProperty("CongThucTinhLuong.DienGiai")]
    [ImageName("BO_Category")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietPhanQuyenTinhLuong.Unique", DefaultContexts.Save, "PhanQuyenTinhLuong;CongThucTinhLuong")]
    public class ChiTietPhanQuyenTinhLuong : BaseObject
    {
        private CongThucTinhLuong _CongThucTinhLuong;
        private PhanQuyenTinhLuong _PhanQuyenTinhLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Phân quyền tính lương")]
        [Association("PhanQuyenTinhLuong-ListChiTietPhanQuyenTinhLuong")]
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

        [ModelDefault("Caption", "Công thức tính lương")]
        public CongThucTinhLuong CongThucTinhLuong
        {
            get
            {
                return _CongThucTinhLuong;
            }
            set
            {
                SetPropertyValue("CongThucTinhLuong", ref _CongThucTinhLuong, value);
            }
        }

        public ChiTietPhanQuyenTinhLuong(Session session):base(session)
        {
        }
    }
}
