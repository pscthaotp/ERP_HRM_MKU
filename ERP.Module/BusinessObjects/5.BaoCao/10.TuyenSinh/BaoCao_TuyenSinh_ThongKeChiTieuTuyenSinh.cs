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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Thống kê chỉ tiêu tuyển sinh - Tuyển sinh")]
    public class BaoCao_TuyenSinh_ThongKeChiTieuTuyenSinh : StoreProcedureReport, ICongTy
    {
        //
        private CongTy _CongTy;
        private NamHoc _NamHoc;

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
            }
        }

        public BaoCao_TuyenSinh_ThongKeChiTieuTuyenSinh(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //CongTy = Common.CongTy(Session);
            //
            NamHoc = Common.GetCurrentNamHoc(Session);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            param[1] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_ThongKeChiTieuTuyenSinh", CommandType.StoredProcedure, param);
            
            return cmd;
        }
    }
}
