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
using ERP.Module.DanhMuc.HocPhi;

namespace ERP.Module.Report.HocSinh.HocSinh
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Kiểm tra ngày đổ điểm danh - Học sinh")]
    public class Report_HocSinh_NgayDoDiemDanh : StoreProcedureReport,ILop
    {
        // Fields...
        private CongTy _CongTy;
        private Lop _Lop;
        private DateTime _Thang;
        private LoaiBangDiemDanh _LoaiBangDiemDanh;

        [ModelDefault("Caption", "Trường")]
        [ImmediatePostData]
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
                if (!IsLoading && value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("GCRecord IS NULL AND LoaiLop = 1 AND CongTy = ?", CongTy.Oid);
                    DSLop = new XPCollection<Lop>(Session, filter);
                }
            }
        }

        [ModelDefault("Caption", "Lớp")]
        [DataSourceProperty("DSLop")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Loại bảng điểm danh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiBangDiemDanh LoaiBangDiemDanh
        {
            get
            {
                return _LoaiBangDiemDanh;
            }
            set
            {
                SetPropertyValue("LoaiBangDiemDanh", ref _LoaiBangDiemDanh, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách lớp")]
        public XPCollection<Lop> DSLop { get; set; }

        public Report_HocSinh_NgayDoDiemDanh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@LoaiBangDiemDanh", LoaiBangDiemDanh.Oid);
            parameter[3] = new SqlParameter("@Thang", Thang);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocSinh_NgayDoDiemDanh", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }

}
