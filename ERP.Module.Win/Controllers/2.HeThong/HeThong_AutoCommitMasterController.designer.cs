using ERP.Module.HeThong;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.HeThong
{
    partial class HeThong_AutoCommitMasterController
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
            this.components = new Container();
            //this.TargetObjectType = typeof(QuanLyTuyenDung);           
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);           
            
        }

        #endregion       
    }
}
