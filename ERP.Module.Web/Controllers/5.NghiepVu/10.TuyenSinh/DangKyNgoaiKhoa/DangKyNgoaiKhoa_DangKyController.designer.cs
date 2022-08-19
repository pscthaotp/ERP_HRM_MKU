using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.TuyenSinh
{
    partial class DangKyNgoaiKhoa_DangKyController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            
            this.btDangKyNK = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
           
            // btDangKyNK
            // 
            this.btDangKyNK.Caption = "Đăng ký ngoại khóa";
            this.btDangKyNK.ConfirmationMessage = null;
            this.btDangKyNK.Id = "btDangKyNK";
            this.btDangKyNK.ImageName = "Action_Import";
            this.btDangKyNK.TargetObjectType = typeof(ERP.Module.NghiepVu.TuyenSinh.TimKiem_NgoaiKhoa);
            this.btDangKyNK.ToolTip = null;
            this.btDangKyNK.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btDangKyNK_Execute);
            // 
            // DangKyNgoaiKhoa_DangKyController
            // 
            this.Actions.Add(this.btDangKyNK);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btDangKyNK;

    }
}
