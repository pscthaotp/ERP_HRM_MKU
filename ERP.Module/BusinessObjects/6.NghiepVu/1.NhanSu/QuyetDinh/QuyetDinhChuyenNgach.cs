using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chuyển ngạch")]  
    public class QuyetDinhChuyenNgach : QuyetDinh
    {
        private bool _QuyetDinhMoi;

        //
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
        [Association("QuyetDinhChuyenNgach-ListChiTietQuyetDinhChuyenNgach")]
        public XPCollection<ChiTietQuyetDinhChuyenNgach> ListChiTietQuyetDinhChuyenNgach
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhChuyenNgach>("ListChiTietQuyetDinhChuyenNgach");
            }
        }

        public QuyetDinhChuyenNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            QuyetDinhMoi = true;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }

}
