
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.CauHinhChungs;
using ERP.Module.Commons;
using ERP.Module.Extends;

namespace ERP.Module.NghiepVu.MaTuDong
{
    public class MaNhanVien : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = Common.CauHinhChung_GetCauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHoSo != null
                && cauHinh.CauHinhHoSo.TuDongTaoMaQuanLy
                && cauHinh.CauHinhHoSo.MauMaQuanLy.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetValueFromDatabase("spd_NhanVien_MaNhanVienLonNhat", CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhHoSo.SoBatDauMaQuanLy;
                }
                else
                    soThuTu = cauHinh.CauHinhHoSo.SoBatDauMaQuanLy;
                return cauHinh.CauHinhHoSo.MauMaQuanLy.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
