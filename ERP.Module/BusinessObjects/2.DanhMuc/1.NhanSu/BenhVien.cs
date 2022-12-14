using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_BenhVien")]
    [ModelDefault("Caption", "Bệnh viện")]
    [DefaultProperty("TenBenhVien")]
    public class BenhVien : BaseObject
    {
        private string _DiaChi;
        private string _MaQuanLy;
        private string _TenBenhVien;
        private TinhThanh _TinhThanh;
        private string _GhiChu;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên bệnh viện")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBenhVien
        {
            get
            {
                return _TenBenhVien;
            }
            set
            {
                SetPropertyValue("TenBenhVien", ref _TenBenhVien, value);
            }
        }

        [ModelDefault("Caption", "Tỉnh/Thành phố")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TinhThanh TinhThanh
        {
            get
            {
                return _TinhThanh;
            }
            set
            {
                SetPropertyValue("TinhThanh", ref _TinhThanh, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ")]
        public string DiaChi
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

        [Size(200)]
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

        public BenhVien(Session session) : base(session) { }
    }

}
