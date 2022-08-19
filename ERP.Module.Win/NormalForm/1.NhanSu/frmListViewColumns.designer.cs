using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.NormalForm.NhanSu
{
    partial class frmListViewColumns
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmListViewColumns));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.ckChonTatCa = new DevExpress.XtraEditors.CheckEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((ISupportInitialize)(this.ckChonTatCa.Properties)).BeginInit();
            ((ISupportInitialize)(this.gridControl1)).BeginInit();
            ((ISupportInitialize)(this.gridView1)).BeginInit();
            ((ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(250, 516);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Chọn";
            // 
            // ckChonTatCa
            // 
            this.ckChonTatCa.Location = new Point(13, 13);
            this.ckChonTatCa.Name = "ckChonTatCa";
            this.ckChonTatCa.Properties.Caption = "Chọn tất cả";
            this.ckChonTatCa.Size = new Size(132, 19);
            this.ckChonTatCa.TabIndex = 0;
            this.ckChonTatCa.CheckedChanged += new EventHandler(this.ckChonTatCa_CheckedChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = DockStyle.Fill;
            this.gridControl1.Location = new Point(2, 21);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(389, 449);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new Point(15, 38);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new Size(393, 472);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Danh sách cột";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(331, 516);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Thoát";
            // 
            // frmListViewColumns
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(420, 551);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.ckChonTatCa);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new Size(436, 589);
            this.MinimumSize = new Size(436, 589);
            this.Name = "frmListViewColumns";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Chọn cột hiển thị";
            this.Load += new EventHandler(this.frmListViewColumns_Load);
            ((ISupportInitialize)(this.ckChonTatCa.Properties)).EndInit();
            ((ISupportInitialize)(this.gridControl1)).EndInit();
            ((ISupportInitialize)(this.gridView1)).EndInit();
            ((ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.CheckEdit ckChonTatCa;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}