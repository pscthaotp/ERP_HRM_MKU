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
    [ModelDefault("Caption", "Xuất nhập trong ngày - Kho")]
    public class Report_Kho_NhapXuatKhoTrongNgay : StoreProcedureReport
    {
        private DateTime _Ngay;
        private Kho _Kho;
        private bool _Nhap;
        private bool _Xuat;

        [ModelDefault("Caption", "Ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
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

        [ModelDefault("Caption", "Nhập kho")]
        [RuleRequiredField(DefaultContexts.Save)]
        public bool Nhap
        {
            get
            {
                return _Nhap;
            }
            set
            {
                SetPropertyValue("Nhap", ref _Nhap, value);
            }
        }

        [ModelDefault("Caption", "Xuất kho")]
        [RuleRequiredField(DefaultContexts.Save)]
        public bool Xuat
        {
            get
            {
                return _Xuat;
            }
            set
            {
                SetPropertyValue("Xuat", ref _Xuat, value);
            }
        }

        [Browsable(false)]
        public XPCollection<Kho> KhoList { get; set; }

        public Report_Kho_NhapXuatKhoTrongNgay(Session session) : base(session) { }

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
            int NhapKho;
            if (Nhap && Xuat)
                NhapKho = 2;
            else if(Nhap)
                NhapKho = 1;
            else
                NhapKho = 0;
                
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Ngay", Ngay);
            param[1] = new SqlParameter("@Kho", Kho.Oid);
            param[2] = new SqlParameter("@NhapKho", NhapKho);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_Kho_NhapXuatKhoTrongNgay", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
