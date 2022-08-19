using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [ImageName("BO_QuanLyChamCong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết bảng chấm công")]
    //[Appearance("ChiTietChamCongNhanVien", TargetItems = "*", Enabled = false, Criteria = "QuanLyChamCongNhanVien is not null and QuanLyChamCongNhanVien.KyTinhLuong is not null and QuanLyChamCongNhanVien.KyTinhLuong.KhoaSo")]
    public class CC_ChiTietChamCong : BaseObject,IBoPhan
    {
        // 
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private CC_QuanLyChamCong _QuanLyChamCong;
        //
        private decimal _NgayHuongLuong;
        private decimal _NgayHe;
        private decimal _NgayHuongBHXH;
        private decimal _NgayKhongLuong;
        private decimal _NgayPhep;
        private decimal _NgayPhepBu;
        private decimal _TongCong;
        //
        private decimal _TongCongCaNgay;
        private decimal _TongCongTruocDieuChinh;
        private decimal _TongCongSauDieuChinh;
        private decimal _CongChuanTheoLoaiGioLamViec;
        //
        private string _DienGiai;
        //
        private bool _IsWeb = false;
        
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý chấm công")]
        [Association("QuanLyChamCong-ListChiTietChamCong")]
        public CC_QuanLyChamCong QuanLyChamCong
        {
            get
            {
                return _QuanLyChamCong;
            }
            set
            {
                SetPropertyValue("QuanLyChamCong", ref _QuanLyChamCong, value);
                if (!IsLoading)
                {
                    UpdateBoPhanList();
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [DataSourceProperty("BoPhanList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                    UpdateNVList();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading)
                {
                    if (value != null && value.BoPhan != BoPhan)
                        BoPhan = value.BoPhan;
                }
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng BHXH")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayHuongBHXH
        {
            get
            {
                return _NgayHuongBHXH;
            }
            set
            {
                SetPropertyValue("NgayHuongBHXH", ref _NgayHuongBHXH, value);
            }
        }

        [ModelDefault("Caption", "Ngày phép")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayPhep
        {
            get
            {
                return _NgayPhep;
            }
            set
            {
                SetPropertyValue("NgayPhep", ref _NgayPhep, value);
            }
        }

        [ModelDefault("Caption", "Ngày phép bù")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayPhepBu
        {
            get
            {
                return _NgayPhepBu;
            }
            set
            {
                SetPropertyValue("NgayPhepBu", ref _NgayPhepBu, value);
            }
        }

        [ModelDefault("Caption", "Ngày không lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayKhongLuong
        {
            get
            {
                return _NgayKhongLuong;
            }
            set
            {
                SetPropertyValue("NgayKhongLuong", ref _NgayKhongLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày hè")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayHe
        {
            get
            {
                return _NgayHe;
            }
            set
            {
                SetPropertyValue("NgayHe", ref _NgayHe, value);
            }
        }     

        [ModelDefault("Caption", "Tổng công")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongCong
        {
            get
            {
                return _TongCong;
            }
            set
            {
                SetPropertyValue("TongCong", ref _TongCong, value);
            }
        }

        [ModelDefault("Caption", "Tổng công cả ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongCongCaNgay
        {
            get
            {
                return _TongCongCaNgay;
            }
            set
            {
                SetPropertyValue("TongCongCaNgay", ref _TongCongCaNgay, value);
            }
        }

        [ModelDefault("Caption", "Tổng công trước điều chỉnh")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongCongTruocDieuChinh
        {
            get
            {
                return _TongCongTruocDieuChinh;
            }
            set
            {
                SetPropertyValue("TongCongTruocDieuChinh", ref _TongCongTruocDieuChinh, value);
            }
        }

        [ModelDefault("Caption", "Tổng công sau điều chỉnh")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongCongSauDieuChinh
        {
            get
            {
                return _TongCongSauDieuChinh;
            }
            set
            {
                SetPropertyValue("TongCongSauDieuChinh", ref _TongCongSauDieuChinh, value);
            }
        }

        [ModelDefault("Caption", "Công chuẩn theo Loại giờ làm việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal CongChuanTheoLoaiGioLamViec
        {
            get
            {
                return _CongChuanTheoLoaiGioLamViec;
            }
            set
            {
                SetPropertyValue("CongChuanTheoLoaiGioLamViec", ref _CongChuanTheoLoaiGioLamViec, value);
            }
        }

        [Size(2000)]
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

        [Browsable(false)]
        [ModelDefault("Caption", "Dữ liệu Web")]
        public bool IsWeb
        {
            get
            {
                return _IsWeb;
            }
            set
            {
                SetPropertyValue("IsWeb", ref _IsWeb, value);
            }
        }

        public CC_ChiTietChamCong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            //UpdateNVList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        private void UpdateNVList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
            {
                //NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
                NVList.Criteria = CriteriaOperator.Parse("CongTy = ?", QuanLyChamCong.CongTy.Oid);
            }
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        private void UpdateBoPhanList()
        {
            //
            if (BoPhanList == null)
                BoPhanList = new XPCollection<BoPhan>(Session);
            //
            BoPhanList.Criteria = Common.Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(QuanLyChamCong.CongTy);
        }
    }

}
