using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.XuLyNghiepVu
{
    public class QuyetDinhThoiViec_YS : IQuyetDinhThoiViec
    {
        public void Save(Session session, QuyetDinhThoiViec obj)
        {

            if (obj.NghiViecTuNgay <= Common.GetServerCurrentTime())
            {
                TinhTrang tinhTrang = Common.GetTinhTrang_ByTenTinhTrang(session, "nghỉ việc");
                if (tinhTrang == null)
                {
                    tinhTrang = new TinhTrang(session);
                    tinhTrang.TenTinhTrang = "Nghỉ việc";
                    tinhTrang.MaQuanLy = "NV";
                }
                obj.ThongTinNhanVien.TinhTrang = tinhTrang;
                //
                obj.ThongTinNhanVien.NgayNghiViec = obj.NghiViecTuNgay;
            }
        }

        public void Delete(Session session, QuyetDinhThoiViec obj)
        {
            //
            obj.ThongTinNhanVien.TinhTrang = obj.TinhTrangCu;
            //
            obj.ThongTinNhanVien.NgayNghiViec = DateTime.MinValue;
        }
    }
}
