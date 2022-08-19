using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.Enum.HocSinh;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Theo dõi tuổi nợ - Học phí")]
    public class Report_HocPhi_ThoiDoiTuoiNo : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;
        private HeDaoTao _HeDaoTao;
        private Khoi _Khoi;
        private Lop _Lop;
        private KyTinhHocPhi _KyTinhHocPhi;

        #region CongTy
        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [DataSourceCriteria("LoaiTruong = 1")]
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
                    if (CongTy != null)
                    {
                        HeDaoTao = CongTy.HeDaoTao;
                        UpdateKyTinhHocPhi();
                        //Khoi = Session.FindObject<Khoi>(CriteriaOperator.Parse("CongTy =?", CongTy.Oid));
                        KyTinhHocPhi = Session.FindObject<KyTinhHocPhi>(CriteriaOperator.Parse("CongTy =? and Thang =? and Nam =?", CongTy.Oid, DateTime.Now.Month, DateTime.Now.Year));
                    }
                    else
                    {
                        KyTinhHocPhiList.Reload();
                        HeDaoTao = null;
                    }
                }
            }
        }
        #endregion

        #region Hệ đào tạo
        [ModelDefault("Caption", "Hệ đào tạo")]
        [ImmediatePostData]
        public HeDaoTao HeDaoTao
        {
            get
            {
                return _HeDaoTao;
            }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
                if (!IsLoading)
                {
                    Khoi = null;
                    UpdateKhoiList();
                }
            }
        }
        #endregion

        [ModelDefault("Caption", "Khối")]
        [DataSourceProperty("KhoiList")]
        [ImmediatePostData]
        public Khoi Khoi
        {
            get
            {
                return _Khoi;
            }
            set
            {
                SetPropertyValue("Khoi", ref _Khoi, value);
                if (!IsLoading)
                {
                    if (Khoi != null)
                    {
                        //Lop = Session.FindObject<Lop>(CriteriaOperator.Parse("LopCha =?", Khoi.Oid));
                        UpdateLopList();
                    }
                    else
                        Lop = null;
                }
            }
        }
        [ModelDefault("Caption", "Lớp")]
        [DataSourceProperty("LopList")]
        [ImmediatePostData]
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
        [ModelDefault("Caption", "Kỳ tính học phí")]
        [DataSourceProperty("KyTinhHocPhiList", DataSourcePropertyIsNullMode.SelectNothing)]
        [RuleRequiredField(DefaultContexts.Save)]
        public KyTinhHocPhi KyTinhHocPhi
        {
            get
            {
                return _KyTinhHocPhi;
            }
            set
            {
                SetPropertyValue("KyTinhHocPhi", ref _KyTinhHocPhi, value);
            }
        }

        public Report_HocPhi_ThoiDoiTuoiNo(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhHocPhi> KyTinhHocPhiList { get; set; }
        public void UpdateKyTinhHocPhi()
        {
            if (KyTinhHocPhiList == null)
                KyTinhHocPhiList = new XPCollection<KyTinhHocPhi>(Session);
            //
            KyTinhHocPhiList.Criteria = CriteriaOperator.Parse("CongTy = ?", CongTy.Oid);
        }
        [Browsable(false)]
        public XPCollection<Khoi> KhoiList { get; set; }
        protected void UpdateKhoiList()
        {
            KhoiList = new XPCollection<Khoi>(Session);
            if (HeDaoTao != null)
                KhoiList.Criteria = CriteriaOperator.Parse("CongTy =? and LoaiLop =? and HeDaoTao =?", CongTy.Oid, LoaiLopEnum.Khoi, HeDaoTao.Oid);
            OnChanged("Khoi");
        }
        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }
        protected void UpdateLopList()
        {
            if (LopList == null)
                LopList = new XPCollection<Lop>(Session);
            LopList.Criteria = CriteriaOperator.Parse("CongTy =? and LopCha =?", CongTy.Oid, Khoi.Oid);
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
          
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@KyTinhHocPhi", KyTinhHocPhi.Oid);
            parameter[1] = new SqlParameter("@Khoi", Khoi != null ? Khoi.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@TuoiNo", true);
            parameter[4] = new SqlParameter("@HeDaoTao", HeDaoTao != null ? HeDaoTao.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_HocPhi_DanhSachThuPhi", System.Data.CommandType.StoredProcedure, parameter);
            //SqlCommand cmd = DataProvider.GetCommand("spd_HocPhi_rpt_DanhSachChuaDong_DanhSachDaDong_ChiTietThu", System.Data.CommandType.StoredProcedure, parameter);
            //cmd.CommandTimeout = 180; 

            return cmd;
        }
    }

}
