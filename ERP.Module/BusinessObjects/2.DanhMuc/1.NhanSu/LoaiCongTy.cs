using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using System.Drawing;
//
namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_Bank")]
    [DefaultProperty("TenLoaiCongTy")]
    [ModelDefault("Caption", "Loại Trường")]
    public class LoaiCongTy : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiCongTy;
        //
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

        [ModelDefault("Caption", "Tên loại Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiCongTy
        {
            get
            {
                return _TenLoaiCongTy;
            }
            set
            {
                SetPropertyValue("TenLoaiCongTy", ref _TenLoaiCongTy, value);
            }
        }

        public LoaiCongTy(Session session) : base(session) { }
    }

}
