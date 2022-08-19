using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tiếp nhận")]
    public class QuyetDinhTiepNhan : QuyetDinhCaNhan
    {
        private TinhTrang _TinhTrangCu;
        private DateTime _TuNgay;
        private QuyetDinhNghiKhongHuongLuong _QuyetDinhNghiKhongHuongLuong;

        [DataSourceProperty("QuyetDinhList")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "QĐ nghỉ không hưởng lương")]
        public QuyetDinhNghiKhongHuongLuong QuyetDinhNghiKhongHuongLuong
        {
            get
            {
                return _QuyetDinhNghiKhongHuongLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNghiKhongHuongLuong", ref _QuyetDinhNghiKhongHuongLuong, value);
            }
        }

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

        //dùng để lưu vết
        [Browsable(false)]
        [ModelDefault("Caption", "Tình trạng cũ")]
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

        [Browsable(false)]
        private XPCollection<QuyetDinhNghiKhongHuongLuong> QuyetDinhList { get; set; }

        public QuyetDinhTiepNhan(Session session) : base(session) { }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhNghiKhongHuongLuong>(Session);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
            SortProperty sort = new SortProperty("NgayQuyetDinh", DevExpress.Xpo.DB.SortingDirection.Descending);
            QuyetDinhList.Criteria = filter;

            XPCollection<QuyetDinhNghiKhongHuongLuong> qdList = new XPCollection<QuyetDinhNghiKhongHuongLuong>(Session, filter, sort) { TopReturnedObjects = 1 };
            if (qdList.Count == 1)
            {
                QuyetDinhNghiKhongHuongLuong = qdList[0];
                TuNgay = QuyetDinhNghiKhongHuongLuong.DenNgay;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhTiepNhan;
            //
            QuyetDinhMoi = true;
        }

        protected override void AfterNhanVienChanged()
        {
            TinhTrangCu = ThongTinNhanVien.TinhTrang;
            UpdateQuyetDinhList();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {

                if (QuyetDinhMoi)
                {
                    //thiết lập tình trạng
                    if (TuNgay <= Common.GetServerCurrentTime())
                    {
                        TinhTrang tinhTrang = Session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
                        if (tinhTrang == null)
                        {
                            tinhTrang = new TinhTrang(Session);
                            tinhTrang.MaQuanLy = "DLV";
                            tinhTrang.TenTinhTrang = "Đang làm việc";
                        }
                        ThongTinNhanVien.TinhTrang = tinhTrang;

                        JobUpdated = true;
                    }
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (QuyetDinhMoi)
                    ThongTinNhanVien.TinhTrang = TinhTrangCu;

            }

            base.OnDeleting();
        }
    }

}
