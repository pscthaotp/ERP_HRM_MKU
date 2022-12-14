using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Commons;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NghiepVu.NhanSu.BaoHiem
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Quản lý biến động")]
    [DefaultProperty("Caption")]
    [Appearance("QuanLyBienDong.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "KhoaSo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "CongTy;ThoiGian;Dot")]
    public class QuanLyBienDong : BaseObject,ICongTy
    {
        // 
        private bool _KhoaSo;
        private int _Dot = 1;
        private DateTime _ThoiGian;
        private CongTy _CongTy;

        //
        [ModelDefault("Caption", "Thời gian")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime ThoiGian
        {
            get
            {
                return _ThoiGian;
            }
            set
            {
                SetPropertyValue("ThoiGian", ref _ThoiGian, value);
                CreateCaption();
            }
        }

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
                CreateCaption();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }
        
        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string Caption { get; private set; }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách biến động")]
        [Association("QuanLyBienDong-ListBienDong")]
        public XPCollection<BienDong> ListBienDong
        {
            get
            {
                return GetCollection<BienDong>("ListBienDong");
            }
        }

        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
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
            }
        }
        public QuanLyBienDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThoiGian == DateTime.MinValue)
                ThoiGian = Common.GetServerCurrentTime();
            //
            CongTy = Common.CongTy(Session);
        }

        private void CreateCaption()
        {
            if (ThoiGian != DateTime.MinValue && Dot > 0)
                Caption = String.Format("Tháng {0} (Đợt {1})", ThoiGian.ToString("MM/yyyy"), Dot);
        }
    }

}
