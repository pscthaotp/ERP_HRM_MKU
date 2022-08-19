using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using System.ComponentModel;
using DevExpress.Persistent.Base.General;
using System.Drawing;
using DevExpress.ExpressApp.Utils;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.HopDongs;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách thỉnh giảng")]
    public class ThinhGiangView : BaseObject
    {
        private string _MaTapDoan;
        private string _MaNhanVien;       
        private string _HoTen;
        //
        private GioiTinhEnum _GioiTinh;
        private DateTime _NgaySinh;
        private string _CMND;
        private DiaChi _DiaChiThuongTru;
        private DiaChi _NoiOHienNay;
        private string _Email;
        private string _DienThoaiDiDong;
       
        private DateTime _NgayVaoCongTy;
        private string _BoPhan;
        private string _CongTy;
        private string _ChucDanh;
        private string _LoaiHopDong;
        private string _TinhTrang;
        private string _TaiBoMon;
        private string _HopDongThinhGiang;
        private string _HocHam;
        private string _TrinhDoChuyenMon;
        private string _ChuyenMonDaoTao;
        //
        private Guid _Oid;
        private string _ClassName = "GiangVienThinhGiang";
        private ThinhGiangCustomView _ThinhGiangCustomView;

        [ModelDefault("Caption", "Mã tập đoàn")]
        public string MaTapDoan
        {
            get
            {
                return _MaTapDoan;
            }
            set
            {
                SetPropertyValue("MaTapDoan", ref _MaTapDoan, value);
            }
        }

        [ModelDefault("Caption", "Mã nhân sự")]
        [ModelDefault("DisplayFormat", "#####")]
        [ModelDefault("EditMask", "#####")]
        public string MaNhanVien
        {
            get
            {
                return _MaNhanVien;
            }
            set
            {
                SetPropertyValue("MaNhanVien", ref _MaNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [ModelDefault("Caption", "CMND")]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ thường trú")]
        public DiaChi DiaChiThuongTru
        {
            get
            {
                return _DiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru", ref _DiaChiThuongTru, value);
            }
        }

        [ModelDefault("Caption", "Nơi ở hiện nay")]
        public DiaChi NoiOHienNay
        {
            get
            {
                return _NoiOHienNay;
            }
            set
            {
                SetPropertyValue("NoiOHienNay", ref _NoiOHienNay, value);
            }
        }

        [ModelDefault("Caption", "Email")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong
        {
            get
            {
                return _DienThoaiDiDong;
            }
            set
            {
                SetPropertyValue("DienThoaiDiDong", ref _DienThoaiDiDong, value);
            }
        }

        [ModelDefault("Caption", "Công ty")]
        public string CongTy
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

        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
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

        [ModelDefault("Caption", "Tại bộ môn")]
        public string TaiBoMon
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

        [ModelDefault("Caption", "Chức danh")]
        public string ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào công ty")]
        public DateTime NgayVaoCongTy
        {
            get
            {
                return _NgayVaoCongTy;
            }
            set
            {
                SetPropertyValue("NgayVaoCongTy", ref _NgayVaoCongTy, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public string LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public string TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Hợp đồng thỉnh giảng")]
        public string HopDongThinhGiang
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

        [ModelDefault("Caption", "Học hàm")]
        public string HocHam
        {
            get
            {
                return _HocHam;
            }
            set
            {
                SetPropertyValue("HocHam", ref _HocHam, value);
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public string TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Chuyên môn đào tạo")]
        public string ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
            }
        }

        [Browsable(false)]
        public Guid Oid
        {
            get
            {
                return _Oid;
            }
            set
            {
                SetPropertyValue("Oid", ref _Oid, value);
            }
        }

        [Browsable(false)]
        public string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                SetPropertyValue("ClassName", ref _ClassName, value);
            }
        }

        [Browsable(false)]
        public ThinhGiangCustomView ThinhGiangCustomView
        {
            get
            {
                return _ThinhGiangCustomView;
            }
            set
            {
                SetPropertyValue("ThinhGiangCustomView", ref _ThinhGiangCustomView, value);
            }
        }

        public ThinhGiangView(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}