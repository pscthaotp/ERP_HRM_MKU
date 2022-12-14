using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using ERP.Module.Enum.Systems;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    public class OfficeBaseObject : BaseObject, ICongTy
    {
        private LoaiOfficeEnum _LoaiOffice = LoaiOfficeEnum.Office2003;
        private CongTy _CongTy;
        //
        [ModelDefault("Caption", "Loại Office")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiOfficeEnum LoaiOffice
        {
            get
            {
                return _LoaiOffice;
            }
            set
            {
                SetPropertyValue("LoaiOffice", ref _LoaiOffice, value);
            }
        }
        //
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }
        //
        public OfficeBaseObject(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
        }
    }
}
