using System;
using System.Collections.Generic;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.BaoCao.Custom
{
    [NonPersistent]
    [ModelDefault("Caption", "Xuất mẫu báo cáo")]
    public class ExportReport : BaseObject
    {
        [ModelDefault("AllowEdit", "True")]
        [ModelDefault("Caption", "Danh sách báo cáo")]
        public XPCollection<DetailExport> ReportList { get; set; }

        public ExportReport(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ReportList = new XPCollection<DetailExport>(Session, false);
            //
            using (XPCollection<ReportData_Custom> reportList = new XPCollection<ReportData_Custom>(Session))
            {
                foreach (ReportData_Custom item in reportList)
                {
                    DetailExport report = new DetailExport(Session);
                    report.CongTy = item.CongTy;
                    report.Report = item;
                    report.GroupReport = item.GroupReport;
                    //
                    ReportList.Add(report);
                }
            }
        }

    }
}
