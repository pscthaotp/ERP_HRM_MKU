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
using ERP.Module.NghiepVu.HocSinh;
using ERP.Module.Commons;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Hồ sơ nhập học - Tuyển sinh")]
    [Appearance("HoSoNhapHoc.TatCaTruong", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_HoSoNhapHoc : StoreProcedureReport,ICongTy
    {
        //
        private bool _TatCa = true;
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private HoSoNhapHoc _HoSoNhapHoc;
        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả")]
        public bool TatCa
        {
            get
            {
                return _TatCa;
            }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
                if (!IsLoading)
                {
                    CongTy = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCa")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    HoSoNhapHoc = null;
                    UpdateHSList();
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
                if (!IsLoading)
                {
                    HoSoNhapHoc = null;
                    UpdateHSList();
                }
            }
        }

        [ModelDefault("Caption", "Hồ sơ nhập học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("HSList", DataSourcePropertyIsNullMode.SelectAll)]
        public HoSoNhapHoc HoSoNhapHoc
        {
            get
            {
                return _HoSoNhapHoc;
            }
            set
            {
                SetPropertyValue("HoSoNhapHoc", ref _HoSoNhapHoc, value);
            }
        }

        public BaoCao_TuyenSinh_HoSoNhapHoc(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            NamHoc = Common.GetCurrentNamHoc(Session);
            //
            //CongTy = Common.CongTy(Session);
        }
        public override SqlCommand CreateCommand()
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[1] = new SqlParameter("@HoSoNhapHoc", HoSoNhapHoc.Oid);

            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_HoSoNhapHoc", CommandType.StoredProcedure, param);
            //
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<HoSoNhapHoc> HSList { get; set; }

        private void UpdateHSList()
        {
            if (HSList == null)
                HSList = new XPCollection<HoSoNhapHoc>(Session);
            //
            if (TatCa)
                HSList.Criteria = CriteriaOperator.Parse("NamHoc.Oid=?", NamHoc != null ? NamHoc.Oid : Guid.Empty);
            else
                HSList.Criteria = CriteriaOperator.Parse("NamHoc.Oid=? and CongTy.Oid=?", NamHoc != null ? NamHoc.Oid : Guid.Empty,CongTy != null ? CongTy.Oid : Guid.Empty);

        }
    }
}
