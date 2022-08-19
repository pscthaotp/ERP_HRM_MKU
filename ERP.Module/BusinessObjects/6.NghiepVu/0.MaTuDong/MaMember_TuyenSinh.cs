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
    public class MaMember_TuyenSinh : ManageKeyBase
    {
        public override string ManageKey(params SqlParameter[] args)
        {
            IObjectSpace obs = WebApplication.Instance.CreateObjectSpace();
            CauHinhChung cauHinh = obs.FindObject<CauHinhChung>(CriteriaOperator.Parse("CongTy = ?", args[0].Value));
            if (cauHinh != null
                  && cauHinh.CauHinhTuyenSinh != null
                  && cauHinh.CauHinhTuyenSinh.TuDongTaoMaMember
                  && cauHinh.CauHinhTuyenSinh.MauMaMember.IsTemplate())
            {
                int soThuTu;
                object sTT = DataProvider.GetValueFromDatabase("SELECT TOP 1 STT FROM dbo.Membership ORDER BY STT desc", CommandType.Text);

                if (sTT != null && !string.IsNullOrWhiteSpace(sTT.ToString())
                    && int.TryParse(sTT.ToString(), out soThuTu))
                    soThuTu++;
                else
                    soThuTu = cauHinh.CauHinhTuyenSinh.SoBatDauMaMember;

                return cauHinh.CauHinhTuyenSinh.MauMaMember.CreateTemplate(soThuTu);
            }
            return string.Empty;
        }
    }
}
