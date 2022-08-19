using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.IO;
using DevExpress.ExpressApp.Web;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Web.Controllers.ImportClass;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.HeThong;

namespace ERP.Module.Controllers.Web.ExecuteImport.ImportControl.TuyenSinh
{
    public partial class TuyenSinh_ImportHoSoXetTuyenController : ViewController
    {
        IObjectSpace _obs;
        //
        public TuyenSinh_ImportHoSoXetTuyenController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //
            _obs = Application.CreateObjectSpace();
            //
            OfficeBaseObject_Web obj = _obs.CreateObject<OfficeBaseObject_Web>();
            //
            DetailView detailView = Application.CreateDetailView(_obs, obj);
            detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            e.Context = TemplateContext.PopupWindow;
            e.View = detailView;

        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            OfficeBaseObject_Web obj = e.PopupWindow.View.CurrentObject as OfficeBaseObject_Web;
            //
            if (obj != null && obj.File != null && obj.File.Content != null)
            {
                //
                Imp_HoSoXetTuyen.ImportHoSoXetTuyen(_obs, obj);
                //
                View.ObjectSpace.Refresh();
                //
            }
        }

        private void TuyenSinh_ImportHoSoXetTuyenController_Activated(object sender, EventArgs e)
        {
            bool active = false;
            active = Common.IsWriteGranted<HoSoXetTuyen>();
            //popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<HoSoXetTuyen>();
            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
            if (View.Id.Equals("HoSoXetTuyen_ListView") && user.CongTy.Oid.Equals(Config.KeyTanPhu))
            {
                popupWindowShowAction1.Active["TruyCap"] = true;
            }
            else
            {
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
        }
    }
}
