using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Loại thu nhập khác")]
    [DefaultProperty("TenLoaiThuNhapKhac")]
    public class LoaiThuNhapKhac : BaseObject
    {
        private string _MaQuanLy;
        private ChiPhiTienLuong _ChiPhiTienLuong;
        private string _TenLoaiThuNhapKhac;
        private bool _NgungApDung;

        [ModelDefault("Caption", "Mã quản lý (không được sửa)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Chi phí tiền lương")]
        public ChiPhiTienLuong ChiPhiTienLuong
        {
            get
            {
                return _ChiPhiTienLuong;
            }
            set
            {
                SetPropertyValue("ChiPhiTienLuong", ref _ChiPhiTienLuong, value);
            }
        }

        [ModelDefault("Caption", "Tên loại thu nhập khác")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenLoaiThuNhapKhac
        {
            get
            {
                return _TenLoaiThuNhapKhac;
            }
            set
            {
                SetPropertyValue("TenLoaiThuNhapKhac", ref _TenLoaiThuNhapKhac, value);
            }
        }

        [ModelDefault("Caption", "Ngừng áp dụng")]
        public bool NgungApDung
        {
            get
            {
                return _NgungApDung;
            }
            set
            {
                SetPropertyValue("NgungApDung", ref _NgungApDung, value);
            }
        }

        public LoaiThuNhapKhac(Session session) : base(session) { }
    }

}
