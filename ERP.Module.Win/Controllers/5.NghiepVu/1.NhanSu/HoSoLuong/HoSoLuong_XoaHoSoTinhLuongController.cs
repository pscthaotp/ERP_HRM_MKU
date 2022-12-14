using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Extends;
using ERP.Module.Commons;
//

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    public partial class HoSoLuong_XoaHoSoTinhLuongController : ViewController
    {
        public HoSoLuong_XoaHoSoTinhLuongController()
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
            if (obj.Count == 1 && obj[0] is HoSoTinhLuong)
            {
                ExecuteCustomDeleteObjects("spd_HoSoLuong_XoaHoSoTinhLuong");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            HoSoTinhLuong hoSoLuong = View.CurrentObject as HoSoTinhLuong;
            if (hoSoLuong != null)
            {
                if (hoSoLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning("Hồ sơ lương đã khóa. Không thể xóa !!!");
                    return;
                }
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("HoSoTinhLuong",hoSoLuong.Oid);
                param[1] = new SqlParameter("Type", true);
                //
                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa hồ sơ tính lương thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa hồ sơ tính lương!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
