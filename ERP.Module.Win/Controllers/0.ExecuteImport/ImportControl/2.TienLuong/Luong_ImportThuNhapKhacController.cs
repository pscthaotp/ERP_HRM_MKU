using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class Luong_ImportThuNhapKhacController : ViewController
    {
        private IObjectSpace _obs;
        private HoSoTinhLuong _hoSoTinhLuong;
        private OfficeBaseObject _typeOffice;

        public Luong_ImportThuNhapKhacController()
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
            if (_hoSoTinhLuong != null)
            {
                _obs = Application.CreateObjectSpace();
                //
                _typeOffice = _obs.CreateObject<OfficeBaseObject>();
                e.View = Application.CreateDetailView(_obs, _typeOffice);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_typeOffice != null)
            {
                //
                Imp_CacKhoanThuNhapKhac.ImportCacKhoanThuNhapKhac(_obs, _hoSoTinhLuong, _typeOffice.LoaiOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
