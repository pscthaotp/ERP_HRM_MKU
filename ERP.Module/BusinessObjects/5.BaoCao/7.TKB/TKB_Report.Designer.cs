namespace ERP.Module.BusinessObjects.BaoCao.TKB
{
    partial class TKB_Report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dayViewTimeCells1 = new DevExpress.XtraScheduler.Reporting.DayViewTimeCells();
            this.horizontalDateHeaders1 = new DevExpress.XtraScheduler.Reporting.HorizontalDateHeaders();
            this.reportDayView1 = new DevExpress.XtraScheduler.Reporting.ReportDayView();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.timeIntervalInfo1 = new DevExpress.XtraScheduler.Reporting.TimeIntervalInfo();
            this.dayViewTimeRuler1 = new DevExpress.XtraScheduler.Reporting.DayViewTimeRuler();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.reportDayView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // dayViewTimeCells1
            // 
            this.dayViewTimeCells1.Dpi = 100F;
            this.dayViewTimeCells1.HorizontalHeaders = this.horizontalDateHeaders1;
            this.dayViewTimeCells1.LocationFloat = new DevExpress.Utils.PointFloat(49.99982F, 121.5416F);
            this.dayViewTimeCells1.Name = "dayViewTimeCells1";
            this.dayViewTimeCells1.SizeF = new System.Drawing.SizeF(743F, 692.0001F);
            this.dayViewTimeCells1.View = this.reportDayView1;
            this.dayViewTimeCells1.VisibleTimeSnapMode = false;
            // 
            // horizontalDateHeaders1
            // 
            this.horizontalDateHeaders1.Dpi = 100F;
            this.horizontalDateHeaders1.LocationFloat = new DevExpress.Utils.PointFloat(49.99979F, 84.375F);
            this.horizontalDateHeaders1.Name = "horizontalDateHeaders1";
            this.horizontalDateHeaders1.SizeF = new System.Drawing.SizeF(743.0001F, 37.16663F);
            this.horizontalDateHeaders1.View = this.reportDayView1;
            // 
            // reportDayView1
            // 
            this.reportDayView1.VisibleDayCount = 7;
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Dpi = 100F;
            this.bottomMarginBand1.HeightF = 100F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Dpi = 100F;
            this.topMarginBand1.HeightF = 28F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // timeIntervalInfo1
            // 
            this.timeIntervalInfo1.Dpi = 100F;
            this.timeIntervalInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.timeIntervalInfo1.Name = "timeIntervalInfo1";
            this.timeIntervalInfo1.SizeF = new System.Drawing.SizeF(652.4166F, 65.625F);
            this.timeIntervalInfo1.TimeCells = this.dayViewTimeCells1;
            // 
            // dayViewTimeRuler1
            // 
            this.dayViewTimeRuler1.Corners.Top = 50;
            this.dayViewTimeRuler1.Dpi = 100F;
            this.dayViewTimeRuler1.LocationFloat = new DevExpress.Utils.PointFloat(7.947286E-05F, 84.375F);
            this.dayViewTimeRuler1.Name = "dayViewTimeRuler1";
            this.dayViewTimeRuler1.SizeF = new System.Drawing.SizeF(50F, 729.1664F);
            this.dayViewTimeRuler1.TimeCells = this.dayViewTimeCells1;
            this.dayViewTimeRuler1.TimeRuler.ShowMinutes = true;
            this.dayViewTimeRuler1.View = this.reportDayView1;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.dayViewTimeCells1,
            this.horizontalDateHeaders1,
            this.dayViewTimeRuler1,
            this.timeIntervalInfo1});
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 899F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel1});
            this.ReportHeader.Dpi = 100F;
            this.ReportHeader.HeightF = 108.4583F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 100F;
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.95832F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(294.7917F, 23.00002F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 100F;
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 72.95834F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(294.7917F, 23.00002F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 100F;
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001430511F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(792.9998F, 38.54167F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "THỜI KHÓA BIỂU";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TKB_Report
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.ReportHeader});
            this.Margins = new System.Drawing.Printing.Margins(25, 32, 28, 100);
            this.Version = "16.2";
            this.Views.AddRange(new DevExpress.XtraScheduler.Reporting.ReportViewBase[] {
            this.reportDayView1});
            ((System.ComponentModel.ISupportInitialize)(this.reportDayView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraScheduler.Reporting.DayViewTimeCells dayViewTimeCells1;
        private DevExpress.XtraScheduler.Reporting.HorizontalDateHeaders horizontalDateHeaders1;
        private DevExpress.XtraScheduler.Reporting.ReportDayView reportDayView1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraScheduler.Reporting.TimeIntervalInfo timeIntervalInfo1;
        private DevExpress.XtraScheduler.Reporting.DayViewTimeRuler dayViewTimeRuler1;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;




    }
}
