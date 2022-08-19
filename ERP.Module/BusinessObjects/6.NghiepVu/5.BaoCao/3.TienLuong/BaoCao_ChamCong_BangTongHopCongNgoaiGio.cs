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
using ERP.Module.Enum.Systems;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.Report.TienLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng tổng hợp công ngoài giờ - Tiền lương")]
    public class BaoCao_ChamCong_BangTongHopCongNgoaiGio : StoreProcedureReport
    {
        private CongTy _CongTy;
        private CC_KyChamCong _KyChamCong;
        private BoPhan _BoPhan;

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    UpdateKCCList();
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Kỳ chấm công")]
        [DataSourceProperty("KCCList", DataSourcePropertyIsNullMode.SelectAll)]
        public CC_KyChamCong KyChamCong
        {
            get
            {
                return _KyChamCong;
            }
            set
            {
                SetPropertyValue("KyChamCong", ref _KyChamCong, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        public BaoCao_ChamCong_BangTongHopCongNgoaiGio(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            KyChamCong = Common.GetKyChamCong_ByDate(Session,NgayLapBaoCao);
            //
            CongTy = Common.CongTy(Session);
        }

        public override SqlCommand CreateCommand()
        {
            //
            SqlCommand cmd = new SqlCommand("spd_Rpt_ChamCong_BangTongHopCongNgoaiGio");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@KyChamCong", KyChamCong.Oid);
            cmd.Parameters.AddWithValue("@BoPhan", BoPhan  != null ? BoPhan.Oid : Guid.Empty);
            cmd.Parameters.AddWithValue("@CongTy", CongTy.Oid);
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<CC_KyChamCong> KCCList { get; set; }

        private void UpdateKCCList()
        {
            if (KCCList == null)
                KCCList = new XPCollection<CC_KyChamCong>(Session);
            //
            KCCList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", CongTy != null ? CongTy.Oid : Guid.Empty);

        }
    }
}
