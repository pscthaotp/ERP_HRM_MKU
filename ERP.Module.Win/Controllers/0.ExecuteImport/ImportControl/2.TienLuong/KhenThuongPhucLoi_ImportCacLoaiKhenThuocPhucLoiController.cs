using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.Thuong;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Controllers.Win.ExecuteImport.ImportClass.TienLuong;

namespace ERP.Module.Controllers.Win.ExecuteImport.ImportControl.TienLuong
{
    public partial class KhenThuongPhucLoi_ImportCacLoaiKhenThuocPhucLoiController : ViewController
    {

        private IObjectSpace _obs;
        private BangThuongNhanVien _BangThuongNhanVien;
        private OfficeBaseObject _typeOffice;


        public KhenThuongPhucLoi_ImportCacLoaiKhenThuocPhucLoiController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangThuongNhanVien_DetailView";
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            //
            _BangThuongNhanVien = View.CurrentObject as BangThuongNhanVien;
            if (_BangThuongNhanVien != null)
            {
                _obs = Application.CreateObjectSpace();
                //
                _typeOffice = _obs.CreateObject<OfficeBaseObject>();
                e.View = Application.CreateDetailView(_obs, _typeOffice);
            }
        }
        private void KhauTru_ImportKhauTruLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<BangThuongNhanVien>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_typeOffice != null)
            {
                //
                Imp_KhenThuongPhucLoi.ImportCacKhenThuongPhucLoi(_obs, _BangThuongNhanVien, _typeOffice.LoaiOffice);
                //
                View.ObjectSpace.Refresh();
            }
        }


    }
}
