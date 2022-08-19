using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenQuanHe")]
    [ModelDefault("Caption", "Mối quan hệ")]
    [RuleCombinationOfPropertiesIsUnique("QuanHe.Unique", DefaultContexts.Save, "MaQuanLy;TenQuanHe")]
    public class QuanHe : BaseObject
    {
        private LoaiQuanHeEnum _LoaiQuanHe;
        private string _TenQuanHe;
        private string _MaQuanLy;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên quan hệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenQuanHe
        {
            get
            {
                return _TenQuanHe;
            }
            set
            {
                SetPropertyValue("TenQuanHe", ref _TenQuanHe, value);
            }
        }

        [ModelDefault("Caption", "Loại quan hệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiQuanHeEnum LoaiQuanHe
        {
            get
            {
                return _LoaiQuanHe;
            }
            set
            {
                SetPropertyValue("LoaiQuanHe", ref _LoaiQuanHe, value);
            }
        }

        public QuanHe(Session session) : base(session) { }
    }

}
