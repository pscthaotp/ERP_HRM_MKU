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
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.DanhMuc.TKB;

namespace ERP.Module.Report.HocSinh.DanhGia
{
    [NonPersistent]
    [ModelDefault("Caption", "Đánh giá phát triển của trẻ - Học sinh")]
    public class Report_DanhGia_DanhGiaPhatTrienCuaTre : StoreProcedureReport, ILop
    {
        //
        private Lop _Lop;
        private ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh _HocSinh;
        private NamHoc _NamHoc;
        private ChuDeCha _ChuDe;

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
                    CriteriaOperator filter = CriteriaOperator.Parse("Lop =?", Lop.Oid);
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

        [Browsable(false)]
        XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> HSList { get; set; }

        public Report_DanhGia_DanhGiaPhatTrienCuaTre(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@HocSinh", HocSinh.Oid);
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[2] = new SqlParameter("@ChuDe", ChuDe.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_DanhGia_DanhGiaPhatTrienCuaTre", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
