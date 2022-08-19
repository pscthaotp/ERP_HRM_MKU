using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using System.Data;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.TienLuong.TLU.LuongThang13
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo : Bảng kết xuất lương tháng 13 HĐQT-HĐT ( chuyển khoản) chữ thường")]
    [Appearance("BangLuong.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class Report_Luong_KietXuatLuongThang13_HDQT_CK_ChuThuong : StoreProcedureReport
    {
        private ChungTu _ChungTu;
        private bool _TatCaDonVi = true;
        private BoPhan _BoPhan;
        private NganHang _NganHang;

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả đơn vị")]
        public bool TatCaDonVi
        {
            get
            {
                return _TatCaDonVi;
            }
            set
            {
                SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value);
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

        [ModelDefault("Caption", "Ngân hàng")]
        public NganHang NganHang
        {
            get { return _NganHang; }
            set { _NganHang = value; }
        }

        public Report_Luong_KietXuatLuongThang13_HDQT_CK_ChuThuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();                    
        }

        public override SqlCommand CreateCommand()
        {
            List<string> listBP = new List<string>();
            //
            if (TatCaDonVi)
                listBP = Common.Department_GetRoledDepartmentList_ByCurrentUser();
            else
                listBP = Common.Department_GetRoledDepartmentList_ByDepartment(BoPhan);
            //
            StringBuilder roled = new StringBuilder();
            foreach (string item in listBP)
            {
                roled.Append(String.Format("{0};", item));
            }
            SqlCommand cmd = new SqlCommand("spd_Report_Luong_BangKietXuatLuongThang13ChuyenKhoanChuThuong_HDQT");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ChungTu", ChungTu.Oid);
            cmd.Parameters.AddWithValue("@BoPhan", roled.ToString());
            cmd.Parameters.AddWithValue("@NganHang", NganHang != null ? NganHang.Oid : Guid.Empty);
            return cmd;
        }
     
    }
}
