using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Extends;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_UpdateAppObjectController : ViewController
    {
        public HeThong_UpdateAppObjectController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            //
            XPCollection<AppObject> appObjectList = new XPCollection<AppObject>(((XPObjectSpace)obs).Session);
            if (appObjectList != null)
            {
                //Lấy tất cả object trong solution
                var objectAll = from b in View.Model.Application.BOModel
                                where (b.TypeInfo.Type.Namespace.Contains("ERP") == true ||
                                        b.TypeInfo.Type.Namespace.Contains("PMS") == true)
                                && b.TypeInfo.Type.Namespace.Contains("Report") == false
                                orderby b.Caption
                                select new
                                {
                                    Caption = b.Caption,
                                    Type = b.TypeInfo.Type
                                };
                //
                using (DialogUtil.AutoWait())
                {
                    foreach (var item in objectAll)
                    {
                        var appObject = (from o in appObjectList
                                         where o.KeyObject == item.Type.Name
                                         select o).FirstOrDefault();
                        //
                        if (appObject == null)
                        {
                            AppObject appObjectNew = new AppObject(((XPObjectSpace)obs).Session);
                            appObjectNew.KeyObject = item.Type.Name;
                            appObjectNew.Caption = item.Caption;
                            appObjectNew.Save();
                        }
                    }
                    //
                    obs.CommitChanges();
                }

                #region
                //XPCollection<AppController> appControllerList = new XPCollection<AppController>(((XPObjectSpace)obs).Session);
                ////Cập nhật các nút Controller trong source
                //foreach (var item in Frame.Controllers)
                //{
                //    if (item.Name.Contains("ERP.Module.Win.Controllers.NghiepVu.QuanLyKho")
                //        || (item.Name.Contains("ERP.Module.Win.Controllers.NghiepVu.TSCD"))
                //        || (item.Name.Contains("ERP.Module.Win.Controllers.NghiepVu.CCDC")))
                //    {
                //        foreach(var button in item.Actions)
                //        {
                //            if (button.TargetObjectType != null)
                //            {
                //                var temp = (from o in appControllerList
                //                            where o.Key == button.Id
                //                            select o).FirstOrDefault();
                //                //
                //                if (temp == null)
                //                {
                //                    AppController appController = new AppController(((XPObjectSpace)obs).Session);
                //                    appController.Key = button.Id;
                //                    appController.Caption = button.Caption;
                //                    //
                //                    var appObject = (from o in appObjectList
                //                                     where o.KeyObject == button.TargetObjectType.Name
                //                                     select o).FirstOrDefault();
                //                    if (appObject != null)
                //                    {
                //                        appController.AppObject = button.TargetObjectType.Name;
                //                        appController.ObjectCaption = appObject.Caption;
                //                        appController.Save();
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                //obs.CommitChanges();
                #endregion
            }
        }
    }
}