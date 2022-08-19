using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class Luong_ImportNgoaiKhoaController : ViewController
    {
        private IObjectSpace _obs;
        private HoSoTinhLuong _HoSoTinhLuong;
        private OfficeBaseObject _typeOffice;

        public Luong_ImportNgoaiKhoaController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void DinhBien_ImportDinhBienChucDanhController_Activated(object sender, EventArgs e)
        {
            //popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<HoSoTinhLuong>();   
            popupWindowShowAction1.Active["TruyCap"] = false;
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu dữ liệu 
            View.ObjectSpace.CommitChanges();
            //
            _HoSoTinhLuong = View.CurrentObject as HoSoTinhLuong;
            if (_HoSoTinhLuong != null)
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
                Imp_NgoaiKhoa.ImportNgoaiKhoa(_obs, _HoSoTinhLuong, _typeOffice.LoaiOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
