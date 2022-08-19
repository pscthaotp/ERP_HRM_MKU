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
    public partial class NhanVien_ImportQuanHeGiaDinhController : ViewController<DetailView>
    {
        public NhanVien_ImportQuanHeGiaDinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.Id.Equals("NhanVien_ChonNhanVien_DetailView"))
            {
                NhanVien_ChonNhanVien obj = View.CurrentObject as NhanVien_ChonNhanVien;
                if (obj != null)
                {
                    //Xử lý
                    Imp_Staff.ImportFamily(((XPObjectSpace)View.ObjectSpace), obj);
                    //
                    View.Close();
                }
            }
        }

        private void NhanVien_ImportQuanHeGiaDinhController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<NhanVien_ChonNhanVien>();
        }
    }
}
