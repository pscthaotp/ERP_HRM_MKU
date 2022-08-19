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
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông báo nhập học - Tuyển sinh")]
    public class BaoCao_TuyenSinh_ThongBaoNhapHoc : StoreProcedureReport, ICongTy
    {
        //
        private CongTy _CongTy;
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
                    //
                    UpdateHSNHList();
                }
            }
        }

        [ModelDefault("Caption", "Học sinh")]
        [DataSourceProperty("HSList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
      
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

        public BaoCao_TuyenSinh_ThongBaoNhapHoc(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //CongTy = Common.CongTy(Session);
            //
            UpdateHSNHList();
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Oid", CongTy.Oid);

            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_ThongBaoNhapHoc", CommandType.StoredProcedure, param);
            //
            return cmd;
        }


        [Browsable(false)]
        public XPCollection<HocSinh> HSList { get; set; }

        private void UpdateHSNHList()
        {
            if (HSList == null)
                HSList = new XPCollection<HocSinh>(Session);
            //
            HSList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", CongTy!= null ? CongTy.Oid : Guid.Empty);
        }
    }
}
