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
    [ModelDefault("Caption", "Danh sách hoàn phí - miễn giảm (Từ ngày - Đến ngày)")]
    public class Report_HocPhi_HoanPhiCacKhoanThu : StoreProcedureReport
    {
        // Fields...
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private CongTy _CongTy;

        [ModelDefault("Caption", "Từ ngày")]
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

        [ModelDefault("Caption", "Trường")]
        [DataSourceProperty("TruongList", DataSourcePropertyIsNullMode.SelectNothing)]
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
            }
        }

        public Report_HocPhi_HoanPhiCacKhoanThu(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<CongTy> TruongList { get; set; }
        public void UpdateTruong()
        {
            if (TruongList == null)
                TruongList = new XPCollection<CongTy>(Session);
            //
            TruongList.Criteria = CriteriaOperator.Parse("LoaiBoPhan = ?", LoaiBoPhanEnum.CongTy);
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuNgay = DateTime.Now.SetTime(Enum.Systems.SetTimeEnum.StartMonth);
            DenNgay = DateTime.Now.SetTime(Enum.Systems.SetTimeEnum.EndMonth);
            UpdateTruong();
            CongTy = Common.CongTy(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@TuNgay", TuNgay.SetTime(Enum.Systems.SetTimeEnum.StartDay));
            parameter[1] = new SqlParameter("@DenNgay", DenNgay.SetTime(Enum.Systems.SetTimeEnum.EndDay));
            parameter[2] = new SqlParameter("@CongTy", CongTy.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocPhi_HoanPhiCacKhoanThu", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
