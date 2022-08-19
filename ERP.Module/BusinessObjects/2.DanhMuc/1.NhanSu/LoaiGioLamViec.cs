using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_DanToc")]
    [DefaultProperty("Cation")]
    [ModelDefault("Caption", "Loại giờ làm việc")]
    public class LoaiGioLamViec : BaseObject
    {
        private string _TenLoaiGio;
        private decimal _SoGio;
        private CC_CaChamCong _CaChamCong_T2;
        private CC_CaChamCong _CaChamCong_T3;
        private CC_CaChamCong _CaChamCong_T4;
        private CC_CaChamCong _CaChamCong_T5;
        private CC_CaChamCong _CaChamCong_T6;
        private CC_CaChamCong _CaChamCong_T7;
        private CC_CaChamCong _CaChamCong_CN;
        private decimal _SoGioNghiTruaNgoaiGio;

        //
        [NonPersistent]
        [ModelDefault("Caption", "Tên")]
        public string Cation
        {
            get
            {
                return ObjectFormatter.Format("{TenLoaiGio} - {SoGio:N2}", this);
            }
        }

        [ModelDefault("Caption", "Tên loại giờ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiGio
        {
            get
            {
                return _TenLoaiGio;
            }
            set
            {
                SetPropertyValue("TenLoaiGio", ref _TenLoaiGio, value);
            }
        }

        [ModelDefault("Caption", "Số giờ BQ / Ngày")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoGio
        {
            get
            {
                return _SoGio;
            }
            set
            {
                SetPropertyValue("SoGio", ref _SoGio, value);
            }
        }
        
        [ModelDefault("Caption", "Thứ Hai")]
        public CC_CaChamCong CaChamCong_T2
        {
            get
            {
                return _CaChamCong_T2;
            }
            set
            {
                SetPropertyValue("CaChamCong_T2", ref _CaChamCong_T2, value);
            }
        }

        [ModelDefault("Caption", "Thứ Ba")]
        public CC_CaChamCong CaChamCong_T3
        {
            get
            {
                return _CaChamCong_T3;
            }
            set
            {
                SetPropertyValue("CaChamCong_T3", ref _CaChamCong_T3, value);
            }
        }

        [ModelDefault("Caption", "Thứ Tư")]
        public CC_CaChamCong CaChamCong_T4
        {
            get
            {
                return _CaChamCong_T4;
            }
            set
            {
                SetPropertyValue("CaChamCong_T4", ref _CaChamCong_T4, value);
            }
        }

        [ModelDefault("Caption", "Thứ Năm")]
        public CC_CaChamCong CaChamCong_T5
        {
            get
            {
                return _CaChamCong_T5;
            }
            set
            {
                SetPropertyValue("CaChamCong_T5", ref _CaChamCong_T5, value);
            }
        }

        [ModelDefault("Caption", "Thứ Sáu")]
        public CC_CaChamCong CaChamCong_T6
        {
            get
            {
                return _CaChamCong_T6;
            }
            set
            {
                SetPropertyValue("CaChamCong_T6", ref _CaChamCong_T6, value);
            }
        }

        [ModelDefault("Caption", "Thứ Bảy")]
        public CC_CaChamCong CaChamCong_T7
        {
            get
            {
                return _CaChamCong_T7;
            }
            set
            {
                SetPropertyValue("CaChamCong_T7", ref _CaChamCong_T7, value);
            }
        }

        [ModelDefault("Caption", "Chủ Nhật")]
        public CC_CaChamCong CaChamCong_CN
        {
            get
            {
                return _CaChamCong_CN;
            }
            set
            {
                SetPropertyValue("CaChamCong_CN", ref _CaChamCong_CN, value);
            }
        }

        [ModelDefault("Caption", "Số giờ nghỉ trưa ngoài giờ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal SoGioNghiTruaNgoaiGio
        {
            get
            {
                return _SoGioNghiTruaNgoaiGio;
            }
            set
            {
                SetPropertyValue("SoGioNghiTruaNgoaiGio", ref _SoGioNghiTruaNgoaiGio, value);
            }
        }

        public LoaiGioLamViec(Session session) : base(session) { }
    }
}
