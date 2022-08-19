using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.TienLuong.ChamCong
{
    [DefaultClassOptions]
    [DefaultProperty("KyTinhLuong.TenKy")]
    [ImageName("BO_QuanLyChamCong")]
    [ModelDefault("Caption", "Tỉ lệ đánh giá công việc")]
    [RuleCombinationOfPropertiesIsUnique("TiLeDanhGia", DefaultContexts.Save, "CongTy;KyTinhLuong")]
    [Appearance("TiLeDanhGiaCongViec", TargetItems = "*", Enabled = false, Criteria = "KyTinhLuong.KhoaSo")]
    public class TiLeDanhGiaCongViec : BaseObject, ICongTy
    {
        //
        private CongTy _CongTy;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;

        [ImmediatePostData]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        //[DataSourceCriteria("!KhoaSo")]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading)
                {
                    //
                    KyTinhLuong = null;
                    //
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and CongTy.Oid = ? and !KhoaSo", NgayLap.Month, NgayLap.Year, CongTy.Oid));
                }
            }
        }

        [ImmediatePostData]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Công ty/Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết đánh giá")]
        [Association("TiLeDanhGiaCongViec-ListChiTietDanhGiaCongViec")]
        public XPCollection<ChiTietDanhGiaCongViec> ListChiTietDanhGiaCongViec
        {
            get
            {
                return GetCollection<ChiTietDanhGiaCongViec>("ListChiTietDanhGiaCongViec");
            }
        }

        public TiLeDanhGiaCongViec(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            //
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);
            KyTinhLuongList.Criteria = CriteriaOperator.Parse("CongTy.Oid = ? and !KhoaSo", CongTy != null ? CongTy.Oid : Guid.Empty);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            UpdateKyTinhLuongList();
            //
            NgayLap = Common.GetServerCurrentTime();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
        }
    }
}
