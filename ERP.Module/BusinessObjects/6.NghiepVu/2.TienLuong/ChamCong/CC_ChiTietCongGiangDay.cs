using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [ImageName("BO_QuanLyCongNgoaiGio")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết bảng công giảng dạy")]
    [Appearance("CC_ChiTietCongGiangDay.Khoa", TargetItems = "*", Enabled = false, Criteria = "CC_QuanLyCongGiangDay.KhoaSo")]

    public class CC_ChiTietCongGiangDay : BaseObject,IBoPhan
    {
        // 
        private CC_QuanLyCongGiangDay _QuanLyCongGiangDay;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private NhomPhanBo _NhomPhanBo; 
        private decimal _TongCong;
        private decimal _CongVuot;
        private decimal _CongLuyenThi;
        private decimal _CongChuan;
        private string _DienGiai;

        private decimal _HeSo;
        private decimal _DonGia;

        //
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý công giảng dạy")]
        [Association("QuanLyCongKhac-ListChiTietCongKhac")]
        [ImmediatePostData]
        public CC_QuanLyCongGiangDay QuanLyCongGiangDay
        {
            get
            {
                return _QuanLyCongGiangDay;
            }
            set
            {
                SetPropertyValue("QuanLyCongGiangDay", ref _QuanLyCongGiangDay, value);
                if (!IsLoading && value != null)
                {
                    UpdateBoPhanList();
                    UpdateNVList();
                }
            }
        }

        [Browsable(false)]
        //[ImmediatePostData]
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

        [ModelDefault("Caption", "Nhóm phân bổ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhomPhanBo NhomPhanBo
        {
            get
            {
                return _NhomPhanBo;
            }
            set
            {
                SetPropertyValue("NhomPhanBo", ref _NhomPhanBo, value);
            }
        }

        [ModelDefault("Caption", "Tổng công (tiết/số sv)")]
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

        [ModelDefault("Caption", "Công vượt (tiết/số sv)")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal CongVuot
        {
            get
            {
                return _CongVuot;
            }
            set
            {
                SetPropertyValue("CongVuot", ref _CongVuot, value);
            }
        }

        [ModelDefault("Caption", "Công luyện thi (tiết/số sv)")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal CongLuyenThi
        {
            get
            {
                return _CongLuyenThi;
            }
            set
            {
                SetPropertyValue("CongLuyenThi", ref _CongLuyenThi, value);
            }
        }

        [ModelDefault("Caption", "Công chuẩn (tiết/số sv)")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
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

        [Browsable(false)]
        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal HeSo
        {
            get
            {
                return _HeSo;
            }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGia
        {
            get
            {
                return _DonGia;
            }
            set
            {
                SetPropertyValue("DonGia", ref _DonGia, value);
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

        public CC_ChiTietCongGiangDay(Session session) : base(session) { }

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
                NVList.Criteria = CriteriaOperator.Parse("CongTy = ?", QuanLyCongGiangDay.CongTy.Oid);
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
            if (QuanLyCongGiangDay.CongTy != null)
                BoPhanList.Criteria = Common.Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(QuanLyCongGiangDay.CongTy);
        }
    }
}
