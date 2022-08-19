using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using ERP.Module.NghiepVu.TienLuong.KhauTru;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class KhauTru_ImportKhauTruLuongController : ViewController
    {
        private IObjectSpace _obs;
        private BangKhauTruLuong _bangKhauTru;
        private OfficeBaseObject _typeOffice;

        public KhauTru_ImportKhauTruLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void KhauTru_ImportKhauTruLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<BangKhauTruLuong>();           
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu dữ liệu 
            View.ObjectSpace.CommitChanges();
            //
            _bangKhauTru = View.CurrentObject as BangKhauTruLuong;
            if (_bangKhauTru != null)
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
                Imp_KhauTruLuong.ImportKhauTruLuong(_obs, _bangKhauTru, _typeOffice.LoaiOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
