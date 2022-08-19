using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.CauHinhChungs;
using ERP.Module.Commons;
using ERP.Module.Extends;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.Data.Filtering;

namespace ERP.Module.NghiepVu.MaTuDong
{
    public class MaDuThi_TuyenSinh : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            IObjectSpace obs = WebApplication.Instance.CreateObjectSpace();
            CauHinhChung cauHinh = obs.FindObject<CauHinhChung>(CriteriaOperator.Parse("CongTy = ?", args[0].Value));
            if (cauHinh != null
                && cauHinh.CauHinhTuyenSinh != null
                && cauHinh.CauHinhTuyenSinh.TuDongTaoMaDuThi
                && cauHinh.CauHinhTuyenSinh.MauMaDuThi.IsTemplate())
            {
                int soThuTu = 0;
                object sTT = DataProvider.GetValueFromDatabase("SELECT TOP 1 STT FROM dbo.ToChucThi ORDER BY STT desc", CommandType.Text);
                if (sTT != null && !string.IsNullOrWhiteSpace(sTT.ToString())
                    && int.TryParse(sTT.ToString(), out soThuTu))
                    soThuTu++;
                else
                    soThuTu = cauHinh.CauHinhTuyenSinh.SoBatDauMaDuThi;
                //
                return cauHinh.CauHinhTuyenSinh.MauMaDuThi.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
