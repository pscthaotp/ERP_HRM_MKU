
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
    public class MaHopDongThinhGiang : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = Common.CauHinhChung_GetCauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHopDong != null
                && cauHinh.CauHinhHopDong.TuDongTaoSoHopDongTG
                && cauHinh.CauHinhHopDong.MauSoHopDongTG.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetValueFromDatabase("spd_HopDong_SoHopDongThinhGiangLonNhat", CommandType.StoredProcedure, args);
                //
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhHopDong.SoBatDauHopDongTG;
                }
                else
                    soThuTu = cauHinh.CauHinhHopDong.SoBatDauHopDongTG;
                return cauHinh.CauHinhHopDong.MauSoHopDongTG.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
