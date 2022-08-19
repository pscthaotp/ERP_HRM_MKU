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
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp vượt tiết giảng dạy (Cơ hữu)")]
    public class Report_PMS_TongHop_VuotTietGiangDay_CoHuu : StoreProcedureReport
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private BangChotThuLao _BangChotThuLao;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _NhanVien;

        [ModelDefault("Caption", "Trường")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { 
                    SetPropertyValue("CongTy", ref _CongTy, value);
                    if (!IsLoading && value != null)
                    {
                        UpdateBoPhan();
                        UpdateBangChot();
                        listgv = new XPCollection<ThongTinNhanVien>(Session);
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
                if (!IsLoading && value != null)
                {
                    UpdateBangChot();
                }
            }
        }

        [ModelDefault("Caption", "Bảng chốt thù lao")]
        [DataSourceProperty("listbc", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        public BangChotThuLao BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [ImmediatePostData]
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
        public ThongTinNhanVien NhanVien
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
        public XPCollection<BangChotThuLao> listbc { get; set; }

        [Browsable(false)]
        public XPCollection<BoPhan> listbp
        {
            get;
            set;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách giảng viên cơ hữu")]
        public XPCollection<ThongTinNhanVien> listgv
        {
            get;
            set;
        }

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
        //
        public Report_PMS_TongHop_VuotTietGiangDay_CoHuu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            listbc = new XPCollection<BangChotThuLao>(Session);
            UpdateBoPhan();  
            //lstBP = Common.Department_GetRoledDepartmentList_ByDepartment(BoPhan);
        }
        public override SqlCommand CreateCommand()
        {
            return null;
        }

        //Thực hiện cập nhật thông tin bảng chốt
        public void UpdateBangChot()
        {

            if (listbc == null)
            {
                listbc = new XPCollection<BangChotThuLao>(Session);
            }
            if (NamHoc != null && CongTy != null)
            {
                listbc.Criteria = CriteriaOperator.Parse("CongTy = ? and NamHoc=?", CongTy.Oid, NamHoc.Oid);
            }                      
        }       

        //Thực hiện lấy dữ liệu lên báo cáo 
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_TongHop_VuotTietGiangDay_CoHuu", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangChotThuLao", BangChotThuLao == null ? Guid.Empty : BangChotThuLao.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.Fill(DataSource);
            }
        }

    }
}
