using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe
{
    partial class ThongKeLoaiNhanSuController
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointOptions pointOptions1 = new DevExpress.XtraCharts.PointOptions();
            DevExpress.XtraCharts.SimpleDiagram simpleDiagram1 = new DevExpress.XtraCharts.SimpleDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PiePointOptions piePointOptions1 = new DevExpress.XtraCharts.PiePointOptions();
            DevExpress.XtraCharts.PiePointOptions piePointOptions2 = new DevExpress.XtraCharts.PiePointOptions();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chart_Column = new DevExpress.XtraCharts.ChartControl();
            this.menu = new ContextMenuStrip(this.components);
            this.InChart_Column = new ToolStripMenuItem();
            this.InChart_Circle = new ToolStripMenuItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chart_Circle = new DevExpress.XtraCharts.ChartControl();
            ((ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((ISupportInitialize)(this.chart_Column)).BeginInit();
            ((ISupportInitialize)(xyDiagram1)).BeginInit();
            ((ISupportInitialize)(series1)).BeginInit();
            this.menu.SuspendLayout();
            ((ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((ISupportInitialize)(this.chart_Circle)).BeginInit();
            ((ISupportInitialize)(simpleDiagram1)).BeginInit();
            ((ISupportInitialize)(series2)).BeginInit();
            ((ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((ISupportInitialize)(pieSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chart_Column);
            this.groupControl1.Dock = DockStyle.Fill;
            this.groupControl1.Location = new Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new Size(447, 491);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Hình cột";
            // 
            // chart_Column
            // 
            this.chart_Column.ContextMenuStrip = this.menu;
            xyDiagram1.AxisX.Label.Angle = 30;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = true;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.WholeRange.AutoSideMargins = true;
            this.chart_Column.Diagram = xyDiagram1;
            this.chart_Column.Dock = DockStyle.Fill;
            this.chart_Column.Legend.Visible = false;
            this.chart_Column.Location = new Point(2, 21);
            this.chart_Column.Name = "chart_Column";
            series1.ArgumentDataMember = "TenLoaiNhanSu";
            pointOptions1.PointView = DevExpress.XtraCharts.PointView.Argument;
            series1.LegendPointOptions = pointOptions1;
            series1.Name = "Series 1";
            series1.SynchronizePointOptions = false;
            series1.ValueDataMembersSerializable = "SoLuong";
            this.chart_Column.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chart_Column.Size = new Size(443, 468);
            this.chart_Column.TabIndex = 2;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new ToolStripItem[] {
            this.InChart_Column,
            this.InChart_Circle});
            this.menu.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menu.Name = "menu";
            this.menu.Size = new Size(153, 48);
            // 
            // InChart_Column
            // 
            this.InChart_Column.Image = global::ERP.Module.Properties.Resources.Action_Printing_Print_32x32;
            this.InChart_Column.Name = "InChart_Column";
            this.InChart_Column.Size = new Size(152, 22);
            this.InChart_Column.Text = "In biểu đồ cột";
            this.InChart_Column.Click += new EventHandler(this.InChart_Column_Click);
            // 
            // InChart_Circle
            // 
            this.InChart_Circle.Image = global::ERP.Module.Properties.Resources.Action_Printing_Print_32x32;
            this.InChart_Circle.Name = "InChart_Circle";
            this.InChart_Circle.Size = new Size(152, 22);
            this.InChart_Circle.Text = "In biểu đồ tròn";
            this.InChart_Circle.Click += new EventHandler(this.InChart_Circle_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Location = new Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new Size(880, 491);
            this.splitContainerControl1.SplitterPosition = 447;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chart_Circle);
            this.groupControl2.Dock = DockStyle.Fill;
            this.groupControl2.Location = new Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new Size(428, 491);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Hình tròn";
            // 
            // chart_Circle
            // 
            this.chart_Circle.ContextMenuStrip = this.menu;
            simpleDiagram1.EqualPieSize = false;
            this.chart_Circle.Diagram = simpleDiagram1;
            this.chart_Circle.Dock = DockStyle.Fill;
            this.chart_Circle.Location = new Point(2, 21);
            this.chart_Circle.Name = "chart_Circle";
            series2.ArgumentDataMember = "TenLoaiNhanSu";
            piePointOptions1.PercentOptions.ValueAsPercent = false;
            piePointOptions1.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number;
            piePointOptions1.ValueNumericOptions.Precision = 0;
            pieSeriesLabel1.PointOptions = piePointOptions1;
            series2.Label = pieSeriesLabel1;
            piePointOptions2.PercentOptions.ValueAsPercent = false;
            piePointOptions2.PointView = DevExpress.XtraCharts.PointView.Argument;
            piePointOptions2.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number;
            piePointOptions2.ValueNumericOptions.Precision = 0;
            series2.LegendPointOptions = piePointOptions2;
            series2.Name = "Series 2";
            series2.SynchronizePointOptions = false;
            series2.ValueDataMembersSerializable = "SoLuong";
            pieSeriesView1.RuntimeExploding = true;
            series2.View = pieSeriesView1;
            this.chart_Circle.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chart_Circle.Size = new Size(424, 468);
            this.chart_Circle.TabIndex = 1;
            // 
            // ThongKeLoaiNhanSuController
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ThongKeLoaiNhanSuController";
            this.Size = new Size(883, 494);
            this.Load += new EventHandler(this.ThongKeLoaiNhanSuController_Load);
            ((ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((ISupportInitialize)(xyDiagram1)).EndInit();
            ((ISupportInitialize)(series1)).EndInit();
            ((ISupportInitialize)(this.chart_Column)).EndInit();
            this.menu.ResumeLayout(false);
            ((ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((ISupportInitialize)(simpleDiagram1)).EndInit();
            ((ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((ISupportInitialize)(pieSeriesView1)).EndInit();
            ((ISupportInitialize)(series2)).EndInit();
            ((ISupportInitialize)(this.chart_Circle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraCharts.ChartControl chart_Column;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraCharts.ChartControl chart_Circle;
        private ContextMenuStrip menu;
        private ToolStripMenuItem InChart_Column;
        private ToolStripMenuItem InChart_Circle;

    }
}
