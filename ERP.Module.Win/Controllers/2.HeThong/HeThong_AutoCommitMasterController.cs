using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.SystemModule;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_AutoCommitMasterController : ViewController
    {
        public HeThong_AutoCommitMasterController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

         private void ObjectSpace_Committed(object sender, EventArgs e) 
         {
             if (this.View.Id.Contains("DangKyTuyenDung") || this.View.Id.Contains("UngVien") || this.View.Id.Contains("VongTuyenDung")
                 || this.View.Id.Contains("ChiTietVongTuyenDung") || this.View.Id.Contains("ChiTietTuyenDung")               
                 || this.View.Id == "SoPhatThuoc_DetailView")
             {
                 IObjectSpace objectSpace = this.View.ObjectSpace;
                 IObjectSpace parentObjectSpace = null;
                 try
                 {
                     if (objectSpace is XPNestedObjectSpace)
                     {
                         parentObjectSpace = ((XPNestedObjectSpace)objectSpace).ParentObjectSpace;
                         LinkToListViewController linkToListViewController = Frame.GetController<LinkToListViewController>();
                         if (linkToListViewController != null
                             && linkToListViewController.Link != null
                             && linkToListViewController.Link.ListView != null
                             && linkToListViewController.Link.ListView.CollectionSource is PropertyCollectionSource
                             && !objectSpace.IsNewObject(((PropertyCollectionSource)linkToListViewController.Link.ListView.CollectionSource).MasterObject))
                             parentObjectSpace.CommitChanges();
                     }
                 }
                 catch (Exception)
                 {
                     if (parentObjectSpace != null)
                     {
                         parentObjectSpace.Rollback();
                     }
                     throw;
                 }
             }
        }

        protected override void OnActivated() {
            base.OnActivated();
            ObjectSpace.Committed += ObjectSpace_Committed;
        }

        protected override void OnDeactivated() {
            ObjectSpace.Committed -= ObjectSpace_Committed;
            base.OnDeactivated();
        }
                 
      
    }
}
