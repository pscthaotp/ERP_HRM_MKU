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
using ERP.Module.NghiepVu.HocSinh.Lops;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.TKB.ChuongTrinhNgoaiKhoa;
using ERP.Module.DanhMuc.HocPhi;

namespace ERP.Module.Report.HocSinh.HocSinh
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Tiến độ thực hiện ngoại khóa")]
    public class Report_NgoaiKhoa_BaoCaoTienDo : StoreProcedureReport
    {
        // Fields...
        private NamHoc _NamHoc;

        [ImmediatePostData]
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
        public Report_NgoaiKhoa_BaoCaoTienDo(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NamHoc", NamHoc.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_NgoaiKhoa_BaoCaoNgoaiKhoa", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = Common.GetCurrentNamHoc(Session);
        }
    }

}
