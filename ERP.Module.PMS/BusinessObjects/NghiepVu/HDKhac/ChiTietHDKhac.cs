using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.DanhMuc;
using ERP.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết HĐ Khác")]
    public class ChiTietHDKhac : BaseObject
    {
        #region key
        private QuanLyHDKhac _QuanLyHDKhac;
        [Association("QuanLyHDKhac-ListChiTietHDKhac")]
        [ModelDefault("Caption", "Quản lý hoạt động khác")]
        [Browsable(false)]
        public QuanLyHDKhac QuanLyHDKhac
        {
            get
            {
                return _QuanLyHDKhac;
            }
            set
            {
                SetPropertyValue("QuanLyHDKhac", ref _QuanLyHDKhac, value);
            }
        }
        #endregion

        private NhanVien _NhanVien;
        private DanhSachChiTietHDKhac _DanhMucHDKhac;
        private string _NoiDung;
        private decimal _SoTiet;
        private decimal _GioQuyDoiHDKhac;
        private string _GhiChu;
        private string _GhiChuHuyXacNhan;
        private DateTime _NgayThucHien;
        private DateTime _NgayXacNhan;
        private DateTime _NgayCapNhat;
        private bool _DuKien;
        private bool _XacNhan;
        private bool _HuyXacNhan;

        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        
        [ModelDefault("Caption", "Loại hoạt động quản lý")]
        public DanhSachChiTietHDKhac DanhMucHDKhac
        {
            get { return _DanhMucHDKhac; }
            set { SetPropertyValue("DanhMucHDKhac", ref _DanhMucHDKhac, value); 
            }
        }
        [ModelDefault("Caption", "Nội dung")]
        public string NoiDung
        {
            get { return _NoiDung; }
            set { SetPropertyValue("NoiDung", ref _NoiDung, value); }
        }
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
        public decimal GioQuyDoiHDKhac
        {
            get { return _GioQuyDoiHDKhac; }
            set { SetPropertyValue("GioQuyDoiHDKhac", ref _GioQuyDoiHDKhac, value); }
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
        [ModelDefault("Caption", "Ngày thực hiện")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime NgayThucHien
        {
            get { return _NgayThucHien; }
            set { SetPropertyValue("NgayThucHien", ref _NgayThucHien, value); }
        }
        [ModelDefault("Caption", "Ngày xác nhận")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime NgayXacNhan
        {
            get { return _NgayXacNhan; }
            set { SetPropertyValue("NgayXacNhan", ref _NgayXacNhan, value); }
        }
        [ModelDefault("Caption", "Ngày cập nhật")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime NgayCapNhat
        {
            get { return _NgayCapNhat; }
            set { SetPropertyValue("NgayCapNhat", ref _NgayCapNhat, value); }
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
        public ChiTietHDKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NgayThucHien = DateTime.Now;
        }
    }
}
