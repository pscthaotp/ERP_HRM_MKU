using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [DefaultProperty("TenNgayNgoaiGio")]
    [ModelDefault("Caption", "Loại ngày ngoài giờ")]
    public class LoaiNgayNgoaiGio : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNgayNgoaiGio;
        private decimal _HeSo_CT;
        private decimal _HeSo_KCT;
        private decimal _TongHeSo;
        private decimal _TuGio = 0;
        private decimal _DenGio = 0;
        private decimal _TuPhut = 0;
        private decimal _DenPhut = 0;
        private LoaiNgayNghiEnum _LoaiNgay = LoaiNgayNghiEnum.NghiLe;

        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên ngày ngoài giờ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TenNgayNgoaiGio
        {
            get
            {
                return _TenNgayNgoaiGio;
            }
            set
            {
                SetPropertyValue("TenNgayNgoaiGio", ref _TenNgayNgoaiGio, value);
            }
        }

        [ModelDefault("Caption", "Hệ số - CT")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal HeSo_CT
        {
            get
            {
                return _HeSo_CT;
            }
            set
            {
                SetPropertyValue("HeSo_CT", ref _HeSo_CT, value);
                if (!IsLoading)
                {
                    TongHeSo = HeSo_CT + HeSo_KCT;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số - KCT")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal HeSo_KCT
        {
            get
            {
                return _HeSo_KCT;
            }
            set
            {
                SetPropertyValue("HeSo_KCT", ref _HeSo_KCT, value);
                if (!IsLoading)
                {
                    TongHeSo = HeSo_CT + HeSo_KCT;
                }
            }
        }

        [ModelDefault("Caption", "Tổng hệ số")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongHeSo
        {
            get
            {
                return _TongHeSo;
            }
            set
            {
                SetPropertyValue("TongHeSo", ref _TongHeSo, value);
            }
        }

        [ModelDefault("Caption", "Từ giờ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TuGio
        {
            get
            {
                return _TuGio;
            }
            set
            {
                SetPropertyValue("TuGio", ref _TuGio, value);
            }
        }

        [ModelDefault("Caption", "Đến giờ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DenGio
        {
            get
            {
                return _DenGio;
            }
            set
            {
                SetPropertyValue("DenGio", ref _DenGio, value);
            }
        }

        [ModelDefault("Caption", "Từ phút")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TuPhut
        {
            get
            {
                return _TuPhut;
            }
            set
            {
                SetPropertyValue("TuPhut", ref _TuPhut, value);
            }
        }

        [ModelDefault("Caption", "Đến phút")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DenPhut
        {
            get
            {
                return _DenPhut;
            }
            set
            {
                SetPropertyValue("DenPhut", ref _DenPhut, value);
            }
        }

        [ModelDefault("Caption", "Loại ngày làm")]       
        public LoaiNgayNghiEnum LoaiNgay
        {
            get
            {
                return _LoaiNgay;
            }
            set
            {
                SetPropertyValue("LoaiNgay", ref _LoaiNgay, value);
            }
        }
        public LoaiNgayNgoaiGio(Session session) : base(session) { }
    }

}
