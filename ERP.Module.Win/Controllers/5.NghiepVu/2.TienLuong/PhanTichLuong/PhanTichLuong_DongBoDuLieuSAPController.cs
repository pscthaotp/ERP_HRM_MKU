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
    public partial class PhanTichLuong_DongBoDuLieuSAPController : ViewController
    {
        public PhanTichLuong_DongBoDuLieuSAPController()
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
                if (bangPhanTichLuong.ListPhanTichLuongCongTy == null || (bangPhanTichLuong.ListPhanTichLuongCongTy != null && bangPhanTichLuong.ListPhanTichLuongCongTy.Count == 0))
                {
                    //Lưu dữ liệu trước khi chốt
                    View.ObjectSpace.CommitChanges();
                }

                #region Bắt lỗi trước khi tính
                    if (DialogUtil.ShowYesNo(String.Format("Bạn thật sự muốn đồng bộ dữ liệu SAP ???")) == System.Windows.Forms.DialogResult.No)
                    {
                        return; 
                    }
                    if (bangPhanTichLuong.ListPhanTichLuongCongTy.Count == 0)
                    {
                        DialogUtil.ShowWarning(string.Format("Vui lòng thực hiện chức năng Phân tích lương trước khi đồng bộ !!!"));
                        return;
                    }
                    if (!string.IsNullOrEmpty(bangPhanTichLuong.TrangThai))
                    {
                        DialogUtil.ShowError(string.Format("Dữ liệu đã được đồng bộ SAP !!!"));
                        return;
                    }               
                #endregion

                #region Gọi hàm xử lý đồng bộ
                //
                string message = string.Empty;
                //              
                using (DialogUtil.AutoWait())
                {
                    SqlParameter[] param = new SqlParameter[1];                    
                    param[0] = new SqlParameter("@BangPhanTichLuongNhanVien", bangPhanTichLuong.Oid);
                    //
                    int sucessPayroll = DataProvider.ExecuteNonQuery("spd_PhanTichLuong_DongBoDuLieuSAP", CommandType.StoredProcedure, param);
                    //
                    if (sucessPayroll == -1)
                    {                     
                        message = "Bảng phân tích lương [" + bangPhanTichLuong.KyTinhLuong.TenKy + "] bị lỗi không đồng bộ được. Vui lòng báo quản trị hệ thống HRM.";
                    }                   
                }
                #endregion

                //
                if (string.IsNullOrEmpty(message))
                {
                    DialogUtil.ShowInfo("Đồng bộ thành công!!!");
                    //                    
                    View.ObjectSpace.Refresh();
                }
                else
                {
                    DialogUtil.ShowError(message);
                }
            }
        }

        private void PhanTichLuong_DongBoDuLieuSAPController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<BangPhanTichLuongNhanVien>();
        }
    }
}
