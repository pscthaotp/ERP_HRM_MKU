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
using ERP.Module.DanhMuc.TuyenSinh_TP;

namespace ERP.Module.BaoCao.TuyenSinh_TP
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách học sinh đã đến từ trường - tỉnh thành")]
    [Appearance("BaoCaoCham", TargetItems = "TinhThanh", Enabled = false, Criteria = "TatCa")]
    public class BaoCao_TuyenSinh_DanhSachHocSinh_QuanHuyen : StoreProcedureReport
    {
        //
        private bool _TatCa = false;
        private TinhThanh _TinhThanh;
        private CongTy _CongTy;
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
        [ModelDefault("AllowEdit","False")]
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

        //[ModelDefault("Caption", "Danh sách trường")]
        //public DanhMucTruong DanhMucTruong
        //{
        //    get
        //    {
        //        return _DanhMucTruong;
        //    }
        //    set
        //    {
        //        SetPropertyValue("DanhMucTruong", ref _DanhMucTruong, value);

        //    }
        //}

        [ModelDefault("Caption", "Tỉnh thành")]
        public TinhThanh TinhThanh
        {
            get
            {
                return _TinhThanh;
            }
            set
            {
                SetPropertyValue("TinhThanh", ref _TinhThanh, value);

            }
        }

        //[ModelDefault("Caption", "Quận huyện")]
        //public QuanHuyen QuanHuyen
        //{
        //    get
        //    {
        //        return _QuanHuyen;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanHuyen", ref _QuanHuyen, value);

        //    }
        //}
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

        public BaoCao_TuyenSinh_DanhSachHocSinh_QuanHuyen(Session session) : base(session) { }
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
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CongTy", CongTy.Oid);
            param[1] = new SqlParameter("@NamHoc", DenNgay);
            if (TatCa)
                param[2] = new SqlParameter("@TinhThanh", Guid.Empty);
            else if (CongTy != null)
                param[2] = new SqlParameter("@TinhThanh", TinhThanh.Oid);


            param[3] = new SqlParameter("@TuNgay", TuNgay);
            param[4] = new SqlParameter("@DenNgay", DenNgay);
 
            ////
            SqlCommand cmd = DataProvider.GetCommand("spd_Rpt_TuyenSinh_TP_DanhSachKhachHangDaChamSoc", CommandType.StoredProcedure, param);
            ////
            return cmd;
        }
    }
}
