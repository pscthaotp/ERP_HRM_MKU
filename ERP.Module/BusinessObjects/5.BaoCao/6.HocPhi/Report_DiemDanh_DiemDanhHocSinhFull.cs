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

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Điểm danh: Điểm danh học sinh")]
    public class Report_DiemDanh_DiemDanhHocSinhFull : StoreProcedureReport
    {
        // Fields...
        private LoaiBangDiemDanh _LoaiBangDiemDanh;
        private Lop _Lop;
        private DateTime _TuThang;
        private DateTime _DenThang;

        [ModelDefault("Caption", "Loại bảng điểm danh")]
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
        
        [ModelDefault("Caption", "Trường/Khối/Lớp")]
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

        [ModelDefault("Caption", "Từ tháng")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value != DateTime.MinValue ? value.SetTime(Enum.Systems.SetTimeEnum.StartMonth) : value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value != DateTime.MinValue ? value.SetTime(Enum.Systems.SetTimeEnum.EndMonth) : value);
            }
        }

        public Report_DiemDanh_DiemDanhHocSinhFull(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            TuThang = Common.GetServerCurrentTime();
            DenThang = TuThang;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[5];

            parameter[0] = new SqlParameter("@LoaiBangDiemDanh", LoaiBangDiemDanh != null ? LoaiBangDiemDanh.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty );
            parameter[2] = new SqlParameter("@TuThang", TuThang != DateTime.MinValue ? TuThang.ToString("dd/MM/yyyy") : "");
            parameter[3] = new SqlParameter("@DenThang", DenThang != DateTime.MinValue ? DenThang.ToString("dd/MM/yyyy") : "");
            parameter[4] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DiemDanh_DiemDanhHocSinhFull", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
