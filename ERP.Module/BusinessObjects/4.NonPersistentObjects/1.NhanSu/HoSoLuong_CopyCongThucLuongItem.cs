using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn công thức tính lương")]
    public class HoSoLuong_CopyCongThucLuongItem : BaseObject,ICongTy
    {
        private bool _Chon;
        private CongThucTinhLuong _CongThucTinhLuong;
        private CongTy _CongTy;

        [ModelDefault("Caption", "Tất cả")]
        [ImmediatePostData]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Danh sách công thức")]
        [ImmediatePostData]
        public CongThucTinhLuong CongThucTinhLuong
        {
            get
            {
                return _CongThucTinhLuong;
            }
            set
            {
                SetPropertyValue("CongThucTinhLuong", ref _CongThucTinhLuong, value);
            }
        }

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

        public HoSoLuong_CopyCongThucLuongItem(Session session) : base(session) { }  
    }
}
