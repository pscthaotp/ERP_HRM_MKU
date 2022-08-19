using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.BepAn.KhoBep;
using ERP.Module.NghiepVu.QuanLyKho.BangGia;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Dữ liệu import báo giá theo trường sao chép")]
    public class Report_BepAn_KhoBep_DuLieuImportBaoGiaTheoTruong_SaoChep : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;
        private BangBaoGia _BangBaoGia;

        [ModelDefault("Caption", "Công ty/Trường")]
        [ImmediatePostData]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }
        [ModelDefault("Caption", "Bảng báo giá")]
        [ImmediatePostData]
        public BangBaoGia BangBaoGia
        {
            get
            {
                return _BangBaoGia;
            }
            set
            {
                SetPropertyValue("BangBaoGia", ref _BangBaoGia, value);
            }
        }

        public Report_BepAn_KhoBep_DuLieuImportBaoGiaTheoTruong_SaoChep(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);
            parameter[2] = new SqlParameter("@BangBaoGia", BangBaoGia != null ? BangBaoGia.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_KhoBep_DuLieuImportBangBaoGia", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
