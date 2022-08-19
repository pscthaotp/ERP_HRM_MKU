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
    public partial class NhanVien_ImportNhanVienController : ViewController<DetailView>
    {
        public NhanVien_ImportNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null && View.Id.Equals("NhanVien_ChonBoPhan_DetailView"))
            {
                NhanVien_ChonBoPhan obj = View.CurrentObject as NhanVien_ChonBoPhan;
                if (obj != null)
                {
                    //Xử lý (Lưu ý: Từ bây giờ dùng hàm này, hàm kia chỉ dùng khi import dữ liệu lần đầu vào hệ thống)
                    Imp_Staff.ImportStaffNew(((XPObjectSpace)View.ObjectSpace), obj);
                    //
                    View.Close();
                }
            }
        }

        private void NhanVien_ImportNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<NhanVien_ChonBoPhan>();
        }
    }
}
