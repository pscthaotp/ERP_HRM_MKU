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
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;
using ERP.Module.NghiepVu.TienLuong.ChungTus;

namespace ERP.Module.Report.TienLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng trích nộp thuế TNCN - Tiền lương")]
    public class BaoCao_Luong_BangTrichNopThueTNCN : StoreProcedureReport, ICongTy
    {
        private CongTy _CongTy;
        private ChungTu _ChungTu;

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty / Trường")]
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
                    UpdateChungTuList();
                }
            }
        }

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("ChungTuList", DataSourcePropertyIsNullMode.SelectNothing)]
        public ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }
        public BaoCao_Luong_BangTrichNopThueTNCN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }

        public override SqlCommand CreateCommand()
        {
            //
            SqlCommand cmd = new SqlCommand("spd_Rpt_Luong_BangTrichNopThueTNCN");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;       
            cmd.Parameters.AddWithValue("@ChungTu", ChungTu.Oid);
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<ChungTu> ChungTuList { get; set; }

        private void UpdateChungTuList()
        {
            if (ChungTuList == null)
                ChungTuList = new XPCollection<ChungTu>(Session);
            //
            ChungTuList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", CongTy != null ? CongTy.Oid : Guid.Empty);
        }
    }
}
