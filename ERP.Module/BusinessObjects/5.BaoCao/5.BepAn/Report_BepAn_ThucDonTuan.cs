using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.BepAn;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Thực đơn tuần")]
    public class Report_BepAn_ThucDonTuan : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;
        private NhomDinhDuong _NhomDinhDuong;
        private NamHoc _NamHoc;
        private TuanHoc _TuTuanHoc;
        private TuanHoc _DenTuanHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty/Trường")]
        [DataSourceCriteria("LoaiTruong = 1 or LoaiTruong = 2")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Nhóm dinh dưỡng")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NhomDinhDuong NhomDinhDuong
        {
            get
            {
                return _NhomDinhDuong;
            }
            set
            {
                SetPropertyValue("NhomDinhDuong", ref _NhomDinhDuong, value);
            }
        }

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
                if (!IsLoading && value != null)
                {
                    TuTuanHoc = null;
                    DenTuanHoc = null;
                }
            }
        }
        
        [ModelDefault("Caption", "Từ tuần học")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NamHoc.ListTuanHoc")]
        public TuanHoc TuTuanHoc
        {
            get
            {
                return _TuTuanHoc;
            }
            set
            {
                SetPropertyValue("TuTuanHoc", ref _TuTuanHoc, value);
            }
        }

        [ModelDefault("Caption", "Đến tuần học")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NamHoc.ListTuanHoc")]
        public TuanHoc DenTuanHoc
        {
            get
            {
                return _DenTuanHoc;
            }
            set
            {
                SetPropertyValue("DenTuanHoc", ref _DenTuanHoc, value);
            }
        }
        
        public Report_BepAn_ThucDonTuan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            TuTuanHoc = Common.GetCurrentTuanHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@TuTuanHoc", TuTuanHoc != null ? TuTuanHoc.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@DenTuanHoc", DenTuanHoc != null ? DenTuanHoc.Oid : Guid.Empty);
            parameter[4] = new SqlParameter("@NhomDinhDuong", NhomDinhDuong != null ? NhomDinhDuong.Oid : Guid.Empty);
            parameter[5] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BepAn_ThucDonTuan", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
