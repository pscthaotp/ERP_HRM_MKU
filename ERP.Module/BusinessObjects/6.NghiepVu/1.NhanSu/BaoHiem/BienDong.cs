using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.NhanSu.BaoHiem
{
    [ImageName("BO_BienDong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Biến động")]
    [RuleCombinationOfPropertiesIsUnique("BienDong.Unique", DefaultContexts.Save, "ThongTinNhanVien;QuanLyBienDong")]
    public class BienDong : BaseObject
    {
        private string _LoaiBienDong;
        private QuanLyBienDong _QuanLyBienDong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private TyLeDongBaoHiem _TyLeDong;
        private string _GhiChu;

        //
        [ModelDefault("Caption", "Thời gian")]
        [Association("QuanLyBienDong-ListBienDong")]
        public QuanLyBienDong QuanLyBienDong
        {
            get
            {
                return _QuanLyBienDong;
            }
            set
            {
                SetPropertyValue("QuanLyBienDong", ref _QuanLyBienDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
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
                    UpdateNhanVienList();
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
                if (!IsLoading && value != null)
                {
                    AfterThongTinNhanVienChanged();
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }
            }
        }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Loại biến động")]
        public string LoaiBienDong
        {
            get
            {
                return _LoaiBienDong;
            }
            set
            {
                SetPropertyValue("LoaiBienDong", ref _LoaiBienDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                //
                if (value != DateTime.MinValue && value.Day != 1)
                    value = new DateTime(value.Year, value.Month, 1);
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    TyLeDongBaoHiem = Session.FindObject<TyLeDongBaoHiem>(CriteriaOperator.Parse("TuNgay<=? and DenNgay>=?", value, value));
                }
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Tỷ lệ đóng")]
        public TyLeDongBaoHiem TyLeDongBaoHiem
        {
            get
            {
                return _TyLeDong;
            }
            set
            {
                SetPropertyValue("TyLeDong", ref _TyLeDong, value);
            }
        }

        [Size(200)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public BienDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        /// <summary>
        /// Xảy ra khi thông tin nhân viên được thay đổi
        /// </summary>
        protected virtual void AfterThongTinNhanVienChanged()
        {
            
        }
    }
}
