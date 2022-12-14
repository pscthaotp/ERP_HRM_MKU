using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Extends;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NghiepVu.NhanSu.HoSoLuong
{
    [ImageName("BO_Expression")]
    [DefaultProperty("DienGiai")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Chi tiết công thức tính lương")]
    [Appearance("ChiTietCongThucTinhLuong", TargetItems = "CongThucTinhTNCT", Visibility = ViewItemVisibility.Hide, Criteria = "TinhTNCT")]
    public class ChiTietCongThucTinhLuong : BaseObject
    {
        private bool _NgungSuDung;
        private CongThucTinhLuong _CongThucTinhLuong;
        private CongTruEnum _CongTru;
        private string _MaChiTiet;
        private ChiPhiTienLuong _ChiPhiTienLuong;
        private string _CongThucTinhSoTien;
        private string _CongThucTinhBangChu;
        private bool _TinhTNCT;
        private string _CongThucTinhTNCT;
        private string _DienGiai;
        private bool _TruNgayCong;
        //
        private bool _TinhTheoCongThucTe;
        private LoaiNgayCongTinhLuongEnum _LoaiNgayCongTinhLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính lương")]
        [Association("CongThucTinhLuong-ListChiTietCongThucTinhLuong")]
        public CongThucTinhLuong CongThucTinhLuong
        {
            get
            {
                return _CongThucTinhLuong;
            }
            set
            {
                SetPropertyValue("CongThucTinhLuong", ref _CongThucTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Mã chi tiết (không được sửa)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
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

        [ModelDefault("Caption", "Chi phí tiền lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChiPhiTienLuong ChiPhiTienLuong
        {
            get
            {
                return _ChiPhiTienLuong;
            }
            set
            {
                SetPropertyValue("ChiPhiTienLuong", ref _ChiPhiTienLuong, value);
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
                if (!IsLoading && !string.IsNullOrEmpty(value))
                {
                    //
                    GetMaChiTiet();
                }
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

        private string ExpressionType
        {
            get
            {
                return "ERP.Module.NghiepVu.NhanSu.HoSoLuong.ChonGiaTriLapCongThuc";
            }
        }

        [Size(2000)]
        [ModelDefault("Caption", "Công thức tính số tiền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.NhanSu.HoSoLuong.btnEdit_CongThucLuong")]
        public string CongThucTinhSoTien
        {
            get
            {
                return _CongThucTinhSoTien;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTien", ref _CongThucTinhSoTien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại ngày công tính lương")]
        public LoaiNgayCongTinhLuongEnum LoaiNgayCongTinhLuong
        {
            get
            {
                return _LoaiNgayCongTinhLuong;
            }
            set
            {
                SetPropertyValue("LoaiNgayCongTinhLuong", ref _LoaiNgayCongTinhLuong, value);
                if (!IsLoading)
                {
                    if (value == LoaiNgayCongTinhLuongEnum.KhongTruCong)
                    {
                        TruNgayCong = false;
                        TinhTheoCongThucTe = false;
                    }
                    else if (value == LoaiNgayCongTinhLuongEnum.TruNgayCongHuongLuong)
                    {
                        TruNgayCong = true;
                        TinhTheoCongThucTe = false;
                    }
                    else if (value == LoaiNgayCongTinhLuongEnum.TruNgayCongThucTe)
                    {
                        TruNgayCong = false;
                        TinhTheoCongThucTe = true;
                    }
                    else if (value == LoaiNgayCongTinhLuongEnum.TruNgayCongThucTeNguyenNgay)
                    {
                        TruNgayCong = false;
                        TinhTheoCongThucTe = true;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trừ ngày công hưởng lương")]
        public bool TruNgayCong
        {
            get
            {
                return _TruNgayCong;
            }
            set
            {
                SetPropertyValue("TruNgayCong", ref _TruNgayCong, value);
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

        [Size(1000)]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Trừ ngày công thực tế")]
        public bool TinhTheoCongThucTe
        {
            get
            {
                return _TinhTheoCongThucTe;
            }
            set
            {
                SetPropertyValue("TinhTheoCongThucTe", ref _TinhTheoCongThucTe, value);
            }
        }

        public ChiTietCongThucTinhLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TinhTNCT = true;
            CongTru = CongTruEnum.Cong;
            CongThucTinhBangChu = string.Empty;
        }

        private void GetMaChiTiet()
        {
           // this.MaChiTiet = StringHelpers.ReplaceVietnameseChar(StringHelpers.ToTitleCase(this.DienGiai)).Replace(" ", String.Empty);
        }
    }

}
