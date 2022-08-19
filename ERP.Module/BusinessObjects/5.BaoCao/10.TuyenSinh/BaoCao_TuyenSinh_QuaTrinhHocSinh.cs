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
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.Systems;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Quá trình học sinh - Tuyển sinh")]
    public class BaoCao_TuyenSinh_QuaTrinhHocSinh : StoreProcedureReport, ICongTy,ILop
    {
        //
        private CongTy _CongTy;
        private Lop _Lop;
        private HocSinh _HocSinh;

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
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
                if (!IsLoading)
                {
                    Lop = null;
                    UpdateLopList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lớp")]
        [DataSourceProperty("LopList", DataSourcePropertyIsNullMode.SelectAll)]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
                if (!IsLoading)
                {
                    HocSinh = null;
                    UpdateHSList();
                }
            }
        }

        [ModelDefault("Caption", "Học sinh")]
        [DataSourceProperty("HSList", DataSourcePropertyIsNullMode.SelectAll)]
        public HocSinh HocSinh
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


        public BaoCao_TuyenSinh_QuaTrinhHocSinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            param[1] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            param[2] = new SqlParameter("@HocSinh", HocSinh != null ? HocSinh.Oid : Guid.Empty);

            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_QuaTrinhHocCuaHocSinh", CommandType.StoredProcedure, param);
            //
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<HocSinh> HSList { get; set; }

        private void UpdateHSList()
        {
            if (HSList == null)
                HSList = new XPCollection<HocSinh>(Session);
            //
            HSList.Criteria = CriteriaOperator.Parse("Lop.Oid=?", Lop != null ? Lop.Oid : Guid.Empty);
        }


        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }

        private void UpdateLopList()
        {
            if (LopList == null)
                LopList = new XPCollection<Lop>(Session);
            //
            LopList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", CongTy != null ? CongTy.Oid : Guid.Empty);

        }
    }
}
