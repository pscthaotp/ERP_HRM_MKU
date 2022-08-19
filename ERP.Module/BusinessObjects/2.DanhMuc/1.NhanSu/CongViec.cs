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
    [DefaultProperty("TenCongViec")]
    [ModelDefault("Caption", "Công việc")]
    public class CongViec : BaseObject
    {
        private string _MaQuanLy;
        private string _TenCongViec;
        private string _DieuKienDinhBien;
        
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

        [ModelDefault("Caption", "Tên công việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue()]
        public string TenCongViec
        {
            get
            {
                return _TenCongViec;
            }
            set
            {
                SetPropertyValue("TenCongViec", ref _TenCongViec, value);
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
          
        public CongViec(Session session) : base(session) { }
    }

}
