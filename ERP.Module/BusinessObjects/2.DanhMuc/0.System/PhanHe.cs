using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.System
{
    [DefaultClassOptions]
    [ImageName("BO_DanToc")]
    [DefaultProperty("TenPhanHe")]
    [ModelDefault("Caption", "Phân hệ")]
    public class PhanHe : BaseObject
    {
        private string _MaQuanLy;
        private string _TenPhanHe;

        [ModelDefault("Caption", "Mã Quản Lý")]
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

        [ModelDefault("Caption", "Tên phân hệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenPhanHe
        {
            get
            {
                return _TenPhanHe;
            }
            set
            {
                SetPropertyValue("TenPhanHe", ref _TenPhanHe, value);
            }
        }

        public PhanHe(Session session) : base(session) { }

    }

}
