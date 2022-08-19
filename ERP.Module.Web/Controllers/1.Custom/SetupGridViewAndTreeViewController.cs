using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Utils;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using DevExpress.Web.Data;
using System.Collections.Generic;
using ERP.Module.Extends;
using ERP.Module.Commons;
using DevExpress.ExpressApp.SystemModule;
using ERP.Module.NghiepVu.TuyenSinh;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.DanhMuc.TuyenSinh_TP;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace ERP.Module.Web.Controllers.Custom
{
    public partial class SetupGridViewAndTreeViewController : ViewController<ListView>
    {
        private ListView _listView;
        private ASPxGridView _gridView;
        private XPObjectSpace _objectSpace;

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
            View.Refresh();
        }
        private void View_ControlsCreated(object sender, EventArgs e)
        {
            //Lấy listview
            _listView = View as ListView;
            DevExpress.ExpressApp.NonPersistentObjectSpace nonper = View.ObjectSpace as DevExpress.ExpressApp.NonPersistentObjectSpace;
            //_
            if (_listView != null && nonper == null
                && !_listView.Id.Equals("ContextValidationResult_ListView") //Không lấy cái này lỗi
                && !_listView.Id.Equals("SecuritySystemTypePermissionsObjectOwner_TypePermissionMatrix_ListView") //Không lấy cái này lỗi
                && _listView.Editor is ASPxGridListEditor)//Nếu là lưới
            {

                //Ép sang kiểu lưới 
                _gridView = (_listView.Editor as ASPxGridListEditor).Grid;
                if (_gridView != null)
                {
                    //Cài đặt lưới theo ý người dùng
                    CustomGridView();

                    //Custom các sự kiện của lưới
                    CustomEventOfGridView();
                }
            }
        }

        #region GridView
        private void CustomGridView()
        {
            #region Thông tin chung

            //////Cài đặt riêng
            if (_listView.Id.Equals(""))
            {
                ASPxGridUtil.InitGridViewCustom(_gridView);
            }
            else
            {
                ///Cài đặt chung
                ASPxGridUtil.InitGridView(_gridView);
            }
            #endregion

            #region Thông tin khác
            _objectSpace = (XPObjectSpace)View.ObjectSpace;
            if (_objectSpace == null)
                return;
            //
            CongTy congTy = Common.CongTy(_objectSpace.Session);
            //
            if (congTy != null)
            {
                if (congTy.Oid.Equals(Config.KeyTanPhu))
                {
                    //Thay đổi tên cột
                    RenameColumn_PhoThong();

                    //Ẩn cột của gridview
                    InvisibleColumn_PhoThong();


                }
                else
                {
                    //Thay đổi tên cột
                    RenameColumn_Mamnon();

                    //Ẩn cột của gridview
                    InvisibleColumn_Mamnon();

                    // Điều chỉnh đột dài cột
                    CustomWidthCell();
                }
            }
            #endregion
        }

        void InvisibleColumn_PhoThong()
        {
            if (_listView.Id.Equals("ChiTietKeHoachTuyenSinh_ListKhoi_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "DoTuoiTu", "DoTuoiDen", "Khoi.TenLop" });
            }
            if (_listView.Id.Equals("ThongBaoTuyenSinh_ListHocPhi_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "Khoi.TenLop", "LoaiPhi.TenLoaiPhi", "HinhThucDong.TenHinhThucDong" });
            }
            if (_listView.Id.Equals("ThongBaoTuyenSinh_ListUuDai_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "TENCHINHSACH", });
            }

            if (_listView.Id.Equals("ThongTinKhachHang_ListTre_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "TruongDaHoc" });
            }

            if (_listView.Id.Equals("YKienKhachHang_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "HoSoNhapHoc.Cation", "HocSinh.Caption", });
            }

            if (_listView.Id.Equals("ToChucSuKien_ListDanhSachHocSinh_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "HocSinh.Caption", "Lop.TenLop", });
            }

            if (_listView.Id.Equals("ThongBaoNhapHoc_ListChiTietThongBaoNhapHoc_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "HocSinh.Caption" });
            }

            //if (_listView.Id.Equals("HoSoXetTuyen_ListDTB_ListView"))
            //{
            //    if (Common.ID_KHOI_Filter != 0)
            //    {

            //        ASPxGridUtil.InvisibleAllColumn(_gridView);

            //        XPCollection<KetQuaCuoiNam> kq = new XPCollection<KetQuaCuoiNam>(_objectSpace.Session);
            //        kq.Criteria = CriteriaOperator.Parse("ID_KHOI_KEY = ?", Common.ID_KHOI_Filter);
            //        string[] caption = new string[] { };
            //        string strCap = "";
            //        foreach (var item in kq)
            //        {
            //            strCap = "DiemTrungBinh" + item.KhoiSIS;
            //            ASPxGridUtil.InShowColumn(_gridView, new string[] { strCap });
            //        }
            //        ASPxGridUtil.InShowColumn(_gridView, new string[] { "Mon.Cation" });
            //    }
            //    else
            //    {
            //        ASPxGridUtil.InShowAllColumn(_gridView);
            //        ASPxGridUtil.InvisibleColumn(_gridView, new string[] {"STT", "DiemTrungBinh_HK1", "DiemTrungBinh_HK2", "DiemTrungBinh_CaNam" });
            //    }
            //}
        }
        void InvisibleColumn_Mamnon()
        {
            if (_listView.Id.Equals("ChiTietKeHoachTuyenSinh_ListKhoi_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "TuTuoi", "DenTuoi", "KhoiSIS" });
            }
            if (_listView.Id.Equals("ChiTietKeHoachTuyenSinh_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "DenThang" });
            }
            if (_listView.Id.Equals("ThongBaoTuyenSinh_ListHocPhi_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "DanhMucLoaiPhi", "Cation", "DanhMucKhoi.TenKhoi", "DanhMucLoaiPhi.TenLoaiPhi" });
            }
            if (_listView.Id.Equals("KeHoachTuyenSinh_ListChiTietKeHoachTuyenSinh_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "DenThang" });
            }
            if (_listView.Id.Equals("ThongBaoTuyenSinh_ListUuDai_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "TENCHINHSACH" });
            }
            if (_listView.Id.Equals("ThongTinKhachHang_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "DaChamSocKhachHang", "DaCoHSNhapHoc" });
            }
            if (_listView.Id.Equals("ThongTinKhachHang_ListTre_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "KhoiSIS", "DanhMucTruong.TenTruong", "TruongSIS" });
            }
            if (_listView.Id.Equals("ChamSocKhachHang_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "HoSoXetTuyen!" });
            }
            if (_listView.Id.Equals("YKienKhachHang_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "HoSoXetTuyen!" });
            }
            if (_listView.Id.Equals("ToChucSuKien_ListDanhSachHocSinh_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "MAHOCSINH_SIS", "LOP_SIS", "HOTEN_SIS", });
            }

            if (_listView.Id.Equals("ThongBaoNhapHoc_ListChiTietThongBaoNhapHoc_ListView"))
            {
                ASPxGridUtil.InvisibleColumn(_gridView, new string[] { "HoSoXetTuyen!" });
            }
        }

        void RenameColumn_PhoThong()
        {
            if (_listView.Id.Equals("ThongTinKhachHang_ListView"))
            {
                //ASPxGridUtil.RenameColumn(_gridView,"HoTen","Họ tên mẹ");
            }
            if (_listView.Id.Equals("ThongBaoTuyenSinh_ListHocPhi_ListView"))
            {
                ASPxGridUtil.RenameColumn(_gridView, "Cation", "Loại phí");
            }
            if (_listView.Id.Equals("KeHoachTuyenSinh_ListChiTietKeHoachTuyenSinh_ListView"))
            {
                ASPxGridUtil.RenameColumn(_gridView, "Thang", "Từ tháng");
            }

        }
        void RenameColumn_Mamnon()
        {
            if (_listView.Id.Equals("ThongTinKhachHang_ListView"))
            {
                ASPxGridUtil.RenameColumn(_gridView, "HoTen", "Họ tên");
            }

            if (_listView.Id.Equals("KeHoachTuyenSinh_ListChiTietKeHoachTuyenSinh_ListView"))
            {
                ASPxGridUtil.RenameColumn(_gridView, "Thang", "Tháng");
            }

        }

        void CustomWidthCell()
        {
            if (_listView.Id.Equals("YKienKhachHang_ListView"))
            {
                ASPxGridUtil.CustomWithGridCell(_gridView, new string[] { "HoSoXetTuyen!", "HocSinh.Caption" }, 150);
            }
        }
        #endregion

        #region Event

        void CustomEventOfGridView()
        {
            //1. SelectionChanged
            _gridView.SelectionChanged += SelectionChanged;
            // 
        }

        void SelectionChanged(object sender, EventArgs e)
        {
            Common.OidCustomList = new List<Guid>();
            Common.OidCustomListArray = new List<Guid[]>();
            //
            #region 10. Tuyển sinh
            if (_listView.Id.Equals("KiemTraIQ_ListChiTietKiemTraIQ_ListView")
                || _listView.Id.Equals("TuVanTuyenSinh_TongHop_ListChiTietTuVanTuyenSinh_ListView")
                || _listView.Id.Equals("TuVanTuyenSinh_ListChiTietTuVanTuyenSinh_ListView")
                || _listView.Id.Equals("ThongBaoNhapHoc_TongHop_ListChiTietThongBaoNhapHoc_ListView")
                || _listView.Id.Equals("ThongBaoNhapHoc_ListChiTietThongBaoNhapHoc_ListView")
                || _listView.Id.Equals("KeHoachTuyenSinh_ListChiTietKeHoachTuyenSinh_ListView")
                || _listView.Id.Equals("DuyetThongBaoTuyenSinh_ListChiTietDuyetThongBaoTuyenSinh_ListView")
                || _listView.Id.Equals("ToChucSuKien_ListChiTietToChucSuKien_ListView")
                || _listView.Id.Equals("DuyetToChucSuKien_ListChiTietDuyetToChucSuKien_ListView")
                || _listView.Id.Equals("XetTuyen_DotXetTuyen_ListDanhSachXetTuyen_ListView")
                )
            {
                var selectedList = _gridView.GetSelectedFieldValues("Oid");
                //
                foreach (var item in selectedList)
                {
                    Guid oid = new Guid(item.ToString());

                    if (!Common.OidCustomList.Contains(oid))
                    {
                        Common.OidCustomList.Add(oid);
                    }
                }
            }

            if (_listView.Id.Equals("ChamSocKhachHang_TongHop_ListChiTietChamSoc_ListView")
              )
            {
                var selectedList = _gridView.GetSelectedFieldValues("Oid", "OidPhu");
                //
                foreach (object[] item in selectedList)
                {
                    var a = item[0];
                    var b = item[1];
                    Guid[] oid = new Guid[2];
                    oid[0] = new Guid(a.ToString());
                    oid[1] = new Guid(b.ToString());

                    if (!Common.OidCustomListArray.Contains(oid))
                    {
                        Common.OidCustomListArray.Add(oid);
                    }
                }
            }
            #endregion
        }
        #endregion

    }
}
