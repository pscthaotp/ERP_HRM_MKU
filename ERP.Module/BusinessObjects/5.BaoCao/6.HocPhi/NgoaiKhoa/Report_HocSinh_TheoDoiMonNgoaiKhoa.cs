using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.Systems;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.HocSinh.Lops;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.TKB.ChuongTrinhNgoaiKhoa;
using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.Extends;

namespace ERP.Module.Report.HocSinh.HocSinh
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Theo dõi môn ngoại khóa - Học sinh")]
    public class Report_HocSinh_TheoDoiMonNgoaiKhoa : StoreProcedureReport, ICongTy
    {
        // Fields...
        private CongTy _CongTy;
        private LopNgoaiKhoa _LopNgoaiKhoa;
        private LoaiPhi _LoaiPhi;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                if (!IsLoading && value != null)
                {
                    KyTinhHocPhi kt = Session.FindObject<KyTinhHocPhi>(CriteriaOperator.Parse("Thang =? and Nam =? and CongTy =?", DateTime.Now.Month, DateTime.Now.Year, CongTy.Oid));
                    if (kt != null)
                    {
                        TuNgay = kt.TuNgay;
                        DenNgay = kt.DenNgay;
                    }
                    UpdateDSLopNgoaihoa();                   
                }
            }
        }
        
        [ImmediatePostData]
        [DataSourceProperty("DSLopNgoaiKhoa")]
        [ModelDefault("Caption", "Lớp ngoại khóa")]
        public LopNgoaiKhoa LopNgoaiKhoa
        {
            get
            {
                return _LopNgoaiKhoa;
            }
            set
            {
                SetPropertyValue("LopNgoaiKhoa", ref _LopNgoaiKhoa, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại phí")]
        public LoaiPhi LoaiPhi
        {
            get
            {
                return _LoaiPhi;
            }
            set
            {
                SetPropertyValue("LoaiPhi", ref _LoaiPhi, value);
            }
        }

        [ImmediatePostData]
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

        [ImmediatePostData]
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

        [Browsable(false)]
        public XPCollection<LopNgoaiKhoa> DSLopNgoaiKhoa
        {
            get; set;
        }

        public Report_HocSinh_TheoDoiMonNgoaiKhoa(Session session) : base(session) { }


        public void UpdateDSLopNgoaihoa()
        {
            using (DialogUtil.Wait("Thông báo!", "Đang xử lý, vui lòng chờ...."))
            {
                CriteriaOperator filter = CriteriaOperator.Parse("CongTy = ? AND NamHoc = ? AND GCRecord IS NULL", CongTy.Oid, Common.GetCurrentNamHoc(Session).Oid);
                XPCollection<LopNgoaiKhoa> LopNgoaiKhoaList = new XPCollection<LopNgoaiKhoa>(Session, filter);
                if (DSLopNgoaiKhoa != null)
                {
                    DSLopNgoaiKhoa.Reload();
                }
                else
                {
                    DSLopNgoaiKhoa = new XPCollection<LopNgoaiKhoa>(Session, false);
                }
                foreach (LopNgoaiKhoa item in LopNgoaiKhoaList)
                {
                    DSLopNgoaiKhoa.Add(item);
                }
            }

        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@CongTy", CongTy.Oid);
            parameter[1] = new SqlParameter("@LopNgoaiKhoa", LopNgoaiKhoa == null ? Guid.Empty : LopNgoaiKhoa.Oid);
            parameter[2] = new SqlParameter("@LoaiPhi", LoaiPhi == null ? Guid.Empty : LoaiPhi.Oid);
            parameter[3] = new SqlParameter("@TuNgay", TuNgay != DateTime.MinValue ? TuNgay.ToString("dd/MM/yyyy") : "");
            parameter[4] = new SqlParameter("@DenNgay", DenNgay != DateTime.MinValue ? DenNgay.ToString("dd/MM/yyyy") : "");
       

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocSinh_TheoDoiMonNgoaiKhoa", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180;
            return cmd;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
        }
    }

}
