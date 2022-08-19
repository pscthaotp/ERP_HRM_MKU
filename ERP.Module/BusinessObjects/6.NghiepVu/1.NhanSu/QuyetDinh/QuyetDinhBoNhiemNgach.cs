using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định bổ nhiệm ngạch")]
    public class QuyetDinhBoNhiemNgach : QuyetDinh
    {
        private bool _QuyetDinhMoi;

        [ModelDefault("Caption", "Quyết định mới")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhBoNhiemNgach-ListChiTietQuyetDinhBoNhiemNgach")]
        public XPCollection<ChiTietQuyetDinhBoNhiemNgach> ListChiTietQuyetDinhBoNhiemNgach
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhBoNhiemNgach>("ListChiTietQuyetDinhBoNhiemNgach");
            }
        }

        public QuyetDinhBoNhiemNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            QuyetDinhMoi = true;
        }
    }

}
