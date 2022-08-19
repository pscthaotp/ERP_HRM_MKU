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
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.NonPersistentObjects.NgoaiKhoa;
using ERP.Module.Enum.BaoCao;
using ERP.Module.Enum.HocSinh;
using System.Data;
using ERP.Module.Extends;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "In phiếu thu, phiếu chi (Nội bộ) - Học phí")]
    public class Report_InPhieuNoiBo_HangLoat : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private Khoi _Khoi;
        private Lop _Lop;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private NghiepVuInHangLoatEnum _NghiepVu;

        [ModelDefault("Caption", "Trường")]
        [ImmediatePostData]
        [DataSourceCriteria("LoaiTruong = 1")]
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
                if(!IsLoading)
                {
                    if (CongTy != null)
                    {
                        UpdateKhoiList();
                    }
                    else
                        Khoi = null;
                    loadDanhSach();
                }
            }
        }

        [ModelDefault("Caption", "Khối")]
        [DataSourceProperty("KhoiList")]
        [ImmediatePostData]
        public Khoi Khoi
        {
            get
            {
                return _Khoi;
            }
            set
            {
                SetPropertyValue("Khoi", ref _Khoi, value);
                if (!IsLoading)
                {
                    if (Khoi != null)
                    {
                        UpdateLopList();
                    }
                    else
                        Lop = null;

                    loadDanhSach();
                }
            }
        }
        [ModelDefault("Caption", "Lớp")]
        [DataSourceProperty("LopList")]
        [ImmediatePostData]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
                if (!IsLoading)
                {

                    loadDanhSach();

                }
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    if (CongTy != null)
                    {
                        loadDanhSach();
                    }
                }
            }
        }
        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading)
                {
                    if (CongTy != null)
                    {
                        loadDanhSach();
                    }
                }
            }
        }

        [ModelDefault("Caption", "Nghiệp vụ")]
        [ImmediatePostData]
        public NghiepVuInHangLoatEnum NghiepVu
        {
            get
            {
                return _NghiepVu;
            }
            set
            {
                SetPropertyValue("NghiepVu", ref _NghiepVu, value);
                if(!IsLoading)
                {
                    loadDanhSach();
                }
            }
        }

        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<DanhSachPhieuThu_PhieuChi> listDanhSachPhieuThu_PhieuChi
        {
            get;
            set;
        }
        public Report_InPhieuNoiBo_HangLoat(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           // UpdateBLList();
            listDanhSachPhieuThu_PhieuChi = new XPCollection<DanhSachPhieuThu_PhieuChi>(Session, false);
            TuNgay = DateTime.Now;
            CongTy = Common.CongTy(Session);
        }
        [Browsable(false)]
        public XPCollection<Khoi> KhoiList { get; set; }
        protected void UpdateKhoiList()
        {
            if (KhoiList == null)
                KhoiList = new XPCollection<Khoi>(Session);
            KhoiList.Criteria = CriteriaOperator.Parse("CongTy =? and LoaiLop =?", CongTy.Oid, LoaiLopEnum.Khoi);
        }
        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }

        protected void UpdateLopList()
        {
            if (LopList == null)
                LopList = new XPCollection<Lop>(Session);
            LopList.Criteria = CriteriaOperator.Parse("CongTy =? and LopCha =?", CongTy.Oid, Khoi.Oid);
        }

        void loadDanhSach()
        {
            listDanhSachPhieuThu_PhieuChi.Reload();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CongTy", CongTy.Oid);
            param[1] = new SqlParameter("@Khoi", Khoi != null ? Khoi.Oid : Guid.Empty);
            param[2] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            param[3] = new SqlParameter("@Tu", TuNgay != DateTime.MinValue ? TuNgay.ToString("dd/MM/yyyy") : "");
            param[4] = new SqlParameter("@Den", DenNgay != DateTime.MinValue ? DenNgay.ToString("dd/MM/yyyy") : "");
            param[5] = new SqlParameter("@NghiepVu", NghiepVu);

            SqlCommand cmd = DataProvider.GetCommand("spd_HocPhi_LayDanhSachPhieuThu_PhieuChi", CommandType.StoredProcedure, param);
            DataSet dataset = DataProvider.GetDataSet(cmd);
            if (dataset != null)
            {
                DataTable dt = dataset.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    using (DialogUtil.AutoWait())
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            DanhSachPhieuThu_PhieuChi ct = new DanhSachPhieuThu_PhieuChi(Session);
                            ct.Oid = new Guid(r["Oid"].ToString());
                            ct.Caption = r["Caption"].ToString();
                            ct.NoiDung = r["NoiDung"].ToString();
                            ct.NoiDung = r["NoiDung"].ToString();
                            ct.TongTien = Convert.ToDecimal(r["TongTien"].ToString());
                            ct.SoPhieu = r["SoPhieu"].ToString();
                            ct.NoiDung = r["NoiDung"].ToString();
                            ct.NgayLap = Convert.ToDateTime(r["NgayLap"].ToString());
                            listDanhSachPhieuThu_PhieuChi.Add(ct);
                        }
                    }
                }
            }
        }
        public override SqlCommand CreateCommand()
        {
            string dsOid = "";
            foreach(var item in listDanhSachPhieuThu_PhieuChi)
            {
                if(item.Chon)
                {
                    dsOid += " union all select CONVERT(UNIQUEIDENTIFIER, '" + item.Oid.ToString() + "') ";
                }
            }
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@dsOid", dsOid.Substring(11));
            parameter[1] = new SqlParameter("@NghiepVu", NghiepVu);
            SqlCommand cmd = DataProvider.GetCommand("spd_report_InPhieuThu_PhieuChi_HangLoat", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
