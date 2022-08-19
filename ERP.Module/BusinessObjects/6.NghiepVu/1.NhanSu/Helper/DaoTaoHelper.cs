using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using System;
using System.Collections.Generic;
using System.Linq;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.DaoTao;

namespace  ERP.Module.NghiepVu.NhanSu.Helper
{
    public static class DaoTaoHelper
    {
        /// <summary>
        /// Create van bang
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <param name="namTotNghiep"></param>
        public static void CreateVanBang(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien, int namTotNghiep)
        {
            if (quyetDinh != null)
            {
                ChiTietQuyetDinhDaoTao chiTietDaoTao = session.FindObject<ChiTietQuyetDinhDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid));
                if (chiTietDaoTao != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and TruongDaoTao=? and TrinhDoChuyenMon=? and ChuyenNganhDaoTao=?",
                        nhanVien, quyetDinh.TruongDaoTao, quyetDinh.ChuongTrinhDaoTao.TrinhDoChuyenMon, quyetDinh.ChuongTrinhDaoTao.ChuyenMonDaoTao);
                    VanBang bc = session.FindObject<VanBang>(filter);
                    if (bc == null)
                    {
                        bc = new VanBang(session);
                        bc.HoSo = nhanVien;                        
                        bc.TrinhDoChuyenMon = quyetDinh.ChuongTrinhDaoTao.TrinhDoChuyenMon;                         
                        bc.TruongDaoTao = quyetDinh.TruongDaoTao;
                        bc.ChuyenNganhDaoTao = quyetDinh.ChuongTrinhDaoTao.ChuyenMonDaoTao;
                        bc.HinhThucDaoTao = quyetDinh.ChuongTrinhDaoTao.HinhThucDaoTao;
                        bc.NamTotNghiep = namTotNghiep;
                        bc.QuyetDinh = quyetDinh;
                    }
                }
            }
            else
            {
                VanBang bc = new VanBang(session);
                bc.HoSo = nhanVien;
            }
        }

        public static void DeleteVanBang(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            ChiTietQuyetDinhDaoTao chiTietDaoTao = session.FindObject<ChiTietQuyetDinhDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid));
            if (chiTietDaoTao != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and TrinhDoChuyenMon=? and TruongDaoTao=? and ChuyenMonDaoTao=?",
                    nhanVien.Oid, quyetDinh.ChuongTrinhDaoTao.TrinhDoChuyenMon, quyetDinh.TruongDaoTao, quyetDinh.ChuongTrinhDaoTao.ChuyenMonDaoTao);
                VanBang bangCap = session.FindObject<VanBang>(filter);
                if (bangCap != null)
                {
                    session.Delete(bangCap);
                    session.Save(bangCap);
                }
            }
        }

        /// <summary>
        /// Create chung chi
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        public static void CreateChungChi(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien, DateTime ngayCap)
        {
            if (quyetDinh != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and LoaiChungChi=? and NgayCap=?",
                    nhanVien, quyetDinh.ChuongTrinhDaoTao.LoaiChungChi.Oid, ngayCap);
                ChungChi chungChi = session.FindObject<ChungChi>(filter);
                if (chungChi == null)
                {
                    chungChi = new ChungChi(session);
                    chungChi.HoSo = nhanVien;
                    chungChi.QuyetDinh = quyetDinh;
                    chungChi.LoaiChungChi = quyetDinh.ChuongTrinhDaoTao.LoaiChungChi;
                }                
                chungChi.TenChungChi = quyetDinh.ChuongTrinhDaoTao.TenChuongTrinh;
                chungChi.NoiCap = quyetDinh.TruongDaoTao.TenTruongDaoTao;
                chungChi.NgayCap = ngayCap;
            }
        }

        /// <summary>
        /// Delete chung chi
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        public static void DeleteChungChi(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            if (quyetDinh != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and LoaiChungChi=? and NgayCap=?",
                    nhanVien, quyetDinh.ChuongTrinhDaoTao.LoaiChungChi, quyetDinh.DenNgay);
                ChungChi chungChi = session.FindObject<ChungChi>(filter);
                if (chungChi != null)
                {
                    session.Delete(chungChi);
                    session.Save(chungChi);
                }
            }
        }

        public static void CreateVanBang(Session session, ThongTinNhanVien nhanVien,
            TrinhDoChuyenMon trinhDoChuyenMon, ChuyenNganhDaoTao chuyenMonDaoTao, TruongDaoTao truongDaoTao,
            HinhThucDaoTao hinhThucDaoTao, int namTotNghiep, DateTime ngayCapBang)
        {
            if (trinhDoChuyenMon != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and TrinhDoChuyenMon=? and ChuyenMonDaoTao=? and TruongDaoTao=?",
                    nhanVien, trinhDoChuyenMon, chuyenMonDaoTao, truongDaoTao);
                VanBang bc = session.FindObject<VanBang>(filter);
                if (bc == null)
                {
                    bc = new VanBang(session);
                }
                bc.HoSo = nhanVien;
                bc.TrinhDoChuyenMon = trinhDoChuyenMon;
                bc.ChuyenNganhDaoTao = chuyenMonDaoTao;
                bc.TruongDaoTao = truongDaoTao;
                bc.HinhThucDaoTao = hinhThucDaoTao;
                bc.NamTotNghiep = namTotNghiep;
                bc.NgayCapBang = ngayCapBang;
            }
        }


        public static void DeleteVanBang(Session session, ThongTinNhanVien nhanVien,
            TrinhDoChuyenMon trinhDoChuyenMon, ChuyenMonDaoTao chuyenMonDaoTao, TruongDaoTao truongDaoTao,
            HinhThucDaoTao hinhThucDaoTao, int namTotNghiep, DateTime ngayCapBang)
        {
            if (trinhDoChuyenMon != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and TrinhDoChuyenMon=? and ChuyenMonDaoTao=? and TruongDaoTao=?",
                        nhanVien, trinhDoChuyenMon, chuyenMonDaoTao, truongDaoTao);
                VanBang bc = session.FindObject<VanBang>(filter);
                if (bc != null)
                {
                    session.Delete(bc);
                    session.Save(bc);
                }
            }
        }

        /// <summary>
        /// Create dang theo hoc
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="trinhDo"></param>
        //public static void CreateDangTheoHoc(Session session, ThongTinNhanVien nhanVien, TrinhDoChuyenMon trinhDo, QuocGia quocGia)
        //{
        //    string tenChuongTrinhHoc;
        //    //
        //    if (trinhDo.TenTrinhDoChuyenMon.ToLower().Contains("tiến"))
        //        tenChuongTrinhHoc = "Nghiên cứu sinh";
        //    else if (trinhDo.TenTrinhDoChuyenMon.ToLower().Contains("thạc"))
        //        tenChuongTrinhHoc = "Cao học";
        //    else
        //        tenChuongTrinhHoc = trinhDo.TenTrinhDoChuyenMon;


        //    CriteriaOperator filter = CriteriaOperator.Parse("TenChuongTrinhHoc like ?",
        //        tenChuongTrinhHoc);
        //    ChuongTrinhHoc chuongTrinhHoc = session.FindObject<ChuongTrinhHoc>(filter);
        //    if (chuongTrinhHoc == null)
        //    {
        //        chuongTrinhHoc = new ChuongTrinhHoc(session);
        //        chuongTrinhHoc.MaQuanLy = tenChuongTrinhHoc;
        //        chuongTrinhHoc.TenChuongTrinhHoc = tenChuongTrinhHoc;
        //    }
        //    //
        //    {
        //        nhanVien.NhanVienTrinhDo.ChuongTrinhHoc = chuongTrinhHoc;
        //        //
        //        nhanVien.NhanVienTrinhDo.QuocGiaHoc = quocGia;
        //    }
        //}

        /// <summary>
        /// Reset dang theo hoc
        /// </summary>
        /// <param name="nhanVien"></param>
        //public static void ResetDangTheoHoc(ThongTinNhanVien nhanVien)
        //{
        //    nhanVien.NhanVienTrinhDo.ChuongTrinhHoc = null;
        //    nhanVien.NhanVienTrinhDo.QuocGiaHoc = null;
        //}

        /// <summary>
        /// Get chi tiet dao tao
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static ChiTietQuyetDinhDaoTao GetChiTietDaoTao(Session session, QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            if (quyetDinh != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid);
                ChiTietQuyetDinhDaoTao chiTiet = session.FindObject<ChiTietQuyetDinhDaoTao>(filter);
                return chiTiet;
            }
            else
                return null;
        }

        /// <summary>
        /// Get chi tiet dao tao
        /// </summary>
        /// <param name="session"></param>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static ChiTietQuyetDinhCongNhanDaoTao GetChiTietCongNhanDaoTao(Session session, QuyetDinhCongNhanDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            if (quyetDinh != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinhCongNhanDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid);
                ChiTietQuyetDinhCongNhanDaoTao chiTiet = session.FindObject<ChiTietQuyetDinhCongNhanDaoTao>(filter);
                return chiTiet;
            }
            else
                return null;
        }

        //public static TinhTrang GetTinhTrang(Session session, QuocGia quocGia, bool duocHuongLuong)
        //{
        //    TinhTrang tinhTrang = null;

        //    if (DiNuocNgoaiHelper.IsNgoaiNuoc(quocGia))
        //    {
        //        if (!duocHuongLuong)
        //        {
        //            tinhTrang = HoSoHelper.DiHocNgoaiNuocKhongLuong(session);
        //        }
        //        else
        //        {
        //            tinhTrang = HoSoHelper.DiHocNgoaiNuocCoLuong(session);
        //        }
        //    }
        //    else
        //    {
        //        if (!duocHuongLuong)
        //        {
        //            tinhTrang = HoSoHelper.DiHocTrongNuocKhongLuong(session);
        //        }
        //        else
        //        {
        //            tinhTrang = HoSoHelper.DiHocTrongNuocCoLuong(session);
        //        }
        //    }

        //    return tinhTrang;
        //}

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="quyetDinh"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExists(QuyetDinhDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietQuyetDinhDaoTao
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="duyetDK"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static bool IsExists(DuyetDangKyDaoTao duyetDK, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in duyetDK.ListChiTietDuyetDangKyDaoTao
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }

        public static bool IsExists(QuyetDinhCongNhanDaoTao quyetDinh, ThongTinNhanVien nhanVien)
        {
            var exists = (from d in quyetDinh.ListChiTietQuyetDinhCongNhanDaoTao
                          where d.ThongTinNhanVien.Oid == nhanVien.Oid
                          select d).SingleOrDefault();
            return exists != null;
        }

        //public static bool IsExists(DuyetDangKyDaoTao duyetDK, DangKyDaoTao dangkydao)
        //{
        //    var exists = (from d in duyetDK
        //                  where d.DangKyDaoTao == dangkydao.Oid
        //                  select d).SingleOrDefault();
        //    return exists != null;
        //}
    }
}
