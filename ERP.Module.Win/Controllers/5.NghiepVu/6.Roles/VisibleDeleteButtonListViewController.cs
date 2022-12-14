using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.TienLuong.Luong;
using ERP.Module.NghiepVu.TienLuong.TruyLuong;
using ERP.Module.NghiepVu.TienLuong.KhauTru;
using ERP.Module.NghiepVu.TienLuong.ThuNhapKhac;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.NghiepVu.TienLuong.NgoaiGio;
using ERP.Module.HeThong;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.ExpressApp.Web.SystemModule;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.DaoTao;
//
namespace ERP.Module.Win.Controllers.Roles
{
    public partial class VisibleDeleteButtonListViewController : DeleteObjectsViewController
    {
        public VisibleDeleteButtonListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void UpdateActionState()
        {
            //
            DeleteAction.BeginUpdate();
            try
            {
                base.UpdateActionState();
                //
                if (View == null || (!View.Id.Contains("KyTinhLuong") 
                                        && !View.Id.Contains("CC_KyChamCong")
                                        && !View.Id.Contains("TiLeDanhGiaCongViec")
                                        && !View.Id.Contains("HoSoTinhLuong") 
                                        && !View.Id.Contains("BangLuongNhanVien") 
                                        && !View.Id.Contains("BangTruyLinh") 
                                        && !View.Id.Contains("BangTruyThu") 
                                        && !View.Id.Contains("BangKhauTruLuong") 
                                        && !View.Id.Contains("BangThuNhapKhac")
                                        && !View.Id.Contains("ChungTu")
                                        && !View.Id.Contains("CC_QuanLyChamCong")
                                        && !View.Id.Contains("CC_QuanLyCongNgoaiGio")
                                        && !View.Id.Contains("CC_QuanLyCongKhac")
                                        && !View.Id.Contains("BangLuongNgoaiGio")
                                        && !View.Id.Contains("DonDatHang")
                                        && !View.Id.Contains("DonHangMua")
                                        && !View.Id.Contains("DeNghiXuatKho")
                                        && !View.Id.Contains("PhieuNhap")
                                        && !View.Id.Contains("BangChotKho")
                                        && !View.Id.Contains("NhatKyCTKhung")
                                        && !View.Id.Contains("NhatKyCTKhungSua")
                                        && !View.Id.Contains("ThongBao")
                                        && !View.Id.Contains("CTKhung")
                                        && !View.Id.Contains("CTKhung_NDCS")
                                        && !View.Id.Contains("ViTriTuyenDung")
                                        && !View.Id.Contains("DangKyTuyenDung")
                                        && !View.Id.Contains("NhuCauTuyenDung")
                                        && !View.Id.Contains("UngVien")
                                        && !View.Id.Contains("BuocTuyenDung")
                                        && !View.Id.Contains("VongTuyenDung")
                                        && !View.Id.Contains("ChiTietTuyenDung")
                                        && !View.Id.Contains("ChiTietVongTuyenDung")
                                        && !View.Id.Contains("TrungTuyen")
                                        && !View.Id.Contains("ThucDonKhung")
                                        && !View.Id.Contains("DangKyDaoTao")
                                        && !View.Id.Contains("ChiTietDangKyDaoTao")
                                        && !View.Id.Contains("DuyetDangKyDaoTao")
                                        && !View.Id.Contains("QuyetDinhDaoTao")
                                        && !View.Id.Contains("DangKyDaoTao")
                                        && !View.Id.Contains("ChiTietPhanCongGiangDay")
                                        && !View.Id.Contains("BangPhanTichLuong")
                                        && !View.Id.Contains("CC_QuanLyCongGiangDay")))
                    return;

                bool enable = true;

                #region 1. Ph??n h??? nh??n s??? - Ti???n l????ng
                if (View.ObjectTypeInfo.FullName.Contains("TienLuong") || View.ObjectTypeInfo.FullName.Contains("HoSoLuong"))
                {
                    #region 1.1. K??? t??nh l????ng
                    if (View.ObjectTypeInfo.Name == "KyTinhLuong")
                    {
                        KyTinhLuong obj = View.CurrentObject as KyTinhLuong;
                        if (obj != null && obj.KhoaSo)
                            enable = false;
                        //
                        if (obj != null && enable)
                        {
                            //N???u b???ng l????ng ???? t??nh th?? kh??ng cho x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("KyTinhLuong.Oid=? ", obj.Oid);
                            BangLuongNhanVien bangLuong = View.ObjectSpace.FindObject<BangLuongNhanVien>(filter);
                            if (bangLuong != null)
                            {
                                obj.KhoaKyTinhLuong = true;
                                enable = false;
                            }
                            //
                            if (enable)
                            {
                                //N???u b???ng l????ng ???? t??nh th?? kh??ng cho x??a
                                filter = CriteriaOperator.Parse("KyTinhLuong.Oid=? ", obj.Oid);
                                BangLuongNgoaiGio bangLuongNG = View.ObjectSpace.FindObject<BangLuongNgoaiGio>(filter);
                                if (bangLuongNG != null)
                                {
                                    obj.KhoaKyTinhLuong = true;
                                    enable = false;
                                }
                            }

                            if (enable)
                            {
                                //N???u c?? t??? l??? ????nh gi?? cv th?? kh??ng cho x??a
                                filter = CriteriaOperator.Parse("KyTinhLuong.Oid=? ", obj.Oid);
                                TiLeDanhGiaCongViec tiLeDanhGiaCongViec = View.ObjectSpace.FindObject<TiLeDanhGiaCongViec>(filter);
                                if (tiLeDanhGiaCongViec != null)
                                {
                                    obj.KhoaKyTinhLuong = true;
                                    enable = false;
                                }
                            }
                        }
                    }
                    #endregion

                    #region 1.2. K??? ch???m c??ng
                    else if (View.ObjectTypeInfo.Name == "CC_KyChamCong")
                    {
                        CC_KyChamCong obj = View.CurrentObject as CC_KyChamCong;
                        if (obj != null && obj.KhoaSo)
                            enable = false;

                        if (obj != null && enable)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("KyChamCong=? ", obj.Oid);
                            CC_QuanLyChamCong quanLyChamCong = View.ObjectSpace.FindObject<CC_QuanLyChamCong>(filter);
                            CC_QuanLyCongNgoaiGio quanLyCongNgoaiGio = View.ObjectSpace.FindObject<CC_QuanLyCongNgoaiGio>(filter);
                            CC_QuanLyCongKhac quanLyChamCongKhac = View.ObjectSpace.FindObject<CC_QuanLyCongKhac>(filter);
                            if (quanLyChamCong != null)
                            {                                
                                enable = false;
                            }
                            else if (quanLyCongNgoaiGio != null && enable)
                            {                               
                                enable = false;
                            }
                            else if (quanLyChamCongKhac != null && enable)
                            {                                
                                enable = false;
                            }
                        }                       
                    }
                    #endregion

                    #region 1.3. Qu???n l?? ch???m c??ng
                    else if (View.ObjectTypeInfo.Name == "CC_QuanLyChamCong")
                    {
                        CC_QuanLyChamCong obj = View.CurrentObject as CC_QuanLyChamCong;
                        if (obj != null && obj.KyChamCong != null && (obj.KyChamCong.KhoaSo || obj.KhoaChamCong))
                            enable = false;
                    }
                    #endregion

                    #region 1.4. H??? s?? t??nh l????ng
                    else if (View.ObjectTypeInfo.Name == "HoSoTinhLuong")
                    {
                        HoSoTinhLuong obj = View.CurrentObject as HoSoTinhLuong;
                        if (obj != null && obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo)
                        {
                            enable = false;
                            obj.KhoaSo = true;
                        }
                        if (obj != null && enable && obj.KyTinhLuong != null)
                        {
                            //N???u b???ng l????ng v?? ph??? c???p ???? t??nh th?? kh??ng cho x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("KyTinhLuong.Oid=? ", obj.KyTinhLuong.Oid);
                            BangLuongNhanVien bangLuong = View.ObjectSpace.FindObject<BangLuongNhanVien>(filter);
                            if (bangLuong != null)
                            {
                                obj.KhoaSo = true;
                                enable = false;
                            }

                            //N???u b???ng l????ng ngo??i gi??? ???? t??nh th?? kh??ng cho x??a                      
                            BangLuongNgoaiGio bangLuongNgoaiGio = View.ObjectSpace.FindObject<BangLuongNgoaiGio>(filter);
                            if (bangLuongNgoaiGio != null)
                            {
                                obj.KhoaSo = true;
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 1.5. B???ng l????ng v?? ph??? c???p
                    else if (View.ObjectTypeInfo.Name == "BangLuongNhanVien")
                    {
                        BangLuongNhanVien obj = View.CurrentObject as BangLuongNhanVien;
                        if (obj != null && obj.KyTinhLuong != null && (obj.KyTinhLuong.KhoaSo || obj.ChungTu != null))
                            enable = false;
                    }
                    #endregion

                    #region 1.6. B???ng l????ng truy l??nh
                    else if (View.ObjectTypeInfo.Name == "BangTruyLinh")
                    {
                        BangTruyLinh obj = View.CurrentObject as BangTruyLinh;
                        if (obj != null && obj.KyTinhLuong != null && (obj.KyTinhLuong.KhoaSo || obj.ChungTu != null))
                            enable = false;
                    }
                    #endregion

                    #region 1.7. B???ng l????ng truy thu
                    else if (View.ObjectTypeInfo.Name == "BangTruyThu")
                    {
                        BangTruyThu obj = View.CurrentObject as BangTruyThu;
                        if (obj != null && obj.KyTinhLuong != null && (obj.KyTinhLuong.KhoaSo || obj.ChungTu != null))
                            enable = false;
                    }
                    #endregion

                    #region 1.8. B???ng l????ng kh???u tr??? l????ng
                    else if (View.ObjectTypeInfo.Name == "BangKhauTruLuong")
                    {
                        BangKhauTruLuong obj = View.CurrentObject as BangKhauTruLuong;
                        if (obj != null && obj.KyTinhLuong != null && (obj.KyTinhLuong.KhoaSo || obj.ChungTu != null))
                            enable = false;
                    }
                    #endregion

                    #region 1.9. B???ng l????ng thu nh???p kh??c
                    else if (View.ObjectTypeInfo.Name == "BangThuNhapKhac")
                    {
                        BangThuNhapKhac obj = View.CurrentObject as BangThuNhapKhac;
                        if (obj != null && obj.KyTinhLuong != null && (obj.KyTinhLuong.KhoaSo || obj.ChungTu != null))
                            enable = false;
                    }
                    #endregion

                    #region 1.10. Ch???ng t???
                    else if (View.ObjectTypeInfo.Name == "ChungTu")
                    {
                        ChungTu obj = View.CurrentObject as ChungTu;
                        if (obj != null && obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo)
                            enable = false;
                    }
                    #endregion

                    #region 1.11. Qu???n l?? c??ng ngo??i gi???
                    else if (View.ObjectTypeInfo.Name == "CC_QuanLyCongNgoaiGio")
                    {
                        CC_QuanLyCongNgoaiGio obj = View.CurrentObject as CC_QuanLyCongNgoaiGio;
                        if (obj != null && obj.KyChamCong != null && (obj.KyChamCong.KhoaSo || obj.KhoaChamCong))
                            enable = false;
                    }
                    #endregion

                    #region 1.12. B???ng l????ng ngo??i gi???
                    else if (View.ObjectTypeInfo.Name == "BangLuongNgoaiGio")
                    {
                        BangLuongNgoaiGio obj = View.CurrentObject as BangLuongNgoaiGio;
                        if (obj != null && obj.KyTinhLuong != null && (obj.KyTinhLuong.KhoaSo || obj.ChungTu != null))
                            enable = false;
                    }
                    #endregion

                    #region 1.13. T??? l??? ????nh gi?? c??ng vi???c
                    else if (View.ObjectTypeInfo.Name == "TiLeDanhGiaCongViec")
                    {
                        TiLeDanhGiaCongViec obj = View.CurrentObject as TiLeDanhGiaCongViec;
                        if (obj != null && obj.KyTinhLuong != null && obj.KyTinhLuong.KhoaSo)
                            enable = false;
                    }
                    #endregion

                    #region 1.14. Qu???n l?? c??ng kh??c
                    else if (View.ObjectTypeInfo.Name == "CC_QuanLyCongKhac")
                    {
                        CC_QuanLyCongKhac obj = View.CurrentObject as CC_QuanLyCongKhac;
                        if (obj != null && obj.KyChamCong != null)
                        {
                            if (obj.KyChamCong.KhoaSo)
                                enable = false;
                            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyCongKhac=? ", obj.KyChamCong.Oid);
                            KyTinhLuong kyTinhLuong = View.ObjectSpace.FindObject<KyTinhLuong>(filter);
                            if (kyTinhLuong != null)
                            {
                                obj.KhoaChamCong = true;
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 1.15. B???ng ph??n t??ch l????ng
                    else if (View.ObjectTypeInfo.Name == "BangPhanTichLuongNhanVien")
                    {
                        BangPhanTichLuongNhanVien obj = View.CurrentObject as BangPhanTichLuongNhanVien;
                        if (obj != null && obj.KyTinhLuong != null && !string.IsNullOrEmpty(obj.TrangThai))
                            enable = false;
                    }
                    #endregion

                    #region 1.16. B???ng ch???m c??ng gi???ng d???y
                    else if (View.ObjectTypeInfo.Name == "CC_QuanLyCongGiangDay")
                    {
                        CC_QuanLyCongGiangDay obj = View.CurrentObject as CC_QuanLyCongGiangDay;
                        if (obj != null && obj.KyTinhLuong != null && obj.KhoaSo)
                            enable = false;
                    }
                    #endregion
                }
                #endregion

                #region 4.Tuy???n d???ng
                else if (View.ObjectTypeInfo.FullName.Contains("TuyenDung"))
                {
                    #region 4.1. V??? tr?? tuy???n d???ng
                    if (View.ObjectTypeInfo.Name == "ViTriTuyenDung")
                    {
                        ViTriTuyenDung obj = View.CurrentObject as ViTriTuyenDung;
                        if (obj != null)
                        {
                            //N???u ???? ????ng k?? tuy???n d???ng cho v??? tr?? tuy???n d???ng th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("ViTriTuyenDung=? && GCRecord IS NULL", obj.Oid);
                            DangKyTuyenDung dangKy = View.ObjectSpace.FindObject<DangKyTuyenDung>(filter);
                            if (dangKy != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 4.2. ????ng k?? tuy???n d???ng
                    else if (View.ObjectTypeInfo.Name == "DangKyTuyenDung")
                    {
                        DangKyTuyenDung obj = View.CurrentObject as DangKyTuyenDung;
                        if (obj != null)
                        {
                            if (obj.Duyet == true)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 4.3. Duy???t ????ng k?? tuy???n d???ng
                    else if (View.ObjectTypeInfo.Name == "NhuCauTuyenDung")
                    {
                        NhuCauTuyenDung obj = View.CurrentObject as NhuCauTuyenDung;
                        if (obj != null)
                        {
                            //N???u v??? tr?? tuy???n d???ng ???????c duy???t t???n t???i trong chi ti???t tuy???n d???ng th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("NhuCauTuyenDung=? && GCRecord IS NULL", obj);
                            ChiTietTuyenDung chiTietTuyenDung = View.ObjectSpace.FindObject<ChiTietTuyenDung>(filter);
                            if (chiTietTuyenDung != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 4.4. ???ng vi??n
                    else if (View.ObjectTypeInfo.Name == "UngVien")
                    {
                        UngVien obj = View.CurrentObject as UngVien;
                        if (obj != null)
                        {
                            //N???u nh??n vi??n ???? ti???n h??nh ph???ng v???n th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("UngVien=? && GCRecord IS NULL", obj);
                            ChiTietVongTuyenDung chiTietVongTuyenDung = View.ObjectSpace.FindObject<ChiTietVongTuyenDung>(filter);
                            if (chiTietVongTuyenDung != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 4.5. B?????c tuy???n d???ng
                    else if (View.ObjectTypeInfo.Name == "BuocTuyenDung")
                    {
                        BuocTuyenDung obj = View.CurrentObject as BuocTuyenDung;
                        if (obj != null)
                        {
                            // N???u v??ng tuy???n d???ng t???n t???i trong chi ti???t b?????c tuy???n d???ng th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("BuocTuyenDung=? && GCRecord IS NULL", obj.Oid);
                            VongTuyenDung vongTuyenDung = View.ObjectSpace.FindObject<VongTuyenDung>(filter);
                            if (vongTuyenDung != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 4.6. V??ng tuy???n d???ng
                    else if (View.ObjectTypeInfo.Name == "VongTuyenDung")
                    {
                        CriteriaOperator filter;
                        VongTuyenDung obj = View.CurrentObject as VongTuyenDung;
                        if (obj != null)
                        {
                            if (obj.ChiTietTuyenDung != null && obj.BuocTuyenDung != null)
                            {
                                // N???u ch??a ph???i l?? v??ng tuy???n d???ng cu???i th?? kh??ng ???????c x??a
                                filter = CriteriaOperator.Parse("ChiTietTuyenDung=? && BuocTuyenDung.ThuTu =? && GCRecord IS NULL"
                                                                , obj.ChiTietTuyenDung, obj.BuocTuyenDung.ThuTu + 1);
                                VongTuyenDung vongTuyenDung = View.ObjectSpace.FindObject<VongTuyenDung>(filter);
                                if (vongTuyenDung != null)
                                    enable = false;
                            }
                            // N???u ???? c?? ???ng vi??n trong chi ti???t th?? v??ng tuy???n d???ng th?? kh??ng ???????c x??a
                            filter = CriteriaOperator.Parse("VongTuyenDung=? && GCRecord IS NULL", obj.Oid);
                            ChiTietVongTuyenDung chiTietVongTuyenDung = View.ObjectSpace.FindObject<ChiTietVongTuyenDung>(filter);
                            if (chiTietVongTuyenDung != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 4.7. Chi ti???t tuy???n d???ng
                    else if (View.ObjectTypeInfo.Name == "ChiTietTuyenDung")
                    {
                        ChiTietTuyenDung obj = View.CurrentObject as ChiTietTuyenDung;
                        if (obj != null && obj.NhuCauTuyenDung != null)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("NhuCauTuyenDung=? && GCRecord IS NULL", obj.NhuCauTuyenDung);
                            TrungTuyen trungTuyen = View.ObjectSpace.FindObject<TrungTuyen>(filter);
                            if (trungTuyen != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 4.8. Chi ti???t v??ng tuy???n d???ng
                    else if (View.ObjectTypeInfo.Name == "ChiTietVongTuyenDung")
                    {
                        CriteriaOperator filter;
                        ChiTietVongTuyenDung obj = View.CurrentObject as ChiTietVongTuyenDung;
                        if (obj != null && obj.UngVien != null)
                        {
                            if (obj.VongTuyenDung.ChiTietTuyenDung != null && obj.NhuCauTuyenDung != null && obj.VongTuyenDung.BuocTuyenDung != null)
                            {
                                // N???u ch??a ph???i l?? v??ng tuy???n d???ng cu???i c??ng c???a ???ng vi??n th?? kh??ng ???????c x??a
                                filter = CriteriaOperator.Parse("VongTuyenDung.ChiTietTuyenDung=? && NhuCauTuyenDung =? && VongTuyenDung.BuocTuyenDung.ThuTu =? && UngVien =? && GCRecord IS NULL"
                                                                , obj.VongTuyenDung.ChiTietTuyenDung, obj.NhuCauTuyenDung, obj.VongTuyenDung.BuocTuyenDung.ThuTu + 1, obj.UngVien);
                                ChiTietVongTuyenDung ctVongTuyenDung = View.ObjectSpace.FindObject<ChiTietVongTuyenDung>(filter);
                                if (ctVongTuyenDung != null)
                                    enable = false;
                            }
                            // N???u ???ng vi??n ???? ???????c x??t tuy???n th?? kh??ng ???????c x??a
                            filter = CriteriaOperator.Parse("UngVien=? && GCRecord IS NULL", obj.UngVien);
                            TrungTuyen trungTuyen = View.ObjectSpace.FindObject<TrungTuyen>(filter);
                            KhongTrungTuyen khongTrungTuyen = View.ObjectSpace.FindObject<KhongTrungTuyen>(filter);
                            if (trungTuyen != null || khongTrungTuyen != null)
                                enable = false;
                        }
                    }
                    #endregion

                    #region 4.9. Tr??ng tuy???n
                    else if (View.ObjectTypeInfo.Name == "TrungTuyen")
                    {
                        TrungTuyen obj = View.CurrentObject as TrungTuyen;
                        if (obj != null && obj.UngVien != null)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien.CMND=? && GCRecord IS NULL", obj.UngVien.CMND);
                            ChiTietQuyetDinhTuyenDung ctTuyenDung = View.ObjectSpace.FindObject<ChiTietQuyetDinhTuyenDung>(filter);
                            if (ctTuyenDung != null)
                            {
                                enable = false;
                            }
                        }
                    }                    
                    #endregion

                    #region 4.10. Chi ti???t quy???t ?????nh tuy???n d???ng
                    else if (View.ObjectTypeInfo.Name == "ChiTietQuyetDinhTuyenDung")
                    {
                        ChiTietQuyetDinhTuyenDung obj = View.CurrentObject as ChiTietQuyetDinhTuyenDung;
                        if (obj != null && obj.ThongTinNhanVien != null)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("Oid=? && GCRecord IS NULL", obj.ThongTinNhanVien);
                            ThongTinNhanVien thongTinNhanVien = View.ObjectSpace.FindObject<ThongTinNhanVien>(filter);
                            if (thongTinNhanVien != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 4.11. Quy???t ?????nh tuy???n d???ng
                    else if (View.ObjectTypeInfo.Name == "QuyetDinhTuyenDung")
                    {
                        QuyetDinhTuyenDung obj = View.CurrentObject as QuyetDinhTuyenDung;
                        if (obj != null)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhTuyenDung=? && GCRecord IS NULL", obj);
                            ChiTietQuyetDinhTuyenDung ctQuyetDinhTuyenDung = View.ObjectSpace.FindObject<ChiTietQuyetDinhTuyenDung>(filter);
                            if (ctQuyetDinhTuyenDung != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                #region 6.????o t???o
                else if (View.ObjectTypeInfo.FullName.Contains("DaoTao"))
                {
                    #region 6.1. ????ng k?? ????o t???o
                    if (View.ObjectTypeInfo.Name == "DangKyDaoTao")
                    {
                        DangKyDaoTao obj = View.CurrentObject as DangKyDaoTao;
                        if (obj != null)
                        {
                            //N???u ???? ????ng k?? ???? ???????c duy???t th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("DangKyDaoTao=?", obj.Oid);
                            DuyetDangKyDaoTao dangKy = View.ObjectSpace.FindObject<DuyetDangKyDaoTao>(filter);
                            if (dangKy != null)
                            {
                                enable = false;
                            }
                        }
                    }

                    else if (View.ObjectTypeInfo.Name == "ChiTietDangKyDaoTao")
                    {
                        ChiTietDangKyDaoTao obj = View.CurrentObject as ChiTietDangKyDaoTao;
                        if (obj != null && obj.DangKyDaoTao != null && obj.ThongTinNhanVien != null)
                        {
                            //N???u ???? ????ng k?? ???? ???????c duy???t th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and DuyetDangKyDaoTao.DangKyDaoTao=?", obj.ThongTinNhanVien.Oid, obj.DangKyDaoTao.Oid);
                            ChiTietDuyetDangKyDaoTao ctDuyet = View.ObjectSpace.FindObject<ChiTietDuyetDangKyDaoTao>(filter);
                            if (ctDuyet != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 6.2 Duy???t ????ng k?? ????o t???o
                    else if (View.ObjectTypeInfo.Name == "DuyetDangKyDaoTao")
                    {
                        DuyetDangKyDaoTao obj = View.CurrentObject as DuyetDangKyDaoTao;
                        if (obj != null)
                        {
                            //N???u duy???t ????o t???o ???? l???p quy???t ?????nh r???i th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("DuyetDangKyDaoTao=?", obj.Oid);
                            QuyetDinhDaoTao quyetDinhDaoTao = View.ObjectSpace.FindObject<QuyetDinhDaoTao>(filter);
                            if (quyetDinhDaoTao != null)
                            {
                                enable = false;
                            }
                        }
                    }

                    else if (View.ObjectTypeInfo.Name == "ChiTietDuyetDangKyDaoTao")
                    {
                        ChiTietDuyetDangKyDaoTao obj = View.CurrentObject as ChiTietDuyetDangKyDaoTao;
                        if (obj != null)
                        {
                            //N???u ???? ????ng k?? ???? ???????c duy???t th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinhDaoTao.DuyetDangKyDaoTao=?", obj.ThongTinNhanVien.Oid, obj.DuyetDangKyDaoTao.Oid);
                            ChiTietQuyetDinhDaoTao ctquyetdinh = View.ObjectSpace.FindObject<ChiTietQuyetDinhDaoTao>(filter);
                            if (ctquyetdinh != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion

                    #region 6.3. Quy???t ?????nh ????o t???o
                    else if (View.ObjectTypeInfo.Name == "QuyetDinhDaoTao")
                    {
                        QuyetDinhDaoTao obj = View.CurrentObject as QuyetDinhDaoTao;
                        if (obj != null)
                        {
                            //N???u quy???t ?????nh ????o t???o ???? ???????c c??ng nh???n th?? kh??ng ???????c x??a
                            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhDaoTao=?", obj.Oid);
                            QuyetDinhCongNhanDaoTao quyetDinhCongNhanDaoTao = View.ObjectSpace.FindObject<QuyetDinhCongNhanDaoTao>(filter);
                            if (quyetDinhCongNhanDaoTao != null)
                            {
                                enable = false;
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                
                //V?? hi???u h??a
                DeleteAction.Active["ViewAllowDelete"] = enable;
            }
            finally
            {
                DeleteAction.EndUpdate();
            }
        }
    }
}
