using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.QuanLyKho.DonHang;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Phiếu kê chợ")]
    public class Report_BepAn_PhieuKeCho : StoreProcedureReport
    {
        private DonDatHang _DonDatHang;

        [ModelDefault("Caption", "Đơn đặt hàng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DonDatHang DonDatHang
        {
            get
            {
                return _DonDatHang;
            }
            set
            {
                SetPropertyValue("DonDatHang", ref _DonDatHang, value);
            }
        }

        public Report_BepAn_PhieuKeCho(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@DonDatHang", DonDatHang.Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BepAn_PhieuKeCho", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
