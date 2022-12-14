using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chuyển công tác")]
    public class QuyetDinhChuyenCongTac : QuyetDinhCaNhan
    {
        private TinhTrang _TinhTrangCu;
        private DateTime _TuNgay;
        private string _CoQuanMoi;

        [Size(200)]
        [ModelDefault("Caption", "Cơ quan mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string CoQuanMoi
        {
            get
            {
                return _CoQuanMoi;
            }
            set
            {
                SetPropertyValue("CoQuanMoi", ref _CoQuanMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        //Dùng để lưu vết
        [Browsable(false)]
        public TinhTrang TinhTrangCu
        {
            get
            {
                return _TinhTrangCu;
            }
            set
            {
                SetPropertyValue("TinhTrangCu", ref _TinhTrangCu, value);
            }
        }

        public QuyetDinhChuyenCongTac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhChuyenCongTac;
            //
            QuyetDinhMoi = true;
            //
        }

        protected override void AfterNhanVienChanged()
        {
            base.AfterNhanVienChanged();
            //
            TinhTrangCu = ThongTinNhanVien.TinhTrang;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //
                if (QuyetDinhMoi)
                {
                    //Update tình trạng
                    if (TuNgay <= Common.GetServerCurrentTime())
                    {
                        TinhTrang tinhTrang = Common.GetTinhTrang_ByTenTinhTrang(Session, "chuyển công tác");
                        if (tinhTrang == null)
                        {
                            tinhTrang = new TinhTrang(Session);
                            tinhTrang.TenTinhTrang = "chuyển công tác";
                            tinhTrang.MaQuanLy = "CCT";
                        }
                        ThongTinNhanVien.TinhTrang = tinhTrang;
                        //
                        ThongTinNhanVien.NgayNghiViec = TuNgay;

                        JobUpdated = true;
                    }
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving && QuyetDinhMoi)
            {
                //
                ThongTinNhanVien.TinhTrang = TinhTrangCu;
                //
                ThongTinNhanVien.NgayNghiViec = DateTime.MinValue;

              }
            base.OnDeleting();
        }
    }
}
