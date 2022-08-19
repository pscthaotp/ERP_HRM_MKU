using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;

namespace ERP.Module.NghiepVu.NhanSu.XuLyNghiepVu
{
    public class QuyetDinhTuyenDung_HRM : IQuyetDinhTuyenDung
    {     
        public void Save(Session session, ChiTietQuyetDinhTuyenDung obj)
        {
            //update dien bien luong
            ProcessesHelper.UpdateDienBienLuong(session, obj.QuyetDinhTuyenDung, obj.ThongTinNhanVien, obj.NgayHuongLuong, true);

            if (obj.QuyetDinhTuyenDung.QuyetDinhMoi)
            {
                //cập nhật thông tin lương
                obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = obj.NgachLuong;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = obj.BacLuong;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = obj.NgayHuongLuong;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = obj.PhanTramTinhLuong;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = obj.LuongCoBan;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = obj.LuongKinhDoanh;
            }

            //update dien bien luong
            ProcessesHelper.CreateDienBienLuong(session, obj.QuyetDinhTuyenDung, obj.ThongTinNhanVien, obj);

        }

        public void Delete(Session session, ChiTietQuyetDinhTuyenDung obj)
        {
            if (obj.QuyetDinhTuyenDung.QuyetDinhMoi)
            {
                //reset data
                obj.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = null;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = null;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = DateTime.MinValue;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong = 100;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan = 0;
                obj.ThongTinNhanVien.NhanVienThongTinLuong.LuongKinhDoanh = 0;
            }

            //Update đến ngày của diễn biến lương và đến năm của quá trình tham gia bảo hiểm trước đó = null
            ProcessesHelper.UpdateDienBienLuong(session, obj.QuyetDinhTuyenDung, obj.ThongTinNhanVien, obj.NgayHuongLuong, false);

            //xóa diễn biến lương
            ProcessesHelper.DeleteQuaTrinh<DienBienLuong>(session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", obj.QuyetDinhTuyenDung.Oid, obj.ThongTinNhanVien.Oid));

           }
    }
}
