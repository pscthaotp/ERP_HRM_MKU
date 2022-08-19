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
using ERP.Module.Enum.QuanLyKho;
using ERP.Module.NghiepVu.QuanLyKho.HangHoas;

namespace ERP.Module.Report.HocSinh.QuanLyKho
{
    [NonPersistent]
    [ModelDefault("Caption", "Xuất nhập tồn bếp - Kho")]
    public class Report_Kho_XuatNhapTonBep : StoreProcedureReport
    {
        private DateTime _Thang;
        private Kho _Kho;

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

        [Browsable(false)]
        public XPCollection<Kho> KhoList { get; set; }

        public Report_Kho_XuatNhapTonBep(Session session) : base(session) { }

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
            string text = "";
            XPCollection<HangHoa> HHList = new XPCollection<HangHoa>(Session);
            HHList.Criteria = new InOperator("Oid", Common.Get_HangHoa_ByHangHoaCha(Session, LoaiPhanHeEnum.Bep));
            foreach (var item in HHList)
            {
                text += item.Oid;
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Thang", Thang);
            param[1] = new SqlParameter("@Kho", Kho.Oid);
            param[2] = new SqlParameter("@HH", text);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_Kho_XuatNhapTonBep", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
