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
using ERP.Module.NghiepVu.TienLuong.ChamCong;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChamCong
{
    public partial class ChamCong_ChotCongKhacController : ViewController
    {
        public ChamCong_ChotCongKhacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu dữ liệu trước khi chốt
            View.ObjectSpace.CommitChanges();
            //
            CC_ChamCongKhac obj = View.CurrentObject as CC_ChamCongKhac;
            if (obj != null)
            {
                if (obj.Chot)
                {
                    DialogUtil.ShowWarning("Bảng chấm công khác đã chốt.");
                    return;
                }

                try
                {
                    ////
                    using (DialogUtil.AutoWait())
                    {
                        //Chốt chấm công
                        ChotChamCongKhac(obj);

                        DialogUtil.ShowInfo("Chốt chấm công khác thành công.");
                    }
                }
                catch
                {
                    DialogUtil.ShowError("Chốt chấm công khác thất bại.");
                }
            }
        }

        private void ChamCong_ChotCongKhacController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<CC_ChamCongKhac>();
        }

        private void ChotChamCongKhac(CC_ChamCongKhac obj)
        {
            //
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@KyChamCong", obj.KyChamCong.Oid);
            param[1] = new SqlParameter("@LoaiCongKhac", obj.LoaiCongKhac);
            param[2] = new SqlParameter("@CongTy", obj.CongTy.Oid);
            //
            DataProvider.ExecuteNonQuery("spd_ChamCong_ChotChamCongKhac", CommandType.StoredProcedure, param);
            //
            View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
