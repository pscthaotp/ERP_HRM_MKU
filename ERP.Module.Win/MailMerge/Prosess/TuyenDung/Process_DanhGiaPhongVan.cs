using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.MailMerge;
using ERP.Module.MailMerge.NhanSu.TuyenDung;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERP.Module.Win.MailMerge.Prosess.TuyenDung
{
    public static class Process_DanhGiaPhongVan
    {
        public static void ShowMailMerge(DevExpress.ExpressApp.IObjectSpace obs, List<QuanLyTuyenDung> _quanLyTuyenDungList)
        {
            var non_DanhGiaPhongVanList = new List<Non_DanhGiaPhongVan>();
            Non_DanhGiaPhongVan non_DanhGiaPhongVan;
            foreach (QuanLyTuyenDung qlTuyenDung in _quanLyTuyenDungList)
            {
                non_DanhGiaPhongVan = new Non_DanhGiaPhongVan();
                non_DanhGiaPhongVanList.Add(non_DanhGiaPhongVan);
            }
            if (_quanLyTuyenDungList.Count > 0)
            {
                MailMergeTemplate merge = Common.GetTemplate(obs, "DanhGiaPhongVan.rtf", _quanLyTuyenDungList[0].CongTy.Oid);
                if (merge != null)
                    Prosess_Show.ShowEditor<Non_DanhGiaPhongVan>(non_DanhGiaPhongVanList, obs, merge);
                else
                    DialogUtil.ShowError("Không tìm thấy mẫu đánh giá phỏng vấn trong hệ thống.");
            }
        }
    }
}
