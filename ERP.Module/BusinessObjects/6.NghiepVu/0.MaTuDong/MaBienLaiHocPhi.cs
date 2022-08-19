
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
    public class MaBienLaiHocPhi : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = Common.CauHinhChung_GetCauHinhChung;
            CauHinhHocPhi cauHinhHocPhi = cauHinh.ListCauHinhHocPhi.OrderBy(x => x.NgayApDung).Last();
            if (cauHinh != null
                && cauHinhHocPhi != null
                && cauHinhHocPhi.MauMaBienLai.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetValueFromDatabase("spd_HocPhi_MaBienLaiHocPhi", CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinhHocPhi.SoBatDauMaBienLai;
                }
                else
                    soThuTu = cauHinhHocPhi.SoBatDauMaBienLai;
                return cauHinhHocPhi.MauMaBienLai.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
