using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.TreeListEditors.Web;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Actions;
using ERP.Module.Commons;

namespace ERP.Module.Web.Controllers.Custom
{
    public partial class SetupWindowController : ViewController
    {
        public SetupWindowController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void SetupWindowController_ViewControlsCreated(object sender, EventArgs e)
        {

            #region I. DetailView and ListView

            #region 1. Ẩn quick creat action
            WebNewObjectViewController quickCreateAction = Frame.GetController<WebNewObjectViewController>();
            if (quickCreateAction != null)
            {
                quickCreateAction.QuickCreateAction.Items.Clear();
            }

            #endregion

            #region 2. Ẩn Breadcrumbs
            WebViewNavigationController breadcrumbsDetailView = Frame.GetController<WebViewNavigationController>();
            if (breadcrumbsDetailView != null)
            {
                breadcrumbsDetailView.NavigateToAction.Active["DisableBreadcrumbs"] = false;
            }
            #endregion

            #endregion

            #region II. DetailView or ListView
            ASPxTreeListEditor treeList = null;
            //
            DetailView detailView = View as DetailView;
            ListView listView = View as ListView;
            if (listView != null)
                treeList = listView.Editor as ASPxTreeListEditor;
            //
            if (detailView != null)
            {
                #region 1. Ẩn nút chọn item trên detail view
                ChooseThemeController chooseThemeButtonDetailView = Frame.GetController<ChooseThemeController>();
                if (chooseThemeButtonDetailView != null)
                    chooseThemeButtonDetailView.ChooseThemeAction.Active["Visible"] = false;
                #endregion

                #region 2. Ẩn nút thêm trên detail view
                WebNewObjectViewController addButtonDetailView = Frame.GetController<WebNewObjectViewController>();
                if (addButtonDetailView != null)
                    addButtonDetailView.NewObjectAction.Active["Visible"] = false;
                #endregion

                #region 3. Ẩn nút xóa trên detail view
                DeleteObjectsViewController deleteButtonDetailView = Frame.GetController<DeleteObjectsViewController>();
                if (deleteButtonDetailView != null)
                    deleteButtonDetailView.DeleteAction.Active["Visible"] = false;
                #endregion

                #region 4. Ẩn nút refresh trên detail view
                RefreshController refreshController = Frame.GetController<RefreshController>();
                if (refreshController != null)
                {
                    if (View.Id.Contains("TuVanTuyenSinh_Email_DetailView")
                        || View.Id.Contains("TuVanTuyenSinh_DienThoai_DetailView")
                        || View.Id.Contains("ThongBaoNhapHoc_Email_DetailView")
                        || View.Id.Contains("ThongBaoNhapHoc_TrucTiep_DetailView")
                        || View.Id.Contains("ThongBaoNhapHoc_SMS_DetailView")
                        || View.Id.Contains("DuyetThongBaoTuyenSinh_DetailView")
                        || View.Id.Contains("DuyetToChucSuKien_DetailView"))
                    {
                        refreshController.RefreshAction.Active["Visible"] = false;
                    }
                    else
                        refreshController.RefreshAction.Active["Visible"] = true;

                }
                #endregion

                //
                ExportController exportController = Frame.GetController<ExportController>();
                if (exportController != null)
                {
                    exportController.ExportAction.Active[""] = true;
                }
                else
                    exportController.ExportAction.Active["Visible"] = false;
                
            }
            else
            {
                #region 1. Mờ nút chọn item trên detail view
                ChooseThemeController chooseThemeButtonDetailView = Frame.GetController<ChooseThemeController>();
                if (chooseThemeButtonDetailView != null)
                    chooseThemeButtonDetailView.ChooseThemeAction.Active["Visible"] = true;
                #endregion

                #region 2. Hiện nút thêm trên list view
                WebNewObjectViewController addButtonListView = Frame.GetController<WebNewObjectViewController>();
                if (addButtonListView != null)
                    addButtonListView.NewObjectAction.Active["Visible"] = true;
                #endregion

                #region 3. Hiện nút xóa trên detail view
                DeleteObjectsViewController deleteButtonDetailView = Frame.GetController<DeleteObjectsViewController>();
                if (deleteButtonDetailView != null)
                    deleteButtonDetailView.DeleteAction.Active["Visible"] = true;
                #endregion

                #region 4. Hiện nút refresh trên list view
                RefreshController refreshController = Frame.GetController<RefreshController>();
                if (refreshController != null)
                {
                        refreshController.RefreshAction.Active["Visible"] = true;
                }
                #endregion
            
            }

            if (listView != null && treeList == null)
            {
                #region 1. Định dạng lại kiểu tiền tệ khi xuất excel
                ExportController exportController = Frame.GetController<ExportController>();
                if (exportController != null)
                {
                    //exportController.CustomExport += ExportController_CustomExport;
                }
                #endregion

                #region 2. Tắt không cho mở form dạng detailview
               
                //Mở xem view dạng detail
                ListViewProcessCurrentObjectController processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
                if (processCurrentObjectController != null)
                {
                    processCurrentObjectController.ProcessCurrentObjectAction.Enabled["Visible"] = true;
                }
          
                #endregion

            }
            else
            {

            }

            if (treeList != null)
            {
                #region Ẩn nút thêm trên treelist
                if (treeList.Name.Contains("Danh sách chức năng"))
                {
                    NewObjectViewController addButton = Frame.GetController<NewObjectViewController>();
                    if (addButton != null)
                        addButton.NewObjectAction.Active["Visible"] = false;
                }
                #endregion
            }
            #endregion
        }
    }
}
