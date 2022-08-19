using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Windows.Forms;
using ERP.Module.Win.NormalForm.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Win.Editors.Commons;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_ChonCotXuatDuLieuController : ViewController
    {
        public NhanVien_ChonCotXuatDuLieuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            GridControl gridControl = View.Control as GridControl;
            if (gridControl != null)
            {
                GridView gridView = gridControl.FocusedView as GridView;
                if (gridView != null)
                {
                    using (frmListViewColumns dialog = new frmListViewColumns(typeof(ChiTietTimKiemNhanVien)))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            //Hide all columns
                            for (int i = 0; i < gridView.Columns.Count; i++)
                            {
                                gridView.Columns[i].Visible = false;
                            }

                            //Show columns choosed
                            List<ObjectProperty> data = dialog.GetData();
                            foreach (ObjectProperty item in data)
                            {
                                foreach (GridColumn col in gridView.Columns)
                                {
                                    if (col.FieldName == item.PropertyName 
                                        || col.FieldName == item.PropertyName + "!")
                                    {
                                        col.Visible = true;
                                        col.VisibleIndex = item.Index;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            e.ShowViewParameters.Context = TemplateContext.View;
        }
    }
}
