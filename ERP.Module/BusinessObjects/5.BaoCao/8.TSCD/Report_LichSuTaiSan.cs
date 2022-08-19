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

namespace ERP.Module.Report.TSCD
{
    [NonPersistent]
    [ModelDefault("Caption", "Lịch sử tài sản - TSCD")]
    public class Report_LichSuTaiSan : StoreProcedureReport
    {
        private TaiSanCoDinhCaBiet _TaiSanCoDinhCaBiet;

        [ModelDefault("Caption", "Tài sản cố định")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("TSList", DataSourcePropertyIsNullMode.SelectNothing)]   
        public TaiSanCoDinhCaBiet TaiSanCoDinhCaBiet
        {
            get
            {
                return _TaiSanCoDinhCaBiet;
            }
            set
            {
                SetPropertyValue("TaiSanCoDinhCaBiet", ref _TaiSanCoDinhCaBiet, value);
            }
        }

        [Browsable(false)]
        XPCollection<TaiSanCoDinhCaBiet> TSList
        {
            get;
            set;
        }

        public Report_LichSuTaiSan(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TSCD", TaiSanCoDinhCaBiet.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TSCD_LichSuTaiSanCaBiet", CommandType.StoredProcedure, param);
            //
            return cmd;

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (!IsLoading)
            {
                TSList = new XPCollection<TaiSanCoDinhCaBiet>(Session);
                //
                InOperator filter = new InOperator("CongTy", Common.Department_GetRoledDepartmentList_ByCurrentUser());
                TSList.Criteria = filter;
            }
        }
    }
}
