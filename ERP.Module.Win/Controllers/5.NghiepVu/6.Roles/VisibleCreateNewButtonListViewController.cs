using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using System.Drawing;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.IO;
using DevExpress.ExpressApp.SystemModule;
using ERP.Module.HeThong;
using DevExpress.ExpressApp.Win.SystemModule;
//
namespace ERP.Module.Win.Controllers.Roles
{
    public partial class VisibleCreateNewButtonListViewController : ViewController
    {
        public VisibleCreateNewButtonListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void VisibleCreateNewButtonListViewController_Activated(object sender, EventArgs e)
        {
            if (View.Id.Contains("QuanLyCTTiengAnh_ListView")
                || View.Id.Contains("ThucDon_TimKiemThucDonKhung_ThucDonKhungList_ListView"))
            {
                //Tắt nút thêm mới mặc định của DevExpress, sau khi tạo xong qua class SetupWindowController->3. Mở nút thêm trên list view->thêm View.id vào điều kiện.
                WinNewObjectViewController addButtonListView = Frame.GetController<WinNewObjectViewController>();
                if (addButtonListView != null)
                    addButtonListView.NewObjectAction.Active["Visible"] = false;
            }
        }

    }
}
