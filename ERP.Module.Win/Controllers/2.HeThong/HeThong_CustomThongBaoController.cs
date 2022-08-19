using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ScriptRecorder.Win;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Extends;
using ERP.Module.HeThong;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_CustomThongBaoController : ViewController
    {
        public HeThong_CustomThongBaoController()
        {
            InitializeComponent();
            TargetViewId = "ThongBao_ListView;";
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            ListView _listView = View as ListView;
            if (_listView != null && _listView.Editor is GridListEditor)//Nếu là lưới
            {
                //Ép sang kiểu lưới 
                GridView gridView = (_listView.Editor as GridListEditor).GridView;

                if (gridView != null)
                {
                    foreach(GridColumn column in gridView.Columns)
                    {
                        if (column.FieldName != "NotificationMessage")
                        {
                            column.Visible = false;
                        }
                        else
                            column.Visible = true;
                    }
                    //
                    ListViewProcessCurrentObjectController processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
                    processCurrentObjectController.ProcessCurrentObjectAction.Execute += ProcessCurrentObjectAction_Execute;
                }
            }
        }

        void ProcessCurrentObjectAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            ThongBao tb = e.CurrentObject as ThongBao;
            if (tb != null)
            {
                Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == tb.KeyObject);
                if (objecttype != null)
                {
                    object temp = obs.GetObjectByKey(objecttype, tb.Guid);
                    //
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, temp, true);
                    e.ShowViewParameters.Context = TemplateContext.View;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                }
            }
        }
    }
}
