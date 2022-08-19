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
using ERP.Module.NghiepVu.QuanLyKho.Khos;
using ERP.Module.NghiepVu.QuanLyKho.HangHoas;
using System.Collections.Generic;

namespace ERP.Module.Report.HocSinh.QuanLyKho
{
    [NonPersistent]
    [ModelDefault("Caption", "Xuất nhập tồn trong tháng - Kho")]
    public class Report_Kho_XuatNhapTonTrongThang : StoreProcedureReport
    {
        private Kho _Kho;
        private HangHoa _HangHoa;
        private DateTime _Thang;

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
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
        [ModelDefault("Caption", "Kho")]
        [DataSourceProperty("KhoList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Kho Kho
        {
            get
            {
                return _Kho;
            }
            set
            {
                SetPropertyValue("Kho", ref _Kho, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hàng hóa")]
        public HangHoa HangHoa
        {
            get
            {
                return _HangHoa;
            }
            set
            {
                SetPropertyValue("HangHoa", ref _HangHoa, value);
            }
        }

        [Browsable(false)]
        public XPCollection<Kho> KhoList { get; set; }

        public Report_Kho_XuatNhapTonTrongThang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid != Config.KeyTTCEdu)
            {
                CongTy ct = Session.GetObjectByKey<CongTy>(Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid);
                KhoList = new XPCollection<NghiepVu.QuanLyKho.Khos.Kho>(Session, (CriteriaOperator.Parse("CongTy =?", ct.Oid)));
            }
            else
                KhoList = new XPCollection<NghiepVu.QuanLyKho.Khos.Kho>(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Kho", Kho.Oid);
            param[1] = new SqlParameter("@HangHoa", HangHoa == null ? Guid.Empty : HangHoa.Oid);
            param[2] = new SqlParameter("@Thang", Thang);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_Kho_XuatNhapTonTrongThang", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
