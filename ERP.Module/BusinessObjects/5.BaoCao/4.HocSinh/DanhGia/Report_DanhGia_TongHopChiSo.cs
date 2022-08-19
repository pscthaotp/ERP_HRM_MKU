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

namespace ERP.Module.Report.HocSinh.DanhGia
{
    [NonPersistent]
    [ModelDefault("Caption", "Tổng hợp đánh giá chỉ số - Học sinh")]
    public class Report_DanhGia_TongHopChiSo : StoreProcedureReport, ILop
    {
        //
        private ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh _HocSinh;
        private NamHoc _NamHoc;
        private Lop _Lop;

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

        [DataSourceProperty("HSList")]
        [ModelDefault("Caption", "Học sinh")]
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

        [Browsable(false)]
        XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> HSList { get; set; }

        public Report_DanhGia_TongHopChiSo(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@HocSinh", HocSinh.Oid);
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_DanhGia_TongHopChiSo", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
