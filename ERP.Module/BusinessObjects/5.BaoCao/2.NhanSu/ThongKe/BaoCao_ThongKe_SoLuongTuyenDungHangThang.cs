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

namespace ERP.Module.Report.NhanSu.ThongKe
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo số lượng tuyển dụng hàng tháng - Nhân sự")]
    [Appearance("Report_DanhSach_NhanVienTheoPhongBan.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class BaoCao_ThongKe_SoLuongTuyenDungHangThang : StoreProcedureReport
    {
        private bool _TatCaDonVi = false;
        private BoPhan _BoPhan; 
        private DateTime _TuNgay;
        private DateTime _DenNgay;        

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

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        public BaoCao_ThongKe_SoLuongTuyenDungHangThang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuNgay = Common.SetTime(Common.GetServerCurrentTime(), SetTimeEnum.StartMonth);
            DenNgay = Common.SetTime(Common.GetServerCurrentTime(), SetTimeEnum.EndMonth);
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

            SqlCommand cmd = new SqlCommand("spd_Rpt_ThongKe_SoLuongTuyenDungHangThang");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BoPhanDuocPhanQuyenList", sb.ToString());
            cmd.Parameters.AddWithValue("@DenNgay", DenNgay);
            cmd.Parameters.AddWithValue("@TuNgay", TuNgay);
            cmd.Parameters.AddWithValue("@BoPhan", BoPhan != null ? BoPhan.Oid : Common.CongTy(Session) != null ? Common.CongTy(Session).Oid : Guid.Empty);

            return cmd;
        }
    }
}
