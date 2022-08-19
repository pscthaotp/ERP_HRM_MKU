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
using ERP.Module.Enum.Systems;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.HocSinh.HocSinh
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Tình hình tăng giảm trong tháng - Học sinh")]
    public class Report_HocSinh_TangGiamTrongThang : StoreProcedureReport,ICongTy
    {
        // Fields...
        private DateTime _Thang;
        private CongTy _CongTy;

        [ImmediatePostData]
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
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
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

        public Report_HocSinh_TangGiamTrongThang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Thang = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartMonth);
        }

        public override SqlCommand CreateCommand()
        {
            DateTime TuNgay, DenNgay;
            TuNgay = Thang.SetTime(SetTimeEnum.StartMonth);
            DenNgay = Thang.SetTime(SetTimeEnum.EndMonth);

            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@TuNgay", TuNgay);
            parameter[1] = new SqlParameter("@DenNgay", DenNgay);
            parameter[2] = new SqlParameter("@CongTy", CongTy.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocSinh_TangGiamTrongThang", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
