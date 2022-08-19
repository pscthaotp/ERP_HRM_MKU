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
    [ModelDefault("Caption", "Theo dõi tuổi nợ (Full) - Học phí")]
    public class Report_HocPhi_ThoiDoiTuoiNo_FullHeThong : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;
        private DateTime _KyTinhHocPhi;

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
                
            }
        }
        //[ModelDefault("Caption", "Khối")]
        //[DataSourceProperty("KhoiList")]
        //[ImmediatePostData]
        //public Khoi Khoi
        //{
        //    get
        //    {
        //        return _Khoi;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Khoi", ref _Khoi, value);
        //        if (!IsLoading)
        //        {
        //            if (Khoi != null)
        //            {
        //                UpdateLopList();
        //            }
        //            else
        //                Lop = null;
        //        }
        //    }
        //}
        //[ModelDefault("Caption", "Lớp")]
        //[DataSourceProperty("LopList")]
        //[ImmediatePostData]
        //public Lop Lop
        //{
        //    get
        //    {
        //        return _Lop;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Lop", ref _Lop, value);
        //    }
        //}
        [ModelDefault("Caption", "Kỳ tính học phí")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Editmask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime KyTinhHocPhi
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

        public Report_HocPhi_ThoiDoiTuoiNo_FullHeThong(Session session) : base(session) { }

      
        //[Browsable(false)]
        //public XPCollection<Khoi> KhoiList { get; set; }
        //protected void UpdateKhoiList()
        //{
        //    if (KhoiList == null)
        //        KhoiList = new XPCollection<Khoi>(Session);
        //    KhoiList.Criteria = CriteriaOperator.Parse("CongTy =? and LoaiLop =?", CongTy.Oid, LoaiLopEnum.Khoi);
        //}
        //[Browsable(false)]
        //public XPCollection<Lop> LopList { get; set; }
        //protected void UpdateLopList()
        //{
        //    if (LopList == null)
        //        LopList = new XPCollection<Lop>(Session);
        //    LopList.Criteria = CriteriaOperator.Parse("CongTy =? and LopCha =?", CongTy.Oid, Khoi.Oid);
        //}
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            KyTinhHocPhi = DateTime.Now;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@KyTinhHocPhi", KyTinhHocPhi.Date);

            SqlCommand cmd = DataProvider.GetCommand("spd_rpt_HocPhi_TuoiNo", System.Data.CommandType.StoredProcedure, parameter);
            //SqlCommand cmd = DataProvider.GetCommand("spd_HocPhi_rpt_DanhSachChuaDong_DanhSachDaDong_ChiTietThu", System.Data.CommandType.StoredProcedure, parameter);
            //cmd.CommandTimeout = 180; 

            return cmd;
        }
    }

}
