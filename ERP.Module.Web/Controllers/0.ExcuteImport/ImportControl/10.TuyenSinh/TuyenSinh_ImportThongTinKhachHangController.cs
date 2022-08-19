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
    public partial class TuyenSinh_ImportThongTinKhachHangController : ViewController
    {
        IObjectSpace _obs;
        ThongTinKhachHang _thongTinKhachHang;
        //
        public TuyenSinh_ImportThongTinKhachHangController()
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
                Imp_ThongTinKhachHang.ImportThongTinKhachHang(_obs,obj);
                //
                View.ObjectSpace.Refresh();
                //
            }
        }

        private void TuyenSinh_ImportThongTinKhachHangController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<ThongTinKhachHang>();
        }
    }
}
