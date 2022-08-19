using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.TuyenSinh;
using DevExpress.ExpressApp.Web;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_CapNhatNgayThoiHocController : ViewController
    {
        HoSoThoiHoc _hoSoThoiHoc;
        HoSoThoiHoc_NgayThoiHoc _ngayThoiHoc;
        IObjectSpace obs;
        public TuyenSinh_CapNhatNgayThoiHocController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TuyenSinh_CapNhatNgayThoiHocController_Activated(object sender, EventArgs e)
        {
            //
            popupWindowShowAction1.Active["TruyCap"] = Commons.Common.IsWriteGranted<HoSoThoiHoc>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_ngayThoiHoc != null)
            {
                if (View.SelectedObjects != null && View.SelectedObjects.Count > 0)
                {
                    foreach (var item in View.SelectedObjects)
                    {
                        _hoSoThoiHoc = item as HoSoThoiHoc;
                        if (_hoSoThoiHoc != null)
                        {
                            HoSoThoiHoc hs = obs.GetObjectByKey<HoSoThoiHoc>(_hoSoThoiHoc.Oid);
                            if(hs!= null)
                                hs.NgayNghiHoc = _ngayThoiHoc.NgayThoiHoc; 
                        }
                    }
                    obs.CommitChanges();
                    //View.Refresh();
                    View.ObjectSpace.Refresh();
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Cập nhật ngày thôi học thành công.')");
                }
            }
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {

            if (View.SelectedObjects != null && View.SelectedObjects.Count > 0)
            {
                //
                obs = Application.CreateObjectSpace();
                //
                _ngayThoiHoc = obs.CreateObject<HoSoThoiHoc_NgayThoiHoc>();
                DetailView view = Application.CreateDetailView(obs, _ngayThoiHoc);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }
    }
}
