using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NghiepVu.TienLuong.TruyLuong
{
    [ModelDefault("Caption", "Chi tiết truy thu")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietTruyThu.Unique", DefaultContexts.Save, "TruyThuNhanVien;MaChiTiet")]
    public class ChiTietTruyThu : BaseObject
    {
        private TruyThuNhanVien _TruyThuNhanVien;
        private string _MaChiTiet;
        private string _DienGiai;
        private CongTruEnum _CongTru;
        private decimal _SoTien;
        
        [Browsable(false)]
        [ModelDefault("Caption", "Truy thu lương nhân viên")]
        [Association("TruyThuNhanVien-ListChiTietTruyThu")]
        public TruyThuNhanVien TruyThuNhanVien
        {
            get
            {
                return _TruyThuNhanVien;
            }
            set
            {
                SetPropertyValue("TruyThuNhanVien", ref _TruyThuNhanVien, value);
            }
        }
             
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Mã chi tiết")]
        public string MaChiTiet
        {
            get
            {
                return _MaChiTiet;
            }
            set
            {
                SetPropertyValue("MaChiTiet", ref _MaChiTiet, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        [ModelDefault("Caption", "Cộng/Trừ")]
        public CongTruEnum CongTru
        {
            get
            {
                return _CongTru;
            }
            set
            {
                SetPropertyValue("CongTru", ref _CongTru, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        public ChiTietTruyThu(Session session) : base(session) { }
    }

}
 