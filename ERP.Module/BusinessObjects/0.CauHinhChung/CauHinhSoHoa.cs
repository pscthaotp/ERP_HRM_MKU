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
    [ModelDefault("Caption", "Cấu hình số hóa")]
    public class CauHinhSoHoa : BaseObject
    {
        //Tài khoản
        private string _Username;
        private string _Password;

        //Đườnng dẫn
        private string _URL_HoSoNhapHoc;
        private string _URL_NhanVien;
        private int _Width_NhanVien;
        private int _Height_NhanVien;
        private string _URL_HocSinh;
        private int _Width_HocSinh;
        private int _Height_HocSinh;
        private string _URL_NguoiDuaDon;
        private int _Width_NguoiDuaDon;
        private int _Height_NguoiDuaDon;
        private string _URL_ParentDoParentAsk;
        private int _Width_ParentDoParentAsk;
        private int _Height_ParentDoParentAsk;
        private string _URL_CongTy;
        private int _Width_CongTy;
        private int _Height_CongTy;
        private string _URL_TiengAnh;
        private int _Width_TiengAnh;
        private int _Height_TiengAnh;
        private string _URL_Bep;
        private int _Width_Bep;
        private int _Height_Bep;
        private string _URL_GiaoAn;
        private string _URL_DanThuoc;
        private int _Width_DanThuoc;
        private int _Height_DanThuoc;

        #region Thêm theo yêu cầu portal
        private string _URL_KhoanhKhacAbi;
        private string _URL_DanhGiaAbi;
        #endregion
        //
        private string _URL_ToTrinhMuaHang;
        private string _URL_GiayToHoSoNhanSu;
        //  
        [ModelDefault("Caption", "Tài khoản")]
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                SetPropertyValue("Username", ref _Username, value);
            }
        }

        //[ModelDefault("IsPassword", "True")]
        [ModelDefault("Caption", "Mật khẩu")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetPropertyValue("Password", ref _Password, value);
            }
        }

        [ModelDefault("Caption", "Hồ sơ nhập học")]
        public string URL_HoSoNhapHoc
        {
            get
            {
                return _URL_HoSoNhapHoc;
            }
            set
            {
                SetPropertyValue("URL_HoSoNhapHoc", ref _URL_HoSoNhapHoc, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public string URL_NhanVien
        {
            get
            {
                return _URL_NhanVien;
            }
            set
            {
                SetPropertyValue("URL_NhanVien", ref _URL_NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên (rộng)")]
        public int Width_NhanVien
        {
            get
            {
                return _Width_NhanVien;
            }
            set
            {
                SetPropertyValue("Width_NhanVien", ref _Width_NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên (cao)")]
        public int Height_NhanVien
        {
            get
            {
                return _Height_NhanVien;
            }
            set
            {
                SetPropertyValue("Height_NhanVien", ref _Height_NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Học sinh")]
        public string URL_HocSinh
        {
            get
            {
                return _URL_HocSinh;
            }
            set
            {
                SetPropertyValue("URL_HocSinh", ref _URL_HocSinh, value);
            }
        }

        [ModelDefault("Caption", "Học sinh (rộng)")]
        public int Width_HocSinh
        {
            get
            {
                return _Width_HocSinh;
            }
            set
            {
                SetPropertyValue("Width_HocSinh", ref _Width_HocSinh, value);
            }
        }

        [ModelDefault("Caption", "Học sinh (cao)")]
        public int Height_HocSinh
        {
            get
            {
                return _Height_HocSinh;
            }
            set
            {
                SetPropertyValue("Height_HocSinh", ref _Height_HocSinh, value);
            }
        }

        [ModelDefault("Caption", "Người đưa đón")]
        public string URL_NguoiDuaDon
        {
            get
            {
                return _URL_NguoiDuaDon;
            }
            set
            {
                SetPropertyValue("URL_NguoiDuaDon", ref _URL_NguoiDuaDon, value);
            }
        }

        [ModelDefault("Caption", "Người đưa đón (rộng)")]
        public int Width_NguoiDuaDon
        {
            get
            {
                return _Width_NguoiDuaDon;
            }
            set
            {
                SetPropertyValue("Width_NguoiDuaDon", ref _Width_NguoiDuaDon, value);
            }
        }

        [ModelDefault("Caption", "Người đưa đón (cao)")]
        public int Height_NguoiDuaDon
        {
            get
            {
                return _Height_NguoiDuaDon;
            }
            set
            {
                SetPropertyValue("Height_NguoiDuaDon", ref _Height_NguoiDuaDon, value);
            }
        }

        [ModelDefault("Caption", "Parent Do - Parent Ask")]
        public string URL_ParentDoParentAsk
        {
            get
            {
                return _URL_ParentDoParentAsk;
            }
            set
            {
                SetPropertyValue("URL_ParentDoParentAsk", ref _URL_ParentDoParentAsk, value);
            }
        }

        [ModelDefault("Caption", "Parent Do - Parent Ask (rộng)")]
        public int Width_ParentDoParentAsk
        {
            get
            {
                return _Width_ParentDoParentAsk;
            }
            set
            {
                SetPropertyValue("Width_ParentDoParentAsk", ref _Width_ParentDoParentAsk, value);
            }
        }

        [ModelDefault("Caption", "Parent Do - Parent Ask (cao)")]
        public int Height_ParentDoParentAsk
        {
            get
            {
                return _Height_ParentDoParentAsk;
            }
            set
            {
                SetPropertyValue("Height_ParentDoParentAsk", ref _Height_ParentDoParentAsk, value);
            }
        }

        [ModelDefault("Caption", "Logo công ty")]
        public string URL_CongTy
        {
            get
            {
                return _URL_CongTy;
            }
            set
            {
                SetPropertyValue("URL_CongTy", ref _URL_CongTy, value);
            }
        }

        [ModelDefault("Caption", "Logo công ty (rộng)")]
        public int Width_CongTy
        {
            get
            {
                return _Width_CongTy;
            }
            set
            {
                SetPropertyValue("Width_CongTy", ref _Width_CongTy, value);
            }
        }

        [ModelDefault("Caption", "Logo công ty (cao)")]
        public int Height_CongTy
        {
            get
            {
                return _Height_CongTy;
            }
            set
            {
                SetPropertyValue("Height_CongTy", ref _Height_CongTy, value);
            }
        }

        [ModelDefault("Caption", "Tiếng anh")]
        public string URL_TiengAnh
        {
            get
            {
                return _URL_TiengAnh;
            }
            set
            {
                SetPropertyValue("URL_TiengAnh", ref _URL_TiengAnh, value);
            }
        }

        [ModelDefault("Caption", "Tiếng anh (rộng)")]
        public int Width_TiengAnh
        {
            get
            {
                return _Width_TiengAnh;
            }
            set
            {
                SetPropertyValue("Width_TiengAnh", ref _Width_TiengAnh, value);
            }
        }

        [ModelDefault("Caption", "Tiếng anh (cao)")]
        public int Height_TiengAnh
        {
            get
            {
                return _Height_TiengAnh;
            }
            set
            {
                SetPropertyValue("Height_TiengAnh", ref _Height_TiengAnh, value);
            }
        }

        [ModelDefault("Caption", "Bếp")]
        public string URL_Bep
        {
            get
            {
                return _URL_Bep;
            }
            set
            {
                SetPropertyValue("URL_Bep", ref _URL_Bep, value);
            }
        }

        [ModelDefault("Caption", "Bếp (rộng)")]
        public int Width_Bep
        {
            get
            {
                return _Width_Bep;
            }
            set
            {
                SetPropertyValue("Width_Bep", ref _Width_Bep, value);
            }
        }

        [ModelDefault("Caption", "Bếp (cao)")]
        public int Height_Bep
        {
            get
            {
                return _Height_Bep;
            }
            set
            {
                SetPropertyValue("Height_Bep", ref _Height_Bep, value);
            }
        }

        [ModelDefault("Caption", "Dặn thuốc")]
        public string URL_DanThuoc
        {
            get
            {
                return _URL_DanThuoc;
            }
            set
            {
                SetPropertyValue("URL_DanThuoc", ref _URL_DanThuoc, value);
            }
        }

        [ModelDefault("Caption", "Dặn thuốc (rộng)")]
        public int Width_DanThuoc
        {
            get
            {
                return _Width_DanThuoc;
            }
            set
            {
                SetPropertyValue("Width_DanThuoc", ref _Width_DanThuoc, value);
            }
        }

        [ModelDefault("Caption", "Dặn thuốc (cao)")]
        public int Height_DanThuoc
        {
            get
            {
                return _Height_DanThuoc;
            }
            set
            {
                SetPropertyValue("Height_DanThuoc", ref _Height_DanThuoc, value);
            }
        }

        [ModelDefault("Caption", "Tờ trình mua hàng")]
        public string URL_ToTrinhMuaHang
        {
            get
            {
                return _URL_ToTrinhMuaHang;
            }
            set
            {
                SetPropertyValue("URL_ToTrinhMuaHang", ref _URL_ToTrinhMuaHang, value);
            }
        }

        [ModelDefault("Caption", "URL khoản khắc ABI")]
        public string URL_KhoanhKhacAbi
        {
            get
            {
                return _URL_KhoanhKhacAbi;
            }
            set
            {
                SetPropertyValue("URL_KhoanhKhacAbi", ref _URL_KhoanhKhacAbi, value);
            }
        }
        [ModelDefault("Caption", "URL Đánh giá ABI")]
        public string URL_DanhGiaAbi
        {
            get
            {
                return _URL_DanhGiaAbi;
            }
            set
            {
                SetPropertyValue("URL_DanhGiaAbi", ref _URL_DanhGiaAbi, value);
            }
        }

        [ModelDefault("Caption", "Giấy tờ, hồ sơ nhân sự")]
        public string URL_GiayToHoSoNhanSu
        {
            get
            {
                return _URL_GiayToHoSoNhanSu;
            }
            set
            {
                SetPropertyValue("URL_GiayToHoSoNhanSu", ref _URL_GiayToHoSoNhanSu, value);
            }
        }

        [ModelDefault("Caption", "Giáo án")]
        public string URL_GiaoAn
        {
            get
            {
                return _URL_GiaoAn;
            }
            set
            {
                SetPropertyValue("URL_GiaoAn", ref _URL_GiaoAn, value);
            }
        }

        public CauHinhSoHoa(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            Username = "erpftp";
            Password = "ttcedu";
            //
            URL_GiayToHoSoNhanSu = "ftp://images.igc.edu.vn/HRM%20INFO/";
            URL_GiaoAn = "ftp://images.igc.edu.vn/ERP/GiaoAn/";
            URL_HoSoNhapHoc = "C://PSC//ERP//Hosting//crm.ttcedu.vn//Files";
            URL_NhanVien = "ftp://images.igc.edu.vn/Images/NhanSu";
            Width_NhanVien = -1;
            Height_NhanVien = -1;
            URL_HocSinh = "ftp://images.igc.edu.vn/Images/HocSinh";
            Width_HocSinh = -1;
            Height_HocSinh = -1;
            URL_NguoiDuaDon = "ftp://images.igc.edu.vn/Images/NguoiDuaDon";
            Width_NguoiDuaDon = -1;
            Height_NguoiDuaDon = -1;
            URL_ParentDoParentAsk = "ftp://images.igc.edu.vn/Images/ParentDoParentAsk";
            Width_ParentDoParentAsk = -1;
            Height_ParentDoParentAsk = -1;
            URL_CongTy = "ftp://images.igc.edu.vn/Images/CongTy";
            Width_CongTy = -1;
            Height_CongTy = -1;
            URL_TiengAnh = "ftp://images.igc.edu.vn/Images/TiengAnh";
            Width_TiengAnh = -1;
            Height_TiengAnh = -1;
            URL_Bep = "ftp://images.igc.edu.vn/Images/Bep";
            Width_Bep = -1;
            Height_Bep = -1;
            URL_Bep = "ftp://images.igc.edu.vn/Images/imagedanthuoc";
            Width_Bep = -1;
            Height_Bep = -1;
        }
    }

}
