using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.Collections.Generic;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.TienLuong.NgoaiGio;
//
namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ExecuteClass
{
    public static class Luong_TinhLuongNgoaiGio
    {
        public static string XuLy(IObjectSpace obs, BangLuongNgoaiGio obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList)
        {
            string message = string.Empty;
            //
            if (obj != null)
            {

                #region 1. Xóa dữ liệu cũ
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@BangLuongNgoaiGio", obj.Oid);
                int sucessDelete = DataProvider.ExecuteNonQuery("spd_Luong_XoaChiTietLuongNgoaiGio", CommandType.StoredProcedure, param);
                //
                if (sucessDelete == -1)
                {
                    message = "Không thể xóa chi tiết lương đã tính. Toàn vẹn dữ liệu !!!";
                    return message;
                }
                #endregion

                #region 2. Tạo phân quyền bộ phận
                int sucessDeparment_Role = Common.CreateDeparment_Role_ByCurrentUser(((XPObjectSpace)obs).Session);
                if (sucessDeparment_Role == -1)
                {
                    message = "Không thể tạo phân quyền đơn vị !!!";
                    return message;
                }
                #endregion

                #region 3. Tiến hành tính lương  

                //Lấy phân quyền tính lương theo tài khoản
                PhanQuyenTinhLuong phanQuyenTinhLuong = obs.FindObject<PhanQuyenTinhLuong>(CriteriaOperator.Parse("SecuritySystemUser_Custom.Oid = ?", Common.SecuritySystemUser_GetCurrentUser().Oid));
                //
                if (phanQuyenTinhLuong != null) // Nếu có phân quyền theo tài khoản thì tiếp tục
                {
                    foreach (CongThucTinhLuong ct in congThucTinhLuongList)
                    {
                        if (!ct.NgungSuDung && ct.LoaiCongThucLuong == Enum.NhanSu.LoaiCongThucLuongEnum.CongThucLuongNgoaiGio)
                        {
                            ChiTietPhanQuyenTinhLuong chiTietPhanQuyen = obs.FindObject<ChiTietPhanQuyenTinhLuong>(CriteriaOperator.Parse("CongThucTinhLuong.Oid=? and PhanQuyenTinhLuong.Oid = ?", ct.Oid, phanQuyenTinhLuong.Oid));
                            if (chiTietPhanQuyen != null) // Nếu công thức lương này đã được phân quyền
                            {
                                //
                                string dieuKienNhanVien = ct.DieuKienNhanVien.XuLyDieuKien(obs, false, new object[] { obj.KyTinhLuong.TuNgay, obj.KyTinhLuong.DenNgay });

                                //
                                foreach (ChiTietCongThucTinhLuong ctItem in ct.ListChiTietCongThucTinhLuong)
                                {
                                    if (!ctItem.NgungSuDung)
                                    {
                                        param = new SqlParameter[4];
                                        param[0] = new SqlParameter("@BangLuongNgoaiGio", obj.Oid);
                                        param[1] = new SqlParameter("@DieuKienNhanVien", dieuKienNhanVien);
                                        param[2] = new SqlParameter("@CongThucTinhLuong", ctItem.Oid);
                                        param[3] = new SqlParameter("@CongTy", obj.CongTy.Oid);
                                        //
                                        int sucessPayroll = DataProvider.ExecuteNonQuery("spd_Luong_TinhLuongNgoaiGio", CommandType.StoredProcedure, param);
                                        //
                                        if (sucessPayroll == -1)
                                        {
                                            //Nếu lỗi một công thức cũng xóa hết tính lại
                                            param = new SqlParameter[1];
                                            param[0] = new SqlParameter("@BangLuongNhanVien", obj.Oid);
                                            DataProvider.ExecuteNonQuery("spd_Luong_XoaChiTietLuongNgoaiGio", CommandType.StoredProcedure, param);
                                            //
                                            message = "Công thức lương [" + ctItem.DienGiai + "] bị lỗi không tính được !!!";
                                            return message;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //
                    #region 4. Tiến hành xóa đi những người có thực lãnh < 0
                    //Nếu lỗi một công thức cũng xóa hết tính lại
                    param = new SqlParameter[1];
                    param[0] = new SqlParameter("@BangLuongNgoaiGio", obj.Oid);
                    DataProvider.ExecuteNonQuery("spd_Luong_XoaLuongNgoaiGio", CommandType.StoredProcedure, param);
                    //
                    #endregion
                }
                else
                {
                    message = "Tài khoản chưa được phân quyền tính lương !!!";
                    return message;
                }
                #endregion
            }
            //
            return message;
        }
    }
}
