using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.NormalForm.System
{
    partial class frmNhatKyDuLieu
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmNhatKyDuLieu));
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnTim = new DevExpress.XtraEditors.SimpleButton();
            this.gridCtrl_NhatKyDuLieu = new DevExpress.XtraGrid.GridControl();
            this.grid_NhatKyDuLieu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModifiedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPropertyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOldValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((ISupportInitialize)(this.frmNhatKyDuLieulayoutControl1ConvertedLayout)).BeginInit();
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.SuspendLayout();
            ((ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((ISupportInitialize)(this.gridCtrl_NhatKyDuLieu)).BeginInit();
            ((ISupportInitialize)(this.grid_NhatKyDuLieu)).BeginInit();
            ((ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // frmNhatKyDuLieulayoutControl1ConvertedLayout
            // 
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.Controls.Add(this.dateDenNgay);
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.Controls.Add(this.dateTuNgay);
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.Controls.Add(this.btnTim);
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.Controls.Add(this.gridCtrl_NhatKyDuLieu);
            resources.ApplyResources(this.frmNhatKyDuLieulayoutControl1ConvertedLayout, "frmNhatKyDuLieulayoutControl1ConvertedLayout");
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.Name = "frmNhatKyDuLieulayoutControl1ConvertedLayout";
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(512, 250, 450, 400);
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            // 
            // dateDenNgay
            // 
            resources.ApplyResources(this.dateDenNgay, "dateDenNgay");
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateDenNgay.Properties.Buttons"))))});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateDenNgay.Properties.CalendarTimeProperties.Buttons"))))});
            this.dateDenNgay.Properties.Mask.EditMask = resources.GetString("dateDenNgay.Properties.Mask.EditMask");
            this.dateDenNgay.StyleController = this.frmNhatKyDuLieulayoutControl1ConvertedLayout;
            // 
            // dateTuNgay
            // 
            resources.ApplyResources(this.dateTuNgay, "dateTuNgay");
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateTuNgay.Properties.Buttons"))))});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateTuNgay.Properties.CalendarTimeProperties.Buttons"))))});
            this.dateTuNgay.Properties.Mask.EditMask = resources.GetString("dateTuNgay.Properties.Mask.EditMask");
            this.dateTuNgay.StyleController = this.frmNhatKyDuLieulayoutControl1ConvertedLayout;
            // 
            // btnTim
            // 
            resources.ApplyResources(this.btnTim, "btnTim");
            this.btnTim.Name = "btnTim";
            this.btnTim.StyleController = this.frmNhatKyDuLieulayoutControl1ConvertedLayout;
            this.btnTim.Click += new EventHandler(this.btnTim_Click);
            // 
            // gridCtrl_NhatKyDuLieu
            // 
            resources.ApplyResources(this.gridCtrl_NhatKyDuLieu, "gridCtrl_NhatKyDuLieu");
            this.gridCtrl_NhatKyDuLieu.MainView = this.grid_NhatKyDuLieu;
            this.gridCtrl_NhatKyDuLieu.Name = "gridCtrl_NhatKyDuLieu";
            this.gridCtrl_NhatKyDuLieu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grid_NhatKyDuLieu});
            // 
            // grid_NhatKyDuLieu
            // 
            this.grid_NhatKyDuLieu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.colModifiedOn,
            this.colPropertyName,
            this.colOldValue,
            this.colNewValue,
            this.colDescription});
            this.grid_NhatKyDuLieu.GridControl = this.gridCtrl_NhatKyDuLieu;
            this.grid_NhatKyDuLieu.Name = "grid_NhatKyDuLieu";
            // 
            // colUserName
            // 
            resources.ApplyResources(this.colUserName, "colUserName");
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            // 
            // colModifiedOn
            // 
            resources.ApplyResources(this.colModifiedOn, "colModifiedOn");
            this.colModifiedOn.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
            this.colModifiedOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colModifiedOn.FieldName = "ModifiedOn";
            this.colModifiedOn.Name = "colModifiedOn";
            // 
            // colPropertyName
            // 
            resources.ApplyResources(this.colPropertyName, "colPropertyName");
            this.colPropertyName.FieldName = "PropertyName";
            this.colPropertyName.Name = "colPropertyName";
            // 
            // colOldValue
            // 
            resources.ApplyResources(this.colOldValue, "colOldValue");
            this.colOldValue.FieldName = "OldValue";
            this.colOldValue.Name = "colOldValue";
            // 
            // colNewValue
            // 
            resources.ApplyResources(this.colNewValue, "colNewValue");
            this.colNewValue.FieldName = "NewValue";
            this.colNewValue.Name = "colNewValue";
            // 
            // colDescription
            // 
            resources.ApplyResources(this.colDescription, "colDescription");
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlGroup2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new Size(854, 551);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnTim;
            this.layoutControlItem3.Location = new Point(368, 0);
            this.layoutControlItem3.Name = "btnTimitem";
            this.layoutControlItem3.Size = new Size(110, 26);
            this.layoutControlItem3.TextSize = new Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new Point(0, 26);
            this.layoutControlGroup2.Name = "groupControl1item";
            this.layoutControlGroup2.Size = new Size(834, 505);
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridCtrl_NhatKyDuLieu;
            this.layoutControlItem6.Location = new Point(0, 0);
            this.layoutControlItem6.Name = "gridCtrl_NhatKyDuLieuitem";
            this.layoutControlItem6.Size = new Size(808, 459);
            this.layoutControlItem6.TextSize = new Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateDenNgay;
            this.layoutControlItem1.Location = new Point(179, 0);
            this.layoutControlItem1.MaxSize = new Size(189, 26);
            this.layoutControlItem1.MinSize = new Size(189, 26);
            this.layoutControlItem1.Name = "dateDenNgayitem";
            this.layoutControlItem1.Size = new Size(189, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new Size(47, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateTuNgay;
            this.layoutControlItem2.Location = new Point(0, 0);
            this.layoutControlItem2.MaxSize = new Size(179, 26);
            this.layoutControlItem2.MinSize = new Size(179, 26);
            this.layoutControlItem2.Name = "dateTuNgayitem";
            this.layoutControlItem2.Size = new Size(179, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new Size(47, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new Point(478, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(356, 26);
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Money Twins";
            // 
            // frmNhatKyDuLieu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.frmNhatKyDuLieulayoutControl1ConvertedLayout);
            this.Name = "frmNhatKyDuLieu";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.frmNhatKyDuLieu_Load);
            ((ISupportInitialize)(this.frmNhatKyDuLieulayoutControl1ConvertedLayout)).EndInit();
            this.frmNhatKyDuLieulayoutControl1ConvertedLayout.ResumeLayout(false);
            ((ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((ISupportInitialize)(this.gridCtrl_NhatKyDuLieu)).EndInit();
            ((ISupportInitialize)(this.grid_NhatKyDuLieu)).EndInit();
            ((ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gridCtrl_NhatKyDuLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView grid_NhatKyDuLieu;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colModifiedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colOldValue;
        private DevExpress.XtraGrid.Columns.GridColumn colNewValue;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraEditors.SimpleButton btnTim;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraLayout.LayoutControl frmNhatKyDuLieulayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraGrid.Columns.GridColumn colPropertyName;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}