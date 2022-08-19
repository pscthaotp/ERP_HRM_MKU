using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    public partial class MienNhiem_ImportQuyetDinhMienNhiemController : ViewController
    {
        private IObjectSpace _obs;
        private QuyetDinh_ChonNguoiKy _chonNguoiKy;

        public MienNhiem_ImportQuyetDinhMienNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            //
            _chonNguoiKy = _obs.CreateObject<QuyetDinh_ChonNguoiKy>();
            e.View = Application.CreateDetailView(_obs, _chonNguoiKy);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_chonNguoiKy != null)
            {
                Imp_QuyetDinhMienNhiem.XuLy(View.ObjectSpace, _chonNguoiKy);
            }
            //
            View.ObjectSpace.Refresh();
        }

        //
        private void MienNhiem_ImportQuyetDinhMienNhiemController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<QuyetDinhMienNhiem>();
        }
    }
}
