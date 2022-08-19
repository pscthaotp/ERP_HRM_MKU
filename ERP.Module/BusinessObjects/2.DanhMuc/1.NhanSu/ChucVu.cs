using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [DefaultProperty("TenChucVu")]
    [ModelDefault("Caption", "Chức vụ")]
    [ImageName("BO_Position")]
    public class ChucVu : BaseObject
    {
        private string _MaQuanLy;
        private string _TenChucVu;
        private decimal _HSPCChucVu;
        private bool _LaQuanLy;
        private bool _LaTruongDonVi;
        private WebGroup _WebGroup;
        private NhomChucVu _NhomChucVu;
        private decimal _CapBac;
        private bool _KhongConHieuLuc;//Nguyen

        [ModelDefault("Caption", "Không còn hiệu lực")]
        public bool KhongConHieuLuc
        {
            get
            {
                return _KhongConHieuLuc;
            }
            set
            {
                SetPropertyValue("KhongConHieuLuc", ref _KhongConHieuLuc, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucVu
        {
            get
            {
                return _TenChucVu;
            }
            set
            {
                SetPropertyValue("TenChucVu", ref _TenChucVu, value);
            }
        }

        [ModelDefault("Caption", "HSPC Chức vụ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu
        {
            get
            {
                return _HSPCChucVu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu", ref _HSPCChucVu, value);
            }
        }

        [ModelDefault("Caption", "Là quản lý")]
        public bool LaQuanLy
        {
            get
            {
                return _LaQuanLy;
            }
            set
            {
                SetPropertyValue("LaQuanLy", ref _LaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Là trưởng đơn vị")]
        public bool LaTruongDonVi
        {
            get
            {
                return _LaTruongDonVi;
            }
            set
            {
                SetPropertyValue("LaTruongDonVi", ref _LaTruongDonVi, value);
            }
        }

        [ModelDefault("Caption", "Nhóm tài khoản (Portal)")]
        public WebGroup WebGroup
        {
            get
            {
                return _WebGroup;
            }
            set
            {
                SetPropertyValue("WebGroup", ref _WebGroup, value);
            }
        }

        [ModelDefault("Caption", "Nhóm chức vụ")]
        public NhomChucVu NhomChucVu
        {
            get
            {
                return _NhomChucVu;
            }
            set
            {
                SetPropertyValue("NhomChucVu", ref _NhomChucVu, value);
            }
        }

        [ModelDefault("Caption", "Cấp bậc")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal CapBac
        {
            get
            {
                return _CapBac;
            }
            set
            {
                SetPropertyValue("CapBac", ref _CapBac, value);
            }
        }

        public ChucVu(Session session) : base(session) { }
    }

}
