using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;
using System.Data.SqlClient;
using ERP.Module.NghiepVu.PMS.QuanLyGiangDay;
using System.Data;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.PMS
{
    public partial class DongBoKhoiLuongGiangDayThinhGiangController : ViewController
    {
        public DongBoKhoiLuongGiangDayThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            QuanLyKhoiLuongGiangDay_ThinhGiang qly = View.CurrentObject as QuanLyKhoiLuongGiangDay_ThinhGiang;

            if (qly.BangChotThuLao_ThinhGiang == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@KhoiLuongGiangDay", qly.Oid);
                param[1] = new SqlParameter("@LoaiGV", 1);
                param[2] = new SqlParameter("@User", Common.SecuritySystemUser_GetCurrentUser().UserName);
                //
                DataProvider.ExecuteNonQuery("spd_PMS_DongBoKhoiLuongGiangDay", CommandType.StoredProcedure, param);

                //QuyDoi
                SqlParameter[] param1 = new SqlParameter[1];
                param1[0] = new SqlParameter("@KhoiLuongGiangDay", qly.Oid);
                //
                DataProvider.ExecuteNonQuery("spd_PMS_QuyDoiKhoiLuongGiangDay_ThinhGiang", CommandType.StoredProcedure, param1);

                View.ObjectSpace.Refresh();

                MessageBox.Show("Đồng bộ dữ liệu thành công!");
            }
            else
            {
                MessageBox.Show("Bảng chốt đã khóa vui lòng mở khóa bảng chốt trước!");
            }
        }

        private void DongBoKhoiLuongGiangDayThinhGiangController_Activated(object sender, EventArgs e)
        {
            //simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ChungTu>();
        }
        
    }
}
