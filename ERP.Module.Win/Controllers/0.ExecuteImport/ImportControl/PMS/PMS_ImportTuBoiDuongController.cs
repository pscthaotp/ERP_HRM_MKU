using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Extends;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.PMS;
using ERP.Module.NghiepVu.PMS.BoiDuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.PMS
{
    public partial class PMS_ImportTuBoiDuongController : ViewController
    {
        private IObjectSpace _obs;
        private QuanLyBoiDuong _QuanLyBoiDuong;
        private OfficeBaseObject _typeOffice;

        public PMS_ImportTuBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);          
            TargetViewId = "QuanLyBoiDuong_DetailView";
        }
       
        private void popupWindowShowAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _QuanLyBoiDuong = View.CurrentObject as QuanLyBoiDuong;
            if (_QuanLyBoiDuong != null)
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
                Imp_TuBoiDuong.XuLy(_obs, _QuanLyBoiDuong.Oid);
                //
                View.ObjectSpace.Refresh();
            }
        }

        private void PMS_ImportTuBoiDuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction2.Active["TruyCap"] = true;
        }
    }
}
