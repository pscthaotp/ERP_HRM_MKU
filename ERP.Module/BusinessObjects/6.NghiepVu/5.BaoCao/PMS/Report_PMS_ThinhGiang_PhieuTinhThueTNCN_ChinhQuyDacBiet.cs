using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.Persistent.Validation;

namespace ERP.Module.Report.PMS
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: PMS Phiếu tính thuế TNCN _ chính quy đặc biệt(thỉnh giảng)")]
    public class Report_PMS_ThinhGiang_PhieuTinhThueTNCN_ChinhQuyDacBiet : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private GiangVienThinhGiang _GiangVienThinhGiang;

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

        [ModelDefault("Caption", "Giảng viên thỉnh giảng")]
        public GiangVienThinhGiang GiangVienThinhGiang
        {
            get { return _GiangVienThinhGiang; }
            set { _GiangVienThinhGiang = value; }
        }



        public Report_PMS_ThinhGiang_PhieuTinhThueTNCN_ChinhQuyDacBiet(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_ThinhGiang_PhieuTinhThueTNCN_ChinhQuyDacBiet", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc == null ? Guid.Empty : NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@GiangVienThinhGiang", GiangVienThinhGiang == null ? Guid.Empty : GiangVienThinhGiang.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
