using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [DefaultProperty("NhuCauTuyenDung")]
    [ModelDefault("Caption", "Chi tiết tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyTuyenDung;NhuCauTuyenDung")]
    [Appearance("NhuCauTuyenDung", TargetItems = "NhuCauTuyenDung", Enabled = false, Criteria = "NhuCauTuyenDung is not null")]
    [Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    public class ChiTietTuyenDung : BaseObject
    {
        // Fields...
        private bool IsEnable = true;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private NhuCauTuyenDung _NhuCauTuyenDung;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListChiTietTuyenDung")]
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

        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListNhuCauTuyenDung")]
        public NhuCauTuyenDung NhuCauTuyenDung
        {
            get
            {
                return _NhuCauTuyenDung;
            }
            set
            {
                SetPropertyValue("NhuCauTuyenDung", ref _NhuCauTuyenDung, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Vòng tuyển dụng")]
        [Association("ChiTietTuyenDung-ListBuocTuyenDung")]
        public XPCollection<BuocTuyenDung> ListBuocTuyenDung
        {
            get
            {
                return GetCollection<BuocTuyenDung>("ListBuocTuyenDung");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết vòng tuyển dụng")]
        [Association("ChiTietTuyenDung-ListVongTuyenDung")]
        public XPCollection<VongTuyenDung> ListVongTuyenDung
        {
            get
            {
                return GetCollection<VongTuyenDung>("ListVongTuyenDung");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách thi")]
        [Association("ChiTietTuyenDung-ListDanhSachThi")]
        public XPCollection<DanhSachThi> ListDanhSachThi
        {
            get
            {
                return GetCollection<DanhSachThi>("ListDanhSachThi");
            }
        }

        public ChiTietTuyenDung(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<NhuCauTuyenDung> NhuCauTuyenDungList { get; set; }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            //if (QuanLyTuyenDung != null)
            //{
            //    CriteriaOperator filter = CriteriaOperator.Parse("NhuCauTuyenDung=? && GCRecord IS NULL", NhuCauTuyenDung);
            //    TrungTuyen trungTuyen = Session.FindObject<TrungTuyen>(filter);
            //    if (trungTuyen != null)
            //        IsEnable = false;
            //}
        }

        protected override void OnDeleting()
        {
            Session.Delete(ListBuocTuyenDung);
            Session.Save(ListBuocTuyenDung);
            Session.Delete(ListVongTuyenDung);
            Session.Save(ListVongTuyenDung);
            Session.Delete(ListDanhSachThi);
            Session.Save(ListDanhSachThi);

            base.OnDeleting();
        }
    }

}
