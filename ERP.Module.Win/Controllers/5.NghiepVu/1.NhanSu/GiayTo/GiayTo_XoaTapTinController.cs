using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.Extends;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.GiayTo
{
    public partial class GiayTo_XoaTapTinController : ViewController
    {
        public GiayTo_XoaTapTinController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            GiayToHoSo file = View.CurrentObject as GiayToHoSo;
            if (file == null)
            {
                return;
            }
            else if (file.LoaiGiayTo == null)
            {
                //
                DialogUtil.ShowError("Không thể xóa loại tập tin.");
                return;
            }
            else
            {
                // Tiến hành xóa dữ liệu
                ExecuteCustomDeleteObjects(file);
            }
        }

        private void GiayTo_XoaTapTinController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<GiayToHoSo>();
        }

        private void ExecuteCustomDeleteObjects(GiayToHoSo file)
        {
            //
            try
            {
                using (DialogUtil.AutoWait())
                {
                    //Xóa file trên server
                    if (!string.IsNullOrEmpty(file.DuongDanFile) && !string.IsNullOrEmpty(file.TenGiayTo))
                    {
                        //
                        FptProvider.DeleteFileOnServerNew(file.DuongDanFile, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Username, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Password);
                    }
                    //
                    DataProvider.ExecuteNonQuery("spd_GiayToHoSo_XoaGiayToHoSo", CommandType.StoredProcedure, new SqlParameter("@Oid", file.Oid));
                }
                //
                DialogUtil.ShowInfo("Xóa tập tin thành công!!!");
            }
            catch (Exception ex)
            {

                DialogUtil.ShowError("Không thể xóa file trên máy chủ...");
            }
            //
            View.ObjectSpace.Refresh();
        }
    }
}
