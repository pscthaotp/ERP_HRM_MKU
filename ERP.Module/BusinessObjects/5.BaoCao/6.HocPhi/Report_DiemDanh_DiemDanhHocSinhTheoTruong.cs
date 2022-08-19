using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Điểm danh: Điểm danh học sinh theo trường")]
    public class Report_DiemDanh_DiemDanhHocSinhTheoTruong : StoreProcedureReport
    {
        // Fields...
        private LoaiBangDiemDanh _LoaiBangDiemDanh;
        private CongTy _CongTy;
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

        public Report_DiemDanh_DiemDanhHocSinhTheoTruong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            Thang = Common.GetServerCurrentTime();
            //
            CongTy = Common.CongTy(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];

            parameter[0] = new SqlParameter("@LoaiBangDiemDanh", LoaiBangDiemDanh.Oid);
            parameter[1] = new SqlParameter("@CongTy", CongTy.Oid);
            parameter[2] = new SqlParameter("@TuThang", Thang);
            parameter[3] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DiemDanh_DiemDanhHocSinhTheoTruong", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
