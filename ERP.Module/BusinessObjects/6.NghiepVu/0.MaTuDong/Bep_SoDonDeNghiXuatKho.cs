using System.Data.SqlClient;
using System.Data;
using ERP.Module.CauHinhChungs;
using ERP.Module.Commons;
using ERP.Module.Extends;

namespace ERP.Module.NghiepVu.MaTuDong
{
    public class Bep_SoDonDeNghiXuatKho : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = Common.CauHinhChung_GetCauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhKho != null
                && cauHinh.CauHinhKho.MauSoDonDeNghiXuatKho_Bep.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetValueFromDatabase("spd_Bep_SoDonDeNghiXuatKho", CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhKho.SoBatDauDonDeNghiXuatKho_Bep;
                }
                else
                    soThuTu = cauHinh.CauHinhKho.SoBatDauDonDeNghiXuatKho_Bep;
                return cauHinh.CauHinhKho.MauSoDonDeNghiXuatKho_Bep.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
