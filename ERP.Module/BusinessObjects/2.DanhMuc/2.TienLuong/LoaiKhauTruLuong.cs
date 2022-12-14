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
    [DefaultProperty("TenLoaiKhauTru")]
    [ModelDefault("Caption", "Loại khấu trừ lương")]
    public class LoaiKhauTruLuong : BaseObject
    {
        private string _MaQuanLy;
        private ChiPhiTienLuong _ChiPhiTienLuong;
        private string _TenLoaiKhauTru;

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên loại khấu trừ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenLoaiKhauTru
        {
            get
            {
                return _TenLoaiKhauTru;
            }
            set
            {
                SetPropertyValue("TenLoaiKhauTru", ref _TenLoaiKhauTru, value);
            }
        }

        public LoaiKhauTruLuong(Session session) : base(session) { }
    }

}
