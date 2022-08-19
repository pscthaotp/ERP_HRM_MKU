using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NonPersistentObjects.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChamCong
{
    public partial class ChamCong_ThemChamCongKhacController : ViewController
    {
        IObjectSpace _obs;
        ChamCong_KyChamCong _source;

        public ChamCong_ThemChamCongKhacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChamCong_ThemChamCongKhacController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<HoSoTinhLuong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //
            _obs = Application.CreateObjectSpace();
            _source = _obs.CreateObject<ChamCong_KyChamCong>();
            e.View = Application.CreateDetailView(_obs, _source);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source != null && _source.KyChamCong != null)
            {//
                _obs = Application.CreateObjectSpace();
                CC_ChamCongKhac chamCong = _obs.CreateObject<CC_ChamCongKhac>();
                chamCong.KyChamCong = _obs.GetObjectByKey<CC_KyChamCong>(_source.KyChamCong.Oid);
                //
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, chamCong);
            }
        }
    }
}
