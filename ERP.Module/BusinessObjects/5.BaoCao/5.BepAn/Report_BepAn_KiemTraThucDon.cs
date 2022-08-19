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
    [ModelDefault("Caption", "Bếp ăn - Kiểm tra thực đơn")]
    public class Report_BepAn_KiemTraThucDon : StoreProcedureReport, ICongTy
    {

        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private NhomDinhDuong _NhomDinhDuong;
        private TuanHoc _TuanHoc;

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
            }
        }

        [ModelDefault("Caption", "Năm Học")]
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


        public Report_BepAn_KiemTraThucDon(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = Common.GetCurrentNamHoc(Session);
            CongTy = Common.CongTy(Session);
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
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@NhomDinhDuong", NhomDinhDuong != null ? NhomDinhDuong.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@TuanHoc", TuanHoc != null ? TuanHoc.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_KiemTraThucDon", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
