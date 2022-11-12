using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Extends;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.PMS;
using ERP.Module.NghiepVu.PMS.BoiDuong;
using ERP.Module.NghiepVu.PMS.QuanLyHoatDongKhac;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.PMS
{
    public partial class PMS_ImportHoatDongKhacController : ViewController
    {
        private IObjectSpace _obs;
        private QuanLyHoatDongKhac _QuanLyHoatDongKhac;
        private OfficeBaseObject _typeOffice;

        public PMS_ImportHoatDongKhacController()
        {
            InitializeComponent();
            RegisterActions(components);          
            TargetViewId = "QuanLyHoatDongKhac_DetailView";
        }
       
        private void popupWindowShowAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _QuanLyHoatDongKhac = View.CurrentObject as QuanLyHoatDongKhac;
            if (_QuanLyHoatDongKhac != null)
            {
                _obs = Application.CreateObjectSpace();
                //
                _typeOffice = _obs.CreateObject<OfficeBaseObject>();
                e.View = Application.CreateDetailView(_obs, _typeOffice);
            }
            else
            {
                DialogUtil.ShowWarning("Vui lòng kiểm tra from quản lý.");
            }
        }

        private void popupWindowShowAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_typeOffice != null)
            {
                //
                Imp_HoatDongKhac.XuLy(_obs, _QuanLyHoatDongKhac.Oid);
                //
                View.ObjectSpace.Refresh();
            }
        }       
    }
}
