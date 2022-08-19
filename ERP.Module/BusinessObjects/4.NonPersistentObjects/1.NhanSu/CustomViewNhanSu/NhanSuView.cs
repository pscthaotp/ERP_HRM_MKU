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

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách nhân sự")]
    public class NhanSuView : BaseObject
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
        private string _EmailNoiBo;
        private string _DienThoaiDiDong;
        private string _DienThoaiNoiBo;
       
        private DateTime _NgayVaoCongTy;
        private DateTime _NgayVaoTapDoan;
        private string _ThamNienLamViec;
        private string _BoPhan;
        private string _CongTy;
        private string _To;
        
        private string _LoaiHopDong;
        private string _TinhTrang;        
        private DateTime _NgayNghiViec;
        private string _LoaiNhanSu;
        private string _ChucVu;
        private string _ChucDanh;
        
        private string _HocHam;
        private string _TrinhDoChuyenMon;
        private bool _GiangDay;
        private string _NhomPhanBo;
        //
        private Guid _Oid;
        private string _ClassName = "ThongTinNhanVien";
        private NhanSuCustomView _NhanSuCustomView;

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

        [ModelDefault("Caption", "Tổ")]
        public string To
        {
            get
            {
                return _To;
            }
            set
            {
                SetPropertyValue("MaHoSo", ref _To, value);
            }
        }

        [ModelDefault("Caption", "Ngày nghỉ việc")]
        public DateTime NgayNghiViec
        {
            get
            {
                return _NgayNghiViec;
            }
            set
            {
                SetPropertyValue("NgayNghiViec", ref _NgayNghiViec, value);
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

        [ModelDefault("Caption", "Email nội bộ")]
        public string EmailNoiBo
        {
            get
            {
                return _EmailNoiBo;
            }
            set
            {
                SetPropertyValue("EmailNoiBo", ref _EmailNoiBo, value);
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

        [ModelDefault("Caption", "Điện thoại nội bộ")]
        public string DienThoaiNoiBo
        {
            get
            {
                return _DienThoaiNoiBo;
            }
            set
            {
                SetPropertyValue("DienThoaiNoiBo", ref _DienThoaiNoiBo, value);
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

        [ModelDefault("Caption", "Học vị")]
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

        [ModelDefault("Caption", "Loại nhân sự")]
        public string LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public string ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
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

       [ModelDefault("Caption", "Ngày vào tập đoàn")]
        public DateTime NgayVaoTapDoan
        {
            get
            {
                return _NgayVaoTapDoan;
            }
            set
            {
                SetPropertyValue("NgayVaoTapDoan", ref _NgayVaoTapDoan, value);
            }
        }

       //[ModelDefault("Caption", "Thâm niên làm việc")]
       //public string ThamNienLamViec
       //{
       //    get
       //    {
       //        return _ThamNienLamViec;
       //    }
       //    set
       //    {
       //        SetPropertyValue("ThamNienLamViec", ref _ThamNienLamViec, value);
       //    }
       //}

        [ModelDefault("Caption", "Giảng dạy")]
        public bool GiangDay
        {
            get
            {
                return _GiangDay;
            }
            set
            {
                SetPropertyValue("GiangDay", ref _GiangDay, value);
            }
        }

        [ModelDefault("Caption", "Mã phân bổ")]
        public string NhomPhanBo
        {
            get
            {
                return _NhomPhanBo;
            }
            set
            {
                SetPropertyValue("NhomPhanBo", ref _NhomPhanBo, value);
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
        public NhanSuCustomView NhanSuCustomView
        {
            get
            {
                return _NhanSuCustomView;
            }
            set
            {
                SetPropertyValue("NhanSuCustomView", ref _NhanSuCustomView, value);
            }
        }

        public NhanSuView(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}