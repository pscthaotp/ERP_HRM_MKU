using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Persistent.Validation;
using ERP.Module.Commons;

namespace ERP.Module.BaoCao.Custom
{

    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Nhập mẫu báo cáo")]
    public class ImportReport : BaseObject,ICongTy
    {
        //
        private bool _GhiDe;
        private GroupReport _GroupReport;
        private CongTy _CongTy;

        [ModelDefault("Caption", "Nhóm báo cáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public GroupReport GroupReport
        {
            get
            {
                return _GroupReport;
            }
            set
            {
                SetPropertyValue("GroupReport", ref _GroupReport, value);
            }
        }

        [ModelDefault("Caption", "Ghi đè")]
        public bool GhiDe
        {
            get
            {
                return _GhiDe;
            }
            set
            {
                SetPropertyValue("GhiDe", ref _GhiDe, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Trường")]
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
        public ImportReport(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }
    }
}
