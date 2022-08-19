using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.NormalForm.NhanSu
{
    partial class frmChonCanBo
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmChonCanBo));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.donViTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.nhanVienTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.menu = new ContextMenuStrip();
            this.miCheckAll = new ToolStripMenuItem();
            this.miCheckSelected = new ToolStripMenuItem();
            this.toolStripMenuItem1 = new ToolStripSeparator();
            this.miUncheckAll = new ToolStripMenuItem();
            this.miUncheckSelected = new ToolStripMenuItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            ((ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((ISupportInitialize)(this.donViTreeList)).BeginInit();
            ((ISupportInitialize)(this.nhanVienTreeList)).BeginInit();
            this.menu.SuspendLayout();
            ((ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = DockStyle.Fill;
            this.splitContainerControl1.Location = new Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.donViTreeList);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.nhanVienTreeList);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new Size(569, 408);
            this.splitContainerControl1.SplitterPosition = 255;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // donViTreeList
            // 
            this.donViTreeList.Appearance.EvenRow.BackColor = Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(170)))));
            this.donViTreeList.Appearance.EvenRow.Options.UseBackColor = true;
            this.donViTreeList.AppearancePrint.EvenRow.BackColor = Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(170)))));
            this.donViTreeList.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.donViTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.donViTreeList.Dock = DockStyle.Fill;
            this.donViTreeList.Location = new Point(0, 0);
            this.donViTreeList.Name = "donViTreeList";
            this.donViTreeList.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.donViTreeList.OptionsBehavior.Editable = false;
            this.donViTreeList.OptionsBehavior.EnableFiltering = true;
            this.donViTreeList.OptionsView.ShowCheckBoxes = true;
            this.donViTreeList.OptionsView.ShowIndicator = false;
            this.donViTreeList.OptionsView.ShowSummaryFooter = true;
            this.donViTreeList.Size = new Size(255, 408);
            this.donViTreeList.TabIndex = 0;
            this.donViTreeList.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.donViTreeList_BeforeCheckNode);
            this.donViTreeList.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.donViTreeList_AfterCheckNode);
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Danh sách đơn vị";
            this.treeListColumn2.FieldName = "TenBoPhan";
            this.treeListColumn2.MinWidth = 32;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // nhanVienTreeList
            // 
            this.nhanVienTreeList.Appearance.EvenRow.BackColor = Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(170)))));
            this.nhanVienTreeList.Appearance.EvenRow.Options.UseBackColor = true;
            this.nhanVienTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.nhanVienTreeList.ContextMenuStrip = this.menu;
            this.nhanVienTreeList.Dock = DockStyle.Fill;
            this.nhanVienTreeList.Location = new Point(0, 0);
            this.nhanVienTreeList.Name = "nhanVienTreeList";
            this.nhanVienTreeList.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.nhanVienTreeList.OptionsBehavior.Editable = false;
            this.nhanVienTreeList.OptionsBehavior.EnableFiltering = true;
            this.nhanVienTreeList.OptionsSelection.MultiSelect = true;
            this.nhanVienTreeList.OptionsSelection.UseIndicatorForSelection = true;
            this.nhanVienTreeList.OptionsView.ShowCheckBoxes = true;
            this.nhanVienTreeList.OptionsView.ShowSummaryFooter = true;
            this.nhanVienTreeList.Size = new Size(309, 408);
            this.nhanVienTreeList.TabIndex = 0;
            this.nhanVienTreeList.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.nhanVienTreeList_BeforeExpand);
            this.nhanVienTreeList.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.nhanVienTreeList_BeforeCheckNode);
            this.nhanVienTreeList.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.nhanVienTreeList_AfterCheckNode);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Danh sách cán bộ";
            this.treeListColumn1.FieldName = "HoTen";
            this.treeListColumn1.MinWidth = 32;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new ToolStripItem[] {
            this.miCheckAll,
            this.miCheckSelected,
            this.toolStripMenuItem1,
            this.miUncheckAll,
            this.miUncheckSelected});
            this.menu.Name = "menu";
            this.menu.Size = new Size(279, 98);
            // 
            // miCheckAll
            // 
            this.miCheckAll.Image = global::ERP.Module.Properties.Resources.symbol_add;
            this.miCheckAll.Name = "miCheckAll";
            this.miCheckAll.Size = new Size(278, 22);
            this.miCheckAll.Text = "Chọn tất cả cán bộ";
            this.miCheckAll.Click += new EventHandler(this.miCheckAll_Click);
            // 
            // miCheckSelected
            // 
            this.miCheckSelected.Image = global::ERP.Module.Properties.Resources.plus;
            this.miCheckSelected.Name = "miCheckSelected";
            this.miCheckSelected.Size = new Size(278, 22);
            this.miCheckSelected.Text = "Chọn những cán bộ được đánh dấu";
            this.miCheckSelected.Click += new EventHandler(this.miCheckSelected_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(275, 6);
            // 
            // miUncheckAll
            // 
            this.miUncheckAll.Image = global::ERP.Module.Properties.Resources.symbol_delete;
            this.miUncheckAll.Name = "miUncheckAll";
            this.miUncheckAll.Size = new Size(278, 22);
            this.miUncheckAll.Text = "Bỏ chọn tất cả cán bộ";
            this.miUncheckAll.Click += new EventHandler(this.miUncheckAll_Click);
            // 
            // miUncheckSelected
            // 
            this.miUncheckSelected.Image = global::ERP.Module.Properties.Resources.edit_remove;
            this.miUncheckSelected.Name = "miUncheckSelected";
            this.miUncheckSelected.Size = new Size(278, 22);
            this.miUncheckSelected.Text = "Bỏ chọn những cán bộ được đánh dấu";
            this.miUncheckSelected.Click += new EventHandler(this.miUncheckSelected_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnAccept);
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Dock = DockStyle.Bottom;
            this.panelControl1.Location = new Point(0, 379);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new Size(569, 29);
            this.panelControl1.TabIndex = 1;
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = DialogResult.OK;
            this.btnAccept.Location = new Point(406, 2);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Chọn";
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new Point(487, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new Size(75, 23);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new EventHandler(this.btnThoat_Click);
            // 
            // frmChonCanBo
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(569, 408);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChonCanBo";
            this.Text = "Chọn cán bộ";
            this.Load += new EventHandler(this.frmChonCanBo_Load);
            ((ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((ISupportInitialize)(this.donViTreeList)).EndInit();
            ((ISupportInitialize)(this.nhanVienTreeList)).EndInit();
            this.menu.ResumeLayout(false);
            ((ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList donViTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.TreeList nhanVienTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private ContextMenuStrip menu;
        private ToolStripMenuItem miCheckAll;
        private ToolStripMenuItem miCheckSelected;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem miUncheckAll;
        private ToolStripMenuItem miUncheckSelected;
    }
}