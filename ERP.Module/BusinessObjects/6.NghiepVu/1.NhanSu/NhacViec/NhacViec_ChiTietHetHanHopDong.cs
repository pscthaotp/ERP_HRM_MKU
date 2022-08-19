using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nội dung chi tiết - Hết hạn hợp đồng")]
    public class NhacViec_ChiTietHetHanHopDong : BaseObject
    {
        private LoaiHopDong _LoaiHopDong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private HopDong _HopDongHienTai;
        private DateTime _NgayHetHan;
        private string _GhiChu;

        [ImmediatePostData]
        [ModelDefault("Caption", "Số hợp đồng")]
        public HopDong HopDongHienTai
        {
            get
            {
                return _HopDongHienTai;
            }
            set
            {
                SetPropertyValue("HopDongHienTai", ref _HopDongHienTai, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiHopDong LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
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

        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }


        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return _NgayHetHan;
            }
            set
            {
                SetPropertyValue("NgayHetHan", ref _NgayHetHan, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public NhacViec_ChiTietHetHanHopDong(Session session) : base(session) { }

    }

}
