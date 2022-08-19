using System.Data.SqlClient;
using System.Data;
using ERP.Module.CauHinhChungs;
using ERP.Module.Commons;
using ERP.Module.Extends;

namespace ERP.Module.NghiepVu.MaTuDong
{
    public class Bep_SoDonDatHangDuTru : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            CauHinhChung cauHinh = Common.CauHinhChung_GetCauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhKho != null
                && cauHinh.CauHinhKho.MauSoDonDatHangDuTru_Bep.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetValueFromDatabase("spd_Bep_SoDonDatHangDuTru", CommandType.StoredProcedure, args);
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhKho.SoBatDauDonDatHangDuTru_Bep;
                }
                else
                    soThuTu = cauHinh.CauHinhKho.SoBatDauDonDatHangDuTru_Bep;
                return cauHinh.CauHinhKho.MauSoDonDatHangDuTru_Bep.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
