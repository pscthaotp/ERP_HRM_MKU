using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    public partial class NhanVien_ImportHoSoBaoHiemNhanVienController : ViewController
    {
        private IObjectSpace _obs;       
        private OfficeBaseObject _typeOffice;

        public NhanVien_ImportHoSoBaoHiemNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void NhanVien_ImportHoSoBaoHiemNhanVienController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<HoSoBaoHiem>();           
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu dữ liệu 
            View.ObjectSpace.CommitChanges();
            //          
            _obs = Application.CreateObjectSpace();
            //
            _typeOffice = _obs.CreateObject<OfficeBaseObject>();
            e.View = Application.CreateDetailView(_obs, _typeOffice);            
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_typeOffice != null)
            {
                //
                Imp_HoSoBaoHiem.XuLy(_obs, _typeOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
