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
    [ModelDefault("Caption", "Tổng hợp đánh giá chỉ số - Theo lớp")]
    public class Report_DanhGia_TongHopChiSo_TheoLop : StoreProcedureReport, ILop
    {
        //
        private Lop _Lop;
        private NamHoc _NamHoc;
        private DateTime _Thang;

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

        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "{0: MM/yyyy}")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
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
            }
        }


        public Report_DanhGia_TongHopChiSo_TheoLop(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = Common.GetCurrentNamHoc(Session);
            Thang = DateTime.Now;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Lop", Lop.Oid);
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[2] = new SqlParameter("@Thang", Thang);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_DanhGia_TongHopChiSo_TheoLop", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
