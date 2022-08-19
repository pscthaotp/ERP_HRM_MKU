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

namespace ERP.Module.Report.CCDC
{
    [NonPersistent]
    [ModelDefault("Caption", "In mã vạch công cụ dụng cụ cá biệt - CCDC")]
    public class Report_InMaVachCCDC : StoreProcedureReport
    {
        private ViTriPhongBan _viTri;
        private HangHoa _hangHoa;
        private bool _GhiTang;
        private string _CCDC;
        private string _TSCD;

        [Size(-1)]
        [Browsable(false)]
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

        [Size(-1)]
        [Browsable(false)]
        public string CCDC
        {
            get
            {
                return _CCDC;
            }
            set
            {
                SetPropertyValue("CCDC", ref _CCDC, value);
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

        [DataSourceProperty("congCuDungCuList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Chọn nhóm loại công cụ dụng cụ:")]     
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

        //danh sách công cụ dụng cụ
        XPCollection<HangHoa> congCuDungCuList
        {
            get;
            set;
        }

        public Report_InMaVachCCDC(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            if (!GhiTang)
            {
                if (CCDC != null && CCDC.Length > 0)
                {
                    SqlParameter[] param = new SqlParameter[3];
                    if (ViTri == null)
                        param[0] = new SqlParameter("@OidViTri", Guid.Empty.ToString());
                    else
                        param[0] = new SqlParameter("@OidViTri", _viTri.Oid.ToString());
                    if (_hangHoa == null)
                        _hangHoa = Session.GetObjectByKey<HangHoa>(Guid.Parse("A95D06E3-78F4-4223-BCF9-A9E659F59ACF"));
                    param[1] = new SqlParameter("@OidHangHoa", _hangHoa.Oid.ToString());
                    param[2] = new SqlParameter("@CongTy", Common.CongTy(Session).Oid.ToString());
                    //
                    SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_CCDC_InMaVachCongCuDungCuCaBiet", CommandType.StoredProcedure, param);
                    //
                    return cmd;
                }
                else
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
            }
            else
            {
                if (CCDC != null && CCDC.Length > 0)
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@CCDC", CCDC);
                    //
                    SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_CCDC_InMaVachCongCuDungCuTuGhiTang", CommandType.StoredProcedure, param);
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
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (!IsLoading)
            {
                congCuDungCuList = new XPCollection<HangHoa>(Session);
                //
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=?", "A95D06E3-78F4-4223-BCF9-A9E659F59ACF");
                congCuDungCuList.Criteria = filter;
            }
        }
    }
}
