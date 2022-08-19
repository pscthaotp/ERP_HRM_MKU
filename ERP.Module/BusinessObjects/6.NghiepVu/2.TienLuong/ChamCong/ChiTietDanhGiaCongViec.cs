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
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [ImageName("BO_QuanLyChamCong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đánh giá công việc")]
    public class ChiTietDanhGiaCongViec : BaseObject
    {
        //
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private TiLeDanhGiaCongViec _TiLeDanhGiaCongViec;
        private decimal _TiLe;
        private DanhGiaEnum _DanhGia;
        private string _GhiChu;
        
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Tỉ lệ đánh giá")]
        [Association("TiLeDanhGiaCongViec-ListChiTietDanhGiaCongViec")]
        public TiLeDanhGiaCongViec TiLeDanhGiaCongViec
        {
            get
            {
                return _TiLeDanhGiaCongViec;
            }
            set
            {
                SetPropertyValue("TiLeDanhGiaCongViec", ref _TiLeDanhGiaCongViec, value);
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
                if (!IsLoading && value != null)
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

        [ModelDefault("Caption", "Tỉ lệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TiLe
        {
            get
            {
                return _TiLe;
            }
            set
            {
                SetPropertyValue("TiLe", ref _TiLe, value);
            }
        }

        [ModelDefault("Caption", "Đánh giá")]
        public DanhGiaEnum DanhGia
        {
            get
            {
                return _DanhGia;
            }
            set
            {
                SetPropertyValue("DanhGia", ref _DanhGia, value);
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
                SetPropertyValue("_GhiChu", ref _GhiChu, value);
            }
        }

        public ChiTietDanhGiaCongViec(Session session) : base(session) { }

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
                NVList.Criteria = CriteriaOperator.Parse("CongTy = ?", TiLeDanhGiaCongViec.CongTy.Oid);
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
            BoPhanList.Criteria = Common.Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(TiLeDanhGiaCongViec.CongTy);
        }
    }
}
