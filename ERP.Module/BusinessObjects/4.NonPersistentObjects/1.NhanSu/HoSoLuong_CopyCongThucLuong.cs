using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Copy công thức tính lương và phụ cấp")]
    public class HoSoLuong_CopyCongThucLuong : BaseObject,ICongTy
    {
        private CongTy _CongTy;

        [ModelDefault("Caption", "Chọn Trường")]
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

        [ModelDefault("Caption", "Danh sách công thức")]
        public XPCollection<HoSoLuong_CopyCongThucLuongItem> CongThucTinhLuongList { get; set; }

        public HoSoLuong_CopyCongThucLuong(Session session) : base(session) { }

        public void GetCongThucTinhLuongAll(CongTy congTy)
        {
            //
            CongThucTinhLuongList = new XPCollection<HoSoLuong_CopyCongThucLuongItem>(Session, false);
            //
            CriteriaOperator filter = CriteriaOperator.Parse("CongTy.Oid=?", congTy.Oid);
            using (XPCollection<CongThucTinhLuong> congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(Session, filter))
            {
                foreach (var item in congThucTinhLuongList)
                {
                    CongThucTinhLuongList.Add(new HoSoLuong_CopyCongThucLuongItem(Session) { CongThucTinhLuong = item, CongTy = item.CongTy });
                }
            }

        }

    }
}
