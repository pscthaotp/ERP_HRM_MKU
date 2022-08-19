using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
using ERP.Module.Extends;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
//
namespace ERP.Module.Win.Controllers.NghiepVu.TuyenSinh
{
    public partial class DangKyNgoaiKhoa_DangKyController : ViewController
    {
        DangKyNgoaiKhoa _dk;
        public DangKyNgoaiKhoa_DangKyController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void btDangKyNK_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            Session _ses = ((XPObjectSpace)obs).Session;
            _dk = obs.CreateObject<DangKyNgoaiKhoa>();
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, _dk);
            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;

        }
    }
}
