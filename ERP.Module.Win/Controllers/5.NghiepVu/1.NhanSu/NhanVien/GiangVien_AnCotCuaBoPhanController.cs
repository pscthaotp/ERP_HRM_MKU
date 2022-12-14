using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Columns;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using ERP.Module.Win.Editors.NhanSu.NhanVien;
using ERP.Module.HeThong;
using ERP.Module.Win.Editors.NhanSu.NhanViens;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class GiangVien_AnCotCuaBoPhanController : ViewController
    {
        public GiangVien_AnCotCuaBoPhanController()
        {
            InitializeComponent();
            RegisterActions(components);
            //
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(ICategorizedItem);
            Activated += GiangVienThinhGiang_AnCotCuaBoPhanController_Activated;
            Deactivated += GiangVienThinhGiang_AnCotCuaBoPhanController_Deactivating;
        }

        private void GiangVienThinhGiang_AnCotCuaBoPhanController_Activated(object sender, EventArgs e)
        {
            View.ControlsCreated += View_ControlsCreated;
        }

        private void GiangVienThinhGiang_AnCotCuaBoPhanController_Deactivating(object sender, EventArgs e)
        {
            View.ControlsCreated -= View_ControlsCreated;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            #region Thông tin giảng viên thỉnh giảng
            CategorizedListEditor_GiangVienThinhGiang editorGiangVienThinhGiang = (View as DevExpress.ExpressApp.ListView).Editor as CategorizedListEditor_GiangVienThinhGiang;
            if (editorGiangVienThinhGiang != null)
            {
                DevExpress.ExpressApp.ListView listView = editorGiangVienThinhGiang.CategoriesListView;

                if (listView != null)
                {
                    TreeListEditor treeListEditor = listView.Editor as TreeListEditor;
                    if (treeListEditor != null)
                    {
                        TreeList treeList = treeListEditor.TreeList;
                        if (treeList != null)
                        {
                            foreach (TreeListColumn item in treeList.Columns)
                            {
                                if (item.Name != "TenBoPhan")
                                    item.Visible = false;
                            }
                            TreeListColumn column = treeList.Columns["STT"];
                            if (column != null)
                            {
                                column.SortIndex = 0;
                                column.SortOrder = SortOrder.Ascending;
                            }
                            column = treeList.Columns["TenBoPhan"];
                            if (column != null)
                            {
                                column.SortIndex = 1;
                                column.SortOrder = SortOrder.Ascending;
                            }
                        }
                    }

                    SecuritySystemUser_Custom user = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
                    if (user != null)
                    {
                        if (editorGiangVienThinhGiang.Name == "Giảng viên thỉnh giảng")
                        {
                            GridView gridEditor = editorGiangVienThinhGiang.GridView;
                            if (gridEditor != null)
                            {
                                gridEditor.Columns[1].SortIndex = 0;
                                gridEditor.Columns[1].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                                gridEditor.Columns[0].SortIndex = 1;
                                gridEditor.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                            }
                        }
                    }                
                }
            }
            #endregion
        }
    }
}
