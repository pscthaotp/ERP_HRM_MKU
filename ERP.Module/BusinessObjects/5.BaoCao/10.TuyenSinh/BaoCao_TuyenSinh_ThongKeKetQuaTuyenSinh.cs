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
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Thống kê kết quả tuyển sinh - Tuyển sinh")]
    [Appearance("ThongKeKetQuaTuyenSinh.TatCaTruong", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]

    [Appearance("CongTy", TargetItems = "TatCa", Enabled = false, Criteria = "EditCongTy")]
    public class BaoCao_TuyenSinh_ThongKeKetQuaTuyenSinh : StoreProcedureReport, ICongTy
    {
        //
        private bool _TatCa = true;
        private CongTy _CongTy;
        private NamHoc _NamHoc;

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
                    if (TatCa)
                        CongTy = null;
                }
            }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCa")]
        [DataSourceCriteria("LoaiTruong = 1")]
        [ImmediatePostData]
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

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }
        void CheckEditCongTy()
        {
            string CongTyPhanQuyen = Common.System_GetDeparment_Role_ByUser();//Oid công ty dc phân quyền
            int st = 0;
            CriteriaOperator f = CriteriaOperator.Parse("LoaiBoPhan =?", ERP.Module.Enum.NhanSu.LoaiBoPhanEnum.CongTy);
            XPCollection<BoPhan> listbp = new XPCollection<BoPhan>(Session, f);
            foreach (var item in listbp)
            {
                if (CongTyPhanQuyen.Contains(item.Oid.ToString()))
                    st++;
            }
            if (st > 1)
            {
                EditCongTy = false;
                TatCa = true;
                CongTy = null;
            }
            else
            {
                EditCongTy = true;
                TatCa = false;
                CongTy = Common.CongTy(Session);
            }
        }
        private bool _EditCongTy;

        [Browsable(false)]
        public bool EditCongTy
        {
            get { return _EditCongTy; }
            set { SetPropertyValue("EditCongTy", ref _EditCongTy, value); }

        }


        public BaoCao_TuyenSinh_ThongKeKetQuaTuyenSinh(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //
            NamHoc = Common.GetCurrentNamHoc(Session);
            CheckEditCongTy();
        }
        public override SqlCommand CreateCommand()
        {
            

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);
            param[1] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);
            //
            //SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_ThongKeKetQuaTuyenSinh", CommandType.StoredProcedure, param);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKeChiTieuTuyenSinh", CommandType.StoredProcedure, param);
            return cmd;
        }
    }
}
