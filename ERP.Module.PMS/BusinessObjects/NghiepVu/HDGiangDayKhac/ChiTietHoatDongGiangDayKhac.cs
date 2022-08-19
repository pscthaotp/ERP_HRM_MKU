using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết HĐ giảng dạy Khác")]
    public class ChiTietHoatDongGiangDayKhac : BaseObject
    {
        private QuanLyHDGiangDayKhac _QuanLyHDGiangDayKhac;
        [Association("QuanLyHDGiangDayKhac-ListChiTietHoatDongGiangDayKhac")]
        [ModelDefault("Caption", "Quản lý hoạt động khác")]
        [Browsable(false)]
        public QuanLyHDGiangDayKhac QuanLyHDGiangDayKhac
        {
            get
            {
                return _QuanLyHDGiangDayKhac;
            }
            set
            {
                SetPropertyValue("QuanLyHDGiangDayKhac", ref _QuanLyHDGiangDayKhac, value);
            }
        }

        private NhanVien _NhanVien;
        private NhomHoatDong _NhomHoatDong;
        private DanhSachChiTietHDKhac _DanhMucHDKhac;
        private string _MaLop;
        private string _Nhom;
        private decimal _SoLuong;
        private decimal _GioQuyDoiHDGiangDayKhac;
        private DateTime _NgayThucHien;
        private string _GhiChu;

        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Nhóm hoạt động")]
        [ImmediatePostData]
        [DataSourceProperty("listnhomhd")]
        public NhomHoatDong NhomHoatDong
        {
            get { return _NhomHoatDong; }
            set
            {
                SetPropertyValue("NhomHoatDong", ref _NhomHoatDong, value);
                if (!IsLoading && value != null)
                {
                    UpdateDanhMucHDKhac();
                }
            }
        }
        [ModelDefault("Caption", "Loại hoạt động giảng dạy")]
        [DataSourceProperty("listds")]
        public DanhSachChiTietHDKhac DanhMucHDKhac
        {
            get { return _DanhMucHDKhac; }
            set
            {
                SetPropertyValue("DanhMucHDKhac", ref _DanhMucHDKhac, value);
            }
        }
        [ModelDefault("Caption", "Mã lớp")]
        public string MaLop
        {
            get { return _MaLop; }
            set { SetPropertyValue("MaLop", ref _MaLop, value); }
        }
        [ModelDefault("Caption", "Nhóm")]
        public string Nhom
        {
            get { return _Nhom; }
            set { SetPropertyValue("Nhom", ref _Nhom, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiHDGiangDayKhac
        {
            get { return _GioQuyDoiHDGiangDayKhac; }
            set { SetPropertyValue("GioQuyDoiHDGiangDayKhac", ref _GioQuyDoiHDGiangDayKhac, value); }
        }
        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoLuong
        {
            get { return _SoLuong; }
            set { SetPropertyValue("SoLuong", ref _SoLuong, value); }
        }
        [ModelDefault("Caption", "Ngày thực hiện")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime NgayThucHien
        {
            get { return _NgayThucHien; }
            set { SetPropertyValue("NgayThucHien", ref _NgayThucHien, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        //

        [Browsable(false)]
        public XPCollection<NhomHoatDong> listnhomhd { get; set; }

        [Browsable(false)]
        public XPCollection<DanhSachChiTietHDKhac> listds { get; set; }

        //

        public ChiTietHoatDongGiangDayKhac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NgayThucHien = DateTime.Now;
            QuanLyHDGiangDayKhac = new QuanLyHDGiangDayKhac(Session);
            if (QuanLyHDGiangDayKhac != null)
                   UpdateNhomHoatDong();
        }
        public void UpdateDanhMucHDKhac()
        {
            if (listds != null)
            {
                listds.Reload();
            }
            else
            {
                listds = new XPCollection<DanhSachChiTietHDKhac>(Session, false);
            }
            CriteriaOperator filter = CriteriaOperator.Parse("NhomHoatDong = ?", NhomHoatDong.Oid);
            listds = new XPCollection<DanhSachChiTietHDKhac>(Session, filter);
            OnChanged("listds");
        }
        public void UpdateNhomHoatDong()
        {
            if (listnhomhd != null)
            {
                listnhomhd.Reload();
            }
            else
            {
                listnhomhd = new XPCollection<NhomHoatDong>(Session, false);
            }
            //Chỉ load thông tin LoaiHoatDong là Giảng dạy
            CriteriaOperator filter = CriteriaOperator.Parse("LoaiHoatDong = 8");
            listnhomhd = new XPCollection<NhomHoatDong>(Session, filter);
            OnChanged("listnhomhd");
        }

    }
}
