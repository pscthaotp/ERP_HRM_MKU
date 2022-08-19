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
using ERP.Module.DanhMuc.TKB;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Tổng hợp đánh giá theo chủ đề - Học sinh")]
    public class Report_DanhGia_TongHopChiSoTuan : StoreProcedureReport, ILop
    {
        // Fields...
        private NamHoc _NamHoc;
        private ChuDeCha _ChuDe;
        private Lop _Lop;

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

        [ModelDefault("Caption", "Chủ đề")]
        public ChuDeCha ChuDe
        {
            get
            {
                return _ChuDe;
            }
            set
            {
                SetPropertyValue("ChuDe", ref _ChuDe, value);
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

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách tuần")]
        public XPCollection<TuanHoc> DSTuan { get; set; }

        public Report_DanhGia_TongHopChiSoTuan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
            parameter[1] = new SqlParameter("@ChuDe", ChuDe == null ? Guid.Empty : ChuDe.Oid);
            parameter[2] = new SqlParameter("@Lop", Lop == null ? Guid.Empty : Lop.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_DanhGia_TongHopChiSoTuan", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
