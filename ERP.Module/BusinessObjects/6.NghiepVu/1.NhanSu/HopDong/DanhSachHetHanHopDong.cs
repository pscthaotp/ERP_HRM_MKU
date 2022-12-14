using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
//
namespace ERP.Module.NghiepVu.NhanSu.HopDongs
{
    [NonPersistent]
    [ImageName("BO_HopDong")]
    [ModelDefault("Caption", "Danh sách hết hạn hợp đồng")]
    public class DanhSachHetHanHopDong : BaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietHetHanHopDong> ChiTietHetHanHopDongList { get; set; }

        public DanhSachHetHanHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ChiTietHetHanHopDongList = new XPCollection<ChiTietHetHanHopDong>(Session, false);
            //
            DateTime currentTime = Common.GetServerCurrentTime();
            TuNgay = currentTime.SetTime(SetTimeEnum.StartMonth);
            DenNgay = currentTime.SetTime(SetTimeEnum.EndMonth);
        }

        public void LoadData()
        {
            if (TuNgay != DateTime.MinValue &&  DenNgay != DateTime.MinValue &&
                TuNgay < DenNgay)
            {
                ChiTietHetHanHopDongList.Reload();

                #region Hợp đồng làm việc
                //
                CriteriaOperator filter = CriteriaOperator.Parse("DenNgay>=? and DenNgay<=?", TuNgay.SetTime(SetTimeEnum.StartDay), DenNgay.SetTime(SetTimeEnum.EndDay));
                XPCollection<HopDongLamViec> hopDongLamViecList = new XPCollection<HopDongLamViec>(Session, filter);
                //
                foreach (var hopDongLamViec in hopDongLamViecList)
                {
                    ThongTinNhanVien nhanVien = Session.GetObjectByKey<ThongTinNhanVien>(hopDongLamViec.ThongTinNhanVien.Oid);
                    if (nhanVien != null &&
                        nhanVien.HopDongHienTai != null &&
                        nhanVien.HopDongHienTai.Oid == hopDongLamViec.Oid &&
                        !nhanVien.TinhTrang.DaNghiViec)
                    {
                        ChiTietHetHanHopDong chiTietHetHanHopDong = new ChiTietHetHanHopDong(Session);
                        chiTietHetHanHopDong.CongTy = nhanVien.BoPhan != null ? nhanVien.BoPhan.CongTy : null;
                        chiTietHetHanHopDong.BoPhan = nhanVien.BoPhan;
                        chiTietHetHanHopDong.ThongTinNhanVien = nhanVien;
                        chiTietHetHanHopDong.HopDong = nhanVien.HopDongHienTai;
                        chiTietHetHanHopDong.LoaiHopDong = nhanVien.HopDongHienTai.LoaiHopDong;
                        chiTietHetHanHopDong.NgayHetHan = hopDongLamViec.DenNgay;
                        ChiTietHetHanHopDongList.Add(chiTietHetHanHopDong);
                    }
                }
                #endregion

                #region Hợp đồng khoán
                filter = CriteriaOperator.Parse("DenNgay>=? and DenNgay<=?", TuNgay.SetTime(SetTimeEnum.StartDay), DenNgay.SetTime(SetTimeEnum.EndDay));
                XPCollection<HopDongKhoan> hopDongKhoanList = new XPCollection<HopDongKhoan>(Session, filter);
                //
                foreach (var hopDongKhoan in hopDongKhoanList)
                {
                    ThongTinNhanVien nhanVien = Session.GetObjectByKey<ThongTinNhanVien>(hopDongKhoan.ThongTinNhanVien.Oid);
                    if (nhanVien != null &&
                        nhanVien.HopDongHienTai != null &&
                        nhanVien.HopDongHienTai.Oid == hopDongKhoan.Oid &&
                        !nhanVien.TinhTrang.DaNghiViec)
                    {
                        ChiTietHetHanHopDong chiTietHetHanHopDong = new ChiTietHetHanHopDong(Session);
                        chiTietHetHanHopDong.BoPhan = nhanVien.BoPhan;
                        chiTietHetHanHopDong.ThongTinNhanVien = nhanVien;
                        chiTietHetHanHopDong.HopDong = nhanVien.HopDongHienTai;
                        chiTietHetHanHopDong.LoaiHopDong = nhanVien.HopDongHienTai.LoaiHopDong;
                        chiTietHetHanHopDong.NgayHetHan = hopDongKhoan.DenNgay;
                        ChiTietHetHanHopDongList.Add(chiTietHetHanHopDong);
                    }
                }
                #endregion

            }
        }  
    }

}
