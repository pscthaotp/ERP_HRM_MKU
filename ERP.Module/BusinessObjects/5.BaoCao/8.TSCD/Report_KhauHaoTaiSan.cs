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
using ERP.Module.DanhMuc.TSCD;
using ERP.Module.NghiepVu.QuanLyKho.HangHoas;
using ERP.Module.NghiepVu.TSCD;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.TSCD
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo khấu hao tài sản - TSCD")]
    public class Report_KhauHaoTaiSan : StoreProcedureReport
    {
        private DateTime _DenNgay;
        private CongTy _CongTy;
        private bool _KhauHaoHet;
        private BoPhanTS _BoPhanTS;

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ImmediatePostData]
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
                if (!IsLoading && value != null)
                {
                    UpdateBP();
                }
            }
        }

        [ModelDefault("Caption", "Khấu hao hết")]
        public bool KhauHaoHet
        {
            get
            {
                return _KhauHaoHet;
            }
            set
            {
                SetPropertyValue("KhauHaoHet", ref _KhauHaoHet, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("BoPhanList")]
        public BoPhanTS BoPhanTS
        {
            get
            {
                return _BoPhanTS;
            }
            set
            {
                SetPropertyValue("BoPhanTS", ref _BoPhanTS, value);
            }
        }

        [Browsable(false)]
        XPCollection<BoPhanTS> BoPhanList { get; set; }

        public Report_KhauHaoTaiSan(Session session) : base(session) { }

        public void UpdateBP()
        {
            CriteriaOperator filter = new InOperator("BoPhan", Common.Department_GetRoledDepartmentList_ByCurrentUser());
            BoPhanList = new XPCollection<BoPhanTS>(Session, filter);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DenNgay", DenNgay);
            param[1] = new SqlParameter("@CongTy", CongTy.Oid);
            param[2] = new SqlParameter("@KhauHaoHet", KhauHaoHet);
            param[3] = new SqlParameter("@BoPhanTS", BoPhanTS == null ? Guid.Empty : BoPhanTS.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TSCD_KhauHaoTS", CommandType.StoredProcedure, param);
            //
            return cmd;

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
        }
    }
}
