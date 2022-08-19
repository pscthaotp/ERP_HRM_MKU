using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.BaoCao.Custom
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Tạo báo cáo")]
    public class CreateReport : BaseObject,ICongTy
    {
        //
        private GroupReport _GroupReport;
        private string _TenBaoCao;
        private Type _DataType;
        private Type _TargetType;
        private CongTy _CongTy;

        [ModelDefault("Caption", "Tên báo cáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBaoCao
        {
            get
            {
                return _TenBaoCao;
            }
            set
            {
                SetPropertyValue("TenBaoCao", ref _TenBaoCao, value);
            }
        }

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

        [ModelDefault("Caption", "Kiểu dữ liệu")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.Commons.ReportTypeEditor")]
        public Type DataType
        {
            get
            {
                return _DataType;
            }
            set
            {
                SetPropertyValue("DataType", ref _DataType, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Hiển thị trên cửa sổ")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.Commons.PersistentTypeEditor")]
        public Type TargetType
        {
            get
            {
                return _TargetType;
            }
            set
            {
                SetPropertyValue("TargetType", ref _TargetType, value);
            }
        }

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

        public CreateReport(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }
    }
}
