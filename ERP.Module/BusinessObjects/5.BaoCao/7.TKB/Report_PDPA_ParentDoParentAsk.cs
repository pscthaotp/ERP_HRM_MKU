using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "PDPA - Parents do parents ask")]
    public class Report_PDPA_ParentDoParentAsk : StoreProcedureReport, ICongTy, ILop
    {
        // Fields...
        private CongTy _CongTy;
        private Khoi _Khoi;
        private Lop _Lop;
        private NghiepVu.HocSinh.HocSinhs.HocSinh _HocSinh;
        private DanhMuc.NhanSu.NamHoc _NamHoc;
        private DanhMuc.NhanSu.TuanHoc _TuanHoc;

        [ModelDefault("Caption", "Công ty/Trường")]
        [DataSourceCriteria("LoaiTruong = 1")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    Update_Khoi();
                    Update_Lop();
                }
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Khối")]
        [DataSourceProperty("KhoiList", DataSourcePropertyIsNullMode.SelectAll)]
        public Khoi Khoi
        {
            get
            {
                return _Khoi;
            }
            set
            {
                SetPropertyValue("Khoi", ref _Khoi, value);
                if (!IsLoading)
                { Update_Lop(); }
            }
        }

        [ModelDefault("Caption", "Lớp")]
        [DataSourceProperty("LopList",DataSourcePropertyIsNullMode.SelectAll)]
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

        [ModelDefault("Caption", "Học Sinh")]
        public NghiepVu.HocSinh.HocSinhs.HocSinh HocSinh
        {
            get
            {
                return _HocSinh;
            }
            set
            {
                SetPropertyValue("HocSinh", ref _HocSinh, value);
            }
        }

        [ModelDefault("Caption", "Năm Học")]
        public DanhMuc.NhanSu.NamHoc NamHoc
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
                    TuanHoc = null;
                }
            }
        }

        [ModelDefault("Caption", "Tuần Học")]
        [DataSourceProperty("NamHoc.ListTuanHoc")]
        public DanhMuc.NhanSu.TuanHoc TuanHoc
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

        public Report_PDPA_ParentDoParentAsk(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<Khoi> KhoiList { get; set; }

        public void Update_Khoi()
        {
            GroupOperator go = new GroupOperator { OperatorType = GroupOperatorType.And };
            CriteriaOperator filter = null;

            if (CongTy != null)
            {
                filter = CriteriaOperator.Parse("CongTy.Oid = ?", CongTy.Oid);
                go.Operands.Add(filter);
            }  
            if (KhoiList == null)
                KhoiList = new XPCollection<Khoi>(Session);
            KhoiList.Criteria = go;
        }

        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }

        public void Update_Lop()
        {
            GroupOperator go = new GroupOperator { OperatorType = GroupOperatorType.And };
            CriteriaOperator filter = null;

            if (CongTy != null)
            {
                filter = CriteriaOperator.Parse("CongTy.Oid = ?", CongTy.Oid);
                go.Operands.Add(filter);
            }
            if (Khoi != null)
            {
                filter = CriteriaOperator.Parse("LopCha.Oid = ?", Khoi.Oid);
                go.Operands.Add(filter);
            }
            if (LopList == null)
                LopList = new XPCollection<Lop>(Session);
            LopList.Criteria = go;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = Common.GetCurrentNamHoc(Session);
            TuanHoc = Common.GetCurrentTuanHoc(Session);
            Update_Khoi();
            Update_Lop();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            Update_Khoi();
            Update_Lop();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[7];

            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@Khoi", Khoi != null ? Khoi.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@HocSinh", HocSinh != null ? HocSinh.Oid : Guid.Empty);
            parameter[4] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);
            parameter[5] = new SqlParameter("@TuanHoc", TuanHoc != null ? TuanHoc.Oid : Guid.Empty);
            parameter[6] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Web_MamNon_ParentDoParentAsk", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }

}
