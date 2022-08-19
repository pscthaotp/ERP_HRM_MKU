using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Commons;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    public partial class NhanVien_ImportQuaTrinhNhanVienController : ViewController<DetailView>
    {
        public NhanVien_ImportQuaTrinhNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
             if (View != null && View.Id.Equals("NhanVien_ChonQuaTrinh_DetailView"))
            {
                NhanVien_ChonQuaTrinh obj = View.CurrentObject as NhanVien_ChonQuaTrinh;
                if (obj != null)
                {
                    //Xử lý
                    Imp_Staff.ImportProsess(((XPObjectSpace)View.ObjectSpace), obj);
                    //
                    View.Close();
                }
            }    
        }

        private void NhanVien_ImportQuaTrinhNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<NhanVien_ChonQuaTrinh>();
        }
    }
}
