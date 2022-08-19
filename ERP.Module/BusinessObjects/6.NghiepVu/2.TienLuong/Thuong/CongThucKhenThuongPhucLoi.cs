using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.DieuKien;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NghiepVu.TienLuong.Thuong
{
    [DefaultClassOptions]
    [DefaultProperty("DienGiai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Công thức tính thưởng")]
    [Appearance("CongThucKhenThuongPhucLoi.TinhThueTNCN", TargetItems = "CongThucTinhTNCT", Enabled = false, Criteria = "TinhTNCT")]
    [ImageName("BO_Expression")]

    public class CongThucKhenThuongPhucLoi : BaseObject
    {
        private LoaiKhenThuongPhucLoi _LoaiKhenThuongPhucLoi;
        private string _DienGiai;
        private string _DieuKienNhanVien;
        private string _CongThucTinhSoTienNhan;
        private bool _TinhTNCT;
        private string _CongThucTinhTNCT;
        private string _CongThucTinhBangChu;

        [ModelDefault("Caption", "Loại thưởng, phụ cấp")]
        [Association("LoaiKhenThuongPhucLoi-CongThucKhenThuongPhucLoi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiKhenThuongPhucLoi LoaiKhenThuongPhucLoi
        {
            get
            {
                return _LoaiKhenThuongPhucLoi;
            }
            set
            {
                SetPropertyValue("LoaiKhenThuongPhucLoi", ref _LoaiKhenThuongPhucLoi, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        private string ExpressionType
        {
            get
            {
                return "ERP.Module.NghiepVu.NhanSu.HoSoLuong.ChonGiaTriLapCongThuc";
            }
        }

        [Size(-1)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Công thức tính số tiền nhận")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.NhanSu.HoSoLuong.btnEdit_CongThucLuong")]
        public string CongThucTinhSoTienNhan
        {
            get
            {
                return _CongThucTinhSoTienNhan;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTienNhan", ref _CongThucTinhSoTienNhan, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính bằng chữ")]
        public string CongThucTinhBangChu
        {
            get
            {
                return _CongThucTinhBangChu;
            }
            set
            {
                SetPropertyValue("CongThucTinhBangChu", ref _CongThucTinhBangChu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tính TNCT mặc định")]
        public bool TinhTNCT
        {
            get
            {
                return _TinhTNCT;
            }
            set
            {
                SetPropertyValue("TinhTNCT", ref _TinhTNCT, value);
            }
        }

        [Size(1000)]
        [ModelDefault("Caption", "Công thức tính TNCT khác")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TinhTNCT")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.NhanSu.HoSoLuong.btnEdit_CongThucLuong")]
        public string CongThucTinhTNCT
        {
            get
            {
                return _CongThucTinhTNCT;
            }
            set
            {
                SetPropertyValue("CongThucTinhTNCT", ref _CongThucTinhTNCT, value);
            }
        }

        private Type ObjectType
        {
            get
            {
                return typeof(DieuKienTongHop);
            }
        }

        [Size(-1)]
        [CriteriaOptions("ObjectType")]
        [ModelDefault("Caption", "Điều kiện tính lương")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ExtendedCriteriaPropertyEditor")]
        public string DieuKienNhanVien
        {
            get
            {
                return _DieuKienNhanVien;
            }
            set
            {
                SetPropertyValue("DieuKienNhanVien", ref _DieuKienNhanVien, value);
            }
        }


        public CongThucKhenThuongPhucLoi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //
            CongThucTinhBangChu = "";
        }
    }

}
