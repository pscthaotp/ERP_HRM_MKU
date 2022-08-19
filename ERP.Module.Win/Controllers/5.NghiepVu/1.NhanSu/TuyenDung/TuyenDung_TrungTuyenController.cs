using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using DevExpress.XtraEditors;
using ERP.Module.NghiepVu.NhanSu.Helper;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    public partial class TuyenDung_TrungTuyenController : ViewController
    {
        public TuyenDung_TrungTuyenController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TuyenDung_TrungTuyenController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<TrungTuyen>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuanLyTuyenDung qlTuyenDung = View.CurrentObject as QuanLyTuyenDung;
                if (qlTuyenDung != null)
                {
                    qlTuyenDung = obs.GetObjectByKey<QuanLyTuyenDung>(qlTuyenDung.Oid);   
                    if (qlTuyenDung != null)
                    {
                        TuyenDungHelper.TrungTuyen(obs, qlTuyenDung);
                        TuyenDungHelper.KhongTrungTuyen(obs, qlTuyenDung);
                    }
                }
                View.ObjectSpace.Refresh();
                View.Refresh();
            }

            XtraMessageBox.Show("Xét ứng viên trúng tuyển thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);               
        }
    }
}
