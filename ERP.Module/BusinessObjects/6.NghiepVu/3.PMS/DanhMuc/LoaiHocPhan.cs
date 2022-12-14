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
    [DefaultProperty("TenLoaiHocPhan")]
    [ModelDefault("Caption", "Loại học phần")]
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Loại học phần bị trùng", DefaultContexts.Save, "MaQuanLy;TenLoaiHocPhan")]
    public class LoaiHocPhan : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiHocPhan;     
      
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
        
        [ModelDefault("Caption", "Tên loại học phần")]
        public string TenLoaiHocPhan
        {
            get
            {
                return _TenLoaiHocPhan;
            }
            set
            {
                SetPropertyValue("TenLoaiHocPhan", ref _TenLoaiHocPhan, value);
            }
        }
        
        public LoaiHocPhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
