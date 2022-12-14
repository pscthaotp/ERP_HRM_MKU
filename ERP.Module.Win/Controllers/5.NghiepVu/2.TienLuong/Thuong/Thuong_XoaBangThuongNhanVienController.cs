using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.NghiepVu.TienLuong.Thuong;
using ERP.Module.Extends;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.Luong
{
    public partial class Thuong_XoaBangThuongNhanVienController : ViewController
    {
        public Thuong_XoaBangThuongNhanVienController()
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
            if (obj.Count == 1 && obj[0] is BangThuongNhanVien)
            {
                ExecuteCustomDeleteObjects("spd_Thuong_XoaBangThuongNhanVien");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            BangThuongNhanVien bangThuong = View.CurrentObject as BangThuongNhanVien;
            if (bangThuong != null)
            {
                if (bangThuong.ChungTu != null)
                {
                    DialogUtil.ShowError("Không thể xóa bảng thưởng. Bảng thưởng đã được chi tiền!!!");
                    return;
                }

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@BangThuongNhanVien", bangThuong.Oid);
                param[1] = new SqlParameter("@NguoiXoa", Common.SecuritySystemUser_GetCurrentUser().Oid);

                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa bảng thưởng thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa thưởng lương!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
