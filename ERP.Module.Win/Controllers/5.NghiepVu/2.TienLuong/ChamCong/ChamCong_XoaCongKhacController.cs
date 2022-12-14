using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using DevExpress.Data.Filtering;
using ERP.Module.DanhMuc.TienLuong;
//

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChamCong
{
    public partial class ChamCong_XoaCongKhacController : ViewController
    {
        public ChamCong_XoaCongKhacController()
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
            if (obj.Count == 1 && obj[0] is CC_ChamCongKhac)
            {
                ExecuteCustomDeleteObjects("spd_ChamCong_XoaChamCongKhac");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            CC_ChamCongKhac chamCongKhac = View.CurrentObject as CC_ChamCongKhac;
            if (chamCongKhac != null)
            {
                if (chamCongKhac.KyChamCong.KhoaSo)
                {
                    DialogUtil.ShowWarning("Kỳ chấm công đã khóa. Không thể xóa !!!");
                    return;
                }
                if (chamCongKhac.Chot)
                {
                    DialogUtil.ShowWarning("Chấm công đã chốt. Không thể xóa !!!");
                    return;
                }

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("ChamCongKhac", chamCongKhac.Oid);
                //
                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa Chấm công khác thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa Chấm công khác!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
