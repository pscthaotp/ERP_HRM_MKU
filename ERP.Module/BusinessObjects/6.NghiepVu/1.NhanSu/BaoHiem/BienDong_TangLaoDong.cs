using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.BaoHiem
{
    [ImageName("BO_BienDong")]
    [DefaultProperty("SoSoBHXH")]
    [ModelDefault("Caption", "Tăng lao động")]
    public class BienDong_TangLaoDong : BienDong
    {
        //
        private decimal _PCKhac;
        private decimal _ThamNien;
        private int _VuotKhung;
        private decimal _PCCV;
        private decimal _TienLuong;
        private bool _KhongThamGiaBHTN;
        private QuyenLoiHuongBHYT _QuyenLoiHuongBHYT;
        private DateTime _DenThang;
        private DateTime _TuThang;

        [ModelDefault("Caption", "Từ tháng, năm")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng, năm")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
            }
        }

        [ModelDefault("Caption", "Quyền lợi hưởng BHYT")]
        public QuyenLoiHuongBHYT QuyenLoiHuongBHYT
        {
            get
            {
                return _QuyenLoiHuongBHYT;
            }
            set
            {
                SetPropertyValue("QuyenLoiHuongBHYT", ref _QuyenLoiHuongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Không tham gia BHTN")]
        public bool KhongThamGiaBHTN
        {
            get
            {
                return _KhongThamGiaBHTN;
            }
            set
            {
                SetPropertyValue("KhongThamGiaBHTN", ref _KhongThamGiaBHTN, value);
            }
        }

        [ModelDefault("Caption", "Tiền lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCCV
        {
            get
            {
                return _PCCV;
            }
            set
            {
                SetPropertyValue("PCCV", ref _PCCV, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% Vượt khung")]
        public int VuotKhung
        {
            get
            {
                return _VuotKhung;
            }
            set
            {
                SetPropertyValue("VuotKhung", ref _VuotKhung, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% Thâm niên")]
        public decimal ThamNien
        {
            get
            {
                return _ThamNien;
            }
            set
            {
                SetPropertyValue("ThamNien", ref _ThamNien, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCKhac
        {
            get
            {
                return _PCKhac;
            }
            set
            {
                SetPropertyValue("PCKhac", ref _PCKhac, value);
            }
        }

        public BienDong_TangLaoDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiBienDong = "Tăng lao động";
        }

        protected override void AfterThongTinNhanVienChanged()
        {
            if (ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan != 0)
                TienLuong = ThongTinNhanVien.NhanVienThongTinLuong.LuongCoBan;
            else
                TienLuong = ThongTinNhanVien.NhanVienThongTinLuong.PhanTramTinhLuong * ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong / 100;
            PCCV = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            VuotKhung = ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
            ThamNien = ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
            PCKhac = ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();
        }
    }

}
