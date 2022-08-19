using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
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
    [ModelDefault("Caption", "Báo cáo: Thống kê khối lượng giảng dạy (thỉnh giảng) toàn trường (Mẫu 2)")]
    public class Report_PMS_ThongKe_KhoiLuongGiangDay_ThinhGiang_Mau2 : StoreProcedureReport
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;     

        [ModelDefault("Caption", "Trường")]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { SetPropertyValue("CongTy", ref _CongTy, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);              
            }
        }

        public Report_PMS_ThongKe_KhoiLuongGiangDay_ThinhGiang_Mau2(Session session) : base(session) { }

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
           
        //Thực hiện lấy dữ liệu lên báo cáo 
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_ThongKe_KhoiLuongGiangDay_toantruong_Mau1", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;           
                da.SelectCommand.Parameters.AddWithValue("@CongTy",  CongTy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc",NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiGV", 1);
                da.Fill(DataSource);
            }
        }

    }
}
