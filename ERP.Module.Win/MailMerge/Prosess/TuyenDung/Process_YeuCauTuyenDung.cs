using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using ERP.Module.Commons;
using ERP.Module.MailMerge;
using ERP.Module.MailMerge.NhanSu.TuyenDung;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERP.Module.Win.MailMerge.Prosess.TuyenDung
{
    public static class Process_YeuCauTuyenDung
    {
        public static void ShowMailMerge(DevExpress.ExpressApp.IObjectSpace obs, List<DangKyTuyenDung> _dangKyTuyenDungList)
        {
            var list = new List<Non_YeuCauTuyenDungFull>();
            Non_YeuCauTuyenDungFull yeuCauTuyenDung;

            foreach (DangKyTuyenDung dangKyTuyenDungItem in _dangKyTuyenDungList)
            {
                yeuCauTuyenDung = new Non_YeuCauTuyenDungFull();
                yeuCauTuyenDung.Oid = dangKyTuyenDungItem.Oid.ToString();
                yeuCauTuyenDung.DonViChuQuan = dangKyTuyenDungItem.BoPhan.TenBoPhan;
                //master
                Non_YeuCauTuyenDungMaster master = new Non_YeuCauTuyenDungMaster();
                master.Oid = dangKyTuyenDungItem.Oid.ToString();
                master.DonViYeuCau = dangKyTuyenDungItem.BoPhan.TenBoPhan;
                yeuCauTuyenDung.Master.Add(master);

                //detail
                Non_YeuCauTuyenDungDetail detail;
                int stt = 0;
                //     
                SqlParameter[] param = new SqlParameter[1];

                if (dangKyTuyenDungItem.BoPhan != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("BoPhan=? AND QuanLyTuyenDung=?", dangKyTuyenDungItem.BoPhan.Oid, dangKyTuyenDungItem.QuanLyTuyenDung.Oid);
                    using (XPCollection<DangKyTuyenDung> tuyenDungList = new XPCollection<DangKyTuyenDung>(((XPObjectSpace)obs).Session, filter))
                    {
                        if (tuyenDungList != null)
                        {
                            foreach (DangKyTuyenDung item in tuyenDungList)
                            {
                                stt++;
                                detail = new Non_YeuCauTuyenDungDetail();
                                detail.Oid = dangKyTuyenDungItem.Oid.ToString();
                                detail.STT = stt.ToString();
                                detail.ViTriTuyenDung = item.ViTriTuyenDung != null ? item.ViTriTuyenDung.TenViTriTuyenDung : "";
                                detail.SoLuongTuyen = item.SoLuongTuyen.ToString();
                                detail.ThoiGianLamViecDuKienDate = item.ThoiGianLamViecDuKien != DateTime.MinValue ? item.ThoiGianLamViecDuKien.ToString("dd/MM/yyyy") : "";
                                detail.LuongDuKien = item.LuongDuKien.ToString("N0");
                                detail.TrongKeHoach = item.SoLuongDinhBien.ToString();
                                detail.NgoaiKeHoach = item.SoLuongNgoaiDinhBien.ToString();
                                if (item.ThayThe)
                                    detail.ThayThe = "X";
                                if (item.DuAnMoi)
                                    detail.DuAnMoi = "X";

                                yeuCauTuyenDung.Detail.Add(detail);
                            }
                        }
                        list.Add(yeuCauTuyenDung);
                    }
                }
            }

            MailMergeTemplate[] merge = new MailMergeTemplate[3];
            //merge[1] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "YeuCauTuyenDungMaster.rtf")); 
            merge[1] = Common.GetTemplateWithValidDate(obs, "YeuCauTuyenDungMaster.rtf", _dangKyTuyenDungList[0].QuanLyTuyenDung.CongTy.Oid, _dangKyTuyenDungList[0].QuanLyTuyenDung.ThucHienTuNgay);
            //merge[2] = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", "YeuCauTuyenDungDetail.rtf")); 
            merge[2] = Common.GetTemplateWithValidDate(obs, "YeuCauTuyenDungDetail.rtf", _dangKyTuyenDungList[0].QuanLyTuyenDung.CongTy.Oid, _dangKyTuyenDungList[0].QuanLyTuyenDung.ThucHienTuNgay);
            merge[0] = Common.GetTemplateWithValidDate(obs, "YeuCauTuyenDungFull.rtf", _dangKyTuyenDungList[0].QuanLyTuyenDung.CongTy.Oid, _dangKyTuyenDungList[0].QuanLyTuyenDung.ThucHienTuNgay);
            if (merge[0] != null)
                Prosess_Show.ShowEditor<Non_YeuCauTuyenDungFull>(list, obs, merge);
            else
                XtraMessageBox.Show("Không tìm thấy mẫu in yêu cầu tuyển dụng trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
