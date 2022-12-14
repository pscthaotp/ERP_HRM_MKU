using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.NghiepVu.HocPhi.BangHocPhi;
using ERP.Module.NghiepVu.HocPhi.BienLai;

namespace ERP.Module.Web.Controllers.NghiepVu.HocPhi
{
    partial class HocPhi_TaoHoSoHocSinhController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Tạo hồ sơ";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "HocPhi_TaoHoSoHocSinhController";
            this.simpleAction1.ImageName = "BO_Funds";
            this.simpleAction1.TargetObjectType = typeof(TaoHoSoHocSinh);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Tạo hồ sơ học sinh cho trẻ chưa đóng học phí";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // BienLai_ThuPhiLanDauController
            // 
            this.Actions.Add(this.simpleAction1);
            this.Activated += new System.EventHandler(this.HocPhi_TaoHoSoHocSinhController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
