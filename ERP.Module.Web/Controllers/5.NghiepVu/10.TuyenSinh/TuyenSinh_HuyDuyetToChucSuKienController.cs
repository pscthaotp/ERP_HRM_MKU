using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using ERP.Module.Enum.TuyenSinh;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_HuyDuyetToChucSuKienController : ViewController<DetailView>
    {
        public TuyenSinh_HuyDuyetToChucSuKienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            ToChucSuKien obj = View.CurrentObject as ToChucSuKien;
            if (obj != null && obj.DaDuyet)
            {
                obj.DaDuyet = false;
                //
                View.ObjectSpace.CommitChanges();
                View.Refresh();
            }
        }

        private void TuyenSinh_HuyDuyetToChucSuKienController_Activated(object sender, EventArgs e)
        {
            string objectSpace = View.ObjectSpace.ToString();
            //
            if (View == null || objectSpace.Equals("DevExpress.ExpressApp.NonPersistentObjectSpace"))
                return;
            //
            if (Common.KiemTraDuyetHeThong(((XPObjectSpace)View.ObjectSpace).Session, Config.KeyTuyenSinh))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else
                simpleAction1.Active["TruyCap"] = false;
            //
            if (simpleAction1.Active["TruyCap"] == true)
            {
                //
                Common.OidCustomList = new List<Guid>();
            }
        }
    }
}
