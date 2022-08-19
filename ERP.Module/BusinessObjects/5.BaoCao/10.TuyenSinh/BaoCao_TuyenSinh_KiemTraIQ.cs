using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.Systems;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Kiểm tra IQ - Tuyển sinh")]
    [Appearance("KiemTraIQ.TatCaTruong", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_KiemTraIQ : StoreProcedureReport, ICongTy
    {
        //
        private bool _TatCa = true;
        private CongTy _CongTy;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả")]
        public bool TatCa
        {
            get
            {
                return _TatCa;
            }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
                if (!IsLoading)
                {
                    CongTy = null;
                }
            }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save,TargetCriteria ="!TatCa")]
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

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [RuleRequiredField(DefaultContexts.Save)]
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
        public BaoCao_TuyenSinh_KiemTraIQ(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //CongTy = Common.CongTy(Session);
            //
            TuNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartMonth);
            DenNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.EndMonth);

        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CongTy", TatCa ? Guid.Empty : CongTy.Oid);
            param[1] = new SqlParameter("@TuNgay", TuNgay);
            param[2] = new SqlParameter("@DenNgay", DenNgay);

            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_KiemTraIQ", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
