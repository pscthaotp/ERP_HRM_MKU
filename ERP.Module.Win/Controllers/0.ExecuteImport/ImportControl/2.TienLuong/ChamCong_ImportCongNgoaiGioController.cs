using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class ChamCong_ImportCongNgoaiGioController : ViewController
    {
        private IObjectSpace _obs;
        private OfficeBaseObject _chonLoaiOffice;
        private CC_QuanLyCongNgoaiGio _quanLyChamCong;

        public ChamCong_ImportCongNgoaiGioController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu Quản lý chấm công
            View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();
            //
            _quanLyChamCong = View.CurrentObject as CC_QuanLyCongNgoaiGio;
            _chonLoaiOffice = _obs.CreateObject<OfficeBaseObject>();
            e.View = Application.CreateDetailView(_obs, _chonLoaiOffice);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_quanLyChamCong != null)
            {
                //Xử lý
                Imp_ChamCongNgoaiGio.ImportChamCong(((XPObjectSpace)View.ObjectSpace), _chonLoaiOffice,_quanLyChamCong.Oid);
                //
                View.ObjectSpace.Refresh();
            }
        }

        private void ChamCong_ImportCongNgoaiGioController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<CC_QuanLyCongNgoaiGio>();
        }
    }
}
