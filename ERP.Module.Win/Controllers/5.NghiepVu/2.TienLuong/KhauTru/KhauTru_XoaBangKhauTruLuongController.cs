using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.KhauTru;
using ERP.Module.Extends;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.KhauTru
{
    public partial class KhauTru_XoaBangKhauTruLuongController : ViewController
    {
        public KhauTru_XoaBangKhauTruLuongController()
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
            if (obj.Count == 1 && obj[0] is BangKhauTruLuong)
            {
                ExecuteCustomDeleteObjects("spd_KhauTru_XoaBangKhauTruLuong");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            BangKhauTruLuong bangKhauTru = View.CurrentObject as BangKhauTruLuong;
            if (bangKhauTru != null)
            {
                if (bangKhauTru.ChungTu != null)
                {
                    DialogUtil.ShowError("Không thể xóa Bảng khấu trừ. Bảng khấu trừ đã được chi tiền!!!");
                    return;
                }
                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, new SqlParameter("@BangKhauTruLuong", bangKhauTru.Oid));
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa Bảng khấu trừ thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa Bảng khấu trừ!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
