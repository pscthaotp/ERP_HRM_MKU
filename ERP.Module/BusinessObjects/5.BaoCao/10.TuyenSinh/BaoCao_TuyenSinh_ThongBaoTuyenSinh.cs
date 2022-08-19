using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.NhanSu.TuyenSinh //Sai k duoc lam theo
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông báo tuyển sinh - Tuyển sinh")]
    public class BaoCao_TuyenSinh_ThongBaoTuyenSinh : StoreProcedureReport, ICongTy
    {
        //
        private CongTy _CongTy;
        private DateTime _Thang;

        //
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

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("Editmask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
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

        public BaoCao_TuyenSinh_ThongBaoTuyenSinh(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //CongTy = Common.CongTy(Session);
            //
            Thang = Common.GetServerCurrentTime();
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            param[1] = new SqlParameter("@Thang", Thang);

            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_ThongBaoTuyenSinh", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
