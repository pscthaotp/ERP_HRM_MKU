using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenHinhThucThoiViec")]
    [ModelDefault("Caption", "Hình thức thôi việc")]
    public class HinhThucThoiViec : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHinhThucThoiViec;

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

        [ModelDefault("Caption", "Tên hình thức thôi việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHinhThucThoiViec
        {
            get
            {
                return _TenHinhThucThoiViec;
            }
            set
            {
                SetPropertyValue("TenHinhThucThoiViec", ref _TenHinhThucThoiViec, value);
            }
        }

        public HinhThucThoiViec(Session session) : base(session) { }
     
    }

}
