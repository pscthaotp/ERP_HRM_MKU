using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Extends;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class Luong_ImportLuongNhanVienController : ViewController
    {
        private IObjectSpace _obs;
        private HoSoTinhLuong _hoSoTinhLuong;
        private OfficeBaseObject _typeOffice;

        public Luong_ImportLuongNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void Luong_ImportLuongNhanVienController_Activated(object sender, EventArgs e)
        {
            //popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<BangLuongNhanVien>() && Common.IsWriteGranted<HoSoTinhLuong>(); 
            popupWindowShowAction1.Active["TruyCap"] = false;
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu dữ liệu 
            View.ObjectSpace.CommitChanges();
            //
            _hoSoTinhLuong = View.CurrentObject as HoSoTinhLuong;
            if (_hoSoTinhLuong != null && !_hoSoTinhLuong.KyTinhLuong.KhoaKyTinhLuong)
            {
                _obs = Application.CreateObjectSpace();
                //
                _typeOffice = _obs.CreateObject<OfficeBaseObject>();
                e.View = Application.CreateDetailView(_obs, _typeOffice);
            }
            else
            {
                DialogUtil.ShowWarning("Kỳ tính lương đã khóa.");
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_typeOffice != null)
            {
                //
                Imp_LuongNhanVien.ImportLuongNhanVien(_obs, _hoSoTinhLuong, _typeOffice.LoaiOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
