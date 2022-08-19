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
using DevExpress.Data.Filtering;
using System.ComponentModel;
using ERP.Module.NghiepVu.HocSinh.Lops;

namespace ERP.Module.Report.YTe
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Sổ phát thuốc - Y tế")]
    public class Report_YTe_SoPhatThuoc : StoreProcedureReport, ILop
    {
        // Fields...
        private DateTime _Ngay;
        private Lop _Lop;
        
        [ModelDefault("Caption", "Ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime Ngay
        {
            get
            {
                return _Ngay;
            }
            set
            {
                SetPropertyValue("Ngay", ref _Ngay, value);
            }
        }

        [ModelDefault("Caption", "Lớp")]
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

        public Report_YTe_SoPhatThuoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@Ngay", Ngay);
            parameter[1] = new SqlParameter("@Lop", Lop == null ? Guid.Empty : Lop.Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_YTe_SoPhatThuoc", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
