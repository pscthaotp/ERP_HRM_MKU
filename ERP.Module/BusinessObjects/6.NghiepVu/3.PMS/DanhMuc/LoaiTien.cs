using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.PMS.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_Category")]
    [DefaultProperty("TenLoaiTien")]
    [ModelDefault("Caption", "Loại tiền")]
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Tên loại tiền bị trùng", DefaultContexts.Save, "MaQuanLy;TenLoaiTien")]
    public class LoaiTien : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiTien;
       
      
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
        
        [ModelDefault("Caption", "Tên loại tiền")]
        public string TenLoaiTien
        {
            get
            {
                return _TenLoaiTien;
            }
            set
            {
                SetPropertyValue("TenLoaiTien", ref _TenLoaiTien, value);
            }
        }
        
        public LoaiTien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
