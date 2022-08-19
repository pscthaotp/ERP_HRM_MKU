using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [DefaultProperty("Caption")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Nhu cầu tuyển dụng")] // Duyệt đăng ký tuyển dụng
    [RuleCombinationOfPropertiesIsUnique("NhuCauTuyenDung", DefaultContexts.Save, "ViTriTuyenDung;BoPhan")]
    [Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    public class NhuCauTuyenDung : BaseObject, IBoPhan
    {
        // Fields...
        private bool IsEnable = true;
        private ViTriTuyenDung _ViTriTuyenDung;
        private int _SoLuongTuyen;
        private BoPhan _BoPhan;
        private QuanLyTuyenDung _QuanLyTuyenDung;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListNhuCauTuyenDung")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListViTriTuyenDung")]
        public ViTriTuyenDung ViTriTuyenDung
        {
            get
            {
                return _ViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("ViTriTuyenDung", ref _ViTriTuyenDung, value);
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
                if (!IsLoading && value != null)
                {
                    UpdateBoMonList();
                }
            }
        }



        [ModelDefault("Caption", "Số lượng tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoLuongTuyen
        {
            get
            {
                return _SoLuongTuyen;
            }
            set
            {
                SetPropertyValue("SoLuongTuyen", ref _SoLuongTuyen, value);
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("{ViTriTuyenDung.TenViTriTuyenDung} {BoPhan.TenBoPhan}", this);
            }
        }

        public NhuCauTuyenDung(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (ViTriTuyenDung == null) return;
            CriteriaOperator filter = CriteriaOperator.Parse("NhuCauTuyenDung=? && GCRecord IS NULL", this.Oid);
            ChiTietTuyenDung chiTietTuyenDung = Session.FindObject<ChiTietTuyenDung>(filter);
            if (chiTietTuyenDung != null)
                IsEnable = false;
        }

        [Browsable(false)]
        public XPCollection<BoPhan> BoMonList { get; set; }

        private void UpdateBoMonList()
        {
            if (BoMonList == null)
                BoMonList = new XPCollection<BoPhan>(Session);

            BoMonList.Criteria = CriteriaOperator.Parse("BoPhanCha=? and LoaiBoPhan=3",
                BoPhan);
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ViTriTuyenDung=?", ViTriTuyenDung);
                DangKyTuyenDung dangKyTuyenDung = Session.FindObject<DangKyTuyenDung>(filter);
                if (dangKyTuyenDung != null)
                {
                    dangKyTuyenDung.Duyet = false;
                }
            }

            base.OnDeleting();
        }
    }

}
