using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.PMS.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNhomMonHoc")]
    [ModelDefault("Caption", "Nhóm môn học")]
    public class NhomMonHoc : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNhomMonHoc;
        //private LoaiNhanSu _LoaiNhanSu;
        //private string _DieuKienDinhBien;

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

        [ModelDefault("Caption", "Tên nhóm")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue()]
        public string TenNhomMonHoc
        {
            get
            {
                return _TenNhomMonHoc;
            }
            set
            {
                SetPropertyValue("TenNhomMonHoc", ref _TenNhomMonHoc, value);
            }
        }

        //[ModelDefault("Caption", "Loại nhân sự")]
        //public LoaiNhanSu LoaiNhanSu
        //{
        //    get
        //    {
        //        return _LoaiNhanSu;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
        //    }
        //}

        //[ModelDefault("Caption", "Điều kiện định biên")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.ComboBoxEditor_DieuKienDinhBien")]
        //[Size(-1)]
        //public string DieuKienDinhBien
        //{
        //    get
        //    {
        //        return _DieuKienDinhBien;
        //    }
        //    set
        //    {
        //        SetPropertyValue("DieuKienDinhBien", ref _DieuKienDinhBien, value);
        //    }
        //}

        public NhomMonHoc(Session session) : base(session) { }
    }
}
