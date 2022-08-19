using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Commons;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    public partial class HopDong_ImportHopDongLamViecController : ViewController<DetailView>
    {
        public HopDong_ImportHopDongLamViecController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.Id.Equals("HopDong_ChonHopDongLamViec_DetailView"))
            {
                HopDong_ChonHopDongLamViec obj = View.CurrentObject as HopDong_ChonHopDongLamViec;
                if (obj != null)
                {
                    //Xử lý
                    Imp_Contract.ImportWorkContract(((XPObjectSpace)View.ObjectSpace), obj);
                    //
                    View.Close();
                }
            }    
        }

        private void HopDong_ImportHopDongLamViecController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<HopDong_ChonHopDongLamViec>();
        }
    }
}
