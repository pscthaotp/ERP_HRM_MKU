using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using System.ComponentModel;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Duyệt đăng ký tuyển dụng")]
    public class TuyenDung_DuyetDangKyTuyenDung : BaseObject
    {
        private QuanLyTuyenDung _QuanLyTuyenDung;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
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

        [ModelDefault("Caption", "Danh sách đăng ký")]
        public XPCollection<TuyenDung_DuyetDangKyTuyenDungItem> DuyetDangKyTuyenDungList { get; set; }

        public TuyenDung_DuyetDangKyTuyenDung(Session session) : base(session) { }

        public void GetDangKyTuyenDungAll(QuanLyTuyenDung quanLyTuyenDung)
        {
            DuyetDangKyTuyenDungList = new XPCollection<TuyenDung_DuyetDangKyTuyenDungItem>(Session, false);
            //
            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyTuyenDung.Oid=? && ISNULL(Duyet,0) = 0", quanLyTuyenDung.Oid);
            using (XPCollection<DangKyTuyenDung> dangKyTuyenDungList = new XPCollection<DangKyTuyenDung>(Session, filter))
            {
                foreach (var item in dangKyTuyenDungList)
                {
                    DuyetDangKyTuyenDungList.Add(new TuyenDung_DuyetDangKyTuyenDungItem(Session) { DangKyTuyenDung = item, QuanLyTuyenDung = item.QuanLyTuyenDung, ViTriTuyenDung = item.ViTriTuyenDung, BoPhan = item.BoPhan, BoMon = item.BoMon, SoLuongTuyen = item.SoLuongTuyen });
                }
            }
        }
    }
}
