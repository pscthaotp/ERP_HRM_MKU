using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Reports.Win;
using DevExpress.ExpressApp.Layout;
using System.Windows.Forms;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu;
//
namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_ChucNangImportController : ViewController
    {
        public NhanVien_ChucNangImportController()
        {
            InitializeComponent();
            RegisterActions(components);

            TargetViewId = "NhanVienDangLamViec_ListView;ThongTinNhanVien_DetailView;NhanSuCustomView_DetailView";
        }

        private void NhanVien_ChucNangImportController_ViewControlsCreated(object sender, EventArgs e)
        {
                //Khởi tạo cáo chức năng
                singleChoiceActionList.Items.Clear();

                //1. Nhập hồ sơ
                ChoiceActionItem importStaff = new ChoiceActionItem();
                importStaff.Id = "ImportStaff";
                importStaff.Caption = "Nhập hồ sơ";
                importStaff.ImageName = "BO_HoSo";
                singleChoiceActionList.Items.Add(importStaff);
                //2. Nhập các quá trình
                ChoiceActionItem importProsess = new ChoiceActionItem();
                importProsess.Id = "ImportProsess";
                importProsess.Caption = "Nhập các quá trình nhân viên";
                importProsess.ImageName = "BO_List1";
                singleChoiceActionList.Items.Add(importProsess);
                //3. Nhập quan hệ gia đình
                ChoiceActionItem importFamily = new ChoiceActionItem();
                importFamily.Id = "ImportFamily";
                importFamily.Caption = "Nhập quan hệ gia đình";
                importFamily.ImageName = "BO_DanToc";
                singleChoiceActionList.Items.Add(importFamily);
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            //
            IObjectSpace obs = Application.CreateObjectSpace();

            //1. Nhập hồ sơ
            if (e.SelectedChoiceActionItem.Id.Equals("ImportStaff"))
            {
                //
                NhanVien_ChonBoPhan obj = obs.CreateObject<NhanVien_ChonBoPhan>();
                DetailView detailView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.CreatedView = detailView;
                NhanVien_ImportNhanVienController excuteImport = new NhanVien_ImportNhanVienController();
                e.ShowViewParameters.Controllers.Add(excuteImport);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            }
            //2. Nhập các quá trình
            if (e.SelectedChoiceActionItem.Id.Equals("ImportProsess"))
            {
                //
                NhanVien_ChonQuaTrinh obj = obs.CreateObject<NhanVien_ChonQuaTrinh>();
                DetailView detailView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.CreatedView = detailView;
                NhanVien_ImportQuaTrinhNhanVienController excuteImport = new NhanVien_ImportQuaTrinhNhanVienController();
                e.ShowViewParameters.Controllers.Add(excuteImport);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            }

            //3. Nhập quan hệ gia đình
            if (e.SelectedChoiceActionItem.Id.Equals("ImportFamily"))
            {
                //
                NhanVien_ChonNhanVien obj = obs.CreateObject<NhanVien_ChonNhanVien>();
                DetailView detailView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.CreatedView = detailView;
                NhanVien_ImportQuanHeGiaDinhController excuteImport = new NhanVien_ImportQuanHeGiaDinhController();
                e.ShowViewParameters.Controllers.Add(excuteImport);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            }
        }
    }
}
