using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.ExpressApp.Security.Strategy;

namespace ERP.Module.Win.Controller.AuditLog
{
    public partial class Audit_PreviewAuditLogController : ViewController
    {
        private IObjectSpace obs;
        private NhanVien nhanVien;

        public Audit_PreviewAuditLogController()
        {
            InitializeComponent();
            RegisterActions(components);            
        }

        private void Audit_PreviewAuditLogController_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();

            if (View.CurrentObject is BaseObject || View.CurrentObject is SecuritySystemUser || View.CurrentObject is SecuritySystemRole)
            {             
                CollectionSource audit = new CollectionSource(Application.CreateObjectSpace(), typeof(AuditDataItemPersistent));
                if (View.Id.Contains("ThongTinNhanVien_DetailView"))
                {                    
                    nhanVien = obs.FindObject<NhanVien>(CriteriaOperator.Parse("Oid=?", (View.CurrentObject as BaseObject).Oid));
                    if (nhanVien.NhanVienThongTinLuong != null && nhanVien.NhanVienTrinhDo != null)
                        audit.Criteria["AuditObject"] = CriteriaOperator.Parse("AuditedObject.GuidId = ? OR AuditedObject.GuidId = ? OR AuditedObject.GuidId = ?", (View.CurrentObject as BaseObject).Oid, nhanVien.NhanVienThongTinLuong.Oid, nhanVien.NhanVienTrinhDo.Oid);
                }
                else if (View.Id.Contains("SecuritySystemUser_Custom_DetailView"))
                {
                    audit.Criteria["AuditObject"] = CriteriaOperator.Parse("AuditedObject.GuidId = ? and PropertyName not like '%Password%'", (View.CurrentObject as SecuritySystemUser).Oid);
                }
                else if (View.Id.Contains("SecuritySystemRole_Custom_DetailView"))
                {
                    audit.Criteria["AuditObject"] = CriteriaOperator.Parse("AuditedObject.GuidId = ?", (View.CurrentObject as SecuritySystemRole).Oid);
                }
                else
                {
                    audit.Criteria["AuditObject"] = CriteriaOperator.Parse("AuditedObject.GuidId = ?", (View.CurrentObject as BaseObject).Oid);
                }
                e.View = Application.CreateListView(Application.GetListViewId(typeof(AuditDataItemPersistent)), audit, true);
                e.DialogController.Active["ByAuditObject"] = false;
            }            
        }
    }
}
