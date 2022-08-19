using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;

//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nâng thâm niên nghề")]
    public class QuyetDinhNangThamNienNghe : QuyetDinh
    {

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhNangThamNienNghe-ListChiTietQuyetDinhNangThamNienNghe")]
        public XPCollection<ChiTietQuyetDinhNangThamNienNghe> ListChiTietQuyetDinhNangThamNienNghe
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangThamNienNghe>("ListChiTietQuyetDinhNangThamNienNghe");
            }
        }

        public QuyetDinhNangThamNienNghe(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh != null)
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhNangThamNienNghe;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }

}
