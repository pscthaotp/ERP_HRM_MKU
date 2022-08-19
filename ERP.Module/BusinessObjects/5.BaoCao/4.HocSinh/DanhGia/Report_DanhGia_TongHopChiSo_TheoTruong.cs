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

namespace ERP.Module.Report.HocSinh.DanhGia
{
    [NonPersistent]
    [ModelDefault("Caption", "Tổng hợp đánh giá chỉ số - Theo trường")]
    public class Report_DanhGia_TongHopChiSo_TheoTruong : StoreProcedureReport, ICongTy
    {
        //
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private DateTime _Thang;

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
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

        public Report_DanhGia_TongHopChiSo_TheoTruong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            Thang = DateTime.Now;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CongTy", CongTy.Oid);
            param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[2] = new SqlParameter("@Thang", Thang);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_DanhGia_TongHopChiSo_TheoTruong", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
