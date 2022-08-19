using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using DevExpress.Xpo;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe
{
    public partial class ThongKeNgachLuongController : ThongKeBaseController
    {
        //
        public ThongKeNgachLuongController(XafApplication app)
        {
            InitializeComponent();
        }

        private void ThongKeNgachLuongController_Load(object sender, EventArgs e)
        {
            SqlParameter[] param = new SqlParameter[1];
            string quyen = Common.System_GetDeparment_Role_ByUser();
            param[0] = new SqlParameter("@BoPhanPhanQuyen", quyen);
            //
            DataTable dataList = DataProvider.GetDataTable("spd_ThongKe_ThongKeNgachLuong", CommandType.StoredProcedure, param);
            //
            chart_Column.DataSource = dataList;
            chart_Circle.DataSource = dataList;
        }

        private void InChart_Circle_Click(object sender, EventArgs e)
        {
            PrintChart(chart_Circle);
            //
        }

        private void InChart_Column_Click(object sender, EventArgs e)
        {
            PrintChart(chart_Column);
        }
    }
}
