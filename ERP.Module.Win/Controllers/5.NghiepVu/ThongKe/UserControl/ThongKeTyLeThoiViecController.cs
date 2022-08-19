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
using DevExpress.XtraCharts;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe
{
    public partial class ThongKeTyLeThoiViecController : ThongKeBaseController
    {
        //        
        private string quyen = Common.System_GetDeparment_Role_ByUser();
        private int namThuNhat;
        private int namThuHai;
        private int namThuBa;
        //
        public ThongKeTyLeThoiViecController(XafApplication app)
        {           
            InitializeComponent();                       
        }

        private void ThongKeBienCheController_Load(object sender, EventArgs e)
        {            
            SqlParameter[] param = new SqlParameter[4];            
            param[0] = new SqlParameter("@BoPhanPhanQuyen", quyen);
            param[1] = new SqlParameter("@CongTy", Guid.Empty);
            param[2] = new SqlParameter("@TinhDenNgay", DateTime.Now);
            param[3] = new SqlParameter("@LaQuanTri", Common.QuanTriKhoi() ? true : Common.QuanTriToanHeThong() ? true : false);
            //
            DataTable dataList = DataProvider.GetDataTable("spd_ThongKe_ThongKeTyLeThoiViec", CommandType.StoredProcedure, param);
            //
            chart_Column.DataSource = dataList;
            chart_Circle.DataSource = dataList;
            //   
            chartTitle1.Text = String.Concat("Tỷ lệ nhân sự thôi việc qua ba năm");
            chartTitle2.Text = String.Concat("Tỷ trọng nhân sự thôi việc trong năm ", DateTime.Now.Year.ToString());

            namThuNhat = (DateTime.Now.Year - 2);
            namThuHai = (DateTime.Now.Year - 1);
            namThuBa = (DateTime.Now.Year);
            chart_Column.BoundDataChanged += OnBoundDataChanged;
        }

        public override void LoadData(Guid congTy, DateTime tinhDenNgay, bool laQuanTri)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@BoPhanPhanQuyen", quyen);
            param[1] = new SqlParameter("@CongTy", congTy);
            param[2] = new SqlParameter("@TinhDenNgay", tinhDenNgay);
            param[3] = new SqlParameter("@LaQuanTri", laQuanTri);

            DataTable dataList = DataProvider.GetDataTable("spd_ThongKe_ThongKeTyLeThoiViec", CommandType.StoredProcedure, param);
            //
            chart_Column.DataSource = dataList;
            chart_Circle.DataSource = dataList;           
            //  
            chartTitle1.Text = String.Concat("Tỷ lệ nhân sự thôi việc qua ba năm");     
            chartTitle2.Text = String.Concat("Tỷ trọng nhân sự thôi việc trong năm ", tinhDenNgay.Year.ToString());

            namThuNhat = tinhDenNgay.Year - 2;
            namThuHai = tinhDenNgay.Year - 1;
            namThuBa = tinhDenNgay.Year;
            chart_Column.BoundDataChanged += OnBoundDataChanged;
        }

        private void InChart_Circle_Click(object sender, EventArgs e)
        {
            PrintChart(chart_Circle);           
        }

        private void InChart_Column_Click(object sender, EventArgs e)
        {
            PrintChart(chart_Column);
        }

        private void OnBoundDataChanged(object sender, EventArgs args)
        {
            if (chart_Column != null && chart_Column.Series != null)
            {
                chart_Column.Series[0].Name = String.Concat("NS thôi việc ", namThuNhat.ToString());
                chart_Column.Series[1].Name = String.Concat("NS thôi việc ", namThuHai.ToString());
                chart_Column.Series[2].Name = String.Concat("NS thôi việc ", namThuBa.ToString());
            }
        }
    }
}
