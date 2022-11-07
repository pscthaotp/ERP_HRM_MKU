using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using ERP.Module.Commons;
using ERP.Module.NonPersistentObjects;
using System.Data;

namespace ERP.Module.Win.Controllers.PMS
{
    public partial class QuanLyThongTinKLGiangDay_NonController : ViewController
    {

        IObjectSpace _obs = null;
        Session _Session;
        public QuanLyThongTinKLGiangDay_NonController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyThongTinKLGiangDay_Non_DetailView";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            IObjectSpace os1 = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)os1).Session;//Session được sử dụng để load và lưu lại  

            DetailView view = View as DetailView;

            QuanLyThongTinKLGiangDay_Non qly = View.CurrentObject as QuanLyThongTinKLGiangDay_Non;
            //
            if (view != null)
            {
                ControlViewItem item = ((DetailView)View).FindItem("btnLoadData") as ControlViewItem;
                //
                if (item != null)
                {
                    SimpleButton btnSearch = item.Control as SimpleButton;
                    item.Caption = null;
                    if (btnSearch != null)
                    {
                        btnSearch.Text = "Lấy dữ liệu";
                        btnSearch.Width = 80;
                        btnSearch.Click += (obj, ea) =>
                        {
                            //Gọi hàm createCommand() bên class TestDemo
                            qly.LoadData();
                        };
                    }
                }

            }
        }
    }
}