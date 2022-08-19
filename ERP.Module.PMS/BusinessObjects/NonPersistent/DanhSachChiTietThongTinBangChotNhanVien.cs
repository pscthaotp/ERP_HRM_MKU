using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.PMS.Enum;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.NghiepVu;
using ERP.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;

namespace ERP.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách chi tiết thông tin thanh toán thù lao")]
    public class DanhSachChiTietThongTinBangChotNhanVien : BaseObject
    {

        private ThongTinBangChot _ThongTinBangChot;
        private string _Oid;
        #region Hoạt động
        private string _BacDaoTao;
        private string _HeDaoTao;
        private LoaiHoatDongEnum _LoaiHoatDong;
        private string _NoiDungHoatDong;
        private string _TenLopSV;
        private string _OidChiTietHoatDong_String;
        #endregion

        #region Kết quả
        private decimal _DonGiaThanhToanMoi;
        private decimal _SoLuotDiLai; 
        private decimal _TongGio;
        private decimal _DonGiaDiLai;
        private decimal _DonGiaThanhToan;
        private decimal _SoTienThanhToan;
        #endregion



         #region Hoạt động
        [ModelDefault("Caption", "Bậc đào tạo")]
        [Browsable(false)]
        public string Oid
        {
            get { return _Oid; }
            set { SetPropertyValue("_Oid", ref _Oid, value); }
        }


        [ModelDefault("Caption", "Bậc đào tạo")]
        [ModelDefault("AllowEdit","false")]
        public string BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Hệ đào tạo")]
        [ModelDefault("AllowEdit", "false")]
        public string HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }
        [ModelDefault("Caption", "Nguồn")]
        [ModelDefault("AllowEdit", "false")]
        public LoaiHoatDongEnum LoaiHoatDong
        {
            get { return _LoaiHoatDong; }
            set { SetPropertyValue("LoaiHoatDong", ref _LoaiHoatDong, value); }
        }
        [ModelDefault("Caption", "Nội dung hoạt động")]
        [ModelDefault("AllowEdit", "false")]
        public string NoiDungHoatDong
        {
            get { return _NoiDungHoatDong; }
            set { SetPropertyValue("NoiDungHoatDong", ref _NoiDungHoatDong, value); }
        }

        [ModelDefault("Caption", "Tên lớp SV")]
        [Browsable(false)]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }


        [Browsable(false)]
        [Size(-1)]
        [ModelDefault("Caption", "Oid Chi tiết hoạt động_String")]
        public string OidChiTietHoatDong_String
        {
            get
            {
                return _OidChiTietHoatDong_String;
            }
            set
            {
                SetPropertyValue("OidChiTietHoatDong_String", ref _OidChiTietHoatDong_String, value);
            }
        }
        #endregion

        #region Kết quả      
        [ModelDefault("Caption", "Số lượt đi lại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal SoLuotDiLai
        {
            get { return _SoLuotDiLai; }
            set { SetPropertyValue("SoLuotDiLai", ref _SoLuotDiLai, value); }
        }
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        [ModelDefault("Caption", "Đơn giá đi lại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal DonGiaDiLai
        {
            get { return _DonGiaDiLai; }
            set { SetPropertyValue("DonGiaDiLai", ref _DonGiaDiLai, value); }
        }

        [ModelDefault("Caption", "Đơn giá thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal DonGiaThanhToan
        {
            get { return _DonGiaThanhToan; }
            set { SetPropertyValue("DonGiaThanhToan", ref _DonGiaThanhToan, value); }
        }
        [ModelDefault("Caption", "Số tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("AllowEdit", "false")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }


        [ModelDefault("Caption", "Đơn giá thanh toán mới ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("KiemTraDonGiaThanhToanMoi", DefaultContexts.Save, 0, 10000000000)]
        public decimal DonGiaThanhToanMoi
        {
            get { return _DonGiaThanhToanMoi; }
            set { SetPropertyValue("SoTienThanhToan", ref _DonGiaThanhToanMoi, value); }
        }

        #endregion



        public DanhSachChiTietThongTinBangChotNhanVien(Session session)
            : base(session)
        {
        }


    }
}