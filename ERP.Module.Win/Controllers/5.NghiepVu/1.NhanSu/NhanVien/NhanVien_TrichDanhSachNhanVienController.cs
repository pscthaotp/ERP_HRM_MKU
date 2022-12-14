using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using ERP.Module.Win.NormalForm.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_TrichDanhSachNhanVienController : ViewController
    {
        public NhanVien_TrichDanhSachNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            //
            using (frmChonCanBo chonCanBo = new frmChonCanBo(((XPObjectSpace)obs).Session))
            {
                if (chonCanBo.ShowDialog() == DialogResult.OK)
                {
                    (View.CurrentObject as TrichDanhSachNhanVien).ListChiTietTrichDanhSachNhanVien = chonCanBo.GetStaffList();
                    (View as DetailView).Refresh();
                }
                //Show
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                e.ShowViewParameters.Context = TemplateContext.View;
            }
        }
    }
}
