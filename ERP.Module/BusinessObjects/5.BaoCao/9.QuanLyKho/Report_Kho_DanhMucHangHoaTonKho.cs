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
    [ModelDefault("Caption", "Danh mục hàng hóa tồn kho - Kho")]
    public class Report_Kho_DanhMucHangHoaTonKho : StoreProcedureReport
    {
        private Kho _Kho;
        private HangHoa _HangHoa;

        [ImmediatePostData]
        [DataSourceProperty("KhoList")]
        [ModelDefault("Caption", "Kho")]
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

        public Report_Kho_DanhMucHangHoaTonKho(Session session) : base(session) { }

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
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Kho", Kho.Oid);
            param[1] = new SqlParameter("@HangHoa", HangHoa == null ? Guid.Empty : HangHoa.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_Kho_DanhMucHangHoaTonKho", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
