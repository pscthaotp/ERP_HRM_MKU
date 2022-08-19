using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    partial class NhanVien_DoiTenThongTinNhanVienController
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
            // 
            // 
            this.TargetObjectType = typeof(ThongTinNhanVien);
            this.Activated += new EventHandler(this.NhanVien_DoiTenThongTinNhanVienController_Activated);

        }

        #endregion
    }
}
