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
    [DefaultProperty("TenHocVi")]
    [ModelDefault("Caption", "Học vị")]
    public class HocVi : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHocVi;
        
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

        [ModelDefault("Caption", "Tên học vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHocVi
        {
            get
            {
                return _TenHocVi;
            }
            set
            {
                SetPropertyValue("TenHocVi", ref _TenHocVi, value);
            }
        }

        public HocVi(Session session) : base(session) { }
    }

}
