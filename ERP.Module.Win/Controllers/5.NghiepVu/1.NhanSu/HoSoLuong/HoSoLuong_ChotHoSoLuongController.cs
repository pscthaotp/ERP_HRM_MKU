using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Extends;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    public partial class HoSoLuong_ChotHoSoLuongController : ViewController
    {
        public HoSoLuong_ChotHoSoLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu dữ liệu trước khi chốt
            View.ObjectSpace.CommitChanges();
            //
            HoSoTinhLuong obj = View.CurrentObject as HoSoTinhLuong;
            if (obj != null)
            {
                if (obj.KhoaSo || obj.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning("Hồ sơ tính lương hoặc Kỳ tính lương đã khóa sổ.");
                    return;
                }
                //else if (obj.KyTinhLuong.QuanLyChamCong == null)
                //{
                //    DialogUtil.ShowWarning("Chưa chọn Bảng công nhân viên cho kỳ tính lương đang chốt hồ sơ.");
                //    return;
                //}

                try
                {
                    ////
                    using (DialogUtil.AutoWait())
                    {
                        //Chốt hồ sơ lương mới
                        ChotThongTinTinhLuong(obj);

                        DialogUtil.ShowInfo("Chốt hồ sơ tính lương thành công.");
                    }
                }
                catch
                {
                    DialogUtil.ShowError("Chốt hồ sơ tính lương thất bại.");
                }
            }
        }

        private void HoSoLuong_ChotHoSoLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<HoSoTinhLuong>();
        }

        private void ChotThongTinTinhLuong(HoSoTinhLuong obj)
        {
            //
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@HoSoTinhLuong", obj.Oid);
            param[1] = new SqlParameter("@CongTy", obj.CongTy.Oid);
            param[2] = new SqlParameter("@CreateUser", Common.SecuritySystemUser_GetCurrentUser().Oid);
            //
            DataProvider.ExecuteNonQuery("spd_HoSoLuong_ChotHoSoTinhLuong", CommandType.StoredProcedure, param);
            //
            View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
