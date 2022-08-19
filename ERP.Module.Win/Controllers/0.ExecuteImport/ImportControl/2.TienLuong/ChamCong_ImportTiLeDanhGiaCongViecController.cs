using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class ChamCong_ImportTiLeDanhGiaCongViecController : ViewController
    {
        private IObjectSpace _obs;
        private OfficeBaseObject _chonLoaiOffice;
        private TiLeDanhGiaCongViec _tiLeDanhGia;

        public ChamCong_ImportTiLeDanhGiaCongViecController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu Quản lý chấm công
            View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();
            //
            _tiLeDanhGia = View.CurrentObject as TiLeDanhGiaCongViec;
            _chonLoaiOffice = _obs.CreateObject<OfficeBaseObject>();
            e.View = Application.CreateDetailView(_obs, _chonLoaiOffice);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_tiLeDanhGia != null)
            {
                //Xử lý
                Imp_TiLeDanhGiaCongViec.ImportTiLeDanhGia(((XPObjectSpace)View.ObjectSpace), _chonLoaiOffice, _tiLeDanhGia.Oid);
                //
                View.ObjectSpace.Refresh();
            }
        }

        private void ChamCong_ImportCongKhacController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<CC_QuanLyCongKhac>();
        }
    }
}
