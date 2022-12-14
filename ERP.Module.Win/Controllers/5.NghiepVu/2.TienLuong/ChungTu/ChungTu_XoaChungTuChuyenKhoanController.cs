using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.Extends;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Win.Editors;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChungTus
{
    public partial class ChungTu_XoaChungTuChuyenKhoanController : ViewController
    {
        public ChungTu_XoaChungTuChuyenKhoanController()
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

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View is ListView)
            {
                GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
                listEditor.GridView.OptionsSelection.MultiSelect = false;
            }
        }

        private void ObjectSpace_CustomDeleteObjects(object sender, CustomDeleteObjectsEventArgs e)
        {
            ArrayList obj = (ArrayList)e.Objects;
            //
            if (obj.Count > 0 && obj[0] is ChungTu)
            {
                ExecuteCustomDeleteObjects("spd_ChungTu_XoaChungTuChuyenKhoan");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            ChungTu chungTu = View.CurrentObject as ChungTu;
            if (chungTu != null)
            {
                if (chungTu.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowError("Kỳ tính lương đã khóa sổ. Không thể xóa Chứng từ!!!");
                    return;
                }
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ChungTu", chungTu.Oid);
                param[1] = new SqlParameter("@NguoiXoa", Common.SecuritySystemUser_GetCurrentUser().Oid);

                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa Chứng từ thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa Chứng từ!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
