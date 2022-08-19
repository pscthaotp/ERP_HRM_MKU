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
using ERP.Module.Enum.TKB;

namespace ERP.Module.Report.ThoiKhoaBieu
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Mẫu khung giờ TKB")]
    public class Report_Mau_KhungGioTKB : StoreProcedureReport
    {
        private CongTy _CongTy;
        private Lop _Lop;
        private LoaiKhoaEnum _LoaiKhoaEnum;
        private bool _TatCa;

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [DataSourceCriteria("LoaiTruong = 1")]
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
                if (!IsLoading && value != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("GCRecord IS NULL AND LoaiLop = 0 AND CongTy = ?", CongTy.Oid);
                    DSLop = new XPCollection<Lop>(Session, filter);
                }
            }
        }
 
        [ModelDefault("Caption", "Khối")]
        [DataSourceProperty("DSLop")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
            }
        }

        [ModelDefault("Caption", "Loại khóa")]
        [ImmediatePostData]
        //[RuleRequiredField(DefaultContexts.Save)]
        public LoaiKhoaEnum LoaiKhoaEnum
        {
            get
            {
                return _LoaiKhoaEnum;
            }
            set
            {
                SetPropertyValue("LoaiKhoaEnum", ref _LoaiKhoaEnum, value);
                if( !IsLoading)
                {
                    TatCa = false;
                }
            }
        }

        [ModelDefault("Caption", "Tất cả")]
        [ImmediatePostData]
        //[RuleRequiredField(DefaultContexts.Save)]
        public bool TatCa
        {
            get
            {
                return _TatCa;
            }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
            }
        }



        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách lớp")]
        public XPCollection<Lop> DSLop { get; set; }

        public Report_Mau_KhungGioTKB(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            CongTy = Common.CongTy(Session);
            TatCa = true;
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@TuanHoc", Guid.Empty);
            parameter[1] = new SqlParameter("@Khoi", Lop != null ? Lop.Oid :  Guid.Empty);
            parameter[2] = new SqlParameter("@NamHoc", Guid.Empty);
            parameter[3] = new SqlParameter("@CongTy", CongTy.Oid);
            if(TatCa == true)
                parameter[4] = new SqlParameter("@LoaiKhoa", 100);
            else
                parameter[4] = new SqlParameter("@LoaiKhoa", LoaiKhoaEnum);
            parameter[5] = new SqlParameter("@LoaiMau", 1);          
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_MauImport_TA_ABI", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
