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

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [DefaultProperty("ChucVu")]
    [ModelDefault("Caption", "Chức vụ người ký")]
    [ImageName("BO_Position")]
    public class ChucVuNguoiKy : BaseObject
    {
        private ChucVu _ChucVu;

        //
        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
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
        
        public ChucVuNguoiKy(Session session) : base(session) { }
    }

}
