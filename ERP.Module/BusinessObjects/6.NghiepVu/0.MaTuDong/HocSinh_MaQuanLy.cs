
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
    public class HocSinh_MaQuanLy : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = Common.CauHinhChung_GetCauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHocSinh != null
                && cauHinh.CauHinhHocSinh.MauMaQuanLy.IsTemplate())
            {
                int soThuTu;
                string temp ="";
                object obj = DataProvider.GetValueFromDatabase("spd_HocSinh_MaQuanLy", CommandType.StoredProcedure, args);
                if (obj != null && obj.ToString() != "")
                {
                    string mau = obj.ToString();
                    temp = mau.Substring(0, 2);
                    string so = mau.Substring(2, 4); //mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhHocSinh.SoBatDauMaQuanLy;

                    return temp = cauHinh.CauHinhHocSinh.MauMaQuanLy.CreateTemplate(soThuTu).Replace("NienKhoa", mau.Substring(0, 2));
                }
                else
                {
                    soThuTu = cauHinh.CauHinhHocSinh.SoBatDauMaQuanLy;
                    temp = cauHinh.CauHinhHocSinh.MauMaQuanLy.CreateTemplate(soThuTu);
                    temp = temp.Replace("NienKhoa", Common.GetCurrentNamHoc(cauHinh.Session).NgayBatDau.Year.ToString().Substring(2,2));
                    return temp;
                }
                //return (temp + soThuTu);
            }
            return string.Empty;
        }
    }
}
