using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Module.NghiepVu.NhanSu.Helper
{
    public class TuyenDungHelper
    {
        private static TinhTrang GetTinhTrang(IObjectSpace obs)
        {
            TinhTrang obj = obs.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
            if (obj == null)
            {
                obj = obs.CreateObject<TinhTrang>();
                obj.MaQuanLy = "01";
                obj.TenTinhTrang = "Đang làm việc";
            }
            return obj;
        }

        /// <summary>
        /// Create nhan vien from ung vien
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="item"></param>
        /// <param name="tinhTrang"></param>
        /// <returns></returns>
        public static ThongTinNhanVien HoSoNhanVien(IObjectSpace obs, TrungTuyen item)
        {
            ThongTinNhanVien nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("CMND=?", item.UngVien.CMND.Trim()));
            if (nhanVien != null && nhanVien.CongTy.Oid != item.UngVien.NhuCauTuyenDung.ViTriTuyenDung.BoPhan.CongTy.Oid)
                item.DaTaoHoSo = false;
            else
                item.DaTaoHoSo = true;

            if (nhanVien == null || item.DaTaoHoSo == false)
            {
                nhanVien = obs.CreateObject<ThongTinNhanVien>();
                if (item.NhuCauTuyenDung.BoPhan != null)
                    nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(item.NhuCauTuyenDung.BoPhan.Oid);
                if (item.NhuCauTuyenDung.ViTriTuyenDung.ChucDanh != null)
                    nhanVien.ChucDanh = obs.GetObjectByKey<ChucDanh>(item.NhuCauTuyenDung.ViTriTuyenDung.ChucDanh.Oid);
                if (item.NhuCauTuyenDung.ViTriTuyenDung.ChucVu != null)
                    nhanVien.ChucVu = obs.GetObjectByKey<ChucVu>(item.NhuCauTuyenDung.ViTriTuyenDung.ChucVu.Oid);
                if (item.NhuCauTuyenDung.ViTriTuyenDung != null)
                    nhanVien.CongViecHienNay = obs.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec like ?", "%" + item.NhuCauTuyenDung.ViTriTuyenDung.TenViTriTuyenDung + "%"));
                nhanVien.Ho = item.UngVien.Ho;
                nhanVien.Ten = item.UngVien.Ten;
                nhanVien.GioiTinh = item.UngVien.GioiTinh;
                nhanVien.NgaySinh = item.UngVien.NgaySinh;
                if (item.UngVien.NoiSinh != null)
                    nhanVien.NoiSinh = obs.GetObjectByKey<DiaChi>(item.UngVien.NoiSinh.Oid);
                nhanVien.LoaiHopDong = obs.FindObject<LoaiHopDong>(CriteriaOperator.Parse("TenLoaiHopDong like ?", "%thử việc%"));
                if (item.UngVien.QueQuan != null)
                    nhanVien.QueQuan = obs.GetObjectByKey<DiaChi>(item.UngVien.QueQuan.Oid);
                if (item.UngVien.DiaChiThuongTru != null)
                    nhanVien.DiaChiThuongTru = obs.GetObjectByKey<DiaChi>(item.UngVien.DiaChiThuongTru.Oid);
                if (item.UngVien.NoiOHienNay != null)
                    nhanVien.NoiOHienNay = Common.Copy<DiaChi>(((XPObjectSpace)obs).Session, item.UngVien.NoiOHienNay);
                nhanVien.CMND = item.UngVien.CMND;
                nhanVien.NgayCap = item.UngVien.NgayCap;
                if (item.UngVien.NoiCap != null)
                    nhanVien.NoiCap = obs.GetObjectByKey<TinhThanh>(item.UngVien.NoiCap.Oid);
                nhanVien.DienThoaiNhaRieng = item.UngVien.DienThoaiNhaRieng;
                nhanVien.DienThoaiDiDong = item.UngVien.DienThoaiDiDong;
                nhanVien.Email = item.UngVien.Email;
                nhanVien.TinhTrang = GetTinhTrang(obs);
                nhanVien.HinhThucTuyenDung = item.UngVien.HinhThucTuyenDung;
                nhanVien.LoaiNhanSu = obs.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu like ?", "%Nhân viên%"));
                nhanVien.DonViTuyenDung = item.UngVien.QuanLyTuyenDung.CongTy.TenBoPhan;
                if (item.UngVien.TinhTrangHonNhan != null)
                    nhanVien.TinhTrangHonNhan = obs.GetObjectByKey<TinhTrangHonNhan>(item.UngVien.TinhTrangHonNhan.Oid);
                nhanVien.GhiChu = item.UngVien.GhiChu;
                if (item.UngVien.DanToc != null)
                    nhanVien.DanToc = obs.GetObjectByKey<DanToc>(item.UngVien.DanToc.Oid);
                if (item.UngVien.TonGiao != null)
                    nhanVien.TonGiao = obs.GetObjectByKey<TonGiao>(item.UngVien.TonGiao.Oid);
                if (item.UngVien.TrinhDoChuyenMon != null)
                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = obs.GetObjectByKey<TrinhDoChuyenMon>(item.UngVien.TrinhDoChuyenMon.Oid);
                if (item.UngVien.ChuyenNganhDaoTao != null)
                    nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = obs.GetObjectByKey<ChuyenNganhDaoTao>(item.UngVien.ChuyenNganhDaoTao.Oid);
                if (item.UngVien.TruongDaoTao != null)
                    nhanVien.NhanVienTrinhDo.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(item.UngVien.TruongDaoTao.Oid);
                if (item.UngVien.HinhThucDaoTao != null)
                    nhanVien.NhanVienTrinhDo.HinhThucDaoTao = obs.GetObjectByKey<HinhThucDaoTao>(item.UngVien.HinhThucDaoTao.Oid);
                nhanVien.NhanVienTrinhDo.NamTotNghiep = item.UngVien.NamTotNghiep;
                //if (item.UngVien.ChuongTrinhHoc != null)
                //    nhanVien.NhanVienTrinhDo.ChuongTrinhHoc = obs.GetObjectByKey<ChuongTrinhHoc>(item.UngVien.ChuongTrinhHoc.Oid);
                if (item.UngVien.TrinhDoTinHoc != null)
                    nhanVien.NhanVienTrinhDo.TrinhDoTinHoc = obs.GetObjectByKey<TrinhDoTinHoc>(item.UngVien.TrinhDoTinHoc.Oid);
                if (item.UngVien.NgoaiNgu != null)
                    nhanVien.NhanVienTrinhDo.NgoaiNgu = obs.GetObjectByKey<NgoaiNgu>(item.UngVien.NgoaiNgu.Oid);
                if (item.UngVien.TrinhDoNgoaiNgu != null)
                    nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = obs.GetObjectByKey<TrinhDoNgoaiNgu>(item.UngVien.TrinhDoNgoaiNgu.Oid);
                if (item.UngVien.TrinhDoVanHoa != null)
                    nhanVien.NhanVienTrinhDo.TrinhDoVanHoa = obs.GetObjectByKey<TrinhDoVanHoa>(item.UngVien.TrinhDoVanHoa.Oid);
                nhanVien.NgayVaoCongTy = item.NgayNhanViec;
                nhanVien.ThamNienLamViec = item.NgayNhanViec;
                // Thông tin lương nhân viên
                if (item.NgachLuong != null)
                    nhanVien.NhanVienThongTinLuong.NgachLuong = obs.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                if (item.BacLuong != null)
                    nhanVien.NhanVienThongTinLuong.BacLuong = obs.GetObjectByKey<BacLuong>(item.BacLuong.Oid); 
                nhanVien.NhanVienThongTinLuong.LuongCoBan = item.LuongCoBan;
                nhanVien.NhanVienThongTinLuong.LuongKinhDoanh = item.LuongKinhDoanh;
                nhanVien.NhanVienThongTinLuong.NgayHuongLuong = item.NgayNhanViec;
                nhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = item.NgayBoNhiemNgach;
                nhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = item.PhanTramTinhLuong;

                //copy lịch sử bản thân
                CopyLichSuBanThan(obs, item.UngVien, nhanVien);
                //copy quá trình công tác
                CopyQuaTrinhCongTac(obs, item.UngVien, nhanVien);
                //copy ngoại ngữ
                CopyTrinhDoNgoaiNguKhac(obs, item.UngVien, nhanVien);
                //copy chứng chỉ
                CopyChungChi(obs, item.UngVien, nhanVien);
                //copy bằng cấp
                CopyVanBang(obs, item.UngVien, nhanVien);
            }              
            
            return nhanVien;
        }

        /// <summary>
        /// Trúng tuyển
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="qlTuyenDung"></param>
        //public static void TrungTuyen(IObjectSpace obs, QuanLyTuyenDung qlTuyenDung)
        //{
        //    if (qlTuyenDung != null)
        //    {
        //        XPCollection<NhuCauTuyenDung> lstNhuCau = new XPCollection<NhuCauTuyenDung>(((XPObjectSpace)obs).Session);
        //        lstNhuCau.Criteria = CriteriaOperator.Parse("QuanLyTuyenDung=?", qlTuyenDung.Oid);

        //        foreach (NhuCauTuyenDung itemNhuCau in lstNhuCau)
        //        {
        //            TrungTuyen trungTuyen;
        //            object obj = ((XPObjectSpace)obs).Session.Evaluate<BuocTuyenDung>(CriteriaOperator.Parse("Max(ThuTu)"), CriteriaOperator.Parse("ChiTietTuyenDung.QuanLyTuyenDung=? and ChiTietTuyenDung.NhuCauTuyenDung=?", qlTuyenDung.Oid, itemNhuCau.Oid));
        //            if (obj != null)
        //            {
        //                CriteriaOperator filter;
        //                SortProperty sort = new SortProperty("Diem", DevExpress.Xpo.DB.SortingDirection.Descending);
        //                XPCollection<ChiTietVongTuyenDung> ctVongTuyenDung;
        //                int soLuong;

        //                foreach (NhuCauTuyenDung item in qlTuyenDung.ListNhuCauTuyenDung)
        //                {
        //                    filter = CriteriaOperator.Parse("VongTuyenDung.BuocTuyenDung.ThuTu=? and VongTuyenDung.ChiTietTuyenDung.QuanLyTuyenDung=? and UngVien.NhuCauTuyenDung=? and DuocChuyenQuaVongSau",
        //                       obj, qlTuyenDung.Oid, item.Oid);
        //                    ctVongTuyenDung = new XPCollection<ChiTietVongTuyenDung>(((XPObjectSpace)obs).Session, filter, sort);

        //                    soLuong = 0;
        //                    foreach (ChiTietVongTuyenDung ctItem in ctVongTuyenDung)
        //                    {
        //                        trungTuyen = obs.FindObject<TrungTuyen>(CriteriaOperator.Parse("QuanLyTuyenDung=? and UngVien=?",
        //                            qlTuyenDung.Oid, ctItem.UngVien.Oid));
        //                        if (trungTuyen == null)
        //                        {
        //                            trungTuyen = obs.CreateObject<TrungTuyen>();
        //                            trungTuyen.QuanLyTuyenDung = qlTuyenDung;
        //                            trungTuyen.UngVien = ctItem.UngVien;
        //                        }
        //                        soLuong++;

        //                        //lấy từ trên xuống dưới, đủ số lượng thì dừng
        //                        if (soLuong > item.SoLuongTuyen)
        //                            break;
        //                    }
        //                }
        //                obs.CommitChanges();
        //            }
        //        }
        //    }
        //}

        public static void TrungTuyen(IObjectSpace obs, QuanLyTuyenDung qlTuyenDung)
        {
            if (qlTuyenDung != null)
            {
                TrungTuyen trungTuyen;
                CriteriaOperator filter;
                SortProperty sort = new SortProperty("Diem", DevExpress.Xpo.DB.SortingDirection.Descending);
                XPCollection<ChiTietVongTuyenDung> ctVongTuyenDung;
                int soLuong;

                foreach (NhuCauTuyenDung item in qlTuyenDung.ListNhuCauTuyenDung)
                {
                    object obj = ((XPObjectSpace)obs).Session.Evaluate<BuocTuyenDung>(CriteriaOperator.Parse("Max(ThuTu)"), CriteriaOperator.Parse("ChiTietTuyenDung.QuanLyTuyenDung=? and ChiTietTuyenDung.NhuCauTuyenDung=?", qlTuyenDung.Oid, item.Oid));
                    if (obj != null)
                    {
                        filter = CriteriaOperator.Parse("VongTuyenDung.BuocTuyenDung.ThuTu=? and VongTuyenDung.ChiTietTuyenDung.QuanLyTuyenDung=? and UngVien.NhuCauTuyenDung=? and DuocChuyenQuaVongSau",
                               obj, qlTuyenDung.Oid, item.Oid);
                        ctVongTuyenDung = new XPCollection<ChiTietVongTuyenDung>(((XPObjectSpace)obs).Session, filter, sort);

                        soLuong = 0;
                        foreach (ChiTietVongTuyenDung ctItem in ctVongTuyenDung)
                        {
                            trungTuyen = obs.FindObject<TrungTuyen>(CriteriaOperator.Parse("QuanLyTuyenDung=? and UngVien=?", qlTuyenDung.Oid, ctItem.UngVien.Oid));
                            if (trungTuyen == null)
                            {
                                trungTuyen = obs.CreateObject<TrungTuyen>();
                                trungTuyen.QuanLyTuyenDung = qlTuyenDung;
                                trungTuyen.UngVien = ctItem.UngVien;                               
                            }
                            soLuong++;

                            //lấy từ trên xuống dưới, đủ số lượng thì dừng
                            if (soLuong > item.SoLuongTuyen)
                                break;
                        }
                        obs.CommitChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Không trúng tuyển
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="qlTuyenDung"></param>
        public static void KhongTrungTuyen(IObjectSpace obs, QuanLyTuyenDung qlTuyenDung)
        {
            if (qlTuyenDung != null)
            {
                KhongTrungTuyen khongTrungTuyen;
                CriteriaOperator filter;
                //SortProperty sort = new SortProperty("Diem", DevExpress.Xpo.DB.SortingDirection.Descending);
                XPCollection<ChiTietVongTuyenDung> ctVongTuyenDung;

                foreach (NhuCauTuyenDung item in qlTuyenDung.ListNhuCauTuyenDung)
                {
                    //object obj = ((XPObjectSpace)obs).Session.Evaluate<BuocTuyenDung>(CriteriaOperator.Parse("Max(ThuTu)"), CriteriaOperator.Parse("ChiTietTuyenDung.QuanLyTuyenDung=? and ChiTietTuyenDung.NhuCauTuyenDung=?", qlTuyenDung.Oid, item.Oid));
                    //if (obj != null)
                    //{
                        filter = CriteriaOperator.Parse("VongTuyenDung.ChiTietTuyenDung.QuanLyTuyenDung=? and UngVien.NhuCauTuyenDung=? and !DuocChuyenQuaVongSau", qlTuyenDung.Oid, item.Oid);
                        ctVongTuyenDung = new XPCollection<ChiTietVongTuyenDung>(((XPObjectSpace)obs).Session, filter);

                        foreach (ChiTietVongTuyenDung ctItem in ctVongTuyenDung)
                        {
                            khongTrungTuyen = obs.FindObject<KhongTrungTuyen>(CriteriaOperator.Parse("QuanLyTuyenDung=? and UngVien=?", qlTuyenDung.Oid, ctItem.UngVien.Oid));
                            if (khongTrungTuyen == null)
                            {
                                khongTrungTuyen = obs.CreateObject<KhongTrungTuyen>();
                                khongTrungTuyen.QuanLyTuyenDung = qlTuyenDung;
                                khongTrungTuyen.UngVien = ctItem.UngVien;
                            }
                        }
                        obs.CommitChanges();
                    //}
                }
            }
        }

        /// <summary>
        /// Create buoc tuyen dung
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="chiTiet"></param>
        /// <param name="thuTu"></param>
        /// <param name="tenBuocTuyenDung"></param>
        /// <param name="thangDiem"></param>
        public static void CreateBuocTuyenDung(IObjectSpace obs, ChiTietTuyenDung chiTiet, int thuTu, string tenBuocTuyenDung, int thangDiem)
        {
            BuocTuyenDung buocTuyenDung = obs.FindObject<BuocTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and TenBuocTuyenDung like ?", chiTiet.Oid, tenBuocTuyenDung));
            if (buocTuyenDung == null)
            {
                buocTuyenDung = obs.CreateObject<BuocTuyenDung>();
                buocTuyenDung.ThuTu = thuTu;
                buocTuyenDung.TenBuocTuyenDung = tenBuocTuyenDung;
                buocTuyenDung.ThangDiem = thangDiem;
                if (tenBuocTuyenDung.ToLower().Contains("thi"))
                    buocTuyenDung.CoToChucThiTuyen = true;
                chiTiet.ListBuocTuyenDung.Add(buocTuyenDung);
            }
        }

        public static void CreateViTriTuyenDung(Session session, QuanLyTuyenDung quanLy, string maQuanLy, string tenViTriTuyendung, LoaiTuyenDung loaiTuyenDung)
        {
            ViTriTuyenDung viTri = session.FindObject<ViTriTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=? and TenViTriTuyenDung like ? and LoaiTuyenDung=?", quanLy.Oid, tenViTriTuyendung, loaiTuyenDung.Oid));
            if (viTri == null)
            {
                viTri = new ViTriTuyenDung(session);
                viTri.QuanLyTuyenDung = quanLy;
                viTri.MaQuanLy = maQuanLy;
                viTri.TenViTriTuyenDung = tenViTriTuyendung;
                viTri.LoaiTuyenDung = loaiTuyenDung;
            }
        }

        /// <summary>
        /// copy lich su ban than
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="ungVien"></param>
        /// <param name="hoSo"></param>
        private static void CopyLichSuBanThan(IObjectSpace obs, UngVien ungVien, ThongTinNhanVien thongTinNhanVien)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ungVien.Oid);
            using (XPCollection<LichSuBanThan> lichSuBanThan = new XPCollection<LichSuBanThan>(((XPObjectSpace)obs).Session, filter))
            {
                LichSuBanThan obj;
                foreach (LichSuBanThan lsItem in lichSuBanThan)
                {
                    obj = obs.CreateObject<LichSuBanThan>();
                    obj.ThongTinNhanVien = thongTinNhanVien;
                    obj.CongTy = lsItem.CongTy;
                    obj.ChucDanh = lsItem.ChucDanh;
                    obj.TuNam = lsItem.TuNam;
                    obj.DenNam = lsItem.DenNam;
                    obj.LyDoNghiViec = lsItem.LyDoNghiViec;
                    obj.GhiChu = lsItem.GhiChu;
                }
            }
        }

        private static void CopyQuaTrinhCongTac(IObjectSpace obs, UngVien ungVien, ThongTinNhanVien thongTinNhanVien)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ungVien.Oid);
            using (XPCollection<QuaTrinhCongTac> data = new XPCollection<QuaTrinhCongTac>(((XPObjectSpace)obs).Session, filter))
            {
                QuaTrinhCongTac obj;
                foreach (QuaTrinhCongTac item in data)
                {
                    obj = obs.CreateObject<QuaTrinhCongTac>();
                    obj.ThongTinNhanVien = thongTinNhanVien;
                    obj.TuNam = item.TuNam;
                    obj.DenNam = item.DenNam;
                    obj.NoiDung = item.NoiDung;
                }
            }
        }

        private static void CopyTrinhDoNgoaiNguKhac(IObjectSpace obs, UngVien ungVien, HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            TrinhDoNgoaiNguKhac obj;
            foreach (TrinhDoNgoaiNguKhac item in ungVien.ListNgoaiNgu)
            {
                obj = obs.CreateObject<TrinhDoNgoaiNguKhac>();
                obj.HoSo = hoSo;
                if (item.NgoaiNgu != null)
                    obj.NgoaiNgu = obs.GetObjectByKey<NgoaiNgu>(item.NgoaiNgu.Oid);
                if (item.TrinhDoNgoaiNgu != null)
                    obj.TrinhDoNgoaiNgu = obs.GetObjectByKey<TrinhDoNgoaiNgu>(item.TrinhDoNgoaiNgu.Oid);
                obj.Diem = item.Diem;
            }
        }

        private static void CopyChungChi(IObjectSpace obs, UngVien ungVien, HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            ChungChi obj;
            foreach (ChungChi item in ungVien.ListChungChi)
            {
                obj = obs.CreateObject<ChungChi>();
                obj.HoSo = hoSo;
                if (item.LoaiChungChi != null)
                    obj.LoaiChungChi = obs.GetObjectByKey<LoaiChungChi>(item.LoaiChungChi.Oid);
                obj.TenChungChi = item.TenChungChi;
                obj.NoiCap = item.NoiCap;
                obj.NgayCap = item.NgayCap;
                obj.XepLoai = item.XepLoai;
                obj.Diem = item.Diem;
            }
        }

        private static void CopyVanBang(IObjectSpace obs, UngVien ungVien, HoSo hoSo)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=?", ungVien.Oid);
            VanBang obj;
            foreach (VanBang item in ungVien.ListVanBang)
            {
                obj = obs.CreateObject<VanBang>();
                obj.HoSo = hoSo;
                if (item.TrinhDoChuyenMon != null)
                    obj.TrinhDoChuyenMon = obs.GetObjectByKey<TrinhDoChuyenMon>(item.TrinhDoChuyenMon.Oid);
                if (item.ChuyenNganhDaoTao != null)
                    obj.ChuyenNganhDaoTao = obs.GetObjectByKey<ChuyenNganhDaoTao>(item.ChuyenNganhDaoTao.Oid);
                if (item.TruongDaoTao != null)
                    obj.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(item.TruongDaoTao.Oid);
                if (item.HinhThucDaoTao != null)
                    obj.HinhThucDaoTao = obs.GetObjectByKey<HinhThucDaoTao>(item.HinhThucDaoTao.Oid);
                obj.NamTotNghiep = item.NamTotNghiep;
                obj.XepLoai = item.XepLoai;
                obj.DiemTrungBinh = item.DiemTrungBinh;
            }
        }
    }
}
