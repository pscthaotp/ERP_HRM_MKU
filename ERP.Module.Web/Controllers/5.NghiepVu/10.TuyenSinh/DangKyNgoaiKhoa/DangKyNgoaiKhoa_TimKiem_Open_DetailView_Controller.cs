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
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using Ctkhung = ERP.Module.NghiepVu.TKB.ChuongTrinhGiaoDuc.CTKhung;
using Ctkhung_ndcs = ERP.Module.NghiepVu.TKB.ChuongTrinhGiaoDuc.CTKhung_NDCS;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.NgoaiKhoa;
namespace ERP.Module.Win.Controllers.NghiepVu.TuyenSinh
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DangKyNgoaiKhoa_TimKiem_Open_DetailView_Controller : ViewController
    {

        public DangKyNgoaiKhoa_TimKiem_Open_DetailView_Controller()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View.Id == "TimKiem_NgoaiKhoa_ListDangKyNgoaiKhoa_ListView")
            {
                ListViewProcessCurrentObjectController processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
                if (processCurrentObjectController != null)
                {
                    processCurrentObjectController.ProcessCurrentObjectAction.Execute += ProcessCurrentObjectAction_Execute1;
                }
            }
        }

        void ProcessCurrentObjectAction_Execute1(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            Session _ses = ((XPObjectSpace)obs).Session;
            DangKyNgoaiKhoa dk = _ses.GetObjectByKey<DangKyNgoaiKhoa>((e.CurrentObject as DangKyNgoaiKhoa_DanhSachHocSinh).DangKyNgoaiKhoa);
            if (dk != null)
            {
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, dk);
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }
    }
}
