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
    [ModelDefault("Caption", "Báo cáo phân quyền chức năng - thống kê")]
    public class Report_BaoCaoThongKeTheoDoiPhanQuyenChucNang : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Công ty/trường")]
        [ImmediatePostData]
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
                if (value != null)
                {
                    UpdateDS();
                }
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [ImmediatePostData]
        [DataSourceProperty("DSBoPhan")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);                
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh bộ phận")]
        public XPCollection<BoPhan> DSBoPhan { get; set; }

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
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HeThong_TheoDoiPhanQuyenChucNang", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
        public void UpdateDS()
        {
            if (DSBoPhan != null)
            { DSBoPhan.Reload(); }
            else
            { DSBoPhan = new XPCollection<BoPhan>(Session, false); }

            if (CongTy != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("BoPhanCha =?", CongTy.Oid);
                XPCollection<BoPhan> List = new XPCollection<BoPhan>(Session, filter);
                foreach (var item in List)
                {
                    DSBoPhan.Add(item);
                }
            }
        }
    }
}
