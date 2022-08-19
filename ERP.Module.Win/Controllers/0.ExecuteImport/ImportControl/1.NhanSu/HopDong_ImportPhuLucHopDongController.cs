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
    public partial class HopDong_ImportPhuLucHopDongController : ViewController<DetailView>
    {
        public HopDong_ImportPhuLucHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.Id.Equals("HopDong_ChonPhuLucHopDong_DetailView"))
            {
                HopDong_ChonPhuLucHopDong obj = View.CurrentObject as HopDong_ChonPhuLucHopDong;
                if (obj != null)
                {
                    //Xử lý
                    Imp_Contract.ImportAnnexContract(((XPObjectSpace)View.ObjectSpace), obj);
                    //
                    View.Close();
                }
            }    
        }

        private void HopDong_ImportPhuLucHopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<HopDong_ChonPhuLucHopDong>();
        }
    }
}
