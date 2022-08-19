using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.TienLuong.TruyLuong
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Truy lĩnh lương nhân viên")]
    [RuleCombinationOfPropertiesIsUnique("TruyLinhNhanVien.Unique", DefaultContexts.Save, "BangTruyLinh;ThongTinNhanVien")]
    public class TruyLinhNhanVien : BaseObject, IBoPhan
    {
        private BangTruyLinh _BangTruyLinh;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _GhiChu;
        //

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Bảng truy lĩnh")]
        [Association("BangTruyLinh-ListTruyLinhNhanVien")]
        public BangTruyLinh BangTruyLinh
        {
            get
            {
                return _BangTruyLinh;
            }
            set
            {
                SetPropertyValue("BangTruyLinh", ref _BangTruyLinh, value);
                if (!IsLoading)
                {
                    UpdateBoPhanList();
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [DataSourceProperty("BoPhanList", DataSourcePropertyIsNullMode.SelectAll)]
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
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết truy lĩnh")]
        [Association("TruyLinhNhanVien-ListChiTietTruyLinh")]
        public XPCollection<ChiTietTruyLinh> ListChiTietTruyLuong
        {
            get
            {
                return GetCollection<ChiTietTruyLinh>("ListChiTietTruyLuong");
            }
        }

        public TruyLinhNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
            {
                //NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
                NVList.Criteria = CriteriaOperator.Parse("CongTy = ?", BangTruyLinh.CongTy.Oid);
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
            BoPhanList.Criteria = Common.Criteria_BoPhan_DanhSachBoPhanDuocPhanQuyen(BangTruyLinh.CongTy);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //UpdateNhanVienList();
        }
        //

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            //UpdateNhanVienList();
        }
    }
}
