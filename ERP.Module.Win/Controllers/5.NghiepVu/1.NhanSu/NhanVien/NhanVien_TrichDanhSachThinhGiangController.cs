using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using ERP.Module.Win.NormalForm.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_TrichDanhSachThinhGiangController : ViewController
    {
        public NhanVien_TrichDanhSachThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            using (frmChonThinhGiang chonCanBo = new frmChonThinhGiang(((XPObjectSpace)obs).Session))
            {
                if (chonCanBo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    (View.CurrentObject as TrichDanhSachThinhGiang).ListChiTietTrichDanhSachThinhGiang = chonCanBo.GetStaffList();
                    (View as DetailView).Refresh();
                }
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                e.ShowViewParameters.Context = TemplateContext.View;
            }
        }
    }
}
