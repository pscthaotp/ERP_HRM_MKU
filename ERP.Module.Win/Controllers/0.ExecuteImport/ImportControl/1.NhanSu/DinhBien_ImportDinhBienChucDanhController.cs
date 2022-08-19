using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.DinhBien;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.NhanSu;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu
{
    public partial class DinhBien_ImportDinhBienChucDanhController : ViewController
    {
        private IObjectSpace _obs;
        private QuanLyDinhBienChucDanh _qlDinhBienChucDanh;
        private OfficeBaseObject _typeOffice;

        public DinhBien_ImportDinhBienChucDanhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void DinhBien_ImportDinhBienChucDanhController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<QuanLyDinhBienChucDanh>();           
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu dữ liệu 
            View.ObjectSpace.CommitChanges();
            //
            _qlDinhBienChucDanh = View.CurrentObject as QuanLyDinhBienChucDanh;
            if (_qlDinhBienChucDanh != null)
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
                Imp_DinhBienChucDanh.ImportDinhBienChucDanh(_obs, _qlDinhBienChucDanh, _typeOffice.LoaiOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
