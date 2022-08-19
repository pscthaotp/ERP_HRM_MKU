using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.NghiepVu.TKB.ChuongTrinhTiengAnh;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.ThoiKhoaBieu
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Chương trình tiếng anh")]
    public class Report_CTTiengAnh : StoreProcedureReport
    {
        private QuanLyCTTiengAnh _QuanLyCTTiengAnh;
        //private Lop _Lop;
        //private NamHoc _NamHoc;
        private TuanHoc _TuanHoc;

        [ModelDefault("Caption", "Quản lý tiếng anh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuanLyCTTiengAnh QuanLyCTTiengAnh
        {
            get
            {
                return _QuanLyCTTiengAnh;
            }
            set
            {
                SetPropertyValue("QuanLyCTTiengAnh", ref _QuanLyCTTiengAnh, value);
                if (value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc =?", value.NamHoc.Oid);
                    DSTuan = new XPCollection<TuanHoc>(Session, filter);
                }
            }
        }
        //[ImmediatePostData]
        //[ModelDefault("Caption", "Năm học")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public NamHoc NamHoc
        //{
        //    get
        //    {
        //        return _NamHoc;
        //    }
        //    set
        //    {
        //        SetPropertyValue("NamHoc", ref _NamHoc, value);
        //        if (!IsLoading && value != null)
        //        {
        //            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid);
        //            DSTuan = new XPCollection<TuanHoc>(Session, filter);
        //        }
        //    }
        //}

        //[ModelDefault("Caption", "Lớp")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tuần học")]
        [DataSourceProperty("DSTuan")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [ModelDefault("Caption", "Danh sách tuần")]
        public XPCollection<TuanHoc> DSTuan { get; set; }

        public Report_CTTiengAnh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            TuanHoc = Common.GetCurrentTuanHoc(Session);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@TuanHoc", TuanHoc.Oid);
            parameter[1] = new SqlParameter("@Lop", QuanLyCTTiengAnh.Lop.Oid);
            parameter[2] = new SqlParameter("@NamHoc", QuanLyCTTiengAnh.NamHoc.Oid);
            parameter[3] = new SqlParameter("@CapDuyet", false);
            SqlCommand cmd = DataProvider.GetCommand("spd_Web_MamNon_CTTiengAnh_NoiDung", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
