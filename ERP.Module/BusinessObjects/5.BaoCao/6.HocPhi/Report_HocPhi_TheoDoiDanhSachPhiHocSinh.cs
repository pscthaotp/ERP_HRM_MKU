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
    [ModelDefault("Caption", "Theo dõi tuổi nợ")]
    public class Report_HocPhi_TheoDoiDanhSachPhiHocSinh : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private Khoi _Khoi;
        private Lop _Lop;
        private DateTime _ThangNam;

        [ModelDefault("Caption", "Trường")]
        [ImmediatePostData]
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
                if(!IsLoading )
                {
                    if (CongTy != null)
                    {
                        //Khoi = Session.FindObject<Khoi>(CriteriaOperator.Parse("CongTy =?", CongTy.Oid));
                        UpdateKhoiList();
                    }
                    else
                        Khoi = null;
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
        [ModelDefault("Editmask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime ThangNam
        {
            get
            {
                return _ThangNam;
            }
            set
            {
                SetPropertyValue("ThangNam", ref _ThangNam, value);
            }
        }

        public Report_HocPhi_TheoDoiDanhSachPhiHocSinh(Session session) : base(session) { }
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
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            ThangNam = DateTime.Now;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@ThangNam", ThangNam);
            parameter[2] = new SqlParameter("@Khoi", Khoi != null ? Khoi.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);

            SqlCommand cmd = DataProvider.GetCommand("spd_HocPhi_rpt_TheoDoiDanhSachPhiHocSinh", System.Data.CommandType.StoredProcedure, parameter);
            //cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
