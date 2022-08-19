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
////
namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class GiangVien_ChucNangImportController : ViewController
    {
        public GiangVien_ChucNangImportController()
        {
            InitializeComponent();
            RegisterActions(components);

            TargetViewId = "GiangVienThinhGiang_ListView;GiangVienThinhGiang_DetailView;ThinhGiangCustomView_DetailView";
        }

        private void GiangVien_ChucNangImportController_ViewControlsCreated(object sender, EventArgs e)
        {
                //Khởi tạo cáo chức năng
                singleChoiceActionList.Items.Clear();

                //1. Nhập hồ sơ
                ChoiceActionItem importGiangVienThinhGiang = new ChoiceActionItem();
                importGiangVienThinhGiang.Id = "ImportGiangVienThinhGiang";
                importGiangVienThinhGiang.Caption = "Nhập hồ sơ giảng viên thỉnh giảng";
                importGiangVienThinhGiang.ImageName = "BO_HoSo";
                singleChoiceActionList.Items.Add(importGiangVienThinhGiang);
               
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            //
            IObjectSpace obs = Application.CreateObjectSpace();

            //1. Nhập hồ sơ
            if (e.SelectedChoiceActionItem.Id.Equals("ImportGiangVienThinhGiang"))
            {
                //
                GiangVien_ChonLoaiGiangVien obj = obs.CreateObject<GiangVien_ChonLoaiGiangVien>();
                DetailView detailView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.CreatedView = detailView;
                GiangVien_ImportGiangVienController excuteImport = new GiangVien_ImportGiangVienController();
                e.ShowViewParameters.Controllers.Add(excuteImport);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            }
          
        }
    }
}
