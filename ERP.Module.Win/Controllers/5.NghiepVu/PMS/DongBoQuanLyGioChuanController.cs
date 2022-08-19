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
using ERP.Module.NghiepVu.PMS.QuanLyGioChuan;

namespace ERP.Module.Win.Controllers.NghiepVu.PMS
{
    public partial class DongBoQuanLyGioChuanController : ViewController
    {
        public DongBoQuanLyGioChuanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            QuanLyGioChuan qly = View.CurrentObject as QuanLyGioChuan;

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@QuanLyGioChuan", qly.Oid);
            //
            DataProvider.ExecuteNonQuery("spd_PMS_DongBoQuanLyGioChuan", CommandType.StoredProcedure, param);          

            View.ObjectSpace.Refresh();
        }

        private void DongBoQuanLyGioChuanController_Activated(object sender, EventArgs e)
        {
            //simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ChungTu>();
        }
        
    }
}
