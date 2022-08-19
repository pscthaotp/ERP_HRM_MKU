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

namespace ERP.Module.Report.ThoiKhoaBieu
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Thời khóa biểu cá nhân - TKB")]
    public class Report_TKB_TKBCaNhan : StoreProcedureReport, ILop
    {
        // Fields...
        private Lop _Lop;
        private ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh _HocSinh;
        private NamHoc _NamHoc;
        private bool _ChamDuong;
        private bool _GiaoDuc;
        private TuanHoc _TuanHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Lớp")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
                if (!IsLoading && value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("Lop =?", Lop.Oid);
                    HocSinhList = new XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(Session, filter);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Học sinh")]
        [DataSourceProperty("HocSinhList")]
        public ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh HocSinh
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

        [ModelDefault("Caption", "Chăm dưỡng")]
        public bool ChamDuong
        {
            get
            {
                return _ChamDuong;
            }
            set
            {
                SetPropertyValue("ChamDuong", ref _ChamDuong, value);
            }
        }

        [ModelDefault("Caption", "Giáo dục")]
        public bool GiaoDuc
        {
            get
            {
                return _GiaoDuc;
            }
            set
            {
                SetPropertyValue("GiaoDuc", ref _GiaoDuc, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách học sinh")]
        public XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> HocSinhList { get; set; }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách tuần")]
        public XPCollection<TuanHoc> DSTuan { get; set; }

        public Report_TKB_TKBCaNhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuanHoc = Common.GetCurrentTuanHoc(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            int CaHai = 2;
            if(ChamDuong == true && GiaoDuc == false)   
                CaHai = 1;
            else if (ChamDuong == false && GiaoDuc == true)
                CaHai = 0;

            if (HocSinh != null)
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@HocSinh", HocSinh.Oid);
                parameter[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
                parameter[2] = new SqlParameter("@TuanHoc", TuanHoc.Oid);
                parameter[3] = new SqlParameter("@CaHai", CaHai);
                SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TKB_TKBCaNhan", System.Data.CommandType.StoredProcedure, parameter);
                //cmd.CommandTimeout = 180;
                return cmd;
            }
            else
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@Lop", Lop.Oid);
                parameter[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
                parameter[2] = new SqlParameter("@TuanHoc", TuanHoc.Oid);
                parameter[3] = new SqlParameter("@CaHai", CaHai);
                SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TKB_TKBLop", System.Data.CommandType.StoredProcedure, parameter);
                //cmd.CommandTimeout = 180;
                return cmd;
            }
        }
    }

}
