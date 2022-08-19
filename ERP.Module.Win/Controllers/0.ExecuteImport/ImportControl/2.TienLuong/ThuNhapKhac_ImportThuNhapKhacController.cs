using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using ERP.Module.NghiepVu.TienLuong.ThuNhapKhac;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class ThuNhapKhac_ImportThuNhapKhacController : ViewController
    {
        private IObjectSpace _obs;
        private BangThuNhapKhac _bangThuNhapKhac;
        private OfficeBaseObject _typeOffice;

        public ThuNhapKhac_ImportThuNhapKhacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThuNhapKhac_ImportThuNhapKhacController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<BangThuNhapKhac>();           
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu dữ liệu 
            View.ObjectSpace.CommitChanges();
            //
            _bangThuNhapKhac = View.CurrentObject as BangThuNhapKhac;
            if (_bangThuNhapKhac != null)
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
                Imp_ThuNhapKhac.ImportThuNhapKhac(_obs, _bangThuNhapKhac, _typeOffice.LoaiOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
