using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.Extends;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.Luong
{
    public partial class Luong_XoaBangLuongNhanVienController : ViewController
    {
        public Luong_XoaBangLuongNhanVienController()
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
            if (obj.Count == 1 && obj[0] is BangLuongNhanVien)
            {
                ExecuteCustomDeleteObjects("spd_Luong_XoaBangLuongNhanVien");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            BangLuongNhanVien bangLuong = View.CurrentObject as BangLuongNhanVien;
            if (bangLuong != null)
            {
                if (bangLuong.ChungTu != null)
                {
                    DialogUtil.ShowError("Không thể xóa bảng lương. Bảng lương đã được chi tiền!!!");
                    return;
                }

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@BangLuongNhanVien", bangLuong.Oid);
                param[1] = new SqlParameter("@NguoiXoa", Common.SecuritySystemUser_GetCurrentUser().Oid);

                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa bảng lương thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa bảng lương!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
