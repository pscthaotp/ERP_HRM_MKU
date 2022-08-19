using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định khen thưởng")]
    //[RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "SoQuyetDinh;QuanLyKhenThuong")]

    public class QuyetDinhKhenThuong : QuyetDinh
    {
        // Fields...
        //private QuanLyKhenThuong _QuanLyTuyenDung;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;
        private string _LyDo;
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
        [ModelDefault("Caption", "Danh hiệu khen thưởng")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
                //if (!IsLoading && value != null)
                //    UpdateDeNghiKhenThuong();
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        //[ImmediatePostData]
        //[ModelDefault("Caption", "Quản lý khen thưởng")]
        //public QuanLyTuyenDung QuanLyKhenThuong
        //{
        //    get
        //    {
        //        return _QuanLyKhenThuong;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanLyKhenThuong", ref _QuanLyKhenThuong, value);
        //    }
        //}        

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhKhenThuong-ListChiTietQuyetDinhKhenThuongNhanVien")]
        public XPCollection<ChiTietQuyetDinhKhenThuongNhanVien> ListChiTietQuyetDinhKhenThuongNhanVien
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhKhenThuongNhanVien>("ListChiTietQuyetDinhKhenThuongNhanVien");
            }
        }

        public QuyetDinhKhenThuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhKhenThuong;
            QuyetDinhMoi = true;           
        }

        public void CreateListChiTietQuyetDinhTuyenDung(ThongTinNhanVien nhanVien)
        {
            ChiTietQuyetDinhKhenThuongNhanVien chiTiet = new ChiTietQuyetDinhKhenThuongNhanVien(Session);
            chiTiet.BoPhan = nhanVien.BoPhan;
            chiTiet.ThongTinNhanVien = nhanVien;
            this.ListChiTietQuyetDinhKhenThuongNhanVien.Add(chiTiet);
        }

        protected override void OnDeleting()
        {
            Session.Delete(ListChiTietQuyetDinhKhenThuongNhanVien);
            Session.Save(ListChiTietQuyetDinhKhenThuongNhanVien);
            base.OnDeleting();
        }
    }
}
