using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nghỉ hưu")]
    public class QuyetDinhNghiHuu : QuyetDinhCaNhan
    {
        private TinhTrang _TinhTrangCu;
        private DateTime _NghiViecTuNgay;

        [ModelDefault("Caption", "Nghỉ việc từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NghiViecTuNgay
        {
            get
            {
                return _NghiViecTuNgay;
            }
            set
            {
                SetPropertyValue("NghiViecTuNgay", ref _NghiViecTuNgay, value);
            }
        }
        

        //Lưu vết tình trạng
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

        public QuyetDinhNghiHuu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhNghiHuu;
        }

        protected override void AfterNhanVienChanged()
        {
            //
            TinhTrangCu = ThongTinNhanVien.TinhTrang;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted)
            {

                if (NghiViecTuNgay <= Common.GetServerCurrentTime())
                {
                    TinhTrang tinhTrang = Common.GetTinhTrang_ByTenTinhTrang(Session, "nghỉ hưu");
                    if (tinhTrang == null)
                    {
                        tinhTrang = new TinhTrang(Session);
                        tinhTrang.TenTinhTrang = "Nghỉ hưu";
                        tinhTrang.MaQuanLy = "NH";
                    }
                    ThongTinNhanVien.TinhTrang = tinhTrang;
                    //
                    ThongTinNhanVien.NgayNghiViec = NghiViecTuNgay;
                }
           }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //
                ThongTinNhanVien.TinhTrang = TinhTrangCu;
                //
                ThongTinNhanVien.NgayNghiViec = DateTime.MinValue;
            }
            //
            base.OnDeleting();
        }
    }

}
