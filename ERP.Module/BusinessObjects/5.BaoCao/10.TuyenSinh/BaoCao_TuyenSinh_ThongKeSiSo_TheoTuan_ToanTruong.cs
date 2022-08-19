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
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.BaoCao.TuyenSinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Thống kê sỉ số theo tuần toàn trường - Tuyển sinh")]
    public class BaoCao_TuyenSinh_ThongKeSiSo_TheoTuan_ToanTruong : StoreProcedureReport
    {
        //
        private DateTime _Thang;

        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Editmask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        public BaoCao_TuyenSinh_ThongKeSiSo_TheoTuan_ToanTruong(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //CongTy = Common.CongTy(Session);
            //
            Thang = Common.GetServerCurrentTime();
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Thang", Thang);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_ThongKeSiSo_TheoTuan_ToanCongTy", CommandType.StoredProcedure, param);
            
            return cmd;
        }
    }
}
