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
    public partial class ChamCong_XoaBangChamCongController : ViewController
    {
        public ChamCong_XoaBangChamCongController()
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
            if (obj.Count == 1 && obj[0] is CC_QuanLyChamCong)
            {
                ExecuteCustomDeleteObjects("spd_ChamCong_XoaQuanLyChamCong");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            //
            CC_QuanLyChamCong bangChamcong = View.CurrentObject as CC_QuanLyChamCong;
            if (bangChamcong != null)
            {
                if (bangChamcong.KyChamCong.KhoaSo)
                {
                    DialogUtil.ShowWarning("Kỳ chấm công đã khóa. Không thể xóa !!!");
                    return;
                }
                //
                CriteriaOperator filter = CriteriaOperator.Parse("QuanLyChamCong.Oid=?",bangChamcong.Oid);
                KyTinhLuong kyTinhLuong = View.ObjectSpace.FindObject<KyTinhLuong>(filter);
                if (kyTinhLuong != null)
                {
                    DialogUtil.ShowWarning("Đã liên kết kỳ tính lương. Không thể xóa !!!");
                    return;
                }

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("QuanLyChamCong", bangChamcong.Oid);
                //
                int message = DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, param);
                if (message != -1)
                {
                    DialogUtil.ShowInfo("Xóa bảng chấm công thành công!!!");
                }
                else
                {
                    DialogUtil.ShowError("Không thể xóa Bảng chấm công!!!");
                }
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
