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
using DevExpress.Data.Filtering;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChamCong
{
    public partial class ChamCong_HuyChotCongKhacController : ViewController
    {
        public ChamCong_HuyChotCongKhacController()
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
                //Nếu bảng quản lý công khác khóa rồi thì không cho hủy
                CriteriaOperator filter = CriteriaOperator.Parse("KyChamCong.Oid=? and LoaiCongKhac=?",obj.KyChamCong.Oid,obj.LoaiCongKhac);
                CC_QuanLyCongKhac quanLy = View.ObjectSpace.FindObject<CC_QuanLyCongKhac>(filter);
                if (quanLy != null && quanLy.KhoaChamCong)
                {
                    DialogUtil.ShowWarning("Bảng chấm công khác đã khóa.");
                    return;
                }

                try
                {
                    ////
                    using (DialogUtil.AutoWait())
                    {
                        //Hủy Chốt công
                        HuyhotChamCongKhac(obj);

                        DialogUtil.ShowInfo("Hủy chốt chấm công khác thành công.");
                    }
                }
                catch
                {
                    DialogUtil.ShowError("Hủy chốt chấm công khác thất bại.");
                }
            }
        }

        private void ChamCong_HuyChotCongKhacController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<CC_ChamCongKhac>();
        }

        private void HuyhotChamCongKhac(CC_ChamCongKhac obj)
        {
            //
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@KyChamCong", obj.KyChamCong.Oid);
            param[1] = new SqlParameter("@LoaiCongKhac", obj.LoaiCongKhac);
            param[2] = new SqlParameter("@CongTy", obj.CongTy.Oid);
            //
            DataProvider.ExecuteNonQuery("spd_ChamCong_HuyChotChamCongKhac", CommandType.StoredProcedure, param);
            //
            View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
