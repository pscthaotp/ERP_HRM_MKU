using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.PMS.CVHT;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.PMS;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.PMS
{
    public partial class PMS_ImportCoVanHocTapController : ViewController
    {
        private IObjectSpace _obs;
        private QuanLyCVHT _QuanLyCVHT;
        private OfficeBaseObject _typeOffice;

        public PMS_ImportCoVanHocTapController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void PMS_ImportCoVanHocTapController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = true;           
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            ////Lưu dữ liệu 
            //View.ObjectSpace.CommitChanges();
            //
            _QuanLyCVHT = View.CurrentObject as QuanLyCVHT;
            if (_QuanLyCVHT != null)
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

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_typeOffice != null)
            {
                //
                Imp_CoVanHocTap.XuLy(_obs, _QuanLyCVHT.Oid);
                //
                View.ObjectSpace.Refresh();
            }
        }
    }
}
