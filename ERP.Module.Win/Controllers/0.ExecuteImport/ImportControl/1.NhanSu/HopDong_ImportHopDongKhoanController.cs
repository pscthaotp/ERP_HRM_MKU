using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu;
using ERP.Module.Commons;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    public partial class HopDong_ImportHopDongKhoanController : ViewController<DetailView>
    {
        public HopDong_ImportHopDongKhoanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.Id.Equals("HopDong_ChonHopDongKhoan_DetailView"))
            {
                HopDong_ChonHopDongKhoan obj = View.CurrentObject as HopDong_ChonHopDongKhoan;
                if (obj != null)
                {
                    //Xử lý
                    Imp_Contract.ImportHardContract(((XPObjectSpace)View.ObjectSpace), obj);
                    //
                    View.Close();
                }
            }    
        }

        private void HopDong_ImportHopDongKhoanController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<HopDong_ChonHopDongKhoan>();
        }
    }
}
