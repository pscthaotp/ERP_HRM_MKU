using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.DanhMuc.NhanSu
{
    [ImageName("BO_CreditCard")]
    [DefaultProperty("SoTaiKhoan")]
    [ModelDefault("Caption", "Tài khoản ngân hàng")]
    public class TaiKhoanNganHang : BaseObject
    {
        private bool _TaiKhoanChinh;
        private CongTy _CongTy;
        private NhanVien _NhanVien;
        private string _SoTaiKhoan;
        private NganHang _NganHang;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("NhanVien-ListTaiKhoanNganHang")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin Trường")]
        [Association("CongTy-ListTaiKhoanNganHang")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string SoTaiKhoan
        {
            get
            {
                return _SoTaiKhoan;
            }
            set
            {
                SetPropertyValue("SoTaiKhoan", ref _SoTaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Ngân hàng")]      
        [RuleRequiredField(DefaultContexts.Save)]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
            set
            {
                SetPropertyValue("NganHang", ref _NganHang, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản chính")]
        public bool TaiKhoanChinh
        {
            get
            {
                return _TaiKhoanChinh;
            }
            set
            {
                SetPropertyValue("TaiKhoanChinh", ref _TaiKhoanChinh, value);
            }
        }

        public TaiKhoanNganHang(Session session) : base(session) { }

    }
}
