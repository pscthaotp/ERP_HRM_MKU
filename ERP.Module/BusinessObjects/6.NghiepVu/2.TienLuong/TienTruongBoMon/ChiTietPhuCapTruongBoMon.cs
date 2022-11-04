using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;

namespace ERP.Module.NghiepVu.TienLuong.Thuong
{
    [DefaultProperty("NhanVien")]
    [ModelDefault("Caption", "Chi tiết thưởng trưởng bộ môn")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "PhuCapTruongBoMon;NhanVien")]
    public class ChiTietPhuCapTruongBoMon : BaseObject, IBoPhan
    {
        private PhuCapTruongBoMon _PhuCapTruongBoMon;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private DateTime _NgayLap;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _GhiChu;
        private DateTime _NgayThuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Phụ cấp trưởng bộ môn")]
        [Association("PhuCapTruongBoMon-ListChiTietPhuCapTruongBoMon")]
        public PhuCapTruongBoMon PhuCapTruongBoMon
        {
            get
            {
                return _PhuCapTruongBoMon;
            }
            set
            {
                SetPropertyValue("PhuCapTruongBoMon", ref _PhuCapTruongBoMon, value);
            }
        }

        //Chỉ dùng để lập công thức
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong { get; set; }


        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (value.BoPhan != BoPhan)
                        BoPhan = value.BoPhan;
                }
            }
        }
        [ModelDefault("Caption", "Ngày lập")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [ModelDefault("Caption", "Ngày thưởng")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayThuong
        {
            get
            {
                return _NgayThuong;
            }
            set
            {
                SetPropertyValue("NgayThuong", ref _NgayThuong, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }


      

        

        



        [Size(500)]
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

        public ChiTietPhuCapTruongBoMon(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<NhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<NhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }
        //

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }
    }

}
