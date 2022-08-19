using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using DevExpress.XtraBars;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.ExpressApp.Templates;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Columns;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class CustomNumberColumnController : ViewController
    {
        bool _allowSave = true;
        string _propertyName = "";

        public CustomNumberColumnController()
        {
            InitializeComponent();
        }

        private void CustomNumberColumnController_Activated(object sender, EventArgs e)
        {
            //Cài đặt lưới ở đây
            if (View.Id.Contains("DeNghiCungCapTaiSan") || View.Id.Contains("DonDatHang")
                || View.Id.Contains("DonHangMua") || View.Id.Contains("DonHangBan")
                || View.Id.Contains("ChotKho") || View.Id.Contains("KiemKeKho")
                || View.Id.Contains("PhieuNhap") || View.Id.Contains("PhieuXuat")
                || View.Id.Contains("DeNghiXuatKho"))
                View.ControlsCreated += View_ControlsCreated;
        }

        private void CustomNumberColumnController_Deactivated(object sender, EventArgs e)
        {
            //Cài đặt lưới ở đây
            if (View.Id.Contains("DeNghiCungCapTaiSan") || View.Id.Contains("DonDatHang")
               || View.Id.Contains("DonHangMua") || View.Id.Contains("DonHangBan")
               || View.Id.Contains("ChotKho") || View.Id.Contains("KiemKeKho")
               || View.Id.Contains("PhieuNhap") || View.Id.Contains("PhieuXuat")
               || View.Id.Contains("DeNghiXuatKho"))
                View.ControlsCreated -= View_ControlsCreated;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            //Tùy theo View là Detail hay ListView (vì mỗi view sẽ xử lý khác nhau)
            if (View is DetailView)
            {
                //Detail sẽ bắt Commitchange của chính đối tượng đang mở, bắt vào từng Property
                foreach (DecimalPropertyEditor propertyEditor in ((DetailView)View).GetItems<DecimalPropertyEditor>())
                {
                    //Viết lại hàm khi giá trị của Property bị thay đổi
                    propertyEditor.ControlValueChanged += propertyEditor_ControlValueChanged;

                    //Viết lại hàm Commitchange
                    View.ObjectSpace.Committing += ObjectSpace_Committing;
                }
            }

            if (View is ListView)
            {
                //List sẽ bắt Commitchange của form cha, nên tất cả xử lý sẽ làm khi bấm save form cha
                View.ObjectSpace.Committing += ObjectSpace_Committing_ListView;
            }
        }

        void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _allowSave = true;
            string EditorTypeName = e.Column.ColumnEdit.GetType().ToString();
            _propertyName = _propertyName.Replace(e.Column.Caption + "; ", "");
            if (EditorTypeName == "DevExpress.ExpressApp.Win.Editors.RepositoryItemDecimalEdit" && (Convert.ToDecimal(e.Value)) < 0)
            {
                _propertyName += e.Column.Caption + "; ";
                _allowSave = false;
            }
        }

        void propertyEditor_ControlValueChanged(object sender, EventArgs e)
        {
            _allowSave = true; //Gắn giá trị cho biến kiểm tra
            var propertyEditor = ((DecimalPropertyEditor)sender);
            _propertyName = _propertyName.Replace(propertyEditor.Caption + "; ", ""); //Xóa tên cột trong thông báo khi xuất lỗi

            //Kiểm tra giá trị cột nếu < 0 thì gắn giá trị biến kiểm tra = false và thêm tên cột vào chuỗi thống báo
            if (Convert.ToDecimal(propertyEditor.ControlValue.ToString()) < 0) 
            {
                _propertyName += propertyEditor.Caption + "; ";
                _allowSave = false;
            }
        }

        void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_allowSave)
            {
                e.Cancel = true; //Hủy thao tác save
                DialogUtil.ShowError(_propertyName.Substring(0, _propertyName.Length - 2) + " không được nhỏ hơn 0"); //Xuất thông báo
            }
        }

        void ObjectSpace_Committing_ListView(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(View is ListView)
            {
                GridListEditor grid = ((ListView)View).Editor as GridListEditor; //Lấy grid hiện tại
                _allowSave = true;
                _propertyName = "";

                foreach(GridColumn column in grid.GridView.Columns) //Duyệt qua từng cột của grid kiểm tra kiểu dữ liệu
                {
                    if (column.ColumnEdit.GetType().ToString() == "DevExpress.ExpressApp.Win.Editors.RepositoryItemDecimalEdit"
                        && column.Visible == true) //Kiểu decimal và hiện trên form
                    {
                        grid.GridView.ClearSorting(); //Clear hết những cột đã sort
                        column.SortIndex = 0; //Sort cột này để lấy giá trị nhỏ nhất. Tiết kiệm chi phí duyệt từng dòng giá trị
                        decimal giatri = Convert.ToDecimal(grid.GridView.GetRowCellValue(0, column)); //Lấy giá trị cột nhỏ nhất
                        if (giatri < 0)
                        {
                            _propertyName = _propertyName.Replace(column.Caption + "; ", ""); //Xóa tên cột trong chuỗi thông báo nếu có
                            _propertyName += column.Caption + "; "; //Thêm tên cột vào chuỗi thống báo
                            _allowSave = false;
                        }
                    }
                }
                if (View.Id != "LapPhieuXuatKhoDongPhucHocPham_ListChiTietThuPhi_ListView")
                {
                    if (!_allowSave)
                    {
                        e.Cancel = true; //Hủy thao tác save
                        DialogUtil.ShowError(_propertyName.Substring(0, _propertyName.Length - 2) + " không được nhỏ hơn 0"); //Xuất thông báo
                    }
                }
            }
        }
    }
}
