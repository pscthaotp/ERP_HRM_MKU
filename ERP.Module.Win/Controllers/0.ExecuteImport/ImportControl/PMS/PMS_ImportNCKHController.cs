using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.PMS.CVHT;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.PMS;
using ERP.Module.NghiepVu.PMS.NCKH;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.PMS
{
    public partial class PMS_ImportNCKHController : ViewController
    {
        private IObjectSpace _obs;
        private QuanLyNCKH _QuanLyNCKH;
        private OfficeBaseObject _typeOffice;

        public PMS_ImportNCKHController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyNCKH_DetailView";
        }

        private void PMS_ImportNCKHController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction2.Active["TruyCap"] = true;           
        }         
       
        private void popupWindowShowAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _QuanLyNCKH = View.CurrentObject as QuanLyNCKH;
            if (_QuanLyNCKH != null)
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
                Imp_NCKH.XuLy(_obs, _QuanLyNCKH.Oid);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
