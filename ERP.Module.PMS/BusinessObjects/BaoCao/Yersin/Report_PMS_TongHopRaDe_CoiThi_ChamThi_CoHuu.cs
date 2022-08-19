using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.NghiepVu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp ra đề - coi thi - chấm thi(cơ hữu)")]
    public class Report_PMS_TongHopRaDe_CoiThi_ChamThi_CoHuu : StoreProcedureReport
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading && value != null)
                {                  
                    UpdateBoPhan();
                    listgv = new XPCollection<NhanVien>(Session);
                    listgv.Criteria = CriteriaOperator.Parse("CongTy = ?", CongTy.Oid);
                }
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);           
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("listbp", DataSourcePropertyIsNullMode.SelectAll)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    listgv.Criteria = CriteriaOperator.Parse("CongTy = ? and BoPhan=?", CongTy.Oid, BoPhan.Oid);
                }
            }
        }

        [ModelDefault("Caption", "Nhân Viên")]
        [ImmediatePostData]
        [DataSourceProperty("listgv")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && BoPhan == null && NhanVien != null)
                    BoPhan = NhanVien.BoPhan;
            }
        }

        [Browsable(false)]
        public XPCollection<BoPhan> listbp
        {
            get;
            set;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách giảng viên tính thù lao")]
        public XPCollection<NhanVien> listgv
        {
            get;
            set;
        }

        public Report_PMS_TongHopRaDe_CoiThi_ChamThi_CoHuu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            UpdateBoPhan();   
        }
        public override SqlCommand CreateCommand()
        {
            return null;
        }

        //Thực hiện cập nhật bộ phận
        public void UpdateBoPhan()
        {
            if (listbp == null)
            {
                listbp = new XPCollection<BoPhan>(Session);
            }
            if (CongTy != null)
            {
                listbp.Criteria = CriteriaOperator.Parse("CongTy = ?", CongTy.Oid);
            }
        }
   
        
        //Thực hiện lấy dữ liệu lên báo cáo 
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_TongHopRaDe_CoiThi_ChamThi", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@CongTy", CongTy == null ? Guid.Empty : CongTy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc",NamHoc==null?Guid.Empty: NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiGV", 0);
                da.Fill(DataSource);
            }
        }
    }
}
