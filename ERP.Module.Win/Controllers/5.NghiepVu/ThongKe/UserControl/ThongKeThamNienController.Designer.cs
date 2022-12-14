using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe
{
    partial class ThongKeThamNienController
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            //DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThongKeDoTuoiController));
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            //DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chart_Column = new DevExpress.XtraCharts.ChartControl();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.InChart_Column = new System.Windows.Forms.ToolStripMenuItem();
            this.InChart_Circle = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chart_Circle = new DevExpress.XtraCharts.ChartControl();
            this.chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Column)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Circle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chart_Column);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(477, 491);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Hình cột";
            // 
            // chart_Column
            // 
            this.chart_Column.ContextMenuStrip = this.menu;
            this.chart_Column.DataBindings = null;
            xyDiagram1.AxisX.Label.Angle = 30;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chart_Column.Diagram = xyDiagram1;
            this.chart_Column.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_Column.Legend.Name = "Default Legend";
            this.chart_Column.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chart_Column.Location = new System.Drawing.Point(2, 20);
            this.chart_Column.Name = "chart_Column";
            series1.ArgumentDataMember = "ThamNienCT";
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.LegendTextPattern = "{A}";
            series1.Name = "Series 1";
            series1.ValueDataMembersSerializable = "SoLuongTNCT";
            sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(86)))), ((int)(((byte)(111)))));
            series1.View = sideBySideBarSeriesView1;
            this.chart_Column.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chart_Column.Size = new System.Drawing.Size(473, 469);
            this.chart_Column.TabIndex = 2;
            chartTitle1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chart_Column.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InChart_Column,
            this.InChart_Circle});
            this.menu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(153, 48);
            // 
            // InChart_Column
            // 
            this.InChart_Column.Image = ((System.Drawing.Image)(resources.GetObject("InChart_Column.Image")));
            this.InChart_Column.Name = "InChart_Column";
            this.InChart_Column.Size = new System.Drawing.Size(152, 22);
            this.InChart_Column.Text = "In biểu đồ cột";
            this.InChart_Column.Click += new System.EventHandler(this.InChart_Column_Click);
            // 
            // InChart_Circle
            // 
            this.InChart_Circle.Image = ((System.Drawing.Image)(resources.GetObject("InChart_Circle.Image")));
            this.InChart_Circle.Name = "InChart_Circle";
            this.InChart_Circle.Size = new System.Drawing.Size(152, 22);
            this.InChart_Circle.Text = "In biểu đồ tròn";
            this.InChart_Circle.Click += new System.EventHandler(this.InChart_Circle_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(880, 491);
            this.splitContainerControl1.SplitterPosition = 477;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chart_Circle);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(398, 491);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Hình tròn";
            // 
            // chart_Circle
            // 
            this.chart_Circle.AppearanceNameSerializable = "The Trees";
            this.chart_Circle.ContextMenuStrip = this.menu;
            this.chart_Circle.DataBindings = null;
            this.chart_Circle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_Circle.Legend.Name = "Default Legend";
            this.chart_Circle.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chart_Circle.Location = new System.Drawing.Point(2, 20);
            this.chart_Circle.Name = "chart_Circle";
            this.chart_Circle.PaletteName = "Aspect";
            series2.ArgumentDataMember = "ThamNienCT";
            pieSeriesLabel1.TextPattern = "{A}: {V:N1}%";
            series2.Label = pieSeriesLabel1;
            series2.LegendTextPattern = "{A}:{VP:N1}";
            series2.Name = "Series 2";
            series2.ValueDataMembersSerializable = "PhanTramTNCT";
            pieSeriesView1.RuntimeExploding = true;
            series2.View = pieSeriesView1;
            this.chart_Circle.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chart_Circle.Size = new System.Drawing.Size(394, 469);
            this.chart_Circle.TabIndex = 1;
            chartTitle2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chart_Circle.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle2});
            // 
            // ThongKeDoTuoiController
            // 
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ThongKeThamNienController";
            this.Size = new System.Drawing.Size(883, 494);
            this.Load += new System.EventHandler(this.ThongKeThamNienController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Column)).EndInit();
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Circle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraCharts.ChartControl chart_Column;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraCharts.ChartControl chart_Circle;
        private ContextMenuStrip menu;
        private DevExpress.XtraCharts.ChartTitle chartTitle1;
        private DevExpress.XtraCharts.ChartTitle chartTitle2;
        private ToolStripMenuItem InChart_Column;
        private ToolStripMenuItem InChart_Circle;

    }
}
