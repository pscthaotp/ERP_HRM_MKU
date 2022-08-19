﻿using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.Commons;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo phân quyền chức năng - thống kê")]
    public class Report_BaoCaoThongKeTheoDoiPhanQuyenChucNang : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;

        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        public Report_BaoCaoThongKeTheoDoiPhanQuyenChucNang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (!Common.TaiKhoanEdu())
            {
                CongTy = Common.CongTy(Session);
            }
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HeThong_TheoDoiPhanQuyenChucNang", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}
