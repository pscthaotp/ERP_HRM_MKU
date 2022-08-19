using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using ERP.Module.Extends;

namespace ERP.Module.Win.Diagrams
{
    public partial class QuyTrinhBaseController : XtraUserControl
    {
        protected XafApplication Application { get; set; }
        protected IObjectSpace ObjectSpace { get; set; }
        public string Caption { get; private set; }

        protected virtual void PrintChart(ChartControl chart)
        {
            if (!chart.IsPrintingAvailable)
            {
                DialogUtil.ShowError("Xảy ra lỗi trong quá trình in biểu đồ.");
            }
            chart.ShowPrintPreview();
        }
        //
        public QuyTrinhBaseController()
        {
            InitializeComponent();
        }
        public QuyTrinhBaseController(XafApplication application, IObjectSpace objectSpace)
        {
            InitializeComponent();

            Application = application;
            ObjectSpace = objectSpace;
        }

        public virtual void SetLocation(int left, int top)
        {
            Location = new Point(left, top);
        }
    }
}
