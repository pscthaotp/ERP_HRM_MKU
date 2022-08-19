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
    [ModelDefault("Caption", "Báo cáo: Bảng thanh toán giờ giảng")]
    public class Report_PMS_ThanhToanGioGiangTheoDot : StoreProcedureReport
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private GiangVienThinhGiang _NhanVien;
        private BangChotThuLao_ThinhGiang _BangChotThuLao_ThinhGiang;

        [ModelDefault("Caption", "Trường")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set {
                    SetPropertyValue("CongTy", ref _CongTy, value);
                    if (!IsLoading && value != null)
                    {
                        UpdateBangChot();
                        listgv = new XPCollection<GiangVienThinhGiang>(Session);
                        listgv.Criteria = CriteriaOperator.Parse("CongTy = ?", CongTy.Oid);
                    }
                }
        }

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        //[RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                {
                    UpdateBangChot();
                } 
            }
        }
        
     
        [ModelDefault("Caption", "Nhân Viên")]
        [DataSourceProperty("listgv")]
        [ImmediatePostData]
        public GiangVienThinhGiang NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Bảng chốt thù lao giảng dạy")]
        [ImmediatePostData]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("listbc", DataSourcePropertyIsNullMode.SelectAll)]
        public BangChotThuLao_ThinhGiang BangChotThuLao_ThinhGiang
        {
            get { return _BangChotThuLao_ThinhGiang; }
            set
            {
                SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách giảng viên tính thù lao thỉnh giảng")]
        public XPCollection<GiangVienThinhGiang> listgv
        {
            get;
            set;
        }
        [Browsable(false)]
        public XPCollection<BangChotThuLao_ThinhGiang> listbc
        {
            get;
            set;
        }


        //Thực hiện cập nhật thông tin bảng chốt
        public void UpdateBangChot()
        {
            if (listbc == null)
            {
                listbc = new XPCollection<BangChotThuLao_ThinhGiang>(Session);
            }
            if (NamHoc != null && CongTy != null)
            {
                listbc.Criteria = CriteriaOperator.Parse("CongTy = ? and NamHoc=?", CongTy.Oid, NamHoc.Oid);
            }
        }
            
        public Report_PMS_ThanhToanGioGiangTheoDot(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            //lstBP = Common.Department_GetRoledDepartmentList_ByDepartment(BoPhan);
        }
        public override SqlCommand CreateCommand()
        {
            return null;
        }

        //Thực hiện cập nhật thông tin       
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_BangThanhToangGioGiang", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@CongTy", CongTy != null ? CongTy.Oid : Guid.Empty);               
                da.SelectCommand.Parameters.AddWithValue("@NhanVien",NhanVien!=null ? NhanVien.Oid:Guid.Empty);
                da.SelectCommand.Parameters.AddWithValue("@bangchot", BangChotThuLao_ThinhGiang != null ? BangChotThuLao_ThinhGiang.Oid : Guid.Empty);
                da.Fill(DataSource);
            }
        }

    }
}
