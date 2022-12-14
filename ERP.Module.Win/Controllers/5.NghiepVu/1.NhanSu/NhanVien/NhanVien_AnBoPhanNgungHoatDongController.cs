using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.Win.Editors.NhanSu.NhanVien;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_AnBoPhanNgungHoatDongController : ViewController<ListView>
    {
        public NhanVien_AnBoPhanNgungHoatDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            //
        }

        private void NhanVien_AnBoPhanNgungHoatDongController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;

            if (listView!= null && listView.Editor is CategorizedListEditor_NhanVien)
            {
                //// Chức năng này dùng để ẩn những bộ phận ngừng hoạt động đi
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Type", false);
                DataProvider.ExecuteNonQuery("spd_NhanVien_AnBoPhanNgungHoatDong", CommandType.StoredProcedure, param);
            }
            else
            {
                //// Chức năng này dùng để hiện những bộ phận ngừng hoạt động lên
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Type", true);
                DataProvider.ExecuteNonQuery("spd_NhanVien_AnBoPhanNgungHoatDong", CommandType.StoredProcedure, param);
            }
        }

       
    }
}
