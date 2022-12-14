using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using System.ComponentModel;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn đăng ký tuyển dụng để duyệt")]
    public class TuyenDung_DuyetDangKyTuyenDungItem : BaseObject, IBoPhan
    {
        private bool _Chon;
        private BoPhan _BoMon;
        private ViTriTuyenDung _ViTriTuyenDung;
        private int _SoLuongTuyen;
        private BoPhan _BoPhan;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private DangKyTuyenDung _DangKyTuyenDung;

        [Browsable(false)]
        [ModelDefault("Caption", "Đăng ký tuyển dụng")]
        public DangKyTuyenDung DangKyTuyenDung
        {
            get
            {
                return _DangKyTuyenDung;
            }
            set
            {
                SetPropertyValue("DangKyTuyenDung", ref _DangKyTuyenDung, value);
            }
        }

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

        [ModelDefault("Caption", "Vị trí tuyển dụng")]
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

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Bộ môn")]
        public BoPhan BoMon
        {
            get
            {
                return _BoMon;
            }
            set
            {
                SetPropertyValue("BoMon", ref _BoMon, value);
            }
        }

        [ModelDefault("Caption", "Số lượng tuyển")]
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

        public TuyenDung_DuyetDangKyTuyenDungItem(Session session) : base(session) { }
    }
}
