using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.NhanSu;
using System.Drawing;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.NhanSu.BoPhans
{
    [DefaultClassOptions]
    [ImageName("BO_Category")]
    [DefaultProperty("TenHeDaoTao")]
    [ModelDefault("Caption", "Khối tập đoàn")]
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Tên khối bị trùng", DefaultContexts.Save, "MaQuanLy;TenKhoi")]
    public class KhoiTapDoan : BaseObject
    {
        private string _MaQuanLy;
        private string _TenKhoi;

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
        
        [ModelDefault("Caption", "Tên khối")]
        public string TenKhoi
        {
            get
            {
                return _TenKhoi;
            }
            set
            {
                SetPropertyValue("TenKhoi", ref _TenKhoi, value);
            }
        }
        public KhoiTapDoan(Session session) : base(session) { }
    }
}
