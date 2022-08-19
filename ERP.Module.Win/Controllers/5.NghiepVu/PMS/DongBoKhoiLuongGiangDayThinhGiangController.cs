using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.Win.Controllers.NghiepVu.TienLuong.ExecuteClass;
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

            if (qly.BangChotThuLao_ThinhGiang == null)
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
