using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocPhi.BienLai;
using ERP.Module.NghiepVu.HocPhi.BangCongNos;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "In công nợ cá nhân - Học phí")]
    public class Report_CongNo_InCongNoCaNhan : StoreProcedureReport
    {
        // Fields...
        private HocSinhCongNo _HocSinhCongNo;
        private CongNo _CongNo;

        [ImmediatePostData]
        [Browsable(false)]
        [ModelDefault("Caption", "Học sinh công nợ")]
        public HocSinhCongNo HocSinhCongNo
        {
            get
            {
                return _HocSinhCongNo;
            }
            set
            {
                SetPropertyValue("HocSinhCongNo", ref _HocSinhCongNo, value);
                if (!IsLoading && value != null)
                    UpdateCongNo();
            }
        }

        [ModelDefault("Caption", "Công nợ")]
        [DataSourceProperty("CongNoList", DataSourcePropertyIsNullMode.SelectAll)]
        public CongNo CongNo
        {
            get
            {
                return _CongNo;
            }
            set
            {
                SetPropertyValue("CongNo", ref _CongNo, value);
            }
        }

        public Report_CongNo_InCongNoCaNhan(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<CongNo> CongNoList { get; set; }

        public void UpdateCongNo()
        {
            if (CongNoList == null)
                CongNoList = new XPCollection<CongNo>(Session);
            //
            CongNoList.Criteria = CriteriaOperator.Parse("HocSinhCongNo = ?", HocSinhCongNo.Oid);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@CongNo", CongNo.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BienLai_InCongNoCaNhan", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
