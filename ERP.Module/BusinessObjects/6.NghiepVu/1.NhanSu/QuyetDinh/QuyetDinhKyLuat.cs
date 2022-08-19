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
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định kỷ luật")]
    //[RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "SoQuyetDinh;QuanLyKyLuat")]

    public class QuyetDinhKyLuat : QuyetDinhCaNhan
    {
        // Fields...
        //private QuanLyKyLuat _QuanLyKyLuat;
        private string _SoBienBan;
        private DateTime _NgayLapBienBan;
        private HinhThucKyLuat _HinhThucKyLuat;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
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

        [ModelDefault("Caption", "Số biên bản")]
        public string SoBienBan
        {
            get
            {
                return _SoBienBan;
            }
            set
            {
                SetPropertyValue("SoBienBan", ref _SoBienBan, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập biên bản")]
        public DateTime NgayLapBienBan
        {
            get
            {
                return _NgayLapBienBan;
            }
            set
            {
                SetPropertyValue("NgayLapBienBan", ref _NgayLapBienBan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hình thức kỷ luật")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HinhThucKyLuat HinhThucKyLuat
        {
            get
            {
                return _HinhThucKyLuat;
            }
            set
            {
                SetPropertyValue("HinhThucKyLuat", ref _HinhThucKyLuat, value);
                //if (!IsLoading && value != null)
                //    UpdateDeNghiKyLuat();
            }
        }

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
        //[ModelDefault("Caption", "Quản lý kỷ luật")]
        //public QuanLyKyLuat QuanLyKyLuat
        //{
        //    get
        //    {
        //        return _QuanLyKyLuat;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanLyKyLuat", ref _QuanLyKyLuat, value);
        //    }
        //}      

        public QuyetDinhKyLuat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhKyLuat;
            QuyetDinhMoi = true;           
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && Session is NestedUnitOfWork)
            {
                //Tạo quá trình kỷ luật khi lưu quyết định
                ProcessesHelper.CreateQuaTrinhKyLuat(Session, ThongTinNhanVien, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Xóa quá trình kỷ luật
                ProcessesHelper.DeleteQuaTrinhNhanVien<QuaTrinhKyLuat>(Session, this.Oid, this.ThongTinNhanVien.Oid);
            }
            base.OnDeleting();
        }
    }
}
