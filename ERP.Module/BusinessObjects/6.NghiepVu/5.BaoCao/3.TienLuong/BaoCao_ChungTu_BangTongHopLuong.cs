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
using ERP.Module.NghiepVu.TienLuong.ChungTus;


namespace ERP.Module.Report.TienLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo : Bảng tổng hợp lương")]
    public class BaoCao_ChungTu_BangTongHopLuong : StoreProcedureReport
    {
        //
        private ChungTu _ChungTu;
        private CongTy _CongTy;
        private bool _TatCa = true;
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
                    UpdateKTLList();
                }
            }
        }

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
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCa")]
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

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField(DefaultContexts.Save)]
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




        public BaoCao_ChungTu_BangTongHopLuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            List<string> listBP = new List<string>();
            //
            if (TatCa)
                listBP = Common.Department_GetRoledDepartmentList_ByCurrentUser();
            else
                listBP = Common.Department_GetRoledDepartmentList_ByDepartment(BoPhan);
            //
            StringBuilder roled = new StringBuilder();
            foreach (string item in listBP)
            {
                roled.Append(String.Format("{0};", item));
            }
            //
            SqlCommand cmd = new SqlCommand("spd_Rpt_ChungTu_BangTongHopLuongThang", (SqlConnection)Session.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            //
            cmd.Parameters.AddWithValue("@ChungTu", ChungTu != null ? ChungTu.Oid : Guid.Empty);
            cmd.Parameters.AddWithValue("@BoPhanPhanQuyen", roled.ToString());
            cmd.Parameters.AddWithValue("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);

            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //ThangThuHai = Common.GetServerCurrentTime().Date;
            //ThangThuNhat = ThangThuHai.AddMonths(-1);
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
