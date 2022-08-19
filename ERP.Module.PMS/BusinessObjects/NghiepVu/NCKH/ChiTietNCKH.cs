using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.DanhMuc;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết NCKH")]
    public class ChiTietNCKH : BaseObject
    {
        #region key
        private QuanLyNCKH _QuanLyNCKH;
        [Association("QuanLyNCKH-ListChiTietNCKH")]
        [ModelDefault("Caption", "Quản lý nghiên cứu khoa học")]
        [Browsable(false)]
        public QuanLyNCKH QuanLyNCKH
        {
            get
            {
                return _QuanLyNCKH;
            }
            set
            {
                SetPropertyValue("QuanLyNCKH", ref _QuanLyNCKH, value);
            }
        }
        #endregion

        private DanhSachChiTietHDKhac _DanhMucNCKH;
        private NhanVien _NhanVien;
        private string _TenDeTai;
        private string _TenTapChi_HoiThao;
        private string _ChiSo;
        private DateTime _ThoiGianXuatBan;
        //
        private string _FileDinhKem;
        //
        private int _SoTinChi;
        private string _KhoaNganh; 
        //
        private decimal _SoTiet;
        private DateTime _NgayNhap;
        private decimal _GioQuyDoiNCKH;
        private int _SoLuongTV;
        private VaiTroNCKH _VaiTro;
        private bool _DuKien;
        private bool _XacNhan;
        private bool _HuyXacNhan;
        private DateTime _NgayXacNhan;
        private DateTime _NgayCapNhat;
        private string _NguoiCapNhat;
        private decimal _DinhMuc;
        private string _GhiChu;
        private string _GhiChuHuyXacNhan; 

        //

        [ModelDefault("Caption", "Loại NCKH")]
        //[DataSourceCriteria("DanhMucNCKH.NhomHoatDong.LoaiHoatDong = 7")]
        public DanhSachChiTietHDKhac DanhMucNCKH
        {
            get { return _DanhMucNCKH; }
            set { SetPropertyValue("DanhMucNCKH", ref _DanhMucNCKH, value); }
        }
        [ModelDefault("Caption","Giảng viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Tên đề tài")]
        public string TenDeTai
        {
            get { return _TenDeTai; }
            set { SetPropertyValue("TenDeTai", ref _TenDeTai, value); }
        }
        [ModelDefault("Caption", "Tên tạp chí / Hội thảo")]
        public string TenTapChi_HoiThao
        {
            get { return _TenTapChi_HoiThao; }
            set { SetPropertyValue("TenTapChi_HoiThao", ref _TenTapChi_HoiThao, value); }
        }
        [ModelDefault("Caption", "Chỉ số")]
        public string ChiSo
        {
            get { return _ChiSo; }
            set { SetPropertyValue("ChiSo", ref _ChiSo, value); }
        }
        [ModelDefault("Caption", "Thời gian xuất bản")]
        public DateTime ThoiGianXuatBan
        {
            get { return _ThoiGianXuatBan; }
            set { SetPropertyValue("ThoiGianXuatBan", ref _ThoiGianXuatBan, value); }
        }
        //
        [ModelDefault("Caption", "Thông tin File đính kèm")]
        public string FileDinhKem
        {
            get { return _FileDinhKem; }
            set { SetPropertyValue("FileDinhKem", ref _FileDinhKem, value); }
        }
        //
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public int SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }
        [ModelDefault("Caption", "Tên khóa ngành")]
        public string KhoaNganh
        {
            get { return _KhoaNganh; }
            set { SetPropertyValue("KhoaNganh", ref _KhoaNganh, value); }
        }
        //
        [ModelDefault("Caption", "Số tiết")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTiet
        {
            get { return _SoTiet; }
            set { SetPropertyValue("SoTiet", ref _SoTiet, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiNCKH
        {
            get { return _GioQuyDoiNCKH; }
            set { SetPropertyValue("GioQuyDoiNCKH", ref _GioQuyDoiNCKH, value); }
        }

        [ModelDefault("Caption", "Định mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMuc
        {
            get { return _DinhMuc; }
            set { SetPropertyValue("DinhMuc", ref _DinhMuc, value); }
        }

        [ModelDefault("Caption", "Số lượng TV")]
        public int SoLuongTV
        {
            get { return _SoLuongTV; }
            set { SetPropertyValue("SoLuongTV", ref _SoLuongTV, value); }
        }

        [ModelDefault("Caption", "Vai trò")]
        public VaiTroNCKH VaiTro
        {
            get { return _VaiTro; }
            set { SetPropertyValue("VaiTro", ref _VaiTro, value); }
        }


        [ModelDefault("Caption", "Dự kiến")]
        public bool DuKien
        {
            get { return _DuKien; }
            set { SetPropertyValue("DuKien", ref _DuKien, value); }
        }

        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }

        [ModelDefault("Caption", "Hủy xác nhận")]
        public bool HuyXacNhan
        {
            get { return _HuyXacNhan; }
            set { SetPropertyValue("HuyXacNhan", ref _HuyXacNhan, value); }
        }

        [ModelDefault("Caption", "Ngày nhập")]
        [Browsable(false)]
        public DateTime NgayNhap
        {
            get { return _NgayNhap; }
            set { SetPropertyValue("NgayNhap", ref _NgayNhap, value); }
        }

        [ModelDefault("Caption", "Ngày xác nhận")]
        [Browsable(false)]
        public DateTime NgayXacNhan
        {
            get { return _NgayXacNhan; }
            set { SetPropertyValue("NgayXacNhan", ref _NgayXacNhan, value); }
        }

        [ModelDefault("Caption", "Ngày cập nhật")]
        [Browsable(false)]
        public DateTime NgayCapNhat
        {
            get { return _NgayCapNhat; }
            set { SetPropertyValue("NgayCapNhat", ref _NgayCapNhat, value); }
        }

        [ModelDefault("Caption","Người cập nhật")]
        [Browsable(false)]
        public string NguoiCapNhat
        {
            get { return _NguoiCapNhat; }
            set { SetPropertyValue("NguoiCapNhat", ref _NguoiCapNhat, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Ghi chú hủy xác nhận")]
        public string GhiChuHuyXacNhan
        {
            get { return _GhiChuHuyXacNhan; }
            set { SetPropertyValue("GhiChuHuyXacNhan", ref _GhiChuHuyXacNhan, value); }
        }
        public ChiTietNCKH(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
