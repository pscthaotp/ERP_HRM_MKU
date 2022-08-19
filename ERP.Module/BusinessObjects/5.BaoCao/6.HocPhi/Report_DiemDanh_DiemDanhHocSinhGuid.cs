using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Điểm danh: Điểm danh học sinh Guid")]
    public class Report_DiemDanh_DiemDanhHocSinhGuid : StoreProcedureReport
    {
        // Fields...
        private Guid _LoaiBangDiemDanh = Guid.Empty;
        private Guid _Lop = Guid.Empty;
        private DateTime _Thang;

        [ModelDefault("Caption", "Loại bảng điểm danh")]
        public Guid LoaiBangDiemDanh
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
        public Guid Lop
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

        public Report_DiemDanh_DiemDanhHocSinhGuid(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Thang = Common.GetServerCurrentTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];

            parameter[0] = new SqlParameter("@LoaiBangDiemDanh", LoaiBangDiemDanh);
            parameter[1] = new SqlParameter("@Lop", Lop);
            parameter[2] = new SqlParameter("@TuThang", Thang);
            parameter[3] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DiemDanh_DiemDanhHocSinh", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
