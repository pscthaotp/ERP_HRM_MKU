using System;
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
    [ModelDefault("Caption", "Báo cáo phân quyền - thống kê - hệ thống")]
    public class Report_BaoCaoThongKePhanQuyen : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;

        [ModelDefault("Caption", "Công ty")]
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

        public Report_BaoCaoThongKePhanQuyen(Session session) : base(session) { }

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
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_PhanQuyen_BaoCao_Lop_BoPhan", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}
