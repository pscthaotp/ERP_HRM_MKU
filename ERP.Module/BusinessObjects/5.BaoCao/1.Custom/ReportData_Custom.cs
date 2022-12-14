using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Drawing;
using DevExpress.Xpo.Metadata;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraReports.UI;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Reports;

namespace ERP.Module.BaoCao.Custom
{
    [ImageName("BO_Report")]
    [DefaultProperty("ReportName")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("Caption", "Báo cáo")]
    public class ReportData_Custom : ReportData
    {
        private string _TargetTypeName;
        private GroupReport _GroupReport;
        private CongTy _CongTy;
        private string _Code;

        [ModelDefault("Caption", "Nhóm báo cáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
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

        [Browsable(false)]
        public string TargetTypeName
        {
            get
            {
                return _TargetTypeName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    SetPropertyValue("TargetTypeName", ref _TargetTypeName, value);
            }
        }

        [NonPersistent]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Hiển thị trên cửa sổ")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.Commons.PersistentTypeEditor")]
        public Type TargetType
        {
            get
            {
                if (!String.IsNullOrEmpty(TargetTypeName))
                {
                    ITypeInfo info = XafTypesInfo.Instance.FindTypeInfo(TargetTypeName);
                    if (info != null)
                        return info.Type;
                    return null;
                }
                return null;
            }
            set
            {
                TargetTypeName = (value != null) ? value.FullName : string.Empty;
            }
        }


        [Delayed]
        [ModelDefault("Caption", "Hình ảnh")]
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(ImageValueConverter))]
        [VisibleInListView(false)]
        public Image HinhAnh
        {
            get
            {
                return GetDelayedPropertyValue<Image>("HinhAnh");
            }
            set
            {
                SetDelayedPropertyValue<Image>("HinhAnh", value);
            }
        }

        [ModelDefault("Caption", "Trường")]
        [Browsable(false)]
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

        [ModelDefault("Caption", "Code")]
        [Browsable(false)]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

        public ReportData_Custom(Session session) : base(session) { }
        public ReportData_Custom(Session session, Type dataType) : base(session, dataType) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            HinhAnh = Properties.Resources.Report;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            if(HinhAnh == null)
                HinhAnh = Properties.Resources.Report;
        }
        public override XtraReport LoadReport(IObjectSpace objectSpace)
        {
            CustomXafReport report = (CustomXafReport)base.LoadReport(objectSpace);
            //
            if (report.DataType != null && report.DataType.BaseType == typeof(StoreProcedureReport))
            {
                List<StoreProcedureReport> dataSource = new List<StoreProcedureReport>();
                StoreProcedureReport.Param.FillDataSource();
                dataSource.Add(StoreProcedureReport.Param);
                report.DataSource = dataSource;
            }
            return report;
        }

        //
        protected override XafReport CreateReport()
        {
            CustomXafReport report = new CustomXafReport
            {
                GroupReport = GroupReport,
                TargetType = TargetType
            };
            //
            return report;
        }
    }
}
