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
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using DevExpress.ExpressApp.Actions;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.ExpressApp.ScriptRecorder;
using ERP.Module.Win.Controllers.HeThong;
using DevExpress.ExpressApp.ScriptRecorder.Win;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_ScriptRecorderController : ViewController
    {
        public HeThong_ScriptRecorderController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        protected override void OnActivated()
        {
            //Nếu chuỗi script != null thì tìm LogInOut hiện tại và cập nhật ActivitiesLog
            if (DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog != null 
                    && AuthenticationStandard_CustomWin.logInOut != null)
            {
                //Lưu lại ActivitiesLog
                IObjectSpace obs = Application.CreateObjectSpace();
                LogInOut log = obs.GetObjectByKey<LogInOut>(AuthenticationStandard_CustomWin.logInOut.Oid);
                if (log != null)
                {
                    //Replace password lại
                    log.ActivitiesLog = DateTime.Now.ToString() + " - " + DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog;
                    obs.CommitChanges();
                }
            }
        }
        
        protected override void OnDeactivated()
        {
            //Nếu chuỗi script != null thì tìm LogInOut hiện tại và cập nhật ActivitiesLog
            if (DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog != null 
                    && AuthenticationStandard_CustomWin.logInOut != null)
            {
                //Lưu lại ActivitiesLog
                IObjectSpace obs = Application.CreateObjectSpace();
                LogInOut log = obs.GetObjectByKey<LogInOut>(AuthenticationStandard_CustomWin.logInOut.Oid);
                if (log != null)
                {
                    //Replace password lại
                    log.ActivitiesLog = DateTime.Now.ToString() + " - " + DevExpress.ExpressApp.ScriptRecorder.Logger.Instance.Script.ScriptLog;
                    obs.CommitChanges();
                }
            }
        }
    }
}