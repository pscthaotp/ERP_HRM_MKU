using ERP.Module.MailMerge;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.NormalForm.MailMerge
{
    partial class frmMailMergeManager
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMailMergeManager));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnMacDinh = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.listTemplate = new DevExpress.Xpo.XPCollection(this.components);
            this.unitOfWork1 = new DevExpress.Xpo.UnitOfWork(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSuDungMacDinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaQuanLy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenTaiLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((ISupportInitialize)(this.gridControl1)).BeginInit();
            ((ISupportInitialize)(this.listTemplate)).BeginInit();
            ((ISupportInitialize)(this.unitOfWork1)).BeginInit();
            ((ISupportInitialize)(this.gridView1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnMacDinh);
            this.layoutControl1.Controls.Add(this.btnDelete);
            this.layoutControl1.Controls.Add(this.btnAdd);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Dock = DockStyle.Fill;
            this.layoutControl1.Location = new Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new Size(559, 364);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnMacDinh
            // 
            this.btnMacDinh.Location = new Point(444, 64);
            this.btnMacDinh.Name = "btnMacDinh";
            this.btnMacDinh.Size = new Size(103, 22);
            this.btnMacDinh.StyleController = this.layoutControl1;
            this.btnMacDinh.TabIndex = 11;
            this.btnMacDinh.Text = "Mặc định";
            this.btnMacDinh.Click += new EventHandler(this.btnMacDinh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new Point(444, 38);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(103, 22);
            this.btnDelete.StyleController = this.layoutControl1;
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new Point(444, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(103, 22);
            this.btnAdd.StyleController = this.layoutControl1;
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.listTemplate;
            this.gridControl1.Location = new Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(428, 340);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // listTemplate
            // 
            this.listTemplate.DeleteObjectOnRemove = true;
            this.listTemplate.ObjectType = typeof(MailMergeTemplate);
            this.listTemplate.Session = this.unitOfWork1;
            // 
            // unitOfWork1
            // 
            this.unitOfWork1.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.unitOfWork1.TrackPropertiesModifications = false;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSuDungMacDinh,
            this.colMaQuanLy,
            this.colTenTaiLieu});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colSuDungMacDinh
            // 
            this.colSuDungMacDinh.Caption = "Mặc định";
            this.colSuDungMacDinh.FieldName = "SuDungMacDinh";
            this.colSuDungMacDinh.Name = "colSuDungMacDinh";
            this.colSuDungMacDinh.Visible = true;
            this.colSuDungMacDinh.VisibleIndex = 0;
            // 
            // colMaQuanLy
            // 
            this.colMaQuanLy.Caption = "Mã quản lý";
            this.colMaQuanLy.FieldName = "MaQuanLy";
            this.colMaQuanLy.Name = "colMaQuanLy";
            this.colMaQuanLy.Visible = true;
            this.colMaQuanLy.VisibleIndex = 1;
            // 
            // colTenTaiLieu
            // 
            this.colTenTaiLieu.Caption = "Tên biểu mẫu";
            this.colTenTaiLieu.FieldName = "TenTaiLieu";
            this.colTenTaiLieu.Name = "colTenTaiLieu";
            this.colTenTaiLieu.Visible = true;
            this.colTenTaiLieu.VisibleIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new Point(444, 90);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(103, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(444, 116);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(103, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new Size(559, 364);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new Size(432, 344);
            this.layoutControlItem5.TextSize = new Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new Point(432, 78);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new Size(107, 26);
            this.layoutControlItem4.TextSize = new Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new Point(432, 130);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(107, 214);
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new Point(432, 104);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(107, 26);
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnAdd;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new Point(432, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(107, 26);
            this.layoutControlItem1.TextSize = new Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnDelete;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new Point(432, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new Size(107, 26);
            this.layoutControlItem3.TextSize = new Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnMacDinh;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new Point(432, 52);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new Size(107, 26);
            this.layoutControlItem6.TextSize = new Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // frmMailMergeManager
            // 
            this.AcceptButton = this.btnSave;
            this.ClientSize = new Size(559, 364);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMailMergeManager";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý biểu mẫu";
            this.Load += new EventHandler(this.frmMailMergeManager_Load);
            ((ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((ISupportInitialize)(this.gridControl1)).EndInit();
            ((ISupportInitialize)(this.listTemplate)).EndInit();
            ((ISupportInitialize)(this.unitOfWork1)).EndInit();
            ((ISupportInitialize)(this.gridView1)).EndInit();
            ((ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.Xpo.XPCollection listTemplate;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.Xpo.UnitOfWork unitOfWork1;
        private DevExpress.XtraGrid.Columns.GridColumn colSuDungMacDinh;
        private DevExpress.XtraGrid.Columns.GridColumn colMaQuanLy;
        private DevExpress.XtraGrid.Columns.GridColumn colTenTaiLieu;
        private DevExpress.XtraEditors.SimpleButton btnMacDinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}