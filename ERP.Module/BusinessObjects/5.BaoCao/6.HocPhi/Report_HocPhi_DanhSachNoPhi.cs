using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocPhi.BienLai;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using ERP.Module.NghiepVu.HocPhi.PhieuChi;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.Enum.HocSinh;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách nợ phí - Học phí")]
    public class Report_HocPhi_DanhSachNoPhi : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private Khoi _Khoi;
        private Lop _Lop;

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [DataSourceCriteria("LoaiTruong = 1")]
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
                        UpdateKhoiList();
                    }
                    else Khoi = null;
                }
            }
        }
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
                        UpdateLopList();
                    }
                    else Lop = null;
                }
            }
        }
        [ModelDefault("Caption", "Lớp")]
        [DataSourceProperty("LopList")]
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
        [Browsable(false)]
        public XPCollection<Khoi> KhoiList { get; set; }
        protected void UpdateKhoiList()
        {
            if (KhoiList == null)
                KhoiList = new XPCollection<Khoi>(Session);
            KhoiList.Criteria = CriteriaOperator.Parse("CongTy =? and LoaiLop =?", CongTy.Oid, LoaiLopEnum.Khoi);
        }
        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }

        protected void UpdateLopList()
        {
            if (LopList == null)
                LopList = new XPCollection<Lop>(Session);
            LopList.Criteria = CriteriaOperator.Parse("CongTy =? and LopCha =?", CongTy.Oid, Khoi.Oid);
        }

        public Report_HocPhi_DanhSachNoPhi(Session session) : base(session) { }

    
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@CongTy", CongTy.Oid);
            parameter[1] = new SqlParameter("@Khoi", Khoi!=null?Khoi.Oid:Guid.Empty);
            parameter[2] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@NamHoc", Guid.Empty);// Common.GetCurrentNamHoc(Session));
            SqlCommand cmd = DataProvider.GetCommand("spd_HocPhi_LayDanhSachPhi_NoPhi_Full", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
