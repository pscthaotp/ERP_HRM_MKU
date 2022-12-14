using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.Commons;
using System.Data.SqlClient;
using System.Data;
//
namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [DefaultProperty("TenNienDo")]
    [ImageName("BO_KyTinhLuong")]
    [ModelDefault("Caption", "Niên độ tài chính")]
    [RuleCombinationOfPropertiesIsUnique("Niên độ tài chính đã tồn tại.", DefaultContexts.Save, "TuNgay;DenNgay;CongTy")]
    public class NienDoTaiChinh : BaseObject, ICongTy
    {
        private CongTy _CongTy;
        private string _TenNienDo;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private decimal _SoThang;
        private DateTime _BatDauHe;
        private DateTime _KetThucHe;
        private decimal _SoNgayNghiHe;

        [ModelDefault("Caption", "Công ty / Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [Size(200)]
        [ModelDefault("Caption", "Tên niên độ")]
        public string TenNienDo
        {
            get
            {
                return _TenNienDo;
            }
            set
            {
                SetPropertyValue("TenNienDo", ref _TenNienDo, value);
            }
        }
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading)
                    SoThang = Common.GetMonthNumber(TuNgay, DenNgay);
            }
        }
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading)
                    SoThang = Common.GetMonthNumber(TuNgay, DenNgay);
            }
        }

        [ModelDefault("Caption", "Số tháng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bắt đầu hè")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "CongTy.LoaiTruong != 0 and CongTy.LoaiTruong != 3")]
        public DateTime BatDauHe
        {
            get
            {
                return _BatDauHe;
            }
            set
            {
                SetPropertyValue("BatDauHe", ref _BatDauHe, value);
                if (!IsLoading)
                    SoNgayNghiHe = Common.GetDayNumberSubtrackWeekend_ManNonAndPhoThong(BatDauHe, KetThucHe, Session);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kết thúc hè")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "CongTy.LoaiTruong != 0 and CongTy.LoaiTruong != 3")]
        public DateTime KetThucHe
        {
            get
            {
                return _KetThucHe;
            }
            set
            {
                SetPropertyValue("KetThucHe", ref _KetThucHe, value);
                if (!IsLoading)
                    SoNgayNghiHe = Common.GetDayNumberSubtrackWeekend_ManNonAndPhoThong(BatDauHe, KetThucHe, Session);
            }
        }

        [ModelDefault("Caption", "Số ngày nghỉ hè")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoNgayNghiHe
        {
            get
            {
                return _SoNgayNghiHe;
            }
            set
            {
                SetPropertyValue("SoNgayNghiHe", ref _SoNgayNghiHe, value);
            }
        }

        public NienDoTaiChinh(Session session) :base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
        }

    }

}
