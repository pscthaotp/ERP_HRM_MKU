using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.TienLuong.ChamCong;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChamCong
{
    public partial class ChamCong_LayDuLieuCongGiangDayController : ViewController
    {
        public ChamCong_LayDuLieuCongGiangDayController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu dữ liệu trước khi chốt
            View.ObjectSpace.CommitChanges();
            //
            CC_QuanLyCongGiangDay obj = View.CurrentObject as CC_QuanLyCongGiangDay;
            if (obj != null && obj.KyTinhLuong != null)
            {
                if (obj.KhoaSo)
                {
                    DialogUtil.ShowWarning("Dữ liệu đã đồng bộ SAP.");
                    return;
                }

                try
                {
                    ////
                    using (DialogUtil.AutoWait())
                    {
                        //Chốt chấm công
                        LayDuDieuChamCongGiangDay(obj);

                        DialogUtil.ShowInfo("Lấy dữ liệu chấm công từ đào tạo (CoreUIS) thành công.");
                    }
                }
                catch
                {
                    DialogUtil.ShowError("Lấy dữ liệu chấm công từ đào tạo (CoreUIS) thất bại.");
                }
            }
            else
            {
                DialogUtil.ShowError("Chưa nhập kỳ tính lương.");
            }
        }

        private void ChamCong_LayDuLieuCongGiangDayController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<CC_QuanLyCongGiangDay>();
        }

        private void LayDuDieuChamCongGiangDay(CC_QuanLyCongGiangDay obj)
        {
            //
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@QuanLyCongGiangDay", obj.Oid);
            param[1] = new SqlParameter("@TuNgay", obj.KyTinhLuong.TuNgay.Date);
            param[2] = new SqlParameter("@DenNgay", obj.KyTinhLuong.DenNgay.Date);
            param[3] = new SqlParameter("@CongTy", obj.CongTy.Oid);
            //
            DataProvider.ExecuteNonQuery("spd_ChamCongGiangDay_LayDuLieuCoreUIS", CommandType.StoredProcedure, param);
            //
            View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
