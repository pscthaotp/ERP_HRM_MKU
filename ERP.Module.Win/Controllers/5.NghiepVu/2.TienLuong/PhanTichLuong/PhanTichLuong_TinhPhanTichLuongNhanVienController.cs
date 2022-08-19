using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
//
namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.Luong
{
    public partial class PhanTichLuong_TinhPhanTichLuongNhanVienController : ViewController
    {
        public PhanTichLuong_TinhPhanTichLuongNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            BangPhanTichLuongNhanVien bangPhanTichLuong = View.CurrentObject as BangPhanTichLuongNhanVien;
            if (bangPhanTichLuong != null)
            {
                if (bangPhanTichLuong.ListPhanTichLuongNhanVien == null || (bangPhanTichLuong.ListPhanTichLuongNhanVien != null && bangPhanTichLuong.ListPhanTichLuongNhanVien.Count == 0))
                {
                    //Lưu dữ liệu trước khi chốt
                    View.ObjectSpace.CommitChanges();
                }

                #region Bắt lỗi trước khi tính
                if (!bangPhanTichLuong.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '[{0}]' chưa khóa sổ.", bangPhanTichLuong.KyTinhLuong.TenKy));
                    return;
                }
                if (!string.IsNullOrEmpty(bangPhanTichLuong.TrangThai))
                {
                    DialogUtil.ShowWarning(String.Format("Dữ liệu phân tích lương '[{0}]' đã đồng bộ SAP.", bangPhanTichLuong.KyTinhLuong.TenKy));
                    return;
                }
                #endregion

                #region Gọi hàm xử lý tính lương
                //
                string message = string.Empty;
                //
                CC_QuanLyCongGiangDay chamCongGiangDay = View.ObjectSpace.FindObject<CC_QuanLyCongGiangDay>(CriteriaOperator.Parse("KyTinhLuong = ?", bangPhanTichLuong.KyTinhLuong.Oid));
                if (chamCongGiangDay == null || chamCongGiangDay.ListChiTietCongGiangDay == null || (chamCongGiangDay.ListChiTietCongGiangDay != null && chamCongGiangDay.ListChiTietCongGiangDay.Count == 0))
                    DialogUtil.ShowWarning(String.Format("Chưa cập nhật dữ liệu chấm công giảng dạy (nếu có). Bạn có thế bỏ qua bước này !!!"));
                //
                using (DialogUtil.AutoWait())
                {
                    SqlParameter[] param = new SqlParameter[1];                    
                    param[0] = new SqlParameter("@KyTinhLuong", bangPhanTichLuong.KyTinhLuong.Oid);
                    //
                    int sucessPayroll = DataProvider.ExecuteNonQuery("spd_PhanTichLuong_PhanTichChiPhiTienLuong", CommandType.StoredProcedure, param);
                    //
                    if (sucessPayroll == -1)
                    {                     
                        message = "Bảng phân tích lương [" + bangPhanTichLuong.KyTinhLuong.TenKy + "] bị lỗi không tính được. Vui lòng báo quản trị hệ thống HRM.";
                    }                   
                }
                #endregion

                //
                if (string.IsNullOrEmpty(message))
                {
                    DialogUtil.ShowInfo("Phân tích lương thành công!!!");
                    //
                    View.ObjectSpace.Refresh();
                }
                else
                {
                    DialogUtil.ShowError(message);
                }
            }
        }

        private void PhanTichLuong_TinhPhanTichLuongNhanVienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<BangPhanTichLuongNhanVien>();
        }
    }
}
