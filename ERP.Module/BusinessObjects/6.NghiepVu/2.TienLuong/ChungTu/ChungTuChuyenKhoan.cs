using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.TienLuong.ChungTus
{
    [DefaultClassOptions]
    [DefaultProperty("SoChungTu")]
    [ImageName("BO_ChuyenKhoan")]
    [ModelDefault("Caption", "Chứng từ chuyển khoản")]
    [RuleCombinationOfPropertiesIsUnique("ChungTuChuyenKhoan.Unique", DefaultContexts.Save, "ChungTu;ThongTinNhanVien")]
    public class ChungTuChuyenKhoan : BaseObject, IBoPhan
    {
        //
        private ChungTu _ChungTu;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _SoTaiKhoanChuyen;
        private NganHang _NganHangChuyen;
        private NganHang _NganHangNhan;
        private string _SoTaiKhoanNhan;
        private decimal _ThuNhap;
        private decimal _ThueTNCN;
        private decimal _ThucNhan;

        [Browsable(false)]
        [Association("ChungTu-ListChungTuChuyenKhoan")]
        public ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    //
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngân hàng chuyển")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NganHang NganHangChuyen
        {
            get
            {
                return _NganHangChuyen;
            }
            set
            {
                SetPropertyValue("NganHangChuyen", ref _NganHangChuyen, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản chuyển")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string SoTaiKhoanChuyen
        {
            get
            {
                return _SoTaiKhoanChuyen;
            }
            set
            {
                SetPropertyValue("SoTaiKhoanChuyen", ref _SoTaiKhoanChuyen, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngân hàng nhận")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NganHang NganHangNhan
        {
            get
            {
                return _NganHangNhan;
            }
            set
            {
                SetPropertyValue("NganHangNhan", ref _NganHangNhan, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản nhận")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string SoTaiKhoanNhan
        {
            get
            {
                return _SoTaiKhoanNhan;
            }
            set
            {
                SetPropertyValue("SoTaiKhoanNhan", ref _SoTaiKhoanNhan, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThuNhap
        {
            get
            {
                return _ThuNhap;
            }
            set
            {
                SetPropertyValue("ThuNhap", ref _ThuNhap, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCN
        {
            get
            {
                return _ThueTNCN;
            }
            set
            {
                SetPropertyValue("ThueTNCN", ref _ThueTNCN, value);
            }
        }

        [ModelDefault("Caption", "Thực nhận")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThucNhan
        {
            get
            {
                return _ThucNhan;
            }
            set
            {
                SetPropertyValue("ThucNhan", ref _ThucNhan, value);
            }
        }

        public ChungTuChuyenKhoan(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }
        //
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }
    }

}
