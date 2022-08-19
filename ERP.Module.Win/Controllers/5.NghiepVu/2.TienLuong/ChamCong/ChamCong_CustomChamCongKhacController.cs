using System;
using DevExpress.ExpressApp;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.Win.Editors;
using System.Globalization;
using System.Drawing;
using DevExpress.Data.Filtering;
using ERP.Module.Extends;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChamCong
{
    public partial class ChamCong_CustomChamCongKhacController : ViewController
    {
        private ListView _listView;
        private static Guid _oidKyChamCong;

        public ChamCong_CustomChamCongKhacController()
        {
            InitializeComponent();
            //
            RegisterActions(components);
        }

        private void CustomTheoDoiTreViewController_Activated(object sender, EventArgs e)
        {
            //Cài đặt listView ở đây
            View.ControlsCreated += View_ControlsCreated;

            //Lấy quản lý chấm công để truy vấn tháng, năm
            if (View.SelectedObjects.Count > 0)
            {
                CC_ChamCongKhac chamCong = View.SelectedObjects[0] as CC_ChamCongKhac;
                if (chamCong != null && chamCong.KyChamCong != null)
                    _oidKyChamCong = chamCong.KyChamCong.Oid;
            }
        }


        private void View_ControlsCreated(object sender, EventArgs e)
        {
            //Lấy listView
            _listView = View as ListView;

            if (_listView != null && _listView.Editor is GridListEditor &&
                _listView.Id.Equals("CC_ChamCongKhac_ListChiTietChamCongKhac_ListView"))//Nếu là lưới
            {
                //Ép sang kiểu lưới 
                GridView gridView = (_listView.Editor as GridListEditor).GridView;
                if (gridView != null)
                {
                    //Cài đặt lưới
                    CustomGridView(gridView);
                    //
                    DisableColumnAutoWidthOfObject(gridView);
                }
            }
        }

        private void DisableColumnAutoWidthOfObject(GridView gridView)
        {
            //
            gridView.OptionsView.ColumnAutoWidth = false;
            //
            gridView.BestFitColumns();
            gridView.BestFitMaxRowCount = -1;
        }

        private void CustomGridView(GridView gridView)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            if (gridView != null)
            {
                ////Cài đặt thông tin cơ bản của lưới
                GridUtil.InitGridView(gridView);
                //
                if (_oidKyChamCong != Guid.Empty)
                {                   
                    //
                    CultureInfo _cultureInfo = CultureInfo.CreateSpecificCulture("vi-VN");
                    _cultureInfo.DateTimeFormat.DayNames = new string[] { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
                    //Lấy kỳ chấm công
                    DateTime tuNgay = DateTime.MinValue;
                    DateTime denNgay = DateTime.MinValue;
                    CC_KyChamCong kyChamCong = obs.GetObjectByKey<CC_KyChamCong>(_oidKyChamCong);
                    if (kyChamCong != null)
                    {
                        tuNgay = kyChamCong.TuNgay;
                        denNgay = kyChamCong.DenNgay;
                    }
                    else
                    {
                        DialogUtil.ShowError("Chưa tạo tạo kỳ chấm công.");
                        return;
                    }
                    //
                    int index = 1;
                    DateTime currentDay = new DateTime(tuNgay.Year, tuNgay.Month, 26);
                    while (currentDay != DateTime.MinValue && currentDay <= denNgay)
                    {
                        string nameColumn = "Ngay" + index.ToString();
                        //
                        GridColumn column = gridView.Columns[nameColumn];
                        if (column != null)
                        {
                            //
                            DateTime dt = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day);
                            //
                            column.Caption = string.Format("Ngày {0} ({1})", currentDay.Day, dt.ToString("dddd", _cultureInfo));
                            //
                            if (dt.ToString("dddd", _cultureInfo) == "T7")
                            {
                                column.AppearanceCell.BackColor = Color.LightBlue;
                            }
                            else if (dt.ToString("dddd", _cultureInfo) == "CN")
                            {
                                column.AppearanceCell.BackColor = Color.LightSalmon;
                            }
                            //

                            //Tăng ngày hiện tại lên
                            index += 1;
                            currentDay = currentDay.AddDays(1);
                        }
                    }
                }
            }
        }
    }
}
