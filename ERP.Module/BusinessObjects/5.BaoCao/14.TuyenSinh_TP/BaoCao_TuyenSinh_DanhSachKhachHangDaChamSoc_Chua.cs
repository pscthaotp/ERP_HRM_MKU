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
using ERP.Module.NghiepVu.HocSinh;
using ERP.Module.Commons;
using ERP.Module.Enum.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.HocSinh.Lops;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.BaoCao.TuyenSinh_TP
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách phụ huynh đã - chưa chăm sóc")]
    [Appearance("BaoCaoCham", TargetItems = "CongTy", Enabled = false, Criteria = "TatCa")]
    [Appearance("TheoNamHoc", TargetItems = "TuNgay;DenNgay", Visibility = ViewItemVisibility.Hide, Criteria = "!TheoNgayLap")]
    [Appearance("TheoNgayLap", TargetItems = "NamHoc", Visibility = ViewItemVisibility.Hide, Criteria = "TheoNgayLap")]

    public class BaoCao_TuyenSinh_DanhSachKhachHangDaChamSoc_Chua : StoreProcedureReport
    {
        //
        private bool _TatCa = false;
        private bool _DaChamSoc = true;
        private CongTy _CongTy;
        private bool _TheoNgayLap = false;
        private NamHoc _NamHoc;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả")]
        public bool TatCa
        {
            get
            {
                return _TatCa;
            }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
                if (!IsLoading)
                {
                    CongTy = null;
                }
            }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCa")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Theo ngày lập")]
        public bool TheoNgayLap
        {
            get
            {
                return _TheoNgayLap;
            }
            set
            {
                SetPropertyValue("TheoNgayLap", ref _TheoNgayLap, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TheoNgayLap")]
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

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TheoNgayLap")]
        [ModelDefault("Editmask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TheoNgayLap")]
        [ModelDefault("Editmask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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

        [ModelDefault("Caption", "Đã chăm sóc")]
        public bool DaChamSoc
        {
            get
            {
                return _DaChamSoc;
            }
            set
            {
                SetPropertyValue("DaChamSoc", ref _DaChamSoc, value);

            }
        }

        public BaoCao_TuyenSinh_DanhSachKhachHangDaChamSoc_Chua(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            TuNgay = DateTime.Today.SetTime(Enum.Systems.SetTimeEnum.StartMonth);
            DenNgay = DateTime.Today.SetTime(Enum.Systems.SetTimeEnum.EndMonth);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[6];
            if (TatCa)
                param[0] = new SqlParameter("@CongTy", Guid.Empty);
            else if (CongTy != null)
                param[0] = new SqlParameter("@CongTy", CongTy.Oid);
            param[1] = new SqlParameter("@TheoNgayLap", TheoNgayLap);
            param[2] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[3] = new SqlParameter("@TuNgay", TuNgay);
            param[4] = new SqlParameter("@DenNgay", DenNgay);
            param[5] = new SqlParameter("@DaChamSoc", DaChamSoc);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_TP_DanhSachKhachHangDaChamSoc", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
