using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_Country_v92")]
    [DefaultProperty("TenDot")]
    [ModelDefault("Caption", "Đợt tính lương")]
    public class DotTinhLuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDot;
        //
        [ModelDefault("Caption", "Mã quản lý (sửa)")]
        [RuleRequiredField("",DefaultContexts.Save)]
        [RuleUniqueValue("",DefaultContexts.Save)]
        [ModelDefault("AllowEdit","False")]
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

        [ModelDefault("Caption", "Tên đợt")]
        [RuleRequiredField("",DefaultContexts.Save)]
        public string TenDot
        {
            get
            {
                return _TenDot;
            }
            set
            {
                SetPropertyValue("TenDot", ref _TenDot, value);
                if (!IsLoading && !string.IsNullOrEmpty(value))
                {
                    //
                    GetMaQuanLy();
                }
            }
        }
        //
        public DotTinhLuong(Session session) : base(session) { }

        private void GetMaQuanLy()
        {
            this.MaQuanLy = ERP.Module.Extends.StringHelpers.ReplaceVietnameseChar(ERP.Module.Extends.StringHelpers.ToTitleCase(this.TenDot)).Replace(" ", String.Empty);
        }
    }

}
