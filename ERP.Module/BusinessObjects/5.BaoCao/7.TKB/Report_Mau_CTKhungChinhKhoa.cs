using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.NghiepVu.BepAn.ThucDonMonAn;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.NghiepVu.TKB.ChuongTrinhTiengAnh;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Report.ThoiKhoaBieu
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Mẫu chương trình khung chính khóa")]
    public class Report_Mau_CTKhungChinhKhoa : StoreProcedureReport
    {
        private DanhMucKhoiEdu _DanhMucKhoiEdu;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Khối")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public DanhMucKhoiEdu DanhMucKhoiEdu
        {
            get
            {
                return _DanhMucKhoiEdu;
            }
            set
            {
                SetPropertyValue("DanhMucKhoiEdu", ref _DanhMucKhoiEdu, value);           
            }
        }
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

        public Report_Mau_CTKhungChinhKhoa(Session session) : base(session) { }

        public override void AfterConstruction()
        {           
            NamHoc = Common.GetCurrentNamHoc(Session);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@DanhMucKhoiEdu", DanhMucKhoiEdu != null ? DanhMucKhoiEdu.Oid : Guid.Empty);
            parameter[1] = new SqlParameter("@NamHoc", NamHoc.Oid);          
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_MauImport_CTKhungChinhKhoa", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
