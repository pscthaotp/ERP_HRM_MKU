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

namespace ERP.Module.Report.HocSinh.HocSinh
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Biểu đồ cá nhân học sinh - Học sinh")]
    public class Report_HocSinh_BieuDoHocSinh : StoreProcedureReport,ILop
    {
        // Fields...
        private Lop _Lop;
        private ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh _HocSinh;

        [ImmediatePostData]
        [ModelDefault("Caption", "Lớp")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
                if (!IsLoading && value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("Lop =?",Lop.Oid);
                    HSList = new XPCollection<NghiepVu.HocSinh.HocSinhs.HocSinh>(Session, filter);
                }
            }
        }

        [ModelDefault("Caption", "Học sinh")]
        [DataSourceProperty("HSList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh HocSinh
        {
            get
            {
                return _HocSinh;
            }
            set
            {
                SetPropertyValue("HocSinh", ref _HocSinh, value);
            }
        }

        [Browsable(false)]
        XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> HSList { get; set; }

        public Report_HocSinh_BieuDoHocSinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@HocSinh", HocSinh.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocSinh_BieuDoHocSinh", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }

}
