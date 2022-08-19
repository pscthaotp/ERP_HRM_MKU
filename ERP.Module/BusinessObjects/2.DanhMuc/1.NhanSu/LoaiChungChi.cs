using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChungChi")]
    [ModelDefault("Caption", "Loại chứng chỉ")]
    public class LoaiChungChi : BaseObject
    {
        private string _MaQuanLy;
        private string _TenChungChi;

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

        [ModelDefault("Caption", "Chứng chỉ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChungChi
        {
            get
            {
                return _TenChungChi;
            }
            set
            {
                SetPropertyValue("TenChungChi", ref _TenChungChi, value);
            }
        }

        public LoaiChungChi(Session session) : base(session) { }

    }

}
