
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
    public class MaPhieuChi : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = Common.CauHinhChung_GetCauHinhChung;
            CauHinhHocPhi cauHinhHocPhi = cauHinh.ListCauHinhHocPhi.OrderBy(x => x.NgayApDung).Last();
            if (cauHinh != null
                && cauHinhHocPhi != null
                && cauHinhHocPhi.MauMaPhieuChi.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetValueFromDatabase("spd_PhieuChi_MaPhieuChi", CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinhHocPhi.SoBatDauMaPhieuChi;
                }
                else
                    soThuTu = cauHinhHocPhi.SoBatDauMaPhieuChi;
                return cauHinhHocPhi.MauMaPhieuChi.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
