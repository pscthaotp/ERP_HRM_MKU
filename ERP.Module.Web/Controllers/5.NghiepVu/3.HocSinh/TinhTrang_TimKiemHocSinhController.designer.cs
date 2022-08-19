using ERP.Module.NghiepVu.HocSinh.HocSinhs;
using ERP.Module.NonPersistentObjects.HocSinh;
using System;
using System.ComponentModel;

namespace ERP.Module.Web.Controllers.NghiepVu.HocSinh
{
    partial class TinhTrang_TimKiemHocSinhController
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
            // HoSo_TrichNhanVienController
            // 
            this.TargetObjectType = typeof(TinhTrang_CapNhatTinhTrang);
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.ViewControlsCreated += new EventHandler(this.TinhTrang_TimKiemHocSinhController_ViewControlsCreated);

        }

        #endregion
    }
}
