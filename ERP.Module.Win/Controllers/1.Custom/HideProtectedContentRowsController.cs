using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Win.Editors;
using System;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using System.Linq;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class HideProtectedContentRowsController : ViewController<ListView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            GridListEditor gridListEditor = View.Editor as GridListEditor;
            if (gridListEditor != null &&
                (View.Id.Contains("LopNgoaiKhoa_ListView")
                || View.Id == ("DangKyNgoaiKhoa_ListView")
                || View.Id.Contains("LopNgoaiKhoa_LookupListView")
                || View.Id.Contains("LoaiPhi_LookupListView")))
            {
                if (gridListEditor.GridView != null)
                {
                }
                else
                {
                    gridListEditor.ControlsCreated += gridListEditor_ControlsCreated;
                }
            }
        }
        protected void gridListEditor_ControlsCreated(object sender, EventArgs e)
        {
            ((GridListEditor)sender).GridView.CustomRowFilter += GridView_CustomRowFilter;
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            GridListEditor gridListEditor = View.Editor as GridListEditor;
            if (gridListEditor != null &&
                (View.Id.Contains("LopNgoaiKhoa_ListView")
                || View.Id == ("DangKyNgoaiKhoa_ListView")
                || View.Id.Contains("LopNgoaiKhoa_LookupListView")
                || View.Id.Contains("LoaiPhi_LookupListView")))
            {
                gridListEditor.GridView.CustomRowFilter -= GridView_CustomRowFilter;
            }
        }
        void GridView_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            GridView gridView = (GridView)sender;
            IList list = gridView.DataSource as IList;
            if (list == null) return;
            object obj = list[e.ListSourceRow];
            if (obj == null) return;
            var className = obj.GetType().Name;
            #region Kiểm tra class Nonpersistent
            Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == className);
            var isNonper = objecttype.GetCustomAttributes(typeof(NonPersistentAttribute), false).Length != 0;
            #endregion
            if (className != "QuanLyLopNgoaiKhoa_DanhSachLop")//if(!isNonper)//Không biết đang lỗi phân quyền gì? nếu không cần kiểm tra phân quyền cho class nonpersistent thì sử dụng biến isNonper để kiểm tra
            {
                var item = SecuritySystem.IsGranted(new ClientPermissionRequest(obj.GetType(), null, ObjectSpace.GetObjectHandle(obj), SecurityOperations.Read));
                if (!item)//PermissionRequest(View.ObjectSpace, obj.GetType(), "Read"))) ;//
                {
                    e.Visible = false;
                    e.Handled = true;
                }
            }
        }
    }

}
