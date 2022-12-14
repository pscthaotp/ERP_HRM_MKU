using DevExpress.ExpressApp;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.ChamCong;

//

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChamCong
{
    public partial class ChamCong_XoaBangCongGiangDayController : ViewController
    {
        public ChamCong_XoaBangCongGiangDayController()
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
            if (obj.Count == 1 && obj[0] is CC_QuanLyCongNgoaiGio)
            {
                ExecuteCustomDeleteObjects("spd_ChamCong_XoaQuanLyCongGiangDay");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            CC_QuanLyCongGiangDay bangChamcong = View.CurrentObject as CC_QuanLyCongGiangDay;
            if (bangChamcong != null)
            {
                if (bangChamcong.KhoaSo)
                {
                    DialogUtil.ShowWarning("Bảng công giảng dạy đã khóa. Không thể xóa !!!");
                    return;
                }

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("QuanLyChamCongGiangDay", bangChamcong.Oid);
                //
                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa bảng chấm công giảng dạy thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa Bảng chấm công giảng dạy!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
