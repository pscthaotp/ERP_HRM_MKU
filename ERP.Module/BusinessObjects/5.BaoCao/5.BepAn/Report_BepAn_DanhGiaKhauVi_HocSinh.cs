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

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Đánh giá khẩu vị - học sinh")]
    public class Report_BepAn_DanhGiaKhauVi_HocSinh : StoreProcedureReport, ILop, ICongTy
    {

        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private Lop _Lop;
        private ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh _HocSinh;
        private NhomDinhDuong _NhomDinhDuong;
        private TuanHoc _TuanHoc;
        private DateTime _TuNgay;
        private DateTime _DenNgay;


        [ModelDefault("Caption", "Công Ty")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("LoaiTruong = 1 or LoaiTruong = 2")]
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
                    UpdateLopList();
                }
            }
        }

        [ModelDefault("Caption", "Năm Học")]
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
                if(!IsLoading && value != null)
                {
                    UpdateTuanHocList();
                }
            }
        }

        [ModelDefault("Caption", "Tuần học")]
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
                if(!IsLoading && value != null)
                {
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Nhóm dinh dưỡng")]
        public NhomDinhDuong NhomDinhDuong
        {
            get
            {
                return _NhomDinhDuong;
            }
            set
            {
                SetPropertyValue("NhomDinhDuong", ref _NhomDinhDuong, value);
            }
        }

        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }

        [Browsable(false)]
        public XPCollection<TuanHoc> TuanHocList { get; set; }

        [Browsable(false)]
        public XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> HocSinhList { get; set; }

        public Report_BepAn_DanhGiaKhauVi_HocSinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            TuanHoc = Common.GetCurrentTuanHoc(Session);
            if(Lop != null)
                UpdateHocSinhList();
            UpdateLopList();
            UpdateTuanHocList();
        }

        public void UpdateLopList()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("CongTy = ? AND GCRecord IS NULL", CongTy.Oid);
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

        public void UpdateHocSinhList()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("Lop = ? AND GCRecord IS NULL", Lop.Oid);
            XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh> DSHocSinh = new XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(Session, filter);
            if (HocSinhList != null)
            {
                HocSinhList.Reload();
            }
            else
            {
                HocSinhList = new XPCollection<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(Session, false);
            }
            foreach (ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh item2 in DSHocSinh)
            {
                HocSinhList.Add(item2);
            }
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@CongTy", CongTy.Oid);
            parameter[1] = new SqlParameter("@HocSinh", HocSinh != null ? HocSinh.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@NhomDinhDuong", NhomDinhDuong != null ? NhomDinhDuong.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            parameter[4] = new SqlParameter("@TuNgay", TuNgay);
            parameter[5] = new SqlParameter("@DenNgay", DenNgay);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BepAn_DanhGiaKhauVi_HocSinh", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
