using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn công thức tính lương và phụ cấp")]
    public class Luong_ChonCongThucLuong : BaseObject
    {

        [ModelDefault("Caption", "Danh sách công thức")]
        public XPCollection<Luong_ChonCongThucLuongItem> CongThucTinhLuongList { get; set; }

        public Luong_ChonCongThucLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongThucTinhLuongList = new XPCollection<Luong_ChonCongThucLuongItem>(Session, false);
            //
            CriteriaOperator filter = string.Empty;
            XPCollection<CongThucTinhLuong> congThucTinhLuongList = null;
            //
            if (!Common.QuanTriToanHeThong())
            {
                filter = CriteriaOperator.Parse("CongTy.Oid=?", Common.CongTy(Session).Oid);
                congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(Session, filter);
            }
            else
            {
                congThucTinhLuongList = new XPCollection<CongThucTinhLuong>(Session);
            }
            //
            foreach (var item in congThucTinhLuongList)
            {
                CongThucTinhLuongList.Add(new Luong_ChonCongThucLuongItem(Session) { CongThucTinhLuong = item, CongTy = item.CongTy });
            }
        }

    }
}
