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
    [DefaultProperty("ChucDanh")]
    [ModelDefault("Caption", "Chức vụ người ký")]
    [ImageName("BO_Position")]
    public class ChucVuNguoiKy : BaseObject
    {
        private ChucDanh _ChucDanh;

        //
        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }
        
        public ChucVuNguoiKy(Session session) : base(session) { }
    }

}
