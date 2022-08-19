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
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.Controllers.Win.ExecuteImport.ImportControl.NhanSu;
//
namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HopDongs
{
    public partial class HopDong_ChucNangImportController : ViewController
    {
        QuanLyHopDong quanLyHopDong;
        public HopDong_ChucNangImportController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HopDong_ChucNangImportController_ViewControlsCreated(object sender, EventArgs e)
        {
          
            //Khởi tạo cáo chức năng
            singleChoiceActionList.Items.Clear();

            //1. Hợp đồng lao động
            ChoiceActionItem importWorkContract = new ChoiceActionItem();
            importWorkContract.Id = "ImportWorkContract";
            importWorkContract.Caption = "Nhập hợp đồng lao động";
            importWorkContract.ImageName = "BO_HoSo";
            singleChoiceActionList.Items.Add(importWorkContract);

            //2. Hợp đồng khoán
            ChoiceActionItem importHardContract = new ChoiceActionItem();
            importHardContract.Id = "ImportHardContract";
            importHardContract.Caption = "Nhập hợp đồng khoán";
            importHardContract.ImageName = "BO_HoSo";
            singleChoiceActionList.Items.Add(importHardContract);

            //3. Phụ lục hợp đồng
            ChoiceActionItem importAnnexContract = new ChoiceActionItem();
            importHardContract.Id = "ImportAnnexContract";
            importHardContract.Caption = "Nhập phụ lục hợp đồng";
            importHardContract.ImageName = "BO_HoSo";
            singleChoiceActionList.Items.Add(importAnnexContract);
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            //Lấy quản lý hợp đồng hiện tại
            quanLyHopDong = View.CurrentObject as QuanLyHopDong;

            //
            IObjectSpace obs = Application.CreateObjectSpace();

            //1. Hợp đồng lao động
            if (e.SelectedChoiceActionItem.Id.Equals("ImportWorkContract"))
            {
                //
                HopDong_ChonHopDongLamViec obj = obs.CreateObject<HopDong_ChonHopDongLamViec>();
                obj.QuanLyHopDong = obs.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                //
                DetailView detailView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.CreatedView = detailView;
                HopDong_ImportHopDongLamViecController excuteImport = new HopDong_ImportHopDongLamViecController();
                e.ShowViewParameters.Controllers.Add(excuteImport);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            }
            //2. Hợp đồng khoán
            if (e.SelectedChoiceActionItem.Id.Equals("ImportHardContract"))
            {
                //
                HopDong_ChonHopDongKhoan obj = obs.CreateObject<HopDong_ChonHopDongKhoan>();
                obj.QuanLyHopDong = obs.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                //
                DetailView detailView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.CreatedView = detailView;
                HopDong_ImportHopDongKhoanController excuteImport = new HopDong_ImportHopDongKhoanController();
                e.ShowViewParameters.Controllers.Add(excuteImport);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            }

            //3. Phụ lục hợp đồng
            if (e.SelectedChoiceActionItem.Id.Equals("ImportAnnexContract"))
            {
                //
                HopDong_ChonPhuLucHopDong obj = obs.CreateObject<HopDong_ChonPhuLucHopDong>();
                obj.QuanLyHopDong = obs.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                //
                DetailView detailView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.CreatedView = detailView;
                HopDong_ImportPhuLucHopDongController excuteImport = new HopDong_ImportPhuLucHopDongController();
                e.ShowViewParameters.Controllers.Add(excuteImport);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            }        
        }
    }
}
