using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.SystemModule;

namespace TestProject.Module.Win.Controllers {
    public class DeactivateDataLockingController :ViewController<DetailView> {
        protected override void OnActivated() {
            base.OnActivated();
            ProcessDataLockingInfoController dataLockingController = Frame.GetController<ProcessDataLockingInfoController>();
            if (dataLockingController != null)
                dataLockingController.Active["CustomLocking"] = false;
        }

        protected override void OnDeactivated() {
            ProcessDataLockingInfoController dataLockingController = Frame.GetController<ProcessDataLockingInfoController>();
            if(dataLockingController != null)
                dataLockingController.Active.RemoveItem("CustomLocking");
            base.OnDeactivated();
        }
    }
}
