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
using ERP.Module.NghiepVu.HocSinh;
using ERP.Module.Commons;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Phiếu đăng ký nhập học - Tuyển sinh")]
    public class BaoCao_TuyenSinh_PhieuDangKyNhapHoc : StoreProcedureReport
    {
        //
        private NamHoc _NamHoc;

        //
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
        public BaoCao_TuyenSinh_PhieuDangKyNhapHoc(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            NamHoc = Common.GetCurrentNamHoc(Session);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);     
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_PhieuDangKyNhapHoc", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
