using System.Data.SqlClient;
using System.Data;
using ERP.Module.CauHinhChungs;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NghiepVu.MaTuDong
{
    public class MaHopDongLamViec : ManageKeyCompanyBase
    {
        public override string ManageKeyCompany(CongTy congTy, params SqlParameter[] args)
        {
            CauHinhChung cauHinh = congTy.CauHinhChung;
            if (cauHinh != null
                && cauHinh.CauHinhHopDong != null
                && cauHinh.CauHinhHopDong.TuDongTaoSoHopDongLamViec
                && cauHinh.CauHinhHopDong.MauSoHopDongLamViec.IsTemplate())
            {
                int soThuTu;
                object obj = DataProvider.GetValueFromDatabase("spd_HopDong_SoHopDongLaoDongLonNhat", CommandType.StoredProcedure, args);
                //
                if (obj != null)
                {
                    string mau = obj.ToString();
                    string so = mau.GetNumberFromTemplate();
                    if (!string.IsNullOrWhiteSpace(so)
                        && int.TryParse(so, out soThuTu))
                        soThuTu++;
                    else
                        soThuTu = cauHinh.CauHinhHopDong.SoBatDauHopDongLamViec;
                }
                else
                    soThuTu = cauHinh.CauHinhHopDong.SoBatDauHopDongLamViec;
                return cauHinh.CauHinhHopDong.MauSoHopDongLamViec.CreateTemplateWithYear(soThuTu);
            }
            return string.Empty;
        }
    }
}
