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
//
namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.Luong
{
    public partial class Luong_TinhLuongNhanVienController : ViewController
    {
        public Luong_TinhLuongNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            BangLuongNhanVien bangLuong = View.CurrentObject as BangLuongNhanVien;
            if (bangLuong != null)
            {
                if (bangLuong.ListLuongNhanVien == null || (bangLuong.ListLuongNhanVien != null && bangLuong.ListLuongNhanVien.Count == 0))
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
                    DialogUtil.ShowWarning("Bảng lương đã được lập chứng từ chi tiền.");
                    return;
                }
                if (bangLuong.KyTinhLuong.QuanLyChamCong == null)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '[{0}]' chưa có bảng chấm công.", bangLuong.KyTinhLuong.TenKy));
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
                    //Lấy công thức tính lương + tính phép hè
                    CriteriaOperator filter = CriteriaOperator.Parse("DotTinhLuong = ? and CongTy = ? and (LoaiCongThucLuong = 1 or LoaiCongThucLuong = 3)", bangLuong.DotTinhLuong.Oid, bangLuong.CongTy.Oid);
                    XPCollection<CongThucTinhLuong> congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(((XPObjectSpace)View.ObjectSpace).Session, filter);
                    //
                    message = Luong_TinhLuongVaPhuCap.XuLy(View.ObjectSpace, bangLuong, congThucTinhLuongList);
                }
                #endregion

                //
                if (string.IsNullOrEmpty(message))
                {
                    DialogUtil.ShowInfo("Tính lương thành công!!!");
                    //
                    View.ObjectSpace.Refresh();
                }
                else
                {
                    DialogUtil.ShowError(message);
                }
            }
        }

        private void Luong_TinhLuongNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<BangLuongNhanVien>();
        }
    }
}
