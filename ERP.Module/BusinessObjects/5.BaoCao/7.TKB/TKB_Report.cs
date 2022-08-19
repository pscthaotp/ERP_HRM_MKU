using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraReports.UI;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.Reporting;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TKB;
using ERP.Module.Enum.TKB;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
using ERP.Module.NghiepVu.TKB.ThoiKhoaBieu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace ERP.Module.BusinessObjects.BaoCao.TKB
{
    public partial class TKB_Report : DevExpress.XtraScheduler.Reporting.XtraSchedulerReport
    {
        Session _ses;
        bool _check = false;
        ThoiKhoaBieu _tkb;

        public TKB_Report()
        {
            InitializeComponent();
        }

        public void DesingReport(Session ses)
        {
            _ses = ses;
            SchedulerAdapter.FirstDayOfWeek = DevExpress.XtraScheduler.FirstDayOfWeek.Monday;
            PrintColorSchema = PrintColorSchema.FullColor;
            //dayViewTimeRuler1.TimeCells.VisibleTime = new DevExpress.XtraScheduler.TimeOfDayInterval(new TimeSpan(6, 00, 0), new TimeSpan(21, 00, 0));
            timeIntervalInfo1.CustomizeText += timeIntervalInfo1_CustomizeText;
            
            //Ân giờ bắt đầu và kết thúc
            dayViewTimeCells1.AppointmentDisplayOptions.EndTimeVisibility = AppointmentTimeVisibility.Never;
            dayViewTimeCells1.AppointmentDisplayOptions.StartTimeVisibility = AppointmentTimeVisibility.Never;

            //
            dayViewTimeRuler1.CustomDrawDayViewTimeRuler += dayViewTimeRuler1_CustomDrawDayViewTimeRuler;
        }

        private void dayViewTimeRuler1_CustomDrawDayViewTimeRuler(object sender, CustomDrawObjectEventArgs e)
        {
            TimeRulerViewInfo info = e.ObjectInfo as TimeRulerViewInfo;

            CriteriaOperator filter = CriteriaOperator.Parse("LoaiKhungGioTKB.LoaiTKB != ?", LoaiTKBEnum.ChamDuong);

            XPCollection<KhungGioTKB> KGList = new XPCollection<KhungGioTKB>(_ses, filter);
            KGList.Sorting.Add(new SortProperty("TuGio", SortingDirection.Ascending));

            List<ViewInfoItem> list = new List<ViewInfoItem>();
            foreach (var item1 in info.Items)
            {
                ViewInfoTextItem viewInfoText = item1 as ViewInfoTextItem;
                if (viewInfoText != null)
                {
                    list.Add(viewInfoText);
                }
            }

            // Draw the TimeRuler as usual, but with empty captions.
            e.DrawDefault();

            // Duyệt danh sách Khung giờ
            foreach (var item in KGList)
            {
                KhungGioTKB tkb = item;
                //
                bool tuGio = false;
                bool denGio = false;
                //
                int StttuGio = 0;
                int SttdenGio = 0;
                //
                int temp = 0;

                for (int i = 0; i < list.Count / 3; i++)
                {
                    ViewInfoTextItem text = (ViewInfoTextItem)list[i];
                    if (Convert.ToInt32(text.Text) != null && Convert.ToInt32(text.Text) <= Convert.ToInt32(item.TuGio.Hours) && tuGio == false)
                    {
                        StttuGio = i;
                    }
                    else
                        tuGio = true;

                    if (Convert.ToInt32(text.Text) != null && (Convert.ToInt32(text.Text) + (12 * temp)) <= Convert.ToInt32(item.DenGio.Hours) && denGio == false)
                    {
                        SttdenGio = i;
                    }
                    else
                        denGio = true;

                    if (Convert.ToInt32(text.Text) == 12 && temp == 0)
                        temp = 1;

                }

                ViewInfoItem x1;
                ViewInfoItem x2;

                if (item.TuGio.Minutes < 30)
                    x1 = (ViewInfoTextItem)list[(list.Count/3) + (StttuGio * 2)];
                else
                    x1 = (ViewInfoTextItem)list[(list.Count / 3) + (StttuGio * 2) + 1];

                if (item.DenGio.Minutes < 30)
                    x2 = (ViewInfoTextItem)list[(list.Count / 3) + (SttdenGio * 2)];
                else
                    x2 = (ViewInfoTextItem)list[(list.Count / 3) + (SttdenGio * 2) + 1];

                //// Create string to draw.
                String drawString = tkb.LoaiKhungGioTKB.TenLoaiKhungGio;

                //// Create font and brush.
                //Font drawFont = new Font("Arial", 10);
                //SolidBrush drawBrush = new SolidBrush(Color.Black);

                //// Create point for upper-left corner of drawing.
                ////PointF drawPoint = new PointF(x.Bounds.X, x.Bounds.Y);

                //Rectangle displayRectangle = new Rectangle(new Point(x.Bounds.X, x.Bounds.Y), new Size(y.Bounds.X + 30, y.Bounds.Y));

                //// Draw string to screen.
                //e.Cache.Graphics.DrawString(drawString, drawFont, drawBrush, displayRectangle);

                Rectangle displayRectangle = new Rectangle(new Point(x1.Bounds.X, x1.Bounds.Y), new Size(40, x2.Bounds.Y - x1.Bounds.Y));

                e.Cache.FillRectangle(e.Cache.GetSolidBrush(Color.LightYellow), displayRectangle);

                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    e.Cache.DrawVString(drawString,
                        e.Cache.GetFont(new Font(Font.FontFamily, 8, FontStyle.Bold), FontStyle.Bold),
                        e.Cache.GetSolidBrush(Color.Gray), displayRectangle, sf, 0); //e.Bounds, sf, -90);


                }
            }

            // Cancel default painting procedure.
            e.Handled = true;

        }

        void timeIntervalInfo1_CustomizeText(object sender, TextCustomizingEventArgs e)
        {
            TimeIntervalTextCustomizingEventArgs args = (TimeIntervalTextCustomizingEventArgs)e;

            CriteriaOperator filter = CriteriaOperator.Parse("ThoiKhoaBieu.HocSinh =? and Ngay =? and LoaiTKB =?", _tkb.HocSinh.Oid, args.Interval.Start, LoaiTKBEnum.GiaoDuc);
            ChiTietThoiKhoaBieu chitiet = _ses.FindObject<ChiTietThoiKhoaBieu>(filter);

            if (chitiet != null)
            {
                e.Text = "Chủ đề chính: " + chitiet.ChiTietChuongTrinhGD.ChiTietChuongTrinhGD_NDCS[0].ChuDeCha.TenChuDe;
                e.Text += "\nChủ đề nhánh : " + chitiet.ChiTietChuongTrinhGD.ChiTietChuongTrinhGD_NDCS[0].ChuDe.TenChuDe;

                args.SecondLineText = "";
            }
            //args.SecondLineText = "Từ ngày: " + args.Interval.Start.Date.ToShortDateString() + " - " + "Đến ngày: " + args.Interval.End.Date.ToShortDateString();
        }

        public void SetLabel(Session ses, ThoiKhoaBieu tkb)
        {
            _tkb = tkb;
            xrLabel2.Text = "Họ tên: " + tkb.HocSinh.HoTen;
            xrLabel3.Text = "Lớp : " + tkb.HocSinh.Lop.TenLop;
            //
            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc =?", tkb.QuanLyThoiKhoaBieu.NamHoc.Oid);
            XPCollection<TuanHoc> tuanList = new XPCollection<TuanHoc>(ses, filter);
            tuanList.Sorting.Add(new SortProperty("TuNgay", SortingDirection.Ascending));
            DateTime TuNgay = tuanList[0].TuNgay;
            tuanList.Sorting.Clear();
            tuanList.Sorting.Add(new SortProperty("DenNgay", SortingDirection.Descending));
            DateTime DenNgay = tuanList[0].DenNgay;

            SchedulerAdapter.TimeInterval = new TimeInterval(TuNgay, DenNgay);
        }
    }
}
