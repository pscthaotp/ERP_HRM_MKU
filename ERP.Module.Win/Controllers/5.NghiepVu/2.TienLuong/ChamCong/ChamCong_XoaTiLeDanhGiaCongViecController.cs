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
//

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChamCong
{
    public partial class ChamCong_XoaTiLeDanhGiaCongViecController : ViewController
    {
        public ChamCong_XoaTiLeDanhGiaCongViecController()
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
            if (obj.Count == 1 && obj[0] is TiLeDanhGiaCongViec)
            {
                ExecuteCustomDeleteObjects("spd_ChamCong_XoaTiLeDanhGiaCongViec");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            TiLeDanhGiaCongViec tiLe = View.CurrentObject as TiLeDanhGiaCongViec;
            if (tiLe != null)
            {
                if (tiLe.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning("Kỳ tính lương đã khóa. Không thể xóa !!!");
                    return;
                }

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("TiLeDanhGia", tiLe.Oid);
                //
                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa bảng Tỉ lệ đánh giá thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa Bảng tỉ lệ đánh giá!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
