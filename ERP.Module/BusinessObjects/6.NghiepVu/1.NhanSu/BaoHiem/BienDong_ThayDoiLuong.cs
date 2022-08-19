using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.NghiepVu.NhanSu.BaoHiem
{
    [ImageName("BO_BienDong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Điều chỉnh lương")]
    public class BienDong_ThayDoiLuong : BienDong
    {
        //
        private bool _Tang;
        private decimal _TNGDMoi;
        private int _TNVKMoi;
        private decimal _PCKMoi; 
        private decimal _PCCVMoi;
        private decimal _TienLuongMoi;
        private decimal _PCKCu;
        private decimal _TNGDCu;
        private int _TNVKCu;
        private decimal _PCCVCu;
        private decimal _TienLuongCu;

        [ModelDefault("Caption", "Tiền lương cũ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienLuongCu
        {
            get
            {
                return _TienLuongCu;
            }
            set
            {
                SetPropertyValue("TienLuongCu", ref _TienLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp chức vụ cũ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCCVCu
        {
            get
            {
                return _PCCVCu;
            }
            set
            {
                SetPropertyValue("PCCVCu", ref _PCCVCu, value);
            }
        }

        [ModelDefault("Caption", "% Vượt khung cũ")]
        public int TNVKCu
        {
            get
            {
                return _TNVKCu;
            }
            set
            {
                SetPropertyValue("TNVKCu", ref _TNVKCu, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% Thâm niên cũ")]
        public decimal TNGDCu
        {
            get
            {
                return _TNGDCu;
            }
            set
            {
                SetPropertyValue("TNGDCu", ref _TNGDCu, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp khác cũ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCKCu
        {
            get
            {
                return _PCKCu;
            }
            set
            {
                SetPropertyValue("PCKCu", ref _PCKCu, value);
            }
        }

        [ModelDefault("Caption", "Tiền lương mới")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienLuongMoi
        {
            get
            {
                return _TienLuongMoi;
            }
            set
            {
                SetPropertyValue("TienLuongMoi", ref _TienLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp chức vụ mới")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCCVMoi
        {
            get
            {
                return _PCCVMoi;
            }
            set
            {
                SetPropertyValue("PCCVMoi", ref _PCCVMoi, value);
            }
        }

        [ModelDefault("Caption", "% Vượt khung mới")]
        public int TNVKMoi
        {
            get
            {
                return _TNVKMoi;
            }
            set
            {
                SetPropertyValue("TNVKMoi", ref _TNVKMoi, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% Thâm niên mới")]
        public decimal TNGDMoi
        {
            get
            {
                return _TNGDMoi;
            }
            set
            {
                SetPropertyValue("TNGDMoi", ref _TNGDMoi, value);
            }
        }

        [ModelDefault("Caption", "Phụ các khác mới")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCKMoi
        {
            get
            {
                return _PCKMoi;
            }
            set
            {
                SetPropertyValue("PCKMoi", ref _PCKMoi, value);
            }
        }

        //Dùng để xác định biến động tăng giảm
        [Browsable(false)]
        [ImmediatePostData]
        public bool Tang
        {
            get
            {
                return _Tang;
            }
            set
            {
                SetPropertyValue("Tang", ref _Tang, value);
                if (!IsLoading)
                {
                    if (value)
                        LoaiBienDong = "Tăng mức đóng";
                    else
                        LoaiBienDong = "Giảm mức đóng";
                }
            }
        }

        public BienDong_ThayDoiLuong(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
        }

        protected override void AfterThongTinNhanVienChanged()
        {
            TienLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            TienLuongMoi = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            PCCVCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            PCCVMoi = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            TNVKCu = ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
            TNVKMoi = ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
            TNGDCu = ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
            TNGDMoi = ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
            PCKCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac;
            PCKMoi = ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if (TienLuongMoi > TienLuongCu || PCCVMoi > PCCVCu || TNVKMoi > TNVKCu || TNGDMoi > TNGDCu || PCKMoi > PCKCu)
                    Tang = true;
                else
                    Tang = false;
            }
        }
    }

}
