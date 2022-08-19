using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "SoQuyetDinh;QuanLyTuyenDung")]

    public class QuyetDinhTuyenDung : QuyetDinh
    {
        // Fields...
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private DateTime _NgayPhatSinhBienDong = DateTime.Now;            

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày phát sinh biến động")]
        public DateTime NgayPhatSinhBienDong
        {
            get
            {
                return _NgayPhatSinhBienDong;
            }
            set
            {
                SetPropertyValue("NgayPhatSinhBienDong", ref _NgayPhatSinhBienDong, value);
            }
        }

        [ImmediatePostData]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhTuyenDung-ListChiTietQuyetDinhTuyenDung")]
        public XPCollection<ChiTietQuyetDinhTuyenDung> ListChiTietQuyetDinhTuyenDung
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhTuyenDung>("ListChiTietQuyetDinhTuyenDung");
            }
        }

        public QuyetDinhTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhTuyenDung;
            QuyetDinhMoi = true;           
        }

        public void CreateListChiTietQuyetDinhTuyenDung(ThongTinNhanVien nhanVien)
        {
            ChiTietQuyetDinhTuyenDung chiTiet = new ChiTietQuyetDinhTuyenDung(Session);
            chiTiet.BoPhan = nhanVien.BoPhan;
            chiTiet.ThongTinNhanVien = nhanVien;
            this.ListChiTietQuyetDinhTuyenDung.Add(chiTiet);
        }

        protected override void OnDeleting()
        {
            Session.Delete(ListChiTietQuyetDinhTuyenDung);
            Session.Save(ListChiTietQuyetDinhTuyenDung);
            base.OnDeleting();
        }
    }
}
