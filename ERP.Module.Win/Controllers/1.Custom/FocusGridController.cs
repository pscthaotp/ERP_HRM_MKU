using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using DevExpress.XtraBars;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.ExpressApp.Templates;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
using System.Drawing;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NonPersistentObjects.ReportCustom;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class FocusGridController : ViewController<ListView>
    {
        ListView _listView;
        GridView _gridView;
        TreeList _treeView;

        public FocusGridController()
        {
            InitializeComponent();
        }

        private void FocusGridController_Activated(object sender, EventArgs e)
        {
            //Cài đặt lưới ở đây
            View.ControlsCreated += View_ControlsCreated;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            //Lấy listview
            _listView = View as ListView;

            //Nếu đối tượng là lưới và NonPersistent
            if (_listView != null  && _listView.Editor is GridListEditor
                                   && _listView.ObjectTypeInfo != null 
                                   && !_listView.ObjectTypeInfo.IsPersistent
                                   && View.Id != "TimKiem_NgoaiKhoa_ListDangKyNgoaiKhoa_ListView")
            {
                //Ép sang kiểu lưới 
                _gridView = (_listView.Editor as GridListEditor).GridView;
                if (_gridView != null)
                {
                    _gridView.RowCellClick += gridView_RowCellClick;
                }
            }

            //Nếu đối tượng là cây và NonPersistent
            if (_listView != null && _listView.Editor is TreeListEditor
                                  && _listView.ObjectTypeInfo != null
                                  && !_listView.ObjectTypeInfo.IsPersistent)
            {
                //Ép sang kiểu cây 
                _treeView = (_listView.Editor as TreeListEditor).TreeList;
                _treeView.Click += tree_Click;
            }
        }

        void tree_Click(object sender, EventArgs e)
        { 
        
            //#region CTKhung_DoTuan_DoTuanNhanhList_ListView
            //if (_listView.Id == "CTKhung_DoTuan_DoTuanNhanhList_ListView")
            //{
            //    System.Windows.Forms.MouseEventArgs mouse = ((System.Windows.Forms.MouseEventArgs)e);
            //    //Lấy các thuộc tính của vùng thao tác
            //    TreeListHitInfo hitInfo = _treeView.CalcHitInfo(new Point(mouse.X, mouse.Y));
            //    if (hitInfo.HitInfoType != HitInfoType.Button) //Kiểm tra kiểu của vùng thao tác (trường hợp bấm vào nút Expand)
            //    {
            //        TreeListColumn col = hitInfo.Column; //Lấy cột đang click
            //        if (col != null && col.ColumnType.FullName == "System.Boolean") //Nếu kiểu cột là bool
            //        {
            //            CTKhung_DoTuanNhanh data = (CTKhung_DoTuanNhanh)hitInfo.Node.Tag; //Lấy object của node đang bấm
            //            //
            //            if ((col.FieldName == "Chon" && data != null && data.DoTuanNhanhCha == null)
            //                 || (col.FieldName == "Chuan" && data != null && data.DoTuanNhanhCha != null))
            //            {
            //                return;
            //            }

            //            object cellValue = hitInfo.Node.GetValue(col); //Lấy giá trị của node
            //            if (cellValue is bool)
            //            {
            //                if (col.FieldName == "Chon")
            //                    data.Chon = !((bool)cellValue);
            //                else
            //                    data.Chuan = !((bool)cellValue);
            //            }
            //        }
            //    }
            //}
            //View.Refresh();
            //#endregion

            #region HocSinhCustomView_LopList_ListView
            if (_listView.Id == "HocSinhCustomView_LopList_ListView"
                || _listView.Id == "NhanSuCustomView_BoPhanList_ListView"
                || _listView.Id == "ThinhGiangCustomView_BoPhanList_ListView"
                || _listView.Id == "QuanLyHocSinh_NghiHoc_LopList_ListView"
                || _listView.Id == "ReportCustomView_ReportViewList_ListView"
                )
            {
                System.Windows.Forms.MouseEventArgs mouse = ((System.Windows.Forms.MouseEventArgs)e);
                //Lấy các thuộc tính của vùng thao tác
                TreeListHitInfo hitInfo = _treeView.CalcHitInfo(new Point(mouse.X, mouse.Y));
                if (hitInfo.HitInfoType != HitInfoType.Button) //Kiểm tra kiểu của vùng thao tác (trường hợp bấm vào nút Expand)
                {
                    TreeListColumn col = hitInfo.Column; //Lấy cột đang click
                    if (col != null && hitInfo.Node != null)
                    {
                        //if (hitInfo.Node.Tag is LopView)
                        //{
                        //    LopView data = (LopView)hitInfo.Node.Tag; //Lấy object của node đang bấm

                        //    if (data.HocSinhCustomView != null)
                        //    {
                        //        if (data.LoaiLop == LoaiLopEnum.Truong)
                        //        {
                        //            bool Chon = Common.CauHinhChung_GetCauHinhChung.CauHinhHocSinh.KhongHienHocSinhKhiChonTruong;
                        //            if (Chon)
                        //                return;
                        //        }
                        //        data.HocSinhCustomView.LoadData(data.Oid);
                        //    }
                        //    if(data.QuanLyHocSinh_NghiHoc!=null)
                        //    {
                        //        data.QuanLyHocSinh_NghiHoc.LoadDanhSachBaoLuu_NghiHoc(data.Oid);//data.Oid - Lớp
                        //    }
                        //}

                        if (hitInfo.Node.Tag is BoPhanView)
                        {
                            BoPhanView data = (BoPhanView)hitInfo.Node.Tag; //Lấy object của node đang bấm
                            if (data != null)
                            {
                                if (data.LoaiBoPhan == LoaiBoPhanEnum.CongTy)
                                {
                                    bool Chon = Common.CauHinhChung_GetCauHinhChung.CauHinhHoSo.KhongHienNhanVienKhiChonCongTy;
                                    if (Chon)
                                        return;
                                }
                                data.NhanSuCustomView.LoadData(data.Oid);
                            }
                        }

                        if (hitInfo.Node.Tag is BoPhanTGView)
                        {
                            BoPhanTGView data = (BoPhanTGView)hitInfo.Node.Tag; //Lấy object của node đang bấm
                            data.ThinhGiangCustomView.LoadData(data.Oid);
                        }

                        #region Báo cáo - hưng
                        var a = hitInfo.Node.Tag.ToString();
                        if (hitInfo.Node.Tag is ReportViewGroup)
                        {
                            ReportViewGroup rpt = (ReportViewGroup)hitInfo.Node.Tag; //Lấy object của node đang bấm
                            ERP.Module.BaoCao.Custom.ReportData_Custom report = rpt.Session.FindObject<ERP.Module.BaoCao.Custom.ReportData_Custom>(CriteriaOperator.Parse("Oid =?", rpt.ID));
                            if (report != null)
                            {
                                using (DialogUtil.Wait("Đang lấy thông tin báo cáo","Vui lòng đợi...."))
                                {
                                    rpt.ReportCustomView.HinhAnh = report.HinhAnh;
                                    rpt.ReportCustomView.TenBaoCao = report.ReportName;
                                    rpt.ReportCustomView.OidReport = report.Oid;
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("Vui lòng chọn báo cáo", "Thông báo!");
                                rpt.ReportCustomView.TenBaoCao = null;
                                rpt.ReportCustomView.HinhAnh = null;
                                return;
                            }
                        }
                        #endregion
                    }
                }
                View.Refresh();
            }
            #endregion
        }

        void gridView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.ColumnType.FullName == "System.Boolean") //Nếu kiểu cột là bool
            {
                object value = e.CellValue; //Lấy giá trị hiện tại của cột
                if (value is bool)
                {
                    _gridView.SetRowCellValue(e.RowHandle, e.Column, !((bool)value)); //Gắn giá trị ngược lại cho cột
                    View.Refresh();//Làm mới view để đổi giá trị 
                }
            }

            if (_listView.Id == "NhanSuCustomView_NhanSuList_ListView"
              || _listView.Id == "ThinhGiangCustomView_ThinhGiangList_ListView")
            {
                if (View.SelectedObjects.Count > 0)
                {
                    if (View.SelectedObjects[0] is NhanSuView)
                    {
                        NhanSuView nsView = View.SelectedObjects[0] as NhanSuView;
                        nsView.NhanSuCustomView.Oid = nsView.Oid;
                    }
                    
                    if (View.SelectedObjects[0] is ThinhGiangView)
                    {
                        ThinhGiangView tgView = View.SelectedObjects[0] as ThinhGiangView;
                        tgView.ThinhGiangCustomView.Oid = tgView.Oid;
                    }

                    View.Refresh();
                }
            }
        }
    }
}
