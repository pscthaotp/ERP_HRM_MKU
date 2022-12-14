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
    [DefaultProperty("TenLoaiNhanSu")]
    [ModelDefault("Caption", "Loại nhân sự")]
    [RuleCombinationOfPropertiesIsUnique("LoaiNhanSu.Identifier", DefaultContexts.Save, "MaQuanLy;TenLoaiNhanSu", "Loại nhân sự đã tồn tại trong hệ thống.")]
    public class LoaiNhanSu : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiNhanSu;
        private decimal _CapDo;

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

        [ModelDefault("Caption", "Tên loại nhân sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiNhanSu
        {
            get
            {
                return _TenLoaiNhanSu;
            }
            set
            {
                SetPropertyValue("TenLoaiNhanSu", ref _TenLoaiNhanSu, value);
            }
        }

        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }
        public LoaiNhanSu(Session session) : base(session) { }
    }

}
