using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;
//
namespace ERP.Module.NghiepVu.TienLuong.Luong
{
    [DefaultProperty("DienGiai")]
    [ImageName("BO_ChiTietLuong")]
    [ModelDefault("Caption", "Chi tiết phân tích lương nhân viên")]
    [ModelDefault("AllowEdit", "False")]
    public class ChiTietPhanTichLuongNhanVien : BaseObject
    {
        private PhanTichLuongNhanVien _PhanTichLuongNhanVien;
        private string _DienGiai;
        private string _CostCenter1;
        private string _CostCenter2;        
        private decimal _SoTien;        
        private CongTruEnum _CongTru;     

        [Browsable(false)]
        [ModelDefault("Caption", "Phân tích Lương nhân viên")]
        [Association("PhanTichLuongNhanVien-ListChiTietPhanTichLuongNhanVien")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public PhanTichLuongNhanVien PhanTichLuongNhanVien
        {
            get
            {
                return _PhanTichLuongNhanVien;
            }
            set
            {
                SetPropertyValue("PhanTichLuongNhanVien", ref _PhanTichLuongNhanVien, value);
            }
        }       

        [ModelDefault("Caption", "Mã nhóm phân bổ")]        
        public string CostCenter1
        {
            get
            {
                return _CostCenter1;
            }
            set
            {
                SetPropertyValue("CostCenter1", ref _CostCenter1, value);
            }
        }

        [ModelDefault("Caption", "Mã chi phí tiền lương")]
        public string CostCenter2
        {
            get
            {
                return _CostCenter2;
            }
            set
            {
                SetPropertyValue("CostCenter2", ref _CostCenter2, value);
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
        
        public ChiTietPhanTichLuongNhanVien(Session session) : base(session) { }
    }

}
