using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Reports;
using DevExpress.XtraReports;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Drawing.Printing;

namespace ERP.Module.BaoCao.Custom
{
    [RootClass]
    public class CustomXafReport : XafReport
    {
        public GroupReport GroupReport { get; set; }
        public Type TargetType { get; set; }

        public CustomXafReport()
        {
            PaperKind = PaperKind.A4;
        }

        protected override void RefreshDataSourceForPrint()
        {
            if (DataType != null && DataType.BaseType == typeof(StoreProcedureReport))
                return;
            base.RefreshDataSourceForPrint();
        }
    }
}
