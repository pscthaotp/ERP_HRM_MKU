using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;

namespace ERP.Module.BaoCao.Custom
{
    [NonPersistent]
    [ImageName("BO_Report")]
    public abstract class StoreProcedureReport : BaseObject
    {
        private DateTime _NgayLapBaoCao;
        public static StoreProcedureReport Param;

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "HeaderAndFooter")]
        public DataSet HeaderAndFooter { get; set; }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "MainData")]
        public DataSet DataSource { get; set; }

        [ModelDefault("Caption", "Ngày lập báo cáo")]
        public DateTime NgayLapBaoCao
        {
            get
            {
                return _NgayLapBaoCao;
            }
            set
            {
                SetPropertyValue("NgayLapBaoCao", ref _NgayLapBaoCao, value);
            }
        }

        public StoreProcedureReport(Session session) : base(session) { }

        public abstract SqlCommand CreateCommand();

        public virtual void FillDataSource()
        {
            SqlCommand cmd = CreateCommand();
            DataSource = DataProvider.GetDataSet(cmd);
        }

        private void GetDataHeaderAndFooter()
        {
            HeaderAndFooter = new DataSet();
            //
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ThongTinNhanVien", Common.SecuritySystemUser_GetCurrentUser().ThongTinNhanVien != null ? Common.SecuritySystemUser_GetCurrentUser().ThongTinNhanVien.Oid : Guid.Empty);
                param[1] = new SqlParameter("@OidNguoiSuDung", Common.SecuritySystemUser_GetCurrentUser().Oid);

                SqlCommand cmdHeaderAndFooter = DataProvider.GetCommand("spd_HeThong_ReportHeaderAndFooter", CommandType.StoredProcedure, param);

                using (SqlDataAdapter da = new SqlDataAdapter(cmdHeaderAndFooter))
                {
                    da.SelectCommand.Connection = DataProvider.GetConnection();
                    //
                    da.Fill(HeaderAndFooter);
                }
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //Khởi tạo dataset
            DataSource = new DataSet();
            //Lấy ngày hiện tại
            NgayLapBaoCao = Common.GetServerCurrentTime();
            //Lấy dữ liệu header và footer
            GetDataHeaderAndFooter();
        }
    }

}
