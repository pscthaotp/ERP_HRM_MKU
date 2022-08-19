using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NonPersistentObjects.NhanSu;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.BoPhans
{
    public partial class BoPhan_ThemBoPhanController : ViewController
    {
        private BoPhan _department;
        private BoPhan_ChonLoaiBoPhan _chonLoaiBoPhan;
        private IObjectSpace _obs;
        //
        public BoPhan_ThemBoPhanController()
        {
            InitializeComponent();
            RegisterActions(components);
            //
            popupWindowShowAction1.TargetObjectType = typeof(BoPhan);
        }

        private void BoPhan_ThemBoPhanController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<BoPhan>() || Common.IsWriteGranted<BoPhan>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu Bộ phận hiện hành
            View.ObjectSpace.CommitChanges();
            //
            _department = View.CurrentObject as BoPhan;
            _obs = Application.CreateObjectSpace();
            _chonLoaiBoPhan = _obs.CreateObject<BoPhan_ChonLoaiBoPhan>();
            
            e.View = Application.CreateDetailView(_obs, _chonLoaiBoPhan);

        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();
            //
            BoPhan newBoPhan;
            if (_chonLoaiBoPhan.LoaiBoPhan == LoaiBoPhanEnum.CongTy)
            {
                newBoPhan = _obs.CreateObject<CongTy>();
            }
            else
            {
                newBoPhan = _obs.CreateObject<BoPhan>();
            }
            //
            if (_department != null)
                newBoPhan.BoPhanCha = _obs.GetObjectByKey<BoPhan>(_department.Oid);
            newBoPhan.LoaiBoPhan = _chonLoaiBoPhan.LoaiBoPhan;
            //
            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, newBoPhan);
            e.ShowViewParameters.CreatedView.ObjectSpace.Committed += ObjectSpace_Committed;
        }

        //
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
    }
}
