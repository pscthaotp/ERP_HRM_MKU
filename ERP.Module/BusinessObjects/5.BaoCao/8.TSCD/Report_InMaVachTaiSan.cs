using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.Commons;
using ERP.Module.BaoCao.Custom;
using ERP.Module.DanhMuc.TSCD;
using ERP.Module.NghiepVu.QuanLyKho.HangHoas;
using ERP.Module.NghiepVu.TSCD;

namespace ERP.Module.Report.TSCD
{
    [NonPersistent]
    [ModelDefault("Caption", "In mã vạch tài sản cố định cá biệt - TSCD")]
    public class Report_InMaVachTaiSan : StoreProcedureReport
    {
        private ViTriPhongBan _viTri;
        private HangHoa _hangHoa;
        private bool _GhiTang;
        private string _TSCD;

        [Browsable(false)]
        [Size(-1)]
        public string TSCD
        {
            get
            {
                return _TSCD;
            }
            set
            {
                SetPropertyValue("TSCD", ref _TSCD, value);
            }
        }

        [Browsable(false)]
        public bool GhiTang
        {
            get
            {
                return _GhiTang;
            }
            set
            {
                SetPropertyValue("GhiTang", ref _GhiTang, value);
            }
        }

        [ModelDefault("Caption", "Chọn vị trí đặt để")]       
        //[RuleRequiredField(DefaultContexts.Save)]
        public ViTriPhongBan ViTri
        {
            get
            {
                return _viTri;
            }
            set
            {
                SetPropertyValue("ViTri", ref _viTri, value);
            }
        }

        [DataSourceProperty("taiSanCoDinhList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Chọn nhóm loại TSCĐ:")]     
        //[RuleRequiredField(DefaultContexts.Save)]
        public HangHoa HangHoa
        {
            get
            {
                return _hangHoa;
            }
            set
            {
                SetPropertyValue("HangHoa", ref _hangHoa, value);
            }
        }

        //danh sách tài sản cố định
        XPCollection<HangHoa> taiSanCoDinhList
        {
            get;
            set;
        }

        public Report_InMaVachTaiSan(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            if (!GhiTang)
            {
                SqlParameter[] param = new SqlParameter[3];
                if (ViTri == null)
                    param[0] = new SqlParameter("@OidViTri", Guid.Empty.ToString());
                else
                    param[0] = new SqlParameter("@OidViTri", _viTri.Oid.ToString());
                if (_hangHoa == null)
                    _hangHoa = Session.GetObjectByKey<HangHoa>(Guid.Parse("93DACA84-497E-4809-9AD4-18A04C04D14B"));
                param[1] = new SqlParameter("@OidHangHoa", _hangHoa.Oid.ToString());
                param[2] = new SqlParameter("@CongTy", Common.CongTy(Session).Oid.ToString());
                //
                SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TSCD_InMaVachTaiSanCoDinhCaBiet", CommandType.StoredProcedure, param);
                //
                return cmd;
            }
            else
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@TSCD", TSCD);
                //
                SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TSCD_InMaVachTaiSanTuGhiTang", CommandType.StoredProcedure, param);
                //
                return cmd;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (!IsLoading)
            {
                taiSanCoDinhList = new XPCollection<HangHoa>(Session);
                //
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=?", "93DACA84-497E-4809-9AD4-18A04C04D14B");
                taiSanCoDinhList.Criteria = filter;
            }
        }
    }
}
