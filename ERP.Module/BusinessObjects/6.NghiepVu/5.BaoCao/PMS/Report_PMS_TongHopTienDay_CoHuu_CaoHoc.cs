using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.PMS
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Tổng hợp tiền dạy của GV cơ hữu lớp cao học")]
    public class Report_PMS_TongHopTienDay_CoHuu_CaoHoc : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        //private KyTinhLuong _KyTinhLuong;

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

        //[ImmediatePostData]
        //[ModelDefault("Caption", "Kỳ tính lương")]
        //[RuleRequiredField("", DefaultContexts.Save, "Chưa chọn kỳ tính lương")]
        // public KyTinhLuong  KyTinhLuong
        //{
        //    get
        //    {
        //        return _KyTinhLuong;
        //    }
        //    set
        //    {
        //        SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
        //    }
        //}

        public Report_PMS_TongHopTienDay_CoHuu_CaoHoc(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_TongHopTienDay_CoHuu_CaoHoc", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc == null ? Guid.Empty : NamHoc.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
