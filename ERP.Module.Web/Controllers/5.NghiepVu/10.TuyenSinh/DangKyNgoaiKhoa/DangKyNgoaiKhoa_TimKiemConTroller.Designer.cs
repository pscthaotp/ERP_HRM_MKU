using ERP.Module.NghiepVu.TuyenSinh;
namespace ERP.Module.Win.Controllers.NghiepVu.TuyenSinh
{
    partial class DangKyNgoaiKhoa_TimKiemConTroller
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
            this.btTimKiem = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btTimKiem
            // 
            this.btTimKiem.Caption = "Tìm kiếm";
            this.btTimKiem.ConfirmationMessage = null;
            this.btTimKiem.Id = "btTimKiem";
            this.btTimKiem.ImageName = "BO_LocDuLieu";
            this.btTimKiem.TargetObjectType = typeof(ERP.Module.NghiepVu.TuyenSinh.TimKiem_NgoaiKhoa);
            this.btTimKiem.ToolTip = null;
            this.btTimKiem.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btTimKiem_Execute);
            // 
            // DangKyNgoaiKhoa_TimKiem
            // 
            this.Actions.Add(this.btTimKiem);
            this.TargetObjectType = typeof(ERP.Module.NghiepVu.TuyenSinh.TimKiem_NgoaiKhoa);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btTimKiem;
    }
}
