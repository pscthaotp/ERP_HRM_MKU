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
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Enum.HocPhi;
using ERP.Module.NonPersistentObjects.HocPhis;
using ERP.Module.Extends;
using System.Data;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "In biên lai học phí - Học phí")]
    public class Report_BienLai_InBienLai : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private string _So;
        private BienLai _BienLai;
        private NamHoc _NamHoc;

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

        private bool _InHoaDonCongTy;

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
                if (!IsLoading)
                {
                    //UpdateBLList();
                    Update_rptBLList();
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
                if (!IsLoading)
                    Update_rptBLList();
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
                if (!IsLoading && value != string.Empty)
                {
                    BienLai = Session.FindObject<BienLai>(CriteriaOperator.Parse("So = ? and LoaiChungTu = 0 and CongTy =?", So, CongTy.Oid));
                    if (BienLai != null)
                        TongTien = BienLai.SoTienPHDong;
                }
            }
        }

        [ModelDefault("Caption", "Biên lai")]
        //[DataSourceProperty("BLList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData]
        [Browsable(false)]
        public BienLai BienLai
        {
            get
            {
                return _BienLai;
            }
            set
            {
                SetPropertyValue("BienLai", ref _BienLai, value);
            }
        }

        [ModelDefault("Caption", "In hóa đơn công ty")]
        public bool InHoaDonCongTy
        {
            get
            {
                return _InHoaDonCongTy;
            }
            set
            {
                SetPropertyValue("InHoaDonCongTy", ref _InHoaDonCongTy, value);
            }
        }

        #region Tạm không dùng
        //[Browsable(false)]
        //public XPCollection<BienLai> BLList { get; set; }

        //private void UpdateBLList()
        //{
        //    BLList = new XPCollection<BienLai>(Session);
        //    if (CongTy != null && NamHoc != null)
        //        BLList.Criteria = CriteriaOperator.Parse("TrangThaiChungTu =? and CongTy = ? and KyTinhHocPhi.NamHoc =?", TrangThaiChungTuEnum.HieuLuc, CongTy.Oid, NamHoc.Oid);
        //}

        #endregion


        #region ReportBienLaiList
        private Report_BienLai _Report_BienLai;
        [ModelDefault("Caption", "Biên lai")]
        [DataSourceProperty("ReportBienLaiList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData]
        public Report_BienLai Report_BienLai
        {
            get
            {
                return _Report_BienLai;
            }
            set
            {
                SetPropertyValue("Report_BienLai", ref _Report_BienLai, value);
                if (!IsLoading)
                {
                    if (Report_BienLai != null)
                    {
                        BienLai = Session.FindObject<BienLai>(CriteriaOperator.Parse("Oid =?", Report_BienLai.OidBienLai));
                        TongTien = Report_BienLai.SoTienPHDong;
                    }
                    else
                        TongTien = 0;
                }
            }
        }
        [Browsable(false)]
        public XPCollection<Report_BienLai> ReportBienLaiList { get; set; }

        private void Update_rptBLList()
        {
            ReportBienLaiList.Reload();
            if (CongTy != null && CongTy.BoPhanCha != null && NamHoc != null)
            {
                using (DialogUtil.AutoWait("Đang lấy danh sách biên lai!"))
                {
                    SqlParameter[] parameter = new SqlParameter[2];
                    parameter[0] = new SqlParameter("@CongTy", CongTy.Oid);
                    parameter[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
                    DataTable dt = DataProvider.GetDataTable("spd_Report_BienLai_DanhSachBienLai", CommandType.StoredProcedure, parameter);
                    if (dt != null)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            Report_BienLai item = new Report_BienLai(Session);
                            item.OidBienLai = new Guid(r["OidBienLai"].ToString());
                            item.TenBienLai = r["TenBienLai"].ToString();
                            item.SoTienPHDong = Convert.ToDecimal(r["SoTienPHDong"].ToString());
                            if (Convert.ToInt32(r["LoaiChungTu"].ToString()) == 1)
                                item.LoaiChungTu = LoaiChungTuEnum.HoaDon;
                            else
                                item.LoaiChungTu = LoaiChungTuEnum.PhieuThu;
                            item.SoBienLai = r["SoBienLai"].ToString();
                            ReportBienLaiList.Add(item);
                        }
                        OnChanged("ReportBienLaiList");
                    }
                }
            }
        }
        #endregion

        #region Chưa dùng

        private string _HD;
        [ModelDefault("Caption", "BL")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.HocPhi.chkComboxEdit_BienLai")]
        [Browsable(false)]
        [ImmediatePostData]
        public string HD
        {
            get { return _HD; }
            set
            {
                SetPropertyValue("HD", ref _HD, value);
            }
        }
        #endregion
        public Report_BienLai_InBienLai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); ReportBienLaiList = new XPCollection<Report_BienLai>(Session, false);
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@BienLai", BienLai.Oid);
            parameter[1] = new SqlParameter("@InHoaDonCongTy", InHoaDonCongTy);
            parameter[2] = new SqlParameter("@LoaiChungTu_In", LoaiChungTuEnum.PhieuThu);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BienLai_InHoaDon", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}