using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.TKB.ChuongTrinhNgoaiKhoa;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Điểm danh: Điểm danh học sinh theo tháng")]
    [Appearance("Report_DiemDanh_DiemDanhHocSinhThang.LopNgoaiKhoa", TargetItems = "LopNgoaiKhoa;NamHoc", Enabled = false, Criteria = "LoaiBangDiemDanh.MacDinh")]
    [Appearance("Report_DiemDanh_DiemDanhHocSinhThang.Lop", TargetItems = "Lop", Enabled = false, Criteria = "!LoaiBangDiemDanh.MacDinh")]

    public class Report_DiemDanh_DiemDanhHocSinhThang : StoreProcedureReport, ICongTy
    {
        // Fields...
        private LoaiBangDiemDanh _LoaiBangDiemDanh;
        private CongTy _CongTy;
        private Lop _Lop;
        private NamHoc _NamHoc;
        private LopNgoaiKhoa _LopNgoaiKhoa;
        private DateTime _Thang;

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại bảng điểm danh")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public LoaiBangDiemDanh LoaiBangDiemDanh
        {
            get
            {
                return _LoaiBangDiemDanh;
            }
            set
            {
                SetPropertyValue("LoaiBangDiemDanh", ref _LoaiBangDiemDanh, value);
                if (!IsLoading)
                {
                    Lop = null;
                    LopNgoaiKhoa = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty/Trường")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if(!IsLoading)
                {
                    Lop = null;
                    LopNgoaiKhoa = null;
                    Update_LopList();
                    Update_LopNgoaiKhoaList();
                }
            }
        }

        [ModelDefault("Caption", "Trường/Khối/Lớp")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("LopList", DataSourcePropertyIsNullMode.SelectAll)]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Lớp ngoại khóa")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("LopNgoaiKhoaList", DataSourcePropertyIsNullMode.SelectAll)]
        public LopNgoaiKhoa LopNgoaiKhoa
        {
            get
            {
                return _LopNgoaiKhoa;
            }
            set
            {
                SetPropertyValue("LopNgoaiKhoa", ref _LopNgoaiKhoa, value);
            }
        }

        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value.SetTime(Enum.Systems.SetTimeEnum.StartMonth));
            }
        }

        public Report_DiemDanh_DiemDanhHocSinhThang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Thang = Common.GetServerCurrentTime();

            Update_LopList();
            Update_LopNgoaiKhoaList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            Update_LopList();
            Update_LopNgoaiKhoaList();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[6];

            parameter[0] = new SqlParameter("@LoaiBangDiemDanh", LoaiBangDiemDanh != null ? LoaiBangDiemDanh.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : (LopNgoaiKhoa != null ? LopNgoaiKhoa.Oid : Guid.Empty));
            parameter[3] = new SqlParameter("@TuThang", Thang != DateTime.MinValue ? Thang.ToString("dd/MM/yyyy") : "");
            parameter[4] = new SqlParameter("@DenThang", Thang != DateTime.MinValue ? Thang.SetTime(Enum.Systems.SetTimeEnum.EndMonth).ToString("dd/MM/yyyy") : "");
            parameter[5] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DiemDanh_DiemDanhHocSinhFullGroup", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }

        public void Update_LopList()
        {
            GroupOperator go = new GroupOperator { OperatorType = GroupOperatorType.And };
            CriteriaOperator filter = null;
            if (CongTy != null)
            {
                filter = CriteriaOperator.Parse("CongTy.Oid = ?", CongTy.Oid);
                go.Operands.Add(filter);
            }
            if (LopList == null)
                LopList = new XPCollection<Lop>(Session);
            LopList.Criteria = go;
        }

        [Browsable(false)]
        public XPCollection<LopNgoaiKhoa> LopNgoaiKhoaList { get; set; }
        public void Update_LopNgoaiKhoaList()
        {
            GroupOperator go = new GroupOperator { OperatorType = GroupOperatorType.And };
            CriteriaOperator filter = null;
            if (CongTy != null)
            {
                filter = CriteriaOperator.Parse("CongTy.Oid = ?", CongTy.Oid);
                go.Operands.Add(filter);
            }
            if (NamHoc != null)
            {
                filter = CriteriaOperator.Parse("NamHoc.Oid = ?", NamHoc.Oid);
                go.Operands.Add(filter);
            }
            if (LopNgoaiKhoaList == null)
                LopNgoaiKhoaList = new XPCollection<LopNgoaiKhoa>(Session);
            LopNgoaiKhoaList.Criteria = go;
        }
    }

}
