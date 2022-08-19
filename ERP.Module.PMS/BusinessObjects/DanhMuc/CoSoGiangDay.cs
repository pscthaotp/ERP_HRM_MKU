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
    [ModelDefault("Caption", "Cơ sở giảng dạy")]
    [DefaultProperty("TenCoSo")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "MaQuanLy", "Mã cơ sở đã tồn tại.")]
    public class CoSoGiangDay : BaseObject
    {
        private string _MaQuanLy;
        private string _TenCoSo;
        private string _DiaChi;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Cơ sở")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit", "False")]
        public string TenCoSo
        {
            get { return _TenCoSo; }
            set { SetPropertyValue("TenCoSo", ref _TenCoSo, value); }
        }
        [ModelDefault("Caption", "Địa chỉ")]
        public string DiaChi
        {
            get { return _DiaChi; }
            set { SetPropertyValue("DiaChi", ref _DiaChi, value); }
        }

        public CoSoGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}