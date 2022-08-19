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
    public partial class ThongKeTongHopBienDongNhanSuController : ThongKeBaseController
    {
        //        
        private string quyen = Common.System_GetDeparment_Role_ByUser();        
        //
        public ThongKeTongHopBienDongNhanSuController(XafApplication app)
        {
            InitializeComponent();           
        }

        private void ThongKeTongHopBienDongNhanSuController_Load(object sender, EventArgs e)
        {            
            SqlParameter[] param = new SqlParameter[4];            
            param[0] = new SqlParameter("@BoPhanPhanQuyen", quyen);
            param[1] = new SqlParameter("@CongTy", Guid.Empty);
            param[2] = new SqlParameter("@TinhDenNgay", DateTime.Now);
            param[3] = new SqlParameter("@LaQuanTri", Common.QuanTriKhoi() ? true : Common.QuanTriToanHeThong() ? true : false);
            //
            DataTable dataList = DataProvider.GetDataTable("spd_ThongKe_ThongKeBienDongNhanSu", CommandType.StoredProcedure, param);
            //
            chart_Column.DataSource = dataList;
            chart_Circle.DataSource = dataList;
            //           
            chartTitle1.Text = String.Concat("Biến động nhân sự năm ", (DateTime.Now.Year).ToString());
            chartTitle2.Text = String.Concat("Tỷ trọng nhân sự năm ", (DateTime.Now.Year).ToString());            
        }

        public override void LoadData(Guid congTy, DateTime tinhDenNgay, bool laQuanTri)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@BoPhanPhanQuyen", quyen);
            param[1] = new SqlParameter("@CongTy", congTy);
            param[2] = new SqlParameter("@TinhDenNgay", tinhDenNgay);
            param[3] = new SqlParameter("@LaQuanTri", laQuanTri);

            DataTable dataList = DataProvider.GetDataTable("spd_ThongKe_ThongKeBienDongNhanSu", CommandType.StoredProcedure, param);
            //
            chart_Column.DataSource = dataList;
            chart_Circle.DataSource = dataList;           
            //            
            chartTitle1.Text = String.Concat("Biến động nhân sự năm ", (tinhDenNgay.Year).ToString());
            chartTitle2.Text = String.Concat("Tỷ trọng nhân sự năm ", (tinhDenNgay.Year).ToString()); ;   
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
