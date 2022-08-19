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
using DevExpress.Persistent.BaseImpl;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Report.NhanSu.DanhSach
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo thống kê danh sách nhân viên giảng viên nhà trường - Nhân sự")]   
    public class BaoCao_ThongKe_CBNVGVNhaTruong : StoreProcedureReport
    {
        private CongTy _CongTy; 
        private NamHoc _NamHoc;


        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
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
      

        public BaoCao_ThongKe_CBNVGVNhaTruong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();        
        }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Rpt_ThongKe_CanBoNVGV_NhaTruong");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NamHoc", NamHoc!=null?NamHoc.Oid:Guid.Empty);
            cmd.Parameters.AddWithValue("@CongTy", CongTy != null ? CongTy.Oid:Guid.Empty);

            return cmd;
        }
    }
}
