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
    [ModelDefault("Caption", "Chi tiết bảng công khác")]
    public class CC_ChiTietCongKhac : BaseObject,IBoPhan
    {
        // 
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private CC_QuanLyCongKhac _QuanLyCongKhac;
        private decimal _TongCong;
        private string _DienGiai;
        private bool _DayChinh = true;

        //
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý công khác")]
        [Association("QuanLyCongKhac-ListChiTietCongKhac")]
        [ImmediatePostData]
        public CC_QuanLyCongKhac QuanLyCongKhac
        {
            get
            {
                return _QuanLyCongKhac;
            }
            set
            {
                SetPropertyValue("QuanLyCongKhac", ref _QuanLyCongKhac, value);
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

        [ModelDefault("Caption", "Tổng công")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [Appearance("GiangDayChinh", TargetItems = "DayChinh", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyCongKhac != null && QuanLyCongKhac.LoaiCongKhac!=3")]
        [ModelDefault("Caption", "Dạy chính")]
        public bool DayChinh
        {
            get
            {
                return _DayChinh;
            }
            set
            {
                SetPropertyValue("DayChinh", ref _DayChinh, value);
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

        public CC_ChiTietCongKhac(Session session) : base(session) { }

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
                NVList.Criteria = CriteriaOperator.Parse("CongTy = ?", QuanLyCongKhac.CongTy.Oid);
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
            BoPhanList.Criteria = Common.Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(QuanLyCongKhac.CongTy);
        }
    }
}
