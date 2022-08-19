using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChucDanh")]
    [ModelDefault("Caption", "Chức danh")]
    public class ChucDanh : BaseObject
    {
        private string _MaQuanLy;
        private string _TenChucDanh;
        private ChucVu _ChucVu;
        private decimal _CapBac;
        private string _DieuKienDinhBien;
        private bool _KhongConHieuLuc;//Nguyen

        //[NonPersistent]
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

        [ModelDefault("Caption", "Tên chức danh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucDanh
        {
            get
            {
                return _TenChucDanh;
            }
            set
            {
                SetPropertyValue("TenChucDanh", ref _TenChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
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

        [ModelDefault("Caption", "Điều kiện định biên")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.NhanSu.DinhBien.chkComboxEdit_DieuKienDinhBien")]
        [Size(-1)]
        public string DieuKienDinhBien
        {
            get
            {
                return _DieuKienDinhBien;
            }
            set
            {
                SetPropertyValue("DieuKienDinhBien", ref _DieuKienDinhBien, value);
            }
        }

        public ChucDanh(Session session) : base(session) { }
    }

}
