using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;

namespace ERP.Module.Report.NhanSu.DanhSach
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo danh sách nhân sự theo phòng ban")]
    [Appearance("BaoCao_DanhSach_BoNhiemNhanSu.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class BaoCao_DanhSach_BaoCaoNhanSu : StoreProcedureReport
    {
        private bool _TatCaDonVi = false;
        private BoPhan _BoPhan; 

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả")]
        public bool TatCaDonVi
        {
            get
            {
                return _TatCaDonVi;
            }
            set
            {
                SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value);
                if (!IsLoading && value == true)
                {
                    BoPhan = null;
                }
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaDonVi")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }



        public BaoCao_DanhSach_BaoCaoNhanSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            List<string> lstBP;
            if (TatCaDonVi)
                lstBP = Common.Department_GetRoledDepartmentList_ByDepartment(null);
            else
            {
                if (Common.QuanTriToanHeThong() || Common.QuanTriKhoi())
                    lstBP = Common.Department_GetChildDepartmentList_ByCompany(BoPhan);
                else
                    lstBP = Common.Department_GetRoledDepartmentList_ByDepartment(BoPhan);
            }

            StringBuilder sb = new StringBuilder();
            foreach (string item in lstBP)
            {
                sb.Append(String.Format("{0},", item));
            }

            SqlCommand cmd = new SqlCommand("spd_Rpt_DanhSach_BaoCaoNhanSu");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BoPhanDuocPhanQuyenList", sb.ToString());
            cmd.Parameters.AddWithValue("@BoPhan", BoPhan != null ? BoPhan.Oid : Common.CongTy(Session) != null ? Common.CongTy(Session).Oid : Guid.Empty);

            return cmd;
        }
    }
}
