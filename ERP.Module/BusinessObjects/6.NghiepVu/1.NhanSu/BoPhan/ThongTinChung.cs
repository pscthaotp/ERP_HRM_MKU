using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.NghiepVu.NhanSu.BoPhans
{
    [DefaultProperty("Caption")]
    [ImageName("BO_Money_Calculator")]
    [ModelDefault("Caption", "Thông tin chung")]
    public class ThongTinChung : BaseObject
    {
        //Mức đóng tối đa cho NLĐ
        private decimal _MucLuongCoBan;
        private decimal _MucLuongToiThieuVung;
        private decimal _MucDongBHXHToiDa;
        private decimal _MucDongBHYTToiDa;
        private decimal _MucDongBHTNToiDa;
        private decimal _MucDongCDToiDa;

        //Bảo hiểm người lao động
        private decimal _PTBHXH;
        private decimal _PTBHYT;
        private decimal _PTBHTN;
        private decimal _PTCD;

        //Bảo hiểm người sử lao động
        private decimal _PTBHXHCTY;
        private decimal _PTBHYTCTY;
        private decimal _PTBHTNCTY;
        private decimal _PTCDCTY;

        //Dùng tính thuế TNCN
        private decimal _GiamTruBanThan;
        private decimal _GiamTruNguoiPhuThuoc;
   
        //Ngoài giờ
        private decimal _DonGiaNgoaiGio;
        private decimal _DonGiaThuBay;
        private decimal _DonGiaNgonNgu;
        private decimal _DonGiaTiengAnh;
        private decimal _DonGiaGoKid;

        //Phân tích lương
        private decimal _CongChuan;

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("BHXH {PTBHXH:N1}, BHYT {PTBHYT:N1}, BHTN {PTBHTN:N1}", this);
            }
        }

        [ModelDefault("Caption", "Mức lương cơ bản")]
        [ModelDefault("EditMask", "n0")]
        [ModelDefault("DisplayFormat", "n0")]       
        public decimal MucLuongCoBan
        {
            get
            {
                return _MucLuongCoBan;
            }
            set
            {
                SetPropertyValue("MucLuongCoBan", ref _MucLuongCoBan, value);
                if (!IsLoading && value > 0)
                {
                    MucDongBHXHToiDa = MucDongBHYTToiDa = 20 * value;
                }
            }
        }

        [ModelDefault("Caption", "Mức lương tối thiểu vùng")]
        [ModelDefault("EditMask", "n0")]
        [ModelDefault("DisplayFormat", "n0")]
        public decimal MucLuongToiThieuVung
        {
            get
            {
                return _MucLuongToiThieuVung;
            }
            set
            {
                SetPropertyValue("MucLuongToiThieuVung", ref _MucLuongToiThieuVung, value);
                if (!IsLoading && value > 0)
                    MucDongBHTNToiDa = 20 * value;           
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức đóng BHXH tối đa")]
        [ModelDefault("EditMask", "n0")]
        [ModelDefault("DisplayFormat", "n0")]        
        public decimal MucDongBHXHToiDa
        {
            get
            {
                return _MucDongBHXHToiDa;
            }
            set
            {
                SetPropertyValue("MucDongBHXHToiDa", ref _MucDongBHXHToiDa, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức đóng BHYT tối đa")]
        [ModelDefault("EditMask", "n0")]
        [ModelDefault("DisplayFormat", "n0")]       
        public decimal MucDongBHYTToiDa
        {
            get
            {
                return _MucDongBHYTToiDa;
            }
            set
            {
                SetPropertyValue("MucDongBHYTToiDa", ref _MucDongBHYTToiDa, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức đóng BHTN tối đa")]
        [ModelDefault("EditMask", "n0")]
        [ModelDefault("DisplayFormat", "n0")]
        public decimal MucDongBHTNToiDa
        {
            get
            {
                return _MucDongBHTNToiDa;
            }
            set
            {
                SetPropertyValue("MucDongBHTNToiDa", ref _MucDongBHTNToiDa, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức đóng CĐ tối đa")]
        [ModelDefault("EditMask", "n0")]
        [ModelDefault("DisplayFormat", "n0")]
        public decimal MucDongCDToiDa
        {
            get
            {
                return _MucDongCDToiDa;
            }
            set
            {
                SetPropertyValue("MucDongCDToiDa", ref _MucDongCDToiDa, value);
            }
        }

        [ModelDefault("Caption", "% BHXH NLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHXH
        {
            get
            {
                return _PTBHXH;
            }
            set
            {
                SetPropertyValue("PTBHXH", ref _PTBHXH, value);
            }
        }

        [ModelDefault("Caption", "% BHYT NLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHYT
        {
            get
            {
                return _PTBHYT;
            }
            set
            {
                SetPropertyValue("PTBHYT", ref _PTBHYT, value);
            }
        }

        [ModelDefault("Caption", "% BHTN NLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHTN
        {
            get
            {
                return _PTBHTN;
            }
            set
            {
                SetPropertyValue("PTBHTN", ref _PTBHTN, value);
            }
        }

        [ModelDefault("Caption", "% CĐ NLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTCD
        {
            get
            {
                return _PTCD;
            }
            set
            {
                SetPropertyValue("PTCD", ref _PTCD, value);
            }
        }

        [ModelDefault("Caption", "% BHXH NSDLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHXHCTY
        {
            get
            {
                return _PTBHXHCTY;
            }
            set
            {
                SetPropertyValue("PTBHXHCTY", ref _PTBHXHCTY, value);
            }
        }

        [ModelDefault("Caption", "% BHYT NSDLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHYTCTY
        {
            get
            {
                return _PTBHYTCTY;
            }
            set
            {
                SetPropertyValue("PTBHYTCTY", ref _PTBHYTCTY, value);
            }
        }

        [ModelDefault("Caption", "% BHTN NSDLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHTNCTY
        {
            get
            {
                return _PTBHTNCTY;
            }
            set
            {
                SetPropertyValue("PTBHTNCTY", ref _PTBHTNCTY, value);
            }
        }

        [ModelDefault("Caption", "% CĐ NSDLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTCDCTY
        {
            get
            {
                return _PTCDCTY;
            }
            set
            {
                SetPropertyValue("PTCDCTY", ref _PTCDCTY, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ bản thân")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal GiamTruBanThan
        {
            get
            {
                return _GiamTruBanThan;
            }
            set
            {
                SetPropertyValue("GiamTruBanThan", ref _GiamTruBanThan, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ người phụ thuộc")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal GiamTruNguoiPhuThuoc
        {
            get
            {
                return _GiamTruNguoiPhuThuoc;
            }
            set
            {
                SetPropertyValue("GiamTruNguoiPhuThuoc", ref _GiamTruNguoiPhuThuoc, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá ngoài giờ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaNgoaiGio
        {
            get
            {
                return _DonGiaNgoaiGio;
            }
            set
            {
                SetPropertyValue("DonGiaNgoaiGio", ref _DonGiaNgoaiGio, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá thứ bảy")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaThuBay
        {
            get
            {
                return _DonGiaThuBay;
            }
            set
            {
                SetPropertyValue("_DonGiaThuBay", ref _DonGiaThuBay, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá ngôn ngữ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaNgonNgu
        {
            get
            {
                return _DonGiaNgonNgu;
            }
            set
            {
                SetPropertyValue("DonGiaNgonNgu", ref _DonGiaNgonNgu, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá tiếng anh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaTiengAnh
        {
            get
            {
                return _DonGiaTiengAnh;
            }
            set
            {
                SetPropertyValue("DonGiaTiengAnh", ref _DonGiaTiengAnh, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá Gokid")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaGoKid
        {
            get
            {
                return _DonGiaGoKid;
            }
            set
            {
                SetPropertyValue("DonGiaGoKid", ref _DonGiaGoKid, value);
            }
        }

        [ModelDefault("Caption", "Công chuẩn")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal CongChuan
        {
            get
            {
                return _CongChuan;
            }
            set
            {
                SetPropertyValue("CongChuan", ref _CongChuan, value);
            }
        }

        public ThongTinChung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
        }
    }
}
