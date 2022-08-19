using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERP.Module.NghiepVu.NhanSu.Helper
{
    public static class ProcessesHelper
    {
        /// <summary>
        /// Create qua trinh bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="chucVu"></param>
        /// <param name="hspcChucVu"></param>
        /// <param name="ngayHuong"></param>
        public static void CreateQuaTrinhBoNhiem(Session session, QuyetDinhCaNhan quyetDinh, ChucVu chucVu, ChucDanh chucDanh, decimal pcKiemNhiem, decimal pcTrachNhiem, DateTime ngayBoNhiem, DateTime ngayHetNhiemKy, string ghiChu)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhBoNhiem quaTrinhBoNhiem = session.FindObject<QuaTrinhBoNhiem>(filter);
            if (quaTrinhBoNhiem == null)
            {
                quaTrinhBoNhiem = new QuaTrinhBoNhiem(session);
                quaTrinhBoNhiem.QuyetDinh = quyetDinh;
                quaTrinhBoNhiem.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
            }
            quaTrinhBoNhiem.SoQuyetDinh = quyetDinh.SoQuyetDinh;
            quaTrinhBoNhiem.ChucVu = chucVu;
            quaTrinhBoNhiem.ChucDanh = chucDanh;
            quaTrinhBoNhiem.PhuCapKiemNhiem = pcKiemNhiem;
            quaTrinhBoNhiem.PhuCapTrachNhiem = pcTrachNhiem;
            quaTrinhBoNhiem.NgayBoNhiemChucVu = ngayBoNhiem;
            quaTrinhBoNhiem.TuNam = quyetDinh.NgayHieuLuc.ToString("d");
            quaTrinhBoNhiem.DenNam = ngayHetNhiemKy.ToString("d");
            quaTrinhBoNhiem.GhiChu = ghiChu;
        }

        /// <summary>
        /// Create qua trinh bo nhiem
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="chucVu"></param>
        /// <param name="hspcChucVu"></param>
        /// <param name="ngayHuong"></param>
        public static void CreateQuaTrinhBoNhiemKiemNhiem(Session session, QuyetDinhCaNhan quyetDinh, ChucVu chucVu, decimal hspcChucVu, DateTime ngayHuong)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=? and NgayHuongChucVu=?", quyetDinh, quyetDinh.ThongTinNhanVien, ngayHuong);
            QuaTrinhBoNhiemKiemNhiem quaTrinhBoNhiem = session.FindObject<QuaTrinhBoNhiemKiemNhiem>(filter);
            if (quaTrinhBoNhiem == null)
            {
                quaTrinhBoNhiem = new QuaTrinhBoNhiemKiemNhiem(session);
                quaTrinhBoNhiem.QuyetDinh = quyetDinh;
                quaTrinhBoNhiem.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
            }
            quaTrinhBoNhiem.SoQuyetDinh = quyetDinh.SoQuyetDinh;
            quaTrinhBoNhiem.ChucVu = chucVu;
            quaTrinhBoNhiem.HSPCChucVu = hspcChucVu;
            quaTrinhBoNhiem.NgayHuongChucVu = ngayHuong;
            quaTrinhBoNhiem.TuNam = quyetDinh.NgayHieuLuc.ToString("d");
        }

        /// <summary>
        /// Update diễn biến lương của quyết định trước (New)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <param name="ngay"></param>
        public static void UpdateDienBienLuong(Session session, QuyetDinh quyetDinh, ThongTinNhanVien nhanVien, DateTime ngay, bool themMoi)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay<=?", nhanVien, ngay);
            SortProperty sort = new SortProperty("TuNgay", SortingDirection.Descending);
            XPCollection<DienBienLuong> dienBienLuongList = new XPCollection<DienBienLuong>(session, filter, sort) { TopReturnedObjects = 2 };
            if (dienBienLuongList.Count > 0)
            {
                if (themMoi == true && dienBienLuongList[0].QuyetDinh != quyetDinh)
                    dienBienLuongList[0].DenNgay = ngay.AddDays(-1);
                else if (themMoi == false && dienBienLuongList.Count > 1)
                    dienBienLuongList[1].DenNgay = DateTime.MinValue;
            }
        }

        /// <summary>
        /// Create diễn biến lương
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="objectDetail"></param>
        public static void CreateDienBienLuong(Session session, QuyetDinh quyetDinh, ThongTinNhanVien thongTinNhanVien, Object objectDetail)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", quyetDinh, thongTinNhanVien);
            DienBienLuong dienBienLuong = session.FindObject<DienBienLuong>(filter);
            if (dienBienLuong == null)
            {
                dienBienLuong = new DienBienLuong(session);
                dienBienLuong.QuyetDinh = quyetDinh;
            }
            dienBienLuong.SoQuyetDinh = quyetDinh.SoQuyetDinh;
            dienBienLuong.LyDo = quyetDinh.NoiDung;
            dienBienLuong.NgayQuyetDinh = quyetDinh.NgayQuyetDinh;

            //Cập nhật một số thông tin về lương
            if (objectDetail != null)
            {
                if (objectDetail is ChiTietQuyetDinhNangLuong)
                {
                    //
                    dienBienLuong.ThongTinNhanVien = ((ChiTietQuyetDinhNangLuong)objectDetail).ThongTinNhanVien;
                    dienBienLuong.NgachLuong = ((ChiTietQuyetDinhNangLuong)objectDetail).NgachLuongMoi;
                    dienBienLuong.BacLuong = ((ChiTietQuyetDinhNangLuong)objectDetail).BacLuongMoi;
                    dienBienLuong.LuongCoBan = ((ChiTietQuyetDinhNangLuong)objectDetail).LuongCoBanMoi;
                    dienBienLuong.LuongKinhDoanh = ((ChiTietQuyetDinhNangLuong)objectDetail).LuongKinhDoanhMoi;
                    dienBienLuong.TuNgay = ((ChiTietQuyetDinhNangLuong)objectDetail).NgayHuongLuongMoi;
                    dienBienLuong.PhuCapKiemNhiem = ((ChiTietQuyetDinhNangLuong)objectDetail).PhuCapKiemNhiemMoi;
                    dienBienLuong.PhuCapTrachNhiem = ((ChiTietQuyetDinhNangLuong)objectDetail).PhuCapTrachNhiemMoi;
                    dienBienLuong.PhuCapBanTru = ((ChiTietQuyetDinhNangLuong)objectDetail).PhuCapBanTruMoi;
                    dienBienLuong.PhanTramTinhLuong = ((ChiTietQuyetDinhNangLuong)objectDetail).PhanTramTinhLuongMoi;
                    //Lấy thêm thông tin từ nhân viên
                    ThongTinNhanVien nhanVien = ((ChiTietQuyetDinhNangLuong)objectDetail).ThongTinNhanVien;
                    if (nhanVien != null)
                    {
                        dienBienLuong.PhuCapDienThoai = nhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
                        dienBienLuong.PhuCapTienAn = nhanVien.NhanVienThongTinLuong.PhuCapTienAn;
                        dienBienLuong.PhuCapTienXang = nhanVien.NhanVienThongTinLuong.PhuCapTienXang;                       
                    }
                }
                if (quyetDinh is QuyetDinhTienLuongChinhThuc)
                {
                    dienBienLuong.NgachLuong = ((QuyetDinhTienLuongChinhThuc)quyetDinh).NgachLuongMoi;
                    dienBienLuong.BacLuong = ((QuyetDinhTienLuongChinhThuc)quyetDinh).BacLuongMoi;
                    dienBienLuong.ThongTinNhanVien = ((QuyetDinhTienLuongChinhThuc)quyetDinh).ThongTinNhanVien;
                    dienBienLuong.TuNgay = ((QuyetDinhTienLuongChinhThuc)quyetDinh).NgayHieuLuc;
                    dienBienLuong.LuongCoBan = ((QuyetDinhTienLuongChinhThuc)quyetDinh).LuongCoBanMoi;
                    dienBienLuong.LuongKinhDoanh = ((QuyetDinhTienLuongChinhThuc)quyetDinh).LuongKinhDoanhMoi;
                    dienBienLuong.PhanTramTinhLuong = ((QuyetDinhTienLuongChinhThuc)quyetDinh).MucHuongThuViecMoi;

                    //Lấy thêm thông tin từ nhân viên
                    ThongTinNhanVien nhanVien = ((QuyetDinhTienLuongChinhThuc)quyetDinh).ThongTinNhanVien;
                    if (nhanVien != null)
                    {
                        dienBienLuong.PhuCapDienThoai = nhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
                        dienBienLuong.PhuCapTienAn = nhanVien.NhanVienThongTinLuong.PhuCapTienAn;
                        dienBienLuong.PhuCapTienXang = nhanVien.NhanVienThongTinLuong.PhuCapTienXang;                        
                        dienBienLuong.PhuCapKiemNhiem = nhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
                    }
                }
                else if (quyetDinh is QuyetDinhTienLuongThuViec)
                {
                    dienBienLuong.ThongTinNhanVien = ((QuyetDinhTienLuongThuViec)quyetDinh).ThongTinNhanVien;
                    dienBienLuong.TuNgay = ((QuyetDinhTienLuongThuViec)quyetDinh).NgayHieuLuc;
                    dienBienLuong.NgachLuong = ((QuyetDinhTienLuongThuViec)quyetDinh).NgachLuongMoi;
                    dienBienLuong.BacLuong = ((QuyetDinhTienLuongThuViec)quyetDinh).BacLuongMoi;
                    dienBienLuong.LuongCoBan = ((QuyetDinhTienLuongThuViec)quyetDinh).LuongCoBanMoi;
                    dienBienLuong.LuongKinhDoanh = ((QuyetDinhTienLuongThuViec)quyetDinh).LuongKinhDoanhMoi;
                    dienBienLuong.PhanTramTinhLuong = ((QuyetDinhTienLuongThuViec)quyetDinh).MucHuongThuViec;

                    //Lấy thêm thông tin từ nhân viên
                    ThongTinNhanVien nhanVien = ((QuyetDinhTienLuongThuViec)quyetDinh).ThongTinNhanVien;
                    if (nhanVien != null)
                    {
                        dienBienLuong.PhuCapDienThoai = nhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
                        dienBienLuong.PhuCapTienAn = nhanVien.NhanVienThongTinLuong.PhuCapTienAn;
                        dienBienLuong.PhuCapTienXang = nhanVien.NhanVienThongTinLuong.PhuCapTienXang;                        
                        dienBienLuong.PhuCapKiemNhiem = nhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
                    }
                }
                if (objectDetail is ChiTietQuyetDinhTuyenDung)
                {
                    //
                    dienBienLuong.ThongTinNhanVien = ((ChiTietQuyetDinhTuyenDung)objectDetail).ThongTinNhanVien;
                    dienBienLuong.NgachLuong = ((ChiTietQuyetDinhTuyenDung)objectDetail).NgachLuong;
                    dienBienLuong.BacLuong = ((ChiTietQuyetDinhTuyenDung)objectDetail).BacLuong;
                    dienBienLuong.LuongCoBan = ((ChiTietQuyetDinhTuyenDung)objectDetail).LuongCoBan;
                    dienBienLuong.LuongKinhDoanh = ((ChiTietQuyetDinhTuyenDung)objectDetail).LuongKinhDoanh;
                    dienBienLuong.TuNgay = ((ChiTietQuyetDinhTuyenDung)objectDetail).NgayHuongLuong;                   
                    dienBienLuong.PhanTramTinhLuong = ((ChiTietQuyetDinhTuyenDung)objectDetail).PhanTramTinhLuong;                                
                }
            }
            else
            {
                if (quyetDinh is QuyetDinhDieuDong)
                {
                    dienBienLuong.ThongTinNhanVien = ((QuyetDinhDieuDong)quyetDinh).ThongTinNhanVien;
                    dienBienLuong.TuNgay = ((QuyetDinhDieuDong)quyetDinh).NgayHieuLuc;

                    //Lấy thêm thông tin từ nhân viên
                    ThongTinNhanVien nhanVien = ((QuyetDinhDieuDong)quyetDinh).ThongTinNhanVien;
                    if (nhanVien != null)
                    {
                        dienBienLuong.NgachLuong = nhanVien.NhanVienThongTinLuong.NgachLuong;
                        dienBienLuong.BacLuong = nhanVien.NhanVienThongTinLuong.BacLuong;
                        dienBienLuong.LuongCoBan = nhanVien.NhanVienThongTinLuong.LuongCoBan;
                        dienBienLuong.LuongKinhDoanh = nhanVien.NhanVienThongTinLuong.LuongKinhDoanh;
                        dienBienLuong.PhuCapDienThoai = nhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
                        dienBienLuong.PhuCapTienAn = nhanVien.NhanVienThongTinLuong.PhuCapTienAn;
                        dienBienLuong.PhuCapTienXang = nhanVien.NhanVienThongTinLuong.PhuCapTienXang;
                        dienBienLuong.PhanTramTinhLuong = nhanVien.NhanVienThongTinLuong.PhanTramTinhLuong;
                        dienBienLuong.PhuCapKiemNhiem = nhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
                    }
                }
            }
        }

        /// <summary>
        /// Xóa quá trình
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        public static void DeleteQuaTrinhNhanVien<T>(Session session, Guid quyetDinh, Guid nhanVien) where T : BaseObject
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? && ThongTinNhanVien=?", quyetDinh, nhanVien);
            T quaTrinh = session.FindObject<T>(filter);
            if (quaTrinh != null)
            {
                session.Delete(quaTrinh);
                session.Save(quaTrinh);
            }
        }

        /// <summary>
        /// Update quá trình bổ nhiệm
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="type"></param>
        public static void UpdateQuaTrinhBoNhiem(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay, bool type)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhBoNhiem quaTrinhBoNhiem = session.FindObject<QuaTrinhBoNhiem>(filter);
            if (quaTrinhBoNhiem != null)
            {
                if (type)
                    quaTrinhBoNhiem.DenNam = ngay.AddDays(-1).ToString("d");
                else
                    quaTrinhBoNhiem.DenNam = string.Empty;
            }
        }

        /// <summary>
        /// Update quá trình bổ nhiệm kiêm nhiệm
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="type"></param>
        public static void UpdateQuaTrinhBoNhiemKiemNhiem(Session session, QuyetDinhCaNhan quyetDinh, DateTime ngay, bool type)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", quyetDinh, quyetDinh.ThongTinNhanVien);
            QuaTrinhBoNhiemKiemNhiem quaTrinhBoNhiem = session.FindObject<QuaTrinhBoNhiemKiemNhiem>(filter);
            if (quaTrinhBoNhiem != null)
            {
                if (type)
                    quaTrinhBoNhiem.DenNam = ngay.AddDays(-1).ToString("d");
                else
                    quaTrinhBoNhiem.DenNam = string.Empty;
            }
        }

        /// <summary>
        /// Xóa quá trình
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="quyetDinh"></param>
        public static void DeleteQuaTrinh<T>(Session session, CriteriaOperator filter) where T : BaseObject
        {
            T quaTrinh = session.FindObject<T>(filter);
            if (quaTrinh != null)
            {
                session.Delete(quaTrinh);
                session.Save(quaTrinh);
            }
        }

        /// <summary>
        /// Create qua trinh dieu dong
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="ngaydieudong"></param>
        public static void CreateQuaTrinhDieuDong(Session session, QuyetDinhDieuDong quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=? and NgayDieuChuyen=?", quyetDinh.Oid, quyetDinh.ThongTinNhanVien, quyetDinh.NgayHieuLuc);
            QuaTrinhDieuDong quaTrinhDieuDong = session.FindObject<QuaTrinhDieuDong>(filter);
            if (quaTrinhDieuDong == null)
            {
                quaTrinhDieuDong = new QuaTrinhDieuDong(session);
                quaTrinhDieuDong.QuyetDinh = quyetDinh;
                quaTrinhDieuDong.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
            }
            quaTrinhDieuDong.NgayDieuChuyen = quyetDinh.NgayHieuLuc;
            quaTrinhDieuDong.DonViMoi = quyetDinh.BoPhanMoi;
            quaTrinhDieuDong.DonViCu = quyetDinh.BoPhan;
            quaTrinhDieuDong.ChucDanhCu = quyetDinh.ChucDanhCu;
            quaTrinhDieuDong.ChucDanhMoi = quyetDinh.ChucDanhMoi;
            quaTrinhDieuDong.ChucVuCu = quyetDinh.ChucVuCu;
            quaTrinhDieuDong.ChucVuMoi = quyetDinh.ChucVuMoi;
            quaTrinhDieuDong.LyDo = string.Format("{0} - {1}", quyetDinh.CanCu, quyetDinh.NoiDung);
            quaTrinhDieuDong.CongTyCu = quyetDinh.CongTyCu;          
        }

        public static void CreateQuaTrinhLuanChuyen(Session session, QuyetDinhLuanChuyen quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=? and NgayDieuChuyen=?", quyetDinh.Oid, quyetDinh.ThongTinNhanVien, quyetDinh.NgayHieuLuc);
            QuaTrinhDieuDong quaTrinhDieuDong = session.FindObject<QuaTrinhDieuDong>(filter);
            if (quaTrinhDieuDong == null)
            {
                quaTrinhDieuDong = new QuaTrinhDieuDong(session);
                quaTrinhDieuDong.QuyetDinh = quyetDinh;
                quaTrinhDieuDong.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
            }
            quaTrinhDieuDong.NgayDieuChuyen = quyetDinh.NgayHieuLuc;
            quaTrinhDieuDong.DonViMoi = quyetDinh.BoPhanMoi;
            quaTrinhDieuDong.DonViCu = quyetDinh.BoPhan;
            quaTrinhDieuDong.ChucDanhCu = quyetDinh.ChucDanhCu;
            quaTrinhDieuDong.ChucDanhMoi = quyetDinh.ChucDanhMoi;
            quaTrinhDieuDong.ChucVuCu = quyetDinh.ChucVuCu;
            quaTrinhDieuDong.ChucVuMoi = quyetDinh.ChucVuMoi;
            quaTrinhDieuDong.LyDo = string.Format("{0} - {1}", quyetDinh.CanCu, quyetDinh.NoiDung);
            quaTrinhDieuDong.CongTyCu = quyetDinh.CongTyCu;
            quaTrinhDieuDong.CongTyMoi = quyetDinh.CongTyMoi;
        }

        public static void CreateQuaTrinhBietPhai(Session session, QuyetDinhBietPhai quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=? and NgayDieuChuyen=?", quyetDinh.Oid, quyetDinh.ThongTinNhanVien, quyetDinh.NgayHieuLuc);
            QuaTrinhDieuDong quaTrinhDieuDong = session.FindObject<QuaTrinhDieuDong>(filter);
            if (quaTrinhDieuDong == null)
            {
                quaTrinhDieuDong = new QuaTrinhDieuDong(session);
                quaTrinhDieuDong.QuyetDinh = quyetDinh;
                quaTrinhDieuDong.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
            }
            quaTrinhDieuDong.NgayDieuChuyen = quyetDinh.NgayHieuLuc;
            quaTrinhDieuDong.DonViMoi = quyetDinh.BoPhanMoi;
            quaTrinhDieuDong.DonViCu = quyetDinh.BoPhan;
            quaTrinhDieuDong.ChucDanhCu = quyetDinh.ChucDanhCu;
            quaTrinhDieuDong.ChucDanhMoi = quyetDinh.ChucDanhMoi;
            quaTrinhDieuDong.ChucVuCu = quyetDinh.ChucVuCu;
            quaTrinhDieuDong.ChucVuMoi = quyetDinh.ChucVuMoi;
            quaTrinhDieuDong.LyDo = string.Format("{0} - {1}", quyetDinh.CanCu, quyetDinh.NoiDung);
        }

        /// <summary>
        /// Create qua trinh cong tac
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="noiDung"></param>
        public static void CreateQuaTrinhCongTac(Session session, QuyetDinhCaNhan quyetDinh, string noiDung)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                    quyetDinh.ThongTinNhanVien, quyetDinh);
            QuaTrinhCongTac quaTrinhCongTac = session.FindObject<QuaTrinhCongTac>(filter);
            if (quaTrinhCongTac == null)
            {
                quaTrinhCongTac = new QuaTrinhCongTac(session);
                quaTrinhCongTac.ThongTinNhanVien = quyetDinh.ThongTinNhanVien;
                quaTrinhCongTac.QuyetDinh = quyetDinh;
            }
            quaTrinhCongTac.TuNam = quyetDinh.NgayHieuLuc.ToString("d");
            quaTrinhCongTac.NoiDung = noiDung;
        }

        public static void CreateQuaTrinhCongTac(Session session, ThongTinNhanVien nhanVien, QuyetDinh quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                    nhanVien, quyetDinh);
            QuaTrinhCongTac quaTrinhCongTac = session.FindObject<QuaTrinhCongTac>(filter);
            if (quaTrinhCongTac == null)
            {
                quaTrinhCongTac = new QuaTrinhCongTac(session);
                quaTrinhCongTac.ThongTinNhanVien = nhanVien;
                quaTrinhCongTac.QuyetDinh = quyetDinh;
            }
            quaTrinhCongTac.TuNam = quyetDinh.NgayHieuLuc.ToString("d");
            quaTrinhCongTac.NoiDung = quyetDinh.NoiDung;
        }

        /// <summary>
        /// Create qua trinh dao tao
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinhDaoTao"></param>
        /// <param name="truong"></param>
        public static void CreateQuaTrinhDaoTao(Session session, QuyetDinhCongNhanDaoTao qdCongNhanDaoTao, ThongTinNhanVien nhanVien)
        {
            QuaTrinhDaoTao quaTrinhDaoTao;
            CriteriaOperator filter;
            if (qdCongNhanDaoTao != null && qdCongNhanDaoTao.QuyetDinhDaoTao != null)
            {
                //ChiTietQuyetDinhDaoTao item1 = DaoTaoHelper.GetChiTietDaoTao(session, qdCongNhanDaoTao.QuyetDinhDaoTao, nhanVien);
                ChiTietQuyetDinhCongNhanDaoTao item2 = DaoTaoHelper.GetChiTietCongNhanDaoTao(session, qdCongNhanDaoTao, nhanVien);

                if (item2 != null)
                {
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                        item2.ThongTinNhanVien, qdCongNhanDaoTao.QuyetDinhDaoTao.Oid);
                    quaTrinhDaoTao = session.FindObject<QuaTrinhDaoTao>(filter);
                    if (quaTrinhDaoTao == null)
                    {
                        quaTrinhDaoTao = new QuaTrinhDaoTao(session);
                        quaTrinhDaoTao.ThongTinNhanVien = item2.ThongTinNhanVien;
                        quaTrinhDaoTao.QuyetDinh = qdCongNhanDaoTao.QuyetDinhDaoTao;
                    }

                    quaTrinhDaoTao.SoVanBang = item2.SoVanBang;
                    quaTrinhDaoTao.NamTotNghiep = item2.NgayCapVB.Year;
                    quaTrinhDaoTao.TruongDaoTao = qdCongNhanDaoTao.QuyetDinhDaoTao.TruongDaoTao;
                    quaTrinhDaoTao.HinhThucDaoTao = qdCongNhanDaoTao.QuyetDinhDaoTao.ChuongTrinhDaoTao.HinhThucDaoTao;
                    quaTrinhDaoTao.ChuyenMonDaoTao = qdCongNhanDaoTao.QuyetDinhDaoTao.ChuongTrinhDaoTao.ChuyenMonDaoTao;
                }
            }
            else
            {
                quaTrinhDaoTao = new QuaTrinhDaoTao(session);
                quaTrinhDaoTao.ThongTinNhanVien = nhanVien;
                quaTrinhDaoTao.QuyetDinh = null;
            }
        }

        public static void CreateQuaTrinhDaoTao(Session session, ThongTinNhanVien nhanVien,
            TrinhDoChuyenMon trinhDoChuyenMon, ChuyenNganhDaoTao chuyenMonDaoTao, TruongDaoTao truongDaoTao,
            HinhThucDaoTao hinhThucDaoTao, int namTotNghiep, DateTime ngayCapBang)
        {
            QuaTrinhDaoTao quaTrinhDaoTao;
            CriteriaOperator filter;
            if (trinhDoChuyenMon != null)
            {
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? and BangCap=? and ChuyenMonDaoTao=? and TruongDaoTao=?",
                    nhanVien, trinhDoChuyenMon, chuyenMonDaoTao, truongDaoTao);
                quaTrinhDaoTao = session.FindObject<QuaTrinhDaoTao>(filter);
                if (quaTrinhDaoTao == null)
                {
                    quaTrinhDaoTao = new QuaTrinhDaoTao(session);
                    quaTrinhDaoTao.ThongTinNhanVien = nhanVien;
                }
                quaTrinhDaoTao.NamTotNghiep = namTotNghiep;

                quaTrinhDaoTao.TruongDaoTao = truongDaoTao;
                quaTrinhDaoTao.HinhThucDaoTao = hinhThucDaoTao;
                quaTrinhDaoTao.ChuyenMonDaoTao = chuyenMonDaoTao;
            }
        }

        public static void DeleteQuaTrinhDaoTao(Session session, ThongTinNhanVien nhanVien,
            TrinhDoChuyenMon trinhDoChuyenMon, ChuyenMonDaoTao chuyenMonDaoTao, TruongDaoTao truongDaoTao,
            HinhThucDaoTao hinhThucDaoTao, int namTotNghiep, DateTime ngayCapBang)
        {
            QuaTrinhDaoTao quaTrinhDaoTao;
            CriteriaOperator filter;
            if (trinhDoChuyenMon != null)
            {
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? and BangCap=? and ChuyenMonDaoTao=? and TruongDaoTao=?",
                    nhanVien, trinhDoChuyenMon, chuyenMonDaoTao, truongDaoTao);
                quaTrinhDaoTao = session.FindObject<QuaTrinhDaoTao>(filter);
                if (quaTrinhDaoTao == null)
                {
                    session.Delete(quaTrinhDaoTao);
                    session.Save(quaTrinhDaoTao);
                }
            }
        }

        /// <summary>
        /// Tạo lịch sử bản thân
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinhDaoTao"></param>
        /// <param name="truong"></param>
        public static void CreateLichSuBanThan(Session session, ThongTinNhanVien nhanVien, QuyetDinhChamDutHopDong quyetDinh, string noiDung)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                    nhanVien, quyetDinh);
            LichSuBanThan lichSuBanThan = session.FindObject<LichSuBanThan>(filter);
            if (lichSuBanThan == null)
            {
                lichSuBanThan = new LichSuBanThan(session);
                lichSuBanThan.ThongTinNhanVien = nhanVien;
                if (nhanVien.ChucDanh != null)
                {
                    ChucDanh chucDanh = session.GetObjectByKey<ChucDanh>(nhanVien.ChucDanh.Oid);
                    lichSuBanThan.ChucDanh = chucDanh.TenChucDanh;
                }
                if (nhanVien.CongTy != null)
                {
                    CongTy congTy = session.GetObjectByKey<CongTy>(nhanVien.CongTy.Oid);
                    lichSuBanThan.CongTy = congTy.TenBoPhan;
                }
                if (nhanVien.NgayVaoCongTy != DateTime.MinValue)
                    lichSuBanThan.TuNam = nhanVien.NgayVaoCongTy.ToString("d");
                lichSuBanThan.QuyetDinh = quyetDinh;                
            }
            lichSuBanThan.DenNam = quyetDinh.NgayHieuLuc.ToString("d");
            lichSuBanThan.LyDoNghiViec = quyetDinh.NoiDungLyDo;
            lichSuBanThan.GhiChu = noiDung;
        }

        public static void CreateLichSuBanThan(Session session, ThongTinNhanVien nhanVien, QuyetDinhTuyenDung quyetDinh, DateTime ngayVao, string noiDung)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh =?",
                    nhanVien, quyetDinh);
            LichSuBanThan lichSuBanThan = session.FindObject<LichSuBanThan>(filter);
            if (lichSuBanThan == null)
            {
                lichSuBanThan = new LichSuBanThan(session);
                lichSuBanThan.ThongTinNhanVien = nhanVien;
                lichSuBanThan.QuyetDinh = quyetDinh;
                if (nhanVien.ChucDanh != null)
                {
                    ChucDanh chucDanh = session.GetObjectByKey<ChucDanh>(nhanVien.ChucDanh.Oid);
                    lichSuBanThan.ChucDanh = chucDanh.TenChucDanh;
                }
                if (nhanVien.CongTy != null)
                {
                    CongTy congTy = session.GetObjectByKey<CongTy>(nhanVien.CongTy.Oid);
                    lichSuBanThan.CongTy = congTy.TenBoPhan;
                }                               
            }
            if (nhanVien.NgayVaoCongTy != DateTime.MinValue)
                lichSuBanThan.TuNam = ngayVao.ToString("d");   
            lichSuBanThan.GhiChu = noiDung;            
        }

        /// <summary>
        /// tạo quá trình khen thưởng
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="quyetDinh"></param>
        public static void CreateQuaTrinhKhenThuong(Session session, ThongTinNhanVien nhanVien, QuyetDinhKhenThuong quyetDinh, ChiTietQuyetDinhKhenThuongNhanVien chiTiet)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh.Oid=? and ThongTinNhanVien.Oid=?",
                    quyetDinh.Oid, nhanVien.Oid);
            QuaTrinhKhenThuong quaTrinhKhenThuong = session.FindObject<QuaTrinhKhenThuong>(filter);
            if (quaTrinhKhenThuong == null)
            {
                quaTrinhKhenThuong = new QuaTrinhKhenThuong(session);
                quaTrinhKhenThuong.QuyetDinh = quyetDinh;
                quaTrinhKhenThuong.ThongTinNhanVien = nhanVien;
            }
            quaTrinhKhenThuong.SoQuyetDinh = quyetDinh.SoQuyetDinh;
            quaTrinhKhenThuong.NgayKhenThuong = quyetDinh.NgayHieuLuc;            
            quaTrinhKhenThuong.DanhHieuKhenThuong = chiTiet.DanhHieuKhenThuong != null ? chiTiet.DanhHieuKhenThuong : quyetDinh.DanhHieuKhenThuong;
            quaTrinhKhenThuong.LyDo = !string.IsNullOrEmpty(chiTiet.LyDo) ? chiTiet.LyDo : quyetDinh.LyDo;
        }

        /// <summary>
        /// tạo quá trình kỷ luật
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="quyetDinh"></param>
        public static void CreateQuaTrinhKyLuat(Session session, ThongTinNhanVien nhanVien, QuyetDinhKyLuat quyetDinh)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh.Oid=? and ThongTinNhanVien.Oid=?",
                    quyetDinh.Oid, nhanVien.Oid);
            QuaTrinhKyLuat quaTrinhKyLuat = session.FindObject<QuaTrinhKyLuat>(filter);
            if (quaTrinhKyLuat == null)
            {
                quaTrinhKyLuat = new QuaTrinhKyLuat(session);
                quaTrinhKyLuat.QuyetDinh = quyetDinh;
                quaTrinhKyLuat.ThongTinNhanVien = nhanVien;
            }
            quaTrinhKyLuat.SoQuyetDinh = quyetDinh.SoQuyetDinh;
            quaTrinhKyLuat.NgayQuyetDinh = quyetDinh.NgayQuyetDinh;
            quaTrinhKyLuat.TuNgay = quyetDinh.TuNgay;
            quaTrinhKyLuat.DenNgay = quyetDinh.DenNgay;
            quaTrinhKyLuat.HinhThucKyLuat = quyetDinh.HinhThucKyLuat != null ? quyetDinh.HinhThucKyLuat : quyetDinh.HinhThucKyLuat;
            quaTrinhKyLuat.LyDo = quyetDinh.LyDo;
        }
    }
}
