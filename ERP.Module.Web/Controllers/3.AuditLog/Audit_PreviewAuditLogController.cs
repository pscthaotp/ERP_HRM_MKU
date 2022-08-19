using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;

namespace ERP.Module.Web.Controllers.AuditLog
{
    public partial class Audit_PreviewAuditLogController : ViewController
    {
        public Audit_PreviewAuditLogController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void Audit_PreviewAuditLogController_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View.CurrentObject is BaseObject)
            {
                CollectionSource auditLog = new CollectionSource(Application.CreateObjectSpace(), typeof(AuditDataItemPersistent));
                auditLog.Criteria["AuditObject"] = CriteriaOperator.Parse("AuditedObject.GuidId = ?", (View.CurrentObject as BaseObject).Oid);
                e.View = Application.CreateListView(Application.GetListViewId(typeof(AuditDataItemPersistent)), auditLog, true);
                e.DialogController.Active["ByAuditObject"] = false;
            }
        }
    }
}
