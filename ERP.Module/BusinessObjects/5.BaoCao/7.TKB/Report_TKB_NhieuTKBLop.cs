using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.ThoiKhoaBieu
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Thời khóa biểu in nhiều - TKB")]
    public class Report_TKB_NhieuTKBLop : StoreProcedureReport, ICongTy, ILop
    {
        // Fields...
        private CongTy _CongTy;
        private Lop _Lop;
        private NamHoc _NamHoc;
        private TuanHoc _TuanHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty/trường")]
        [DataSourceCriteria("LoaiTruong = 1")]
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
                    CriteriaOperator filter = CriteriaOperator.Parse("GCRecord IS NULL AND LoaiLop = 0 AND CongTy = ?", CongTy.Oid);
                    DSLop = new XPCollection<Lop>(Session, filter);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khối")]
        [DataSourceProperty("DSLop")]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid);
                    DSTuan = new XPCollection<TuanHoc>(Session, filter);
                }
            }
        }

        [ModelDefault("Caption", "Tuần học")]
        [DataSourceProperty("DSTuan")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TuanHoc TuanHoc
        {
            get
            {
                return _TuanHoc;
            }
            set
            {
                SetPropertyValue("TuanHoc", ref _TuanHoc, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách tuần")]
        public XPCollection<TuanHoc> DSTuan { get; set; }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách khối")]
        public XPCollection<Lop> DSLop { get; set; }

        public Report_TKB_NhieuTKBLop(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuanHoc = Common.GetCurrentTuanHoc(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            if (!Common.TaiKhoanBinhThuong_NotEdu())
                CongTy = null;
        }

        public override SqlCommand CreateCommand()
        {          
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@Khoi", Lop != null ? Lop.Oid :  Guid.Empty);
                parameter[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
                parameter[2] = new SqlParameter("@TuanHoc", TuanHoc.Oid);
                parameter[3] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
                SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TKB_NhieuLop", System.Data.CommandType.StoredProcedure, parameter);
                //cmd.CommandTimeout = 180;
                return cmd;        }
    }

}
