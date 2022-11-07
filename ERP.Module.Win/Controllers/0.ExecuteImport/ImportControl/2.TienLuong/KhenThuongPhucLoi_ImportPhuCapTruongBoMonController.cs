using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;
using ERP.Module.NghiepVu.TienLuong.Thuong;
//
namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class KhenThuongPhucLoi_ImportPhuCapTruongBoMonController : ViewController
    {
        private IObjectSpace _obs;
        private OfficeBaseObject _chonLoaiOffice;
        private PhuCapTruongBoMon _PhuCapTruongBoMon;

        public KhenThuongPhucLoi_ImportPhuCapTruongBoMonController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "PhuCapTruongBoMon_DetailView";
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu Quản lý chấm công
            View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();
            //
            _PhuCapTruongBoMon = View.CurrentObject as PhuCapTruongBoMon;
            _chonLoaiOffice = _obs.CreateObject<OfficeBaseObject>();
            e.View = Application.CreateDetailView(_obs, _chonLoaiOffice);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_PhuCapTruongBoMon != null)
            {
                //Xử lý
                Imp_PhuCapTruongBoMon.ImportPhuCapTruongBoMon(((XPObjectSpace)View.ObjectSpace), _PhuCapTruongBoMon, _chonLoaiOffice.LoaiOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }

        private void ChamCong_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<PhuCapTruongBoMon>();
        }
    }
}
