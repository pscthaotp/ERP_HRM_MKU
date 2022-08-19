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
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.QuanLyKho.Khos;
using ERP.Module.NghiepVu.QuanLyKho.HangHoas;
using ERP.Module.NghiepVu.QuanLyKho.DonHang;

namespace ERP.Module.Report.HocSinh.QuanLyKho
{
    [NonPersistent]
    [ModelDefault("Caption", "Phiếu yêu cầu mua sắm - Kho")]
    public class Report_DonDatHang_YeuCauMuaSam : StoreProcedureReport
    {
        private DonDatHang _DonDatHang;

        [ModelDefault("Caption", "Đơn đặt hàng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DonDatHang DonDatHang
        {
            get
            {
                return _DonDatHang;
            }
            set
            {
                SetPropertyValue("DonDatHang", ref _DonDatHang, value);
            }
        }

        public Report_DonDatHang_YeuCauMuaSam(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@DonDatHang", DonDatHang.Oid);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DonDatHang_YeuCauMuaSam", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
