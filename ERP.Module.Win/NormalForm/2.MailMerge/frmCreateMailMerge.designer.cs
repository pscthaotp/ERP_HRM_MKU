using ERP.Module.MailMerge;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.NormalForm.MailMerge
{
    partial class frmCreateMailMerge
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
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmCreateMailMerge));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.obj = new DevExpress.Xpo.XPCollection(this.components);
            this.unitOfWork1 = new DevExpress.Xpo.UnitOfWork(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((ISupportInitialize)(this.obj)).BeginInit();
            ((ISupportInitialize)(this.unitOfWork1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Dock = DockStyle.Fill;
            this.layoutControl1.Location = new Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new Size(373, 93);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new Size(340, 13);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Chú ý: Đặt tên biểu mẫu khác với tên các biểu mẫu đã có sẵn";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = DialogResult.OK;
            this.btnSave.Location = new Point(188, 53);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(85, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(277, 53);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(84, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new Binding("EditValue", this.obj, "TenTaiLieu", true, DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit1.Location = new Point(83, 29);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new Size(278, 20);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 4;
            // 
            // obj
            // 
            this.obj.ObjectType = typeof(MailMergeTemplate);
            this.obj.Session = this.unitOfWork1;
            // 
            // unitOfWork1
            // 
            this.unitOfWork1.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.unitOfWork1.TrackPropertiesModifications = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new Size(373, 93);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEdit1;
            this.layoutControlItem1.CustomizationFormText = "Tên biểu mẫu:";
            this.layoutControlItem1.Location = new Point(0, 17);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(353, 24);
            this.layoutControlItem1.Text = "Tên biểu mẫu:";
            this.layoutControlItem1.TextSize = new Size(68, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new Point(0, 41);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(176, 32);
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new Point(265, 41);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new Size(88, 32);
            this.layoutControlItem3.TextSize = new Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new Point(176, 41);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new Size(89, 32);
            this.layoutControlItem4.TextSize = new Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labelControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new Size(353, 17);
            this.layoutControlItem5.TextSize = new Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmCreateMailMerge
            // 
            this.AcceptButton = this.btnSave;
            this.ClientSize = new Size(373, 93);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateMailMerge";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Tạo biểu mẫu";
            ((ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((ISupportInitialize)(this.obj)).EndInit();
            ((ISupportInitialize)(this.unitOfWork1)).EndInit();
            ((ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.Xpo.XPCollection obj;
        private DevExpress.Xpo.UnitOfWork unitOfWork1;
    }
}