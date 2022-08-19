using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định miễn nhiễm")]
    public class QuyetDinhThoiChuc : QuyetDinhCaNhan
    {
        //
        private QuyetDinhBoNhiem _QuyetDinhBoNhiem;
        //
        private ChucVu _ChucVuCu;
        private ChucDanh _ChucDanhCu;
        private decimal _PhuCapTrachNhiemCu;
        private decimal _PhuCapKiemNhiemCu;
        private DateTime _NgayBNChucVuCu;
        //
        private ChucVu _ChucVuMoi;
        private ChucDanh _ChucDanhMoi;
        private decimal _PhuCapTrachNhiemMoi;
        private decimal _PhuCapKiemNhiemMoi;
        //
        private string _LyDo;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định bổ nhiệm")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QDList")]
        public QuyetDinhBoNhiem QuyetDinhBoNhiem
        {
            get
            {
                return _QuyetDinhBoNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiem", ref _QuyetDinhBoNhiem, value);       
            }
        }

        [ModelDefault("Caption", "Ngày BH chức vụ cũ")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayBNChucVuCu
        {
            get
            {
                return _NgayBNChucVuCu;
            }
            set
            {
                SetPropertyValue("NgayBNChucVuCu", ref _NgayBNChucVuCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ cũ")]
        [ModelDefault("AllowEdit", "False")]
        public ChucVu ChucVuCu
        {
            get
            {
                return _ChucVuCu;
            }
            set
            {
                SetPropertyValue("ChucVuCu", ref _ChucVuCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVuMoi
        {
            get
            {
                return _ChucVuMoi;
            }
            set
            {
                SetPropertyValue("ChucVuMoi", ref _ChucVuMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức danh cũ")]
        [ModelDefault("AllowEdit", "False")]
        public ChucDanh ChucDanhCu
        {
            get
            {
                return _ChucDanhCu;
            }
            set
            {
                SetPropertyValue("ChucDanhCu", ref _ChucDanhCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức danh mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucDanh ChucDanhMoi
        {
            get
            {
                return _ChucDanhMoi;
            }
            set
            {
                SetPropertyValue("ChucDanhMoi", ref _ChucDanhMoi, value);
            }
        }

        [ModelDefault("Caption", "PC kiêm nhiệm cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("AllowEdit", "False")]
        public decimal PhuCapKiemNhiemCu
        {
            get
            {
                return _PhuCapKiemNhiemCu;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiemCu", ref _PhuCapKiemNhiemCu, value);
            }
        }


        [ModelDefault("Caption", "PC kiêm nhiệm mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKiemNhiemMoi
        {
            get
            {
                return _PhuCapKiemNhiemMoi;
            }
            set
            {
                SetPropertyValue("PhuCapKiemNhiemMoi", ref _PhuCapKiemNhiemMoi, value);
            }
        }

        [ModelDefault("Caption", "PC trách nhiệm cũ")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTrachNhiemCu
        {
            get
            {
                return _PhuCapTrachNhiemCu;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCu", ref _PhuCapTrachNhiemCu, value);
            }
        }

        [ModelDefault("Caption", "PC trách nhiệm mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTrachNhiemMoi
        {
            get
            {
                return _PhuCapTrachNhiemMoi;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemMoi", ref _PhuCapTrachNhiemMoi, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]
        [Size(500)]
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

        [Browsable(false)]
        public XPCollection<QuyetDinhBoNhiem> QDList { get; set; }

        public QuyetDinhThoiChuc(Session session): base(session)  { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhThoiChuc;
            //
            QuyetDinhMoi = true;
            //
            UpdateQuyetDinhList();
        }

        protected override void AfterNhanVienChanged()
        {
            //
            QuyetDinhBoNhiem = null;
            //
            PhuCapKiemNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem;
            PhuCapTrachNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem;
            ChucVuCu = ThongTinNhanVien.ChucVu;
            ChucDanhCu = ThongTinNhanVien.ChucDanh;
            NgayBNChucVuCu = ThongTinNhanVien.NgayBoNhiemChucVu;
            //
            UpdateQuyetDinhList();
        }

        private void UpdateQuyetDinhList()
        {
            if(QDList == null)
             QDList = new XPCollection<QuyetDinhBoNhiem>(Session);
            //
            QDList.Criteria = CriteriaOperator.Parse("ThongTinNhanVien.Oid=? and QuyetDinhMoi", ThongTinNhanVien != null ? ThongTinNhanVien.Oid : Guid.Empty);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if (QuyetDinhMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Vô hiệu hóa quyết định bổ nhiệm
                    QuyetDinhBoNhiem.QuyetDinhMoi = false;

                    //Cập nhật thông tin lương
                    ThongTinNhanVien.ChucVu = ChucVuMoi;
                    ThongTinNhanVien.ChucDanh = ChucDanhMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = PhuCapKiemNhiemMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = PhuCapTrachNhiemMoi;
                    ThongTinNhanVien.NgayBoNhiemChucVu = DateTime.MinValue;

                    JobUpdated = true;
                }

                //Cập nhật đến ngày của quá trình bổ nhiệm
                if (NgayHieuLuc != DateTime.MinValue)
                    ProcessesHelper.UpdateQuaTrinhBoNhiem(Session, QuyetDinhBoNhiem, NgayHieuLuc, true);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //
                if (QuyetDinhMoi)
                {
                    //Mở lại quyết định bổ nhiệm
                    QuyetDinhBoNhiem.QuyetDinhMoi = true;

                    //Cập nhật thông tin
                    ThongTinNhanVien.ChucVu = ChucVuCu;
                    ThongTinNhanVien.ChucDanh = ChucDanhCu;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapKiemNhiem = PhuCapKiemNhiemCu;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiem = PhuCapTrachNhiemCu;
                    ThongTinNhanVien.NgayBoNhiemChucVu = NgayBNChucVuCu;
                }

                if (NgayHieuLuc != DateTime.MinValue)
                    //Cập nhật đến ngày của quá trình bổ nhiệm
                    ProcessesHelper.UpdateQuaTrinhBoNhiem(Session, QuyetDinhBoNhiem, NgayHieuLuc,false);
            }

            base.OnDeleting();
        }
    }
}
