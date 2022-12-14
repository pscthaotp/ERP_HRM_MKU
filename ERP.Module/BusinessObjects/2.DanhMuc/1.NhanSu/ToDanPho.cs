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
    [DefaultProperty("TenToDanPho")]
    [ModelDefault("Caption", "Tổ dân phố")]
    public class ToDanPho : BaseObject
    {
        private string _MaQuanLy;
        private string _TenToDanPho;

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

        [ModelDefault("Caption", "Tên tổ dân phố")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenToDanPho
        {
            get
            {
                return _TenToDanPho;
            }
            set
            {
                SetPropertyValue("TenToDanPho", ref _TenToDanPho, value);
            }
        }


        public ToDanPho(Session session) : base(session) { }
    }

}
