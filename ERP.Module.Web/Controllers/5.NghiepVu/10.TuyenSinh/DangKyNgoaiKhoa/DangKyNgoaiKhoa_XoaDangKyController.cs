using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using DevExpress.Data.Filtering;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TuyenSinh;
using DevExpress.ExpressApp.Web;
//

namespace ERP.Module.Win.Controllers.NghiepVu.TuyenSinh
{
    public partial class DangKyNgoaiKhoa_XoaDangKyController : ViewController
    {
        public DangKyNgoaiKhoa_XoaDangKyController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            View.ObjectSpace.CustomDeleteObjects += ObjectSpace_CustomDeleteObjects;
        }

        protected override void OnDeactivated()
        {
            View.ObjectSpace.CustomDeleteObjects -= ObjectSpace_CustomDeleteObjects;
            base.OnDeactivated();
        }

        private void ObjectSpace_CustomDeleteObjects(object sender, CustomDeleteObjectsEventArgs e)
        {
            ArrayList obj = (ArrayList)e.Objects;
            //
            if (obj.Count == 1 && obj[0] is DangKyNgoaiKhoa)
            {
                ExecuteCustomDeleteObjects("spd_HocPhi_XoaCongNoHuyDangKyNgoaiKhoa");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            DangKyNgoaiKhoa dangKy = View.CurrentObject as DangKyNgoaiKhoa;
            if (dangKy != null)
            {
                if (dangKy.ChiTietCongNo != null && dangKy.ChiTietCongNo.BienLai != null) //Cần viết func để ktra đã thu cho trường hợp này do ChiTietCongNo luu theo quá trình thu
                {
                    //DialogUtil.ShowWarning("Đã thu phí cho đăng ký này. Không thể xóa !!!");
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Gia hạn thành công !!!')");

                    return;
                }

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@DangKyNgoaiKhoa", dangKy.Oid);
                //
                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != 0)
                {
                    DialogUtil.ShowInfo("Xóa Đăng ký thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa Đăng ký!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
