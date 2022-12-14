using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.PMS
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: PMS Bảng tổng hợp tiền dạy của giảng viên mời _ lớp tại chức")]
    public class Report_PMS_ThinhGiang_BangTongHopTienDayCuaGiangVien_LopTaiChuc : StoreProcedureReport
    {

        private HocKy _HocKy;
        private NamHoc _NamHoc;
        private NganHang _NganHang;


        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { _NamHoc = value; }
        }

        [ModelDefault("Caption","Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { _HocKy = value; }
        }

        public NganHang NganHang
        {
            get { return _NganHang; }
            set { _NganHang = value; }
        }


        public Report_PMS_ThinhGiang_BangTongHopTienDayCuaGiangVien_LopTaiChuc(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_ThinhGiang_BangTongHopTienDayCuaGiangVien_LopTaiChuc", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc == null ? Guid.Empty : NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NganHang", NganHang == null ? Guid.Empty : NganHang.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
