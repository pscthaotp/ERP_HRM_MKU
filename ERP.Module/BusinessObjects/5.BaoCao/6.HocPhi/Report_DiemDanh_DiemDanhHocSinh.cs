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

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Điểm danh: Điểm danh học sinh")]
    public class Report_DiemDanh_DiemDanhHocSinh : StoreProcedureReport
    {
        // Fields...
        private LoaiBangDiemDanh _LoaiBangDiemDanh;
        private CongTy _CongTy;
        private Lop _Lop;
        private DateTime _Thang;

        [ModelDefault("Caption", "Loại bảng điểm danh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiBangDiemDanh LoaiBangDiemDanh
        {
            get
            {
                return _LoaiBangDiemDanh;
            }
            set
            {
                SetPropertyValue("LoaiBangDiemDanh", ref _LoaiBangDiemDanh, value);
            }
        }

        [ModelDefault("Caption", "Công ty/Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
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
            }
        }

        [ModelDefault("Caption", "Lớp")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        public Report_DiemDanh_DiemDanhHocSinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            Thang = Common.GetServerCurrentTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[5];

            parameter[0] = new SqlParameter("@LoaiBangDiemDanh", LoaiBangDiemDanh.Oid);
            parameter[1] = new SqlParameter("@CongTy", CongTy.Oid);
            parameter[2] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty );
            parameter[3] = new SqlParameter("@TuThang", Thang);
            parameter[4] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DiemDanh_DiemDanhHocSinh", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
