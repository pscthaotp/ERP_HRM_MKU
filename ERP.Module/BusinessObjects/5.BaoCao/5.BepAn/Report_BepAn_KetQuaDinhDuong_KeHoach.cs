using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;

namespace ERP.Module.Report.BepAn
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bếp ăn - Kết quả dinh dưỡng - Kế hoạch")]
    public class Report_BepAn_KetQuaDinhDuong_KeHoach : StoreProcedureReport
    {
        // Fields...
        private ThucDonNgay _ThucDonNgay;
        private SuatAn _SuatAn;
        private ThucDonKhung _ThucDonKhung;
        private ThucDonKhung_SuatAn _ThucDonKhung_SuatAn;
        //private Guid _GuidOid;

        [ImmediatePostData]
        [ModelDefault("Caption", "Suất ăn")]
        public SuatAn SuatAn
        {
            get
            {
                return _SuatAn;
            }
            set
            {
                SetPropertyValue("SuatAn", ref _SuatAn, value);
                if (!IsLoading && value != null)
                {
                    ThucDonNgay = null;
                    ThucDonKhung = null;
                    ThucDonKhung_SuatAn = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thực đơn ngày")]
        public ThucDonNgay ThucDonNgay
        {
            get
            {
                return _ThucDonNgay;
            }
            set
            {
                SetPropertyValue("ThucDonNgay", ref _ThucDonNgay, value);
                if (!IsLoading && value != null)
                {
                    SuatAn = null;
                    ThucDonKhung = null;
                    ThucDonKhung_SuatAn = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thực đơn khung")]
        public ThucDonKhung ThucDonKhung
        {
            get
            {
                return _ThucDonKhung;
            }
            set
            {
                SetPropertyValue("ThucDonKhung", ref _ThucDonKhung, value);
                if (!IsLoading && value != null)
                {
                    SuatAn = null;
                    ThucDonNgay = null;
                    ThucDonKhung_SuatAn = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thực đơn khung - Suất ăn")]
        public ThucDonKhung_SuatAn ThucDonKhung_SuatAn
        {
            get
            {
                return _ThucDonKhung_SuatAn;
            }
            set
            {
                SetPropertyValue("ThucDonKhung_SuatAn", ref _ThucDonKhung_SuatAn, value);
                if (!IsLoading && value != null)
                {
                    SuatAn = null;
                    ThucDonNgay = null;
                    ThucDonKhung = null;
                }
            }
        }

        public Report_BepAn_KetQuaDinhDuong_KeHoach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@SuatAn", SuatAn != null ? SuatAn.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@ThucDonNgay", ThucDonNgay != null ? ThucDonNgay.Oid : Guid.Empty);
            parameter[2] = new SqlParameter("@ThucDonKhung", ThucDonKhung != null ? ThucDonKhung.Oid : Guid.Empty);
            parameter[3] = new SqlParameter("@ThucDonKhung_SuatAn", ThucDonKhung_SuatAn != null ? ThucDonKhung_SuatAn.Oid : Guid.Empty);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BepAn_KetQuaDinhDuong_KeHoach", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
