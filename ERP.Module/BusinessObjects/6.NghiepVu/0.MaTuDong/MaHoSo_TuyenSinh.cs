
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
    public class MaHoSo_TuyenSinh : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = Common.CauHinhChung_GetCauHinhChung;
            if (Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.Equals(Config.KeyTanPhu))
            {
                if (cauHinh != null
             && cauHinh.CauHinhTuyenSinh != null
             && cauHinh.CauHinhTuyenSinh.TuDongTaoSoHoSo
             && cauHinh.CauHinhTuyenSinh.MauSoHoSo.IsTemplate())
                {
                    int soThuTu;
                    object obj = DataProvider.GetValueFromDatabase("spd_HoSoXetTuyen_MaHoSoXetTuyenLonNhat", CommandType.StoredProcedure, args);
                    if (obj != null)
                    {
                        string mau = obj.ToString();
                        string so = mau.GetNumberFromTemplate();
                        if (!string.IsNullOrWhiteSpace(so)
                            && int.TryParse(so, out soThuTu))
                            soThuTu++;
                        else
                            soThuTu = cauHinh.CauHinhTuyenSinh.SoBatDauSoHoSo;
                    }
                    else
                        soThuTu = cauHinh.CauHinhTuyenSinh.SoBatDauSoHoSo;
                    return cauHinh.CauHinhTuyenSinh.MauSoHoSo.CreateTemplate(soThuTu);
                }
            }

            else
            {
                if (cauHinh != null
              && cauHinh.CauHinhTuyenSinh != null
              && cauHinh.CauHinhTuyenSinh.TuDongTaoSoHoSo
              && cauHinh.CauHinhTuyenSinh.MauSoHoSo.IsTemplate())
                {
                    int soThuTu;
                    object obj = DataProvider.GetValueFromDatabase("spd_HoSoNhapHoc_MaHoSoLonNhat", CommandType.StoredProcedure, args);
                    if (obj != null)
                    {
                        string mau = obj.ToString();
                        string so = mau.GetNumberFromTemplate();
                        if (!string.IsNullOrWhiteSpace(so)
                            && int.TryParse(so, out soThuTu))
                            soThuTu++;
                        else
                            soThuTu = cauHinh.CauHinhTuyenSinh.SoBatDauSoHoSo;
                    }
                    else
                        soThuTu = cauHinh.CauHinhTuyenSinh.SoBatDauSoHoSo;
                    return cauHinh.CauHinhTuyenSinh.MauSoHoSo.CreateTemplate(soThuTu);
                }
            }
            return string.Empty;
        }
    }
}
