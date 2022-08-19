using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.Extends;
using ERP.Module.Commons;
//
namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.Luong
{
    public partial class PhanTichLuong_CapNhatCostCenterChiPhiLuongController : ViewController
    {
        public PhanTichLuong_CapNhatCostCenterChiPhiLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            BangPhanTichLuongNhanVien bangPhanTichLuong = View.CurrentObject as BangPhanTichLuongNhanVien;
            if (bangPhanTichLuong != null && bangPhanTichLuong.KyTinhLuong != null)
            {
                if (bangPhanTichLuong.ListPhanTichLuongCongTy == null || (bangPhanTichLuong.ListPhanTichLuongCongTy != null && bangPhanTichLuong.ListPhanTichLuongCongTy.Count == 0))
                {
                    //Lưu dữ liệu trước khi chốt
                    View.ObjectSpace.CommitChanges();
                }

                #region Bắt lỗi trước khi tính               
                if (!string.IsNullOrEmpty(bangPhanTichLuong.TrangThai))
                {
                    DialogUtil.ShowError(String.Format("Dữ liệu đã được đồng bộ SAP !!!"));
                    return;
                }
                #endregion

                #region Gọi hàm xử lý
                //
                string message = string.Empty;
                //              
                using (DialogUtil.AutoWait())
                {
                    SqlParameter[] param = new SqlParameter[1];                    
                    param[0] = new SqlParameter("@KyTinhLuong", bangPhanTichLuong.KyTinhLuong.Oid);
                    //
                    int sucessPayroll = DataProvider.ExecuteNonQuery("spd_PhanTichLuong_CapNhatCostCenterChiPhiLuong", CommandType.StoredProcedure, param);
                    //
                    if (sucessPayroll == -1)
                    {                     
                        message = "Cập nhật mã phân bổ chi phí tiền lương [" + bangPhanTichLuong.KyTinhLuong.TenKy + "] bị lỗi. Vui lòng báo quản trị hệ thống HRM.";
                    }                   
                }
                #endregion

                //
                if (string.IsNullOrEmpty(message))
                {
                    DialogUtil.ShowInfo("Cập nhật thành công!!!");
                    //                    
                    View.ObjectSpace.Refresh();
                }
                else
                {
                    DialogUtil.ShowError(message);
                }
            }
        }

        private void PhanTichLuong_CapNhatCostCenterChiPhiLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<BangPhanTichLuongNhanVien>();
        }
    }
}
