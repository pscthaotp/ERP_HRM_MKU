using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.MaTuDong;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.DanhMuc.PMS;

namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [DefaultProperty("HoTen")]
    [ModelDefault("Caption", "Giảng viên thỉnh giảng")]
    [ModelDefault("EditorTypeName", "ERP.Module.Win.Editors.NhanSu.NhanViens.CategorizedListEditor_GiangVienThinhGiang")]
    
    public class GiangVienThinhGiang : NhanVien
    {
        //private string _CMND_ThinhGiang;
        private string _DonViCongTac;
        private string _TaiLieuGiangDay;
        private BoPhan _TaiBoMon;
        private HopDongThinhGiang _HopDongThinhGiang;
        //private DonGiaDiLai _KhoanCach; ThuHuong tắt . Nghiệp vụ không sử dụng
        
        //[ModelDefault("Caption", "Số CMNDTG")]
        //[RuleUniqueValue("", DefaultContexts.Save)]
        //[RuleRequiredField("", DefaultContexts.Save)]
        //public string CMND_ThinhGiang
        //{
        //    get
        //    {
        //        return _CMND_ThinhGiang;
        //    }
        //    set
        //    {
        //        SetPropertyValue("CMND_ThinhGiang", ref _CMND_ThinhGiang, value);
        //if (!IsLoading && value != null)
        //    CMND = value;
        //    }
        //}

        [ModelDefault("Caption", "Tại Khoa/Bộ môn")]
        public BoPhan TaiBoMon
        {
            get
            {
                return _TaiBoMon;
            }
            set
            {
                SetPropertyValue("TaiBoMon", ref _TaiBoMon, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị công tác")]
        public string DonViCongTac
        {
            get
            {
                return _DonViCongTac;
            }
            set
            {
                SetPropertyValue("DonViCongTac", ref _DonViCongTac, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Tài liệu giảng dạy")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.DirectoryEditor")]
        public string TaiLieuGiangDay
        {
            get
            {
                return _TaiLieuGiangDay;
            }
            set
            {
                SetPropertyValue("TaiLieuGiangDay", ref _TaiLieuGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Hợp đồng hiện tại")]
        [DataSourceProperty("ListHopDong")]
        public HopDongThinhGiang HopDongThinhGiang
        {
            get
            {
                return _HopDongThinhGiang;
            }
            set
            {
                SetPropertyValue("HopDongThinhGiang", ref _HopDongThinhGiang, value);
            }
        }

        //[ModelDefault("Caption", "Khoản cách")]
        //public DonGiaDiLai KhoanCach
        //{
        //    get
        //    {
        //        return _KhoanCach;
        //    }
        //    set
        //    {
        //        SetPropertyValue("KhoanCach", ref _KhoanCach, value);
        //    }
        //}

        public GiangVienThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
            //Loại hồ sơ
            LoaiHoSo = LoaiHoSoEnum.GiangVien;

            //Công ty
            CongTy = Common.CongTy(Session);

            if (CongTy != null)
                BoPhan = Session.GetObjectByKey<BoPhan>(CongTy.Oid);
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted)
            {
                //Tạo nhân sự
                if (string.IsNullOrEmpty(MaNhanVien))
                    MaNhanVien = ManageKeyFactory.ManageKey(ManageKeyEnum.MaNhanVien);
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            //
            //Đồng bộ SIS
            if (OidHoSoCha == Guid.Empty)
            {
                DongBoThinhGiangSIS();
                CapNhatTaiKhoanPhanQuyenTong();
            }
        }

        public void DongBoThinhGiangSIS()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NhanVien", Oid);
            //
            DataProvider.ExecuteNonQuery("spd_HeThong_DongBoThinhGiangSIS", CommandType.StoredProcedure, parameter);
            //
        }

        public void CapNhatTaiKhoanPhanQuyenTong()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@NhanVien", Oid);
            //
            DataProvider.ExecuteNonQuery("spd_PhanQuyenTong_TaoTaiKhoanTG_URM", CommandType.StoredProcedure, parameter);
            //
        }
    }

}
