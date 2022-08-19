using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.BepAn;
using ERP.Module.DanhMuc.TKB;
using ERP.Module.Enum.BepAn;
using ERP.Module.NghiepVu.BepAn.KhoBep;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocSinh.Lops;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.DanhMuc.HocSinh;

namespace ERP.Module.Report.HocSinh
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Report - Học sinh - Chi tiết đánh giá chỉ số học sinh theo tuần")]
    public class Report_HocSinh_ChiTietDanhGiaChiSoHocSinhTheoTuan : StoreProcedureReport, ICongTy
    {

        private CongTy _CongTy;
        private Lop _Khoi;
        private Lop _Lop;
        private NamHoc _NamHoc;
        private TuanHoc _TuanHoc;
        private ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh _HocSinh;

        [ModelDefault("Caption", "Công Ty")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading && value != null)
                {
                    UpdateKhoiList();
                }
            }
        }

        [ModelDefault("Caption", "Khối")]
        [DataSourceProperty("KhoiList")]
        [ImmediatePostData]
        public Lop Khoi
        {
            get
            {
                return _Khoi;
            }
            set
            {
                SetPropertyValue("Khoi", ref _Khoi, value);
                if(!IsLoading && value != null)
                {
                    UpdateLopList();
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
                if (!IsLoading && value != null)
                {
                    UpdateHocSinhList();
                }
            }
        }

        [ModelDefault("Caption", "Học sinh")]
        [DataSourceProperty("HocSinhList")]
        [ImmediatePostData]
        public ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh HocSinh
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

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    UpdateTuanHocList();
                }              
            }
        }

        [ModelDefault("Caption", "Tuần học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("TuanHocList")]
        [ImmediatePostData]
        public TuanHoc TuanHoc
        {
            get
            {
                return _TuanHoc;
            }
            set
            {
                SetPropertyValue("TuanHoc", ref _TuanHoc, value);
            }
        }

        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }

        [Browsable(false)]
        public XPCollection<Lop> KhoiList { get; set; }
        [Browsable(false)]
        public XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> HocSinhList { get; set; }
        [Browsable(false)]
        public XPCollection<TuanHoc> TuanHocList { get; set; }


        public Report_HocSinh_ChiTietDanhGiaChiSoHocSinhTheoTuan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            TuanHoc = Common.GetCurrentTuanHoc(Session);
        }

        public void UpdateKhoiList()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("CongTy = ? AND GCRecord IS NULL AND LoaiLop = 0", CongTy.Oid);
            XPCollection<Lop> DSKhoi = new XPCollection<Lop>(Session, filter);
            if (KhoiList != null)
            {
                KhoiList.Reload();
            }
            else
            {
                KhoiList = new XPCollection<Lop>(Session, false);
            }
            foreach (Lop item in DSKhoi)
            {
                KhoiList.Add(item);
            }
        }

        public void UpdateLopList()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("LopCha = ? AND GCRecord IS NULL AND LoaiLop = 1", Khoi.Oid);
            XPCollection<Lop> DSLop = new XPCollection<Lop>(Session, filter);
            if (LopList != null)
            {
                LopList.Reload();
            }
            else
            {
                LopList = new XPCollection<Lop>(Session, false);
            }
            foreach (Lop item in DSLop)
            {
                LopList.Add(item);
            }
        }

        public void UpdateHocSinhList()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("GCRecord IS NULL AND Lop = ? AND CongTy = ?", Lop.Oid, CongTy.Oid);
            XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> DSHocSinh = new XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(Session, filter);
            if (HocSinhList != null)
            {
                HocSinhList.Reload();
            }
            else
            {
                HocSinhList = new XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(Session, false);
            }
            foreach (ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh item in DSHocSinh)
            {
                HocSinhList.Add(item);
            }
        }

        public void UpdateTuanHocList()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ? AND GCRecord IS NULL", NamHoc.Oid);
            XPCollection<TuanHoc> DSTuan = new XPCollection<TuanHoc>(Session, filter);
            if (TuanHocList != null)
            {
                TuanHocList.Reload();
            }
            else
            {
                TuanHocList = new XPCollection<TuanHoc>(Session, false);
            }
            foreach (TuanHoc item1 in DSTuan)
            {
                TuanHocList.Add(item1);
            }
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@Khoi", Khoi != null ? Khoi.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);
            parameter[4] = new SqlParameter("@TuanHoc", TuanHoc != null ? TuanHoc.Oid : Guid.Empty);
            parameter[5] = new SqlParameter("@HocSinh", HocSinh != null ? HocSinh.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocSinh_ChiTietDanhGiaChiSoHocSinhTheoTuan", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
