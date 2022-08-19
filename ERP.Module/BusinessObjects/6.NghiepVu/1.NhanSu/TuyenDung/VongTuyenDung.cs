using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [DefaultProperty("BuocTuyenDung")]
    [ModelDefault("Caption", "Chi tiết vòng tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique("VongTuyenDung", DefaultContexts.Save, "ChiTietTuyenDung;BuocTuyenDung")]
    [Appearance("VongTuyenDung", TargetItems = "BuocTuyenDung", Enabled = false, Criteria = "BuocTuyenDung is not null")]
    //[Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    public class VongTuyenDung : BaseObject
    {
        // Fields...
        private bool IsEnable = true;
        private BuocTuyenDung _BuocTuyenDung;
        private ChiTietTuyenDung _ChiTietTuyenDung;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Chi tiết tuyển dụng")]
        [Association("ChiTietTuyenDung-ListVongTuyenDung")]
        public ChiTietTuyenDung ChiTietTuyenDung
        {
            get
            {
                return _ChiTietTuyenDung;
            }
            set
            {
                SetPropertyValue("ChiTietTuyenDung", ref _ChiTietTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên vòng tuyển dụng")]
        [DataSourceProperty("ChiTietTuyenDung.ListBuocTuyenDung", DataSourcePropertyIsNullMode.SelectNothing)]
        public BuocTuyenDung BuocTuyenDung
        {
            get
            {
                return _BuocTuyenDung;
            }
            set
            {
                SetPropertyValue("BuocTuyenDung", ref _BuocTuyenDung, value);
                if (!IsLoading && value != null)
                {
                    //load danh sách ứng viên đạt ở vòng trước vô đây
                    CriteriaOperator filter;
                    ChiTietVongTuyenDung chiTietVongTuyenDung;
                    if (value.ThuTu == 1)
                    {
                        filter = CriteriaOperator.Parse("NhuCauTuyenDung=?",
                            ChiTietTuyenDung.NhuCauTuyenDung.Oid);
                        using (XPCollection<UngVien> ungVienList = new XPCollection<UngVien>(Session, filter))
                        {
                            foreach (UngVien item in ungVienList)
                            {
                                filter = CriteriaOperator.Parse("VongTuyenDung=? and UngVien=?", Oid, item.Oid);
                                chiTietVongTuyenDung = Session.FindObject<ChiTietVongTuyenDung>(filter);
                                if (chiTietVongTuyenDung == null)
                                {
                                    chiTietVongTuyenDung = new ChiTietVongTuyenDung(Session);
                                    ListChiTietVongTuyenDung.Add(chiTietVongTuyenDung);
                                    chiTietVongTuyenDung.UngVien = item;
                                }
                            }
                        }
                    }
                    else
                    {
                        int thuTu = value.ThuTu - 1;
                        filter = CriteriaOperator.Parse("VongTuyenDung.ChiTietTuyenDung.NhuCauTuyenDung=? and VongTuyenDung.BuocTuyenDung.ThuTu=? and DuocChuyenQuaVongSau",
                            ChiTietTuyenDung.NhuCauTuyenDung.Oid, thuTu);
                        using (XPCollection<ChiTietVongTuyenDung> chiTietList = new XPCollection<ChiTietVongTuyenDung>(Session, filter))
                        {
                            foreach (ChiTietVongTuyenDung item in chiTietList)
                            {
                                filter = CriteriaOperator.Parse("VongTuyenDung=? and UngVien=?", Oid, item.UngVien.Oid);
                                chiTietVongTuyenDung = Session.FindObject<ChiTietVongTuyenDung>(filter);
                                if (chiTietVongTuyenDung == null)
                                {
                                    chiTietVongTuyenDung = new ChiTietVongTuyenDung(Session);
                                    ListChiTietVongTuyenDung.Add(chiTietVongTuyenDung);
                                    chiTietVongTuyenDung.UngVien = item.UngVien;
                                }
                            }
                        }
                    }
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách ứng viên")]
        [Association("VongTuyenDung-ListChiTietVongTuyenDung")]
        public XPCollection<ChiTietVongTuyenDung> ListChiTietVongTuyenDung
        {
            get
            {
                return GetCollection<ChiTietVongTuyenDung>("ListChiTietVongTuyenDung");
            }
        }

        public VongTuyenDung(Session session) : base(session) { }

        //public bool IsExist(UngVien ungVien)
        //{
        //    foreach (ChiTietVongTuyenDung item in ListChiTietVongTuyenDung)
        //    {
        //        if (item.UngVien.Oid == ungVien.Oid)
        //            return true;
        //    }
        //    return false;
        //}


        protected override void OnLoaded()
        {
            base.OnLoaded();
            CriteriaOperator filter;
            if (ChiTietTuyenDung != null && BuocTuyenDung != null)
            {
                //Nếu chưa phải là vòng tuyển dụng cuối thì không được xóa
                filter = CriteriaOperator.Parse("ChiTietTuyenDung=? && BuocTuyenDung.ThuTu =? && GCRecord IS NULL"
                                               , ChiTietTuyenDung, BuocTuyenDung.ThuTu + 1);
                VongTuyenDung vongTuyenDung = Session.FindObject<VongTuyenDung>(filter);
                if (vongTuyenDung != null)
                    IsEnable = false;
            }
            // Nếu đã có ứng viên trong chi tiết thì vòng tuyển dụng thì không được xóa
            filter = CriteriaOperator.Parse("VongTuyenDung=? && GCRecord IS NULL", this.Oid);
            ChiTietVongTuyenDung chiTietVongTuyenDung = Session.FindObject<ChiTietVongTuyenDung>(filter);
            if (chiTietVongTuyenDung != null)
            {
                IsEnable = false;
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            Session.Save(ListChiTietVongTuyenDung);
        }

        protected override void OnDeleting()
        {
            Session.Delete(ListChiTietVongTuyenDung);
            Session.Save(ListChiTietVongTuyenDung);

            base.OnDeleting();
        }
    }
}
