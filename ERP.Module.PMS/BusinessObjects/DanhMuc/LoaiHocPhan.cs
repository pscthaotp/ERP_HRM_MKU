using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using System.ComponentModel;

namespace ERP.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Loại học phần")]
    [DefaultProperty("TenLoaiHocPhan")]
    public class LoaiHocPhan : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiHocPhan;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Loại học phần")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiHocPhan
        {
            get { return _TenLoaiHocPhan; }
            set { SetPropertyValue("TenLoaiHocPhan", ref _TenLoaiHocPhan, value); }
        }
        public LoaiHocPhan(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}