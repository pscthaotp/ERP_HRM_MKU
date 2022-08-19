using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace ERP.Module.Win.Diagrams
{
    public partial class XuLyQuyTrinhController : ViewController
    {
        public XuLyQuyTrinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        private void XuLySoDoController_ViewControlsCreated(object sender, EventArgs e)
        {
            DashboardView view = View as DashboardView;
            if (view != null && view.Id == "QuyTrinh_DashboardView")
            {
                ControlViewItem itemQuyTrinh = ((DashboardView)View).FindItem("CustomControl") as ControlViewItem;
                //
                if (itemQuyTrinh!=null)
                {
                    PanelControl panel = itemQuyTrinh.Control as PanelControl;
                    if (panel != null)
                    {
                        panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        panel.Dock = DockStyle.Fill;

                        QuyTrinhBaseController control = QuyTrinhFactory.CreateControl(Application, View.ObjectSpace);
                        view.Caption = control.Caption;

                        int left = CalculatorLocation(panel.Width, control.Width);
                        int top = CalculatorLocation(panel.Height, control.Height);
                        control.SetLocation(top, left);
                        //
                        panel.Controls.Clear();
                        panel.Controls.Add(control);
                        panel.Dock = DockStyle.Fill;
                        control.PerformLayout();
                    }
                }
            }
        }
        private int CalculatorLocation(int width1, int width2)
        {
            int location = 0;
            if (width1 > width2)
                location = (width1 - width2) / 2;

            return location;
        }
    }
}
