using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using ERP.Module.Win.NormalForm.System;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_XemNhatKyDuLieuContrller : ViewController
    {
        public HeThong_XemNhatKyDuLieuContrller()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            //
            frmNhatKyDuLieu frm = new frmNhatKyDuLieu(session);
            frm.ShowDialog();
            //
        }

        private void HeThong_XemNhatKyDuLieuContrller_Activated(object sender, EventArgs e)
        {
            if (!Common.TaiKhoanBinhThuong())
            {
                this.simpleAction1.Active["TruyCap"] = true;
            }
            else
            {
                this.simpleAction1.Active["TruyCap"] = false;
            }
        }
    }
}
