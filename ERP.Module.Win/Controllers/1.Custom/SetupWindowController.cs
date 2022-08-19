using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Reports.Win;
using DevExpress.XtraLayout;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Layout;
using System.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.ExpressApp.TreeListEditors.Win;
using System.Resources;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using ERP.Module.HeThong;

namespace ERP.Module.Win.Controllers.Custom
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
            #region I. Basic
            TreeListEditor treeList = null;
            //
            DetailView detailView = View as DetailView;
            ListView listView = View as ListView;
            if (listView != null)
            {
                treeList = listView.Editor as TreeListEditor;
                #region 1. mở nút refresh trên list view nếu đã ẩn ở detaiview
                RefreshController refreshController = Frame.GetController<RefreshController>();
                if (refreshController != null)
                {
                    refreshController.RefreshAction.Active["Visible"] = true;
                }
                #endregion
                #region 2. Mở nút save trên list view nếu đã ẩn ở detaiview
                ModificationsController modificationsController = Frame.GetController<ModificationsController>();
                if (modificationsController != null)
                {
                    modificationsController.SaveAndCloseAction.Active["TruyCap"] = true;
                    modificationsController.SaveAction.Active["TruyCap"] = true;
                }
                #endregion
            }

            //
            if (detailView != null)
            {
                #region 0. Change caption of property use resource
                /* Tạm thời đi dùng khi cần thiết
                 
                // Note: Chỉ dùng cho các class BusinessObjects
                //Lấy resource hiện tại
                ResourceManager currentResource = Common.GetResourceManager(this.GetType().Assembly);

                //Set tên object
                string[] objectName = detailView.Id.Split('_');
                detailView.Caption = currentResource.GetString(objectName[0]);

                //Set tên property
                foreach (var item in detailView.GetItems<PropertyEditor>())
                {
                    string resourceKey = currentResource.GetString(item.Id);
                    if (resourceKey != null)
                        item.Caption = resourceKey;
                } 
                 */
                #endregion

                #region 1. Tô màu sau khi thay đổi lựa chọn trên from
                ((LayoutControl)(((WinLayoutManager)(detailView.LayoutManager)).Container)).OptionsView.HighlightFocusedItem = true;
                ((LayoutControl)(((WinLayoutManager)(detailView.LayoutManager)).Container)).Appearance.ControlFocused.BackColor = Color.FromArgb(255, 201, 100);
                #endregion

                #region 2. Ẩn nút thêm trên detail view
                NewObjectViewController addButtonDetailView = Frame.GetController<NewObjectViewController>();
                if (addButtonDetailView != null)
                   addButtonDetailView.NewObjectAction.Active["Visible"] = false;
                #endregion

                #region 3. Ẩn nút xóa trên detail view
                DeleteObjectsViewController deleteButtonDetailView = Frame.GetController<DeleteObjectsViewController>();
                if (deleteButtonDetailView != null)
                    deleteButtonDetailView.DeleteAction.Active["Visible"] = false;
                #endregion

                #region 4. Ẩn nút reset trên detailview
                ResetViewSettingsController resetButtonDetailView = Frame.GetController<ResetViewSettingsController>();
                if (resetButtonDetailView != null)
                    resetButtonDetailView.Active["Visible"] = false;
                #endregion

                #region 5. Ẩn nút refresh trên detail view
                // sau khi thêm detaiview ở đây lên "1. mở nút refresh trên list view" thêm id trong này.
                RefreshController refreshController = Frame.GetController<RefreshController>();
                if (refreshController != null)
                {

                    if ((View.CurrentObject != null
                            && View.CurrentObject is DevExpress.Persistent.BaseImpl.BaseObject
                            && ((DevExpress.Persistent.BaseImpl.BaseObject)View.CurrentObject).ClassInfo != null
                            && ((DevExpress.Persistent.BaseImpl.BaseObject)View.CurrentObject).ClassInfo.IsPersistent == false)
                        || View.Id.Contains("DangKyNgoaiKhoa_DetailView"))
                    #region if củ
                    //if (View.Id.Contains("ThuPhi_DetailView") 
                    //    || View.Id.Contains("ThuPhiLanDau_DetailView")
                    //    || View.Id.Contains("ThuChiPhatSinh_DetailView")
                    //    || View.Id.Contains("ThucDon_TimKiemNguyenPhuLieuTrongNgay_DetailView")
                    //    || View.Id.Contains("ThucDon_TimKiemTieuChuanTrongNgay_DetailView")
                    //    || View.Id.Contains("ThucDon_TimKiemThucDonTuan_DetailView")
                    //    || View.Id.Contains("ThucDon_TimKiemThucDonKhung_DetailView")
                    //    || View.Id.Contains("ChotSuatAn_TimKiemChotSuatAn_DetailView")
                    //    || View.Id.Contains("KhoBep_Tim_Bep_DanhGiaNhaCungCap_DetailView")
                    //    || View.Id.Contains("KhoBep_DeNghiXuatNhapKho_DetailView")
                    //    || View.Id.Contains("KhoBep_Tim_Bep_DonDatHangDuTru_DetailView")
                    //    || View.Id.Contains("KhoBep_Tim_Bep_DeNghiMuaHang_DetailView")
                    //    || View.Id.Contains("KhoBep_Tim_Bep_DeNghiXuatKho_DetailView")
                    //    || View.Id.Contains("KhoBep_Tim_Bep_Kho_DetailView")
                    //    || View.Id.Contains("DanhGiaKhauVi_HocSinh_DetailView")
                    //    || View.Id.Contains("SoKiemThuc_TimKiemSoKiemThucBuoc1A_DetailView")
                    //    || View.Id.Contains("SoKiemThuc_TimKiemSoKiemThucBuoc1B_DetailView")
                    //    || View.Id.Contains("SoKiemThuc_TimKiemSoKiemThucBuoc2_DetailView")
                    //    || View.Id.Contains("SoKiemThuc_TimKiemSoKiemThucBuoc3_DetailView")
                    //    || View.Id.Contains("SoKiemThuc_TimKiemSoLuuMau_DetailView")
                    //    || View.Id.Contains("QuanLyDonHangBan_DetailView")
                    //    || View.Id.Contains("CTKhung_DuyetCTKhung_DetailView")
                    //    || View.Id.Contains("CTKhung_DoTuan_DetailView")
                    //    || View.Id.Contains("CTKhung_DSChuDe_DetailView")
                    //    || View.Id.Contains("KeHoachTuan_TimParentDoParentAskKhoiHoanThanh_DetailView")
                    //    || View.Id.Contains("KeHoachTuan_TimParentDoParentAskKhoiDuyet1_DetailView")
                    //    || View.Id.Contains("KeHoachTuan_TimParentDoParentAskKhoiDuyet_DetailView")
                    //    || View.Id.Contains("KeHoachTuan_TimParentDoParentAskKhoiDoLop_DetailView")
                    //    || View.Id.Contains("TiemChung_TimTiemChungNgoaiTruong_DetailView")
                    //    )
                    #endregion
                    {
                        refreshController.RefreshAction.Active["Visible"] = false;
                    }
                    else
                        refreshController.RefreshAction.Active["Visible"] = true;

                }
                #endregion

                #region 6. Ẩn nút save trên detail view
                ModificationsController modificationsController = Frame.GetController<ModificationsController>();
                if (modificationsController != null)
                {
                    if (detailView.Id == "QuanLyCTKhung_DetailView"
                        || detailView.Id == "CTKhung_DuyetCTKhung_DetailView"
                        || detailView.Id == "CTKhung_DoTuan_DetailView"
                        || detailView.Id == "ReportCustomView_DetailView")
                    {
                        modificationsController.SaveAndCloseAction.Active["TruyCap"] = false;
                        modificationsController.SaveAction.Active["TruyCap"] = false;
                        if (detailView.Id == "ReportCustomView_DetailView")
                            modificationsController.CancelAction.Active["TruyCap"] = false;
                    }
                    else
                    {
                        modificationsController.SaveAndCloseAction.Active["TruyCap"] = true;
                        modificationsController.SaveAction.Active["TruyCap"] = true;
                    }
                }
                #endregion

                //Refesh model
                //detailView.SaveModel();
                //detailView.Refresh();

            }
            else
            {
                #region 1. Hiện nút xóa trên List view
                DeleteObjectsViewController deleteButtonDetailView = Frame.GetController<DeleteObjectsViewController>();
                if (deleteButtonDetailView != null)
                    deleteButtonDetailView.DeleteAction.Active["Visible"] = true;
                #endregion

            }

            if (listView != null && treeList == null)
            {
                #region 0. Change caption of property use resource   
                
                /* Tạm thời đi dùng khi cần thiết

                if (listView.Editor is GridListEditor)//Nếu là lưới
                {
                    //Ép sang kiểu lưới 
                    GridView gridView = (listView.Editor as GridListEditor).GridView;
                    if (gridView != null)
                    {   
                        //Lấy resource hiện tại
                        ResourceManager currentResource = Common.GetResourceManager(this.GetType().Assembly);

                        //Set tên object
                        string[] objectName = listView.Id.Split('_');
                        listView.Caption = currentResource.GetString(objectName[0]);

                        //Set tên property
                        foreach (GridColumn item in gridView.Columns)
                        {
                            string resourceKey = currentResource.GetString(item.Name);
                            if (resourceKey != null)
                                item.Caption = resourceKey;
                        }
                    }
                } */
                #endregion

                #region 1. Ẩn nút sửa trên báo cáo
                EditReportController editButtonReport = Frame.GetController<EditReportController>();
                if (editButtonReport != null)
                {
                    editButtonReport.Active.Clear();
                    editButtonReport.Active["TruyCap"] = false;
                }
                #endregion

                #region 2. Định dạng lại kiểu tiền tệ khi xuất excel
                ExportController exportController = Frame.GetController<ExportController>();
                if (exportController != null)
                {
                    exportController.CustomExport += ExportController_CustomExport;
                }
                #endregion

                #region 3. Mở nút thêm trên list view
                if (!View.Id.Contains("HocSinhDangLamViec_ListView")
                    && !View.Id.Contains("HocSinhDaNghiViec_ListView") 
                    && !View.Id.Contains("HocSinh_DetailView")
                    && !View.Id.Contains("NhanVienDangLamViec_ListView")
                    && !View.Id.Contains("NhanVienDangLamViec_DetailView")
                    && !View.Id.Contains("CongCuDungCuCaBiet_ListView")
                    && !View.Id.Contains("TaiSanCoDinhCaBiet_ListView")
                    && !View.Id.Contains("QuanLyCTTiengAnh_ListView")
                    && !View.Id.Contains("ThucDon_TimKiemThucDonKhung_ThucDonKhungList_ListView"))
                {
                    WinNewObjectViewController addButtonListView = Frame.GetController<WinNewObjectViewController>();
                    if (addButtonListView != null)
                        addButtonListView.NewObjectAction.Active["Visible"] = true;
                }
                #endregion
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


            #region Mẫn thêm code hỗ trợ LogInOut
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
            #endregion
        }

        #region Event control
        void ExportController_CustomExport(object sender, CustomExportEventArgs e)
        {
            GridListEditor gridListEditor = ((ListView)View).Editor as GridListEditor;

            if (gridListEditor != null)
            {
                //Set format tạm cho cột trước khi xuất ra excel để bỏ kiểu currency
                foreach (GridColumn item in gridListEditor.GridView.Columns)
                {
                    if (item.ColumnType == typeof(decimal))
                        item.DisplayFormat.FormatString = "g";
                    else if (item.ColumnType == typeof(Int32))
                        item.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                }
                //
                XlsExportOptions options = new XlsExportOptions();
                options.SheetName = "Sheet1";
                options.ShowGridLines = true;
                options.ExportMode = XlsExportMode.SingleFile;
                options.TextExportMode = TextExportMode.Value;
                gridListEditor.GridView.ExportToXls(e.Stream, options);
                e.Handled = true;

                //Khôi phụ lại format thật cho cột
                foreach (GridColumn item in gridListEditor.GridView.Columns)
                {
                    if (item.ColumnType == typeof(decimal) || item.ColumnType == typeof(Int32))
                        item.DisplayFormat.FormatString = item.RealColumnEdit.DisplayFormat.FormatString;
                }
            }
        }

        #endregion

        //protected override void OnActivated()
        //{
        //    base.OnActivated();
        //    if (View.ObjectSpace.ToString().Contains("DevExpress.ExpressApp.NonPersistentObjectSpace"))
        //    {
        //        Frame.GetController<ModificationsController>().SaveAction.Active["Visible"] = false;
        //        Frame.GetController<ModificationsController>().SaveAndCloseAction.Active["Visible"] = false;
        //    }
        //}
    }
}
