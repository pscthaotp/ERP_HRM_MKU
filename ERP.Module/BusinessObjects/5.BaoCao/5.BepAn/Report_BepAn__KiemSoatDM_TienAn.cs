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
    [ModelDefault("Caption", "Bếp ăn - Kiểm soát định mức tiền ăn")]
    public class Report_BepAn__KiemSoatDM_TienAn : StoreProcedureReport, ICongTy
    {

        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private NhomDinhDuong _NhomDinhDuong;
        private TuanHoc _TuanHoc;
        private DateTime _TuNgay;
        private DateTime _DenNgay;


        [ModelDefault("Caption", "Công Ty")]
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
        public XPCollection<TuanHoc> TuanHocList { get; set; }


        public Report_BepAn__KiemSoatDM_TienAn(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = Common.GetCurrentNamHoc(Session);
            TuanHoc = Common.GetCurrentTuanHoc(Session);
            UpdateTuanHocList();
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
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@NhomDinhDuong", NhomDinhDuong != null ? NhomDinhDuong.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@TuNgay", TuNgay);
            parameter[3] = new SqlParameter("@DenNgay", DenNgay);
            parameter[4] = new SqlParameter("@SecuritySystemUser", Common.SecuritySystemUser_GetCurrentUser().Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_KhoBep_KiemSoatDM_TienAn", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
