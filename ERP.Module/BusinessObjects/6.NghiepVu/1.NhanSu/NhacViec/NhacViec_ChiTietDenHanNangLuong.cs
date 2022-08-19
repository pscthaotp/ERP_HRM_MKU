using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;

//
namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nội dung chi tiết - Đến hạn nâng lương")]
    public class NhacViec_ChiTietDenHanNangLuong : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _NgayHuongLuong;
        private DateTime _MocNangLuong;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private int _PhanTramVuotKhung;
        private string _GhiChu;

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    BoPhan = value.BoPhan;
                    MocNangLuong = value.NhanVienThongTinLuong.MocNangLuongDieuChinh != DateTime.MinValue ? value.NhanVienThongTinLuong.MocNangLuongDieuChinh : value.NhanVienThongTinLuong.MocNangLuongLanSau;
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuong = value.NhanVienThongTinLuong.BacLuong;
                    PhanTramVuotKhung = value.NhanVienThongTinLuong.VuotKhung;
                    GhiChu = "Đến hạn nâng lương vào ngày " + MocNangLuong.ToString("d");
                }
            }
        }

        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }


        [ModelDefault("Caption", "Bậc lương")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "% vượt khung")]
        public int PhanTramVuotKhung
        {
            get
            {
                return _PhanTramVuotKhung;
            }
            set
            {
                SetPropertyValue("PhanTramVuotKhung", ref _PhanTramVuotKhung, value);
            }
        }

        [ModelDefault("Caption", "Ngày hương lương")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương")]
        [ModelDefault("DisplayFormat","dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime MocNangLuong
        {
            get
            {
                return _MocNangLuong;
            }
            set
            {
                SetPropertyValue("MocNangLuong", ref _MocNangLuong, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public NhacViec_ChiTietDenHanNangLuong(Session session) : base(session) { }

    }

}
