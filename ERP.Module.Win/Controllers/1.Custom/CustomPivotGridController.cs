using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.PivotGrid.Win;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class CustomPivotGridController : ViewController
    {

        public CustomPivotGridController()
        {
            InitializeComponent();
            TargetViewId = "ThongKeDuLieuKhachHang_listThongKe_ListView;"
                            + "ThongKeDanhSachHocSinh_listThongKe_ListView;"
                            + "NgoaiKhoa_ThongKeNgoaiKhoa_listChiTiet_ListView;"
                            + "NgoaiKhoa_TienDoThucHien_listChiTiet_ListView;"
                            + "HocSinh_ThongKeSiSoTheoLop_DetailView;"
                            + "HocSinh_ThongKeSiSoTheoLop_listChiTiet_ListView;"
                            + "HocSinh_ThongKeDinhDuong_DetailView;"
                            + "HocSinh_ThongKeDinhDuong_listChiTiet_ListView;"
                            + "HocSinhChinhSach_TongHop_listChiTiet_ListView";
        }

        // Hide Totals và các dữ liệu drop, data, headers.
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            DevExpress.ExpressApp.ListView listView = View as DevExpress.ExpressApp.ListView;
            DetailView detailView = View as DetailView;
            if (listView != null)
            {
                PivotGridListEditor PivotEditor = ((DevExpress.ExpressApp.ListView)View).Editor as PivotGridListEditor;
                PivotEditor.PivotGridControl.OptionsView.ShowColumnGrandTotalHeader = false;
                PivotEditor.PivotGridControl.OptionsView.ShowColumnGrandTotals = false;
                PivotEditor.PivotGridControl.OptionsView.ShowColumnTotals = false;
                PivotEditor.PivotGridControl.OptionsView.ShowRowGrandTotalHeader = false;
                PivotEditor.PivotGridControl.OptionsView.ShowRowGrandTotals = false;
                PivotEditor.PivotGridControl.OptionsView.ShowRowTotals = false;
                PivotEditor.PivotGridControl.OptionsView.ShowFilterHeaders = false;
                PivotEditor.PivotGridControl.OptionsView.ShowColumnHeaders = false;
                PivotEditor.PivotGridControl.OptionsView.ShowDataHeaders = false;
            }
        }

       
    }
}