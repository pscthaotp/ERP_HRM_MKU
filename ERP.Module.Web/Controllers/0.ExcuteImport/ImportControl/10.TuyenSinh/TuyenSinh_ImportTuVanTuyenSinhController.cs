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

namespace ERP.Module.Controllers.Web.ExecuteImport.ImportControl.TuyenSinh
{
    public partial class TuyenSinh_ImportTuVanTuyenSinhController : ViewController
    {
        IObjectSpace _obs;
        TuVanTuyenSinh _tuVanTuyenSinh;
        //
        public TuyenSinh_ImportTuVanTuyenSinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //
            View.ObjectSpace.CommitChanges();
            //
            _tuVanTuyenSinh = View.CurrentObject as TuVanTuyenSinh;
            //
            if (_tuVanTuyenSinh != null)
            {
                _obs = Application.CreateObjectSpace();
                //
                OfficeBaseObject_Web obj = _obs.CreateObject<OfficeBaseObject_Web>();
                //
                DetailView detailView = Application.CreateDetailView(_obs, obj);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                e.Context = TemplateContext.PopupWindow;
                e.View = detailView;
            }

        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            OfficeBaseObject_Web obj = e.PopupWindow.View.CurrentObject as OfficeBaseObject_Web;
            //
            if (obj != null && obj.File != null && obj.File.Content != null)
            {
                //
                Imp_TuVanTuyenSinh.ImportTuVanTuyenSinh(_obs,_tuVanTuyenSinh,obj);
                //
                View.ObjectSpace.Refresh();
                //
            }
        }

        private void TuyenSinh_ImportTuVanTuyenSinhController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<TuVanTuyenSinh>();
        }   
    }
}
