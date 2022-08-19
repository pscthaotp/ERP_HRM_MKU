﻿using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.BepAn;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Sổ kiểm thực - Sổ lưu mẫu")]
    public class Report_BepAn_SoKiemThuc_SoLuuMau : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;
        private NhomDinhDuong _NhomDinhDuong;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty/Trường")]
        [DataSourceCriteria("LoaiTruong = 1 or LoaiTruong = 2")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Nhóm dinh dưỡng")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NhomDinhDuong NhomDinhDuong
        {
            get
            {
                return _NhomDinhDuong;
            }
            set
            {
                SetPropertyValue("NhomDinhDuong", ref _NhomDinhDuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading) { DenNgay = TuNgay; }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        public Report_BepAn_SoKiemThuc_SoLuuMau(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //CongTy = Common.CongTy(Session);
            TuNgay = Common.GetServerCurrentTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@NhomDinhDuong", NhomDinhDuong != null ? NhomDinhDuong.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@TuNgay", TuNgay != null && TuNgay != DateTime.MinValue ? TuNgay.ToString("dd/MM/yyyy") : "");
            parameter[3] = new SqlParameter("@DenNgay", DenNgay != null && DenNgay != DateTime.MinValue ? DenNgay.ToString("dd/MM/yyyy") : "");

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BepAn_SoKiemThuc_SoLuuMau", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
