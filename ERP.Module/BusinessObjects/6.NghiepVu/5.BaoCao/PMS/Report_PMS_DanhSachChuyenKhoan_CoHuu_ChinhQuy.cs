using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Persistent.Validation;

namespace ERP.Module.Report.PMS
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách chuyển khoản tiền dạy của GV cơ hữu lớp chính quy")]
    public class Report_PMS_DanhSachChuyenKhoan_CoHuu_ChinhQuy : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private NganHang _NganHang;

        [ImmediatePostData]
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

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }
       
        [ModelDefault("Caption", "Ngân hàng")]       
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
            set
            {
                SetPropertyValue("NganHang", ref _NganHang, value);
            }
        }

        public Report_PMS_DanhSachChuyenKhoan_CoHuu_ChinhQuy(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_DanhSachChuyenKhoan_CoHuu_ChinhQuy", (SqlConnection)Session.Connection))
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
