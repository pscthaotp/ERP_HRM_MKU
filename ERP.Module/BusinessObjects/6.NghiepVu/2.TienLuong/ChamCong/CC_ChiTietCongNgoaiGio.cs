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
    [ImageName("BO_QuanLyCongNgoaiGio")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết công ngoài giờ")]
    //[Appearance("ChiTietChamCongNhanVien", TargetItems = "*", Enabled = false, Criteria = "QuanLyCongNgoaiGioNhanVien is not null and QuanLyCongNgoaiGioNhanVien.KyTinhLuong is not null and QuanLyCongNgoaiGioNhanVien.KyTinhLuong.KhoaSo")]
    public class CC_ChiTietCongNgoaiGio : BaseObject,IBoPhan
    {
        // 
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private CC_QuanLyCongNgoaiGio _QuanLyCongNgoaiGio;
        private decimal _TongSoGio_CT; //Số giờ chịu thuế
        private decimal _TongSoGio_KCT;//Số giờ không chịu thuế
        private decimal _NgayPhepBuTrongThang;
        private decimal _TongNgayPhepBu;
        private decimal _SoNgayBuConLai;
        private string _DienGiai;
        private decimal _CongChuanTheoLoaiGioLamViec;

        //
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý chấm công")]
        [Association("QuanLyCongNgoaiGio-ListChiTietChamCong")]
        public CC_QuanLyCongNgoaiGio QuanLyCongNgoaiGio
        {
            get
            {
                return _QuanLyCongNgoaiGio;
            }
            set
            {
                SetPropertyValue("QuanLyCongNgoaiGio", ref _QuanLyCongNgoaiGio, value);
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
                {
                    UpdateNVList();
                }
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

        [ModelDefault("Caption", "Tổng số giờ - KCT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoGio_KCT
        {
            get
            {
                return _TongSoGio_KCT;
            }
            set
            {
                SetPropertyValue("TongSoGio_KCT", ref _TongSoGio_KCT, value);
            }
        }

        [ModelDefault("Caption", "Tổng số giờ - CT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoGio_CT
        {
            get
            {
                return _TongSoGio_CT;
            }
            set
            {
                SetPropertyValue("TongSoGio_CT", ref _TongSoGio_CT, value);
            }
        }

        [ModelDefault("Caption", "Ngày phép bù trong tháng")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayPhepBuTrongThang
        {
            get
            {
                return _NgayPhepBuTrongThang;
            }
            set
            {
                SetPropertyValue("NgayPhepBuTrongThang", ref _NgayPhepBuTrongThang, value);
            }
        }

        [ModelDefault("Caption", "Tổng ngày phép bù")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongNgayPhepBu
        {
            get
            {
                return _TongNgayPhepBu;
            }
            set
            {
                SetPropertyValue("TongNgayPhepBu", ref _TongNgayPhepBu, value);
            }
        }

        [ModelDefault("Caption", "Số ngày bù còn lại")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayBuConLai
        {
            get
            {
                return _SoNgayBuConLai;
            }
            set
            {
                SetPropertyValue("SoNgayBuConLai", ref _SoNgayBuConLai, value);
            }
        }

        [Size(1000)]
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


        public CC_ChiTietCongNgoaiGio(Session session) : base(session) { }

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
            //UpdateBoPhanList();
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
                NVList.Criteria = CriteriaOperator.Parse("CongTy = ?", QuanLyCongNgoaiGio.CongTy.Oid);
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
            BoPhanList.Criteria = Common.Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(QuanLyCongNgoaiGio.CongTy);
        }
    }
}
