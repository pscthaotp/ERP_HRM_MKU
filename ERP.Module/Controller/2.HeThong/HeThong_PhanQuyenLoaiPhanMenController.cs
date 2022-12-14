using System;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.HeThong;
using ERP.Module.Commons;
//
namespace ERP.Module.Controller.HeThong
{
    public partial class HeThong_PhanQuyenLoaiPhanMenController : ViewController<ListView>
    {
        public HeThong_PhanQuyenLoaiPhanMenController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(ILoaiPhanMen);
        }

        private void HeThong_PhanQuyenLoaiPhanMenController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            //
            if (listView != null)
            {
                #region
                if (listView.ObjectTypeInfo.Implements<ILoaiPhanMen>())
                {
                    CriteriaOperator criteria = string.Empty;
                    if (Config.TypeApplication.Equals("WebForm"))
                    {
                        criteria = CriteriaOperator.Parse("LoaiPhanMen = 1");
                    }
                    else
                    {
                        criteria = CriteriaOperator.Parse("LoaiPhanMen = 0");
                    }
                    //
                    listView.CollectionSource.Criteria["PhanQuyenLoaiPhanMen"] = criteria;
                }
                #endregion
            }
        }
    }
}
