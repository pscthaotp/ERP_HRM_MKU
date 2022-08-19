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
    public partial class GiangVien_ImportGiangVienController : ViewController<DetailView>
    {
        public GiangVien_ImportGiangVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View != null)
            {
                GiangVien_ChonLoaiGiangVien obj = View.CurrentObject as GiangVien_ChonLoaiGiangVien;
                if (obj != null)
                {
                    //Xử lý (Lưu ý: Từ bây giờ dùng hàm này, hàm kia chỉ dùng khi import dữ liệu lần đầu vào hệ thống)
                    Imp_GiangVienThinhGiang.ImportGiangVienThinhGiangNew(((XPObjectSpace)View.ObjectSpace), obj);
                    //
                    View.Close();
                }
            }
        }

        private void GiangVien_ImportGiangVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<GiangVien_ChonLoaiGiangVien>();
        }
    }
}
