using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định điều chỉnh tiền lương")]
    public class QuyetDinhNangLuong : QuyetDinh
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [ImmediatePostData]
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

        [ImmediatePostData]
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

        //
        //[NonPersistent]
        //[ModelDefault("Caption", "Bộ phận")]
        //public string BoPhanText
        //{
        //    get
        //    {
        //        try { return ListChiTietQuyetDinhNangLuong[0].BoPhan != null ? ListChiTietQuyetDinhNangLuong[0].BoPhan.TenBoPhan : "";}
        //        catch (Exception ex) { return null; }
        //    }
        //}

        //[NonPersistent]
        //[ModelDefault("Caption", "Nhân viên")]
        //public string NhanVienText
        //{
        //    get
        //    {
        //        try { return ListChiTietQuyetDinhNangLuong[0].ThongTinNhanVien != null ? ListChiTietQuyetDinhNangLuong[0].ThongTinNhanVien.HoTen : ""; }
        //        catch (Exception ex) { return null; }
        //    }
        //}

        [Aggregated]
        [ModelDefault("Caption", "Thông tin lương thay đổi")]
        [Association("QuyetDinhNangLuong-ListChiTietQuyetDinhNangLuong")]
        public XPCollection<ChiTietQuyetDinhNangLuong> ListChiTietQuyetDinhNangLuong
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangLuong>("ListChiTietQuyetDinhNangLuong");
            }
        }

        public QuyetDinhNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhNangLuong;
            //
            QuyetDinhMoi = true;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (ListChiTietQuyetDinhNangLuong.Count > 0)
            {
                BoPhan = ListChiTietQuyetDinhNangLuong[0].BoPhan != null ? ListChiTietQuyetDinhNangLuong[0].BoPhan : null;
                ThongTinNhanVien = ListChiTietQuyetDinhNangLuong[0].ThongTinNhanVien != null ? ListChiTietQuyetDinhNangLuong[0].ThongTinNhanVien : null;
            }            
        }
    }

}
