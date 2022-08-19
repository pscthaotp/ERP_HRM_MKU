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

namespace ERP.Module.Report.HocSinh.QuanLyKho
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách đơn hàng mua - Kho")]
    public class Report_Kho_DanhSachDonHangMua : StoreProcedureReport
    {
        private DateTime _Ngay;
        private Kho _Kho;

        [ModelDefault("Caption", "Ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime Ngay
        {
            get
            {
                return _Ngay;
            }
            set
            {
                SetPropertyValue("Ngay", ref _Ngay, value);
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

        [Browsable(false)]
        public XPCollection<Kho> KhoList { get; set; }

        public Report_Kho_DanhSachDonHangMua(Session session) : base(session) { }

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
            param[0] = new SqlParameter("@Ngay", Ngay);
            param[1] = new SqlParameter("@Kho", Kho.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_Kho_DanhSachDonHangMua", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
