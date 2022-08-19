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
using ERP.Module.NghiepVu.TKB.ChuongTrinhTiengAnh;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.ThoiKhoaBieu
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Mẫu Import chương trình tiếng anh")]
    public class Report_Mau_ImportCTTiengAnh : StoreProcedureReport
    {
        private CongTy _CongTy;
        private Lop _Lop;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Trường")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("LoaiTruong = 1")]
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
                    CriteriaOperator filter = CriteriaOperator.Parse("GCRecord IS NULL AND LoaiLop = 0 AND CongTy = ?", CongTy.Oid);
                    DSLop = new XPCollection<Lop>(Session, filter);
                }
            }
        }
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

        [ModelDefault("Caption", "Khối")]
        [DataSourceProperty("DSLop")]
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
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách lớp")]
        public XPCollection<Lop> DSLop { get; set; }

        public Report_Mau_ImportCTTiengAnh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@TuanHoc", Guid.Empty);
            parameter[1] = new SqlParameter("@Khoi", Lop.Oid);
            parameter[2] = new SqlParameter("@NamHoc", NamHoc.Oid);
            parameter[3] = new SqlParameter("@CongTy", CongTy.Oid);
            parameter[4] = new SqlParameter("@LoaiKhoa", 4);
            parameter[5] = new SqlParameter("@LoaiMau", 2);           
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_MauImport_TA_ABI", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
