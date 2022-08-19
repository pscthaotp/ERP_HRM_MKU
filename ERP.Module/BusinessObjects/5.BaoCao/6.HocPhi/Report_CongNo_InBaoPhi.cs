using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocPhi.BienLai;
using ERP.Module.NghiepVu.HocPhi.BangCongNos;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.HocPhi;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "In phiếu báo - Học phí")]
    public class Report_CongNo_InBaoPhi : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private KyTinhHocPhi _KyTinhHocPhi;

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
                if(!IsLoading && CongTy!=null)
                {
                    if (NamHoc != null)
                        UpdateKyTinhHocPhiList();
                }
            }
        }

        [ModelDefault("Caption", "Năm học")]
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
                if(!IsLoading && NamHoc!=null)
                {
                    UpdateKyTinhHocPhiList();
                }
            }
        }

        [ModelDefault("Caption", "Kỳ tính học phí")]
        [DataSourceProperty("KyTinhHocPhiList", DataSourcePropertyIsNullMode.SelectAll)]
        public KyTinhHocPhi KyTinhHocPhi
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
       

        public Report_CongNo_InBaoPhi(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhHocPhi> KyTinhHocPhiList { get; set; }

        public void UpdateKyTinhHocPhiList()
        {
            if (KyTinhHocPhiList == null)
                KyTinhHocPhiList = new XPCollection<KyTinhHocPhi>(Session);
            KyTinhHocPhiList.Criteria = CriteriaOperator.Parse("NamHoc = ? and CongTy = ?", NamHoc.Oid, CongTy.Oid);
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@KyTinhHocPhi", KyTinhHocPhi.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocPhi_InPhieuBaoHocPhi", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
