using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.Win.Controllers.NghiepVu.TienLuong.ExecuteClass;
using ERP.Module.NghiepVu.TienLuong.NgoaiGio;
using System.Windows.Forms;
using ERP.Module.NghiepVu.TienLuong.Thuong;
using ERP.Module.DanhMuc.TienLuong;
//
namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.Thuong
{
    public partial class Thuong_TinhKhenThuongPhucLoiController : ViewController
    {
        public Thuong_TinhKhenThuongPhucLoiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            BangThuongNhanVien bangLuong = View.CurrentObject as BangThuongNhanVien;
            if (bangLuong != null)
            {
                if (bangLuong.ListChiTietThuongNhanVien == null || (bangLuong.ListChiTietThuongNhanVien != null && bangLuong.ListChiTietThuongNhanVien.Count == 0))
                {
                    //Lưu dữ liệu trước khi chốt
                    View.ObjectSpace.CommitChanges();
                }

                #region Bắt lỗi trước khi tính
                if (bangLuong.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '[{0}]' đã khóa sổ.", bangLuong.KyTinhLuong.TenKy));
                    return;
                }
                if (bangLuong.ChungTu != null)
                {
                    DialogUtil.ShowWarning("Bảng thưởng đã được lập chứng từ chi tiền.");
                    return;
                }
                #endregion

                #region Gọi hàm xử lý tính lương
                //
                string message = string.Empty;
                //
                using (DialogUtil.AutoWait())
                {
                    //Lấy tất cả công thức lương thỏa điều kiện
                    CriteriaOperator filter = CriteriaOperator.Parse("CongTy = ?", bangLuong.CongTy.Oid);
                    XPCollection<LoaiKhenThuongPhucLoi> loaiKhenThuongPhucLoiList = new XPCollection<LoaiKhenThuongPhucLoi>(((XPObjectSpace)View.ObjectSpace).Session, filter);
                    //
                    message = Luong_TinhKhenThuong.XuLy(View.ObjectSpace, bangLuong, loaiKhenThuongPhucLoiList);
                }
                #endregion

                //
                if (string.IsNullOrEmpty(message))
                {
                    DialogUtil.ShowInfo("Tính thưởng thành công!!!");
                    //
                    View.ObjectSpace.Refresh();
                }
                else
                {
                    DialogUtil.ShowError(message);
                }
            }
        }

        private void Thuong_TinhKhenThuongPhucLoiController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<BangThuongNhanVien>();
        }
    }
}
