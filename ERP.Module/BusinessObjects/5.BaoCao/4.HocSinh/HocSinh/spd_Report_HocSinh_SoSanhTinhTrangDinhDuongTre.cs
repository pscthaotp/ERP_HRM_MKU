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
    [ModelDefault("Caption", "Report - Học sinh - So sánh tình trạng dinh dưỡng trẻ")]
    public class spd_Report_HocSinh_SoSanhTinhTrangDinhDuongTre : StoreProcedureReport, ICongTy
    {

        private CongTy _CongTy;
        private Lop _Khoi;
        private Lop _Lop;
        private DateTime _Thang1;
        private DateTime _Thang2;

        [ModelDefault("Caption", "Công Ty")]
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
            }
        }

        [ModelDefault("Caption", "Tháng thứ nhất")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ImmediatePostData]
        public DateTime Thang1
        {
            get
            {
                return _Thang1;
            }
            set
            {
                SetPropertyValue("Thang1", ref _Thang1, value);
            }
        }

        [ModelDefault("Caption", "Tháng thứ hai")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ImmediatePostData]
        public DateTime Thang2
        {
            get
            {
                return _Thang2;
            }
            set
            {
                SetPropertyValue("Thang2", ref _Thang2, value);
            }
        }

        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }

        [Browsable(false)]
        public XPCollection<Lop> KhoiList { get; set; }


        public spd_Report_HocSinh_SoSanhTinhTrangDinhDuongTre(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = null;        
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

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@Khoi", Khoi != null ? Khoi.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@ThangThu1", Thang1);
            parameter[4] = new SqlParameter("@ThangThu2", Thang2);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocSinh_SoSanhTinhTrangDinhDuongTre", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }
}
