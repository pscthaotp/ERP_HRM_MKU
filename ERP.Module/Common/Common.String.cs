using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.CauHinhChungs;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Enum.NhanSu;
using ERP.Module.HeThong;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.Commons
{
    public  static partial class Common
    {

        /// <summary>
        /// Lấy quyền xem bộ phận of User
        /// </summary>
        /// <returns></returns>
        public static string System_GetDeparment_Role_ByUser()
        {
            string quyenDonViOfCurrentUser = string.Empty;
            //
            if (SecuritySystem.CurrentUser != null && (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department != null)
                quyenDonViOfCurrentUser = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department.Quyen;
            return quyenDonViOfCurrentUser;
        }

        /// <summary>
        /// Lấy quyền xem lớp of User
        /// </summary>
        /// <returns></returns>
        public static string System_GetClass_Role_ByUser()
        {
            string quyenLopOfCurrentUser = string.Empty;
            //
            if (SecuritySystem.CurrentUser != null && (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Class != null)
                quyenLopOfCurrentUser = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Class.Quyen;
            return quyenLopOfCurrentUser;
        }


        /// <summary>
        /// Lấy quyền xem báo cáo of User
        /// </summary>
        /// <returns></returns>
        public static string System_Report_Role_ByUser()
        {
            string quyenBaoCaoOfCurrentUser = string.Empty;
            //
            if (SecuritySystem.CurrentUser != null && (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Report != null)
                quyenBaoCaoOfCurrentUser = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Report.Quyen;
            return quyenBaoCaoOfCurrentUser;
        }

        /// <summary>
        /// Lấy danh sách đơn vị được phân quyền by bộ phận cha
        /// </summary>
        public static List<string> Department_GetRoledDepartmentList_ByDepartment(BoPhan boPhan)
        {
            List<string> danhSachBoPhanDuocPhanQuyenList = new List<string>();
            List<string> resultList = new List<string>();
            List<string> resultBoPhanConList = new List<string>();

            if (boPhan != null)
            {
                string[] split = null;
                if (SecuritySystem.CurrentUser != null)
                {
                    SecuritySystemRole_Department permission = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department;
                    if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                    {
                        //
                        split = permission.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (split.Length > 0)
                            danhSachBoPhanDuocPhanQuyenList.AddRange(split);
                    }
                }
                //Lấy danh sách kết quả
                resultList = Department_GetChildDepartmentList_ByData(boPhan, danhSachBoPhanDuocPhanQuyenList, resultBoPhanConList);
                //
            }
            else
            {
                resultList = Department_GetRoledDepartmentList_ByCurrentUser();
            }

            return resultList;
        }

        /// <summary>
        ///  Lấy danh sách đơn vị được phân quyền by người dùng
        /// </summary>
        public static List<string> Department_GetRoledDepartmentList_ByCurrentUser()
        {
            List<string> resultList = new List<string>();
            //
            string[] split = null;
            if (SecuritySystem.CurrentUser != null)
            {
                SecuritySystemRole_Department permission = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department;
                if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                {
                    //
                    split = permission.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length > 0)
                        resultList.AddRange(split);
                }
            }
            //
            return resultList;
        }

        /// <summary>
        ///  Lấy danh sách Lớp được phân quyền by người dùng
        /// </summary>
        public static List<string> Class_GetRoledClassList_ByCurrentUser()
        {
            List<string> resultList = new List<string>();
            //
            string[] split = null;
            if (SecuritySystem.CurrentUser != null)
            {
                SecuritySystemRole_Class permission = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Class;
                if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                {
                    //
                    split = permission.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length > 0)
                        resultList.AddRange(split);
                }
            }
            //
            return resultList;
        }

        /// <summary>
        /// Lấy danh sách id của Đơn vị theo phân quyền của user hiện tại
        /// </summary>
        public static List<string> Department_GetRoledDepartmentList_BySession(Session session)
        {
            List<string> resultList = new List<string>();
            //
            string[] split = null;
            if (SecuritySystem.CurrentUser != null && !QuanTriToanHeThong())
            {
                SecuritySystemRole_Department permission = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department;
                if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                {
                    //
                    split = permission.Quyen.ToLower().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length > 0)
                        resultList.AddRange(split);
                }
            }
            else
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("NgungHoatDong=false");
                XPCollection<BoPhan> boPhanList = new XPCollection<BoPhan>(session, criteria);
                foreach (BoPhan item in boPhanList)
                {
                    resultList.Add(item.Oid.ToString().ToLower());
                }
            }
            //
            return resultList;
        }

        /// <summary>
        /// Lấy danh sách bộ phận con
        /// </summary>
        private static List<string> Department_GetChildDepartmentList_ByData(BoPhan bp, List<string> danhSachBoPhanDuocPhanQuyenList, List<string> resultBoPhanConList)
        {

            if (bp.NgungHoatDong == false && !resultBoPhanConList.Contains(bp.Oid.ToString()))
                resultBoPhanConList.Add(bp.Oid.ToString());
            //
            if (QuanTriToanHeThong())
            {
                //tìm Đơn vị con đưa vào danh sách
                foreach (BoPhan item in bp.ListBoPhanCon)
                {
                    resultBoPhanConList.Add(item.Oid.ToString());
                    //
                    if (item.NgungHoatDong == false && item.ListBoPhanCon.Count > 0)
                        Department_GetChildDepartmentList_ByData(item, danhSachBoPhanDuocPhanQuyenList, resultBoPhanConList);
                }
            }
            else
            {
                //tìm Đơn vị con đưa vào danh sách
                foreach (BoPhan item in bp.ListBoPhanCon)
                {
                    foreach (string boPhanPhanQuyen in danhSachBoPhanDuocPhanQuyenList)
                    {
                        if (item.Oid.ToString().Trim() == boPhanPhanQuyen.Trim())
                            resultBoPhanConList.Add(item.Oid.ToString());
                    }
                    //
                    if (item.ListBoPhanCon.Count > 0)
                        Department_GetChildDepartmentList_ByData(item, danhSachBoPhanDuocPhanQuyenList, resultBoPhanConList);
                }
            }

            return resultBoPhanConList;
        }

        /// <summary>
        /// Lấy danh sách bộ phận con
        /// </summary>
        private static List<string> Department_GetChildDepartmentList_ByCompanyData(BoPhan bp, List<string> danhSachBoPhanDuocPhanQuyenList, List<string> resultBoPhanConList)
        {

            if (bp.NgungHoatDong == false && !resultBoPhanConList.Contains(bp.Oid.ToString()))
                resultBoPhanConList.Add(bp.Oid.ToString());
           
            //tìm Đơn vị con đưa vào danh sách
            foreach (BoPhan item in bp.ListBoPhanCon)
            {
                foreach (string boPhanPhanQuyen in danhSachBoPhanDuocPhanQuyenList)
                {
                    if (item.Oid.ToString().Trim() == boPhanPhanQuyen.Trim())
                        resultBoPhanConList.Add(item.Oid.ToString());
                }
                //
                if (item.ListBoPhanCon.Count > 0 && item.CongTy.Oid == bp.CongTy.Oid)
                    Department_GetChildDepartmentList_ByCompanyData(item, danhSachBoPhanDuocPhanQuyenList, resultBoPhanConList);
            }
           
            return resultBoPhanConList;
        }

        /// <summary>
        /// Lấy danh sách bộ phận con
        /// </summary>
        public static List<string> Department_GetChildDepartmentList_ByCompany(BoPhan boPhan)
        {

            List<string> danhSachBoPhanDuocPhanQuyenList = new List<string>();
            List<string> resultList = new List<string>();
            List<string> resultBoPhanConList = new List<string>();

            if (boPhan != null)
            {
                string[] split = null;
                if (SecuritySystem.CurrentUser != null)
                {
                    SecuritySystemRole_Department permission = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department;
                    if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                    {
                        //
                        split = permission.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (split.Length > 0)
                            danhSachBoPhanDuocPhanQuyenList.AddRange(split);
                    }
                }
                //Lấy danh sách kết quả
                resultList = Department_GetChildDepartmentList_ByCompanyData(boPhan, danhSachBoPhanDuocPhanQuyenList, resultBoPhanConList);
                //
            }
            else
            {
                resultList = Department_GetRoledDepartmentList_ByCurrentUser();
            }

            return resultList;
        }

        /// <summary>
        /// Đọc số tiền
        /// </summary>
        /// <param name="NumCurrency">số tiền</param>
        /// <returns></returns>
        public static string DocTien(decimal NumCurrency)
        {
            string SoRaChu = "";
            NumCurrency = Math.Abs(NumCurrency);
            if (NumCurrency == 0)
            {
                SoRaChu = "Không đồng";
                return SoRaChu;
            }

            string[] CharVND = new string[10] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string BangChu;
            int I;
            //As String, BangChu As String, I As Integer
            int SoLe, SoDoi;
            string PhanChan, Ten;
            string DonViTien, DonViLe;
            int NganTy, Ty, Trieu, Ngan;
            int Dong, Tram, Muoi, DonVi;

            SoDoi = 0;
            Muoi = 0;
            Tram = 0;
            DonVi = 0;

            Ten = "";
            DonViTien = "đồng";
            DonViLe = "xu";


            SoLe = (int)((NumCurrency - (Int64)NumCurrency) * 100); //'2 kí so^' le?
            PhanChan = ((Int64)NumCurrency).ToString().Trim();

            int khoangtrang = 15 - PhanChan.Length;
            for (int i = 0; i < khoangtrang; i++)
                PhanChan = "0" + PhanChan;

            NganTy = int.Parse(PhanChan.Substring(0, 3));
            Ty = int.Parse(PhanChan.Substring(3, 3));
            Trieu = int.Parse(PhanChan.Substring(6, 3));
            Ngan = int.Parse(PhanChan.Substring(9, 3));
            Dong = int.Parse(PhanChan.Substring(12, 3));

            if (NganTy == 0 & Ty == 0 & Trieu == 0 & Ngan == 0 & Dong == 0)
            {
                BangChu = String.Format("không {0} ", DonViTien);
                I = 5;
            }
            else
            {
                BangChu = "";
                I = 0;
            }

            while (I <= 5)
            {
                switch (I)
                {
                    case 0:
                        SoDoi = NganTy;
                        Ten = "ngàn tỷ";
                        break;
                    case 1:
                        SoDoi = Ty;
                        Ten = "tỷ";
                        break;
                    case 2:
                        SoDoi = Trieu;
                        Ten = "triệu";
                        break;
                    case 3:
                        SoDoi = Ngan;
                        Ten = "ngàn";
                        break;
                    case 4:
                        SoDoi = Dong;
                        Ten = DonViTien;
                        break;
                    case 5:
                        SoDoi = SoLe;
                        Ten = DonViLe;
                        break;
                }

                if (SoDoi != 0)
                {
                    Tram = (int)(SoDoi / 100);
                    Muoi = (int)((SoDoi - Tram * 100) / 10);
                    DonVi = (SoDoi - Tram * 100) - Muoi * 10;
                    BangChu = BangChu.Trim() + (BangChu.Length == 0 ? "" : " ") + (Tram != 0 ? CharVND[Tram].Trim() + " trăm " : "");
                    if (Muoi == 0 & Tram == 0 & DonVi != 0 & BangChu != "")
                        BangChu = BangChu + "không trăm lẻ ";
                    else if (Muoi != 0 & Tram == 0 & (DonVi == 0 || DonVi != 0) & BangChu != "")
                        BangChu = BangChu + "không trăm ";
                    else if (Muoi == 0 & Tram != 0 & DonVi != 0 & BangChu != "")
                        BangChu = BangChu + "lẻ ";
                    if (Muoi != 0 & Muoi > 0)
                        BangChu = BangChu + ((Muoi != 0 & Muoi != 1) ? CharVND[Muoi].Trim() + " mươi " : "mười ");

                    if (Muoi != 0 & DonVi == 5)
                        BangChu = String.Format("{0}lăm {1} ", BangChu, Ten);
                    else if (Muoi > 1 & DonVi == 1)
                        BangChu = String.Format("{0}mốt {1} ", BangChu, Ten);
                    else
                        BangChu = BangChu + ((DonVi != 0) ? String.Format("{0} {1} ", CharVND[DonVi].Trim(), Ten) : Ten + " ");
                }
                else
                    BangChu = BangChu + ((I == 4) ? DonViTien + " " : "");

                I = I + 1;
            }

            BangChu = BangChu[0].ToString().ToUpper() + BangChu.Substring(1);
            SoRaChu = BangChu;
            return SoRaChu;
        }

        public static string DocSo(string number)
        {
            if (number.Contains("."))
                number = number.Replace(".", "");

            Regex regex = new Regex(@"^\d+(\,?\d+)?$");
            if (regex.IsMatch(number))
            {
                string[] split = number.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length == 2)
                    return String.Format("{0}phẩy {1}", ChuyenSo(split[0]), ChuyenSo(split[1]));
                else
                    return ChuyenSo(number);
            }
            return "";
        }

        private static string ChuyenSo(string number)
        {
            string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỷ" };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string doc;
            int i, j, k, n, len, found, ddv;
            bool state = false;

            len = number.Length;
            number += "ss";
            doc = "";
            found = 0;
            ddv = 0;

            i = 0;
            while (i < len)
            {
                //So chu so o hang dang duyet
                n = (len - i + 2) % 3 + 1;

                //Kiem tra so 0
                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }

                //Duyet n chu so
                if (found == 1)
                {
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3)
                                    doc += RemoveEmptyString(cs[0]);
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0')
                                        doc += "lẻ ";
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                if (n - j == 3)
                                    doc += cs[1] + " ";
                                if (n - j == 2)
                                {
                                    doc += "mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0)
                                        k = 0;
                                    else
                                        k = i + j - 1;

                                    if (number[k] != '1' && number[k] != '0')
                                        doc += "mốt ";
                                    else
                                        doc += RemoveEmptyString(cs[1]);
                                }
                                break;
                            case '5':
                                if (j == n - 1)
                                    doc += "lăm ";
                                else
                                    doc += RemoveEmptyString(cs[5]);
                                break;
                            default:
                                doc += RemoveEmptyString(cs[(int)number[i + j] - 48]);
                                break;
                        }

                        //Doc don vi nho
                        if (ddv == 1)
                        {
                            doc += RemoveEmptyString(dv[n - j - 1]);
                        }
                    }
                }


                //Doc don vi lon
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (found != 0)
                        {
                            state = true;
                            for (k = 0; k < (len - i - n) / 9; k++)
                            {
                                doc += "tỷ ";
                            }
                        }
                        else
                        {
                            if (!state)
                                doc += "tỷ ";
                        }
                    }
                    else
                        if (found != 0)
                            doc += RemoveEmptyString(dv[((len - i - n + 1) % 9) / 3 + 2]);
                }

                i += n;
            }

            if (len == 1)
                if (number[0] == '0' || number[0] == '5')
                    return cs[(int)number[0] - 48] + " ";

            return doc;
        }


        private static string RemoveEmptyString(string input)
        {
            if (!string.IsNullOrEmpty(input))
                return input + " ";
            return "";
        }


        /// <summary>
        /// Lấy thông tin nơi làm việc cho quá trình BHXH
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static string NoiLamViec(Session session, ThongTinNhanVien nhanVien, BoPhan boPhan)
        {
            CongTy congTy = CongTy(session);
            string chucDanh = String.Format("{0} {1}, ", (nhanVien.ChucVu != null ? nhanVien.ChucVu.TenChucVu : "Nhân viên"), boPhan.TenBoPhan);
            string luong = "";
            if (nhanVien.NhanVienThongTinLuong.BacLuong == null)
                luong = String.Format("mức lương {0:N0}, ", nhanVien.NhanVienThongTinLuong.LuongCoBan);
            else if (nhanVien.NhanVienThongTinLuong.NgachLuong != null &&
                nhanVien.NhanVienThongTinLuong.NgachLuong.TotKhung != null &&
                nhanVien.NhanVienThongTinLuong.BacLuong != null)
                luong = String.Format("bậc {0}/{1} {2}, ", nhanVien.NhanVienThongTinLuong.BacLuong.MaQuanLy,
                    nhanVien.NhanVienThongTinLuong.NgachLuong.TotKhung.MaQuanLy,
                    nhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy);
            string coQuan = "";
            if (congTy != null && congTy.DiaChi != null)
                coQuan = String.Format("{0}, {1}", congTy.TenBoPhan, congTy.DiaChi.FullDiaChi);

            string result = String.Format("{0}{1}{2}", chucDanh, luong, coQuan);

            return result;
        }
    }
}
