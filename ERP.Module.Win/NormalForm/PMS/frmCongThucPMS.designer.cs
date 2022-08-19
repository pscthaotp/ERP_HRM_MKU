using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.NormalForm.PMS
{
    partial class frmCongThucPMS
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmCongThucPMS));
            this.trFields = new DevExpress.XtraTreeList.TreeList();
            this.colCaption = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colHienThi = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((ISupportInitialize)(this.trFields)).BeginInit();
            this.SuspendLayout();
            // 
            // trFields
            // 
            this.trFields.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            this.trFields.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCaption,
            this.colHienThi});
            this.trFields.Location = new Point(0, 0);
            this.trFields.Name = "trFields";
            this.trFields.OptionsBehavior.Editable = false;
            this.trFields.OptionsView.ShowIndicator = false;
            this.trFields.Size = new Size(380, 296);
            this.trFields.TabIndex = 0;
            this.trFields.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.trFields_BeforeExpand);
            this.trFields.DoubleClick += new EventHandler(this.trFields_DoubleClick);
            // 
            // colCaption
            // 
            this.colCaption.Caption = "Dữ liệu";
            this.colCaption.FieldName = "Caption";
            this.colCaption.Name = "colCaption";
            this.colCaption.OptionsColumn.AllowEdit = false;
            this.colCaption.Visible = true;
            this.colCaption.VisibleIndex = 0;
            this.colCaption.Width = 150;
            // 
            // colHienThi
            // 
            this.colHienThi.Caption = "Hiển thị";
            this.colHienThi.FieldName = "HienThi";
            this.colHienThi.Name = "colHienThi";
            this.colHienThi.OptionsColumn.AllowEdit = false;
            this.colHienThi.Visible = true;
            this.colHienThi.VisibleIndex = 1;
            this.colHienThi.Width = 70;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(212, 301);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(78, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Đồng ý";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(296, 301);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(78, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Không";
            // 
            // frmFormula
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(380, 329);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.trFields);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFormulaPMS";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Chọn dữ liệu";
            this.Load += new EventHandler(this.frmCongThucPMS_Load);
            ((ISupportInitialize)(this.trFields)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList trFields;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCaption;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colHienThi;
    }
}