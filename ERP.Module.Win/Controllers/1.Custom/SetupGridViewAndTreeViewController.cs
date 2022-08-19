using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraTreeList;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.Utils;
using System.Drawing;
using System.Drawing.Drawing2D;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.Enum.Systems;
using ERP.Module.HeThong;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using DevExpress.ExpressApp.SystemModule;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class SetupGridViewAndTreeViewController : ViewController<ListView>
    {
        private ListView _listView;

        public SetupGridViewAndTreeViewController()
        {
            InitializeComponent();
            //
            RegisterActions(components);
        }

        private void SetupGridViewAndTreeViewController_Activated(object sender, EventArgs e)
        {
            //Cài đặt lưới ở đây
            View.ControlsCreated += View_ControlsCreated;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            //Lấy listview
            _listView = View as ListView;

            if (_listView != null)
            {
                if (_listView.Editor is GridListEditor)//Nếu là lưới
                {
                    //Ép sang kiểu lưới 
                    GridView gridView = (_listView.Editor as GridListEditor).GridView;
                    if (gridView != null)
                    {
                        //Cài đặt lưới theo ý người dùng
                        CustomGridView(gridView);
                    }
                }
                else if (!_listView.Id.Contains("NhanSu") && _listView.Editor is TreeListEditor)//Nếu là cây
                {
                    //Ép sang kiểu cây
                    TreeList tree = (_listView.Editor as TreeListEditor).TreeList;
                    if (tree != null)
                    {
                        //Cài đặt cây theo ý người dùng
                        CustomTreeList(tree);
                    }
                }
            }
        }

        #region GridView

        private void CustomGridView(GridView gridView)
        {
            if (gridView != null)
            {
                ////Cài đặt thông tin cơ bản của lưới
                GridUtil.InitGridView(gridView);

                {// Tùy chỉnh thông tin theo đối tượng

                    //Hiển thị summaries trên đối tượng
                    SetupSummariesOfObject(gridView);

                    //Hiển thị group summaries trên đối tượng
                    //SetupGroupSummariesOfObject(gridView);

                    //Không cho tự động giản cột 
                    DisableColumnAutoWidthOfObject(gridView);

                    //Ẩn đi các cột của lưới
                    //VisibleColumnsOfObject(gridView);

                    //Hiện dấu check all lên lưới
                    SetCheckAllBoxToBooleanGridColumn(gridView);

                    //Căn chỉnh lại dữ liệu 
                    SetupHorzAlignmentGridView(gridView);

                    //Sort gridview
                    SortGridView(gridView);

                    //Không cho chọn nhiều dòng trên lưới
                    SetupMultiSelectGridView(gridView);

                    //Nhóm lưới theo cột
                    GroupingByColumnOfGridView(gridView);

                    //Lưới nhiều dòng
                    SetupShowMultilineGridCell(gridView);

                    //Lưới nhiều dòng
                    SetupShowImageGridCell(gridView);

                    //Không cho mở form dạng detailview
                    VisibleShowDetailView(gridView);

                    //Master - Detail view
                    //ShowMasterDetailView(gridView);

                    //Không cho Export
                    VisibleExport(gridView);

                    //Merge dòng trong GridView
                    SetupCellMerge(gridView);

                    SetupFixColumnGridView(gridView);

                    //Canh text giữa cho Banded Grid Views
                    SetupBandedGridViews(gridView);

                    //Ẩn export file XLSX
                    Visiable_XLSX_EXPORT(gridView);

                    //ShowAutoFilterRow
                    ShowAutoFilterRow(gridView);

                }
            }
        }
        
        private void ShowAutoFilterRow(GridView gridView)
        {
            gridView.OptionsView.ShowAutoFilterRow = true;
        }

        private void SortGridView(GridView gridView)
        {
            if (_listView.Id == "BangChotThongTinTinhLuong_ListThongTinTinhLuong_ListView")
            {
                //
                //Xóa tất cả sort cũ
                gridView.ClearSorting();          
                //Sort theo cột STT
                gridView.Columns["STT"].OptionsColumn.AllowSort = DefaultBoolean.True;

                gridView.Columns["NgayVaoCoQuan"].OptionsColumn.AllowSort = DefaultBoolean.True;

                //Sắp xếp tăng dần
                gridView.Columns["STT"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView.Columns["NgayVaoCoQuan"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            }

            if (_listView.Id == "BangChotThuLao_ListThongTinBangChot_ListView")
            {
                //
                //Xóa tất cả sort cũ
                gridView.ClearSorting();
                //Sort theo cột Họ tên
                gridView.Columns["NhanVien.HoTen"].OptionsColumn.AllowSort = DefaultBoolean.True;                
                //Sắp xếp tăng dần
                gridView.Columns["NhanVien.HoTen"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;              

            }

        }

        private void SetupSummariesOfObject(GridView gridView)
        {
            if (_listView.Id.Contains("NhanVienDangLamViec_ListView")
                || _listView.Id.Contains("NhanVienDaNghiViec_ListView")
                || _listView.Id.Contains("GiangVienThinhGiang_ListView"))
            {
                //Hiển thị cột tổng số dòng ở footer
                GridUtil.ShowSummaries(gridView, true, false, false, false, false, "Ten");
            }

            else if (_listView.Id.Contains("HocSinhDangLamViec_ListView")
                || _listView.Id.Contains("HocSinhDaNghiViec_ListView"))
            {
                //Hiển thị cột tổng số dòng ở footer
                GridUtil.ShowSummaries(gridView, true, false, false, false, false, "Ten");
            }

            else if (_listView.Id.Contains("ThuPhi_ListChiTietThuPhi_ListView"))
            {
                //Hiển thị cột tổng số dòng ở footer
                GridUtil.ShowSummaries(gridView, false, false, true, false, false, "SoTienThu");
            }

            else if (_listView.Id.Contains("ThuPhiLanDau_ListChiTietThuPhiLanDau_ListView"))
            {
                //Hiển thị cột tổng số dòng ở footer
                GridUtil.ShowSummaries(gridView, false, false, true, false, false, "SoTienThu");
            }

        }

        private void SetupGroupSummariesOfObject(GridView gridView)
        {
            if (_listView.Id.Contains("Bep_DonDatHangDuTru_ListBep_DonDatHangDuTruTong_ListView"))
            {
                //Hiển thị cột tổng số dòng ở GroupRow
                GridUtil.ShowGroupSummaries(gridView, false, false, true, false, false, "ThanhTien");
                GridUtil.ShowGroupSummaries(gridView, true, false, false, false, false, "ThucPham");
                gridView.Columns["NgayApDung"].Group();
            }
        }

        //private void VisibleColumnsOfObject(GridView gridView)
        //{
        //    //Chú ý: Trưởng hợp này chỉ dùng cho listview dạng lưới còn detailview phải phân quyền trên property

        //    if (Config.CompanyKey.Equals("IUH"))
        //    {
        //        if (_listView.Id == "DanhSachDenHanNangLuong_DanhSachNhanVien_ListView")
        //        {
        //            //
        //            GridUtil.InvisibleColumn(gridView, new string[] { "SoHieuCongChuc" });
        //        }
        //    }
        //}
        
        private void DisableColumnAutoWidthOfObject(GridView gridView)
        {
            //Lưu ý muốn sửa gì phải hỏi lại nhóm
            if (
                //Kiểm tra phân hệ
                _listView.ObjectTypeInfo.ToString().Contains("HocPhi")
                || (_listView.ObjectTypeInfo.ToString().Contains("HocSinh") && !_listView.ObjectTypeInfo.ToString().Contains("NguoiDuaDon"))
                //Kiểm tra listview
                //1. Phận hệ nhân sự
                || _listView.Id.Equals("TrichDanhSachNhanVien_ListChiTietTrichDanhSachNhanVien_ListView")
                || _listView.Id.Equals("TimKiemNhanVien_ListChiTietTimKiemNhanVien_ListView")
                || _listView.Id.Equals("TrichDanhSachThinhGiang_ListChiTietTrichDanhSachThinhGiang_ListView")
                || _listView.Id.Equals("TimKiemThinhGiang_ListChiTietTimKiemThinhGiang_ListView")
                || _listView.Id.Equals("NhanSuCustomView_NhanSuList_ListView")
                || _listView.Id.Equals("ThinhGiangCustomView_ThinhGiangList_ListView")
                //2. Phận hệ tài sản
                || (_listView.Id.Contains("TaiSan") && _listView.Id.Contains("ListView"))
                //3. Phận hệ công cụ dụng cụ
                || (_listView.Id.Contains("CongCuDungCu") && _listView.Id.Contains("ListView"))
                //4. Phân hệ bếp
                //|| _listView.ObjectTypeInfo.ToString().Contains("BepAn")
                || _listView.Id.Contains("ThucPham")
                || _listView.Id.Contains("Bep_DeNghiMuaHangChiTiet")
                || _listView.Id.Contains("Bep_DeNghiMuaHangTong")
                || _listView.Id.Contains("Bep_DeNghiXuatKhoChiTiet")
                || _listView.Id.Contains("Bep_DeNghiXuatKhoTong")
                || _listView.Id.Contains("Bep_DonDatHangDuTruChiTiet")
                || _listView.Id.Contains("Bep_DonDatHangDuTruTong")
                || _listView.Id.Contains("ThucDonTuan")
                || _listView.Id.Contains("HoSoTinhLuong_ListChiTietLuong_Old_ListView")
                || _listView.Id.Contains("HoSoTinhLuong_ListChiTietLuong_ListView")
                || _listView.Id.Contains("NhatKyCTKhungWeb_ListView")
                || _listView.Id.Contains("NhatKyCTKhungSua_ListView")
                || _listView.Id.Contains("QuanLyThongTinBienLaiPushSAP_ChiTietList_ListView")
                || _listView.Id.Contains("QuanLyThongTinHoaDonPushSAP_ChiTietList_ListView")
                || _listView.Id.Contains("QuanLyThongTinHoaDonPushSAP_HoaDonList_ListView")
                || _listView.Id.Contains("QuanLyThongTinHoaDonDienTuSAP_HoaDonList_ListView")
                || _listView.Id.Contains("QuanLyThongTinHoaDonDienTuSAP_ChiTietList_ListView")
                || _listView.Id.Contains("KhoiLuongGiangDay_ListChiTiet_ListView")//Hương sửa 
                || _listView.Id.Contains("KhoiLuongGiangDay_ThinhGiang_ListChiTiet_ListView")//Hương sửa 
                || _listView.Id.Contains("KhoiLuongGiangDay_ListChiTietKhoaLuan_DoAn_ChuyenDe_ListView")//Hương sửa
                || _listView.Id.Contains("KhoiLuongGiangDay_ThinhGiang_ListChiTietKhoiLuongGiangDay_ThinhGiang_ListView")//Hương sửa 
                || _listView.Id.Contains("KhoiLuongGiangDay_ThinhGiang_ListChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang_ListView")//Hương sửa
                || _listView.Id.Contains("QuanLyCheDoXaHoi_ListChiTietCheDoXH_ListView")//Hương sửa
                || _listView.Id.Contains("QuanLyNCKH_ListChiTietNCKH_ListView")//Hương sửa
                || _listView.Id.Contains("QuanLyNCKH_ListChiTietNCKH_ListView")//Hương sửa
                || _listView.Id.Contains("QuanLyHDGiangDayKhac_ListChiTietHoatDongGiangDayKhac_ListView")//Hương sửa
                || _listView.Id.Contains("QuanLyHDKhac_ListChiTietHDKhac_ListView")//Hương sửa
                || _listView.Id.Contains("QuanLyKhaoThi_ListChiTietCoiThi_ListView")//Hương sửa
                || _listView.Id.Contains("QuanLyKhaoThi_ListChiTietRaDe_ListView")//Hương sửa
                || _listView.Id.Contains("QuanLyKhaoThi_ListChiTietChamBai_ListView")//Hương sửa   
                || _listView.Id.Contains("BangChotThuLao_ListThongTinBangChot_ListView")//Hương sửa     
                || _listView.Id.Contains("BangChotThuLao_ThinhGiang_ListThongTinBangChot_ListView")//Hương sửa   
                || _listView.Id.Contains("QuanLyCongTacPhi_ListChiTietCongTacPhi_ListView")//Hương sửa 
                || _listView.Id.Contains("QuanLyNVThinhGiang_ThanhToanThuLao_ListdsQuanLyNVThinhGiang_ThanhToanThuLao_ListView")//Hương sửa
                || _listView.Id.Contains("BangChotThuLao_ListThongTinBangChot_ListView")//Hương sửa     
            )
            {
                gridView.OptionsView.ColumnAutoWidth = false;
                //
                gridView.BestFitColumns();
                gridView.BestFitMaxRowCount = -1;
            }
            else
            {
                gridView.OptionsView.ColumnAutoWidth = true;
            }
        }

        private void SetCheckAllBoxToBooleanGridColumn(GridView gridView)
        {
            //Tạo ô cho chọn tất cả
            if (_listView.Id.Equals("ExportReport_ReportList_ListView")
                || _listView.Id.Equals("HoSoLuong_CopyCongThucLuong_CongThucTinhLuongList_ListView")
                || _listView.Id.Equals("Luong_ChonCongThucLuong_CongThucTinhLuongList_ListView")
                || _listView.Id.Equals("PhieuNhap_ChonDonHangMua_ListDonHang_ListView")
                || _listView.Id.Equals("DonHang_ChonDonDatHang_ListDonHang_ListView")
                || _listView.Id.Equals("TKB_DoTKB_TuanList_ListView")
                || _listView.Id.Equals("PhieuXuat_ChonDeNghiXuatKho_ListDeNghi_ListView")
                || _listView.Id.Equals("Lop_ChonLop_LopList_ListView")
                || _listView.Id.Equals("HS_ChonHS_ListHS_ListView")
                || _listView.Id.Equals("BangCongNo_InCongNo_ListInCongNoChiTiet_ListView")
                || _listView.Id.Equals("BienBanThanhLy_ChonDeNghiThanhLy_ListDeNghiThanhLy_ListView")
                || _listView.Id.Equals("ThucDon_TimKiemThucDonKhung_ThucDonKhungList_ListView")
                || _listView.Id.Equals("ThucDon_TimKiemThucDonTuan_ThucDonTuanList_ListView")
                || _listView.Id.Equals("KhoBep_DeNghiXuatNhapKho_KhoBep_DeNghiXuatNhapKhoChiTietList_ListView")
                || _listView.Id.Equals("KeHoachTuan_TimParentDoParentAskKhoiDoLop_ParentDoParentAskKhoiList_ListView")
                || _listView.Id.Equals("KeHoachTuan_TimParentDoParentAskKhoiDuyet_ParentDoParentAskKhoiList_ListView")
                || _listView.Id.Equals("KeHoachTuan_TimParentDoParentAskKhoiDuyet1_ParentDoParentAskKhoiList_ListView")
                || _listView.Id.Equals("KeHoachTuan_TimParentDoParentAskKhoiHoanThanh_ParentDoParentAskKhoiList_ListView")
                || _listView.Id.Equals("ChonChucNangPhu_ChucNangList_ListView")
                || _listView.Id.Equals("HuyCongNo_TimKiem_ListHuyCongNoChiTiet_ListView")//HERE
                || _listView.Id.Equals("HuyCongNo_TimKiem_List_HuyCongNo_ChiTietCongNo_ListView")//HERE
                || _listView.Id.Equals("CTKhung_CopyABI_DanhSachABI_ListView")//HERE
                || _listView.Id.Equals("CTKhung_CopyABIQuaNam_DanhSachABI_ListView")//HERE
                || _listView.Id.Equals("DangKyNgoaiKhoa_Ngung_GiaHanNgoaiKhoa_listDanhSachHocSinhNgoaiKhoa_ListView")//HERE - Hưng
                || _listView.Id.Equals("DangKyNgoaiKhoa_DangKyHangLoat_listDanhSachHocSinh_ListView")//HERE - Hưng
                || _listView.Id.Equals("QuanLyDichVuBus_ListChiTietDangKyBus_ListView")//HERE - Hưng
                || _listView.Id.Equals("DangKyBus_DangKyHangLoat_listDanhSachHocSinh_ListView")//HERE - Hưng
                || _listView.Id.Equals("Report_InPhieuNoiBo_HangLoat_listDanhSachPhieuThu_PhieuChi_ListView")//HERE - Hưng

                || _listView.Id.Equals("ChiTietTheoDoiTre_TimKiem_Web_MamNon_ChiTietTheoDoiTreList_ListView")//HERE - Hưng
                || _listView.Id.Equals("Non_DuThuHocPhi_Web_listChiTietDuThu_ListView")// Dự thu học phí HERE - Hưng
                || _listView.Id.Equals("HocPhi_DuThuPhi_HoSoNhapHoc_listHocSinh_ListView")// Dự thu học phí HSNH - Hưng
                || _listView.Id.Equals("CongNoMax_HuyChiTiet_listChiTietCongNo_HuyCongNoHangLoat_ListView")// Hủy hàng loạt - Hưng
                || _listView.Id.Equals("Push_ERP_SAP_listBienLai_ListView")//SAB_ERP- Hưng
                || _listView.Id.Equals("Push_ERP_SAP_listChiTietBienLai_ListView")//SAB_ERP- Hưng
                || _listView.Id.Equals("CreateCostCenterSAP_ListView")
                || _listView.Id.Equals("QuanLyThongTinHocSinhPushSAP_ChiTietList_ListView")
                || _listView.Id.Equals("QuanLyThongTinBienLaiPushSAP_BienLaiList_ListView")
                || _listView.Id.Equals("QuanLyChonDanhSachHangLoat_ListChinhSach_ListView")
                || _listView.Id.Equals("QuanLyChonDanhSachHangLoat_ListHocSinh_ListView")

                || _listView.Id.Equals("ChonNhanVien_listNhanVien_ListView")//Hương : PMS 
                || _listView.Id.Equals("QuanLyNVThinhGiang_ThanhToanThuLao_listBangChot_ListView")//Hương :
                 || _listView.Id.Equals("NonDataLoadChotCongNo_ListHocSinh_ListView")//Chốt công nợ
                 || _listView.Id.Equals("NonDataLoadChotCongNo_ListInCongNoChiTiet_ListView")//Chốt công nợ
                 || _listView.Id.Equals("NonDataLoadChotCongNo_TheoQuy_ListHocSinh_ListView")//Chốt công nợ
                 || _listView.Id.Equals("NonDataLoadChotCongNo_TheoQuy_ListInCongNoChiTiet_ListView")//Chốt công nợ
                )
            {
                //
                GridUtil.BooleanCheckAllBox.SetCheckAllBoxToBooleanGridColumn(gridView, gridView.Columns["Chon"], DevExpress.Utils.HorzAlignment.Near);
            }

            if (_listView.Id.Equals("HoSoTinhLuong_ListChiTietLuong_ListView")
                || _listView.Id.Equals("HoSoTinhLuong_ListChiTietLuong_Old_ListView"))
            {
                //
                GridUtil.BooleanCheckAllBox.SetCheckAllBoxToBooleanGridColumn(gridView, gridView.Columns["TinhLuong"], DevExpress.Utils.HorzAlignment.Near);
            }
        }

        private void SetupHorzAlignmentGridView(GridView gridView)
        {
            if (_listView.Id == "CongThucTinhLuong_ListView" ||
                 _listView.Id == "CongThucTinhLuong_ListChiTietCongThucTinhLuong_ListView"
               )
            {
                GridUtil.SetupHorzAlignmentGridView(gridView);
            }
        }

        private void SetupMultiSelectGridView(GridView gridView)
        {
            if (_listView.Id.Contains("ChungTu_ListView"))
            {
                GridUtil.SetupMultiSelectGridView(gridView, false);
            }
        }

        private void GroupingByColumnOfGridView(GridView gridView)
        {

            if (_listView.Id.Equals("QuanLyHopDong_ListHopDong_ListView"))
            {
                GridUtil.GroupingByColumn(gridView, new string[] { "LoaiHopDong!" });
            }
            if (_listView.Id.Equals("BacLuong_ListView"))
            {
                GridUtil.GroupingByColumn(gridView, new string[] { "NgachLuong!" });
            }
        }

        private void SetupShowImageGridCell(GridView gridView)
        {
            if (_listView.Id.Contains("HS_TimKiemNDD_chiTietList_ListView"))
            {
                GridUtil.ShowImageGridCell(gridView, new string[] { "HinhAnh" }, 50);
            }
        }

        private void SetupShowMultilineGridCell(GridView gridView)
        {
            if (_listView.Id.Contains("MonAn_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "TenMonAn" });
            }
            else if(_listView.Id.Contains("MonAn_LookupListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "TenMonAn" });
            }

            else if (_listView.Id.Contains("SuatAn_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "NoiDungSuatAn" });
            }
            else if (_listView.Id.Contains("SuatAn_LookupListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "NoiDungSuatAn" });
            }
            //else if (_listView.Id.Contains("SuatAn_ListSuatAn_MonAn_ListView"))
            //{
            //    GridUtil.ShowMultilineGridCell(gridView, new string[] { "MonAn" });
            //}

            else if (_listView.Id.Contains("ThucDonNgay_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "NoiDungThucDon" });
            }
            else if (_listView.Id.Contains("ThucDonNgay_LookupListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "NoiDungThucDon" });
            }
            else if (_listView.Id.Contains("ThucDonNgay_ListThucDonNgay_MonAn_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "TenMonAn" });
            }
            else if (_listView.Id.Contains("ThucDonNgay_ListThucDonNgay_SuatAn_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "SuatAn" });
            }

            else if (_listView.Id.Contains("ThucDonKhung_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "NoiDungThucDon" });
            }
            else if (_listView.Id.Contains("ThucDonKhung_LookupListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "NoiDungThucDon" });
            }

            else if (_listView.Id.Contains("ThucDon_TimKiemThucDonTuan_ThucDonTuanList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "ThucDonNgayT2.NoiDungThucDon", "ThucDonNgayT3.NoiDungThucDon", "ThucDonNgayT4.NoiDungThucDon", "ThucDonNgayT5.NoiDungThucDon", "ThucDonNgayT6.NoiDungThucDon", "ThucDonNgayT7.NoiDungThucDon", "ThucDonNgayCN.NoiDungThucDon" });
            }
            else if (_listView.Id.Contains("ThucDon_TimKiemThucDonKhung_ThucDonKhungList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "NoiDungThucDon" });
            }
            else if (_listView.Id.Contains("KhoBep_DeNghiXuatNhapKho_KhoBep_DeNghiXuatNhapKhoChiTietList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "ThucDonKhung_SuatAn!" });
            }

            else if (_listView.Id.Contains("DanhGiaKhauVi_HocSinh_ChiTietDanhGiaDinhDuongList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "ThucDonKhung_SuatAn!" });
            }
            else if (_listView.Id.Contains("DanhGiaKhauVi_CongTy_DanhGiaKhauViCongTyList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "ThucDonKhung_SuatAn!" });
            }

            else if (_listView.Id.Contains("ChiTietTheoDoiTre_TimKiem_Web_MamNon_ChiTietTheoDoiTreList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "GhiChu!" });
            }

            else if (_listView.Id.Contains("TinNhan_TimWeb_MamNon_TinNhan_ListWeb_MamNon_TinNhan_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "NoiDung" });
            }

            else if (_listView.Id.Contains("QuanLyCTKhung_CTKhungList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "HoatDong.TenHoatDong" });
            }

            else if (_listView.Id.Contains("CTKhung_ChinhSua_CTKhungNDCSList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, null, new string[] { "Hide" });
            }

            else if (_listView.Id.Contains("QuanLyCTTiengAnh_CTTiengAnhList_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, null, new string[] { "Objective.TenObjective" });
            }
        }

        private void VisibleShowDetailView(GridView gridView)
        {
            //Những trường hợp đã chỉnh cho edit trên listview thì sẽ không cho mở form detail
            //Có thể thêm những trường hợp ngoại lệ bằng Id của ListView
            if (
                (_listView.Id.Contains("QuanLyThoiKhoaBieu_ThoiKhoaBieuList_ListView"))
                || (_listView.Id.Contains("SecuritySystemUser_Roles_ListView"))
                || (_listView.Id.Contains("NotificationsObject_Notifications_ListView"))
                || (_listView.Id.Contains("QuanLyDanhGiaTiengAnhHeESL_CTTiengAnhList_ListView"))
                || (_listView.AllowEdit.ResultValue == true)
                && !(_listView.Id.Contains("QuanLyCTKhung_CTKhungList_ListView"))
                && !(_listView.Id.Contains("QuanLyCTKhung_CTKhung_NDCSList_ListView"))
                && !(_listView.Id.Contains("CTKhung_CTKhung_NDCS_ListView"))
                && !(_listView.Id.Contains("ChuongTrinhGD_ChiTietChuongTrinhGDList_ListView"))
                && !(_listView.Id.Contains("ThucDon_TimKiemThucDonTuan_ThucDonTuanList_ListView"))
                && !(_listView.Id.Contains("DinhPhi_ListDinhPhiDongPhucHocPham_ListView"))
                && !(_listView.Id.Contains("DinhPhi_ListDinhPhiNgoaiKhoa_ListView"))
                && !(_listView.Id.Contains("DinhPhi_ListQuyDinhHoanPhi_ListView"))
                && !(_listView.Id.Contains("DoiTuongDinhDuong_ListNhuCauDinhDuong_ListView"))
                && !(_listView.Id.Contains("VacxinVitamin_ListView"))
                && !(_listView.Id.Equals("KeHoachTuan_TimParentDoParentAskKhoiDoLop_ParentDoParentAskKhoiList_ListView"))
                && !(_listView.Id.Equals("KeHoachTuan_TimParentDoParentAskKhoiDuyet_ParentDoParentAskKhoiList_ListView"))
                && !(_listView.Id.Equals("KeHoachTuan_TimParentDoParentAskKhoiDuyet1_ParentDoParentAskKhoiList_ListView"))
                && !(_listView.Id.Equals("KeHoachTuan_TimParentDoParentAskKhoiHoanThanh_ParentDoParentAskKhoiList_ListView"))
                && !(_listView.Id.Equals("ThucDon_TimKiemThucDonKhung_ThucDonKhungList_ListView"))
                && !(_listView.Id.Contains("QuanLyCTTiengAnh_CTTiengAnhList_ListView"))
                && !(_listView.Id.Contains("HocSinhChinhSachHopDongGoi_ListView"))
                && !(_listView.Id.Contains("HinhThucDong_ListView"))
                && !(_listView.Id.Contains("LoaiPhi_ListView"))
                && !(_listView.Id.Contains("QuanLyDichVuBus_ListChiTietDangKyBus_ListView"))//HERE - Hưng
                )
            {
                ListViewProcessCurrentObjectController processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
                processCurrentObjectController.ProcessCurrentObjectAction.Enabled["Visible"] = false;
            }
            else
            {
                ListViewProcessCurrentObjectController processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
                processCurrentObjectController.ProcessCurrentObjectAction.Enabled["Visible"] = true;
            }
        }

        private void VisibleExport(GridView gridView)
        {
            if (
                (_listView.Id.Contains("QuanLyCTKhung_CTKhung_NDCSList_ListView"))
                || (_listView.Id.Contains("QuanLyCTKhung_CTKhungList_ListView"))
                || (_listView.Id.Contains("CTKhung_NDCS_CTKhung_ListView"))
                || (_listView.Id.Contains("CTKhung_CTKhung_NDCS_ListView"))
                || (_listView.Id.Contains("ChuongTrinhGD_ChiTietChuongTrinhGDList_ListView"))
                || (_listView.Id.Contains("ChiTietChuongTrinhGD_ChiTietChuongTrinhGD_NDCS_ListView"))
                || (_listView.Id.Contains("DinhPhi_ListDinhPhiHocPhi_ListView"))
                || (_listView.Id.Contains("DinhPhi_ListDinhPhiNgoaiKhoa_ListView"))
                || (_listView.Id.Contains("DinhPhi_ListDinhPhiDongPhucHocPham_ListView"))
                || (_listView.Id.Contains("DinhPhi_ListDinhPhiNgoaiGio_ListView"))
                || (_listView.Id.Contains("DinhPhi_ListQuyDinhHoanPhi_ListView"))
                || (_listView.Id.Contains("DinhPhi_ListDinhPhiBUS_ListView"))
                || (_listView.Id.Contains("DinhPhi_ListDinhAppMobile_ListView"))
                || (_listView.Id.Contains("DinhPhi_ListBieuPhi_ListView"))
                )
            {
                //Tắt copy bằng ctr +C
                ListViewFocusedElementToClipboardController CopyCellValue = Frame.GetController<ListViewFocusedElementToClipboardController>();
                if (CopyCellValue != null)
                {
                    CopyCellValue.CopyCellValueAction.Active.SetItemValue("myReason", false);
                }

                //tắt export to
                //if (Common.TaiKhoanBinhThuong_NotEdu())
                //{
                    Frame.GetController<WinExportController>().Active["TruyCap"] = false;
                //}
                //else
                //{
                //    Frame.GetController<WinExportController>().Active["TruyCap"] = true;
                //}
            }
        }
        
        private void ShowMasterDetailView(GridView gridView)
        {
            if (_listView.Id == "ThuPhi_ListChiTietThuPhi_ListView")
            {
                gridView.OptionsPrint.PrintDetails = true;
                gridView.OptionsDetail.ShowDetailTabs = true;
                gridView.OptionsView.ShowDetailButtons = true;
                gridView.OptionsDetail.EnableMasterViewMode = true;
                gridView.MasterRowExpanded += view_MasterRowExpanded;
            }
        }

        private void view_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            GridView masterView = sender as GridView;
            GridView detailView = masterView.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detailView.BeginUpdate();
            detailView.OptionsDetail.EnableMasterViewMode = false;
            detailView.OptionsBehavior.Editable = true;
            detailView.OptionsBehavior.AutoExpandAllGroups = true;
            foreach (GridColumn col in detailView.Columns)
            {
                if (col.FieldName.Contains("!Key"))
                {
                    col.Visible = false;
                    col.OptionsColumn.ShowInCustomizationForm = false;
                }
            }
            detailView.EndUpdate();
        }

        private void SetupBandedGridViews(GridView gridView)
        {
          
            if ((gridView is BandedGridView))
            {
                BandedGridView bandedGriv = (BandedGridView)gridView;

                foreach (GridBand gridBand in bandedGriv.Bands)
                {
                    gridBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                }
            }
        }

        private void SetupCellMerge(GridView gridView)
        {
            if (_listView.Id == "DeNghiCungCapCanTin_ChiTietDeNghiCungCapCanTin_ListView"
                && _listView.Id != "CTKhung_ChinhSua_CTKhungList_ListView")
            {
                GridUtil.SetupCellMerge(gridView);
            }
        }


        private void SetupFixColumnGridView(GridView gridView)
        {
            if (_listView.Id == "BangChotThuLao_ListThongTinBangChot_ListView" || _listView.Id == "KhoiLuongGiangDay_ThinhGiang_ListChiTietKhoiLuongGiangDay_ThinhGiang_ListView"
                || _listView.Id == "KhoiLuongGiangDay_ThinhGiang_ListChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang_ListView"
                || _listView.Id == "KhoiLuongGiangDay_ListChiTiet_ListView" || _listView.Id == "KhoiLuongGiangDay_ListChiTietKhoaLuan_DoAn_ChuyenDe_ListView"
                
                )
            {
                //
                GridUtil.FixColumn(gridView, new string[] { "NhanVien!" });
                gridView.Columns["NhanVien!"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }           
        }


        private void Visiable_XLSX_EXPORT(GridView gridView)
        {
            foreach(var item in Frame.GetController<WinExportController>().ExportAction.Items)
            {
                if (item.Id == "XLSX File")
                {
                    Frame.GetController<WinExportController>().ExportAction.Items.Remove(item);
                    break;
                }
            }
        }

        private static void SetXlsOptions(XlsExportOptions xlsExportOptions)
        {
            // XLS-specific options:  
            xlsExportOptions.SheetName = "CustomXlsSheetTitle";
            xlsExportOptions.ShowGridLines = false;
        }
        #endregion

        #region TreeList
        private void CustomTreeList(TreeList tree)
        {
            if (tree != null)
            {
                #region 1. Setup basic
                //Cài đặt cây theo ý người dùng
                TreeUtil.InitTreeView(tree);

                //Sắp sếp theo cột của cây
                SortColumnOfTreeList(tree);

                //Ẩn đi các cột của cây
                //VisibleColumnsOfObject(tree);

                //Chỉnh lại chiều rộng của cây
                SetWidthOfColumn(tree);

                //xuống dòng cho các column
                ShowWordWrapColumnForTree(tree);

                #endregion

                #region 2. Event of tree
                //Chỉnh màu của cây
                //tree.CustomDrawNodeCell += treeList_CustomDrawNodeCell;
                tree.NodeCellStyle += treeList_NodeCellStyle;
                #endregion
            }
        }

        //chỉnh xuống hàng column cho cây
        private void ShowWordWrapColumnForTree(TreeList tree)
        {
            string[] files;
            switch (_listView.Id)
            {
                //cây danh mục tài sản
                case "DanhMucTaiSanCoDinh_ListView":
                case "DanhMucTaiSanCoDinh_DetailView":
                case "DanhMucTaiSanCoDinh_LookupListView":
                    files = new string[1] { "Ten" };
                    TreeUtil.ShowWordWrapColumn(tree, files);
                    break;
                //cây vị trí phòng ban
                case "ViTriPhongBan_ListView":
                case "ViTriPhongBan_DetailView":
                case "ViTriPhongBan_LookupListView":
                    files = new string[2] { "TenViTri", "DiaChiCuThe" };
                    TreeUtil.ShowWordWrapColumn(tree, files);
                    break;
                ///cây vị danh mục công cụ dụng cụ
                case "DanhMucCCDC_DetailView":
                case "DanhMucCCDC_ListView":
                case "DanhMucCCDC_LookupListView": 
                      files = new string[1] { "Ten"};
                    TreeUtil.ShowWordWrapColumn(tree, files);
                    break;
                default:
                    break;
            }
        }


        //private void VisibleColumnsOfObject(TreeList tree)
        //{
        //    //Chú ý: Trưởng hợp này chỉ dùng cho listview dạng cây còn detailview phải phân quyền trên property

        //    if (Config.CompanyKey.Equals(""))
        //    {
        //        if (_listView.Id == "BoPhan_ListView")
        //        {
        //            TreeUtil.InvisibleColumn(tree, new string[] { "TrinhDoChuyenMonCaoNhat" });
        //        }
        //    }
        //}

        private void SortColumnOfTreeList(TreeList tree)
        {
            if (_listView.Id.Contains("Department_ListView"))
            {
                //Sort theo số thứ tự
                TreeUtil.AllowSortColumn(tree, new string[] { "STT" }, true);
            }
            if (_listView.Id.Contains("AppMenu_ListView"))
            {
                //Sort theo số thứ tự
                TreeUtil.AllowSortColumn(tree, new string[] { "SoThuTu" }, true);
            }
            if (_listView.Id.Contains("HoSo_ListGiayToHoSo_ListView"))
            {
                //Sort theo số thứ tự
                TreeUtil.AllowSortColumn(tree, new string[] { "STT" }, true);
            }
        }

        private void SetWidthOfColumn(TreeList tree)
        {
            if (_listView.Id.Contains("Department_ListView"))
            {
                //
                TreeUtil.SetWidthOfColumn(tree, new string[] { "TenBoPhan" }, 300);
            }
        }

        private void treeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            // Create brushes for cells.
            Brush backBrush, foreBrush;
            if (e.Node != (sender as TreeList).FocusedNode)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.PapayaWhip, Color.PeachPuff, LinearGradientMode.ForwardDiagonal);
                foreBrush = Brushes.Black;
            }
            else
            {
                backBrush = Brushes.DarkBlue;
                foreBrush = new SolidBrush(Color.PeachPuff);
            }

            // Fill the background.
            e.Graphics.FillRectangle(backBrush, e.Bounds);

            // Paint the node value.
            e.Graphics.DrawString(e.CellText, e.Appearance.Font, foreBrush, e.Bounds, e.Appearance.GetStringFormat());

            // Prohibit default painting.
            e.Handled = true;
        }

        private void treeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            //Giấy tờ hồ sơ
            if (_listView.Id.Equals("HoSo_ListGiayToHoSo_ListView"))
            {
                GiayToHoSo file = ((GiayToHoSo)e.Node.Tag);
                //
                if (file != null && file.LoaiGiayTo == null)
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, 9, FontStyle.Bold);
                }
                else
                {
                    //
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }

           
            //Cây menu
            if (_listView.Id.Equals("AppMenu_ListView"))
            {
                //
                //if (e.Column.FieldName != "Budget") return;
                //
                AppMenu appMenu = ((AppMenu)e.Node.Tag);
                if (appMenu == null) return;
                //
                if (appMenu.AppObject == null)
                {
                    if (appMenu.PhanHe.Oid.ToString().Equals("00000000-0000-0000-0000-000000000001"))
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.SteelBlue;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else if (appMenu.PhanHe.Oid.ToString().Equals("00000000-0000-0000-0000-000000000002"))
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.LightPink;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else if (appMenu.PhanHe.Oid.ToString().Equals("00000000-0000-0000-0000-000000000003"))
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.SlateBlue;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else if (appMenu.PhanHe.Oid.ToString().Equals("00000000-0000-0000-0000-000000000006"))
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.SaddleBrown;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.LightSlateGray;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
                else
                {
                    if (appMenu.PhanHe.Oid.ToString().Equals("00000000-0000-0000-0000-000000000001"))
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.SteelBlue;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                    }
                    else if (appMenu.PhanHe.Oid.ToString().Equals("00000000-0000-0000-0000-000000000002"))
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.LightPink;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                    }
                    else if (appMenu.PhanHe.Oid.ToString().Equals("00000000-0000-0000-0000-000000000003"))
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.SlateBlue;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                    }
                    else if (appMenu.PhanHe.Oid.ToString().Equals("00000000-0000-0000-0000-000000000006"))
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.SaddleBrown;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                    }
                    else
                    {
                        //e.Appearance.BackColor = Color.Gray;
                        e.Appearance.ForeColor = Color.LightSlateGray;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                    }
                }
            }
        }
        #endregion


    }
}
