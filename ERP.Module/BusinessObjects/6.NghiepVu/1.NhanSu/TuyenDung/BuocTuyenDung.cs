using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [DefaultProperty("TenBuocTuyenDung")]
    [ModelDefault("Caption", "Bước tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique("BuocTuyenDung.Unique", DefaultContexts.Save, "ChiTietTuyenDung;ThuTu")]
    [Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    public class BuocTuyenDung : BaseObject
    {
        // Fields...
        private bool IsEnable = true;
        private bool _CoToChucThiTuyen;
        private ChiTietTuyenDung _ChiTietTuyenDung;
        private int _ThangDiem;
        private int _ThuTu;
        private string _TenBuocTuyenDung;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Chi tiết tuyển dụng")]
        [Association("ChiTietTuyenDung-ListBuocTuyenDung")]
        public ChiTietTuyenDung ChiTietTuyenDung
        {
            get
            {
                return _ChiTietTuyenDung;
            }
            set
            {
                SetPropertyValue("ChiTietTuyenDung", ref _ChiTietTuyenDung, value);
                if (!IsLoading && value != null)
                {
                    int temp = 1;
                    foreach (BuocTuyenDung item in value.ListBuocTuyenDung)
                    {
                        temp++;
                    }
                    ThuTu = temp;
                }
            }
        }

        [ModelDefault("Caption", "Thứ tự")]
        //[ModelDefault("AllowEdit", "False")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThuTu
        {
            get
            {
                return _ThuTu;
            }
            set
            {
                SetPropertyValue("ThuTu", ref _ThuTu, value);
            }
        }

        [ModelDefault("Caption", "Tên bước tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBuocTuyenDung
        {
            get
            {
                return _TenBuocTuyenDung;
            }
            set
            {
                SetPropertyValue("TenBuocTuyenDung", ref _TenBuocTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Thang điểm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThangDiem
        {
            get
            {
                return _ThangDiem;
            }
            set
            {
                SetPropertyValue("ThangDiem", ref _ThangDiem, value);
            }
        }

        [ModelDefault("Caption", "Có tổ chức thi tuyển")]
        public bool CoToChucThiTuyen
        {
            get
            {
                return _CoToChucThiTuyen;
            }
            set
            {
                SetPropertyValue("CoToChucThiTuyen", ref _CoToChucThiTuyen, value);
            }
        }

        public BuocTuyenDung(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            CriteriaOperator filter = CriteriaOperator.Parse("BuocTuyenDung=? && GCRecord IS NULL", this.Oid);
            VongTuyenDung vongTuyenDung = Session.FindObject<VongTuyenDung>(filter);
            if (vongTuyenDung != null)
                IsEnable = false;
        }
    }
}
