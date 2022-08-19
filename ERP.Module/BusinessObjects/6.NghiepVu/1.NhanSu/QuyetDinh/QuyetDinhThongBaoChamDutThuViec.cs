using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using System.ComponentModel;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định thông báo chấm dứt thử việc")]
    public class QuyetDinhThongBaoChamDutThuViec : QuyetDinhCaNhan
    {
        //
        private HopDong _HopDong;

        [DataSourceProperty("HDList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Hợp đồng")]
        public HopDong HopDong
        {
            get
            {
                return _HopDong;
            }
            set
            {
                SetPropertyValue("HopDong", ref _HopDong, value);
            }
        }

        public QuyetDinhThongBaoChamDutThuViec(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhThongBaoChamDutThuViec;
        }

        protected override void AfterNhanVienChanged()
        {
            BoPhan = ThongTinNhanVien.BoPhan;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted)
            {           
                //Update tình trạng
                if (NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    TinhTrang tinhTrang = Common.GetTinhTrang_ByTenTinhTrang(Session, "%nghỉ việc%");
                    if (tinhTrang == null)
                    {
                        tinhTrang = new TinhTrang(Session);
                        tinhTrang.TenTinhTrang = "Nghỉ việc";
                        tinhTrang.MaQuanLy = "NV";
                    }
                    ThongTinNhanVien.TinhTrang = tinhTrang;
                    ThongTinNhanVien.NgayNghiViec = NgayHieuLuc;
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                TinhTrang tinhTrang = Common.GetTinhTrang_ByTenTinhTrang(Session, "%đang làm việc%");
                if (tinhTrang != null)
                {
                    ThongTinNhanVien.TinhTrang = tinhTrang;
                    ThongTinNhanVien.NgayNghiViec = DateTime.MinValue;
                }
            }

            base.OnDeleting();
        }

        [Browsable(false)]
        public XPCollection<HopDong> HDList { get; set; }

        private void UpdateHDList()
        {
            //
            if (HDList == null)
                HDList = new XPCollection<HopDong>(Session);
            //
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien.Oid=?", ThongTinNhanVien != null ? ThongTinNhanVien.Oid : Guid.Empty);
            HDList.Criteria = filter;
        }
    }
}
