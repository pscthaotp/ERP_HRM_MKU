using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace ERP.Module.Win.Diagrams
{
    partial class QuyTrinhNghiViecController
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.ContentImage = global::ERP.Module.Win.Properties.Resources.QuyTrinhNghiViec;
            this.panelControl1.Dock = DockStyle.Fill;
            this.panelControl1.Location = new Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new Size(828, 474);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.BorderStyle = BorderStyles.NoBorder;
            // 
            // QuyTrinhNghiViecController
            // 
            this.Controls.Add(this.panelControl1);
            this.Name = "QuyTrinhNghiViecController";
            ((ISupportInitialize)(this.panelControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PanelControl panelControl1;

    }
}
