using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
    [DefaultProperty("TenNgonNgu")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "MaQuanLy", "Mã cơ sở đã tồn tại.")]
    public class NgonNguGiangDay : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNgonNgu;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNgonNgu
        {
            get { return _TenNgonNgu; }
            set { SetPropertyValue("TenNgonNgu", ref _TenNgonNgu, value); }
        }

        public NgonNguGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}