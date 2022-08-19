using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [DefaultProperty("UngVien")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Không trúng tuyển")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyTuyenDung;UngVien;NhuCauTuyenDung")]
    public class KhongTrungTuyen : BaseObject
    {
        // Fields...
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private NhuCauTuyenDung _NhuCauTuyenDung;
        private UngVien _UngVien;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListKhongTrungTuyen")]
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
        [ModelDefault("Caption", "Ứng viên")]
        [DataSourceProperty("QuanLyTuyenDung.ListUngVien")]
        [RuleRequiredField(DefaultContexts.Save)]
        public UngVien UngVien
        {
            get
            {
                return _UngVien;
            }
            set
            {
                SetPropertyValue("UngVien", ref _UngVien, value);
                if (!IsLoading && value != null)
                    NhuCauTuyenDung = value.NhuCauTuyenDung;
            }
        }

        [ModelDefault("Caption", "Vị trí ứng tuyển")]
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

        public KhongTrungTuyen(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<UngVien> UngVienList { get; set; }

        private void UpdateUngVienList()
        {
            if (UngVienList == null)
                UngVienList = new XPCollection<UngVien>(Session);
            else
                UngVienList.Reload();

            //chỉ lấy các ứng viên ở vòng tuyển dụng cuối cùng
            List<Guid> oid = new List<Guid>();
            CriteriaOperator filter;
            object thuTu;
            foreach (ChiTietTuyenDung item in QuanLyTuyenDung.ListChiTietTuyenDung)
            {
                filter = CriteriaOperator.Parse("ChiTietTuyenDung=?",
                    item.Oid);
                thuTu = Session.Evaluate<BuocTuyenDung>(CriteriaOperator.Parse("Max(ThuTu)"), filter);
                if (thuTu != null)
                {
                    filter = CriteriaOperator.Parse("VongTuyenDung.ChiTietTuyenDung=? and VongTuyenDung.BuocTuyenDung.ThuTu=?",
                        item.Oid, thuTu);
                    using (XPCollection<ChiTietVongTuyenDung> ctList = new XPCollection<ChiTietVongTuyenDung>(Session, filter))
                    {
                        foreach (ChiTietVongTuyenDung ctItem in ctList)
                        {
                            oid.Add(ctItem.UngVien.Oid);
                        }
                    }
                }
            }
            UngVienList.Criteria = new InOperator("Oid", oid);
        }
    }
}
