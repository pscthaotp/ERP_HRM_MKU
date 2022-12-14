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

namespace ERP.Module.NghiepVu.NhanSu.HoSoLuong
{
    [DefaultClassOptions]
    [DefaultProperty("DienGiai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Công thức tính lương")]
    [ImageName("BO_Expression")]
    public class CongThucTinhLuong : BaseObject, ICongTy
    {
        private LoaiCongThucLuongEnum _LoaiCongThucLuong = LoaiCongThucLuongEnum.CongThucLuongVaPhuCap;
        private bool _NgungSuDung;
        private string _DienGiai;
        private string _DieuKienNhanVien;
        private CongTy _CongTy;
        private DotTinhLuong _DotTinhLuong;

        [ModelDefault("Caption", "Loại công thức")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiCongThucLuongEnum LoaiCongThucLuong
        {
            get
            {
                return _LoaiCongThucLuong;
            }
            set
            {
                SetPropertyValue("LoaiCongThucLuong", ref _LoaiCongThucLuong, value);
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

        [ModelDefault("Caption", "Ngừng sử dụng")]
        public bool NgungSuDung
        {
            get
            {
                return _NgungSuDung;
            }
            set
            {
                SetPropertyValue("NgungSuDung", ref _NgungSuDung, value);
            }
        }
        
        private Type ObjectType
        {
            get
            {
                return typeof(DieuKienTongHop);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Đợt tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DotTinhLuong DotTinhLuong
        {
            get
            {
                return _DotTinhLuong;
            }
            set
            {
                SetPropertyValue("DotTinhLuong", ref _DotTinhLuong, value);
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

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách công thức tính lương")]
        [Association("CongThucTinhLuong-ListChiTietCongThucTinhLuong")]
        public XPCollection<ChiTietCongThucTinhLuong> ListChiTietCongThucTinhLuong
        {
            get
            {
                return GetCollection<ChiTietCongThucTinhLuong>("ListChiTietCongThucTinhLuong");
            }
        }

        public CongThucTinhLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            DotTinhLuong = Common.GetDotTinhLuong(Session,"Dot1");
        }
    }

}
