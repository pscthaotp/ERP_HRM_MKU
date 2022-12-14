using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.ThuNhapKhac;
using ERP.Module.Extends;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ThuNhapKhac
{
    public partial class ThuNhapKhac_XoaBangThuNhapKhacController : ViewController
    {
        public ThuNhapKhac_XoaBangThuNhapKhacController()
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
            if (obj.Count == 1 && obj[0] is BangThuNhapKhac)
            {
                ExecuteCustomDeleteObjects("spd_ThuNhapKhac_XoaBangThuNhapKhac");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            BangThuNhapKhac bangThuNhapKhac = View.CurrentObject as BangThuNhapKhac;
            if (bangThuNhapKhac != null)
            {
                if (bangThuNhapKhac.ChungTu != null)
                {
                    DialogUtil.ShowError("Không thể xóa Bảng thu nhập khác. Bảng thu nhập khác đã được chi tiền!!!");
                    return;
                }
                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, new SqlParameter("@BangThuNhapKhac", bangThuNhapKhac.Oid));
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa Bảng thu nhập khác thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa Bảng thu nhập khác!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
