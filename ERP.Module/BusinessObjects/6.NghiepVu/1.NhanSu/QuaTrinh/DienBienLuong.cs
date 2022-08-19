using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    //[ImageName("BO_QuaTrinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Diễn biến lương và phụ cấp")]
    [RuleCombinationOfPropertiesIsUnique("DienBienLuong.Identifier", DefaultContexts.Save, "ThongTinNhanVien;SoQuyetDinh", "Quyết định nâng lương đã tồn tại trong hệ thống.")]
     public class DienBienLuong : BaseObject
    {
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private QuyetDinh _QuyetDinh;       
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        //
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _PhanTramTinhLuong;
        private decimal _LuongCoBan;
        private decimal _LuongKinhDoanh;
        private decimal _PhuCapKiemNhiem;
        private decimal _PhuCapTrachNhiem;
        private decimal _PhuCapBanTru;
        private decimal _PhuCapDienThoai;
        private decimal _PhuCapTienAn;
        private decimal _PhuCapTienXang;
        private string _LyDo;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListDienBienLuong")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        [Browsable(false)]
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
                if (!IsLoading && value != null)
                {
                    LyDo = value.NoiDung;
                    SoQuyetDinh = value.SoQuyetDinh;
                    NgayQuyetDinh = value.NgayQuyetDinh;
                }
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Lương chức danh")]
        public decimal LuongCoBan
        {
            get
            {
                return _LuongCoBan;
            }
            set
            {
                SetPropertyValue("LuongCoBan", ref _LuongCoBan, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Lương bổ sung(HQCV)")]
        public decimal LuongKinhDoanh
        {
            get
            {
                return _LuongKinhDoanh;
            }
            set
            {
                SetPropertyValue("LuongKinhDoanh", ref _LuongKinhDoanh, value);
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

        [DataSourceProperty("NgachLuong.ListBacLuong")]
        [ModelDefault("Caption", "Bậc lương")]
        [ImmediatePostData()]
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

        [ModelDefault("Caption", "Phụ cấp kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapKiemNhiem
        {
            get
            {
                return _PhuCapKiemNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiem", ref _PhuCapKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp chủ nhiệm")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapTrachNhiem
        {
            get
            {
                return _PhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiem", ref _PhuCapTrachNhiem, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp bán trú")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapBanTru
        {
            get
            {
                return _PhuCapBanTru;
            }
            set
            {
                SetPropertyValue("PhuCapBanTru", ref _PhuCapBanTru, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienAn
        {
            get
            {
                return _PhuCapTienAn;
            }
            set
            {
                SetPropertyValue("PhuCapTienAn", ref _PhuCapTienAn, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp điện thoại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDienThoai
        {
            get
            {
                return _PhuCapDienThoai;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoai", ref _PhuCapDienThoai, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienXang
        {
            get
            {
                return _PhuCapTienXang;
            }
            set
            {
                SetPropertyValue("PhuCapTienXang", ref _PhuCapTienXang, value);
            }
        }


        [Size(-1)]
        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "% tính lương")]
        public decimal PhanTramTinhLuong
        {
            get
            {
                return _PhanTramTinhLuong;
            }
            set
            {
                SetPropertyValue("PhanTramTinhLuong", ref _PhanTramTinhLuong, value);
            }
        }
               
        public DienBienLuong(Session session) : base(session) { }
    }

}
