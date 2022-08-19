using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.Extends;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.GiayTo
{
    public partial class GiayTo_XemTapTinController : ViewController
    {
        //
        public GiayTo_XemTapTinController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            GiayToHoSo obj = View.CurrentObject as GiayToHoSo;
            if (obj != null)
            {
                if (obj.LoaiGiayTo == null)
                {
                    DialogUtil.ShowWarning("Chọn tập tin để xem !!!");
                    return;
                }
                //
                try
                {
                    byte[] data = FptProvider.DownloadFile(obj.DuongDanFile, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Username, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Password);
                    if (data != null)
                    {
                        string strTenFile = obj.TenGiayTo;

                        //Lưu file vào thư mục bin\Debug
                        FptProvider.SaveFilePDF(data, strTenFile);

                        //Đọc file pdf
                        Process.Start(new ProcessStartInfo(strTenFile));
                    }
                    else
                        DialogUtil.ShowError("Không có chương trình đọc tập tin.");
                }
                catch(Exception ex)
                {
                    DialogUtil.ShowError("Giấy tờ hồ sơ không tồn tại trên máy chủ." + ex.Message);
                }
                
            }
        }

        private void GiayTo_XemTapTinController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<GiayToHoSo>();
        }
    }
}
