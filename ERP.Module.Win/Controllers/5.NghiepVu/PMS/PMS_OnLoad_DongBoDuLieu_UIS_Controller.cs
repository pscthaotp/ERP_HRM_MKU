using DevExpress.ExpressApp;
using DevExpress.Xpo;
using ERP.Module.Commons;
using System.Data;

namespace ERP.Module.Win.Controllers.PMS
{
    public partial class PMS_OnLoad_DongBoDuLieu_UIS_Controller : ViewController
    {

        IObjectSpace _obs = null;
        Session _Session;
        public PMS_OnLoad_DongBoDuLieu_UIS_Controller()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            //TargetViewId = "AppMenu_DetailView";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            ListView _listView = View as ListView;
            if (_listView != null)
                DataProvider.ExecuteNonQuery("dbo.spd_PMS_DongBoDuLieu_PMS", CommandType.StoredProcedure);
        }
    }
}