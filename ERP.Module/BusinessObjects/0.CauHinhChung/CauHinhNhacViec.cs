using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình nhắc việc")]
    public class CauHinhNhacViec : BaseObject
    {
        // Fields...
        private bool _TheoDoiSinhNhat;
        private bool _TheoDoiDenHanNangLuong;
        private bool _TheoDoiDenHanNangThamNien;
        private bool _TheoDoiHetNhiemKyChucVu;
        private bool _TheoDoiHetHanNghiBHXH;
        private bool _TheoDoiHetHanHopDong;
        private bool _TheoDoiDenTuoiNghiHuu;
        //
        private int _SoThangTruocKhiHetHanHopDong;
        private int _SoThangTruocKhiNghiHuu;
        private int _SoThangTruocKhiHetNhiemKyChucVu;
        private int _SoThangTruocKhiNangLuong;
        private int _SoThangTruocKhiNangThamNien;
        private int _SoThangTruocKhiHetHanNghiThaiSan;
        private int _SoThangTruocKhiDenSinhNhat;


        [ModelDefault("Caption", "Theo dõi hết hạn hợp đồng")]
        public bool TheoDoiHetHanHopDong
        {
            get
            {
                return _TheoDoiHetHanHopDong;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanHopDong", ref _TheoDoiHetHanHopDong, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi hết hạn nghỉ hưởng BHXH")]
        public bool TheoDoiHetHanNghiBHXH
        {
            get
            {
                return _TheoDoiHetHanNghiBHXH;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanNghiBHXH", ref _TheoDoiHetHanNghiBHXH, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Theo dõi hết nhiệm kỳ chức vụ")]
        public bool TheoDoiHetNhiemKyChucVu
        {
            get
            {
                return _TheoDoiHetNhiemKyChucVu;
            }
            set
            {
                SetPropertyValue("TheoDoiHetNhiemKyChucVu", ref _TheoDoiHetNhiemKyChucVu, value);
            }
        }

        [ModelDefault("Caption", "Số ngày trước khi hết nhiệm kỳ chức vụ")]
        public int SoThangTruocKhiHetNhiemKyChucVu
        {
            get
            {
                return _SoThangTruocKhiHetNhiemKyChucVu;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetNhiemKyChucVu", ref _SoThangTruocKhiHetNhiemKyChucVu, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi đến hạn nâng lương")]
        public bool TheoDoiDenHanNangLuong
        {
            get
            {
                return _TheoDoiDenHanNangLuong;
            }
            set
            {
                SetPropertyValue("TheoDoiDenHanNangLuong", ref _TheoDoiDenHanNangLuong, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi đến hạn nâng thâm niên")]
        public bool TheoDoiDenHanNangThamNien
        {
            get
            {
                return _TheoDoiDenHanNangThamNien;
            }
            set
            {
                SetPropertyValue("TheoDoiDenHanNangThamNien", ref _TheoDoiDenHanNangThamNien, value);
            }
        }


        [ModelDefault("Caption", "Theo dõi sinh nhật")]
        public bool TheoDoiSinhNhat
        {
            get
            {
                return _TheoDoiSinhNhat;
            }
            set
            {
                SetPropertyValue("TheoDoiSinhNhat", ref _TheoDoiSinhNhat, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Theo dõi đến tuổi nghỉ hưu")]
        public bool TheoDoiDenTuoiNghiHuu
        {
            get
            {
                return _TheoDoiDenTuoiNghiHuu;
            }
            set
            {
                SetPropertyValue("TheoDoiDenTuoiNghiHuu", ref _TheoDoiDenTuoiNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "Số ngày trước khi nghỉ hưu")]
        public int SoThangTruocKhiNghiHuu
        {
            get
            {
                return _SoThangTruocKhiNghiHuu;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiNghiHuu", ref _SoThangTruocKhiNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "Số ngày trước khi hết hạn hợp đồng")]
        public int SoThangTruocKhiHetHanHopDong
        {
            get
            {
                return _SoThangTruocKhiHetHanHopDong;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetHanHopDong", ref _SoThangTruocKhiHetHanHopDong, value);
            }
        }

        [ModelDefault("Caption", "Số ngày trước khi nâng lương")]
        public int SoThangTruocKhiNangLuong
        {
            get
            {
                return _SoThangTruocKhiNangLuong;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiNangLuong", ref _SoThangTruocKhiNangLuong, value);
            }
        }

        [ModelDefault("Caption", "Số ngày trước khi nâng thâm niên")]
        public int SoThangTruocKhiNangThamNien
        {
            get
            {
                return _SoThangTruocKhiNangThamNien;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiNangThamNien", ref _SoThangTruocKhiNangThamNien, value);
            }
        }

        [ModelDefault("Caption", "Số ngày trước khi hết hạn nghỉ thai sản")]
        public int SoThangTruocKhiHetHanNghiThaiSan
        {
            get
            {
                return _SoThangTruocKhiHetHanNghiThaiSan;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetHanNghiThaiSan", ref _SoThangTruocKhiHetHanNghiThaiSan, value);
            }
        }

        [ModelDefault("Caption", "Số ngày trước khi đến sinh nhật")]
        public int SoThangTruocKhiDenSinhNhat
        {
            get
            {
                return _SoThangTruocKhiDenSinhNhat;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiDenSinhNhat", ref _SoThangTruocKhiDenSinhNhat, value);
            }
        }

        public CauHinhNhacViec(Session session) : base(session) { }

    }

}
