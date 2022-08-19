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

namespace ERP.Module.BaoCao.TuyenSinh_TP
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách phụ huynh đã - chưa tư vấn")]
    [Appearance("BaoCaoTuVan", TargetItems = "DaTuVan", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_DanhSachKhachHangDaTuVan_Chua : StoreProcedureReport
    {
        //
        private bool _TatCa = false;
        private bool _DaTuVan = true;
        private CongTy _CongTy;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private LoaiKhachHangTuyenSinhEnum _LoaiKhachHang = LoaiKhachHangTuyenSinhEnum.KhongXacDinh;

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

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Loại khách hàng")]

        public LoaiKhachHangTuyenSinhEnum LoaiKhachHang
        {
            get
            {
                return _LoaiKhachHang;
            }
            set
            {
                SetPropertyValue("LoaiKhachHang", ref _LoaiKhachHang, value);

            }
        }
        [ModelDefault("Caption", "Đã tư vấn")]
        public bool DaTuVan
        {
            get
            {
                return _DaTuVan;
            }
            set
            {
                SetPropertyValue("DaTuVan", ref _DaTuVan, value);

            }
        }
        public BaoCao_TuyenSinh_DanhSachKhachHangDaTuVan_Chua(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
        }
        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CongTy", CongTy.Oid);
            param[1] = new SqlParameter("@TuNgay", TuNgay);
            param[2] = new SqlParameter("@DenNgay", DenNgay);
            param[3] = new SqlParameter("@DaTuVan", DaTuVan);
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_TP_DanhSachTrungTuyen", CommandType.StoredProcedure, param);
            //
            return cmd;
        }
    }
}
