using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.Enum;
using ERP.Module.PMS.DanhMuc;


namespace ERP.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Chi tiết bảng chốt")]
    [ModelDefault("AllowNew", "false")]
    [Appearance("ToMauSoTien", TargetItems = "SoTienThanhToan", BackColor = "lightsteelblue", FontColor = "Black", FontStyle = System.Drawing.FontStyle.Bold)]
    [Appearance("ToMauTongGio", TargetItems = "TongGio",  BackColor = "lightyellow",FontColor = "Red")]
    [Appearance("ToMauDaTinhTien", TargetItems = "DonGiaThanhToan", BackColor = "palegreen", FontColor = "Black")]
    public class ChiTietBangChotThuLaoGiangDay : BaseObject
    {
        #region key
        private ThongTinBangChot _ThongTinBangChot;
        [Association("ThongTinBangChot-ListChiTietBangChot")]
        [ModelDefault("Caption", "Bảng chốt thông tin giảng dạy")]
        [Browsable(false)]
        public ThongTinBangChot ThongTinBangChot
        {
            get
            {
                return _ThongTinBangChot;
            }
            set
            {
                SetPropertyValue("ThongTinBangChot", ref _ThongTinBangChot, value);
            }
        }
        #endregion

        #region Khai báo

        #region Hoạt động
        private BacDaoTao _BacDaoTao;
        private HeDaoTao_PMS _HeDaoTao;
        private LoaiHoatDongEnum _LoaiHoatDong;
        private string _NoiDungHoatDong;
        private string _TenLopSV;
        private string _OidChiTietHoatDong_String;
        #endregion

        #region Kết quả
        private decimal _SoLuotDiLai; 
        private decimal _TongGio;
        private decimal _DonGiaDiLai;
        private decimal _DonGiaThanhToan;
        private decimal _SoTienThanhToan;
        #endregion

        private Guid _OidChiTietHoatDong;

        #endregion


        #region Hoạt động
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao_PMS HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }
        [ModelDefault("Caption", "Nguồn")]
        public LoaiHoatDongEnum LoaiHoatDong
        {
            get { return _LoaiHoatDong; }
            set { SetPropertyValue("LoaiHoatDong", ref _LoaiHoatDong, value); }
        }
        [ModelDefault("Caption", "Nội dung hoạt động")]
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
        public decimal SoLuotDiLai
        {
            get { return _SoLuotDiLai; }
            set { SetPropertyValue("SoLuotDiLai", ref _SoLuotDiLai, value); }
        }
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        [ModelDefault("Caption", "Đơn giá đi lại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGiaDiLai
        {
            get { return _DonGiaDiLai; }
            set { SetPropertyValue("DonGiaDiLai", ref _DonGiaDiLai, value); }
        }

        [ModelDefault("Caption", "Đơn giá thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGiaThanhToan
        {
            get { return _DonGiaThanhToan; }
            set { SetPropertyValue("DonGiaThanhToan", ref _DonGiaThanhToan, value); }
        }
        [ModelDefault("Caption", "Số tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }
        #endregion

        #region Guid
        [Browsable(false)]
        [ModelDefault("Caption", "Oid Chi tiết hoạt động")]
        public Guid OidChiTietHoatDong
        {
            get
            {
                return _OidChiTietHoatDong;
            }
            set
            {
                SetPropertyValue("OidChiTietHoatDong", ref _OidChiTietHoatDong, value);
            }
        }
        #endregion


        public ChiTietBangChotThuLaoGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}