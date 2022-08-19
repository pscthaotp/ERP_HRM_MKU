using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocPhi.BienLai;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using ERP.Module.NghiepVu.HocPhi.PhieuChi;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "In phiếu chi - Học phí")]
    public class Report_PhieuChi_InPhieuChi : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private string _So;
        private PhieuChi _PhieuChi;
        private bool _InPhieuNoiBo;

        #region TongTien
        private decimal _TongTien;
        [ModelDefault("Caption", "Tổng tiền")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "False")]
        public decimal TongTien
        {
            get
            {
                return _TongTien;
            }
            set
            {
                SetPropertyValue("TongTien", ref _TongTien, value);
            }
        }
        #endregion

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
                if (!IsLoading && CongTy != null)
                    UpdatePhieuChiList();
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Số")]
        public string So
        {
            get
            {
                return _So;
            }
            set
            {
                SetPropertyValue("So", ref _So, value);
                if (!IsLoading && value != string.Empty && CongTy != null)
                    PhieuChi = Session.FindObject<PhieuChi>(CriteriaOperator.Parse("So = ? and CongTy =?", So, CongTy.Oid));
            }
        }

        [ModelDefault("Caption", "Phiếu chi")]
        [DataSourceProperty("PhieuChiList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData]
        public PhieuChi PhieuChi
        {
            get
            {
                return _PhieuChi;
            }
            set
            {
                SetPropertyValue("PhieuChi", ref _PhieuChi, value);
                if (!IsLoading)
                    if (PhieuChi != null)
                        TongTien = PhieuChi.SoTienThucThu;
                    else
                        TongTien = 0;
            }
        }


        [ModelDefault("Caption", "In phiếu nội bộ")]
        public bool InPhieuNoiBo
        {
            get { return _InPhieuNoiBo; }
            set
            {
                SetPropertyValue("InPhieuNoiBo", ref _InPhieuNoiBo, value);
            }
        }
        public Report_PhieuChi_InPhieuChi(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<PhieuChi> PhieuChiList { get; set; }

        private void UpdatePhieuChiList()
        {
            if (PhieuChiList == null)
                PhieuChiList = new XPCollection<PhieuChi>(Session);
            PhieuChiList.Criteria = CriteriaOperator.Parse("CongTy = ?", CongTy.Oid);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            UpdatePhieuChiList();
            InPhieuNoiBo = false;
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@PhieuChi", PhieuChi.Oid);
            parameter[1] = new SqlParameter("@InNoiBo", InPhieuNoiBo);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_InPhieuChi", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
