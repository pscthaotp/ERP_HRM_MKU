using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_CreateAppmenuController : ViewController
    {
        private AppMenu _appMenu;
        private ChonLoaiChucNang _chonLoaiChucNang;
        private IObjectSpace _obs;
        //
        public HeThong_CreateAppmenuController()
        {
            InitializeComponent();
            RegisterActions(components);
            //
            popupWindowShowAction1.TargetObjectType = typeof(AppMenu);
        }

        private void HeThong_CreateAppmenuController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<AppMenu>() || Common.IsAccessGranted(typeof(AppMenu));
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu Bộ phận hiện hành
            View.ObjectSpace.CommitChanges();
            //
            _appMenu = View.CurrentObject as AppMenu;
            _obs = Application.CreateObjectSpace();
            _chonLoaiChucNang = _obs.CreateObject<ChonLoaiChucNang>();
            e.View = Application.CreateDetailView(_obs, _chonLoaiChucNang);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();
            //
            AppMenu newAppMenu;
            AppMenu oidRoot = _obs.GetObjectByKey<AppMenu>(new Guid("F65089D4-69C9-4F5D-AF2A-52221317C5D3"));
            if (_chonLoaiChucNang.LoaiChucNang == LoaiChucNangEnum.ThuMucQuanLy)
            {
                newAppMenu = _obs.CreateObject<AppMenu>();
                newAppMenu.ThuMucQuanLy = oidRoot;
                newAppMenu.LaThuMuc = true;
            }
            else
            {
                newAppMenu = _obs.CreateObject<AppMenu>();
                newAppMenu.ThuMucQuanLy = _obs.GetObjectByKey<AppMenu>(_appMenu.Oid);
                newAppMenu.LaThuMuc = false;
                newAppMenu.PhanHe = _obs.GetObjectByKey<ERP.Module.DanhMuc.System.PhanHe>(_appMenu.PhanHe.Oid);
            }
            //
            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, newAppMenu);
            e.ShowViewParameters.CreatedView.ObjectSpace.Committed += ObjectSpace_Committed;
        }
        //
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        public DevExpress.Data.Filtering.CriteriaOperator CriteriaOperator { get; set; }
    }
}
