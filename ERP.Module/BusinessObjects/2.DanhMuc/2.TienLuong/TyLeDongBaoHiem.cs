using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using ERP.Module.Commons;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_Purse")]
    [ModelDefault("Caption", "Tỷ lệ đóng bảo hiểm")]
    [DefaultProperty("Caption")]
    public class TyLeDongBaoHiem : BaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private decimal _LuongCoBan;
        private decimal _PTBHXH;
        private decimal _PTBHYT;
        private decimal _PTBHTN;
        private decimal _PTBHXHCTY;
        private decimal _PTBHYTCTY;
        private decimal _PTBHTNCTY;

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value.SetTime(SetTimeEnum.StartMonth));
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value.SetTime(SetTimeEnum.EndMonth));
            }
        }

        [ModelDefault("Caption", "Lương chức danh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongCoBan
        {
            get
            {
                return _LuongCoBan;
            }
            set
            {
                SetPropertyValue("LuongCoBan", ref _LuongCoBan, value);
            }
        }

        [ModelDefault("Caption", "% BHXH NLĐ đóng")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [ModelDefault("Caption", "% BHXH NSDLĐ đóng")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [Persistent]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "% Người SDLĐ đóng")]
        public decimal PhanTramNguoiSDLDDong
        {
            get
            {
                return PTBHXHCTY + PTBHYTCTY + PTBHTNCTY;
            }
        }

        [Persistent]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "% Người LĐ đóng")]
        public decimal PhanTramNguoiLDDong
        {
            get
            {
                return PTBHXH + PTBHYT + PTBHTN;
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("LgCB: {LuongCoBan:N0}; NSDLĐ: {PhanTramNguoiSDLDDong:N1}%; NLĐ: {PhanTramNguoiLDDong:N1}%", this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
            }
        }

        public TyLeDongBaoHiem(Session session) : base(session) { }
    }

}
