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

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Doanh thu các khoản học phí (Tháng) - Học phí")]
    public class Report_HocPhi_DoanhThuCacKhoanHocPhiThang : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private KyTinhHocPhi _KyTinhHocPhi;

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
                if (!IsLoading)
                {
                    if (CongTy != null)
                    {
                        UpdateKyTinhHocPhi();
                        KyTinhHocPhi = Session.FindObject<KyTinhHocPhi>(CriteriaOperator.Parse("Thang =? and Nam =? and CongTy =?", DateTime.Now.Month, DateTime.Now.Year, CongTy.Oid));
                    }
                    else
                        KyTinhHocPhiList.Reload();
                }
            }
        }

        [ModelDefault("Caption", "Kỳ tính học phí")]
        [DataSourceProperty("KyTinhHocPhiList", DataSourcePropertyIsNullMode.SelectNothing)]
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

        public Report_HocPhi_DoanhThuCacKhoanHocPhiThang(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhHocPhi> KyTinhHocPhiList { get; set; }
        public void UpdateKyTinhHocPhi()
        {
            if (KyTinhHocPhiList == null)
                KyTinhHocPhiList = new XPCollection<KyTinhHocPhi>(Session);
            //
            KyTinhHocPhiList.Criteria = CriteriaOperator.Parse("CongTy = ?", CongTy.Oid);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            //UpdateKyTinhHocPhi();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@KyTinhHocPhi", KyTinhHocPhi.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocPhi_DoanhThuCacKhoanHocPhiThang", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
