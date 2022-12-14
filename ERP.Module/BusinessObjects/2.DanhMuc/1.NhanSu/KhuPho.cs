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
    [DefaultProperty("TenKhuPho")]
    [ModelDefault("Caption", "Khu phố")]
    public class KhuPho : BaseObject
    {
        private string _MaQuanLy;
        private string _TenKhuPho;

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

        [ModelDefault("Caption", "Tên khu phố")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenKhuPho
        {
            get
            {
                return _TenKhuPho;
            }
            set
            {
                SetPropertyValue("TenKhuPho", ref _TenKhuPho, value);
            }
        }

       
        public KhuPho(Session session) : base(session) { }
    }

}
