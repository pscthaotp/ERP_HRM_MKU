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
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.TienLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng thanh toán lương tiền mặt - Tiền lương")]
    public class BaoCao_ChungTu_BangThanhToanLuongTienMat : StoreProcedureReport
    {
        private KyTinhLuong _KyTinhLuong;
        private CongTy _CongTy;

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
                    UpdateKTLList();
                }
            }
        }

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("KTLList", DataSourcePropertyIsNullMode.SelectAll)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }
       
        public BaoCao_ChungTu_BangThanhToanLuongTienMat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            KyTinhLuong = Common.GetKyTinhLuong_ByDate(Session,NgayLapBaoCao);
            //
            CongTy = Common.CongTy(Session);
        }

        public override SqlCommand CreateCommand()
        {
            //
            SqlCommand cmd = new SqlCommand("spd_Rpt_ChungTu_BangThanhToanLuongTienMat");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;          
            cmd.Parameters.AddWithValue("@KyTinhLuong", KyTinhLuong.Oid);
            cmd.Parameters.AddWithValue("@CongTy", CongTy.Oid);
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KTLList { get; set; }

        private void UpdateKTLList()
        {
            if (KTLList == null)
                KTLList = new XPCollection<KyTinhLuong>(Session);
            //
            KTLList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", CongTy != null ? CongTy.Oid : Guid.Empty);

        }
    }
}
