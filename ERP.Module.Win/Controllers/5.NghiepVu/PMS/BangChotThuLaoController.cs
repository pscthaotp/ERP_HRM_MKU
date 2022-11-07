﻿using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.PMS.BangChotThuLao;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.PMS
{
    public partial class BangChotThuLaoController : ViewController
    {
        public BangChotThuLaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BangChotThuLao qly = View.CurrentObject as BangChotThuLao;
            if (!qly.Khoa)
            {
                SqlParameter[] param1 = new SqlParameter[1];
                param1[0] = new SqlParameter("@BangChotThuLao", qly.Oid);
                //
                DataProvider.ExecuteNonQuery("spd_PMS_ChotThuLao_CoHuu", CommandType.StoredProcedure, param1);

                View.ObjectSpace.Refresh();
                MessageBox.Show("Chốt dữ liệu thành công!");
            }
            else
            {
                MessageBox.Show("Bảng chốt đã khóa vui lòng mở khóa bảng chốt trước!");
            }
        }

        private void DongBoBangChotThuLaoController_Activated(object sender, EventArgs e)
        {
            //simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ChungTu>();
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BangChotThuLao qly = View.CurrentObject as BangChotThuLao;
            if (qly.Khoa)
            {
                SqlParameter[] param1 = new SqlParameter[1];
                param1[0] = new SqlParameter("@BangChotThuLao", qly.Oid);
                //
                DataProvider.ExecuteNonQuery("spd_PMS_XoaBangChotThuLao_CoHuu", CommandType.StoredProcedure, param1);

                View.ObjectSpace.Refresh();
                MessageBox.Show("Mở khóa bảng chốt dữ liệu thành công!");
            }
            else
            {
                MessageBox.Show("Chưa có bảng chốt, vui lòng kiểm tra lại!");
            }
        }
    }
}